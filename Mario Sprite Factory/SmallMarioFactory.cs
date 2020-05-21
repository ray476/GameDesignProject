using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace template_test
{
    class SmallMarioFactory : IMarioFactory
    {
        ISprite product;
        ContentManager content;

        public SmallMarioFactory(ContentManager manager)
        {
            content = manager;
        }

        public ISprite build(IMovementState mState)
        {
            if(mState is LeftIdleState)
            {
                Texture2D texture = content.Load<Texture2D>("mario_idle_small");
                // not sure about how to best deal with position of the sprite.  pass a mario all the way
                // to this point and might as well assign his sprite directly rather than return a sprite
                // going to default to 0,0 then let the state change the sprite location to match the objects
                product = new SpriteAnimated(texture, 1, 1, 30, false);
            } else if(mState is LeftJumpingIdleState || mState is LeftIdleFallingState)
            {
                Texture2D texture = content.Load<Texture2D>("mario_jump_small");
                product = new SpriteAnimated(texture, 1, 1, 30, false);
            } else if (mState is LeftJumpingState || mState is LeftFallingState){
                //this repeats with above, possible to optimize down the number of elseif branches at a later date
                Texture2D texture = content.Load<Texture2D>("mario_jump_small");
                product = new SpriteAnimated(texture, 1, 1, 30, false);
            } else if (mState is LeftWalkingState)
            {
                Texture2D texture = content.Load<Texture2D>("mario_small_walk");
                product = new SpriteAnimated(texture, 1, 3, 12, false);
            } // mState should only be null during initialization
            else if (mState is RightIdleState || mState is null)
            {
                Texture2D texture = content.Load<Texture2D>("mario_idle_small");
                product = new SpriteStatic(texture, true);
            } else if (mState is RightJumpingIdleState || mState is RightIdleFallingState)
            {
                Texture2D texture = content.Load<Texture2D>("mario_jump_small");
                product = new SpriteStatic(texture, true);
            } else if (mState is RightJumpingState || mState is RightFallingState)
            {
                Texture2D texture = content.Load<Texture2D>("mario_jump_small");
                product = new SpriteStatic(texture, true);
            } else if (mState is RightWalkingState)
            {
                Texture2D texture = content.Load<Texture2D>("mario_small_walk");
                product = new SpriteAnimated(texture, 1, 3, 12, true);
            }
            else if (mState is DeadState)
            {
                Texture2D texture = content.Load<Texture2D>("mario_die");
                product = new SpriteStatic(texture, true);

            }
            else if (mState is RightCrouchingState)
            {
                Texture2D texture = content.Load<Texture2D>("mario_idle_small");
                product = new SpriteStatic(texture, true);
            } 
            else if (mState is LeftCrouchingState)
            {
                Texture2D texture = content.Load<Texture2D>("mario_idle_small");
                product = new SpriteAnimated(texture,1,1,12, false);
            }

            return product;
        }
    }
}
