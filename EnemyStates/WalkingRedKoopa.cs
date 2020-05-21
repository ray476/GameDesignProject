using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class WalkingRedKoopa : AbsEnemyState
    {
        public WalkingRedKoopa(EnemyObject enemy)
         : base(enemy)
        {
            enemy.Hitbox = new BoundingBox(new Vector3(enemy.Position.X, enemy.Position.Y, 0),
                new Vector3(enemy.Position.X + 16, enemy.Position.Y + 24, 0));
        }

        public override void TakeDamage()
        {
            enemy.enemyState = new DeadRedKoopa(enemy);
        }

        public override void Move()
        {

        }

    }
}
