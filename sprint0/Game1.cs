using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint0.Classes;
using sprint0.Interfaces;
using sprint0.PlayerStates;
using sprint0.Sprites;
using sprint0.Commands;
using sprint0.Collisions;
using System;
using System.Collections.Generic;
using System.IO;
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

    // 1x1 white texture used for minimap overlay
    _minimapOverlay = new Texture2D(GraphicsDevice, 1, 1);
    _minimapOverlay.SetData(new[] { Color.White });

    // UI font for minimap labels
    _uiFont = Content.Load<SpriteFont>("Font/font"); // matches Content/Font/font.xnb

            sprint0.Sprites.Texture2DStorage.LoadAllTextures(Content, GraphicsDevice);
            
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

        controllers = new List<IController>();
        keyboard = new KeyboardController(this, null);
        controllers.Add(keyboard);

        // Map: 0..16 -> Rooms 1..17 (row-major in a 3x6 grid)
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

        foreach (var controller in controllers)
        {
            controller.Update();
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
        enemy.Draw(_spriteBatch, new Vector2(400, 100));
        item.Draw(_spriteBatch, new Vector2(200, 100));
        link.Draw(_spriteBatch);
        // Draw the minimap rectangle as a translucent red box so it's visible
        if (_minimapOverlay != null)
        {
            _spriteBatch.Draw(_minimapOverlay, mapRect, _minimapColor);
        }

        // Draw simple room numbers inside each minimap cell
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
        int cellW = r.Width / MapCols;
        int cellH = r.Height / MapRows;

        const float scale = 0.55f; // make labels smaller; tweak 0.45–0.70 as needed

        for (int row = 0; row < MapRows; row++)
        {
            for (int col = 0; col < MapCols; col++)
            {
                int idx = row * MapCols + col;         // 0..(MapRows*MapCols-1)
                if (!mapCellCommands.ContainsKey(idx)) // skip unused
                    continue;

                int room = idx + 1;
                string text = $"R{room}";

                // measure and center inside the cell with the chosen scale
                var size = _uiFont.MeasureString(text) * scale;
                var cellX = r.X + col * cellW;
                var cellY = r.Y + row * cellH;
                var pos   = new Vector2(
                    cellX + (cellW - size.X) * 0.5f,
                    cellY + (cellH - size.Y) * 0.5f
                );

                // shadow + text at 'scale'
                sb.DrawString(_uiFont, text, pos + new Vector2(1, 1),
                              Color.Black * 0.7f, 0f, Vector2.Zero, scale,
                              SpriteEffects.None, 0f);

                sb.DrawString(_uiFont, text, pos, _minimapTextColor,
                              0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }
    }

    private void LoadRoom(int roomIndex)
    {
        currentRoomIndex = roomIndex;

        string csv;
        string csvPath = Path.Combine("sprint0", "Content", "Dungeon", $"Room{roomIndex}.csv");

        try
        {
            csv = File.ReadAllText(csvPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load room {roomIndex}: {ex.Message}");
            Console.WriteLine($"Looking for file at: {Path.GetFullPath(csvPath)}");
            throw; // Re-throw so we can see the full error
        }

        dungeon = new sprint0.Sprites.DungeonLoader(BlockFactory.Instance, csv);

    // Populate rectangles / block list used by collision system
    dungeon.LoadRectangles();

        // Position Link (Link exposes 'position')
        link.position = new Microsoft.Xna.Framework.Vector2(100, 100);
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
    collisionUpdater = new CollisionUpdater(dungeon, link);
    collisionUpdater.getList();
    }
}