using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace template_test
{
    class KeyboardController : IController
    {
        private KeyboardState previousKeyboardState;
        public Dictionary<Keys, ICommand> commandDict;
        public Dictionary<Keys, ICommand> moveCommandDict;
        public Dictionary<Keys, ICommand> releaseCommandDict;
        ICommand command;
        

        public KeyboardController()
        {
            
            commandDict = new Dictionary<Keys, ICommand>();
            moveCommandDict = new Dictionary<Keys, ICommand>();
            releaseCommandDict = new Dictionary<Keys, ICommand>();
            previousKeyboardState = Keyboard.GetState();
            


        }

        public void Update()
        {
            KeyboardState currentState = Keyboard.GetState();
            Keys[] keysPressed = currentState.GetPressedKeys();
            Keys[] prevKeyPressed = previousKeyboardState.GetPressedKeys();
            foreach (Keys key in keysPressed)
            {
                if(!previousKeyboardState.IsKeyDown(key) && commandDict.ContainsKey(key))
                {
                    command = commandDict[key];
                    command.Execute();
                }
                else if(moveCommandDict.ContainsKey(key))
                {
                    command = moveCommandDict[key];
                    command.Execute();
                }
            }
            foreach (Keys prevKey in prevKeyPressed)
	        {
                if (releaseCommandDict.ContainsKey(prevKey) && !currentState.IsKeyDown(prevKey))
                {
                    command = releaseCommandDict[prevKey];
                    command.Execute();
                }
            }               
            previousKeyboardState = currentState;
        }
    }
}
