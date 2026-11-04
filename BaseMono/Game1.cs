using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using GameUtility;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System.Security.Cryptography.X509Certificates;
using System.Net.Mime;

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

    int gameState = 1;

    public const int INTRO = 0;
    public const int PLAYERNUMSELECT = 1;
    public const int CLASSSELECT = 2;
    public const int EVENT = 3;
    public const int FIGHT = 4;
    public const int INVENTORY = 5;
    public const int VICTORY = 6;
    public const int DEFEAT = 7;
    
    Texture2D forestBG;

    Rectangle forestBGRec;

    ////Player Num Select
    int playerSelectNum = 1;

    string playerSelect = "1";
    string playerSelectUp = "+";
    string playerSelectDown = "-";
    string playerSelectConfirm = "Confirm";

    Vector2 playerSelectLoc =  new Vector2(500, 500);
    Vector2 playerSelectUpLoc =  new Vector2(450, 500);
    Vector2 playerSelectDownLoc =  new Vector2(550, 500);
    Vector2 playerSelectConfirmLoc =  new Vector2(500, 600);

    Rectangle playerSelectUpRec = new Rectangle (450, 500, 50, 50);
    Rectangle playerSelectDownRec = new Rectangle (550, 500, 50, 50);
    Rectangle playerSelectConfirmRec = new Rectangle (500, 600, 100, 50);

    //Class Select
    
    // Screens
    ClassSelectScreen classSelectScreen;

    //Input stuff
    MouseState mouse;
    MouseState prevMouse;

    SpriteFont font;

    //troop stuff
    List<Combatant> playerTroops = new List<Combatant>();

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

        classSelectScreen = new ClassSelectScreen();

        // Temporary smoke test: keep it out of the normal startup path so the game can run.
        // Call RunCombatSmokeTest() manually when you want to validate combat logic.

        base.Initialize();
    }

    private static void RunCombatSmokeTest()
    {
        var caster = new wizard("Smoke test caster") { currentMana = 4 };
        var target = new fighter("Smoke test target");

        Combatant[] targets = {caster, target};
        
        var turnManager = new TurnManager();
        turnManager.allCombatants.Add(caster);
        turnManager.allCombatants.Add(target);

        Combatant[] selectedTargets = {target};

        double startingHealth = target.currentHealth;

        caster.useSkillTwo(selectedTargets, rng, turnManager);

        if (caster.currentMana != 2 || target.currentHealth >= startingHealth ||
            target.poisonStacks != 2 || turnManager.currentCharacter != 1)
        {
            throw new InvalidOperationException("Combat smoke test failed.");
        }

        System.Diagnostics.Debug.WriteLine("Combat smoke test passed: skill cost, damage, poison, and turn advance.");
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        forestBG = Content.Load<Texture2D>("Images/Backgrounds/ForestBG");

        forestBGRec = new Rectangle (0, 0, screenWidth, screenHeight);

        font = Content.Load<SpriteFont>("Fonts/SignFont.spritesheet");
            
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
            prevMouse = mouse;
            mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed && !(prevMouse.LeftButton == ButtonState.Pressed))
            {
                if (playerSelectUpRec.Contains(mouse.Position) && playerSelectNum < 5)
                {
                    playerSelectNum = Convert.ToInt32(playerSelect) + 1;
                    playerSelect = Convert.ToString(playerSelectNum);   
                }
                else if (playerSelectDownRec.Contains(mouse.Position) && playerSelectNum > 1)
                {
                    playerSelectNum = Convert.ToInt32(playerSelect) - 1;
                    playerSelect = Convert.ToString(playerSelectNum); 
                }
                else if (playerSelectConfirmRec.Contains(mouse.Position))
                {
                    gameState = CLASSSELECT;
                }
            }

            break;

            case CLASSSELECT:
            //Put player on class selection Screen

            classSelectScreen.update(gameTime);

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
        _spriteBatch.Begin();
        
        _spriteBatch.Draw(forestBG, forestBGRec, Color.White);

        //Draw in player num select
        if (gameState == 1)
        {
            _spriteBatch.DrawString(font, playerSelect, playerSelectLoc, Color.White);
            _spriteBatch.DrawString(font, playerSelectDown, playerSelectDownLoc, Color.White);
            _spriteBatch.DrawString(font, playerSelectUp, playerSelectUpLoc, Color.White);
            _spriteBatch.DrawString(font, playerSelectConfirm, playerSelectConfirmLoc, Color.White);
        }

        //Draw in player class Select
        if (gameState == 2)
        {
            classSelectScreen.draw(_spriteBatch, font);
        }
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}
