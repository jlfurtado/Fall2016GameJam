using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Polychrome
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GameState currentGameState = GameState.TITLE_SCREEN;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // textures for menus
        public static Texture2D menuCorner;
        public static Texture2D menuBar;
        public static Texture2D menuCenter;
        public static Texture2D textCorner;
        public static Texture2D textBar;
        public static Texture2D textCenter;

        //fonts
        public static SpriteFont font144;
        public static SpriteFont font72;
        public static SpriteFont font16;
        public static SpriteFont font12;
        public static SpriteFont font10;

        // menus and text boxes for screens
        Menu titleMenu;
        TextBox titleBox;
        Menu infoMenu;
        TextBox infoBox;
        Menu victoryMenu;
        TextBox victoryBox;
        Menu gameoverMenu;

        // misc text boxes
        TextBox gameoverBox;
        TextBox thankYou;
        TextBox credits;

        // backgrounds
        Texture2D whiteBG;
        Texture2D redBG;
        Texture2D greenBG;
        Texture2D blueBG;
        Texture2D blackBG;

        public static Texture2D[] TILES;
        public static Texture2D[] ENEMIES;
        public static Texture2D playerSprite;

        // levels
        public static World world;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font144 = Content.Load<SpriteFont>("source-sans-144");
            font72 = Content.Load<SpriteFont>("source-sans-72");
            font16 = Content.Load<SpriteFont>("source-sans-16");
            font12 = Content.Load<SpriteFont>("source-sans-12");
            font10 = Content.Load<SpriteFont>("source-sans-10");

            textCorner = Content.Load<Texture2D>("BoxCornerSmall");
            textBar = Content.Load<Texture2D>("BoxSideSmall");
            textCenter = Content.Load<Texture2D>("BoxMidSmall");

            menuCorner = textCorner;
            menuBar = textBar;
            menuCenter = textCenter;

            string[] t = { "Start game", "Info Screen", "Exit" };
            titleMenu = new Menu(t, new MenuSelectionCallback[] { SwapToPlaying, SwapToInfo, ExitGame }, new Vector2(800, 460), menuCorner, menuBar, menuCenter, 20, 10, font16);

            string[] t2 = { "Polychrome" };
            titleBox = new TextBox(t2, new Vector2(600, 150), textCorner, textBar, textCenter, 45, 10, font72);

            string[] t5 = { "Return to title", "Exit" };
            infoMenu = new Menu(t5, new MenuSelectionCallback[] { SwapToTitle, ExitGame }, new Vector2(800, 460), menuCorner, menuBar, menuCenter, 20, 10, font16);

            string[] t6 = { "Made the weekend of November 23, 2016", " at a Neumont University Game Jam", "Created by Justin Furtado and Michael Vanderlip" };
            infoBox = new TextBox(t6, new Vector2(600, 150), textCorner, textBar, textCenter, 45, 10, font16);

            string[] t7 = { "Return to title", "Exit" };
            victoryMenu = new Menu(t7, new MenuSelectionCallback[] { SwapToTitle, ExitGame }, new Vector2(800, 460), menuCorner, menuBar, menuCenter, 20, 10, font16);

            string[] t8 = { "Congratulations!" };
            victoryBox = new TextBox(t8, new Vector2(600, 10), textCorner, textBar, textCenter, 45, 10, font72);

            string[] t9 = { "Return to title", "Exit" };
            gameoverMenu = new Menu(t9, new MenuSelectionCallback[] { SwapToTitle, ExitGame }, new Vector2(800, 460), menuCorner, menuBar, menuCenter, 20, 10, font16);

            string[] t10 = { "GAME OVER!" };
            gameoverBox = new TextBox(t10, new Vector2(600, 10), textCorner, textBar, textCenter, 45, 10, font72);

            string[] t11 = { "Art, programming and sound by Justin Furtado and Michael Vanderlip" };
            credits = new TextBox(t11, new Vector2(600, 750), textCorner, textBar, textCenter, 45, 4, font16);

            string[] t12 = { "Thanks for playing, we hope you enjoyed Polychrome" };
            thankYou = new TextBox(t12, new Vector2(600, 170), textCorner, textBar, textCenter, 45, 4, font16);

            whiteBG = Content.Load<Texture2D>("BG_White");
            redBG = Content.Load<Texture2D>("BG_Red");
            greenBG = Content.Load<Texture2D>("BG_Green");
            blueBG = Content.Load<Texture2D>("BG_Blue");
            blackBG = Content.Load<Texture2D>("BG_Black");

            TILES = new Texture2D[] { Content.Load<Texture2D>(@"FloorTiles\TopTile") };
            ENEMIES = new Texture2D[] { Content.Load<Texture2D>(@"BasicEnemies\RedBasic") };
            playerSprite = Content.Load<Texture2D>(@"Player\PlayerBlue");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            InputManager.Update(gameTime);

            switch (currentGameState)
            {
                case GameState.TITLE_SCREEN:
                    titleMenu.ProcessInput();
                    break;

                case GameState.PLAYING:
                    world.Update(gameTime);
                    if (world.IsGameOver) { currentGameState = world.IsGameWon ? GameState.VICTORY_SCREEN : GameState.GAMEOVER_SCREEN; }

                    break;

                case GameState.INFO_SCREEN:
                    infoMenu.ProcessInput();
                    break;

                case GameState.VICTORY_SCREEN:
                    victoryMenu.ProcessInput();
                    break;

                case GameState.GAMEOVER_SCREEN:
                    gameoverMenu.ProcessInput();
                    break;

            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (currentGameState)
            {
                case GameState.TITLE_SCREEN:
                    spriteBatch.Draw(whiteBG, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    titleBox.Draw(spriteBatch);
                    titleMenu.Draw(spriteBatch);
                    credits.Draw(spriteBatch);
                    break;

                case GameState.PLAYING:
                    world.Draw(spriteBatch, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
                    break;

                case GameState.INFO_SCREEN:
                    spriteBatch.Draw(whiteBG, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    infoBox.Draw(spriteBatch);
                    infoMenu.Draw(spriteBatch);
                    credits.Draw(spriteBatch);
                    break;

                case GameState.VICTORY_SCREEN:
                    spriteBatch.Draw(whiteBG, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    victoryBox.Draw(spriteBatch);
                    victoryMenu.Draw(spriteBatch);
                    credits.Draw(spriteBatch);
                    thankYou.Draw(spriteBatch);
                    break;

                case GameState.GAMEOVER_SCREEN:
                    spriteBatch.Draw(whiteBG, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    gameoverBox.Draw(spriteBatch);
                    gameoverMenu.Draw(spriteBatch);
                    credits.Draw(spriteBatch);
                    thankYou.Draw(spriteBatch);
                    break;

            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void SwapToPlaying()
        {
            currentGameState = GameState.PLAYING;
            world = new World(new Texture2D[] { whiteBG, redBG, greenBG, blueBG, blackBG },
                              new string[] { @"Content\IntroLevel.xml", @"Content\RedLevel.xml", @"Content\GreenLevel.xml", @"Content\BlueLevel.xml", @"Content\FinalLevel.xml" },
                              GraphicsDevice.Viewport.Width);
        }

        private void SwapToInfo()
        {
            currentGameState = GameState.INFO_SCREEN;
            infoMenu.ResetMenu();
        }

        private void ExitGame()
        {
            Exit();
        }

        private void SwapToVictory()
        {
            currentGameState = GameState.VICTORY_SCREEN;
            victoryMenu.ResetMenu();
        }

        private void SwapToGameOver()
        {
            currentGameState = GameState.GAMEOVER_SCREEN;
            gameoverMenu.ResetMenu();
        }

        private void SwapToTitle()
        {
            currentGameState = GameState.TITLE_SCREEN;
            titleMenu.ResetMenu();
        }
    }

    enum GameState
    {
        TITLE_SCREEN,
        PLAYING,
        GAMEOVER_SCREEN,
        VICTORY_SCREEN,
        INFO_SCREEN
    }
}
