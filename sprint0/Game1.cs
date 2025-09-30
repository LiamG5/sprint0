
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using sprint0.Interfaces;
using sprint0.Classes;
using sprint0.Player;




namespace sprint0;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private LinkSprite linkSprite;
    private Link link;

    private List<IController> controllers;

    private Texture2D linkSheet;
    private Texture2D bossSheet;
    private Texture2D enemiesSheet;
    private Texture2D miscSheet;
    private Texture2D itemSheet;
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
        

        base.Initialize();
    }

    protected override void LoadContent()
    {
        Texture2DStorage.LoadAllTextures(Content);
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        font = Texture2DStorage.GetFont();
        linkSprite = new LinkSprite(Texture2DStorage.GetLinkSpriteSheet());
        link = new Link(_spriteBatch, linkSprite);
        

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
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        _spriteBatch.Begin();
        link.Update();
        _spriteBatch.DrawString(font, "Created by team 6, the goo crew", new Vector2(200, 350), Color.White );
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
