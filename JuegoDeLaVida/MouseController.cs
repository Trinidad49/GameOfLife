using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoDeLaVida
{
    //Class that controlls the mouse
    class MouseController
    {

        public static MouseController instance;

        public static MouseController getInstance()
        {
            if (instance == null)
                instance = new MouseController();
            return instance;
        }

        private Texture2D cursor;

        private bool lastLeft;
        private bool lastRight;
        private bool currentLeft;
        private bool currentRight;
        private int prevWheelValue;
        private int actualWheelValue;

        public MouseController()
        {
            lastLeft = false;
            lastRight = false;
            currentLeft = false;
            currentRight = false;
            prevWheelValue = 0;
            actualWheelValue = 0;
        }

        public void update()
        {
            lastRight = currentRight;
            lastLeft = currentLeft;
            prevWheelValue = actualWheelValue;

            currentLeft = Mouse.GetState().LeftButton == ButtonState.Pressed;
            currentRight = Mouse.GetState().RightButton == ButtonState.Pressed;
            actualWheelValue = Mouse.GetState().ScrollWheelValue;

        }

        public void load(ContentManager contentManager)
        {
            cursor = contentManager.Load<Texture2D>("cursor");
        }

        public void draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRect = new Rectangle(Point.Zero, new Point(cursor.Width, cursor.Height));
            Rectangle destinationRect = new Rectangle(Mouse.GetState().Position, new Point(16, 16));
            spriteBatch.Draw(cursor, destinationRect, sourceRect, Color.White);
        }

        public Point getLocation()
        {
            return Mouse.GetState().Position;
        }
        
        public bool leftClick()
        {
            return !lastLeft && currentLeft;
        }

        public bool rightClick()
        {
            return !lastRight && currentRight;
        }

        public bool leftReleased()
        {
            return lastLeft && !currentLeft;
        }

        public bool rightReleased()
        {
            return lastRight && !currentRight;
        }

        public bool leftDown()
        {
            return currentLeft;
        }

        public bool rightDown()
        {
            return currentRight;
        }

        public float mouseWheel()
        {
            if (prevWheelValue > actualWheelValue)
                return -1f;
            if (prevWheelValue < actualWheelValue)
                return 1f;
            return 0f;
        }

    }
}