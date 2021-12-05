using System;
using System.Collections.Generic;

namespace Zadanie_1
{
    public class NPCDialogPart: IDialogPart
    {
        public List<HeroDialogPart> heroDialogParts = new List<HeroDialogPart>();
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