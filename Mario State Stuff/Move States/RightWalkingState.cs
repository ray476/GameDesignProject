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
    class RightWalkingState : AbsMovementState
    {
        public RightWalkingState(AbsAvatarObject avatar)
            : base(avatar)
        {
            Console.WriteLine("right walking");
            avatar.isGrounded = true;
            avatar._velocity.X = 1.25f;
            avatar._velocity.Y = 0;
        }
        public override void Up()
        {
            avatar.movementState = new RightJumpingState(avatar);
        }

        public override void Down()
        {
            avatar.movementState = new RightCrouchingState(avatar);
        }
        public override void Left()
        {
            avatar.movementState = new RightIdleState(avatar);
        }
        public override void Right()
        {

        }
        public override void Update()
        {
            if (avatar.Velocity.Y > 0)
            {
                avatar.movementState = new RightFallingState(avatar);
            }
        }

    }
}
