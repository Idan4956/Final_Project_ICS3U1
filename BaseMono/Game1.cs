using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using GameUtility;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace BaseMono;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    //Put global Variables here
    //basic screen info
    int screenWidth;
    int screenHeight;
    int targetFPS = 30;

    //Random num generator
    static Random rng = new Random();

    int gameState = 0;

    public const int INTRO = 0;
    public const int PLAYERNUMSELECT = 1;
    public const int CLASSSELECT = 2;
    public const int EVENT = 3;
    public const int FIGHT = 4;
    public const int INVENTORY = 5;
    public const int VICTORY = 6;
    public const int DEFEAT = 7;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
       
        //Setup Screen Dimensions
        _graphics.PreferredBackBufferWidth = 1200;
        _graphics.PreferredBackBufferHeight = 800;
        _graphics.ApplyChanges();

        //FPS controller
        _graphics.SynchronizeWithVerticalRetrace = false;
        IsFixedTimeStep = true;
        TargetElapsedTime = TimeSpan.FromMilliseconds(1000.0f/ targetFPS);

        //Storing the Screen dimensions
        screenWidth = _graphics.GraphicsDevice.Viewport.Width;
        screenHeight = _graphics.GraphicsDevice.Viewport.Height;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        float timePassed = (float)gameTime.ElapsedGameTime.TotalSeconds;

        switch(gameState)
        {
            case INTRO:
            //Put on title Screen
            break;

            case PLAYERNUMSELECT:
            //Put player on game set up screen
            break;

            case CLASSSELECT:
            //Put player on class selection Screen
            break;

            case EVENT:
            //Put player on event selection Screen
            break;

            case FIGHT:
            //Put player on fight Screen
            break;

            case INVENTORY:
            //Put player on inventory Screen
            break;

            case VICTORY:
            //Put player on victory Screen
            break;

            case DEFEAT:
            //Put player on defeat screen
            break;
    

        }
            
    
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        
        base.Draw(gameTime);
    }
}
