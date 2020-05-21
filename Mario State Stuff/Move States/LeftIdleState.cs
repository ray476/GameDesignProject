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
    class LeftIdleState : AbsMovementState
    {
        public LeftIdleState(AbsAvatarObject avatar)
            : base(avatar)
        {
            Console.WriteLine("left idle");
            avatar.isGrounded = true;
            avatar._velocity.X = 0;
            avatar._velocity.Y = 0;
        }
        public override void Up()
        {
            avatar.movementState = new LeftJumpingIdleState(avatar);
        }

        public override void Down()
        {
            avatar.movementState = new LeftCrouchingState(avatar);
        }
        public override void Left()
        {
            avatar.movementState = new LeftWalkingState(avatar);
        }
        public override void Right()
        {
            avatar.movementState = new RightIdleState(avatar);
        }
        public override void Update()
        {
            if (avatar.Velocity.Y > 0 && avatar.isGrounded)
            {
                avatar.movementState = new LeftIdleFallingState(avatar);
            }
        }
    }
}
