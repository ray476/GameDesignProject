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
    class PauseGameState : GameState
    {
        private List<IController> controllers;
        private KeyboardController keyboard;
        private GamepadController gamepad;
        private AudioManager audio;
        private HudObject hud;
        private SpriteFont font;
        private GraphicsDeviceManager graphicsManager;
        private Vector2 CoC;


        public PauseGameState(GraphicsDevice graphicsDevice, HudObject hud, AudioManager audio, GraphicsDeviceManager gManager)
            : base(graphicsDevice)
        {
            this.hud = hud;
            this.audio = audio;
            graphicsManager = gManager;
            CoC = new Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2);
        }

        public override void Initialize()
        {
            controllers = new List<IController>();
        }

        public override void LoadContent(ContentManager content)
        {
            keyboard = new KeyboardController();
            gamepad = new GamepadController();
            keyboard.commandDict.Add(Keys.P, new PauseCommand(graphics, hud, audio, graphicsManager));
            gamepad.commandDict.Add(Buttons.Back, new PauseCommand(graphics, hud, audio, graphicsManager));
            keyboard.commandDict.Add(Keys.M, new MainMenuCommand(graphics, graphicsManager));
            keyboard.commandDict.Add(Keys.Q, new QuitCommand());
            controllers.Add(keyboard);
            controllers.Add(gamepad);
            font = content.Load<SpriteFont>("temp_font");

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

        public override void Draw(SpriteBatch spriteBatch)
        {
            // dynamically changing string locations as follows.  graphics.viewport.height/width / 2 to find the center of the screen
            // the subtracting by an offset to shift text to the left in an attempt to center text on screen. (same for Y direction)
            // I am guessing on the offsets right now, can be tweeked for better a e s t h e t i c time permitting
            graphics.Clear(Color.Black);
            spriteBatch.Begin();
            hud.Draw(spriteBatch);
            spriteBatch.DrawString(font, "GAME PAUSED", new Vector2(CoC.X-120, CoC.Y-50), Color.White);
            spriteBatch.DrawString(font, "Press M for Main Menu", new Vector2(CoC.X-160, CoC.Y+50), Color.White);
            spriteBatch.DrawString(font, "Press Q to Quit", new Vector2(CoC.X-120, CoC.Y+80), Color.White);
            spriteBatch.End();
        }
    }
}
