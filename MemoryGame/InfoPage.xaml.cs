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

namespace MemoryGame
{
    /// <summary>
    /// Logika interakcji dla klasy InfoPage.xaml
    /// </summary>
    public partial class InfoPage : Page
    {
        public InfoPage()
        {
            InitializeComponent();
        }

        private void highscores_backbutton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainWindowPage());
        }
    }
}
