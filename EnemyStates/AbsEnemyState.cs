using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    abstract class AbsEnemyState : IEnemyState
    {
        public EnemyObject enemy;
        public IEnemyFactory factory;

        public AbsEnemyState(EnemyObject enemy)
        {
            this.enemy = enemy;
            factory = new EnemySpriteFactory(enemy.content);
            enemy.Sprite = factory.build(enemy.Position, this);
        }
        public abstract void Move();
        public abstract void TakeDamage();
    }
}
