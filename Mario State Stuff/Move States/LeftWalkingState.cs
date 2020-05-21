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
    class LeftWalkingState : AbsMovementState
    {
        public LeftWalkingState(AbsAvatarObject avatar)
            : base(avatar)
        {
            Console.WriteLine("left walking");
            avatar.isGrounded = true;
            avatar._velocity.X = -1.25f;
            avatar._velocity.Y = 0;
        }
        public override void Up()
        {
            avatar.movementState = new LeftJumpingState(avatar);
        }

        public override void Down()
        {
            avatar.movementState = new LeftCrouchingState(avatar);
        }
        public override void Left()
        {

        }
        public override void Right()
        {
            avatar.movementState = new LeftIdleState(avatar);
        }
        public override void Update()
        {
            if(avatar.Velocity.Y > 0)
            {
                avatar.movementState = new LeftFallingState(avatar);
            }
        }

    }
}
