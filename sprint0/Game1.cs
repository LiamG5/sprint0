
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using sprint0.Interfaces;
using sprint0.Classes;




namespace sprint0;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;


    private List<IController> controllers;

    private Texture2D StandingRightMario;
    private Texture2D WalkingRightMario1;
    private Texture2D WalkingRightMario2;
    private Texture2D WalkingRightMario3;
    public MarioSprite marioSprite;
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
            StandingRightMario = Content.Load<Texture2D>("StandingRightMario");
            WalkingRightMario1 = Content.Load<Texture2D>("WalkingRightMario1");
            WalkingRightMario2 = Content.Load<Texture2D>("WalkingRightMario2");
            WalkingRightMario3 = Content.Load<Texture2D>("WalkingRightMario3");
            font = Content.Load<SpriteFont>("font");

        //strapped to get this done will work on better way to do this later 
        marioSprite = new MarioSprite(
        StandingRightMario, 
        WalkingRightMario1, 
        WalkingRightMario2, 
        WalkingRightMario3, 
        new Vector2(100, 300)
    );

            
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
        marioSprite.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        _spriteBatch.Begin();
        marioSprite.Draw(_spriteBatch);
        _spriteBatch.DrawString(font, "Created by Liam Graham \n Sorce files, class foler", new Vector2(200, 350), Color.White );
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
