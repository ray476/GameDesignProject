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
    class MainMenuState : GameState
    {
        private List<IController> controllers;
        private KeyboardController keyboard;
        private GamepadController gamepad;
        private SpriteFont font;
        private GraphicsDeviceManager graphicsManager;

        public MainMenuState(GraphicsDevice graphicsDevice, GraphicsDeviceManager gManager)
            : base(graphicsDevice)
        {
            graphicsManager = gManager;
            graphicsManager.PreferredBackBufferWidth = 800;
            graphicsManager.PreferredBackBufferHeight = 480;
            graphicsManager.ApplyChanges();
        }


        override public void Initialize()
        {
            controllers = new List<IController>();

        }

        override public void LoadContent(ContentManager content)
        {
            keyboard = new KeyboardController();
            gamepad = new GamepadController();
            keyboard.commandDict.Add(Keys.Enter, new SMBStartCommand(graphics, graphicsManager));
            gamepad.commandDict.Add(Buttons.Start, new SMBStartCommand(graphics, graphicsManager));
            keyboard.commandDict.Add(Keys.Space, new DJStartCommand(graphics, graphicsManager));
            gamepad.commandDict.Add(Buttons.Back, new DJStartCommand(graphics, graphicsManager));
            keyboard.commandDict.Add(Keys.Q, new QuitCommand());
            controllers.Add(keyboard);
            controllers.Add(gamepad);
            font = content.Load<SpriteFont>("temp_font");
        }

        override public void UnloadContent()
        {

        }

        override public void Update(GameTime gameTime)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }
        }

        override public void Draw(SpriteBatch spriteBatch)
        {
            graphics.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "PRESS ENTER OR START TO BEGIN", new Vector2(150, 140), Color.White);
            spriteBatch.DrawString(font, "PRESS SPACE OR BACK FOR DOODLE JUMP", new Vector2(110, 340), Color.White);
            spriteBatch.DrawString(font, "PRESS Q TO QUIT", new Vector2(150, 240), Color.White);
            spriteBatch.End();
        }
    }
}
