using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    abstract class AbsDoodleMoveState
    {
        public DoodleObject avatar;
        public IDoodleSpriteFactory factory;

        public AbsDoodleMoveState(DoodleObject avatar)
        {
            this.avatar = avatar;
            factory = new DoodleMetaFactory(avatar.powerState, avatar.content);
            avatar.Sprite = factory.build(this);
        }

        public abstract void Up();
        public abstract void Down();
        public abstract void Left();
        public abstract void Right();
    }
}
