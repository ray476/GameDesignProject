using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;


namespace template_test
{
    public class HudObject : AbsObject

    {
        public int coins;
        public int lives_remaining;
        public int time_remaining;
        public int score;
        private float time_to_update = 0.0f;
        private SpriteFont font;
        public AudioManager audio;

        public HudObject(SpriteFont spriteFont)
        {
            coins = 0;
            lives_remaining = 3;
            time_remaining = 400;
            score = 0;
            font = spriteFont;
            isVisible = true;
            _opacity = 1.0f;
        }

        public void GetCoin()
        {
            coins += 1;
            if(coins == 100)
            {
                ChangeLife(1);
                coins = 0;
            }
            ChangeScore(200);
        }

        public void ChangeScore(int delta)
        {
            score += delta;
        }

        public void ChangeLife(int delta)
        {
            lives_remaining += delta;
        }

        override public void Update(GameTime gameTime)
        {
            time_to_update += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(time_remaining == 0)
            {
                lives_remaining--;
                time_remaining = 200;
            }
            if (time_remaining == 100)
                audio.PlaySound("timeWarning");
            if (lives_remaining == 0)
                audio.PlaySound("gameOver");
            if(time_to_update > 1.0f)
            {
                time_remaining -= 1;
                time_to_update = 0.0f;
            }

        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "SCORE", new Vector2(150, 20), Color.White);
            spriteBatch.DrawString(font, score.ToString(), new Vector2(156, 45), Color.White);
            spriteBatch.DrawString(font, "COINS", new Vector2(300, 20), Color.White);
            spriteBatch.DrawString(font, coins.ToString(), new Vector2(316, 45), Color.White);
            spriteBatch.DrawString(font, "LIVES", new Vector2(450, 20), Color.White);
            spriteBatch.DrawString(font, lives_remaining.ToString(), new Vector2(470, 45), Color.White);
            spriteBatch.DrawString(font, "TIME", new Vector2(600, 20), Color.White);
            spriteBatch.DrawString(font, time_remaining.ToString(), new Vector2(606, 45), Color.White);

        }

        public override void Collide(List<AbsObject> collidedObjects)
        {
        
        }
    }
}
