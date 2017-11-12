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
    public class CollisionManager : Microsoft.Xna.Framework.GameComponent
    {
        private List<ShipBullet> bulletList;
        private List<EnemyBullet> enemyBulletList;
        private EnemyBullet enemyBullet;
        private Ship ship;
        private List<EnemyShip> enemyList;
        private List<EnemyBullet> boss1BulletList;
        private static bool destroyed1 = false;
        private EnemyBoss1 boss1;
        public static bool Destroyed1
        {
            get { return CollisionManager.destroyed1; }
            set { CollisionManager.destroyed1 = value; }
        }




        public CollisionManager(Game game,
            List<ShipBullet> bulletList,
            Ship ship,
            EnemyBullet enemyBullet,
            List<EnemyBullet> enemyBulletList,
            List<EnemyShip> enemyList,
            EnemyBoss1 boss1,
            List<EnemyBullet> boss1BulletList)
            : base(game)
        {
            // TODO: Construct any child components here
            this.bulletList = bulletList;
            this.enemyBulletList = enemyBulletList;
            this.ship = ship;
            this.enemyBullet = enemyBullet;
            this.enemyList = enemyList;
            this.boss1 = boss1;
            this.boss1BulletList = boss1BulletList;
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
        /// In this updat method we determine
        /// the interaction between the objects
        /// using the getbounds method and doing things
        /// when the objects intersect.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            //Rectangle enemyBRect = enemyBullet.getBounds();
            Rectangle shipRect = ship.getBounds();
            Rectangle boss1Rect = boss1.getBounds();
            //Rectangle shipBRect = bullet.getBounds();
            if (EnemyBoss1.boss1Appear)
            {
                for (int k = 0; k < bulletList.Count(); k++)
                {
                    Rectangle bulletRect = bulletList[k].getBounds();
                    if (bulletList[k].Pos.X > Shared.stage.X)
                    {
                        bulletList[k].Dispose();
                        bulletList.Remove(bulletList[k]);
                    }
                    else if (bulletRect.Intersects(boss1Rect))
                    {
                        
                        bulletList[k].Dispose();
                        bulletList.Remove(bulletList[k]);
                        boss1.Boss1Live--;
                    }
                }

                for (int i = 0; i < boss1BulletList.Count(); i++)
                {
                    Rectangle boss1BulletRect = boss1BulletList[i].getBounds();
                    if (boss1BulletRect.Intersects(shipRect))
                    {
                        boss1BulletList[i].Visible = false;
                        boss1BulletList[i].Enabled = false;
                        boss1BulletList.Remove(boss1BulletList[i]);
                        ship.Lives -= 350;
                    }
                }
            }

            else
            {
                for (int i = 0; i < enemyList.Count(); i++)
                {
                    Rectangle enemyRect = enemyList[i].getBounds();

                    if (enemyList[i].Position.X < 0)
                    {
                        enemyList[i].Dead = true;
                        enemyList.Remove(enemyList[i]);
                    }
                    else if (shipRect.Intersects(enemyRect))
                    {
                        enemyList[i].Dead = true;
                        enemyList.Remove(enemyList[i]);
                        ship.Lives -= 350;
                        break;
                    }

                    for (int k = 0; k < bulletList.Count(); k++)
                    {
                        Rectangle bulletRect = bulletList[k].getBounds();
                        if (bulletList[k].Pos.X > Shared.stage.X)
                        {
                            bulletList[k].Dispose();
                            bulletList.Remove(bulletList[k]);
                        }
                        else if (bulletRect.Intersects(enemyRect))
                        {
                            enemyList[i].Dead = true;
                            bulletList[k].Dispose();
                            bulletList.Remove(bulletList[k]);
                            enemyList.Remove(enemyList[i]);
                            destroyed1 = true;
                        }
                    }
                }
            }
            
            for (int i = 0; i < enemyBulletList.Count(); i++)
            {
                Rectangle enemyBulletRect = enemyBulletList[i].getBounds();
                if (enemyBulletRect.Intersects(shipRect))
                {
                    enemyBulletList[i].Visible = false;
                    enemyBulletList[i].Enabled = false;
                    enemyBulletList.Remove(enemyBulletList[i]);
                    ship.Lives-=350;
                    
                }
            }

            
                if (boss1Rect.Intersects(shipRect))
                {
                    ship.Lives = 0;
                } 
            
 




            base.Update(gameTime);
        }
    }
}
