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
    class PlayerNameState : GameState
    {
        private List<IController> controllers;
        private KeyboardController keyboard;
        private GamepadController gamepad;
        private SpriteFont font;
        private NameObject playerName;
        private AudioManager _audio;
        private GraphicsDeviceManager gman;
        public PlayerNameState(GraphicsDevice graphicsDevice,NameObject player,AudioManager audio,GraphicsDeviceManager gManager,int score)
            : base(graphicsDevice)
        {
            playerName = player;
            playerName.score = score;
            gman = gManager;
            _audio = audio;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            
            graphics.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Score: " + playerName.score.ToString(), new Vector2(100, 100), Color.White);
            spriteBatch.DrawString(font, "Please Enter Your Name", new Vector2(100, 200), Color.White);
            spriteBatch.DrawString(font, playerName.arr[0].ToString(), new Vector2(125, 500), Color.White);
            spriteBatch.DrawString(font, playerName.arr[1].ToString(), new Vector2(225, 500), Color.White);
            spriteBatch.DrawString(font, playerName.arr[2].ToString(), new Vector2(325, 500), Color.White);
            spriteBatch.DrawString(font, playerName.arr[3].ToString(), new Vector2(425, 500), Color.White);
            spriteBatch.DrawString(font, playerName.arr[4].ToString(), new Vector2(525, 500), Color.White);
            playerName.Draw(spriteBatch);
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
            keyboard.commandDict.Add(Keys.Left, new NameLeftCommand(playerName));
            keyboard.commandDict.Add(Keys.Right, new NameRightCommand(playerName));
            keyboard.commandDict.Add(Keys.Enter, new NameEnterCommand(graphics,playerName,gman,_audio));
            keyboard.commandDict.Add(Keys.Up, new NameUpCommand(playerName));
            keyboard.commandDict.Add(Keys.Down, new NameDownCommand(playerName));
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
    }
}

