using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmodiaQuest.Core.Items;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace EmodiaQuest.Core.GUI.Screens
{
    public class Inventory_GUI
    {
        private int sliderPos;
        //EventHandler
        void SliderEventValue(object source, SliderEvent_GUI e)
        {
            switch (e.Function)
            {
                case "changeinventory":
                    switch (e.SliderValue)
                    {
                        case 0:
                            platform.updateLabel("changeside", "Aktiver Quest");
                            
                            break;
                        case 1:
                            this.platform.updateLabel("changeside", "Aufgaben");
                            
                            break;
                        case 2:
                            this.platform.updateLabel("changeside", "Questteil");
                            platform.updateLabel("active_quest", "");
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("Function name does not exist");
                    break;
            }
        }

        //EventHandler
        void ChangeValueEventValue(object source, ChangeValueEvent e)
        {
            switch (e.Function)
            {
                case "hp":
                    break;
                case "xp":
                    float scaledXp = (e.ChangeValue / Player.Instance.XPToNextLevel) * 100;

                    platform.updatePlainImage("xpBar", 0, 99, scaledXp, 2);
                    break;
                case "xp_next_lvl":
                    platform.updateLabel("xp_text", Player.Instance.Experience + "/" + Player.Instance.XPToNextLevel);
                    break;
                case "level":
                    platform.updateLabel("lvl", e.ChangeValue.ToString());
                    break;
                case "gold":
                    platform.updateLabel("gold", e.ChangeValue.ToString());
                    break;
                case "armor":
                    platform.updateLabel("armor", "Ruestung: " + e.ChangeValue.ToString());
                    break;
                case "strength":
                    platform.updateLabel("str", "Kraft: " + e.ChangeValue.ToString());
                    break;
                case "skill":
                    platform.updateLabel("skill", "Fertigkeit: " + e.ChangeValue.ToString());
                    break;
                case "intelligence":
                    platform.updateLabel("intel", "Intelligenz: " + e.ChangeValue.ToString());
                    break;
                case "dmg":
                    platform.updateLabel("damage", "Schaden: " + Player.Instance.MinDamage + " - " + Player.Instance.MaxDamage);
                    break;
                case "quest":
                    platform.updateLabel("active_quest", Player.Instance.ActiveQuest.Name != "" ? Player.Instance.ActiveQuest.Name : "Kein aktiver Quest");
                    platform.updateLabel("active_quest_desc", Player.Instance.ActiveQuest.Description != "" ? Player.Instance.ActiveQuest.Description : "");
                    break;
            }
        }

        private Item currentSelectedItem;

        void ButtonEventValue(object source, ButtonEvent_GUI e)
        {
            switch (e.ButtonFunction)
            {
                case "itemSlot1":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 1 && Player.Instance.PlayerInventory[0] != null ? "Name: " + Player.Instance.PlayerInventory[0].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 1 && Player.Instance.PlayerInventory[0] != null ? "Level: " + Player.Instance.PlayerInventory[0].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[0].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 1 && Player.Instance.PlayerInventory[0] != null ? "Ruestung: " + Player.Instance.PlayerInventory[0].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 1 && Player.Instance.PlayerInventory[0] != null ? "Schaden: " + Player.Instance.PlayerInventory[0].MinDamage + " - " + Player.Instance.PlayerInventory[0].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 1 && Player.Instance.PlayerInventory[0] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[0].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 1 && Player.Instance.PlayerInventory[0] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[0].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 1 && Player.Instance.PlayerInventory[0] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[0].IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.PlayerInventory.Count >= 1 && Player.Instance.PlayerInventory[0] != null ? Player.Instance.PlayerInventory[0] : null;

                    if (currentSelectedItem != null)
                    {
                        platform.updateButtonVisibility("equip_item", true);
                        platform.updateButtonClickability("equip_item", true);    
                    }
                    
                    platform.updateButtonVisibility("unequip_item", false);
                    platform.updateButtonClickability("unequip_item", false);
                    break;
                case "itemSlot2":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 2 && Player.Instance.PlayerInventory[1] != null ? "Name: " + Player.Instance.PlayerInventory[1].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 2 && Player.Instance.PlayerInventory[1] != null ? "Level: " + Player.Instance.PlayerInventory[1].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[1].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 2 && Player.Instance.PlayerInventory[1] != null ? "Ruestung: " + Player.Instance.PlayerInventory[1].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 2 && Player.Instance.PlayerInventory[1] != null ? "Schaden: " + Player.Instance.PlayerInventory[1].MinDamage + " - " + Player.Instance.PlayerInventory[1].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 2 && Player.Instance.PlayerInventory[1] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[1].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 2 && Player.Instance.PlayerInventory[1] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[1].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 2 && Player.Instance.PlayerInventory[1] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[1].IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.PlayerInventory.Count >= 2 && Player.Instance.PlayerInventory[1] != null ? Player.Instance.PlayerInventory[1] : null;

                    if (currentSelectedItem != null)
                    {
                        platform.updateButtonVisibility("equip_item", true);
                        platform.updateButtonClickability("equip_item", true);
                    }

                    platform.updateButtonVisibility("unequip_item", false);
                    platform.updateButtonClickability("unequip_item", false);
                    break;
                case "itemSlot3":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 3 && Player.Instance.PlayerInventory[2] != null ? "Name: " + Player.Instance.PlayerInventory[2].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 3 && Player.Instance.PlayerInventory[2] != null ? "Level: " + Player.Instance.PlayerInventory[2].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[2].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 3 && Player.Instance.PlayerInventory[2] != null ? "Ruestung: " + Player.Instance.PlayerInventory[2].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 3 && Player.Instance.PlayerInventory[2] != null ? "Schaden: " + Player.Instance.PlayerInventory[2].MinDamage + " - " + Player.Instance.PlayerInventory[2].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 3 && Player.Instance.PlayerInventory[2] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[2].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 3 && Player.Instance.PlayerInventory[2] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[2].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 3 && Player.Instance.PlayerInventory[2] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[2].IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.PlayerInventory.Count >= 3 && Player.Instance.PlayerInventory[2] != null ? Player.Instance.PlayerInventory[2] : null;

                    if (currentSelectedItem != null)
                    {
                        platform.updateButtonVisibility("equip_item", true);
                        platform.updateButtonClickability("equip_item", true);
                    }

                    platform.updateButtonVisibility("unequip_item", false);
                    platform.updateButtonClickability("unequip_item", false);
                    break;
                case "itemSlot4":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 4 && Player.Instance.PlayerInventory[3] != null ? "Name: " + Player.Instance.PlayerInventory[3].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 4 && Player.Instance.PlayerInventory[3] != null ? "Level: " + Player.Instance.PlayerInventory[3].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[3].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 4 && Player.Instance.PlayerInventory[3] != null ? "Ruestung: " + Player.Instance.PlayerInventory[3].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 4 && Player.Instance.PlayerInventory[3] != null ? "Schaden: " + Player.Instance.PlayerInventory[3].MinDamage + " - " + Player.Instance.PlayerInventory[3].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 4 && Player.Instance.PlayerInventory[3] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[3].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 4 && Player.Instance.PlayerInventory[3] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[3].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 4 && Player.Instance.PlayerInventory[3] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[3].IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.PlayerInventory.Count >= 4 && Player.Instance.PlayerInventory[3] != null ? Player.Instance.PlayerInventory[3] : null;

                    if (currentSelectedItem != null)
                    {
                        platform.updateButtonVisibility("equip_item", true);
                        platform.updateButtonClickability("equip_item", true);
                    }

                    platform.updateButtonVisibility("unequip_item", false);
                    platform.updateButtonClickability("unequip_item", false);
                    break;
                case "itemSlot5":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 5 && Player.Instance.PlayerInventory[4] != null ? "Name: " + Player.Instance.PlayerInventory[4].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 5 && Player.Instance.PlayerInventory[4] != null ? "Level: " + Player.Instance.PlayerInventory[4].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[4].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 5 && Player.Instance.PlayerInventory[4] != null ? "Ruestung: " + Player.Instance.PlayerInventory[4].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 5 && Player.Instance.PlayerInventory[4] != null ? "Schaden: " + Player.Instance.PlayerInventory[4].MinDamage + " - " + Player.Instance.PlayerInventory[4].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 5 && Player.Instance.PlayerInventory[4] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[4].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 5 && Player.Instance.PlayerInventory[4] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[4].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 5 && Player.Instance.PlayerInventory[4] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[4].IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.PlayerInventory.Count >= 5 && Player.Instance.PlayerInventory[4] != null ? Player.Instance.PlayerInventory[4] : null;

                    if (currentSelectedItem != null)
                    {
                        platform.updateButtonVisibility("equip_item", true);
                        platform.updateButtonClickability("equip_item", true);
                    }

                    platform.updateButtonVisibility("unequip_item", false);
                    platform.updateButtonClickability("unequip_item", false);
                    break;
                case "itemSlot6":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 6 && Player.Instance.PlayerInventory[5] != null ? "Name: " + Player.Instance.PlayerInventory[5].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 6 && Player.Instance.PlayerInventory[5] != null ? "Level: " + Player.Instance.PlayerInventory[5].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[5].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 6 && Player.Instance.PlayerInventory[5] != null ? "Ruestung: " + Player.Instance.PlayerInventory[5].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 6 && Player.Instance.PlayerInventory[5] != null ? "Schaden: " + Player.Instance.PlayerInventory[5].MinDamage + " - " + Player.Instance.PlayerInventory[5].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 6 && Player.Instance.PlayerInventory[5] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[5].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 6 && Player.Instance.PlayerInventory[5] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[5].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 6 && Player.Instance.PlayerInventory[5] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[5].IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.PlayerInventory.Count >= 6 && Player.Instance.PlayerInventory[5] != null ? Player.Instance.PlayerInventory[5] : null;

                    if (currentSelectedItem != null)
                    {
                        platform.updateButtonVisibility("equip_item", true);
                        platform.updateButtonClickability("equip_item", true);
                    }

                    platform.updateButtonVisibility("unequip_item", false);
                    platform.updateButtonClickability("unequip_item", false);
                    break;
                case "itemSlot7":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 7 && Player.Instance.PlayerInventory[6] != null ? "Name: " + Player.Instance.PlayerInventory[6].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 7 && Player.Instance.PlayerInventory[6] != null ? "Level: " + Player.Instance.PlayerInventory[6].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[6].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 7 && Player.Instance.PlayerInventory[6] != null ? "Ruestung: " + Player.Instance.PlayerInventory[6].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 7 && Player.Instance.PlayerInventory[6] != null ? "Schaden: " + Player.Instance.PlayerInventory[6].MinDamage + " - " + Player.Instance.PlayerInventory[6].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 7 && Player.Instance.PlayerInventory[6] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[6].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 7 && Player.Instance.PlayerInventory[6] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[6].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 7 && Player.Instance.PlayerInventory[6] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[6].IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.PlayerInventory.Count >= 7 && Player.Instance.PlayerInventory[6] != null ? Player.Instance.PlayerInventory[6] : null;

                    if (currentSelectedItem != null)
                    {
                        platform.updateButtonVisibility("equip_item", true);
                        platform.updateButtonClickability("equip_item", true);
                    }

                    platform.updateButtonVisibility("unequip_item", false);
                    platform.updateButtonClickability("unequip_item", false);
                    break;
                case "itemSlot8":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 8 && Player.Instance.PlayerInventory[7] != null ? "Name: " + Player.Instance.PlayerInventory[7].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 8 && Player.Instance.PlayerInventory[7] != null ? "Level: " + Player.Instance.PlayerInventory[7].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[7].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 8 && Player.Instance.PlayerInventory[7] != null ? "Ruestung: " + Player.Instance.PlayerInventory[7].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 8 && Player.Instance.PlayerInventory[7] != null ? "Schaden: " + Player.Instance.PlayerInventory[7].MinDamage + " - " + Player.Instance.PlayerInventory[7].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 8 && Player.Instance.PlayerInventory[7] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[7].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 8 && Player.Instance.PlayerInventory[7] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[7].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 8 && Player.Instance.PlayerInventory[7] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[7].IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.PlayerInventory.Count >= 8 && Player.Instance.PlayerInventory[7] != null ? Player.Instance.PlayerInventory[7] : null;

                    if (currentSelectedItem != null)
                    {
                        platform.updateButtonVisibility("equip_item", true);
                        platform.updateButtonClickability("equip_item", true);
                    }

                    platform.updateButtonVisibility("unequip_item", false);
                    platform.updateButtonClickability("unequip_item", false);
                    break;
                case "itemSlot9":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 9 && Player.Instance.PlayerInventory[8] != null ? "Name: " + Player.Instance.PlayerInventory[8].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 9 && Player.Instance.PlayerInventory[8] != null ? "Level: " + Player.Instance.PlayerInventory[8].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[8].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 9 && Player.Instance.PlayerInventory[8] != null ? "Ruestung: " + Player.Instance.PlayerInventory[8].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 9 && Player.Instance.PlayerInventory[8] != null ? "Schaden: " + Player.Instance.PlayerInventory[8].MinDamage + " - " + Player.Instance.PlayerInventory[8].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 9 && Player.Instance.PlayerInventory[8] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[8].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 9 && Player.Instance.PlayerInventory[8] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[8].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 9 && Player.Instance.PlayerInventory[8] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[8].IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.PlayerInventory.Count >= 9 && Player.Instance.PlayerInventory[8] != null ? Player.Instance.PlayerInventory[8] : null;

                    if (currentSelectedItem != null)
                    {
                        platform.updateButtonVisibility("equip_item", true);
                        platform.updateButtonClickability("equip_item", true);
                    }

                    platform.updateButtonVisibility("unequip_item", false);
                    platform.updateButtonClickability("unequip_item", false);
                    break;
                case "itemSlot10":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 10 && Player.Instance.PlayerInventory[9] != null ? "Name: " + Player.Instance.PlayerInventory[9].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 10 && Player.Instance.PlayerInventory[9] != null ? "Level: " + Player.Instance.PlayerInventory[9].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[9].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 10 && Player.Instance.PlayerInventory[9] != null ? "Ruestung: " + Player.Instance.PlayerInventory[9].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 10 && Player.Instance.PlayerInventory[9] != null ? "Schaden: " + Player.Instance.PlayerInventory[9].MinDamage + " - " + Player.Instance.PlayerInventory[9].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 10 && Player.Instance.PlayerInventory[9] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[9].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 10 && Player.Instance.PlayerInventory[9] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[9].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 10 && Player.Instance.PlayerInventory[9] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[9].IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.PlayerInventory.Count >= 10 && Player.Instance.PlayerInventory[9] != null ? Player.Instance.PlayerInventory[9] : null;

                    if (currentSelectedItem != null)
                    {
                        platform.updateButtonVisibility("equip_item", true);
                        platform.updateButtonClickability("equip_item", true);
                    }

                    platform.updateButtonVisibility("unequip_item", false);
                    platform.updateButtonClickability("unequip_item", false);
                    break;
                case "itemSlot11":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 11 && Player.Instance.PlayerInventory[10] != null ? "Name: " + Player.Instance.PlayerInventory[10].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 11 && Player.Instance.PlayerInventory[10] != null ? "Level: " + Player.Instance.PlayerInventory[10].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[10].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 11 && Player.Instance.PlayerInventory[10] != null ? "Ruestung: " + Player.Instance.PlayerInventory[10].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 11 && Player.Instance.PlayerInventory[10] != null ? "Schaden: " + Player.Instance.PlayerInventory[10].MinDamage + " - " + Player.Instance.PlayerInventory[10].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 11 && Player.Instance.PlayerInventory[10] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[10].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 11 && Player.Instance.PlayerInventory[10] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[10].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 11 && Player.Instance.PlayerInventory[10] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[10].IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.PlayerInventory.Count >= 11 && Player.Instance.PlayerInventory[10] != null ? Player.Instance.PlayerInventory[10] : null;

                    if (currentSelectedItem != null)
                    {
                        platform.updateButtonVisibility("equip_item", true);
                        platform.updateButtonClickability("equip_item", true);
                    }

                    platform.updateButtonVisibility("unequip_item", false);
                    platform.updateButtonClickability("unequip_item", false);
                    break;
                case "itemSlot12":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 12 && Player.Instance.PlayerInventory[11] != null ? "Name: " + Player.Instance.PlayerInventory[11].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 12 && Player.Instance.PlayerInventory[11] != null ? "Level: " + Player.Instance.PlayerInventory[11].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[11].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 12 && Player.Instance.PlayerInventory[11] != null ? "Ruestung: " + Player.Instance.PlayerInventory[11].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 12 && Player.Instance.PlayerInventory[11] != null ? "Schaden: " + Player.Instance.PlayerInventory[11].MinDamage + " - " + Player.Instance.PlayerInventory[11].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 12 && Player.Instance.PlayerInventory[11] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[11].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 12 && Player.Instance.PlayerInventory[11] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[11].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 12 && Player.Instance.PlayerInventory[11] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[11].IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.PlayerInventory.Count >= 12 && Player.Instance.PlayerInventory[11] != null ? Player.Instance.PlayerInventory[11] : null;

                    if (currentSelectedItem != null)
                    {
                        platform.updateButtonVisibility("equip_item", true);
                        platform.updateButtonClickability("equip_item", true);
                    }

                    platform.updateButtonVisibility("unequip_item", false);
                    platform.updateButtonClickability("unequip_item", false);
                    break;
                case "itemSlot13":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 13 && Player.Instance.PlayerInventory[12] != null ? "Name: " + Player.Instance.PlayerInventory[12].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 13 && Player.Instance.PlayerInventory[12] != null ? "Level: " + Player.Instance.PlayerInventory[12].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[12].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 13 && Player.Instance.PlayerInventory[12] != null ? "Ruestung: " + Player.Instance.PlayerInventory[12].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 13 && Player.Instance.PlayerInventory[12] != null ? "Schaden: " + Player.Instance.PlayerInventory[12].MinDamage + " - " + Player.Instance.PlayerInventory[12].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 13 && Player.Instance.PlayerInventory[12] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[12].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 13 && Player.Instance.PlayerInventory[12] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[12].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 13 && Player.Instance.PlayerInventory[12] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[12].IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.PlayerInventory.Count >= 13 && Player.Instance.PlayerInventory[12] != null ? Player.Instance.PlayerInventory[12] : null;

                    if (currentSelectedItem != null)
                    {
                        platform.updateButtonVisibility("equip_item", true);
                        platform.updateButtonClickability("equip_item", true);
                    }

                    platform.updateButtonVisibility("unequip_item", false);
                    platform.updateButtonClickability("unequip_item", false);
                    break;
                case "itemSlot14":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 14 && Player.Instance.PlayerInventory[13] != null ? "Name: " + Player.Instance.PlayerInventory[13].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 14 && Player.Instance.PlayerInventory[13] != null ? "Level: " + Player.Instance.PlayerInventory[13].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[13].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 14 && Player.Instance.PlayerInventory[13] != null ? "Ruestung: " + Player.Instance.PlayerInventory[13].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 14 && Player.Instance.PlayerInventory[13] != null ? "Schaden: " + Player.Instance.PlayerInventory[13].MinDamage + " - " + Player.Instance.PlayerInventory[13].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 14 && Player.Instance.PlayerInventory[13] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[13].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 14 && Player.Instance.PlayerInventory[13] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[13].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 14 && Player.Instance.PlayerInventory[13] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[13].IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.PlayerInventory.Count >= 14 && Player.Instance.PlayerInventory[13] != null ? Player.Instance.PlayerInventory[13] : null;

                    if (currentSelectedItem != null)
                    {
                        platform.updateButtonVisibility("equip_item", true);
                        platform.updateButtonClickability("equip_item", true);
                    }

                    platform.updateButtonVisibility("unequip_item", false);
                    platform.updateButtonClickability("unequip_item", false);
                    break;
                case "itemSlot15":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 15 && Player.Instance.PlayerInventory[14] != null ? "Name: " + Player.Instance.PlayerInventory[14].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 15 && Player.Instance.PlayerInventory[14] != null ? "Level: " + Player.Instance.PlayerInventory[14].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[14].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 15 && Player.Instance.PlayerInventory[14] != null ? "Ruestung: " + Player.Instance.PlayerInventory[14].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 15 && Player.Instance.PlayerInventory[14] != null ? "Schaden: " + Player.Instance.PlayerInventory[14].MinDamage + " - " + Player.Instance.PlayerInventory[14].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 15 && Player.Instance.PlayerInventory[14] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[14].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 15 && Player.Instance.PlayerInventory[14] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[14].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 15 && Player.Instance.PlayerInventory[14] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[14].IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.PlayerInventory.Count >= 15 && Player.Instance.PlayerInventory[14] != null ? Player.Instance.PlayerInventory[14] : null;

                    if (currentSelectedItem != null)
                    {
                        platform.updateButtonVisibility("equip_item", true);
                        platform.updateButtonClickability("equip_item", true);
                    }

                    platform.updateButtonVisibility("unequip_item", false);
                    platform.updateButtonClickability("unequip_item", false);
                    break;
                case "itemSlot16":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 16 && Player.Instance.PlayerInventory[15] != null ? "Name: " + Player.Instance.PlayerInventory[15].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 16 && Player.Instance.PlayerInventory[15] != null ? "Level: " + Player.Instance.PlayerInventory[15].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[15].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 16 && Player.Instance.PlayerInventory[15] != null ? "Ruestung: " + Player.Instance.PlayerInventory[15].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 16 && Player.Instance.PlayerInventory[15] != null ? "Schaden: " + Player.Instance.PlayerInventory[15].MinDamage + " - " + Player.Instance.PlayerInventory[15].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 16 && Player.Instance.PlayerInventory[15] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[15].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 16 && Player.Instance.PlayerInventory[15] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[15].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 16 && Player.Instance.PlayerInventory[15] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[15].IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.PlayerInventory.Count >= 16 && Player.Instance.PlayerInventory[15] != null ? Player.Instance.PlayerInventory[15] : null;

                    if (currentSelectedItem != null)
                    {
                        platform.updateButtonVisibility("equip_item", true);
                        platform.updateButtonClickability("equip_item", true);
                    }

                    platform.updateButtonVisibility("unequip_item", false);
                    platform.updateButtonClickability("unequip_item", false);
                    break;
                case "itemSlot17":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 17 && Player.Instance.PlayerInventory[16] != null ? "Name: " + Player.Instance.PlayerInventory[16].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 17 && Player.Instance.PlayerInventory[16] != null ? "Level: " + Player.Instance.PlayerInventory[16].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[16].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 17 && Player.Instance.PlayerInventory[16] != null ? "Ruestung: " + Player.Instance.PlayerInventory[16].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 17 && Player.Instance.PlayerInventory[16] != null ? "Schaden: " + Player.Instance.PlayerInventory[16].MinDamage + " - " + Player.Instance.PlayerInventory[16].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 17 && Player.Instance.PlayerInventory[16] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[16].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 17 && Player.Instance.PlayerInventory[16] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[16].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 17 && Player.Instance.PlayerInventory[16] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[16].IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.PlayerInventory.Count >= 17 && Player.Instance.PlayerInventory[16] != null ? Player.Instance.PlayerInventory[16] : null;

                    if (currentSelectedItem != null)
                    {
                        platform.updateButtonVisibility("equip_item", true);
                        platform.updateButtonClickability("equip_item", true);
                    }

                    platform.updateButtonVisibility("unequip_item", false);
                    platform.updateButtonClickability("unequip_item", false);
                    break;
                case "itemSlot18":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 18 && Player.Instance.PlayerInventory[17] != null ? "Name: " + Player.Instance.PlayerInventory[17].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 18 && Player.Instance.PlayerInventory[17] != null ? "Level: " + Player.Instance.PlayerInventory[17].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[17].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 18 && Player.Instance.PlayerInventory[17] != null ? "Ruestung: " + Player.Instance.PlayerInventory[17].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 18 && Player.Instance.PlayerInventory[17] != null ? "Schaden: " + Player.Instance.PlayerInventory[17].MinDamage + " - " + Player.Instance.PlayerInventory[17].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 18 && Player.Instance.PlayerInventory[17] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[17].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 18 && Player.Instance.PlayerInventory[17] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[17].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 18 && Player.Instance.PlayerInventory[17] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[17].IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.PlayerInventory.Count >= 18 && Player.Instance.PlayerInventory[17] != null ? Player.Instance.PlayerInventory[17] : null;

                    if (currentSelectedItem != null)
                    {
                        platform.updateButtonVisibility("equip_item", true);
                        platform.updateButtonClickability("equip_item", true);
                    }

                    platform.updateButtonVisibility("unequip_item", false);
                    platform.updateButtonClickability("unequip_item", false);
                    break;
                case "equip_item":
                    if (Player.Instance.Strength >= currentSelectedItem.RequiredStrength)
                    {
                        switch (currentSelectedItem.Class)
                        {
                            case ItemClass.Weapon:
                                if (Player.Instance.CurrentEquippedWeapon != null)
                                {
                                    Player.Instance.PlayerInventory.Add(Player.Instance.CurrentEquippedWeapon);
                                    Player.Instance.CurrentEquippedWeapon = currentSelectedItem;
                                }
                                platform.updateButtonPicture("equipped_weapon", "weapon");
                                break;
                            case ItemClass.Armor:
                                if (Player.Instance.CurrentEquippedArmor != null)
                                {
                                    Player.Instance.PlayerInventory.Add(Player.Instance.CurrentEquippedArmor);
                                    Player.Instance.CurrentEquippedArmor = currentSelectedItem;
                                }
                                platform.updateButtonPicture("equipped_armor", "armor");
                                break;
                            case ItemClass.Boots:
                                if (Player.Instance.CurrentEquippedBoots != null)
                                {
                                    Player.Instance.PlayerInventory.Add(Player.Instance.CurrentEquippedBoots);
                                    Player.Instance.CurrentEquippedBoots = currentSelectedItem;
                                }
                                platform.updateButtonPicture("equipped_boots", "boot");
                                break;
                            case ItemClass.Helmet:
                                if (Player.Instance.CurrentEquippedHelmet != null)
                                {
                                    Player.Instance.PlayerInventory.Add(Player.Instance.CurrentEquippedHelmet);
                                    Player.Instance.CurrentEquippedHelmet = currentSelectedItem;
                                }
                                platform.updateButtonPicture("equipped_helmet", "helmet");
                                break;
                        }
                        Player.Instance.PlayerInventory.Remove(currentSelectedItem);
                        Player.Instance.GrandStats();
                    }
                    break;
                case "unequip_item":
                    switch (currentSelectedItem.Class)
                    {
                        case ItemClass.Weapon:
                            if (Player.Instance.CurrentEquippedWeapon != null)
                            {
                                Player.Instance.PlayerInventory.Add(Player.Instance.CurrentEquippedWeapon);
                                Player.Instance.CurrentEquippedWeapon = null;
                            }
                            platform.updateButtonPicture("equipped_weapon", "socket");
                            break;
                        case ItemClass.Armor:
                            if (Player.Instance.CurrentEquippedArmor != null)
                            {
                                Player.Instance.PlayerInventory.Add(Player.Instance.CurrentEquippedArmor);
                                Player.Instance.CurrentEquippedArmor = null;
                            }
                            platform.updateButtonPicture("equipped_armor", "socket");
                            break;
                        case ItemClass.Boots:
                            if (Player.Instance.CurrentEquippedBoots != null)
                            {
                                Player.Instance.PlayerInventory.Add(Player.Instance.CurrentEquippedBoots);
                                Player.Instance.CurrentEquippedBoots = null;
                            }
                            platform.updateButtonPicture("equipped_boots", "socket");
                            break;
                        case ItemClass.Helmet:
                            if (Player.Instance.CurrentEquippedHelmet != null)
                            {
                                Player.Instance.PlayerInventory.Add(Player.Instance.CurrentEquippedHelmet);
                                Player.Instance.CurrentEquippedHelmet = null;
                            }
                            platform.updateButtonPicture("equipped_helmet", "socket");
                            break;
                    }
                    Player.Instance.GrandStats();
                    break;
                case "equipped_helmet":
                    platform.updateLabel("item_name", Player.Instance.CurrentEquippedHelmet != null ? "Name: " + Player.Instance.CurrentEquippedHelmet.Class + ": " + Player.Instance.CurrentEquippedHelmet.Name : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.CurrentEquippedHelmet != null ? "Level: " + Player.Instance.CurrentEquippedHelmet.Lvl + " / Benoetigte Staerke: " + Player.Instance.CurrentEquippedHelmet.RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.CurrentEquippedHelmet != null ? "Ruestung: " + Player.Instance.CurrentEquippedHelmet.Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.CurrentEquippedHelmet != null ? "Schaden: " + Player.Instance.CurrentEquippedHelmet.MinDamage + " - " + Player.Instance.CurrentEquippedHelmet.MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.CurrentEquippedHelmet != null ? "Kraft Plus: " + Player.Instance.CurrentEquippedHelmet.StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.CurrentEquippedHelmet != null ? "Fertigkeit Plus: " + Player.Instance.CurrentEquippedHelmet.SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.CurrentEquippedHelmet != null ? "Intelligenz Plus: " + Player.Instance.CurrentEquippedHelmet.IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.CurrentEquippedHelmet ?? null;

                    platform.updateButtonVisibility("equip_item", false);
                    platform.updateButtonClickability("equip_item", false);

                    platform.updateButtonVisibility("unequip_item", true);
                    platform.updateButtonClickability("unequip_item", true);
                    break;
                case "equipped_armor":
                    platform.updateLabel("item_name", Player.Instance.CurrentEquippedArmor != null ? "Name: " + Player.Instance.CurrentEquippedArmor.Class + ": " + Player.Instance.CurrentEquippedArmor.Name : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.CurrentEquippedArmor != null ? "Level: " + Player.Instance.CurrentEquippedArmor.Lvl + " / Benoetigte Staerke: " + Player.Instance.CurrentEquippedArmor.RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.CurrentEquippedArmor != null ? "Ruestung: " + Player.Instance.CurrentEquippedArmor.Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.CurrentEquippedArmor != null ? "Schaden: " + Player.Instance.CurrentEquippedArmor.MinDamage + " - " + Player.Instance.CurrentEquippedArmor.MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.CurrentEquippedArmor != null ? "Kraft Plus: " + Player.Instance.CurrentEquippedArmor.StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.CurrentEquippedArmor != null ? "Fertigkeit Plus: " + Player.Instance.CurrentEquippedArmor.SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.CurrentEquippedArmor != null ? "Intelligenz Plus: " + Player.Instance.CurrentEquippedArmor.IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.CurrentEquippedArmor ?? null;

                    platform.updateButtonVisibility("equip_item", false);
                    platform.updateButtonClickability("equip_item", false);

                    platform.updateButtonVisibility("unequip_item", true);
                    platform.updateButtonClickability("unequip_item", true);
                    break;
                case "equipped_boots":
                    platform.updateLabel("item_name", Player.Instance.CurrentEquippedBoots != null ? "Name: " + Player.Instance.CurrentEquippedBoots.Class + ": " + Player.Instance.CurrentEquippedBoots.Name: "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.CurrentEquippedBoots != null ? "Level: " + Player.Instance.CurrentEquippedBoots.Lvl + " / Benoetigte Staerke: " + Player.Instance.CurrentEquippedBoots.RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.CurrentEquippedBoots != null ? "Ruestung: " + Player.Instance.CurrentEquippedBoots.Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.CurrentEquippedBoots != null ? "Schaden: " + Player.Instance.CurrentEquippedBoots.MinDamage + " - " + Player.Instance.CurrentEquippedBoots.MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.CurrentEquippedBoots != null ? "Kraft Plus: " + Player.Instance.CurrentEquippedBoots.StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.CurrentEquippedBoots != null ? "Fertigkeit Plus: " + Player.Instance.CurrentEquippedBoots.SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.CurrentEquippedBoots != null ? "Intelligenz Plus: " + Player.Instance.CurrentEquippedBoots.IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.CurrentEquippedBoots ?? null;

                    platform.updateButtonVisibility("equip_item", false);
                    platform.updateButtonClickability("equip_item", false);

                    platform.updateButtonVisibility("unequip_item", true);
                    platform.updateButtonClickability("unequip_item", true);
                    break;
                case "equipped_weapon":
                    platform.updateLabel("item_name", Player.Instance.CurrentEquippedWeapon != null ? "Name: " + Player.Instance.CurrentEquippedWeapon.Class + ": " + Player.Instance.CurrentEquippedWeapon.Name: "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.CurrentEquippedWeapon != null ? "Level: " + Player.Instance.CurrentEquippedWeapon.Lvl + " / Benoetigte Staerke: " + Player.Instance.CurrentEquippedWeapon.RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.CurrentEquippedWeapon != null ? "Ruestung: " + Player.Instance.CurrentEquippedWeapon.Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.CurrentEquippedWeapon != null ? "Schaden: " + Player.Instance.CurrentEquippedWeapon.MinDamage + " - " + Player.Instance.CurrentEquippedWeapon.MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.CurrentEquippedWeapon != null ? "Kraft Plus: " + Player.Instance.CurrentEquippedWeapon.StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.CurrentEquippedWeapon != null ? "Fertigkeit Plus: " + Player.Instance.CurrentEquippedWeapon.SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.CurrentEquippedWeapon != null ? "Intelligenz Plus: " + Player.Instance.CurrentEquippedWeapon.IntelligencePlus : "");
                    currentSelectedItem = Player.Instance.CurrentEquippedWeapon ?? null;

                    platform.updateButtonVisibility("equip_item", false);
                    platform.updateButtonClickability("equip_item", false);

                    platform.updateButtonVisibility("unequip_item", true);
                    platform.updateButtonClickability("unequip_item", true);
                    break;
            }
        }

        private static Inventory_GUI instance;

        private Inventory_GUI() { }

        public static Inventory_GUI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Inventory_GUI();
                }
                return instance;
            }
        }

        private Platform_GUI platform = new Platform_GUI();

        public void loadContent(ContentManager Content)
        {
            platform.loadContent(Content);

            this.platform.setBackground(Content, "Content_GUI/inventory_background");

            //this.platform.addSlider(5, 5, 24, 8, 0, 2, 0, "changeinventory");

            this.platform.addLabel(15, 0, 10, "monoFont_big", "Questlog", "changeside", true);
            platform.addLabel(15, 10, 5, "monoFont_big", Player.Instance.ActiveQuest.Name != "" ? Player.Instance.ActiveQuest.Name : "Kein aktiver Quest", "active_quest", true);
            platform.addLabel(15, 15, 4, "monoFont_big", Player.Instance.ActiveQuest.Description != "" ? Player.Instance.ActiveQuest.Description : "", "active_quest_desc", true);

            this.platform.addLabel(50, 0, 10, "monoFont_big", "Charakter Stats", "story", true);

            //this.platform.addLabel(50, 40, 10, "monoFont_big", "Item Stats", "stats", true);
            this.platform.addLabel(85, 0, 10, "monoFont_big", "Inventar", "inventory", true);

            // ItemSockets (Look out to only use 16:9 or it will not be a square!)
            // First row
            this.platform.addButton(69, 42, 4.5f, 8f, "itemSlot1");
            this.platform.addButton(69 + 4.7f, 42, 4.5f, 8f, "itemSlot2");
            this.platform.addButton(69 + 4.7f * 2, 42, 4.5f, 8f, "itemSlot3");
            this.platform.addButton(69 + 4.7f * 3, 42, 4.5f, 8f, "itemSlot4");
            this.platform.addButton(69 + 4.7f * 4, 42, 4.5f, 8f, "itemSlot5");
            this.platform.addButton(69 + 4.7f * 5, 42, 4.5f, 8f, "itemSlot6");

            // Second row
            this.platform.addButton(69, 42 + 8.2f, 4.5f, 8f, "itemSlot7");
            this.platform.addButton(69 + 4.7f, 42 + 8.2f, 4.5f, 8f, "itemSlot8");
            this.platform.addButton(69 + 4.7f * 2, 42 + 8.2f, 4.5f, 8f, "itemSlot9");
            this.platform.addButton(69 + 4.7f * 3, 42 + 8.2f, 4.5f, 8f, "itemSlot10");
            this.platform.addButton(69 + 4.7f * 4, 42 + 8.2f, 4.5f, 8f, "itemSlot11");
            this.platform.addButton(69 + 4.7f * 5, 42 + 8.2f, 4.5f, 8f, "itemSlot12");

            // Third row
            this.platform.addButton(69, 42 + 8.2f * 2, 4.5f, 8f, "itemSlot13");
            this.platform.addButton(69 + 4.7f, 42 + 8.2f * 2, 4.5f, 8f, "itemSlot14");
            this.platform.addButton(69 + 4.7f * 2, 42 + 8.2f * 2, 4.5f, 8f, "itemSlot15");
            this.platform.addButton(69 + 4.7f * 3, 42 + 8.2f * 2, 4.5f, 8f, "itemSlot16");
            this.platform.addButton(69 + 4.7f * 4, 42 + 8.2f * 2, 4.5f, 8f, "itemSlot17");
            this.platform.addButton(69 + 4.7f * 5, 42 + 8.2f * 2, 4.5f, 8f, "itemSlot18");
            
            // Eqippes items
            platform.addButton(83, 10, 4.5f, 8, "equipped_helmet");
            platform.addButton(83, 20, 4.5f, 8, "equipped_armor");
            platform.addButton(83, 32, 4.5f, 8, "equipped_boots");
            platform.addButton(93, 22, 4.5f, 8, "equipped_weapon");

            platform.addButton(45, 75, 12, 6, "equip_item", "Ausruesten");
            platform.updateButtonVisibility("equip_item", false);
            platform.updateButtonClickability("equip_item", false);

            platform.addButton(45, 75, 12, 6, "unequip_item", "Ablegen");
            platform.updateButtonVisibility("unequip_item", false);
            platform.updateButtonClickability("unequip_item", false);

            platform.OnButtonValue += new GUI_Delegate_Button(this.ButtonEventValue);

            // Item stats
            platform.addLabel(50, 40, 5, "monoFont_big", "", "item_name", true);
            platform.addLabel(50, 45, 5, "monoFont_big", "", "item_lvl_strength", true);
            platform.addLabel(50, 50, 5, "monoFont_big", "", "item_armor", true);
            platform.addLabel(50, 55, 5, "monoFont_big", "", "item_damage", true);
            platform.addLabel(50, 60, 5, "monoFont_big", "", "item_str", true);
            platform.addLabel(50, 65, 5, "monoFont_big", "", "item_skill", true);
            platform.addLabel(50, 70, 5, "monoFont_big", "", "item_intel", true);

            //platform.addPlainImage(0, 100 - 100 * 0.189f * 1.777f + 1, 100, 100 * 0.189f * 1.777f, "HUD", "HUD_small");

            // XP Number
            platform.addLabel(96, 97, 3, "monoFont_big", Player.Instance.Experience + "/" + Player.Instance.XPToNextLevel, "xp_text", true);

            // XP Bar
            platform.addPlainImage(0, 99, 0, 2, "xpBar", "pixel_red");

            // Level Number
            platform.addLabel(99, 97, 3, "monoFont_big", Player.Instance.Level.ToString(), "lvl", true);

            // Gold
            platform.addLabel(70, 70, 5, "monoFont_big", Player.Instance.Gold.ToString(), "gold", true);

            // Player stats
            platform.addLabel(50, 10, 5, "monoFont_big", "Ruestung: " + Player.Instance.Armor, "armor", true);
            platform.addLabel(50, 15, 5, "monoFont_big", "Schaden: " + Player.Instance.MinDamage + " - " + Player.Instance.MaxDamage, "damage", true);
            platform.addLabel(50, 20, 5, "monoFont_big", "Kraft: " + Player.Instance.Strength, "str", true);
            platform.addLabel(50, 25, 5, "monoFont_big", "Fertigkeit: " + Player.Instance.Skill, "skill", true);
            platform.addLabel(50, 30, 5, "monoFont_big", "Intelligenz: " + Player.Instance.Intelligence, "intel", true);

            Player.Instance.OnChangeValue += new Delegates_CORE.ChangeValueDelegate(ChangeValueEventValue);

            // Items
            // Hab grad nur First genommen, weil ich erst ein Item drin hab. Sonst natuerlich ElementAt
            this.platform.CharItems.Add(Items.ItemTestClass.Instance.Quests.First());
            this.platform.CharItems.Add(Items.ItemTestClass.Instance.Helmets.First());
        }

        public void update()
        {
            platform.update();

            for (int i = 0; i < Player.Instance.PlayerInventory.Count; i++)
            {
                switch (Player.Instance.PlayerInventory[i].Class)
                {
                    case ItemClass.Weapon:
                        platform.updateButtonPicture("itemSlot" + (i+1), "weapon");
                        break;
                    case ItemClass.Armor:
                        platform.updateButtonPicture("itemSlot" + (i+1), "armor");
                        break;
                    case ItemClass.Boots:
                        platform.updateButtonPicture("itemSlot" + (i+1), "boot");
                        break;
                    case ItemClass.Helmet:
                        platform.updateButtonPicture("itemSlot" + (i+1), "helmet");
                        break;
                    case ItemClass.Quest:
                        platform.updateButtonPicture("itemSlot" + (i+1), "quest");
                        break;
                    case ItemClass.Useable:
                        platform.updateButtonPicture("itemSlot" + (i+1), "useable");
                        break;
                    default:
                        platform.updateButtonPicture("itemSlot" + (i + 1), "socket");
                        break;
                }
            }

            // Get Keyboard input to change overall GameState
            if (Controls_GUI.Instance.keyClicked(Keys.I))
                EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.IngameScreen;
        }

        public void draw(SpriteBatch spritebatch)
        {
            this.platform.draw(spritebatch);
        }

        public void addCharItem(Items.Item item)
        {
            this.platform.CharItems.Add(item);
            Console.WriteLine("New Item with ID: " + item.ID + " has been added.");

            // Ein Aufruf sieht so aus: Man muss halt ein Item aus den Listen aus ItemTestclass nehmen.
            // EmodiaQuest.Core.GUI.Screens.Inventory_GUI.Instance.addCharItem(Items.ItemTestClass.Instance.Armors.First());
        }

    }
}
