using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using GameUtility;
using System.Security.Cryptography.X509Certificates;
using System.Net.Mime;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Sockets;

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
    static readonly Random rng = new Random();

    int gameState = 1;

    public const int INTRO = 0;
    public const int PLAYERNUMSELECT = 1;
    public const int CLASSSELECT = 2;
    public const int EVENT = 3;
    public const int FIGHT = 4;
    public const int INVENTORY = 5;
    public const int VICTORY = 6;
    public const int DEFEAT = 7;

    public const double TROOPSCALESIZE = 0.2;
    
    Texture2D forestBG;

    string defeatText = "Defeat";

    //Fight Box

    Texture2D fightBox;

    Texture2D strikeButton;
    Texture2D strikeHoverButton;

    Vector2 fightBoxLoc = new Vector2(285, 870);

    Vector2 strikeButtonLoc = new Vector2(320, 920);
    Vector2 strikeHoverButtonLoc = new Vector2(320, 920);
    
    Rectangle strikeButtonRec = new Rectangle(320, 920, 300, 80);

    bool isStrikeButtonPressed;

    Timer openEnemySelection = new Timer (500, false);

    //PlayerCharacters
    Texture2D fighterImg;
    Texture2D rangerImg;
    Texture2D rogueImg;
    Texture2D wizardImg;
    Texture2D clericImg;

    //EnemyCharacters
    Texture2D hogImg;
    Texture2D beeImg;
    Texture2D pixieImg;
    Texture2D wandererImg;

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

    //Fight 

    string strikeText = "strike";

    Vector2 strikeTextLoc = new Vector2(335, 935);
    
    // Screens
    ClassSelectScreen classSelectScreen;
    EnemyGeneration enemyGeneration;
    TurnManager turnManager;

    //Input stuff
    MouseState mouse;
    MouseState prevMouse;

    SpriteFont font;

    //troop stuff
    List<Combatant> playerTroops = new List<Combatant>();
    
    List<Combatant> enemyTroops = new List<Combatant>();

    Dictionary<Combatant, Rectangle> playerTroopRects = new();
    Dictionary<Combatant, Rectangle> enemyTroopRects = new();

    Dictionary<int, Vector2> enemyNameBoxLoc = new Dictionary<int, Vector2>();
    Dictionary<int, Rectangle> enemyNameBoxRec = new Dictionary<int, Rectangle>();

    Dictionary<int, Vector2> enemyNameLoc = new Dictionary<int, Vector2>();


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        //Setup Screen Dimensions
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.HardwareModeSwitch = false;
        //_graphics.IsFullScreen = true;
        _graphics.ApplyChanges();

        //FPS controller
        _graphics.SynchronizeWithVerticalRetrace = false;
        IsFixedTimeStep = true;
        TargetElapsedTime = TimeSpan.FromMilliseconds(1000.0f/ targetFPS);

        //Storing the Screen dimensions
        screenWidth = _graphics.GraphicsDevice.Viewport.Width;
        screenHeight = _graphics.GraphicsDevice.Viewport.Height;

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

        fightBox = Content.Load<Texture2D>("Images/Sprites/ActionFrameImg");

        strikeButton = Content.Load<Texture2D>("Images/Sprites/ButtonImg");
        strikeHoverButton = Content.Load<Texture2D>("Images/Sprites/ButtonHoverImg");

        //player Characters
        fighterImg = Content.Load<Texture2D>("Images/Sprites/FighterImg");
        rangerImg = Content.Load<Texture2D>("Images/Sprites/RangerImg");
        rogueImg = Content.Load<Texture2D>("Images/Sprites/RogueImg");
        wizardImg = Content.Load<Texture2D>("Images/Sprites/WizardImg");
        clericImg = Content.Load<Texture2D>("Images/Sprites/ClericImg");

        //Enemy Characters
        hogImg = Content.Load<Texture2D>("Images/Sprites/HogImg");
        beeImg = Content.Load<Texture2D>("Images/Sprites/BeeImg");
        pixieImg = Content.Load<Texture2D>("Images/Sprites/PixieImg");
        wandererImg = Content.Load<Texture2D>("Images/Sprites/WandererImg");

        forestBGRec = new Rectangle (0, 0, screenWidth, screenHeight);

        font = Content.Load<SpriteFont>("Fonts/SignFont.spritesheet");
            
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        float timePassed = (float)gameTime.ElapsedGameTime.TotalSeconds;


        prevMouse = mouse;
        mouse = Mouse.GetState();
    
        switch(gameState)
        {

            case INTRO:
            //Put on title Screen
            break;

            case PLAYERNUMSELECT:
            //Put player on game set up screen
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
                    classSelectScreen = new ClassSelectScreen(playerTroops, playerSelectNum);
                    gameState = 2;
                }
            }

            break;

            case CLASSSELECT:
            //Put player on class selection Screen
            classSelectScreen.update(gameTime);

            //Send players to first fight
            if (classSelectScreen.IsDone)
            {
                for (int i = 0; i < playerTroops.Count; i++)
                {
                    playerTroopRects[playerTroops[i]] = new Rectangle (200, screenHeight -450 - (100 * i), Convert.ToInt32(fighterImg.Width * TROOPSCALESIZE), Convert.ToInt32(fighterImg.Height * TROOPSCALESIZE));
                }

                enemyGeneration = new EnemyGeneration(playerTroops, enemyTroops);

                enemyGeneration.update(rng);

                for (int i = 0; i < enemyTroops.Count; i++)
                {
                    enemyTroopRects[enemyTroops[i]] = new Rectangle (900, screenHeight -450 - (100 * i), Convert.ToInt32(fighterImg.Width * TROOPSCALESIZE), Convert.ToInt32(fighterImg.Height * TROOPSCALESIZE));
                }
                
                turnManager = new TurnManager();
                turnManager.allCombatants.AddRange(playerTroops);
                turnManager.allCombatants.AddRange(enemyTroops);
                turnManager.currentCharacter = 0;

                gameState = 4;
            }

            break;

            case EVENT:
            //Put player on event selection Screen
            break;

            case FIGHT:

            //Checks if there is anythign of type Character in list allCombatants
            bool arePlayersAlive = turnManager.allCombatants.OfType<Character>().Any();
            bool areEnemiesAlive = turnManager.allCombatants.OfType<EnemyCharacter>().Any();

            if (!arePlayersAlive)
            {
                gameState = DEFEAT;
                break;
            }
            if (!areEnemiesAlive)
            {
                gameState = INVENTORY;
                break;
            }

            Combatant currentCombatant = turnManager.allCombatants[turnManager.currentCharacter];

            if (turnManager.isCurrentCharacterPlayer())
            {
                if (mouse.LeftButton == ButtonState.Pressed && !(prevMouse.LeftButton == ButtonState.Pressed))
                {
                    if (strikeButtonRec.Contains(mouse.Position))
                    {   
                        isStrikeButtonPressed = true;    
                        openEnemySelection.ResetTimer(false);
                    }
                }
            }
            else
            {
                if (currentCombatant is EnemyCharacter currentPlayer)
                {
                    currentPlayer.strike(playerTroops, turnManager, rng);
                }
            }

            if (isStrikeButtonPressed)
            {
                //Used to make sure that the active player is of type Character, on top of making sure the active character is a player
                if (currentCombatant is Character currentPlayer)
                {
                    enemyNameBoxLoc.Clear();
                    enemyNameBoxRec.Clear();

                    for (int i = 0; i < enemyTroops.Count; i++)
                    {
                        enemyNameBoxLoc[i] = new Vector2 (330 + 330 * i, 900);   
                        enemyNameBoxRec[i] = new Rectangle (330 + 330 * i, 900, 300, 80);  

                        enemyNameLoc[i] = new Vector2 (enemyNameBoxLoc[i].X + 100, enemyNameBoxLoc[i].Y + 20);   
                    }

                    if (openEnemySelection.IsFinished() == false)
                    {
                        openEnemySelection.Update(gameTime);
                    }
                    else 
                    {
                        for (int i = 0; i < enemyTroops.Count; i++)
                        {
                            if (enemyNameBoxRec[i].Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
                            {
                                currentPlayer.basicStrike(enemyTroops[i], rng, turnManager);
                            }
                        }
                        isStrikeButtonPressed = false;
                    }
                }
            }

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
        mouse = Mouse.GetState();

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
        if (gameState == 4)
        {
            if (turnManager.isCurrentCharacterPlayer())
            {
                _spriteBatch.Draw(fightBox, fightBoxLoc, Color.White);
                // _spriteBatch.DrawString(font, strikeText, strikeTextLoc, Color.White);
            
                if (!strikeButtonRec.Contains(mouse.Position) && !isStrikeButtonPressed)
                {
                    _spriteBatch.Draw(strikeButton, strikeButtonLoc, Color.White);
                    _spriteBatch.DrawString(font, strikeText, strikeTextLoc, Color.White);
                }
                else if (isStrikeButtonPressed)
                {
                    for (int i = 0; i < enemyTroops.Count; i++)
                    {
                        _spriteBatch.Draw(strikeButton, enemyNameBoxLoc[i], Color.White);
                    }
                }
                else
                {
                    _spriteBatch.Draw(strikeHoverButton, strikeHoverButtonLoc, Color.White);
                    _spriteBatch.DrawString(font, strikeText, strikeTextLoc, Color.White);
                }
            }

            for (int i = 0; i < playerTroops.Count; i++)
            {
                Combatant troop = playerTroops[i];
                string troopName = playerTroops[i].name;
                Texture2D texture = null;
                Vector2 troopCords = new Vector2 (playerTroopRects[troop].X, playerTroopRects[troop].Y - font.LineSpacing - 5);

                if (troop.currentHealth > 0)
                {
                    if (playerTroops[i] is fighter)
                    {
                        texture = fighterImg;
                    }
                    else if (playerTroops[i] is ranger)
                    {
                        texture = rangerImg;
                    }
                    else if (playerTroops[i] is rogue)
                    {
                        texture = rogueImg;
                    }
                    else if (playerTroops[i] is wizard)
                    {
                        texture = wizardImg;
                    }
                    else if (playerTroops[i] is cleric)
                    {
                        texture = clericImg;
                    }

                    if (playerTroops[i] != null)
                    {
                        _spriteBatch.Draw(texture, playerTroopRects[troop], Color.White);
                        _spriteBatch.DrawString(font, troopName, troopCords, Color.White);
                    }
                }
            }

            for (int i = 0; i < enemyTroops.Count; i++)
            {
                Combatant troop = enemyTroops[i];
                string enemyName = "";
                Texture2D texture = null;
                Vector2 enemyCords = new Vector2 (enemyTroopRects[troop].X, enemyTroopRects[troop].Y - font.LineSpacing - 5);

                if (troop.currentHealth > 0)
                {
                    if (enemyTroops[i] is hog)
                    {
                        texture = hogImg;
                    }
                    else if (enemyTroops[i] is bee)
                    {
                        texture = beeImg;
                    }
                    else if (enemyTroops[i] is pixie)
                    {
                        texture = pixieImg;
                    }
                    else if (enemyTroops[i] is wanderer)
                    {
                        texture = wandererImg;
                    }

                    enemyName = enemyTroops[i].name;

                    if (enemyTroops[i] != null)
                    {
                        _spriteBatch.Draw(texture, enemyTroopRects[troop], Color.White);
                        _spriteBatch.DrawString(font, enemyName, enemyCords, Color.White);
                        if (isStrikeButtonPressed)
                        {
                            _spriteBatch.DrawString(font, enemyName, enemyNameLoc[i], Color.White);
                        }
                    }
                }
            }
        }

        if (gameState == 7)
        {
            _spriteBatch.DrawString(font, defeatText, fightBoxLoc, Color.White);
        }

        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}
