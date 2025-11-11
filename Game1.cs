using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint0.Classes;
using sprint0.Commands;
using sprint0.Interfaces;
using sprint0.Managers;
using sprint0.PlayerStates;
using sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.IO;
using static sprint0.Sprites.BlockFactory;
using static sprint0.Sprites.DungeonCarousel;
using static sprint0.Sprites.EnemySpriteFactory;
using sprint0.Collisions;
using sprint0.HUD;
using static sprint0.Sprites.ItemFactory;

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
    private CollisionUpdater collisionUpdater;
    private RoomManager roomManager;
    
    // HUD
    private HUD.HudManager hud;
    private HUD.MinimapHud minimapHud;
    private SpriteFont hudFont;

    // temp HUD data
    private int hearts = 3;
    private int maxHearts = 3;
    private int bombs = 0;
    private int arrows = 0;
    private int rupees = 0;
    private int keys = 0;
    private bool hasMap = true; // TODO: Connect to actual map item
    private string levelName = "Level 1";
    
    // Inventory system
    private bool isInventoryOpen = false;
    private HUD.InventoryMenu inventoryMenu;
    private List<ItemFactory.ItemType> inventoryItems;
    private int selectedInventoryIndex = 0;
    private ItemFactory.ItemType itemInSlotB = ItemFactory.ItemType.Boomerang;
    private ItemLoader itemLoader;

    // Minimap click area
    private Rectangle mapRect = new Rectangle(32, 32, 6 * 24, 3 * 24);
    private const int MapRows = 3;
    private const int MapCols = 6;

    private Dictionary<int, ICommand> mapCellCommands;
    private MouseController mouse;

    private Texture2D _minimapOverlay;
    private static readonly Color _minimapColor = new Color(255, 0, 0, 96);
    private static readonly Color _minimapTextColor = Color.Yellow;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        _graphics.PreferredBackBufferWidth = 768;
        _graphics.PreferredBackBufferHeight = 620;
        _graphics.IsFullScreen = false;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        // Debug:
        System.Console.WriteLine($"[Window Size] Requested: 768x620, Actual: {GraphicsDevice.Viewport.Width}x{GraphicsDevice.Viewport.Height}");
        
        sprint0.Sprites.Texture2DStorage.Init(GraphicsDevice);

        try
        {
            sprint0.Sprites.Texture2DStorage.LoadAllTextures(Content);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("[LoadAllTextures] " + ex);
        }

        hudFont = Content.Load<SpriteFont>("Font/font");

        link = new Link(_spriteBatch);
        roomManager = new RoomManager(link, this);

        hud = new HUD.HudManager();

        hud.Add(new HUD.HudBackground(GraphicsDevice.Viewport.Width, HUD.HudConstants.HudHeight, GraphicsDevice));

        hud.Add(new HUD.LevelLabelHud(() => levelName, hudFont, HUD.HudConstants.LevelLabelPos));
        
        minimapHud = new HUD.MinimapHud(
            () => roomManager.CurrentRoomId,
            () => hasMap,
            (roomId) => roomManager.GetRoomConnections(roomId),
            HUD.HudConstants.MinimapPos,
            GraphicsDevice,
            rows: 3,
            cols: 6,
            cellSize: 14);
        hud.Add(minimapHud);

        hud.Add(new HUD.InventorySlotsHud(
            () => GetItemIcon(itemInSlotB),
            () => sprint0.Sprites.Texture2DStorage.GetTexture("icon_sword"),
            HUD.HudConstants.SlotAPos, HUD.HudConstants.SlotBPos,
            hudFont));

        hud.Add(new HUD.CounterIcon(
            sprint0.Sprites.Texture2DStorage.GetTexture("icon_rupee"),
            () => rupees, hudFont, HUD.HudConstants.CountersPos));

        hud.Add(new HUD.CounterIcon(
            sprint0.Sprites.Texture2DStorage.GetTexture("icon_key"),
            () => keys, hudFont, HUD.HudConstants.CountersPos + new Vector2(0, 1 * HUD.HudConstants.CounterLineHeight)));

        hud.Add(new HUD.CounterIcon(
            sprint0.Sprites.Texture2DStorage.GetTexture("icon_bomb"),
            () => bombs, hudFont, HUD.HudConstants.CountersPos + new Vector2(0, 2 * HUD.HudConstants.CounterLineHeight)));

        hud.Add(new HUD.HeartMeter(() => hearts, () => maxHearts, HUD.HudConstants.HeartsPos, hudFont));

        // Initialize inventory
        inventoryItems = new List<ItemFactory.ItemType>
        {
            ItemFactory.ItemType.Boomerang,
            ItemFactory.ItemType.Bomb,
            ItemFactory.ItemType.Bow,
            ItemFactory.ItemType.Arrow,
            ItemFactory.ItemType.CandleRed,
            ItemFactory.ItemType.Recorder,
            ItemFactory.ItemType.Food,
            ItemFactory.ItemType.PotionRed
        };
        
        inventoryMenu = new HUD.InventoryMenu(
            () => inventoryItems,
            () => itemInSlotB,
            (item) => itemInSlotB = item,
            () => selectedInventoryIndex,
            (index) => selectedInventoryIndex = index,
            () => roomManager,
            () => hearts,
            () => maxHearts,
            () => rupees,
            () => keys,
            () => bombs,
            hudFont,
            GraphicsDevice
        );

        _minimapOverlay = new Texture2D(GraphicsDevice, 1, 1);
        _minimapOverlay.SetData(new[] { Color.White });

        blocks = BlockFactory.Instance;
        enemies = EnemySpriteFactory.Instance;
        items = ItemFactory.Instance;

        blockCarousel = new BlockCarousel(blocks, _spriteBatch);
        enemyCarousel = new EnemyCarousel(enemies, _spriteBatch);
        itemCarousel = new ItemCarousel(items, _spriteBatch);

        string dungeonPath = Path.Combine(Content.RootDirectory, "dungeon.csv");
        itemLoader = new ItemLoader(items);
        dungeon = new DungeonLoader(blocks, itemLoader, File.ReadAllText(dungeonPath));
        
        dungeon.LoadRectangles();
        
        tile = blockCarousel.GetCurrentBlock();
        enemy = enemyCarousel.GetCurrentEnemy();
        item = itemCarousel.GetCurrentItem();

        try
        {
            Texture2D enemySheet = sprint0.Sprites.Texture2DStorage.GetEnemiesSpriteSheet();
            var testEnemy = new EnemyGel(enemySheet, new Vector2(400, 100));
            dungeon.AddEnemy(testEnemy);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("[EnemySheet Load] " + ex);
        }

        try
        {
            font = Content.Load<SpriteFont>("Font/font");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("[Font Load] " + ex);
        }

        controllers = new List<IController>();
        keyboard = new KeyboardController(this, null, () => isInventoryOpen);
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

        collisionUpdater = new CollisionUpdater(dungeon, link);
        collisionUpdater.getList();

        previousKeyboardState = Keyboard.GetState();
        previousMouseState = Mouse.GetState();

        System.Console.WriteLine("[LoadContent] completed");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            if (isInventoryOpen)
            {
                isInventoryOpen = false;
            }
            else
            {
                Exit();
            }
        }

        // Pause game updates when inventory is open
        if (!isInventoryOpen)
        {
            link.Update(gameTime);
            collisionUpdater.Update();
            dungeon.Update(gameTime);
        }
        else
        {
            // Update inventory menu when open
            inventoryMenu?.Update(gameTime);
        }

        foreach (var controller in controllers)
        {
            controller.Update();
        }
        
        // Handle minimap room clicks
        HandleMinimapClicks();

        foreach (var e in dungeon.GetEnemies())
        {
            e.Update(gameTime);
        }

        if (!isInventoryOpen)
        {
            enemy.Update(gameTime);
            item.Update(gameTime);
        }
        hud?.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
{
    GraphicsDevice.Clear(Color.CornflowerBlue);

    if (isInventoryOpen)
    {
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        inventoryMenu?.Draw(_spriteBatch);
        _spriteBatch.End();
    }
    else
    {
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        hud?.Draw(_spriteBatch);
        _spriteBatch.End();

        Matrix transform = Matrix.CreateTranslation(0, HUD.HudConstants.HudHeight, 0);
        
        _spriteBatch.Begin(
            samplerState: SamplerState.PointClamp,
            transformMatrix: transform);

        if (dungeon != null)
        {
            dungeon.Draw(_spriteBatch, GraphicsDevice);

            foreach (var e in dungeon.GetEnemies())
            {
                if (!e.IsDead())
                    e.Draw(_spriteBatch, e.GetPosition());
            }
        }

        if (enemy != null)
            enemy.Draw(_spriteBatch, new Vector2(400, 100));
        if (item != null)
            item.Draw(_spriteBatch, new Vector2(200, 100));
        if (link != null)
            link.Draw(_spriteBatch);

        if (_minimapOverlay != null)
            _spriteBatch.Draw(_minimapOverlay, mapRect, _minimapColor);

        DrawMinimapNumbers(_spriteBatch);
        itemLoader.Draw(_spriteBatch);
        _spriteBatch.End();
    }

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

    private void LoadRoom(int roomIndex)
    {
        var csvPath = System.IO.Path.Combine("Content", "Dungeon", $"Room{roomIndex}.csv");
        if (!System.IO.File.Exists(csvPath))
        {
            System.Console.WriteLine($"[LoadRoom] Missing CSV: {csvPath}");
            return;
        }
        var csv = System.IO.File.ReadAllText(csvPath);
        dungeon = new DungeonLoader(BlockFactory.Instance, itemLoader, csv);
        dungeon.LoadRectangles();
        
        if (roomManager != null)
        {
            roomManager.SetCurrentRoom(roomIndex);
            dungeon.SetRoomManager(roomManager, roomIndex);
            itemLoader.LoadItems(roomIndex);
        }
         csvPath = (roomIndex == 1)
        ? System.IO.Path.Combine("Content", "dungeon.csv")
        : System.IO.Path.Combine("Content", "Dungeon", $"Room{roomIndex}.csv");

    if (!System.IO.File.Exists(csvPath))
    {
        System.Console.WriteLine($"[LoadRoom] Missing CSV: {csvPath}");
        return;
    }

    csv = System.IO.File.ReadAllText(csvPath);
    dungeon = new DungeonLoader(BlockFactory.Instance, itemLoader, csv);
    dungeon.LoadRectangles();

    if (roomManager != null)
    {
        roomManager.SetCurrentRoom(roomIndex);
        dungeon.SetRoomManager(roomManager, roomIndex);
    }
        
        collisionUpdater = new CollisionUpdater(dungeon, link);
        System.Console.WriteLine($"[LoadRoom] Loaded Room {roomIndex}");
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
        keyboard = new KeyboardController(this, null, () => isInventoryOpen);
        controllers.Add(keyboard);

        roomManager = new RoomManager(link, this);

        previousKeyboardState = Keyboard.GetState();

        dungeon.LoadRectangles();

        Texture2D enemySheet = sprint0.Sprites.Texture2DStorage.GetEnemiesSpriteSheet();
        var testEnemy = new EnemyGel(enemySheet, new Vector2(400, 100));
        dungeon.AddEnemy(testEnemy);

        collisionUpdater = new CollisionUpdater(dungeon, link);
        collisionUpdater.getList();
    }

    private MouseState previousMouseState;
    
    private void HandleMinimapClicks()
    {
        if (minimapHud == null || !hasMap) return;
        
        var currentMouse = Mouse.GetState();
        var pos = new Point(currentMouse.X, currentMouse.Y);
        
        if (currentMouse.LeftButton == ButtonState.Pressed && 
            previousMouseState.LeftButton == ButtonState.Released)
        {
            var roomNum = minimapHud.GetRoomAtPoint(pos);
            if (roomNum.HasValue && roomNum.Value >= 1 && roomNum.Value <= 17)
            {
                LoadRoom(roomNum.Value);
            }
        }
        
        previousMouseState = currentMouse;
    }

    private void DrawMinimapNumbers(SpriteBatch sb)
    {
        if (font == null) return;

        int cellW = mapRect.Width / MapCols;
        int cellH = mapRect.Height / MapRows;
        const float scale = 0.55f;

        for (int row = 0; row < MapRows; row++)
        {
            for (int col = 0; col < MapCols; col++)
            {
                int idx = row * MapCols + col;
                if (!mapCellCommands.ContainsKey(idx))
                    continue;

                string text = $"R{idx + 1}";
                var size = font.MeasureString(text) * scale;

                var pos = new Vector2(
                    mapRect.X + col * cellW + (cellW - size.X) * 0.5f,
                    mapRect.Y + row * cellH + (cellH - size.Y) * 0.5f
                );

                sb.DrawString(font, text, pos + new Vector2(1, 1),
                              Color.Black * 0.7f, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

                sb.DrawString(font, text, pos, _minimapTextColor,
                              0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }
    }
    
    private Texture2D GetItemIcon(ItemFactory.ItemType itemType)
    {
        return itemType switch
        {
            ItemFactory.ItemType.Boomerang => sprint0.Sprites.Texture2DStorage.GetTexture("icon_boomerang"),
            ItemFactory.ItemType.Bomb => sprint0.Sprites.Texture2DStorage.GetTexture("icon_bomb"),
            ItemFactory.ItemType.Bow => sprint0.Sprites.Texture2DStorage.GetTexture("icon_bow"),
            ItemFactory.ItemType.Arrow => sprint0.Sprites.Texture2DStorage.GetTexture("icon_arrow"),
            ItemFactory.ItemType.CandleRed => sprint0.Sprites.Texture2DStorage.GetTexture("icon_candle"),
            ItemFactory.ItemType.CandleBlue => sprint0.Sprites.Texture2DStorage.GetTexture("icon_candle"),
            ItemFactory.ItemType.Recorder => sprint0.Sprites.Texture2DStorage.GetTexture("icon_recorder"),
            ItemFactory.ItemType.Food => sprint0.Sprites.Texture2DStorage.GetTexture("icon_food"),
            ItemFactory.ItemType.PotionRed => sprint0.Sprites.Texture2DStorage.GetTexture("icon_potion"),
            ItemFactory.ItemType.PotionBlue => sprint0.Sprites.Texture2DStorage.GetTexture("icon_potion"),
            ItemFactory.ItemType.MagicalRod => sprint0.Sprites.Texture2DStorage.GetTexture("icon_rod"),
            ItemFactory.ItemType.Sword => sprint0.Sprites.Texture2DStorage.GetTexture("icon_sword"),
            _ => null
        };
    }
    
    public void ToggleInventoryMenu()
    {
        isInventoryOpen = !isInventoryOpen;
        if (isInventoryOpen)
        {
            selectedInventoryIndex = 0;
        }
    }
    
    public void NavigateInventory(InventoryNavigateCommand.Direction direction)
    {
        if (!isInventoryOpen || inventoryItems == null || inventoryItems.Count == 0)
            return;
        
        const int cols = 4;
        const int rows = 2;
        int maxItems = Math.Min(inventoryItems.Count, 8);
        
        int currentRow = selectedInventoryIndex / cols;
        int currentCol = selectedInventoryIndex % cols;
        
        switch (direction)
        {
            case InventoryNavigateCommand.Direction.Up:
                currentRow = (currentRow - 1 + rows) % rows;
                break;
            case InventoryNavigateCommand.Direction.Down:
                currentRow = (currentRow + 1) % rows;
                break;
            case InventoryNavigateCommand.Direction.Left:
                currentCol = (currentCol - 1 + cols) % cols;
                break;
            case InventoryNavigateCommand.Direction.Right:
                currentCol = (currentCol + 1) % cols;
                break;
        }
        
        int newIndex = currentRow * cols + currentCol;
        if (newIndex < maxItems)
        {
            selectedInventoryIndex = newIndex;
        }
    }
    
    public void SelectInventoryItem()
    {
        if (!isInventoryOpen || inventoryItems == null || selectedInventoryIndex < 0 || selectedInventoryIndex >= inventoryItems.Count)
            return;
        
        itemInSlotB = inventoryItems[selectedInventoryIndex];
        
        isInventoryOpen = false;
    }
}
