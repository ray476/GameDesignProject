using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test 
{
    class PirhanaPlant : AbsEnemyState
    {
        public PirhanaPlant(EnemyObject enemy)
         : base(enemy)
        {
            enemy.Hitbox = new BoundingBox(new Vector3(enemy.Position.X + 1, enemy.Position.Y, 0),
            new Vector3(enemy.Position.X + 15, enemy.Position.Y + 24, 0));
        }
        public override void Move()
        {
            throw new NotImplementedException();
        }

        public override void TakeDamage()
        {
            throw new NotImplementedException();
        }
    }
}
