using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace EmodiaQuest.Core.GUI
{
    class Controls_GUI
    {
        private static Controls_GUI instance;
        private Controls_GUI() { }
        public static Controls_GUI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Controls_GUI();
                }
                return instance;
            }
        }

        // Declare of the MouseState for the GUI
        // Should be set to the middle of the screen when opening menu TODO
        //public MouseState Mouse_GUI { get; set; }
        public MouseState lastMouseState;
        public MouseState currentMouseState;

        public KeyboardState lastKeyboardState;
        public KeyboardState currentKeyboardState;


        public void loadContent()
        {
            currentMouseState = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();
        }
        public void update()
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            lastKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

        }

        // Different possible control states for mouse and keyboard

        public bool mousePressedLeft()
        {
            if (currentMouseState.LeftButton == ButtonState.Pressed)
                return true;
            return false;
        }
        public bool mouseClickAndHoldLeft()
        {
            if (currentMouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                return true;
            return false;
        }
        public bool mouseClickAndHoldRight()
        {
            if (currentMouseState.RightButton == ButtonState.Pressed && lastMouseState.RightButton == ButtonState.Released)
                return true;
            return false;
        }
        public bool mouseReleasedLeft()
        {
            if (currentMouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed)
                return true;
            return false;
        }
        public bool mouseClickedLeft()
        {
            if (currentMouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed)
                return true;
            return false;
        }
        public bool mouseClickedRight()
        {
            if (currentMouseState.RightButton == ButtonState.Released && lastMouseState.RightButton == ButtonState.Pressed)
                return true;
            return false;
        }
        public bool keyClicked(Microsoft.Xna.Framework.Input.Keys inputKey)
        {
            if (currentKeyboardState.IsKeyDown(inputKey) && !lastKeyboardState.IsKeyDown(inputKey))
                return true;
            return false;
        }
    }
}
