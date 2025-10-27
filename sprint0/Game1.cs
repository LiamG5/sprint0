using System;
using System.IO;
using System.Collections.Generic;
using sprint0.Commands;
using sprint0.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint0.Interfaces;
using sprint0.PlayerStates;
using sprint0.Sprites;
using sprint0.Collisions;
using static sprint0.Sprites.BlockFactory;
using static sprint0.Sprites.DungeonCarousel;
using static sprint0.Sprites.EnemySpriteFactory;


namespace sprint0;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private List<IController> controllers;

    public ISprite tile;
    public ISprite enemy;
    public ISprite item;
    private DungeonLoader dungeon;
    private BlockFactory blocks;
    private EnemySpriteFactory enemies;
    private ItemFactory items;
    public BlockCarousel blockCarousel;
    public EnemyCarousel enemyCarousel;
    public ItemCarousel itemCarousel;
    private Texture2D dungeonBorder;
    private Texture2D linkSheet;
    private Texture2D bossSheet;
    private Texture2D enemiesSheet;
    private Texture2D miscSheet;
    private Texture2D itemSheet;
    private Texture2D blockSheet;
    private SpriteFont font;
    public Link link;
    private KeyboardController keyboard;
    public int state;
    private KeyboardState previousKeyboardState;

    // Minimap debug overlay (visible color fill)
    private Texture2D _minimapOverlay;
    private static readonly Color _minimapColor = new Color(255, 0, 0, 96); // semi-transparent red

    // UI font for simple labels
    private SpriteFont _uiFont;
    private static readonly Color _minimapTextColor = Color.Yellow;

    // Minimap config
    private Rectangle mapRect = new Rectangle(32, 32, 6 * 24, 3 * 24);
    private const int MapRows = 3;
    private const int MapCols = 6;

    // Mouse + map
    private Dictionary<int, ICommand> mapCellCommands;
    private MouseController mouse;

    // Optional
    private int currentRoomIndex = 1;

    // Collision updater
    private CollisionUpdater collisionUpdater;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Initialize controllers list
        controllers = new List<IController>();

        // Load textures with GraphicsDevice (safe loader already supports this)
        sprint0.Sprites.Texture2DStorage.LoadAllTextures(Content, GraphicsDevice);

        // Build map index → command table (0..16 → Room1..Room17)
        mapCellCommands = new Dictionary<int, ICommand>
        {
            {  0, new GoToRoom1Command(this)  }, {  1, new GoToRoom2Command(this)  }, {  2, new GoToRoom3Command(this)  },
            {  3, new GoToRoom4Command(this)  }, {  4, new GoToRoom5Command(this)  }, {  5, new GoToRoom6Command(this)  },
            {  6, new GoToRoom7Command(this)  }, {  7, new GoToRoom8Command(this)  }, {  8, new GoToRoom9Command(this)  },
            {  9, new GoToRoom10Command(this) }, { 10, new GoToRoom11Command(this) }, { 11, new GoToRoom12Command(this) },
            { 12, new GoToRoom13Command(this) }, { 13, new GoToRoom14Command(this) }, { 14, new GoToRoom15Command(this) },
            { 15, new GoToRoom16Command(this) }, { 16, new GoToRoom17Command(this) },
        };

        // Mouse controller for the minimap
        mouse = new MouseController(mapRect, MapRows, MapCols, mapCellCommands);
        controllers.Add(mouse);

        // Minimap overlay: 1x1 white texture we tint
        _minimapOverlay = new Texture2D(GraphicsDevice, 1, 1);
        _minimapOverlay.SetData(new[] { Color.White });

        // UI font for simple labels (Content/Font/font.xnb)
        _uiFont = Content.Load<SpriteFont>("Font/font");

        link = new Link(_spriteBatch);
        
        blocks = BlockFactory.Instance;
        enemies = EnemySpriteFactory.Instance;
        items = ItemFactory.Instance;
        
        blockCarousel = new BlockCarousel(blocks, _spriteBatch);
        enemyCarousel = new EnemyCarousel(enemies, _spriteBatch);
        itemCarousel = new ItemCarousel(items, _spriteBatch);

            // dungeon will be loaded per-room in LoadRoom

            tile = blockCarousel.GetCurrentBlock();
            enemy = enemyCarousel.GetCurrentEnemy();
            item = itemCarousel.GetCurrentItem();

        keyboard = new KeyboardController(this, null);
        controllers.Add(keyboard);
            
            Texture2D enemySheet = sprint0.Sprites.Texture2DStorage.GetEnemiesSpriteSheet();
            var testEnemy = new EnemyGel(enemySheet, new Vector2(400, 100));
            dungeon.AddEnemy(testEnemy);

            controllers = new List<IController>();
            keyboard = new KeyboardController(this, null);
            controllers.Add(keyboard);

        previousKeyboardState = Keyboard.GetState();

        // Start in Room 1
        LoadRoom(1);

        // Initialize collision updater now that dungeon and link exist
        collisionUpdater = new CollisionUpdater(dungeon, link);
        collisionUpdater.getList();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        link.Update(gameTime);
        collisionUpdater.Update();
        dungeon.Update(gameTime);

        foreach (var controller in controllers)
        {
            controller.Update();
        }

        foreach (var e in dungeon.GetEnemies())
        {
            e.Update(gameTime);
        }

        enemy.Update(gameTime);
        item.Update(gameTime);

        base.Update(gameTime);
    }


    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        _spriteBatch.Begin();

        dungeon.Draw(_spriteBatch, GraphicsDevice);
        
        foreach (var e in dungeon.GetEnemies())
        {
            if (!e.IsDead())
            {
                e.Draw(_spriteBatch, e.GetPosition());
            }
        }
        
        enemy.Draw(_spriteBatch, new Vector2(400, 100));
        item.Draw(_spriteBatch, new Vector2(200, 100));
        link.Draw(_spriteBatch);

        if (_minimapOverlay != null)
            _spriteBatch.Draw(_minimapOverlay, mapRect, _minimapColor);
        DrawMinimapNumbers(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    public void GoToRoom1()  => LoadRoom(1);
    public void GoToRoom2()  => LoadRoom(2);
    public void GoToRoom3()  => LoadRoom(3);
    public void GoToRoom4()  => LoadRoom(4);
    public void GoToRoom5()  => LoadRoom(5);
    public void GoToRoom6()  => LoadRoom(6);
    public void GoToRoom7()  => LoadRoom(7);
    public void GoToRoom8()  => LoadRoom(8);
    public void GoToRoom9()  => LoadRoom(9);
    public void GoToRoom10() => LoadRoom(10);
    public void GoToRoom11() => LoadRoom(11);
    public void GoToRoom12() => LoadRoom(12);
    public void GoToRoom13() => LoadRoom(13);
    public void GoToRoom14() => LoadRoom(14);
    public void GoToRoom15() => LoadRoom(15);
    public void GoToRoom16() => LoadRoom(16);
    public void GoToRoom17() => LoadRoom(17);

    private void DrawMinimapNumbers(SpriteBatch sb)
    {
        if (_uiFont == null) return;

        var r = mapRect;
        int cellW = r.Width / MapCols, cellH = r.Height / MapRows;
        const float scale = 0.55f;

        for (int row = 0; row < MapRows; row++)
        for (int col = 0; col < MapCols; col++)
        {
            int idx = row * MapCols + col;
            if (!mapCellCommands.ContainsKey(idx)) continue;

            string text = $"R{idx + 1}";
            var size = _uiFont.MeasureString(text) * scale;
            var pos = new Vector2(
                r.X + col * cellW + (cellW - size.X) * 0.5f,
                r.Y + row * cellH + (cellH - size.Y) * 0.5f
            );

            sb.DrawString(_uiFont, text, pos + new Vector2(1, 1), Color.Black * 0.7f, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            sb.DrawString(_uiFont, text, pos, _minimapTextColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
    private void LoadRoom(int roomIndex)
    {
        currentRoomIndex = roomIndex;

        string csv;
        try
        {
            using var s = Microsoft.Xna.Framework.TitleContainer.OpenStream($"Content/Dungeon/Room{roomIndex}.csv");
            using var r = new StreamReader(s);
            csv = r.ReadToEnd();
        }
        catch
        {
            var p = Path.Combine(Content.RootDirectory, "Dungeon", $"Room{roomIndex}.csv");
            if (File.Exists(p))
                csv = File.ReadAllText(p);
            else
            {
                // 12x7 Void grid fallback
                csv = string.Join("\n",
                    System.Linq.Enumerable.Repeat(string.Join(",", System.Linq.Enumerable.Repeat("Void", 12)), 7));
            }
        }

        dungeon = new sprint0.Sprites.DungeonLoader(BlockFactory.Instance, csv);
        if (link != null)
            link.position = new Vector2(100, 100);
    }

    public void ResetGame()
    {
        link = new Link(_spriteBatch);
        
        blockCarousel = new BlockCarousel(blocks, _spriteBatch);
        enemyCarousel = new EnemyCarousel(enemies, _spriteBatch);
        itemCarousel = new ItemCarousel(items, _spriteBatch);
        
        tile = blockCarousel.GetCurrentBlock();
        enemy = enemyCarousel.GetCurrentEnemy();
        item = itemCarousel.GetCurrentItem();

        controllers = new List<IController>();
        keyboard = new KeyboardController(this, null);
        controllers.Add(keyboard);

        mapCellCommands = new Dictionary<int, ICommand>
        {
            {  0, new GoToRoom1Command(this)  },
            {  1, new GoToRoom2Command(this)  },
            {  2, new GoToRoom3Command(this)  },
            {  3, new GoToRoom4Command(this)  },
            {  4, new GoToRoom5Command(this)  },
            {  5, new GoToRoom6Command(this)  },
            {  6, new GoToRoom7Command(this)  },
            {  7, new GoToRoom8Command(this)  },
            {  8, new GoToRoom9Command(this)  },
            {  9, new GoToRoom10Command(this) },
            { 10, new GoToRoom11Command(this) },
            { 11, new GoToRoom12Command(this) },
            { 12, new GoToRoom13Command(this) },
            { 13, new GoToRoom14Command(this) },
            { 14, new GoToRoom15Command(this) },
            { 15, new GoToRoom16Command(this) },
            { 16, new GoToRoom17Command(this) },
        };

    mouse = new MouseController(mapRect, MapRows, MapCols, mapCellCommands);
    controllers.Add(mouse);
        
    previousKeyboardState = Keyboard.GetState();

    // Start in Room 1 after reset
    LoadRoom(1);

    // Recreate collision updater for the new dungeon/link
        
        Texture2D enemySheet = sprint0.Sprites.Texture2DStorage.GetEnemiesSpriteSheet();
        var testEnemy = new EnemyGel(enemySheet, new Vector2(400, 100));
        dungeon.AddEnemy(testEnemy);
        
    collisionUpdater = new CollisionUpdater(dungeon, link);
    collisionUpdater.getList();
    }
}