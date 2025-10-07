
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using sprint0.Interfaces;
using sprint0.Classes;
using sprint0.Sprites;
using System;
using static sprint0.Sprites.BlockFactory;
using static sprint0.Sprites.EnemySpriteFactory;




namespace sprint0;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private List<IController> controllers;

    public ISprite tile;
    public ISprite enemy;
    private BlockFactory blocks;
    private EnemySpriteFactory enemies;
    private Texture2D linkSheet;
    private Texture2D bossSheet;
    private Texture2D enemiesSheet;
    private Texture2D miscSheet;
    private Texture2D itemSheet;
    private Texture2D blockSheet;
    public SpriteMain marioSprite;
    private SpriteFont font;
    public int state;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Texture2DStorage.LoadAllTextures(Content);
        blocks = BlockFactory.Instance; // Initialize Block Factory
        enemies = EnemySpriteFactory.Instance;
        tile = blocks.BuildTileBlock(_spriteBatch); // Assign initial Block
        enemy = enemies.SpawnGel(_spriteBatch);
        
        controllers = new List<IController>
            {
             new KeyboardController(this, marioSprite),
                new MouseController(this, marioSprite)
            }; 
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

         foreach (var controller in controllers)
    {
        controller.Update();
        }

        // Updating block state forward
        if (Keyboard.GetState().IsKeyDown(Keys.Y))
        {
            BlockType currBlock = blocks.getBlock();
            if (currBlock.Equals(BlockType.Tile))
            {
                tile = blocks.BuildChiseledTileBlock(_spriteBatch);
            }
            else if (currBlock.Equals(BlockType.ChiseledTile))
            {
                tile = blocks.BuildFishBlock(_spriteBatch);
            }
            else if (currBlock.Equals(BlockType.Fish))
            {
                tile = blocks.BuildDragonBlock(_spriteBatch);
            }
            else if (currBlock.Equals(BlockType.Dragon))
            {
                tile = blocks.BuildVoidBlock(_spriteBatch);
            }
            else if (currBlock.Equals(BlockType.Void))
            {
                tile = blocks.BuildDirtBlock(_spriteBatch);
            }
            else if (currBlock.Equals(BlockType.Dirt))
            {
                tile = blocks.BuildSolidBlock(_spriteBatch);
            }
            else if (currBlock.Equals(BlockType.Solid))
            {
                tile = blocks.BuildStairBlock(_spriteBatch);
            }
            else if (currBlock.Equals(BlockType.Stair))
            {
                tile = blocks.BuildBrickBlock(_spriteBatch);
            }
            else if (currBlock.Equals(BlockType.Brick))
            {
                tile = blocks.BuildGrateBlock(_spriteBatch);
            }
            else
            {
                tile = blocks.BuildTileBlock(_spriteBatch);
            }
        }

        // Updating block state backward
        if (Keyboard.GetState().IsKeyDown(Keys.T))
        {
            BlockType currBlock = blocks.getBlock();
            if (currBlock.Equals(BlockType.Tile))
            {
                tile = blocks.BuildGrateBlock(_spriteBatch);
            }
            else if (currBlock.Equals(BlockType.ChiseledTile))
            {
                tile = blocks.BuildTileBlock(_spriteBatch);
            }
            else if (currBlock.Equals(BlockType.Fish))
            {
                tile = blocks.BuildChiseledTileBlock(_spriteBatch);
            }
            else if (currBlock.Equals(BlockType.Dragon))
            {
                tile = blocks.BuildFishBlock(_spriteBatch);
            }
            else if (currBlock.Equals(BlockType.Void))
            {
                tile = blocks.BuildDragonBlock(_spriteBatch);
            }
            else if (currBlock.Equals(BlockType.Dirt))
            {
                tile = blocks.BuildVoidBlock(_spriteBatch);
            }
            else if (currBlock.Equals(BlockType.Solid))
            {
                tile = blocks.BuildDirtBlock(_spriteBatch);
            }
            else if (currBlock.Equals(BlockType.Stair))
            {
                tile = blocks.BuildSolidBlock(_spriteBatch);
            }
            else if (currBlock.Equals(BlockType.Brick))
            {
                tile = blocks.BuildStairBlock(_spriteBatch);
            }
            else
            {
                tile = blocks.BuildBrickBlock(_spriteBatch);
            }
        }

        // Updating enemy type forward
        if (Keyboard.GetState().IsKeyDown(Keys.O))
        {
            switch (enemies.getEnemy())
            {
                case EnemyType.BladeTrap:
                    enemy = enemies.SpawnGel(_spriteBatch);
                    break;
                case EnemyType.Gel:
                    enemy = enemies.SpawnKeese(_spriteBatch);
                    break;
                case EnemyType.Keese:
                    enemy = enemies.SpawnRedGoriya(_spriteBatch);
                    break;
                case EnemyType.RedGoriya:
                    enemy = enemies.SpawnStalfos(_spriteBatch);
                    break;
                case EnemyType.Stalfos:
                    enemy = enemies.SpawnWallmaster(_spriteBatch);
                    break;
                case EnemyType.Wallmaster:
                    enemy = enemies.SpawnBladeTrap(_spriteBatch);
                    break;

            }
        }

        // Updating enemy type backward
        if (Keyboard.GetState().IsKeyDown(Keys.P))
        {
            switch (enemies.getEnemy())
            {
                case EnemyType.BladeTrap:
                    enemy = enemies.SpawnWallmaster(_spriteBatch);
                    break;
                case EnemyType.Gel:
                    enemy = enemies.SpawnBladeTrap(_spriteBatch);
                    break;
                case EnemyType.Keese:
                    enemy = enemies.SpawnGel(_spriteBatch);
                    break;
                case EnemyType.RedGoriya:
                    enemy = enemies.SpawnKeese(_spriteBatch);
                    break;
                case EnemyType.Stalfos:
                    enemy = enemies.SpawnRedGoriya(_spriteBatch);
                    break;
                case EnemyType.Wallmaster:
                    enemy = enemies.SpawnStalfos(_spriteBatch);
                    break;

            }
        }

        enemy.Update(gameTime);

        base.Update(gameTime);
    }


    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        _spriteBatch.Begin();
        
        tile.Draw(_spriteBatch); // Draw Blocks
        enemy.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
