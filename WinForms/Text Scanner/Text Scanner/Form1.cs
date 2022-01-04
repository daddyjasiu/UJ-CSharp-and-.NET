using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Text_Scanner
{
    public partial class Form1 : Form
    {   
        private delegate void EventHandle();
        HashSet<string> dictionary = new HashSet<string>();
        public Form1()
        {
            InitializeComponent();
            Thread t = new Thread(getFileInfo);
            t.Start();
        }

        private void getFileInfo()
		{
            var temp = System.IO.File.ReadAllText("C:/Users/jansk/OneDrive/Documents/UJ/C# i .NET/UJ-CSharp-and-.NET/WinForms/Text Scanner/Text Scanner/dictionary.txt").ToLower();
            System.IO.File.WriteAllText("C:/Users/jansk/OneDrive/Documents/UJ/C# i .NET/UJ-CSharp-and-.NET/WinForms/Text Scanner/Text Scanner/dictionaryLower.txt", temp);
            dictionary = System.IO.File.ReadAllLines("C:/Users/jansk/OneDrive/Documents/UJ/C# i .NET/UJ-CSharp-and-.NET/WinForms/Text Scanner/Text Scanner/dictionaryLower.txt").ToHashSet();            
		}

        private void verifyText()
        {
            char[] delimiterChars = {' ', '.', '!', '?', ',', '\t', '\n'};
            var splitInputText = textInputBox.Text.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            var words = splitInputText
                .AsParallel()
                .WithDegreeOfParallelism(4)
                .Select(x => x)
                .Where(x => x != "\r")
                .ToList();

            var wordsDictionary = new Dictionary<string, int>();
            var wordsDictionaryCopy = new Dictionary<string, int>();

			if (progressBar.InvokeRequired)
			{
                progressBar.Invoke(new MethodInvoker(delegate
                {
                    progressBar.Value = 0;
                    progressBar.Minimum = 0;
                    progressBar.Maximum = words.Count;
                }));
			}

            Thread t1 = new Thread(() =>
            {
                foreach (string word in words)
                {
                    if (!wordsDictionary.ContainsKey(word.ToLower()))
                    {
                        wordsDictionary.Add(word, 1);
                    }
                    else
                    {
                        wordsDictionary[word]++;
                    }

                    if (progressBar.InvokeRequired)
                    {
                        progressBar.Invoke(new MethodInvoker(delegate
                        {
                            progressBar.Value++;
                        }));
                    }
                }

                foreach (string word in wordsDictionary.Keys)
                {
                    if (countsTextBox.InvokeRequired)
                    {
                        countsTextBox.Invoke(new MethodInvoker(delegate
                        {
                            countsTextBox.AppendText(word);
                            countsTextBox.AppendText("-");
                            countsTextBox.AppendText(wordsDictionary[word].ToString());
                            countsTextBox.AppendText(Environment.NewLine);
                        }));
                    }

                }
            });

            Thread t2 = new Thread(() =>
            {
                foreach (string word in wordsDictionary.Keys)
                {
                    Console.WriteLine("word: " + word);
                    if (!dictionary.Contains(word.ToLower()))
                    {
                        if (errorsTextBox.InvokeRequired)
                        {
                            errorsTextBox.Invoke(new MethodInvoker(delegate
                            {
                                errorsTextBox.AppendText(word);
                                errorsTextBox.AppendText(Environment.NewLine);
                            }));
                        }

                    }
                }
            });
            t1.Start();
            t1.Join();
            t2.Start();

        }

        private void verifyButton_Click(object sender, EventArgs e)
        {
            errorsTextBox.Clear();
            countsTextBox.Clear();

            if (String.IsNullOrEmpty(textInputBox.Text))
                return;


            Thread t = new Thread(verifyText);
            t.Start();

        }

        private void textInputBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
