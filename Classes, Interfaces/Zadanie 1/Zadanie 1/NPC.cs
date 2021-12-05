using System;
using System.Collections.Generic;

namespace Zadanie_1
{
    public class NPC
    {
        public string name;

        public NPC(string name)
        {
            this.name = name;
        }
        
        public NPCDialogPart StartTalking()
        {
            return new NPCDialogPart();
        }
    }
}