using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmodiaQuest.Core
{
    class Item_Armor : Item_ValueParent
    {
        public int requiredStrength = 0;
        public Item_Armor(string itemName, int requiredStrength, int strength, int tenacity)
        {
            this.itemName = itemName;
            this.requiredStrength = requiredStrength;
            this.strength = strength;
            this.tenacity = tenacity;
        }
    }

    // "Leichter Stoff", 0, 0, 3
    // "Schwerer Stoff", 5, 0, 5

    // "Leichtes Leder", 8, 0, 8
    // "Schweres Leder", 10, 0, 10

    // "Leichtes Eisen", 15, 0, 12
    // "Schweres Eisen", 20, 0, 15

    // "Leichtes Rowen", 30, 0, 20
    // "Schweres Rowen", 50, 0, 25


    // Ruestungen
    // "Leichte Stoffruestung", 0, 0, 3
    // "Schwere Stoffruestung", 5, 0, 5

    // "Leichte Lederruestung", 8, 0, 8
    // "Schwere Lederruestung", 10, 0, 10

    // "Leichte Eisenruestung", 15, 0, 12
    // "Schwere Eisenruestung", 20, 0, 15

    // "Leichte Rowenruestung", 30, 0, 20
    // "Schwere Rowenruestung", 50, 0, 25

    // Helme
    // "Leichter Stoffhelm", 0, 0, 2
    // "Schwerer Stoffhelm", 3, 0, 3

    // "Leichter Lederhelm", 5, 0, 5
    // "Schwerer Lederhelm", 7, 0, 7

    // "Leichter Eisenhelm", 12, 0, 10
    // "Schwerer Eisenhelm", 14, 0, 12

    // "Leichter Rowenhelm", 20, 0, 15
    // "Schwerer Rowenhelm", 35, 0, 20

    // Schuhe
    // "Leichte Stoffschuhe", 0, 0, 2
    // "Schwere Stoffschuhe", 2, 0, 2

    // "Leichte Lederschuhe", 4, 0, 4
    // "Schwere Lederschuhe", 6, 0, 6

    // "Leichte Eisenschuhe", 10, 0, 8
    // "Schwere Eisenschuhe", 12, 0, 10

    // "Leichte Rowenschuhe", 20, 0, 15
    // "Schwere Rowenschuhe", 30, 0, 18


}
