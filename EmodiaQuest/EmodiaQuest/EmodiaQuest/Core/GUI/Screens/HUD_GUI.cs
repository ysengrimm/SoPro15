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
                    //float hpFactor = Settings.Instance.MaxPlayerHealth / 10;
                    float hpFactor = Player.Instance.MaxHp / 10;
                    int intHP = (int)((e.ChangeValue + hpFactor * 0.5f) / hpFactor);
                    if (intHP > 10 || intHP < 0)
                    {
                        intHP = 10;
                        Console.WriteLine("You're either dead or the MaxHp in the player is not updated correctly. Maybe in the moment when you used new items");
                    }
                    //int intHP = (int)(e.ChangeValue) / 10;
                    //this.platform.updatePlainImage("lifetest", 3, 86.5f - intHP * 1.8f+1.8f, 3, intHP * 1.8f);
                    platform.updatePlainImagePicture("healthBar", "Content_GUI/Player2D/health/healthkugel"+intHP);
                    break;
                case "focus":
                    //float hpFactor = Settings.Instance.MaxPlayerHealth / 10;
                    float focusFactor = Player.Instance.MaxFocus / 10;
                    int intFocus = (int)((e.ChangeValue + focusFactor * 0.5f) / focusFactor);
                    if (intFocus > 10 || intFocus < 0)
                    {
                        intFocus = 10;
                        Console.WriteLine("MaxFocus in the player is not updated correctly. Maybe in the moment when you used new items");
                    }
                    //int intHP = (int)(e.ChangeValue) / 10;
                    //this.platform.updatePlainImage("lifetest", 3, 86.5f - intHP * 1.8f+1.8f, 3, intHP * 1.8f);
                    platform.updatePlainImagePicture("concentrationBar", "Content_GUI/Player2D/mana/manakugel" + intFocus);
                    break;
                case "xp":
                    float scaledXp = ((float)Player.Instance.Experience / Player.Instance.XPToNextLevel) * 100f;
                    platform.updatePlainImage("xpBar", 0, 99, scaledXp, 2);

                    platform.updateLabel("xp_text", Player.Instance.Experience + "/" + Player.Instance.XPToNextLevel);
                    break;
                case "xp_next_lvl":
                    float scaledXp2 = ((float)Player.Instance.Experience / Player.Instance.XPToNextLevel) * 100f;
                    platform.updatePlainImage("xpBar", 0, 99, scaledXp2, 2);

                    platform.updateLabel("xp_text", Player.Instance.Experience + "/" + Player.Instance.XPToNextLevel);
                    break;
                case "level":
                    platform.updateLabel("lvl", e.ChangeValue.ToString());
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

            // Content for the healthbar
            platform.addPlainImage(0, 100 - 100 * 0.189f * 1.777f + 1, 100, 100 * 0.189f * 1.777f, "healthBar", "pixel_red");
            platform.updatePlainImagePicture("healthBar", "Content_GUI/Player2D/health/healthkugel10");

            // Content for the concentrationBar
            platform.addPlainImage(0, 100 - 100 * 0.189f * 1.777f + 1, 100, 100 * 0.189f * 1.777f, "concentrationBar", "pixel_red");
            platform.updatePlainImagePicture("concentrationBar", "Content_GUI/Player2D/mana/manakugel10");

            this.platform.addPlainImage(0, 100 - 100 * 0.189f * 1.777f + 1, 100, 100 * 0.189f * 1.777f, "HUD", "HUD_small");
            
            
            //this.platform.addPlainImage(3, 68.5f, 3, 18, "lifetest", "pixel_red");

            

            // XP Number
            platform.addLabel(93, 97, 3, "monoFont_big", Player.Instance.Experience + "/" + Player.Instance.XPToNextLevel, "xp_text", true);

            // XP Bar
            platform.addPlainImage(0, 99, 0, 2, "xpBar", "pixel_red");

            // Level Number
            platform.addLabel(99, 97, 3, "monoFont_big", Player.Instance.Level.ToString(), "lvl", true);

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
