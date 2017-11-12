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
    public class EnemyBoss1 : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private int boss1Live = 10;
        private Vector2 speed;
        private List<EnemyBullet> boss1BulletList = new List<EnemyBullet>();
        public List<EnemyBullet> Boss1BulletList
        {
            get { return boss1BulletList; }
            set { boss1BulletList = value; }
        }
        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
       
        public static bool boss1Appear = false;

        public int Boss1Live
        {
            get
            {
                return boss1Live;
            }

            set
            {
                boss1Live = value;
            }
        }

        public static bool dead = false;

        private EnemyBullet bullet;
        private EnemyBullet bullet2;
        private EnemyBullet bullet3;
        private EnemyBullet bullet4;
        private Rectangle srcRect;
        private Vector2 origin;
        private float rotationFactor = 0f;
        private float scaleFactor = 1f;
        private int timer = 0;
        public EnemyBoss1(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            Vector2 speed
            )
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
            if(position.X > 800)
            {
                position.X -= speed.X;
            }
            if(speed.Y>0&position.Y>Shared.stage.Y-tex.Height/2)
            { 
                speed.Y = -Math.Abs(speed.Y);
            }
            else if(speed.Y<0&&position.Y<= tex.Height / 2)
            { 
                speed.Y = Math.Abs(speed.Y);
            }
            
            position.Y += speed.Y;
            timer++;
            if (timer % 55 == 0)
            {
                bullet = new EnemyBullet(this.Game, spriteBatch, new Vector2(position.X - tex.Width / 2, position.Y-15), -10, 0, Game.Content.Load<Texture2D>("Images/bulletE"), Color.Yellow);
                bullet2 = new EnemyBullet(this.Game, spriteBatch, new Vector2(position.X - tex.Width / 2, position.Y+35), -10, -5, Game.Content.Load<Texture2D>("Images/bulletE"), Color.Yellow);
                bullet3 = new EnemyBullet(this.Game, spriteBatch, new Vector2(position.X - tex.Width / 2, position.Y-35), -10, 5, Game.Content.Load<Texture2D>("Images/bulletE"), Color.Yellow);
                bullet4 = new EnemyBullet(this.Game, spriteBatch, new Vector2(position.X - tex.Width / 2, position.Y+15), -10, 0, Game.Content.Load<Texture2D>("Images/bulletE"), Color.Yellow);
                boss1BulletList.Add(bullet);
                Game.Components.Add(bullet);
                boss1BulletList.Add(bullet2);
                Game.Components.Add(bullet2);
                boss1BulletList.Add(bullet3);
                Game.Components.Add(bullet3);
                boss1BulletList.Add(bullet4);
                Game.Components.Add(bullet4);

            }
            if (boss1Live<=0)
            {
                ActionScene1.score += 1000;
                EnemyBoss1.dead = true;
                bullet.Visible = false;
                bullet.Enabled = false;
                bullet2.Visible = false;
                bullet2.Enabled = false;
                bullet3.Visible = false;
                bullet3.Enabled = false;
                bullet4.Visible = false;
                bullet4.Enabled = false;
                Visible = false;
                Enabled = false;
                position = new Vector2(-2000, -2000);
                this.Dispose();
            }


            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, srcRect, Color.GhostWhite, rotationFactor, origin, scaleFactor, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X - tex.Width / 2, (int)position.Y - tex.Height / 2, tex.Width, tex.Height);
        }
    }
}
