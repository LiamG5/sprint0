using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint0.Classes;
using sprint0.Interfaces;
using sprint0.PlayerStates;
using sprint0.Sprites;
using System;
using System.Collections.Generic;
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
    
    private CollisionResponse collisionResponse;

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

        sprint0.Sprites.Texture2DStorage.LoadAllTextures(Content);
        
        link = new Link(_spriteBatch);
        
        blocks = BlockFactory.Instance;
        enemies = EnemySpriteFactory.Instance;
        items = ItemFactory.Instance;
        
        blockCarousel = new BlockCarousel(blocks, _spriteBatch);
        enemyCarousel = new EnemyCarousel(enemies, _spriteBatch);
        itemCarousel = new ItemCarousel(items, _spriteBatch);

        dungeon = new DungeonLoader(blocks, @"Content/dungeon.csv");

        tile = blockCarousel.GetCurrentBlock();
        enemy = enemyCarousel.GetCurrentEnemy();
        item = itemCarousel.GetCurrentItem();

        controllers = new List<IController>();
        keyboard = new KeyboardController(this, null);
        controllers.Add(keyboard);
        
        previousKeyboardState = Keyboard.GetState();
        
        collisionResponse = new CollisionResponse();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        link.Update(gameTime);
        
        foreach (var controller in controllers)
        {
            controller.Update();
        }
        
        // Store old position for collision response
        Vector2 oldPosition = link.position;
        Vector2 newPosition = link.position + link.velocity;
        
        // Simple collision detection - prevent movement into solid blocks
        List<ICollidable> collidableObjects = dungeon.GetCollidableObjects();
        
        // Check for collisions with the new position
        Rectangle newLinkBounds = new Rectangle((int)newPosition.X, (int)newPosition.Y, 48, 48);
        bool collisionDetected = false;
        
        // Debug: Print Link's bounds
        System.Console.WriteLine($"Link bounds: {newLinkBounds}");
        System.Console.WriteLine($"Collidable objects count: {collidableObjects.Count}");
        
        foreach (ICollidable collidable in collidableObjects)
        {
            Rectangle collidableBounds = collidable.GetBounds();
            System.Console.WriteLine($"Block at: {collidableBounds}, Solid: {collidable.IsSolid()}, Position: {collidable.GetPosition()}");
            
            if (collidable.IsSolid())
            {
                if (newLinkBounds.Intersects(collidableBounds))
                {
                    // Collision detected - don't move
                    System.Console.WriteLine("COLLISION DETECTED!");
                    collisionDetected = true;
                    break;
                }
            }
        }
        
        // Only update position if no collision
        if (!collisionDetected)
        {
            link.position = newPosition;
        }
        // If collision detected, keep the old position (don't update)


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
        _spriteBatch.End();
        base.Draw(gameTime);
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
        
        previousKeyboardState = Keyboard.GetState();
        
        collisionResponse = new CollisionResponse();
    }
}