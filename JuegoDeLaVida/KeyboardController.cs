using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoDeLaVida
{
    class KeyboardController
    {
        private List<Keys> pressedKeys;
        private List<Keys> lastTickReleased;
        private List<Keys> lastTickPressed;

        private static KeyboardController instance;

        public KeyboardController()
        {
            pressedKeys = new List<Keys>();
            lastTickReleased = new List<Keys>();
            lastTickPressed = new List<Keys>();
        }

        public static KeyboardController getInstance()
        {
            if (instance == null)
            {
                instance = new KeyboardController();
            }
            return instance;
        }

        public void update()
        {
            lastTickReleased = new List<Keys>();
            lastTickPressed = new List<Keys>();

            Keys[] keys = Keyboard.GetState().GetPressedKeys();
            foreach (Keys key in keys)
            {
                if (!pressedKeys.Contains(key))
                {
                    lastTickPressed.Add(key);
                    pressedKeys.Add(key);
                }
            }
            foreach (Keys key in pressedKeys)
            {
                if (!keys.Contains(key))
                {
                    lastTickReleased.Add(key);
                }
            }
            foreach (Keys key in lastTickReleased)
                pressedKeys.Remove(key);
        }

        public bool keyDown(Keys key)
        {
            return pressedKeys.Contains(key);
        }

        public bool keyUp(Keys key)
        {
            return !pressedKeys.Contains(key);
        }

        public bool keyReleased(Keys key)
        {
            return lastTickReleased.Contains(key);
        }

        public bool keyPressed(Keys key)
        {
            return lastTickPressed.Contains(key);
        }
    }
}