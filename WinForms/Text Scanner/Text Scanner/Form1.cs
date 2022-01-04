using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Text_Scanner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void verifyText()
        {
            char[] delimiterChars = {' ', '\t', '\n'};
            var splitInputText = textInputBox.Text.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            var words = splitInputText
                .AsParallel()
                .WithDegreeOfParallelism(4)
                .Select(x => x)
                .Where(x => x != "\r")
                .ToList();

            foreach(string word in words)
            {
                Console.WriteLine("word: " + word);
                errorsTextBox.AppendText(word);
                errorsTextBox.AppendText(Environment.NewLine);
            }
            Console.WriteLine(words.Count);
            
        }

        private void verifyButton_Click(object sender, EventArgs e)
        {
            errorsTextBox.Clear();

            if (String.IsNullOrEmpty(textInputBox.Text))
                return;

            verifyText();
        }

        private void textInputBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
