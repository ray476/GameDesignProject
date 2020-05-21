using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace template_test
{
    class DoodleJumpState : GameState
    {
        private Tokenizer token;
        private SpriteFont font;
        private List<IController> controllers;
        private KeyboardController keyboard;
        private GamepadController gamepad;
        private DoodleObject avatar;
        private Camera camera;
        private List<AbsObject> gameObjectsToAdd;
        private List<AbsObject> gameObjectsToRemove;
        private List<Layer> layers;
        private Collision collision;
        private HudObject hud;
        private GraphicsDeviceManager graphicsManager;
        private NameObject name;
        // making these class variables to allow easy change
        private int windowWidth = 750;
        private int windowHeight = 1000;
        private Level_Generator generator;



        public DoodleJumpState(GraphicsDevice graphicsDevice, GraphicsDeviceManager gManager)
            : base(graphicsDevice)
        {
            graphicsManager = gManager;
            graphicsManager.PreferredBackBufferWidth = windowWidth;
            graphicsManager.PreferredBackBufferHeight = windowHeight;
            graphicsManager.ApplyChanges();
        }

        public override void Initialize()
        {
            controllers = new List<IController>();
            collision = new Collision(windowWidth, windowHeight, (windowWidth/10), (windowHeight/10));
            name = new NameObject(graphics);
            gameObjectsToAdd = new List<AbsObject>();
            gameObjectsToRemove = new List<AbsObject>();
            camera = new Camera(graphics.Viewport) { Limits = new Rectangle(0, 0, windowWidth, 3548) };
            layers = new List<Layer>
            {
                //background = 0 foreground = 1 platforms in 2 for now
                new Layer(camera) { Parallax = new Vector2(0.5f, 1.0f) },
                new Layer(camera) { Parallax = new Vector2(1.0f, 1.0f) },
            };
        }

        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("temp_font");
            hud = new HudObject(font);
            string name = "doodle_test.txt";
            token = new Tokenizer(name, content);
            avatar = new DoodleObject(new Vector2(windowWidth / 2, 3450), content, token.audio);
            camera.LookAt(avatar.Position);
            generator = new Level_Generator(content, camera, avatar, token.audio);
            generator.Initialize(layers[1]);
            //collision requires everything to be in the same list, so the platforms need to be with the avatar
            //changing the level floor to be platforms instead of blocks, no need for a level definition.
            //tokenizer does something with the audio so leaving the token object around for now
            layers[1].Objects.Add(new PlatformObject(new Vector2(0, 3540), -1, content));
            layers[1].Objects.Add(new PlatformObject(new Vector2(64, 3540), -1, content));
            layers[1].Objects.Add(new PlatformObject(new Vector2(128, 3540), -1, content));
            layers[1].Objects.Add(new PlatformObject(new Vector2(192, 3540), -1, content));
            layers[1].Objects.Add(new PlatformObject(new Vector2(256, 3540), -1, content));
            layers[1].Objects.Add(new PlatformObject(new Vector2(320, 3540), -1, content));
            layers[1].Objects.Add(new PlatformObject(new Vector2(384, 3540), -1, content));
            layers[1].Objects.Add(new PlatformObject(new Vector2(448, 3540), -1, content));
            layers[1].Objects.Add(new PlatformObject(new Vector2(512, 3540), -1, content));
            layers[1].Objects.Add(new PlatformObject(new Vector2(576, 3540), -1, content));
            layers[1].Objects.Add(new PlatformObject(new Vector2(640, 3540), -1, content));
            layers[1].Objects.Add(new PlatformObject(new Vector2(704, 3540), -1, content));
            //layers[1].Objects.Add(new PlatformObject(new Vector2(0, 3400), new Vector2(windowWidth, 3400), -1, content));
            // nvm try and keep avatar at the end of the list
            layers[1].Objects.Add(avatar);
            layers[0].Objects.Add(new VertBGObject(content));

            avatar.hud = hud;
            hud.audio = avatar.audio;

            keyboard = new KeyboardController();
            gamepad = new GamepadController();
            keyboard.commandDict.Add(Keys.Z, new MainMenuCommand(graphics, graphicsManager));
            controllers.Add(keyboard);
            controllers.Add(gamepad);
            keyboard.moveCommandDict.Add(Keys.Up, new UpCommand(avatar));
            keyboard.releaseCommandDict.Add(Keys.Up, new DownCommand(avatar));
            keyboard.moveCommandDict.Add(Keys.W, new UpCommand(avatar));
            keyboard.releaseCommandDict.Add(Keys.W, new DownCommand(avatar));

            keyboard.moveCommandDict.Add(Keys.Down, new DownCommand(avatar));
            keyboard.moveCommandDict.Add(Keys.S, new DownCommand(avatar));
            keyboard.releaseCommandDict.Add(Keys.Down, new UpCommand(avatar));
            keyboard.releaseCommandDict.Add(Keys.S, new UpCommand(avatar));

            keyboard.moveCommandDict.Add(Keys.Left, new LeftCommand(avatar));
            keyboard.moveCommandDict.Add(Keys.A, new LeftCommand(avatar));
            keyboard.releaseCommandDict.Add(Keys.Left, new RightCommand(avatar));
            keyboard.releaseCommandDict.Add(Keys.A, new RightCommand(avatar));

            keyboard.moveCommandDict.Add(Keys.Right, new RightCommand(avatar));
            keyboard.moveCommandDict.Add(Keys.D, new RightCommand(avatar));
            keyboard.releaseCommandDict.Add(Keys.Right, new LeftCommand(avatar));
            keyboard.releaseCommandDict.Add(Keys.D, new LeftCommand(avatar));

            keyboard.commandDict.Add(Keys.Y, new StandardCommand(avatar));
            keyboard.commandDict.Add(Keys.U, new SuperCommand(avatar));
            keyboard.commandDict.Add(Keys.I, new FireCommand(avatar));
            keyboard.commandDict.Add(Keys.O, new TakeDamageCommand(avatar));
            keyboard.commandDict.Add(Keys.Q, new QuitCommand());
            keyboard.commandDict.Add(Keys.P, new PauseCommand(graphics, hud, avatar.audio, graphicsManager));
            keyboard.commandDict.Add(Keys.M, new MuteCommand(avatar.audio));
            keyboard.commandDict.Add(Keys.Tab, new ThemeCommand(avatar.audio));
            keyboard.commandDict.Add(Keys.R, new ResetDJCommand(graphics, graphicsManager, hud.audio));

            avatar.audio.PlaySound("doodleTheme");
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {

            if(avatar.Position.Y > camera.Position.Y + 1200)
            {
                GameStateManager.GetInstance().Remove();
                GameStateManager.GetInstance().Add(new PlayerNameState(graphics,name,token.audio,graphicsManager,(int)generator.distance_travelled));
            }
            foreach (IController controller in controllers)
            {
                controller.Update();
            }

            foreach (AbsObject gameObject in layers[1].Objects)
            {
                if (gameObject._objectsToAdd.Any())
                {
                    foreach (AbsObject newObject in gameObject._objectsToAdd)
                    {
                        gameObjectsToAdd.Add(newObject);
                    }
                }
                if (gameObject.deleteThis)
                {
                    gameObjectsToRemove.Add(gameObject);
                }
                else if (gameObject != null)
                {
                    gameObject.Update(gameTime);
                }
            }

            collision.Update(layers[1].Objects);
            layers[1].Objects.AddRange(gameObjectsToAdd);
            layers[1].Objects = layers[1].Objects.Except(gameObjectsToRemove).ToList();
            camera.LookAt(avatar.Position);

            layers[1].Objects.Remove(avatar);
            //List<AbsObject> temp_list = new List<AbsObject>();
            //foreach (AbsObject gameObject in layers[1].Objects)
            //{
            //    if(gameObject is ItemObject)
            //    {
            //        int i = layers[1].Objects.IndexOf(gameObject);
            //        temp_list.Add(layers[1].Objects[i]);
            //        layers[1].Objects.Remove(gameObject);
            //    }
            //}

                generator.Update_Platforms(layers[1], camera);
            layers[1].Objects.Add(avatar);
            //layers[1].Objects.AddRange(temp_list);


            if(avatar.Position.Y < 1000)
            {
               Warp_All(layers[1].Objects);
            }


        }

        private void Warp_All(List<AbsObject> list)
        {
            foreach(AbsObject obj in list)
            {
                Vector2 new_pos = new Vector2(obj.Position.X, obj.Position.Y + 2000);
                obj.Position = new_pos;
                if (obj is PlatformObject)
                {
                    //n.b. hitbox does not currently update automatically when given a new position 
                    obj.Hitbox = new BoundingBox(new Vector3(new_pos.X, new_pos.Y, 0), new Vector3(new_pos.X + 192, new_pos.Y + 32, 0));
                } else
                {
                    obj.Hitbox = new BoundingBox(new Vector3(new_pos.X, new_pos.Y, 0), new Vector3(new_pos.X + 64, new_pos.Y + 64, 0));
                }
                //obj.Teleport(obj.Position.X, obj.Position.Y + 1500);
                if (obj is DoodleObject)
                {
                    camera.LookAt(obj.Position);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            graphics.Clear(Color.Black);

            foreach (Layer layer in layers)
            {
                layer.Draw(spriteBatch);
            }

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "WORK IN PROGRESS", new Vector2(20, 10), Color.Red);
            spriteBatch.DrawString(font, "Press Q to Quit", new Vector2(20, 30), Color.Red);
            spriteBatch.DrawString(font, "Press Z for Main Menu", new Vector2(20, 50), Color.Red);
            spriteBatch.End();

        }
    }
}
