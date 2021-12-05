using System;

namespace Zadanie_1
{
    public class DialogParser
    {
        private string name;
        public DialogParser(Hero hero)
        {
            name = hero.name;
        }
        public string ParseDialog(IDialogPart dialogPartInterface)
        {
            string text = dialogPartInterface.DisplayText;
            text = text.Replace("#HERONAME#", name);
            return text;
        }
    }
}