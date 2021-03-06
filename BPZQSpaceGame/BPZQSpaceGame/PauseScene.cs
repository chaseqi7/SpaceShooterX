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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class PauseScene : GameScene
    {
        private MenuComponent menu;

        private Texture2D tex;
        
        public MenuComponent Menu
        {
            get { return menu; }
            set { menu = value; }
        }
        private SpriteBatch spriteBatch;
        string[] menuItems = {"Resume?"};


        public PauseScene(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            menu = new MenuComponent(game, spriteBatch,
                game.Content.Load<SpriteFont>("Fonts/regularFont"),
                game.Content.Load<SpriteFont>("Fonts/hilightFont"),
                menuItems);
            tex = game.Content.Load<Texture2D>("Images/backgroundImageStart");
            this.Components.Add(menu);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, Vector2.Zero, Color.GhostWhite);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
