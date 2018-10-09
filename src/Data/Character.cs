using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Classic.Data.CharacterEnums;

namespace Classic.Data
{
    public class Character
    {
        private static readonly object sync = new object();
        private static int counter = 0;

        public Character()
        {
            lock (sync)
            {
                this.ID = (ulong)counter++;
            }
        }

        public ulong ID { get; }
        public string Name { get; set; }
        public Races Race { get; set; }
        public Classes Class { get; set; }
        public Genders Gender { get; set; }
        public byte Skin { get; set; }
        public byte Face { get; set; }
        public byte HairStyle { get; set; }
        public byte HairColor { get; set; }
        public byte FacialHair { get; set; }
        public byte OutfitId { get; set; }
    }
}
