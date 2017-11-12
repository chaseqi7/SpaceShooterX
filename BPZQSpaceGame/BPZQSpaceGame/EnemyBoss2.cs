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
    public class EnemyBoss2 : Microsoft.Xna.Framework.DrawableGameComponent
    {

        private EnemyBullet bullet;
        private EnemyBullet bullet2;
        private EnemyBullet bullet3;
        private EnemyBullet bullet4;
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        public static bool dead = false;
        private int boss2Live = 20;
        private float scaleFactor = 1.0f;
        int timer=0;
        private List<EnemyBullet> boss2BulletList = new List<EnemyBullet>();
        private Vector2 speed = new Vector2(2,2);
        public List<EnemyBullet> Boss2BulletList
        {
            get { return boss2BulletList; }
            set { boss2BulletList = value; }
        }
        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public static bool boss2Appear = false;

        public int Boss2Live
        {
            get
            {
                return boss2Live;
            }

            set
            {
                boss2Live = value;
            }
        }



        public float RotationFactor
        {
            get
            {
                return rotationFactor;
            }

            set
            {
                rotationFactor = value;
            }
        }

        private Rectangle srcRect;
        private Vector2 origin;
        private float rotationFactor = 0f;
        private float rotationChange = MathHelper.Pi * 2/60;

        public EnemyBoss2(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
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
            if (timer % 60 == 0)
            {
                bullet = new EnemyBullet(this.Game, spriteBatch, new Vector2(position.X + tex.Width / 2-30, position.Y + tex.Height / 2-30), 5, 5, Game.Content.Load<Texture2D>("Images/bullet1"), Color.Yellow);
                bullet2 = new EnemyBullet(this.Game, spriteBatch, new Vector2(position.X + tex.Width / 2 - 30, position.Y - tex.Height / 2+30), 5, -5, Game.Content.Load<Texture2D>("Images/bullet1"), Color.Yellow);
                bullet3 = new EnemyBullet(this.Game, spriteBatch, new Vector2(position.X - tex.Width / 2-30, position.Y + tex.Height / 2+30), -5, 5, Game.Content.Load<Texture2D>("Images/bulletE"), Color.Yellow);
                bullet4 = new EnemyBullet(this.Game, spriteBatch, new Vector2(position.X - tex.Width / 2+30, position.Y - tex.Height / 2+30), -5, -5, Game.Content.Load<Texture2D>("Images/bulletE"), Color.Yellow);
                boss2BulletList.Add(bullet);
                Game.Components.Add(bullet);
                boss2BulletList.Add(bullet2);
                Game.Components.Add(bullet2);
                boss2BulletList.Add(bullet3);
                Game.Components.Add(bullet3);
                boss2BulletList.Add(bullet4);
                Game.Components.Add(bullet4);

            }
            rotationFactor += rotationChange;
            position = position + speed;
            if (RotationFactor >= MathHelper.Pi*2)
            {
                RotationFactor = 0;
            }
            if (position.Y < 0)
            {
                speed = new Vector2(speed.X, Math.Abs(speed.Y));
            }
            if ( position.Y > Shared.stage.Y)
            {
                speed = new Vector2(speed.X, -Math.Abs(speed.Y));
            }
            if (position.X < 0)
            {
                speed = new Vector2(Math.Abs(speed.X), speed.Y);
            }
            if (position.X > Shared.stage.X)
            {
                speed = new Vector2(-Math.Abs(speed.X), speed.Y);
            }
            timer++;
            if (Boss2Live <= 0)
            {
                EnemyBoss2.dead = true;
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
            
            spriteBatch.Draw(tex, position,
                new Rectangle(0, 0, tex.Width, tex.Height),
                Color.White,
                RotationFactor,
                origin,
                scaleFactor,
                SpriteEffects.None,
                0f
                );

            spriteBatch.End();
            base.Draw(gameTime);
        }
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X - tex.Width / 2, (int)position.Y - tex.Height / 2, tex.Width, tex.Height);
        }
    }
}
