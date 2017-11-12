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
    public class Ship : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private int lives = 500000;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public int Lives
        {
            get
            {
                return lives;
            }

            set
            {
                lives = value;
            }
        }

        private Vector2 speed;
        private float rotationFactor = 0f;
        private Rectangle srcRect;
        private Vector2 origin;
        private float scaleFactor = 1f;

        private KeyboardState oldState;

        public Ship(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            Vector2 speed)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.speed = speed;

            srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
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
            if(lives<1000)
            {
                lives ++;
            }
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Up))
            {
                position.Y -= speed.Y;
                if (position.Y < tex.Height / 2)
                {
                    position.Y = tex.Height / 2;
                }

            }
            if (ks.IsKeyDown(Keys.Left))
            {
                if (position.X < tex.Width / 2)
                {
                    position.X = tex.Width / 2;
                }
                position.X -= speed.X;
            }
            if (ks.IsKeyDown(Keys.Down))
            {
                position.Y += speed.Y;
                if (position.Y > Shared.stage.Y - tex.Height / 2)
                {
                    position.Y = Shared.stage.Y - tex.Height / 2;
                }

            }
            if (ks.IsKeyDown(Keys.Right))
            {
                position.X += speed.X;
                if (position.X > Shared.stage.X - tex.Width / 2)
                {
                    position.X = Shared.stage.X - tex.Width / 2;
                }

            }
            //if (ks.IsKeyDown(Keys.Space) & oldState.IsKeyUp(Keys.Space))
            //{
            //    bullet = new Bullet(this.Game, spriteBatch, position + new Vector2(0, -4), 15, 0, this.Game.Content.Load<Texture2D>("Images/bullet1"), Color.White);

            //    this.Game.Components.Add(bullet);

            //}
            oldState = ks;

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, srcRect, Color.LightBlue, rotationFactor, origin, scaleFactor, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X - tex.Width / 2, (int)position.Y - tex.Height / 2, tex.Width, tex.Height);
        }
    }
}
