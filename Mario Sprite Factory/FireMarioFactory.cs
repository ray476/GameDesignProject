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
    class FireMarioFactory : IMarioFactory
    {
        ISprite product;
        ContentManager content;

        public FireMarioFactory(ContentManager manager)
        {
            content = manager;
        }

        public ISprite build(IMovementState mState)
        {
            if (mState is LeftIdleState)
            {
                Texture2D texture = content.Load<Texture2D>("mario_fire_idle");
                // not sure about how to best deal with position of the sprite.  pass a mario all the way
                // to this point and might as well assign his sprite directly rather than return a sprite
                // going to default to 0,0 then let the state change the sprite location to match the objects
                product = new SpriteAnimated(texture, 1, 1, 24, false);
            }
            else if (mState is LeftJumpingIdleState)
            {
                Texture2D texture = content.Load<Texture2D>("mario_fire_jump");
                product = new SpriteAnimated(texture, 1, 1, 24, false);
            }
            else if (mState is LeftJumpingState)
            {
                //this repates with above, possible to optimize down the number of elseif branches at a later date
                Texture2D texture = content.Load<Texture2D>("mario_fire_jump");
                product = new SpriteAnimated(texture, 1, 1, 24, false);
            }
            else if (mState is LeftWalkingState)
            {
                Texture2D texture = content.Load<Texture2D>("mario_fire_walk");
                product = new SpriteAnimated(texture, 1, 3, 12, false);
            }
            else if (mState is RightIdleState || mState is null)
            {
                Texture2D texture = content.Load<Texture2D>("mario_fire_idle");
                product = new SpriteStatic(texture, true);
            }
            else if (mState is RightJumpingIdleState)
            {
                Texture2D texture = content.Load<Texture2D>("mario_fire_jump");
                product = new SpriteStatic(texture, true);
            }
            else if (mState is RightJumpingState)
            {
                Texture2D texture = content.Load<Texture2D>("mario_fire_jump");
                product = new SpriteStatic(texture, true);
            }
            else if (mState is RightWalkingState)
            {
                Texture2D texture = content.Load<Texture2D>("mario_fire_walk");
                product = new SpriteAnimated(texture, 1, 3, 12, true);
            }
            else if (mState is RightCrouchingState)
            {
                Texture2D texture = content.Load<Texture2D>("mario_fire_crouch");
                product = new SpriteStatic(texture, true);

            }
            else if (mState is LeftCrouchingState)
            {
                Texture2D texture = content.Load<Texture2D>("mario_fire_crouch");
                product = new SpriteAnimated(texture, 1, 1, 24, false);
            }
                return product;
        }
    }
}
