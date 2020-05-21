using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace template_test
{
    class DeadRedKoopa : AbsEnemyState
    {
        public DeadRedKoopa(EnemyObject enemy)
         : base(enemy)
        {
            enemy.isVisible = false;
            enemy.Hitbox = null;
            enemy.Velocity = new Vector2(0);
            enemy.deleteThis = true;
        }

        public override void TakeDamage()
        {

        }

        public override void Move()
        {

        }

    }
}
