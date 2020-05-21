using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class WinningGameState : GameState
    {
        private List<IController> controllers;
        private KeyboardController keyboard;
        private GamepadController gamepad;
        private SpriteFont font;
        private HudObject hud;
        private float jump_height;
        private GraphicsDeviceManager graphicsManager;

        public WinningGameState(GraphicsDevice graphicsDevice, HudObject hud, float mario_height, GraphicsDeviceManager gManager)
            : base(graphicsDevice)
        {
            this.hud = hud;
            // using 434 as the floor height
            jump_height = (434 - mario_height);
            graphicsManager = gManager;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            graphics.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "You Won", new Vector2(300, 240), Color.White);
            spriteBatch.DrawString(font, "Press R to Reset", new Vector2(280, 280), Color.White);
            spriteBatch.DrawString(font, "Press Q to Quit", new Vector2(280, 320), Color.White);
            hud.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void Initialize()
        {
            controllers = new List<IController>();
        }

        public override void LoadContent(ContentManager content)
        {
            keyboard = new KeyboardController();
            gamepad = new GamepadController();
            font = content.Load<SpriteFont>("temp_font");
            keyboard.commandDict.Add(Keys.Q, new QuitCommand());
            keyboard.commandDict.Add(Keys.R, new ResetCommand(graphics, graphicsManager, hud.audio));
            controllers.Add(keyboard);
            controllers.Add(gamepad);
            FinalScore();
        }

        public void FinalScore()
        {
            //assume flag pole height of 150 and starting time as 400.  using int division,
            //each increment of 50 gives another 'point' to the multiplier, base value of 1
            //i.e. 113 seconds remaining gives 3 (113/50 = 2 + 1 = 3) and a jump 63 px from ground
            // gives 2 (63/50 = 1 + 1 = 2) for a total multiplier of 5
            int jump_mult = ((int)jump_height / 50) + 1;
            int time_mult = ((int)hud.time_remaining / 50) + 1;
            int total_mult = jump_mult + time_mult;
            int additonal_score = (int)hud.time_remaining * total_mult;
            hud.ChangeScore(additonal_score);
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }
        }
    }
}
