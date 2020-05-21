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
    class GameOverGameState : GameState
    {
        private List<IController> controllers;
        private KeyboardController keyboard;
        private GamepadController gamepad;
        private SpriteFont font;
        private HudObject hud;
        private GraphicsDeviceManager graphicsManager;

        public GameOverGameState(GraphicsDevice graphicsDevice, HudObject hud, GraphicsDeviceManager gManager)
            : base(graphicsDevice)
        {
            this.hud = hud;
            graphicsManager = gManager;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            graphics.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "GAME OVER", new Vector2(300, 240), Color.White);
            spriteBatch.DrawString(font, "Press R to Reset", new Vector2(280, 280), Color.White);
            spriteBatch.DrawString(font, "Press Q to Quit", new Vector2(280, 320), Color.White);
            spriteBatch.DrawString(font, "Press M for Main Menu", new Vector2(230, 370), Color.White);
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
            keyboard.commandDict.Add(Keys.M, new MainMenuCommand(graphics, graphicsManager));
            controllers.Add(keyboard);
            controllers.Add(gamepad);
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
