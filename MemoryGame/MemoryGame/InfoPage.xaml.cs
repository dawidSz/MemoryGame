using System.Windows;
using System.Windows.Controls;

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
