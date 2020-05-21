using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class WalkingGoomba : AbsEnemyState
    {
        public WalkingGoomba(EnemyObject enemy)
         : base(enemy)
        {
                enemy.Hitbox = new BoundingBox(new Vector3(enemy.Position.X, enemy.Position.Y, 0), 
                new Vector3(enemy.Position.X + 16, enemy.Position.Y + 16, 0));
        }

        public override void TakeDamage()
        {
            enemy.Hitbox = new BoundingBox(new Vector3(0),new Vector3(0));
            enemy.isVisible = false;
            enemy.deleteThis = true;
        }

        public override void Move()
        {
        
        }
        
    }
}
