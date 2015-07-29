using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace EmodiaQuest.Core.DungeonGeneration
{
    public class Room
    {
        // these values hold grid coordinates for each corner of the room
        public float X;
        public float X2;
        public float Y;
        public float Y2;

        // width and height of room in terms of grid
        public int Width;
        public int Height;

        // center point of the room
        public Point Center;

        // constructor for creating new rooms
        public Room(int x, int y, int w, int h)
        {
            this.X = x;
            X2 = x + w;
            this.Y = y;
            Y2 = y + h;
            this.Width = w;
            this.Height = h;
            Center = new Point((int)Math.Floor((x + X2) / 2), (int)Math.Floor((y + Y2) / 2));
        }

        // return true if this room intersects provided room
        public bool Intersects(Room room)
        {
            return (X <= room.X2 && X2 >= room.X && Y <= room.Y2 && room.Y2 >= room.Y);
        }
    }
}
