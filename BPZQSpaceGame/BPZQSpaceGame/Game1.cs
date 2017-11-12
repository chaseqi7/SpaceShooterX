using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BPZQSpaceGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //declare all scenes here.............
        private StartScene startScene;
        private HelpScene helpScene;
        private ActionScene1 actionScene1;
        private ActionScene2 actionScene2;
        private HowToScene howToScene;
        private AboutScene aboutScene;
        private PauseScene pauseScene;
        private KeyboardState oldState;
        private EndScene endScene;



        //----------------Scene declaration ends---------------------

        private void hideAllScenes()
        {
            GameScene gs = null;
            foreach (GameComponent item in Components)
            {
                if (item is GameScene)
                {
                    gs = (GameScene)item;
                    gs.hide();
                }
            }
        }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 400;
            graphics.PreferredBackBufferWidth = 1100;

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
            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);

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

            // TODO: use this.Content to load your game content here

            //create all scenes and add to the Components list
            startScene = new StartScene(this, spriteBatch);
            Components.Add(startScene);

            helpScene = new HelpScene(this, spriteBatch);
            Components.Add(helpScene);

            actionScene1 = new ActionScene1(this, spriteBatch);
            Components.Add(actionScene1);

            actionScene2 = new ActionScene2(this, spriteBatch);
            Components.Add(actionScene2);

            aboutScene = new AboutScene(this, spriteBatch);
            Components.Add(aboutScene);

            howToScene = new HowToScene(this, spriteBatch);
            Components.Add(howToScene);

            pauseScene = new PauseScene(this, spriteBatch);
            Components.Add(pauseScene);

            endScene = new EndScene(this, spriteBatch);
            Components.Add(endScene);


            startScene.show();
            //----------------creating scenes ends------------------------------



        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();

            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    actionScene1.Dispose();
                    actionScene1 = new ActionScene1(this, spriteBatch);

                    Components.Add(actionScene1);
                    hideAllScenes();
                    actionScene1.show();
                }
                if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    helpScene.show();
                }
                if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    aboutScene.show();
                }
                if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    howToScene.show();
                }
                //... other scenes here

                if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }

            if (helpScene.Enabled || actionScene1.Enabled || howToScene.Enabled || actionScene2.Enabled
                || aboutScene.Enabled || actionScene2.Enabled || endScene.Enabled)
            {

                if (ks.IsKeyDown(Keys.Escape) || actionScene1.Ship.Lives <= 0)
                {
                    EnemyBoss1.boss1Appear = false;
                    ActionScene1.score = 0;
                    ActionScene2.score2 = 0;
                    actionScene1.Dispose();
                    Components.Add(actionScene1);
                    hideAllScenes();
                    startScene.show();
                }
                if (actionScene1.Enabled && EnemyBoss1.dead == true)
                {
                    EnemyBoss1.dead = false;
                    EnemyBoss1.boss1Appear = false;
                    hideAllScenes();
                    actionScene2.Dispose();
                    actionScene2 = new ActionScene2(this, spriteBatch);
                    Components.Add(actionScene2);
                    actionScene2.show();
                }

                if (actionScene2.Enabled && actionScene2.Ship.Lives <= 0)
                {
                    EnemyBoss2.boss2Appear = false;
                    ActionScene1.score = 0;
                    ActionScene2.score2 = 0;
                    actionScene2.Dispose();
                    Components.Add(actionScene2);
                    hideAllScenes();
                    startScene.show();
                }
                if (actionScene2.Enabled && EnemyBoss2.dead)
                {
                    EnemyBoss2.dead = false;
                    EnemyBoss2.boss2Appear = false;
                    actionScene2.Dispose();
                    hideAllScenes();
                    endScene.show();
                }







            }




            oldState = ks;


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
