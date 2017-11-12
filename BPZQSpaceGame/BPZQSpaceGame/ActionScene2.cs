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
    public class ActionScene2 : GameScene
    {
        int timer = 0;
        int timerSpawn = 0;
        int bossTimer = 0;
        SpriteBatch spriteBatch;
        KeyboardState oldState;
        ShipBullet bullet;
        EnemyShip enemyShip2;
        EnemyShip2 enemyShip21;
        EnemyShip2 enemyShip22;
        EnemyShip2 enemyShip23;
        EnemyShip2 enemyShip24;
        EnemyShip2 enemyShip25;
        SimpleString lives;
        EnemyBoss2 boss2;
        public static int score2 = 0;
        int enemy2Timer = 0;
        SimpleString scoreString;
        SpriteFont font;

        List<EnemyShip2> enemyList2 = new List<EnemyShip2>();
        List<ShipBullet> bulletList = new List<ShipBullet>();

        public EnemyShip EnemyShip
        {
            get { return enemyShip2; }
            set { enemyShip2 = value; }
        }
        EnemyBullet enemyBullet;

        public EnemyBullet EnemyBullet
        {
            get { return enemyBullet; }
            set { enemyBullet = value; }
        }


        public ShipBullet Bullet
        {
            get { return bullet; }
            set { bullet = value; }
        }
        Ship ship;

        public Ship Ship
        {
            get { return ship; }
            set { ship = value; }
        }

        public ActionScene2(Game game,
            SpriteBatch spriteBatch)
            : base(game)
        {
            // TODO: Construct any child components here
            //Paralax Scroll
            this.spriteBatch = spriteBatch;
            Texture2D tex = game.Content.Load<Texture2D>("Images/backgroundImageGame");
            Rectangle srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
            Vector2 pos1 = new Vector2(0, 0);
            ScrollingBackground sb1 = new ScrollingBackground(game, spriteBatch, tex, srcRect, pos1, new Vector2(6, 0),Color.OrangeRed);
            this.Components.Add(sb1);



            //ship
            Texture2D shipTex = game.Content.Load<Texture2D>("Images/Ship1");
            Vector2 pos = new Vector2(shipTex.Width / 2, Shared.stage.Y / 2);
            Vector2 speed = new Vector2(7, 7);
            ship = new Ship(game, spriteBatch, shipTex, pos, speed);
            this.Components.Add(ship);



            font = game.Content.Load<SpriteFont>("Fonts/regularFont");
            string message = "Life:" + ship.Lives;
            Vector2 dimension = font.MeasureString(message);
            Vector2 fontPos = new Vector2(Shared.stage.X - dimension.X, Shared.stage.Y - dimension.Y);
            lives = new SimpleString(game, spriteBatch, font, message, fontPos, Color.White);
            this.Components.Add(lives);

            string scoreMessage = "Score:" + score2;
            Vector2 scoreDimension = font.MeasureString(scoreMessage);
            Vector2 scorePos = new Vector2(0, Shared.stage.Y - scoreDimension.Y);
            scoreString = new SimpleString(game, spriteBatch, font, scoreMessage, scorePos, Color.White);
            this.Components.Add(scoreString);

            Texture2D boss2tex = game.Content.Load<Texture2D>("Images/boss2");
            Vector2 position = new Vector2(1450, 200);
            this.boss2 = new EnemyBoss2(game, spriteBatch, boss2tex, position);



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
            timerSpawn++;
            string message = "Life:" + ship.Lives;
            lives.Message = message;
            string scoreMessage = "Score:" + (score2 + ActionScene1.score);
            scoreString.Message = scoreMessage;
            if (timerSpawn >= 120)
            {
                enemy2Timer++;
                KeyboardState ks = Keyboard.GetState();
                //boss2.RotationFactor += MathHelper.Pi / 60;


                
                if (CollisionManager2.Destroyed2)
                {
                    score2 += 100;
                    CollisionManager2.Destroyed2 = false;
                }
                if (timer % 50 == 0 && score2 < 1000)
                {
                    Texture2D enemyTex2 = this.Game.Content.Load<Texture2D>("Images/enemy2");
                    Random r = new Random();
                    Random s = new Random();
                    Vector2 enemyPos2 = new Vector2(Shared.stage.X + enemyTex2.Width, r.Next(enemyTex2.Height, 401 - enemyTex2.Height / 4));
                    Vector2 enemySpeed2 = new Vector2(-10, s.Next(-10, 10));
                    this.enemyShip21 = new EnemyShip2(this.Game, spriteBatch, enemyTex2, enemyPos2, enemySpeed2);
                    enemyList2.Add(enemyShip21);
                    this.enemyShip22 = new EnemyShip2(this.Game, spriteBatch, enemyTex2, enemyPos2, enemySpeed2);
                    enemyList2.Add(enemyShip22);
                    this.enemyShip23 = new EnemyShip2(this.Game, spriteBatch, enemyTex2, enemyPos2, enemySpeed2);
                    enemyList2.Add(enemyShip23);
                    this.enemyShip24 = new EnemyShip2(this.Game, spriteBatch, enemyTex2, enemyPos2, enemySpeed2);
                    enemyList2.Add(enemyShip24);
                    this.enemyShip25 = new EnemyShip2(this.Game, spriteBatch, enemyTex2, enemyPos2, enemySpeed2);
                    enemyList2.Add(enemyShip25);
                    CollisionManager2 cm2 = new CollisionManager2(this.Game, bulletList, ship, enemyList2, boss2, boss2.Boss2BulletList);
                    this.Components.Add(cm2);
                }
                else if (bossTimer > 150 && !EnemyBoss2.boss2Appear)
                {

                    this.Components.Add(boss2);

                    EnemyBoss2.boss2Appear = true;
                }
                if (score2 >= 1000)
                {
                    bossTimer++;
                }


                if (score2 < 1000)
                {
                    if (enemy2Timer == 5)
                    {
                        Components.Add(enemyShip21);
                    }
                    else if (enemy2Timer == 8)
                    {
                        Components.Add(enemyShip22);
                    }
                    else if (enemy2Timer == 11)
                    {
                        Components.Add(enemyShip23);
                    }
                    else if (enemy2Timer == 14)
                    {
                        Components.Add(enemyShip24);
                    }
                    else if (enemy2Timer == 17)
                    {
                        Components.Add(enemyShip25);

                    }
                    else if (enemy2Timer == 50)
                    {
                        enemy2Timer = 0;
                    }
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
            }
            // TODO: Add your update code here
            base.Update(gameTime);
        }
    }
}
