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
    class PlayState : GameState
    {
        private MarioObject avatar;
        private List<AbsObject> gameObjectsToAdd;
        private List<AbsObject> gameObjectsToRemove;
        private List<IController> controllers;
        private Camera camera;
        private List<Layer> layers;
        private Collision collision;
        private KeyboardController keyboard;
        private GamepadController gamepad;
        private SpriteFont font;
        private HudObject hud;
        private GraphicsDeviceManager graphicsManager;
        private ContentManager content;

        public PlayState(GraphicsDevice graphicsDevice, GraphicsDeviceManager gManager)
            :base(graphicsDevice)
        {
            graphicsManager = gManager;
            graphicsManager.PreferredBackBufferWidth = 800;
            graphicsManager.PreferredBackBufferHeight = 480;
            graphicsManager.ApplyChanges();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            graphics.Clear(Color.CornflowerBlue);
           
            foreach (Layer layer in layers)
            {
                layer.Draw(spriteBatch);
            }
            spriteBatch.Begin();
            hud.Draw(spriteBatch);
            spriteBatch.End();

            
        }

        public override void Initialize()
        { 
            controllers = new List<IController>();
            collision = new Collision(800, 480, 80, 48);
            
            gameObjectsToAdd = new List<AbsObject>();
            gameObjectsToRemove = new List<AbsObject>();
            camera = new Camera(graphics.Viewport) { Limits = new Rectangle(0, 0, 6000, 480) };
            layers = new List<Layer>
            {
                //background = 0 foreground = 1 hud = 2
                new Layer(camera) { Parallax = new Vector2(0.5f, 1.0f) },
                new Layer(camera) { Parallax = new Vector2(1.0f, 1.0f) },
                new Layer(camera) { Parallax = new Vector2(1.0f, 1.0f)}
            };
        }

        public override void LoadContent(ContentManager content)
        {
            this.content = content;
            font = content.Load<SpriteFont>("temp_font");
            hud = new HudObject(font);
            string name = "world_1_1_draft.txt";
            //string name = "test_level.txt";
            // figure out the file pathing later
            Tokenizer test = new Tokenizer(name, content);
            //gameObjects = gameObjects.Concat(test.GetSprites()).ToList();
            layers[1].Objects = test.GetSprites();
            layers[0].Objects.Add(new BGObject(content));
            //layers[2].Objects.Add(hud);
            foreach (AbsObject temp in layers[1].Objects)
            {
                if (temp is MarioObject)
                {
                    avatar = (MarioObject)temp;
                }
                else if (temp is EnemyObject)
                {
                    EnemyObject enemy = (EnemyObject)temp;
                    enemy.Mario = avatar;
                }
            }
            avatar.hud = hud;
            hud.audio = avatar.audio;
            keyboard = new KeyboardController();
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

            keyboard.commandDict.Add(Keys.Space, new ThrowFireballCommand(avatar));
            keyboard.commandDict.Add(Keys.Y, new StandardCommand(avatar));
            keyboard.commandDict.Add(Keys.U, new SuperCommand(avatar));
            keyboard.commandDict.Add(Keys.I, new FireCommand(avatar));
            keyboard.commandDict.Add(Keys.O, new TakeDamageCommand(avatar));
            keyboard.commandDict.Add(Keys.Q, new QuitCommand());
            keyboard.commandDict.Add(Keys.P, new PauseCommand(graphics, hud, avatar.audio, graphicsManager));
            keyboard.commandDict.Add(Keys.M, new MuteCommand(avatar.audio));
            keyboard.commandDict.Add(Keys.Tab, new ThemeCommand(avatar.audio));
            keyboard.commandDict.Add(Keys.R, new ResetCommand(graphics, graphicsManager, hud.audio));
            gamepad = new GamepadController();
            gamepad.moveCommandDict.Add(Buttons.A, new UpCommand(avatar));
            gamepad.releaseCommandDict.Add(Buttons.A, new DownCommand(avatar));
            gamepad.moveCommandDict.Add(Buttons.DPadDown, new DownCommand(avatar));
            gamepad.releaseCommandDict.Add(Buttons.DPadDown, new UpCommand(avatar));
            gamepad.moveCommandDict.Add(Buttons.DPadLeft, new LeftCommand(avatar));
            gamepad.releaseCommandDict.Add(Buttons.DPadLeft, new RightCommand(avatar));
            gamepad.moveCommandDict.Add(Buttons.DPadRight, new RightCommand(avatar));
            gamepad.releaseCommandDict.Add(Buttons.DPadRight, new LeftCommand(avatar));
            gamepad.commandDict.Add(Buttons.B, new ThrowFireballCommand(avatar));
            gamepad.commandDict.Add(Buttons.Start, new QuitCommand());
            gamepad.commandDict.Add(Buttons.Back, new PauseCommand(graphics, hud, avatar.audio, graphicsManager));
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
            avatar.movementState.Update();
            hud.Update(gameTime);
            if(hud.lives_remaining == 0)
            {
                GameStateManager.GetInstance().Add(new GameOverGameState(graphics, hud, graphicsManager));
            } else if(avatar.Position.X >= 3168 && avatar.Position.X < 4000)
            {
                GameStateManager.GetInstance().Add(new WinningGameState(graphics, hud, avatar.Position.Y, graphicsManager));
            }
        }
    }
}
