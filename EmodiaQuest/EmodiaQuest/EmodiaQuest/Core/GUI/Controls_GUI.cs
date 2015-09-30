using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

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

        private List<Wait_GUI> waitList = new List<Wait_GUI>();
        private double elapsedTime = 0.0;


        public void loadContent()
        {
            currentMouseState = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();
        }
        public void update(GameTime gameTime)
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            lastKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();


            elapsedTime = gameTime.ElapsedGameTime.TotalSeconds;
            //Console.WriteLine(elapsedTime);

            for (int i = 0; i < waitList.Count; i++)
			{
			    if(waitList[i].Finished)
                     waitList.RemoveAt(i);
                else
                    waitList[i].CurrentTime += elapsedTime;
			}
            foreach (var item in waitList)
            {
                Console.WriteLine(item.CurrentTime);
            }
        }

        // Add WaitPoint
        public void WaitPoint_New(string name, double length)
        {
            bool nameIsAlreadyPresent = false;
            foreach (Wait_GUI wa in waitList.Where(n => n.Name == name))
                nameIsAlreadyPresent = true;

            if(nameIsAlreadyPresent)
                Console.WriteLine("This WaitPoint exists already! Names HAVE to be unique!");
            else
                waitList.Add(new Wait_GUI(name, length));
        }

        // Check if the WaitPoint is finished
        public bool WaitPoint_IsFinished(string name)
        {
            foreach (Wait_GUI wa in waitList.Where(n => n.Name == name))
            {
                if (wa.Finished)
                    return true;
            }
            return false;
        }

        // Remove WaitPoint
        public void WaitPoint_Remove(string name)
        {
            for (int i = 0; i < waitList.Count; i++)
            {
                if(waitList[i].Name == name)
                    waitList.RemoveAt(i);
            }
        }

        // Set length for WaitPoint
        public void WaitPoint_SetLength(string name, double newLength)
        {
            foreach (Wait_GUI wa in waitList.Where(n => n.Name == name))
                wa.ClosingTime = newLength;
        }

        // Lengthen WaitPoint
        public void WaitPoint_Lengthen(string name, double lengthenPlus)
        {
            foreach (Wait_GUI wa in waitList.Where(n => n.Name == name))
                wa.ClosingTime += lengthenPlus;
        }

        // Factor Length WaitPoint
        public void WaitPoint_FactorLength(string name, double factor)
        {
            foreach (Wait_GUI wa in waitList.Where(n => n.Name == name))
                wa.ClosingTime *= factor;
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
