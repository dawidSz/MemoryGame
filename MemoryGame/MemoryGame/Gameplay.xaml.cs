using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for Gameplay.xaml
    /// </summary>
    public partial class Gameplay : Window
    {
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        TimeSpan ts;

        string time = string.Empty;
        int WIN = 0;
        string name = string.Empty;

        bool first_pick = false;
        bool second_pick = false;
        bool third_pick = false;

        int f_pick;
        int s_pick;
        int t_pick;

        bool keep_uncovered = false;
        int hold_covered = 0;

        // numbering every button, there are 16 of them
        enum b_xx { b_00, b_01, b_02, b_03, b_10, b_11, b_12, b_13, b_20, b_21, b_22, b_23, b_30, b_31, b_32, b_33, };

        int[] Buttons = new int[((int)b_xx.b_33)+1]; // array size is equal to number of fields (16)
                
        public Gameplay()
        {
            InitializeComponent();
            int buffor;
            Random rnd = new Random();
            for(int i = (int)b_xx.b_00; i <= (int)b_xx.b_33; i++) // loop <0;15>
            {
                bool put = true;
                buffor = rnd.Next((int)b_xx.b_00,(int)b_xx.b_33 + 1);
                for(int j = 0; j < i; j++)
                {
                    if(buffor == Buttons[j])
                    {
                        i--;
                        put = false;
                        break;
                    }
                }
                if (put)
                    Buttons[i] = buffor;
            }
            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
            sw.Start();
            dt.Start();
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            ts = sw.Elapsed;
            time = String.Format("{0:00}:{1:00}:{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            TimerLabel.Content = time;
            if(keep_uncovered)
            {
                if(hold_covered > 150) // about 3 seconds
                {
                    hold_covered = 0;
                    Cover();
                }
                else hold_covered++;
            }

            if (!keep_uncovered && hold_covered > 0)
            {
                hold_covered = 0;
                Cover();
            }
        }

        private void Cover()
        {
            int f_cover = f_pick;
            int s_cover = s_pick;

            int count_two = 0; // needs to cover two fields;

            int cover = f_cover; // first field
            while(count_two < 2)
            {
                switch (Buttons[cover])
                {
                    #region 16_cases
                    case (int)b_xx.b_00:
                        {
                            b_00.Background = Brushes.LightGray;
                            keep_uncovered = false;
                            break;
                        }
                    case (int)b_xx.b_01:
                        {
                            b_01.Background = Brushes.LightGray;
                            keep_uncovered = false;
                            break;
                        }
                    case (int)b_xx.b_02:
                        {
                            b_02.Background = Brushes.LightGray;
                            keep_uncovered = false;
                            break;
                        }
                    case (int)b_xx.b_03:
                        {
                            b_03.Background = Brushes.LightGray;
                            keep_uncovered = false;
                            break;
                        }
                    case (int)b_xx.b_10:
                        {
                            b_10.Background = Brushes.LightGray;
                            keep_uncovered = false;
                            break;
                        }
                    case (int)b_xx.b_11:
                        {
                            b_11.Background = Brushes.LightGray;
                            keep_uncovered = false;
                            break;
                        }
                    case (int)b_xx.b_12:
                        {
                            b_12.Background = Brushes.LightGray;
                            keep_uncovered = false;
                            break;
                        }
                    case (int)b_xx.b_13:
                        {
                            b_13.Background = Brushes.LightGray;
                            keep_uncovered = false;
                            break;
                        }
                    case (int)b_xx.b_20:
                        {
                            b_20.Background = Brushes.LightGray;
                            keep_uncovered = false;
                            break;
                        }
                    case (int)b_xx.b_21:
                        {
                            b_21.Background = Brushes.LightGray;
                            keep_uncovered = false;
                            break;
                        }
                    case (int)b_xx.b_22:
                        {
                            b_22.Background = Brushes.LightGray;
                            keep_uncovered = false;
                            break;
                        }
                    case (int)b_xx.b_23:
                        {
                            b_23.Background = Brushes.LightGray;
                            keep_uncovered = false;
                            break;
                        }
                    case (int)b_xx.b_30:
                        {
                            b_30.Background = Brushes.LightGray;
                            keep_uncovered = false;
                            break;
                        }
                    case (int)b_xx.b_31:
                        {
                            b_31.Background = Brushes.LightGray;
                            keep_uncovered = false;
                            break;
                        }
                    case (int)b_xx.b_32:
                        {
                            b_32.Background = Brushes.LightGray;
                            keep_uncovered = false;
                            break;
                        }
                    case (int)b_xx.b_33:
                        {
                            b_33.Background = Brushes.LightGray;
                            keep_uncovered = false;
                            break;
                        }
                    default: break;
#endregion
                }
                count_two++;
                cover = s_cover; // now second field
            }

            if (third_pick) // in case of choosing third field when 2 other fields are uncovered and not paired
            {
                f_pick = t_pick;
                third_pick = false;
            }
        }

        private Brush Change_bg(object sender, RoutedEventArgs e, b_xx button)
        {
            int background_type = 0;

            for (int i = (int)b_xx.b_00; i <= (int)b_xx.b_33; i++)
            {
                if ((int)button == Buttons[i])
                {
                    background_type = i;
                    break;
                }
            }

            switch ((background_type + 1) % 8)
            {
                case 0:
                    {
                        return Brushes.Red;
                    };
                case 1:
                    {
                        return Brushes.Blue;
                    };
                case 2:
                    {
                        return Brushes.Green;
                    };
                case 3:
                    {
                        return Brushes.Yellow;
                    };
                case 4:
                    {
                        return Brushes.Purple;
                    };
                case 5:
                    {
                        return Brushes.Brown;
                    };
                case 6:
                    {
                        return Brushes.Orange;
                    };
                case 7:
                    {
                        return Brushes.Pink;
                    };
                default:
                    {
                        return Brushes.Black;
                    }
            }
        }

        private void First_Pick(b_xx button)
        {
            first_pick = true;
            for (int i = (int)b_xx.b_00; i <= (int)b_xx.b_33; i++)
            {
                if ((int)button == Buttons[i])
                {
                    if (third_pick) t_pick = i;
                    else f_pick = i;
                    break;
                }
            }
        }

        private void Second_Pick(b_xx button)
        {
            second_pick = true;
            for (int i = (int)b_xx.b_00; i <= (int)b_xx.b_33; i++)
            {
                if ((int)button == Buttons[i])
                {
                    s_pick = i;
                    break;
                }
            }

            if (f_pick == s_pick) second_pick = false;

            if (first_pick && second_pick) Check_Pair();
        }

       
        private void Check_Pair() // simple version
        {
            first_pick = false;
            second_pick = false;

            if (((f_pick + 1) % 8) != ((s_pick + 1) % 8)) keep_uncovered = true;
            else
            {
                WIN++;
                if(WIN >= 8)
                {
                    sw.Stop();
                    dt.Stop();
                    //MessageBoxResult result = MessageBox.Show("Your time is: "+time+"\nDo you want to save your score?",
                    //    "YOU WON", MessageBoxButton.YesNo);

                    //if (result == MessageBoxResult.Yes)
                    //{

                    //}
                    WinningInformation.Content = "Congratulations!\nYou won the game!" +
                        "\nEnter your nickname,\nif you want to save your score!";
                    Nickname.Visibility = Visibility.Visible;
                    Save.Visibility = Visibility.Visible;
                }
            }
        }

        private void Pick(b_xx button)
        {
            if (hold_covered == 0)
            {
                if (!first_pick && !second_pick) First_Pick(button);
                else if (first_pick && !second_pick) Second_Pick(button);
            }
            else
            {
                third_pick = true;
                First_Pick(button); // third pick will become first pick of another move
            }
        }

#region Click_methods
        private void b_00_Click(object sender, RoutedEventArgs e)
        {
            if (b_00.Background == Brushes.LightGray)
            {
                keep_uncovered = false;
                b_00.Background = Change_bg(sender, e, b_xx.b_00);
                Pick(b_xx.b_00);
            }
        }   

        private void b_01_Click(object sender, RoutedEventArgs e)
        {
            if (b_01.Background == Brushes.LightGray)
            {
                keep_uncovered = false;
                b_01.Background = Change_bg(sender, e, b_xx.b_01);
                Pick(b_xx.b_01);
            }
        }

        private void b_02_Click(object sender, RoutedEventArgs e)
        {
            if (b_02.Background == Brushes.LightGray)
            {
                keep_uncovered = false;
                b_02.Background = Change_bg(sender, e, b_xx.b_02);
                Pick(b_xx.b_02);
                
            }
        }

        private void b_03_Click(object sender, RoutedEventArgs e)
        {
            if (b_03.Background == Brushes.LightGray)
            {
                keep_uncovered = false;
                b_03.Background = Change_bg(sender, e, b_xx.b_03);
                Pick(b_xx.b_03);
            }
        }

        private void b_10_Click(object sender, RoutedEventArgs e)
        {
            if (b_10.Background == Brushes.LightGray)
            {
                keep_uncovered = false;
                b_10.Background = Change_bg(sender, e, b_xx.b_10);
                Pick(b_xx.b_10);
            }
        }

        private void b_11_Click(object sender, RoutedEventArgs e)
        {
            if (b_11.Background == Brushes.LightGray)
            {
                keep_uncovered = false;
                b_11.Background = Change_bg(sender, e, b_xx.b_11);
                Pick(b_xx.b_11);
            }
        }

        private void b_12_Click(object sender, RoutedEventArgs e)
        {
            if (b_12.Background == Brushes.LightGray)
            {
                keep_uncovered = false;
                b_12.Background = Change_bg(sender, e, b_xx.b_12);
                Pick(b_xx.b_12);
            }
        }

        private void b_13_Click(object sender, RoutedEventArgs e)
        {
            if (b_13.Background == Brushes.LightGray)
            {
                keep_uncovered = false;
                b_13.Background = Change_bg(sender, e, b_xx.b_13);
                Pick(b_xx.b_13);
            }
        }

        private void b_20_Click(object sender, RoutedEventArgs e)
        {
            if (b_20.Background == Brushes.LightGray)
            {
                keep_uncovered = false;
                b_20.Background = Change_bg(sender, e, b_xx.b_20);
                Pick(b_xx.b_20);
            }
        }

        private void b_21_Click(object sender, RoutedEventArgs e)
        {
            if (b_21.Background == Brushes.LightGray)
            {
                keep_uncovered = false;
                b_21.Background = Change_bg(sender, e, b_xx.b_21);
                Pick(b_xx.b_21);
            }
        }

        private void b_22_Click(object sender, RoutedEventArgs e)
        {
            if (b_22.Background == Brushes.LightGray)
            {
                keep_uncovered = false;
                b_22.Background = Change_bg(sender, e, b_xx.b_22);
                Pick(b_xx.b_22);
            }
        }

        private void b_23_Click(object sender, RoutedEventArgs e)
        {
            if (b_23.Background == Brushes.LightGray)
            {
                keep_uncovered = false;
                b_23.Background = Change_bg(sender, e, b_xx.b_23);
                Pick(b_xx.b_23);
            }
        }

        private void b_30_Click(object sender, RoutedEventArgs e)
        {
            if (b_30.Background == Brushes.LightGray)
            {
                keep_uncovered = false;
                b_30.Background = Change_bg(sender, e, b_xx.b_30);
                Pick(b_xx.b_30);
            }
        }

        private void b_31_Click(object sender, RoutedEventArgs e)
        {
            if (b_31.Background == Brushes.LightGray)
            {
                keep_uncovered = false;
                b_31.Background = Change_bg(sender, e, b_xx.b_31);
                Pick(b_xx.b_31);
            }
        }

        private void b_32_Click(object sender, RoutedEventArgs e)
        {
            if (b_32.Background == Brushes.LightGray)
            {
                keep_uncovered = false;
                b_32.Background = Change_bg(sender, e, b_xx.b_32);
                Pick(b_xx.b_32);
            }
        }

        private void b_33_Click(object sender, RoutedEventArgs e)
        {
            if (b_33.Background == Brushes.LightGray)
            {
                keep_uncovered = false;
                b_33.Background = Change_bg(sender, e, b_xx.b_33);
                Pick(b_xx.b_33);
            }
        }
        private void b_41_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
            Gameplay new_game = new Gameplay();
            new_game.ShowDialog();
        }
        private void b_42_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Save_click(object sender, RoutedEventArgs e)
        {
            name = Nickname.Text;
            File.AppendAllText("highscores.txt", time + " " + name + Environment.NewLine);
            Save.Visibility = Visibility.Collapsed;
        }
    }
}
#endregion