using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace EmodiaQuest.Core.GUI.Screens
{
    public class HUD_GUI
    {
        //EventHandler
        void ChangeValueEventValue(object source, ChangeValueEvent e)
        {
            switch (e.Function)
            {
                case "hp":
                    int intHP = (int)(e.ChangeValue + 10) / 10;
                    this.platform.updatePlainImage("lifetest", 3, 86.5f - intHP * 1.8f+1.8f, 3, intHP * 1.8f);
                        //this.platform.addPlainImage(3, 68.5f, 3, 18, "lifetest", "pixel_red");
                    break;
                case "xp":
                    float scaledXp = (e.ChangeValue/Player.Instance.XPToNextLevel) * 100;

                    platform.updatePlainImage("xpBar", 0, 99, scaledXp, 2);
                    break;
                case "xp_next_lvl":
                    platform.updateLabel("xp_text", Player.Instance.Experience + "/" + Player.Instance.XPToNextLevel);
                    break;
                case "level":
                    platform.updateLabel("lvl", e.ChangeValue.ToString());
                    break;
                default:
                    Console.WriteLine("Function name does not exist");
                    break;
            }
        }

        private static HUD_GUI instance;

        private HUD_GUI() { }

        public static HUD_GUI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HUD_GUI();
                }
                return instance;
            }
        }

        private Platform_GUI platform = new Platform_GUI();

        public void loadContent(ContentManager Content)
        {
            this.platform.loadContent(Content);
            this.platform.backgroundOff();

            

            this.platform.addPlainImage(0, 100 - 100 * 0.189f * 1.777f + 1, 100, 100 * 0.189f * 1.777f, "HUD", "HUD_small");
            
            // Lifebar Images
            this.platform.addPlainImage(3, 68.5f, 3, 18, "lifetest", "pixel_red");

            // XP Number
            platform.addLabel(90, 65, 5, "monoFont_big", Player.Instance.Experience + "/" + Player.Instance.XPToNextLevel, "xp_text", true);

            // XP Bar
            platform.addPlainImage(0, 99, 0, 2, "xpBar", "pixel_red");

            // Level Number
            platform.addLabel(80, 65, 5, "monoFont_big", Player.Instance.Level.ToString(), "lvl", true);

            //this.platform.addPlainImage(9.7f, 68.5f, 5, 2.0f, "life10", "pixel_red");
            //this.platform.addPlainImage(8, 68.5f + 1.8f * 1, 7, 2.0f, "life9", "pixel_red");
            //this.platform.addPlainImage(7, 68.5f + 1.8f * 2, 8, 2.0f, "life8", "pixel_red");
            //this.platform.addPlainImage(6.5f, 68.5f + 1.8f * 3, 9, 2.0f, "life7", "pixel_red");
            //this.platform.addPlainImage(6.5f, 68.5f + 1.8f * 4, 5, 2.0f, "life6", "pixel_red");
            //this.platform.addPlainImage(6.5f, 68.5f + 1.8f * 5, 5, 2.0f, "life5", "pixel_red");
            //this.platform.addPlainImage(6.5f, 68.5f + 1.8f * 6, 5, 2.0f, "life4", "pixel_red");
            //this.platform.addPlainImage(6.5f, 68.5f + 1.8f * 7, 5, 2.0f, "life3", "pixel_red");
            //this.platform.addPlainImage(6.5f, 68.5f + 1.8f * 8, 5, 2.0f, "life2", "pixel_red");
            //this.platform.addPlainImage(6.5f, 68.5f + 1.8f * 9, 5, 2.0f, "life1", "pixel_red");
            //this.platform.addPlainImage(6.5f, 68.5f + 1.8f * 10, 5, 2.0f, "life0", "pixel_red");
            //this.platform.addPlainImage(6, 68.5f, 5, 18, "life18", "pixel_red");

            EmodiaQuest.Core.Player.Instance.OnChangeValue += new Delegates_CORE.ChangeValueDelegate(this.ChangeValueEventValue);
            //platform.OnButtonValue += new GUI_Delegate_Button(this.ButtonEventValue);
        }

        public void update()
        {
            this.platform.update();
        }

        public void draw(SpriteBatch spritebatch)
        {
            this.platform.draw(spritebatch);
        }
    }
}
