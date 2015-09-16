using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace EmodiaQuest.Core.GUI
{
    public class DialogueBox_GUI
    {
        public float XPosRelative { get; set; }
        public float YPosRelative { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public float WidthRelative { get; set; }
        public float HeightRelative { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int TextXPos { get; set; }
        public int TextYPos { get; set; }
        public SpriteFont SpriteFont { get; set; }
        public Texture2D ButtonUpTexture { get; set; }
        public Texture2D ButtonDownTexture { get; set; }
        public string LabelText { get; set; }
        public string LabelName { get; set; }
        public float TextScaleFactor { get; set; }
        public bool Centered { get; set; }
        public bool IsVisible = true;
        public bool BoxIsShown = true;
        public float ScaleFactor = 1.0f;
        public int textPlus = 0;

        public float MovementSpeed = 1.0f;
        public float Movement = 0.0f;
        public bool IsMoving = false;

        // Buttons
        public float xPosP1 = 0;
        public float yPosP1 = 0;
        public float widthP1 = 0;
        public float heightP1 = 0;

        public float xPosP2 = 0;
        public float yPosP2 = 0;
        public float widthP2 = 0;
        public float heightP2 = 0;

        public int xPosP1Int = 0;
        public int yPosP1Int = 0;
        public int widthP1Int = 0;
        public int heightP1Int = 0;

        public int xPosP2Int = 0;
        public int yPosP2Int = 0;
        public int widthP2Int = 0;
        public  int heightP2Int = 0;

        public List<DialogueString_GUI> dialogueStrings = new List<DialogueString_GUI>();


        public DialogueBox_GUI(float xPosRelative, float yPosRelative, int xPos, int yPos, float widthRelative, float heightRelative, int width, int height, SpriteFont spriteFont, string labelText, string labelName, int textXPos, int textYPos, float textScaleFactor, int mainWindowSizeX, int mainWindowSizeY, Texture2D startTex)
        {
            this.XPosRelative = xPosRelative;
            this.YPosRelative = yPosRelative;
            this.XPos = xPos;
            this.YPos = yPos;
            this.WidthRelative = widthRelative;
            this.HeightRelative = heightRelative;
            this.Width = width;
            this.Height = height;
            this.SpriteFont = spriteFont;
            this.ButtonUpTexture = startTex;
            this.ButtonDownTexture = startTex;
            this.LabelText = labelText;
            this.LabelName = labelName;
            this.TextXPos = textXPos;
            this.TextYPos = textYPos;
            this.TextScaleFactor = textScaleFactor;

            this.Movement = 0;

            string stringText = labelText;
            string[] passages = stringText.Split(';');
            foreach (string passage in passages)
            {
                dialogueStrings.Add(new DialogueString_GUI(0, passage, false));
            }

            setButtons(mainWindowSizeX, mainWindowSizeY);
        }

        //public DialogueBox_GUI(float xPosRelative, float yPosRelative, int xPos, int yPos, float widthRelative, float heightRelative, int width, int height, SpriteFont spriteFont, string labelText, string labelName, int textXPos, int textYPos, float textScaleFactor, bool justToCopy)
        //{
        //    this.XPosRelative = xPosRelative;
        //    this.YPosRelative = yPosRelative;
        //    this.XPos = xPos;
        //    this.YPos = yPos;
        //    this.WidthRelative = widthRelative;
        //    this.HeightRelative = heightRelative;
        //    this.Width = width;
        //    this.Height = height;
        //    this.SpriteFont = spriteFont;
        //    this.LabelText = labelText;
        //    this.LabelName = labelName;
        //    this.TextXPos = textXPos;
        //    this.TextYPos = textYPos;
        //    this.TextScaleFactor = textScaleFactor;

        //    string stringText = labelText;
        //    string[] passages = stringText.Split(';');
        //    foreach (string passage in passages)
        //    {
        //        dialogueStrings.Add(new DialogueString_GUI(0, passage, false));
        //    }
            
        //}

        public void updateText(string newText)
        {
            dialogueStrings.Clear();
            this.textPlus = 0;
            this.Movement = 0;
            string stringText = newText;
            string[] passages = stringText.Split(';');
            foreach (string passage in passages)
            {
                dialogueStrings.Add(new DialogueString_GUI(0, passage, false));
            }
        }

        public void setButtons(int mainWindowSizeX, int mainWindowSizeY)
        {
            this.xPosP1 = XPosRelative + WidthRelative - WidthRelative * 0.1f - 1;
            this.yPosP1 = YPosRelative + 1;
            this.widthP1 = WidthRelative * 0.1f;
            this.heightP1 = widthP1;

            this.xPosP2 = xPosP1;
            this.yPosP2 = yPosP1 + widthP1 + 2;
            this.widthP2 = widthP1;
            this.heightP2 = widthP1;

            this.xPosP1Int = (int)(mainWindowSizeX * xPosP1 * 0.01);
            this.yPosP1Int = (int)(mainWindowSizeY * yPosP1 * 0.01);
            this.widthP1Int = (int)(mainWindowSizeX * widthP1 * 0.01);
            this.heightP1Int = (int)(mainWindowSizeY * heightP1 * 0.01);

            this.xPosP2Int = (int)(mainWindowSizeX * xPosP2 * 0.01);
            this.yPosP2Int = (int)(mainWindowSizeY * yPosP2 * 0.01);
            this.widthP2Int = (int)(mainWindowSizeX * widthP2 * 0.01);
            this.heightP2Int = (int)(mainWindowSizeY * heightP2 * 0.01);


        }
    }
}
