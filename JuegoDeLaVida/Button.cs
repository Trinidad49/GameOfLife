using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace JuegoDeLaVida
{
    public class Button
    {
        private int buttonX, buttonY,width,height;
        private Rectangle rectangulo;

        public Button(int w, int h, int buttonX, int buttonY)
        {
            this.buttonX = buttonX;
            this.buttonY = buttonY;
            this.width = w;
            this.height = h;
            this.rectangulo = new Rectangle(buttonX,buttonY,w,h);
        }

        public bool EnterButton()
        {
            if (MouseController.getInstance().getLocation().X < buttonX + width &&
                    MouseController.getInstance().getLocation().X > buttonX &&
                    MouseController.getInstance().getLocation().Y < buttonY + height &&
                    MouseController.getInstance().getLocation().Y > buttonY)
            {
                return true;
            }
            return false;
        }

        public Rectangle GetRectangle()
        {
                return rectangulo;
        }

        public int ButtonWidth()
        {
                return width;
        }

        public int ButtonHeight()
        {
                return height;
        }

        public int ButtonX()
        {
                return buttonX;
        }

        public int ButtonY()
        {
                return buttonY;
        }
    }
}