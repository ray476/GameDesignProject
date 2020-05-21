using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class LeaderBoardState : GameState
    {
        private List<IController> controllers;
        private KeyboardController keyboard;
        private GamepadController gamepad;
        private KeyValuePair<int, string> first;
        private KeyValuePair<int, string> second;
        private KeyValuePair<int, string> third;
        private KeyValuePair<int, string> fourth;
        private KeyValuePair<int, string> fifth;
        private SpriteFont font;
        private HudObject hud;
        private SortedDictionary<int, string> board;
        private NameObject name;
        AudioManager audioManager;
        GraphicsDeviceManager gManager;
        public LeaderBoardState(GraphicsDevice graphicsDevice,NameObject player,GraphicsDeviceManager gman,AudioManager audio)
            : base(graphicsDevice)
        {
            name = player;
            gManager = gman;
            audioManager = audio;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            graphics.Clear(Microsoft.Xna.Framework.Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "SCORE", new Vector2(475, 200), Microsoft.Xna.Framework.Color.White);
            spriteBatch.DrawString(font, "PLAYER", new Vector2(300, 200), Microsoft.Xna.Framework.Color.White);
            spriteBatch.DrawString(font, "RANK", new Vector2(125, 200), Microsoft.Xna.Framework.Color.White);
            spriteBatch.DrawString(font, "1st", new Vector2(125, 300), Microsoft.Xna.Framework.Color.White);
            spriteBatch.DrawString(font, "2nd", new Vector2(125, 350), Microsoft.Xna.Framework.Color.White);
            spriteBatch.DrawString(font, "3rd", new Vector2(125, 400), Microsoft.Xna.Framework.Color.White);
            spriteBatch.DrawString(font, "4th", new Vector2(125, 450), Microsoft.Xna.Framework.Color.White);
            spriteBatch.DrawString(font, "5th", new Vector2(125, 500), Microsoft.Xna.Framework.Color.White);
            spriteBatch.DrawString(font, fifth.Value, new Vector2(300, 500), Microsoft.Xna.Framework.Color.White);
            spriteBatch.DrawString(font, fourth.Value, new Vector2(300, 450), Microsoft.Xna.Framework.Color.White);
            spriteBatch.DrawString(font, third.Value, new Vector2(300, 400), Microsoft.Xna.Framework.Color.White);
            spriteBatch.DrawString(font, second.Value, new Vector2(300, 350), Microsoft.Xna.Framework.Color.White);
            spriteBatch.DrawString(font, first.Value, new Vector2(300, 300), Microsoft.Xna.Framework.Color.White);
            spriteBatch.DrawString(font, fifth.Key.ToString(), new Vector2(475, 500), Microsoft.Xna.Framework.Color.White);
            spriteBatch.DrawString(font, fourth.Key.ToString(), new Vector2(475, 450), Microsoft.Xna.Framework.Color.White);
            spriteBatch.DrawString(font, third.Key.ToString(), new Vector2(475, 400), Microsoft.Xna.Framework.Color.White);
            spriteBatch.DrawString(font, second.Key.ToString(), new Vector2(475, 350), Microsoft.Xna.Framework.Color.White);
            spriteBatch.DrawString(font, first.Key.ToString(), new Vector2(475, 300), Microsoft.Xna.Framework.Color.White);
            spriteBatch.DrawString(font, "Press [Q,q] to Quit", new Vector2(125, 600), Microsoft.Xna.Framework.Color.White);
            spriteBatch.DrawString(font, "Press [R,r] to Reset", new Vector2(125, 650), Microsoft.Xna.Framework.Color.White);
            spriteBatch.End();

        }

        public override void Initialize()
        {
            controllers = new List<IController>();
            board = new SortedDictionary<int, string>();
            
        }

        public override void LoadContent(ContentManager content)
        {
            keyboard = new KeyboardController();
            gamepad = new GamepadController();
            board.Add(name.score, name.name);
            using (StreamReader stream = new StreamReader("LeaderBoard.txt"))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    string[] words = line.Split(' ');
                    board.Add(Int32.Parse(words[0]), words[1]);
                }
                board.Remove(board.Keys.First());
                int i = 5;
                foreach(KeyValuePair<int,string> pair in board)
                {
                    switch (i)
                    {
                        case 5:
                            fifth = pair;
                            break;
                        case 4:
                            fourth = pair;
                            break;
                        case 3:
                            third = pair;
                            break;
                        case 2:
                            second = pair;
                            break;
                        case 1:
                            first = pair;
                            break;
                    }
                    i--;

                }
                
            }
            File.WriteAllText("LeaderBoard.txt", string.Empty);
            using (StreamWriter sw = new StreamWriter("LeaderBoard.txt"))
            {
                foreach (KeyValuePair<int,string> pair in board)
                {
                    sw.WriteLine(pair.Key.ToString() + " " + pair.Value);
                }
            }
            font = content.Load<SpriteFont>("temp_font");
            keyboard.commandDict.Add(Keys.Q, new QuitCommand());
            keyboard.commandDict.Add(Keys.R, new ResetDJCommand(graphics,gManager,audioManager));
            controllers.Add(keyboard);
            controllers.Add(gamepad);
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < 1; i++)
                foreach (IController controller in controllers)
                {
                    controller.Update();
                }
        }
    }
}

