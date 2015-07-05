using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace EmodiaQuest.Core.GUI
{
    class Settings_GUI
    {
        private static Settings_GUI instance;

        private Settings_GUI() { }

        public static Settings_GUI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Settings_GUI();
                }
                return instance;
            }
        }

        public void loadContent(ContentManager Content)
        {
            // Has to stay in this order!
            setResolution();
            //setFontSprites(Content);
            setFontSize();
        }

        public void setResolution()
        {
            Platform_GUI.MainWindowSize = EmodiaQuest.Core.Settings.Instance.Resolution;
            Platform_GUI.MainWindowWidthInt = (int)EmodiaQuest.Core.Settings.Instance.Resolution.X;
            Platform_GUI.MainWindowHeightInt = (int)EmodiaQuest.Core.Settings.Instance.Resolution.Y;
        }

        public void setFontSprites(ContentManager Content)
        {
            // Add new fonts here
            Platform_GUI.fonts.Add(new SpriteFonts_GUI("diceFont_big"));
            Platform_GUI.fonts.Add(new SpriteFonts_GUI("monoFont_big"));
            Platform_GUI.fonts.Add(new SpriteFonts_GUI("monoFont_small"));

            // Watch for add-methods in Platform_Gui
            

            // Don't touch this.
            foreach(SpriteFonts_GUI sf in Platform_GUI.fonts)
            {
                sf.SFont = Content.Load<SpriteFont>("Content_GUI/" + sf.FontName);
                sf.fontHeight = sf.SFont.MeasureString("A").Y;
                //monoFont_big.MeasureString("12345")
            }
        }

        public void setFontSize()
        {
            // Scale is: current windowheight divided by standard-size
            Platform_GUI.OverallFontScale = Platform_GUI.MainWindowHeightInt / 480;
            //this.scaleFactor_dice_big = fontFactor_dice_big / MainWindowSize.Y;
            //this.scaleFactor_monoFont_big = scaleFactor_monoFont_big / MainWindowSize.Y;
            //this.scaleFactor_monoFont_small = scaleFactor_monoFont_small / MainWindowSize.Y;
            //overallFontScale = currentY/480 (unsere Ausgangsgroesse)
            //fontFactor_dice_big
            //fontSize_dice_big = dice_big.MeasureString("A");
            //fontSize_monoFont_big = monoFont_big.MeasureString("A");
            //fontSize_monoFont_small = monoFont_small.MeasureString("A");
        }
    }
}
