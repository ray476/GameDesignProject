using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class DoodleJumpingState : AbsDoodleMoveState
    {
        private DoodleObject _avatar;

        public DoodleJumpingState(DoodleObject avatar) : base(avatar)
        {
            _avatar = avatar;
        }

        public override void Up()
        {
        }

        public override void Down()
        {
        }

        public override void Left()
        {
        }

        public override void Right()
        {
        }
    }
}

