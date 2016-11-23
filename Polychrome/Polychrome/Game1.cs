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
        Menu tempWorldMenu;
        TextBox tempWorldBox;
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
        Texture2D titleBG;
        Texture2D gameoverBG;
        Texture2D victoryBG;

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
            titleMenu = new Menu(t, new Vector2(240, 240), menuCorner, menuBar, menuCenter, 20, 10, font16);

            string[] t2 = { "Polychrome" };
            titleBox = new TextBox(t2, new Vector2(40, 50), textCorner, textBar, textCenter, 45, 10, font72);

            string[] t5 = { "Return to title", "Exit" };
            infoMenu = new Menu(t5, new Vector2(240, 240), menuCorner, menuBar, menuCenter, 20, 10, font16);

            string[] t6 = { "Made the weekend of November 23, 2016", " at a Neumont University Game Jam", "Created by Justin Furtado and Michael Vanderlip" };
            infoBox = new TextBox(t6, new Vector2(40, 50), textCorner, textBar, textCenter, 45, 10, font16);

            string[] t7 = { "Return to title", "Exit" };
            victoryMenu = new Menu(t7, new Vector2(240, 240), menuCorner, menuBar, menuCenter, 20, 10, font16);

            string[] t8 = { "Congratulations!" };
            victoryBox = new TextBox(t8, new Vector2(40, 10), textCorner, textBar, textCenter, 45, 10, font72);

            string[] t69 = { "TODO: INSERT GAME HERE!" };
            tempWorldBox = new TextBox(t69, new Vector2(40, 50), textCorner, textBar, textCenter, 45, 10, font16);

            string[] t70 = { "Win Game", "Lose Game" };
            tempWorldMenu = new Menu(t70, new Vector2(240, 240), menuCorner, menuBar, menuCenter, 20, 10, font16);

            string[] t9 = { "Return to title", "Exit" };
            gameoverMenu = new Menu(t9, new Vector2(240, 240), menuCorner, menuBar, menuCenter, 20, 10, font16);

            string[] t10 = { "GAME OVER!" };
            gameoverBox = new TextBox(t10, new Vector2(40, 10), textCorner, textBar, textCenter, 45, 10, font72);

            string[] t11 = { "Art, programming and sound by Justin Furtado and Michael Vanderlip" };
            credits = new TextBox(t11, new Vector2(40, 416), textCorner, textBar, textCenter, 45, 4, font16);

            string[] t12 = { "Thanks for playing, we hope you enjoyed Polychrome" };
            thankYou = new TextBox(t12, new Vector2(40, 170), textCorner, textBar, textCenter, 45, 4, font16);

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

            switch (currentGameState)
            {
                case GameState.TITLE_SCREEN:
                    InputManager.Update(gameTime);
                    titleMenu.ProcessInput();

                    if (titleMenu.IsOver)
                    {
                        if (titleMenu.Selection == 0)
                        {
                            currentGameState = GameState.PLAYING;
                            tempWorldMenu.ResetMenu();
                        }
                        else if (titleMenu.Selection == 1)
                        {
                            currentGameState = GameState.INFO_SCREEN;
                            infoMenu.ResetMenu();
                        }
                        else if (titleMenu.Selection == 2)
                        {
                            Exit();
                        }
                    }
                    break;

                case GameState.PLAYING:
                    InputManager.Update(gameTime);
                    tempWorldMenu.ProcessInput();

                    if (tempWorldMenu.IsOver)
                    {
                        if (tempWorldMenu.Selection == 0)
                        {
                            currentGameState = GameState.VICTORY_SCREEN;
                            victoryMenu.ResetMenu();
                        }
                        else if (tempWorldMenu.Selection == 1)
                        {
                            currentGameState = GameState.GAMEOVER_SCREEN;
                            gameoverMenu.ResetMenu();
                        }
                    }

                    break;

                case GameState.INFO_SCREEN:
                    InputManager.Update(gameTime);
                    infoMenu.ProcessInput();

                    if (infoMenu.IsOver)
                    {
                        if (infoMenu.Selection == 0)
                        {
                            currentGameState = GameState.TITLE_SCREEN;
                            titleMenu.ResetMenu();
                        }
                        else if (infoMenu.Selection == 1)
                        {
                            Exit();
                        }

                    }
                    break;

                case GameState.VICTORY_SCREEN:
                    InputManager.Update(gameTime);
                    victoryMenu.ProcessInput();

                    if (victoryMenu.IsOver)
                    {
                        if (victoryMenu.Selection == 0)
                        {
                            currentGameState = GameState.TITLE_SCREEN;
                            titleMenu.ResetMenu();
                        }
                        else if (victoryMenu.Selection == 1)
                        {
                            Exit();
                        }
                    }
                    break;

                case GameState.GAMEOVER_SCREEN:
                    InputManager.Update(gameTime);
                    gameoverMenu.ProcessInput();

                    if (gameoverMenu.IsOver)
                    {
                        if (gameoverMenu.Selection == 0)
                        {
                            currentGameState = GameState.TITLE_SCREEN;
                            titleMenu.ResetMenu();
                        }
                        else if (gameoverMenu.Selection == 1)
                        {
                            Exit();
                        }
                    }
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
                    //spriteBatch.Draw(titleBG, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    titleBox.Draw(spriteBatch);
                    titleMenu.Draw(spriteBatch);
                    credits.Draw(spriteBatch);
                    break;

                case GameState.PLAYING:
                    tempWorldBox.Draw(spriteBatch);
                    tempWorldMenu.Draw(spriteBatch);
                    break;

                case GameState.INFO_SCREEN:
                    // spriteBatch.Draw(titleBG, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    infoBox.Draw(spriteBatch);
                    infoMenu.Draw(spriteBatch);
                    credits.Draw(spriteBatch);
                    break;

                case GameState.VICTORY_SCREEN:
                    // spriteBatch.Draw(victoryBG, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    victoryBox.Draw(spriteBatch);
                    victoryMenu.Draw(spriteBatch);
                    credits.Draw(spriteBatch);
                    thankYou.Draw(spriteBatch);
                    break;

                case GameState.GAMEOVER_SCREEN:
                    // spriteBatch.Draw(gameoverBG, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    gameoverBox.Draw(spriteBatch);
                    gameoverMenu.Draw(spriteBatch);
                    credits.Draw(spriteBatch);
                    thankYou.Draw(spriteBatch);
                    break;

            }

            spriteBatch.End();

            base.Draw(gameTime);
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
