using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace template_test
{
    class GamepadController : IController
    {
        GamePadState previousGamepadState;
        public Dictionary<Buttons, ICommand> commandDict;
        public Dictionary<Buttons, ICommand> moveCommandDict;
        public Dictionary<Buttons, ICommand> releaseCommandDict;
        
        GamePadState emptyInput;
        public GamepadController()
        {
            commandDict = new Dictionary<Buttons, ICommand>();
            moveCommandDict = new Dictionary<Buttons, ICommand>();
            releaseCommandDict = new Dictionary<Buttons, ICommand>();
            previousGamepadState = GamePad.GetState(PlayerIndex.One);
            emptyInput = new GamePadState(Vector2.Zero, Vector2.Zero, 0, 0, new Buttons());


        }

        public void Update()
        {
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);
            if (currentState.IsConnected)
            {
                if (currentState != emptyInput) // Button Pressed
                {

                    var possibleButtons = (Buttons[])Enum.GetValues(typeof(Buttons));

                    foreach (var button in possibleButtons)
                    {
                        if (currentState.IsButtonDown(button) &&
                            !previousGamepadState.IsButtonDown(button) &&
                            commandDict.ContainsKey(button))
                            commandDict[button].Execute();
                        else if(currentState.IsButtonDown(button) && moveCommandDict.ContainsKey(button))
                        {
                          moveCommandDict[button].Execute();        
                        }
                        else if (previousGamepadState.IsButtonDown(button) && !currentState.IsButtonDown(button) && releaseCommandDict.ContainsKey(button))
                        {
                            releaseCommandDict[button].Execute();
                        }
                    }
                }
                previousGamepadState = currentState;
            }
        }
    }
}
