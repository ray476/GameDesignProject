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
    class RedDoodleFactory : IDoodleSpriteFactory
    {
        ISprite product;
        ContentManager content;

        public RedDoodleFactory(ContentManager manager)
        {
            content = manager;
        }

        public ISprite build(AbsDoodleMoveState mState)
        {
            if (mState is DoodleIdleLeftState)
            {
                Texture2D texture = content.Load<Texture2D>("bouncy_idle_red");
                product = new SpriteAnimated(texture, 1, 25, 12, false);
            }
            else if (mState is DoodleIdleRightState || mState is null)
            {
                Texture2D texture = content.Load<Texture2D>("bouncy_idle_red");
                product = new SpriteAnimated(texture, 1, 25, 12, true);
            }
            else if (mState is DoodleJumpingState)
            {
                //this repeats with above, possible to optimize down the number of elseif branches at a later date
                Texture2D texture = content.Load<Texture2D>("bouncy_jump_red");
                product = new SpriteAnimated(texture, 1, 3, 12, false);
            }
            else if (mState is DoodleFallingState)
            {
                Texture2D texture = content.Load<Texture2D>("bouncy_fall_red");
                product = new SpriteAnimated(texture, 1, 3, 12, false);
            } // mState should only be null during initialization
            else if (mState is DoodleWalkLeftState)
            {
                Texture2D texture = content.Load<Texture2D>("bouncy_run_red");
                product = new SpriteAnimated(texture, 1, 6, 12, false);
            }
            else if (mState is DoodleWalkRightState)
            {
                Texture2D texture = content.Load<Texture2D>("bouncy_run_red");
                product = new SpriteAnimated(texture, 1, 6, 12, true);
            }
            else if (mState is DoodleFlyingState)
            {
                Texture2D texture = content.Load<Texture2D>("bouncy_fly_red");
                product = new SpriteAnimated(texture, 1, 3, 12, false);
            }
                return product;
        }
    }
}

