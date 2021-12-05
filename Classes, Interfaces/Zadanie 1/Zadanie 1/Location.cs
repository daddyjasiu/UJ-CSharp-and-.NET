using System;
using System.Collections.Generic;

namespace Zadanie_1
{
    public class Location
    {
        public List<NPC> npcsList = new List<NPC>();
        public string name;
        public bool isUnlocked;

        public Location(string name, bool isUnlocked)
        {
            this.name = name;
            this.isUnlocked = isUnlocked;
            
            NPC npc1 = new NPC("Tyrael");
            NPC npc2 = new NPC("Adria");

            npcsList.Add(npc1);
            npcsList.Add(npc2);
        }
    }
}