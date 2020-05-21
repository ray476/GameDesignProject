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
    class RightJumpingIdleState : AbsMovementState
    {
        public RightJumpingIdleState(AbsAvatarObject avatar)
            : base(avatar)
        {
            // Console.WriteLine("right idle jumping");
            avatar._velocity.X = 0;
            if (avatar.isGrounded)
                avatar._velocity.Y = -2.6f;
            avatar.isGrounded = false;
        }
        public override void Up()
        {

        }

        public override void Down()
        {
            avatar.movementState = new RightIdleState(avatar);
        }
        public override void Left()
        {
            avatar.movementState = new LeftJumpingIdleState(avatar);
        }
        public override void Right()
        {
            avatar.movementState = new RightJumpingState(avatar);
        }
        public override void Update()
        {
        }

    }
}
