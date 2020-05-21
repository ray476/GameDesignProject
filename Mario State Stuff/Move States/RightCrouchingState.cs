using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace template_test
{
    class RightCrouchingState : AbsMovementState
    {
        public RightCrouchingState(AbsAvatarObject avatar)
            : base(avatar)
        {
            avatar._velocity.X = 0;
            avatar._velocity.Y = 1;
        }
        public override void Up()
        {
            avatar.movementState = new RightIdleState(avatar);
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
        public override void Update()
        {
        }

    }
}
