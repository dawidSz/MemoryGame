using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace MemoryGame
{
    public partial class HighscoresPage : Page
    {
        public HighscoresPage()
        {
            InitializeComponent();
            FileStream fs = new FileStream("highscores.txt", FileMode.Open, FileAccess.Read);
            int NumberOfLines = File.ReadAllLines("highscores.txt").Length;
            string[] s = new string[NumberOfLines]; // array for time in format XX:XX:XX
            string[] time = new string[NumberOfLines]; // array for time in format without ':'
            int[] score = new int[NumberOfLines];       // array for time converted to int
            string[] name = new string[NumberOfLines];  // array for Nickname

            try
            {
                StreamReader sr = new StreamReader(fs);
                for(int i=0; i<NumberOfLines; i++) // loop which gets data from file and sets it into arrays
                {
                    char[] buff = new char[8];
                    sr.ReadBlock(buff, 0, 8);
                    s[i] = new string(buff);
                    time[i] = s[i].Substring(0, 2) + s[i].Substring(3, 2) + s[i].Substring(6, 2);
                    
                    string value = time[i];
                    int number;
                    number = Int32.Parse(value);
                    score[i] = number;
                    name[i] = sr.ReadLine();
                }
                sr.Close();

                for (int i=1; i<NumberOfLines; i++) // bubblesort
                {
                    for(int j=0; j<NumberOfLines-1; j++)
                    {
                        if(score[j] > score[j + 1])
                        {
                            int temp1 = score[j + 1];
                            score[j + 1] = score[j];
                            score[j] = temp1;

                            string temp2 = time[j + 1];
                            time[j + 1] = time[j];
                            time[j] = temp2;

                            string temp3 = name[j + 1];
                            name[j + 1] = name[j];
                            name[j] = temp3;
                        }
                    }
                }
                File.Delete("highscores.txt");
                for (int i=0; i<NumberOfLines; i++) // sets time in format XX:XX:XX and write sorted scores in File
                {
                    s[i] = time[i].Substring(0, 2) +":"+ time[i].Substring(2, 2) +":"+ time[i].Substring(4, 2);                   
                    File.AppendAllText("highscores.txt", s[i] + name[i] + Environment.NewLine);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Scores.Content = File.ReadAllText("highscores.txt");   //Outputs data to a Label     

        }

        private void highscores_backbutton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainWindowPage());
        }
    }
}
