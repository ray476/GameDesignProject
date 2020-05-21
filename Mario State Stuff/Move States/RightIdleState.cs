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
    class RightIdleState : AbsMovementState
    {
        public RightIdleState(AbsAvatarObject avatar)
            : base(avatar)
        {
            Console.WriteLine("right idle");
            avatar.isGrounded = true;
            avatar._velocity.X = 0;
            avatar._velocity.Y = 0;
        }
        public override void Up()
        {
            avatar.movementState = new RightJumpingIdleState(avatar);
        }

        public override void Down()
        {
            avatar.movementState = new RightCrouchingState(avatar);
        }
        public override void Left()
        {
            avatar.movementState = new LeftIdleState(avatar);
        }
        public override void Right()
        {
            avatar.movementState = new RightWalkingState(avatar);
        }
        public override void Update()
        {
            if (avatar.Velocity.Y > 0.1 && avatar.isGrounded)
            {
                avatar.movementState = new RightIdleFallingState(avatar);
            }
        }

    }
}
