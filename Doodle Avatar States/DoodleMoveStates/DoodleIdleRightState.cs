using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class DoodleIdleRightState : AbsDoodleMoveState
    {
        private DoodleObject _avatar;

        public DoodleIdleRightState(DoodleObject avatar) : base(avatar)
        {
            _avatar = avatar;
        }

        public override void Up()
        {
            avatar.moveState = new DoodleJumpingState(avatar);
        }

        public override void Down()
        {
        }

        public override void Left()
        {
            avatar.moveState = new DoodleIdleLeftState(avatar);
        }

        public override void Right()
        {
            avatar.moveState = new DoodleIdleRightState(avatar);
        }
    }
}
