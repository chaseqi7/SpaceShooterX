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
    public class ActionScene1 : GameScene
    {
        int timer = 0;
        int bossTimer = 0;
        SpriteBatch spriteBatch;
        KeyboardState oldState;
        ShipBullet bullet;
        EnemyShip enemyShip;

        EnemyBoss1 boss1;

        SimpleString lives;
        public static int score = 0;

        SimpleString scoreString;

        SpriteFont font;

        List<EnemyShip> enemyList = new List<EnemyShip>();
        List<EnemyShip> enemyList2 = new List<EnemyShip>();
        
        EnemyBullet enemyBullet;


        

        public ShipBullet Bullet
        {
            get { return bullet; }
            set { bullet = value; }
        }
        List<ShipBullet> bulletList = new List<ShipBullet>();
        Ship ship;

        public Ship Ship
        {
            get { return ship; }
            set { ship = value; }
        }

        public ActionScene1(Game game,
            SpriteBatch spriteBatch)
            : base(game)
        {
            // TODO: Construct any child components here
            //Paralax Scroll
            this.spriteBatch = spriteBatch;
            Texture2D tex = game.Content.Load<Texture2D>("Images/backgroundImageGame");
            Rectangle srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
            Vector2 pos1 = new Vector2(0, 0);
            ScrollingBackground sb1 = new ScrollingBackground(game, spriteBatch, tex, srcRect, pos1, new Vector2(6, 0),Color.White);
            this.Components.Add(sb1);

            

            //ship
            Texture2D shipTex = game.Content.Load<Texture2D>("Images/Ship1");
            Vector2 pos = new Vector2(shipTex.Width/2, Shared.stage.Y / 2);
            Vector2 speed = new Vector2(7, 7);
            ship = new Ship(game, spriteBatch, shipTex, pos, speed);
            this.Components.Add(ship);

           
            
            font = game.Content.Load<SpriteFont>("Fonts/regularFont");
            string message = "Life:" + ship.Lives;
            Vector2 dimension = font.MeasureString(message);
            Vector2 fontPos = new Vector2(Shared.stage.X - dimension.X, Shared.stage.Y - dimension.Y);
            lives = new SimpleString(game, spriteBatch, font, message, fontPos, Color.Yellow);
            this.Components.Add(lives);
            
            string scoreMessage = "Score:" + score;
            Vector2 scoreDimension = font.MeasureString(scoreMessage);
            Vector2 scorePos = new Vector2(0, Shared.stage.Y - scoreDimension.Y);
            scoreString = new SimpleString(game, spriteBatch, font, scoreMessage, scorePos, Color.Yellow);
            this.Components.Add(scoreString);

            Texture2D boss1Tex = this.Game.Content.Load<Texture2D>("Images/boss1");
            this.boss1 = new EnemyBoss1(this.Game, spriteBatch, boss1Tex, new Vector2(Shared.stage.X+ boss1Tex.Width/2, Shared.stage.Y/2), new Vector2(4,2));
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
            
            
            KeyboardState ks = Keyboard.GetState();
            
            
            string message = "Life:" + ship.Lives;
            lives.Message = message;
            string scoreMessage = "Score:" + score;
            scoreString.Message = scoreMessage;
            if (CollisionManager.Destroyed1)
            {
                score += 100;
                CollisionManager.Destroyed1 = false;
            }
            if (timer % 80 == 0 && score < 200)
            {
                Texture2D enemyTex = this.Game.Content.Load<Texture2D>("Images/enemy1");
                Random r = new Random();
                Vector2 enemyPos = new Vector2(Shared.stage.X + enemyTex.Width, r.Next(enemyTex.Height / 4, 401 - enemyTex.Height / 4));
                Vector2 enemySpeed = new Vector2(-10, 0);
                this.enemyShip = new EnemyShip(this.Game, spriteBatch, enemyTex, enemyPos, enemySpeed);
                enemyList.Add(enemyShip);
                this.Components.Add(enemyShip);
                
                CollisionManager cm = new CollisionManager(this.Game, bulletList, ship, enemyBullet, enemyShip.EnemyBulletList, enemyList, boss1, boss1.Boss1BulletList);
                this.Components.Add(cm);
            }

            else if (bossTimer > 200 && !EnemyBoss1.boss1Appear)
            {
                this.Components.Add(boss1);
                
                EnemyBoss1.boss1Appear = true;
            }
            if (score >= 200)
            {
                bossTimer++;
            }
            

            if (bulletList.Count < 3)
            {
                if (ks.IsKeyDown(Keys.Space) & oldState.IsKeyUp(Keys.Space))
                {

                    bullet = new ShipBullet(this.Game, spriteBatch, ship.Position + new Vector2(0, -4), 15, 0, this.Game.Content.Load<Texture2D>("Images/bullet1"), Color.White);
                    bulletList.Add(Bullet);
                    this.Game.Components.Add(bullet);
                } 
            }
            oldState = ks;


            timer++;
            // TODO: Add your update code here
            base.Update(gameTime);
        }
    }
}
