using System;
using System.Collections.Generic;

namespace Zadanie_1
{
    public class HeroDialogPart: IDialogPart
    {
        public NPCDialogPart nextNPCDialogPart = new NPCDialogPart();
        public string text;

        public string DisplayText
        {
            get
            {
                return text;
            }
        }
    }
}