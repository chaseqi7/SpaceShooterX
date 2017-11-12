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
    public class CollisionManager2 : Microsoft.Xna.Framework.GameComponent
    {
        private List<ShipBullet> bulletList;
        private Ship ship;
        private List<EnemyShip2> enemyList;

        private List<EnemyBullet> boss2BulletList;

        private EnemyBoss2 boss2;
        private static bool destroyed2 = false;

        public static bool Destroyed2
        {
            get { return CollisionManager2.destroyed2; }
            set { CollisionManager2.destroyed2 = value; }
        }



        public CollisionManager2(Game game,
            List<ShipBullet> bulletList,
            Ship ship,
            List<EnemyShip2> enemyList,
            EnemyBoss2 boss2,
            List<EnemyBullet> boss2BulletList)
            : base(game)
        {
            // TODO: Construct any child components here
            this.bulletList = bulletList;
            this.ship = ship;
            this.enemyList = enemyList;
            this.boss2 = boss2;
            this.boss2BulletList = boss2BulletList;
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
            Rectangle boss2Rect = boss2.getBounds();
            //Rectangle shipBRect = bullet.getBounds();
            if (EnemyBoss2.boss2Appear)
            {
                for (int k = 0; k < bulletList.Count(); k++)
                {
                    Rectangle bulletRect = bulletList[k].getBounds();
                    if (bulletList[k].Pos.X > Shared.stage.X)
                    {
                        bulletList[k].Dispose();
                        bulletList.Remove(bulletList[k]);
                    }
                    else if (bulletRect.Intersects(boss2Rect))
                    {

                        bulletList[k].Dispose();
                        bulletList.Remove(bulletList[k]);
                        boss2.Boss2Live--;
                    }
                }

                for (int i = 0; i < boss2BulletList.Count(); i++)
                {
                    Rectangle boss2BulletRect = boss2BulletList[i].getBounds();
                    if (boss2BulletRect.Intersects(shipRect))
                    {
                        boss2BulletList[i].Visible = false;
                        boss2BulletList[i].Enabled = false;
                        boss2BulletList.Remove(boss2BulletList[i]);
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
                            Destroyed2 = true;
                        }
                    }
                }
            }

            if (boss2Rect.Intersects(shipRect)&& EnemyBoss2.boss2Appear)
            {
                ship.Lives = 0;
            }





            base.Update(gameTime);
        }
    }
}
