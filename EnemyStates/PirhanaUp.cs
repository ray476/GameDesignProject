using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class PirhanaUp : AbsEnemyState
    {
        public PirhanaUp(EnemyObject subject) : base(subject)
        {
            subject.Velocity = new Vector2(0, -1);
        }

        public override void Move()
        {
        }

        public override void TakeDamage()
        {
        }
    }
}
