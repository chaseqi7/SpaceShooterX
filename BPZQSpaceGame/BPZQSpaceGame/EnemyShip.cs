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
    public class EnemyShip : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private List<EnemyBullet> enemyBulletList = new List<EnemyBullet>();

        public List<EnemyBullet> EnemyBulletList
        {
            get { return enemyBulletList; }
            set { enemyBulletList = value; }
        }
        private Vector2 speed;
        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private bool dead = false;
        public bool Dead
        {
            get
            {
                return dead;
            }

            set
            {
                dead = value;
            }
        }

        private EnemyBullet bullet;
        private Rectangle srcRect;
        private Vector2 origin;
        private float rotationFactor = 0f;
        private float scaleFactor = 1f;
        private int timer=0;

        public EnemyShip(Game game,
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
        //     TODO: Add your update code here
            position += speed;
            timer++;

            if (position.Y < 0+tex.Height/2 || position.Y > Shared.stage.Y- tex.Height / 2)
            {
                speed = new Vector2(speed.X, -speed.Y);
            }
            if (timer % 50 == 0||timer==1)
            {
                bullet = new EnemyBullet(this.Game, spriteBatch, new Vector2(position.X- tex.Width/2, position.Y-5), -20, 0, Game.Content.Load<Texture2D>("Images/bulletE"), Color.Yellow);
                enemyBulletList.Add(bullet);
                Game.Components.Add(bullet);

            }
            if (Dead || EnemyBoss1.dead)
            {
                Visible = false;
                Enabled = false;
                this.Dispose();
            }
            if (position.X < 0)
            {
                this.Dispose();
            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, srcRect, Color.Yellow, rotationFactor, origin, scaleFactor, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X-tex.Width/2, (int)position.Y-tex.Height/2, tex.Width, tex.Height);
        }
    }
}
