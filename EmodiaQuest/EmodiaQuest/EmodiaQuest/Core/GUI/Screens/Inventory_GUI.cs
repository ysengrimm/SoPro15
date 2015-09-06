﻿using System;
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
                            this.platform.updateLabel("changeside", "Questside");
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
                    break;
                case "itemSlot2":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 2 && Player.Instance.PlayerInventory[1] != null ? "Name: " + Player.Instance.PlayerInventory[1].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 2 && Player.Instance.PlayerInventory[1] != null ? "Level: " + Player.Instance.PlayerInventory[1].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[1].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 2 && Player.Instance.PlayerInventory[1] != null ? "Ruestung: " + Player.Instance.PlayerInventory[1].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 2 && Player.Instance.PlayerInventory[1] != null ? "Schaden: " + Player.Instance.PlayerInventory[1].MinDamage + " - " + Player.Instance.PlayerInventory[1].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 2 && Player.Instance.PlayerInventory[1] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[1].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 2 && Player.Instance.PlayerInventory[1] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[1].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 2 && Player.Instance.PlayerInventory[1] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[1].IntelligencePlus : "");
                    break;
                case "itemSlot3":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 3 && Player.Instance.PlayerInventory[2] != null ? "Name: " + Player.Instance.PlayerInventory[2].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 3 && Player.Instance.PlayerInventory[2] != null ? "Level: " + Player.Instance.PlayerInventory[2].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[2].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 3 && Player.Instance.PlayerInventory[2] != null ? "Ruestung: " + Player.Instance.PlayerInventory[2].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 3 && Player.Instance.PlayerInventory[2] != null ? "Schaden: " + Player.Instance.PlayerInventory[2].MinDamage + " - " + Player.Instance.PlayerInventory[2].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 3 && Player.Instance.PlayerInventory[2] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[2].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 3 && Player.Instance.PlayerInventory[2] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[2].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 3 && Player.Instance.PlayerInventory[2] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[2].IntelligencePlus : "");
                    break;
                case "itemSlot4":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 4 && Player.Instance.PlayerInventory[3] != null ? "Name: " + Player.Instance.PlayerInventory[3].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 4 && Player.Instance.PlayerInventory[3] != null ? "Level: " + Player.Instance.PlayerInventory[3].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[3].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 4 && Player.Instance.PlayerInventory[3] != null ? "Ruestung: " + Player.Instance.PlayerInventory[3].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 4 && Player.Instance.PlayerInventory[3] != null ? "Schaden: " + Player.Instance.PlayerInventory[3].MinDamage + " - " + Player.Instance.PlayerInventory[3].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 4 && Player.Instance.PlayerInventory[3] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[3].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 4 && Player.Instance.PlayerInventory[3] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[3].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 4 && Player.Instance.PlayerInventory[3] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[3].IntelligencePlus : "");
                    break;
                case "itemSlot5":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 5 && Player.Instance.PlayerInventory[4] != null ? "Name: " + Player.Instance.PlayerInventory[4].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 5 && Player.Instance.PlayerInventory[4] != null ? "Level: " + Player.Instance.PlayerInventory[4].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[4].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 5 && Player.Instance.PlayerInventory[4] != null ? "Ruestung: " + Player.Instance.PlayerInventory[4].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 5 && Player.Instance.PlayerInventory[4] != null ? "Schaden: " + Player.Instance.PlayerInventory[4].MinDamage + " - " + Player.Instance.PlayerInventory[4].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 5 && Player.Instance.PlayerInventory[4] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[4].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 5 && Player.Instance.PlayerInventory[4] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[4].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 5 && Player.Instance.PlayerInventory[4] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[4].IntelligencePlus : "");
                    break;
                case "itemSlot6":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 6 && Player.Instance.PlayerInventory[5] != null ? "Name: " + Player.Instance.PlayerInventory[5].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 6 && Player.Instance.PlayerInventory[5] != null ? "Level: " + Player.Instance.PlayerInventory[5].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[5].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 6 && Player.Instance.PlayerInventory[5] != null ? "Ruestung: " + Player.Instance.PlayerInventory[5].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 6 && Player.Instance.PlayerInventory[5] != null ? "Schaden: " + Player.Instance.PlayerInventory[5].MinDamage + " - " + Player.Instance.PlayerInventory[5].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 6 && Player.Instance.PlayerInventory[5] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[5].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 6 && Player.Instance.PlayerInventory[5] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[5].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 6 && Player.Instance.PlayerInventory[5] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[5].IntelligencePlus : "");
                    break;
                case "itemSlot7":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 7 && Player.Instance.PlayerInventory[6] != null ? "Name: " + Player.Instance.PlayerInventory[6].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 7 && Player.Instance.PlayerInventory[6] != null ? "Level: " + Player.Instance.PlayerInventory[6].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[6].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 7 && Player.Instance.PlayerInventory[6] != null ? "Ruestung: " + Player.Instance.PlayerInventory[6].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 7 && Player.Instance.PlayerInventory[6] != null ? "Schaden: " + Player.Instance.PlayerInventory[6].MinDamage + " - " + Player.Instance.PlayerInventory[6].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 7 && Player.Instance.PlayerInventory[6] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[6].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 7 && Player.Instance.PlayerInventory[6] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[6].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 7 && Player.Instance.PlayerInventory[6] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[6].IntelligencePlus : "");
                    break;
                case "itemSlot8":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 8 && Player.Instance.PlayerInventory[7] != null ? "Name: " + Player.Instance.PlayerInventory[7].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 8 && Player.Instance.PlayerInventory[7] != null ? "Level: " + Player.Instance.PlayerInventory[7].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[7].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 8 && Player.Instance.PlayerInventory[7] != null ? "Ruestung: " + Player.Instance.PlayerInventory[7].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 8 && Player.Instance.PlayerInventory[7] != null ? "Schaden: " + Player.Instance.PlayerInventory[7].MinDamage + " - " + Player.Instance.PlayerInventory[7].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 8 && Player.Instance.PlayerInventory[7] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[7].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 8 && Player.Instance.PlayerInventory[7] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[7].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 8 && Player.Instance.PlayerInventory[7] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[7].IntelligencePlus : "");
                    break;
                case "itemSlot9":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 9 && Player.Instance.PlayerInventory[8] != null ? "Name: " + Player.Instance.PlayerInventory[8].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 9 && Player.Instance.PlayerInventory[8] != null ? "Level: " + Player.Instance.PlayerInventory[8].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[8].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 9 && Player.Instance.PlayerInventory[8] != null ? "Ruestung: " + Player.Instance.PlayerInventory[8].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 9 && Player.Instance.PlayerInventory[8] != null ? "Schaden: " + Player.Instance.PlayerInventory[8].MinDamage + " - " + Player.Instance.PlayerInventory[8].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 9 && Player.Instance.PlayerInventory[8] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[8].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 9 && Player.Instance.PlayerInventory[8] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[8].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 9 && Player.Instance.PlayerInventory[8] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[8].IntelligencePlus : "");
                    break;
                case "itemSlot10":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 10 && Player.Instance.PlayerInventory[9] != null ? "Name: " + Player.Instance.PlayerInventory[9].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 10 && Player.Instance.PlayerInventory[9] != null ? "Level: " + Player.Instance.PlayerInventory[9].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[9].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 10 && Player.Instance.PlayerInventory[9] != null ? "Ruestung: " + Player.Instance.PlayerInventory[9].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 10 && Player.Instance.PlayerInventory[9] != null ? "Schaden: " + Player.Instance.PlayerInventory[9].MinDamage + " - " + Player.Instance.PlayerInventory[9].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 10 && Player.Instance.PlayerInventory[9] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[9].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 10 && Player.Instance.PlayerInventory[9] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[9].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 10 && Player.Instance.PlayerInventory[9] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[9].IntelligencePlus : "");
                    break;
                case "itemSlot11":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 11 && Player.Instance.PlayerInventory[10] != null ? "Name: " + Player.Instance.PlayerInventory[10].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 11 && Player.Instance.PlayerInventory[10] != null ? "Level: " + Player.Instance.PlayerInventory[10].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[10].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 11 && Player.Instance.PlayerInventory[10] != null ? "Ruestung: " + Player.Instance.PlayerInventory[10].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 11 && Player.Instance.PlayerInventory[10] != null ? "Schaden: " + Player.Instance.PlayerInventory[10].MinDamage + " - " + Player.Instance.PlayerInventory[10].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 11 && Player.Instance.PlayerInventory[10] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[10].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 11 && Player.Instance.PlayerInventory[10] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[10].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 11 && Player.Instance.PlayerInventory[10] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[10].IntelligencePlus : "");
                    break;
                case "itemSlot12":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 12 && Player.Instance.PlayerInventory[11] != null ? "Name: " + Player.Instance.PlayerInventory[11].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 12 && Player.Instance.PlayerInventory[11] != null ? "Level: " + Player.Instance.PlayerInventory[11].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[11].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 12 && Player.Instance.PlayerInventory[11] != null ? "Ruestung: " + Player.Instance.PlayerInventory[11].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 12 && Player.Instance.PlayerInventory[11] != null ? "Schaden: " + Player.Instance.PlayerInventory[11].MinDamage + " - " + Player.Instance.PlayerInventory[11].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 12 && Player.Instance.PlayerInventory[11] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[11].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 12 && Player.Instance.PlayerInventory[11] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[11].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 12 && Player.Instance.PlayerInventory[11] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[11].IntelligencePlus : "");
                    break;
                case "itemSlot13":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 13 && Player.Instance.PlayerInventory[12] != null ? "Name: " + Player.Instance.PlayerInventory[12].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 13 && Player.Instance.PlayerInventory[12] != null ? "Level: " + Player.Instance.PlayerInventory[12].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[12].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 13 && Player.Instance.PlayerInventory[12] != null ? "Ruestung: " + Player.Instance.PlayerInventory[12].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 13 && Player.Instance.PlayerInventory[12] != null ? "Schaden: " + Player.Instance.PlayerInventory[12].MinDamage + " - " + Player.Instance.PlayerInventory[12].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 13 && Player.Instance.PlayerInventory[12] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[12].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 13 && Player.Instance.PlayerInventory[12] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[12].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 13 && Player.Instance.PlayerInventory[12] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[12].IntelligencePlus : "");
                    break;
                case "itemSlot14":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 14 && Player.Instance.PlayerInventory[13] != null ? "Name: " + Player.Instance.PlayerInventory[13].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 14 && Player.Instance.PlayerInventory[13] != null ? "Level: " + Player.Instance.PlayerInventory[13].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[13].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 14 && Player.Instance.PlayerInventory[13] != null ? "Ruestung: " + Player.Instance.PlayerInventory[13].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 14 && Player.Instance.PlayerInventory[13] != null ? "Schaden: " + Player.Instance.PlayerInventory[13].MinDamage + " - " + Player.Instance.PlayerInventory[13].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 14 && Player.Instance.PlayerInventory[13] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[13].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 14 && Player.Instance.PlayerInventory[13] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[13].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 14 && Player.Instance.PlayerInventory[13] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[13].IntelligencePlus : "");
                    break;
                case "itemSlot15":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 15 && Player.Instance.PlayerInventory[14] != null ? "Name: " + Player.Instance.PlayerInventory[14].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 15 && Player.Instance.PlayerInventory[14] != null ? "Level: " + Player.Instance.PlayerInventory[14].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[14].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 15 && Player.Instance.PlayerInventory[14] != null ? "Ruestung: " + Player.Instance.PlayerInventory[14].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 15 && Player.Instance.PlayerInventory[14] != null ? "Schaden: " + Player.Instance.PlayerInventory[14].MinDamage + " - " + Player.Instance.PlayerInventory[14].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 15 && Player.Instance.PlayerInventory[14] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[14].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 15 && Player.Instance.PlayerInventory[14] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[14].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 15 && Player.Instance.PlayerInventory[14] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[14].IntelligencePlus : "");
                    break;
                case "itemSlot16":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 16 && Player.Instance.PlayerInventory[15] != null ? "Name: " + Player.Instance.PlayerInventory[15].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 16 && Player.Instance.PlayerInventory[15] != null ? "Level: " + Player.Instance.PlayerInventory[15].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[15].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 16 && Player.Instance.PlayerInventory[15] != null ? "Ruestung: " + Player.Instance.PlayerInventory[15].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 16 && Player.Instance.PlayerInventory[15] != null ? "Schaden: " + Player.Instance.PlayerInventory[15].MinDamage + " - " + Player.Instance.PlayerInventory[15].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 16 && Player.Instance.PlayerInventory[15] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[15].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 16 && Player.Instance.PlayerInventory[15] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[15].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 16 && Player.Instance.PlayerInventory[15] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[15].IntelligencePlus : "");
                    break;
                case "itemSlot17":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 17 && Player.Instance.PlayerInventory[16] != null ? "Name: " + Player.Instance.PlayerInventory[16].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 17 && Player.Instance.PlayerInventory[16] != null ? "Level: " + Player.Instance.PlayerInventory[16].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[16].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 17 && Player.Instance.PlayerInventory[16] != null ? "Ruestung: " + Player.Instance.PlayerInventory[16].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 17 && Player.Instance.PlayerInventory[16] != null ? "Schaden: " + Player.Instance.PlayerInventory[16].MinDamage + " - " + Player.Instance.PlayerInventory[16].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 17 && Player.Instance.PlayerInventory[16] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[16].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 17 && Player.Instance.PlayerInventory[16] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[16].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 17 && Player.Instance.PlayerInventory[16] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[16].IntelligencePlus : "");
                    break;
                case "itemSlot18":
                    platform.updateLabel("item_name", Player.Instance.PlayerInventory.Count >= 18 && Player.Instance.PlayerInventory[17] != null ? "Name: " + Player.Instance.PlayerInventory[17].Class : "");
                    platform.updateLabel("item_lvl_strength", Player.Instance.PlayerInventory.Count >= 18 && Player.Instance.PlayerInventory[17] != null ? "Level: " + Player.Instance.PlayerInventory[17].Lvl + " / Benoetigte Staerke: " + Player.Instance.PlayerInventory[17].RequiredStrength : "");
                    platform.updateLabel("item_armor", Player.Instance.PlayerInventory.Count >= 18 && Player.Instance.PlayerInventory[17] != null ? "Ruestung: " + Player.Instance.PlayerInventory[17].Armor : "");
                    platform.updateLabel("item_damage", Player.Instance.PlayerInventory.Count >= 18 && Player.Instance.PlayerInventory[17] != null ? "Schaden: " + Player.Instance.PlayerInventory[17].MinDamage + " - " + Player.Instance.PlayerInventory[17].MaxDamage : "");
                    platform.updateLabel("item_str", Player.Instance.PlayerInventory.Count >= 18 && Player.Instance.PlayerInventory[17] != null ? "Kraft Plus: " + Player.Instance.PlayerInventory[17].StrengthPlus : "");
                    platform.updateLabel("item_skill", Player.Instance.PlayerInventory.Count >= 18 && Player.Instance.PlayerInventory[17] != null ? "Fertigkeit Plus: " + Player.Instance.PlayerInventory[17].SkillPlus : "");
                    platform.updateLabel("item_intel", Player.Instance.PlayerInventory.Count >= 18 && Player.Instance.PlayerInventory[17] != null ? "Intelligenz Plus: " + Player.Instance.PlayerInventory[17].IntelligencePlus : "");
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

            this.platform.addLabel(50, 0, 10, "monoFont_big", "Character Stats", "story", true);

            //this.platform.addLabel(50, 40, 10, "monoFont_big", "Item Stats", "stats", true);
            this.platform.addLabel(85, 0, 10, "monoFont_big", "Mini-Map", "inventory", true);

            // ItemSockets (Look out to only use 16:9 or it will not be a square!)
            // First row
            this.platform.addButton(69, 42, 4.5f, 8f, "itemSlot1", true, "Content_GUI/itemSocket");
            this.platform.addButton(69 + 4.7f, 42, 4.5f, 8f, "itemSlot2", true, "Content_GUI/itemSocket");
            this.platform.addButton(69 + 4.7f * 2, 42, 4.5f, 8f, "itemSlot3", true, "Content_GUI/itemSocket");
            this.platform.addButton(69 + 4.7f * 3, 42, 4.5f, 8f, "itemSlot4", true, "Content_GUI/itemSocket");
            this.platform.addButton(69 + 4.7f * 4, 42, 4.5f, 8f, "itemSlot5", true, "Content_GUI/itemSocket");
            this.platform.addButton(69 + 4.7f * 5, 42, 4.5f, 8f, "itemSlot6", true, "Content_GUI/itemSocket");

            // Second row
            this.platform.addButton(69, 42 + 8.2f, 4.5f, 8f, "itemSlot7", true, "Content_GUI/itemSocket");
            this.platform.addButton(69 + 4.7f, 42 + 8.2f, 4.5f, 8f, "itemSlot8", true, "Content_GUI/itemSocket");
            this.platform.addButton(69 + 4.7f * 2, 42 + 8.2f, 4.5f, 8f, "itemSlot9", true, "Content_GUI/itemSocket");
            this.platform.addButton(69 + 4.7f * 3, 42 + 8.2f, 4.5f, 8f, "itemSlot10", true, "Content_GUI/itemSocket");
            this.platform.addButton(69 + 4.7f * 4, 42 + 8.2f, 4.5f, 8f, "itemSlot11", true, "Content_GUI/itemSocket");
            this.platform.addButton(69 + 4.7f * 5, 42 + 8.2f, 4.5f, 8f, "itemSlot12", true, "Content_GUI/itemSocket");

            // Third row
            this.platform.addButton(69, 42 + 8.2f * 2, 4.5f, 8f, "itemSlot13", true, "Content_GUI/itemSocket");
            this.platform.addButton(69 + 4.7f, 42 + 8.2f * 2, 4.5f, 8f, "itemSlot14", true, "Content_GUI/itemSocket");
            this.platform.addButton(69 + 4.7f * 2, 42 + 8.2f * 2, 4.5f, 8f, "itemSlot15", true, "Content_GUI/itemSocket");
            this.platform.addButton(69 + 4.7f * 3, 42 + 8.2f * 2, 4.5f, 8f, "itemSlot16", true, "Content_GUI/itemSocket");
            this.platform.addButton(69 + 4.7f * 4, 42 + 8.2f * 2, 4.5f, 8f, "itemSlot17", true, "Content_GUI/itemSocket");
            this.platform.addButton(69 + 4.7f * 5, 42 + 8.2f * 2, 4.5f, 8f, "itemSlot18", true, "Content_GUI/itemSocket");
            
            // Eqippes items
            platform.addButton(83, 10, 4.5f, 8, "equipped_helmet", true, "Content_GUI/itemSocket");
            platform.addButton(83, 20, 4.5f, 8, "equipped_armor", true, "Content_GUI/itemSocket");
            platform.addButton(83, 32, 4.5f, 8, "equipped_boots", true, "Content_GUI/itemSocket");
            platform.addButton(93, 22, 4.5f, 8, "equipped_weapon", true, "Content_GUI/itemSocket");

            platform.OnButtonValue += new GUI_Delegate_Button(this.ButtonEventValue);

            // Item stats
            platform.addLabel(50, 40, 5, "monoFont_big", "", "item_name", true);
            platform.addLabel(50, 45, 5, "monoFont_big", "", "item_lvl_strength", true);
            platform.addLabel(50, 50, 5, "monoFont_big", "", "item_armor", true);
            platform.addLabel(50, 55, 5, "monoFont_big", "", "item_damage", true);
            platform.addLabel(50, 60, 5, "monoFont_big", "", "item_str", true);
            platform.addLabel(50, 65, 5, "monoFont_big", "", "item_skill", true);
            platform.addLabel(50, 70, 5, "monoFont_big", "", "item_intel", true);

            platform.addPlainImage(0, 100 - 100 * 0.189f * 1.777f + 1, 100, 100 * 0.189f * 1.777f, "HUD", "HUD_small");

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

            for (int i = 1; i < Player.Instance.PlayerInventory.Count; i++)
            {
                switch (Player.Instance.PlayerInventory[i].Class)
                {
                    case ItemClass.Weapon:
                        platform.updateButtonPicture("itemSlot" + i, "fbxContent/items/icon_weapon");
                        break;
                    case ItemClass.Armor:
                        platform.updateButtonPicture("itemSlot" + i, "fbxContent/items/icon_armor");
                        break;
                    case ItemClass.Boots:
                        platform.updateButtonPicture("itemSlot" + i, "fbxContent/items/icon_boot");
                        break;
                    case ItemClass.Helmet:
                        platform.updateButtonPicture("itemSlot" + i, "fbxContent/items/icon_helmet");
                        break;
                    case ItemClass.Quest:
                        platform.updateButtonPicture("itemSlot" + i, "fbxContent/items/icon_quest");
                        break;
                    case ItemClass.Useable:
                        platform.updateButtonPicture("itemSlot" + i, "fbxContent/items/icon_useable");
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
