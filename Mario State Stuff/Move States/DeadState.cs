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
    // i suspect there could be some weird interactions with going into/getting out of the death state
    // using these commands.  pbi's dont seem to specify exact desired behavior.  will probably need to return
    // to this (9-23)
    class DeadState : AbsMovementState
    {
        private int updateTilRez;
        public DeadState(AbsAvatarObject avatar)
            : base(avatar)
        {
            avatar.Velocity = new Vector2(0, -1f);
            avatar.Hitbox = new BoundingBox(new Vector3(0), new Vector3(0));
            updateTilRez = 0;
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
        public override void Update()
        {
            updateTilRez++;
            if (updateTilRez >= 240 && avatar.Position.Y > 500)
            {
                avatar.hud.ChangeLife(-1);
                avatar.movementState = new RightIdleState(avatar);
                avatar.powerUpState = new SmallState(avatar);
                avatar.FindCheckpoint();
            }
        }

    }
}
