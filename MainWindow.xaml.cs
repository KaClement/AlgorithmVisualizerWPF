using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AlgorithmVisualizerApp
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            // We pass MainFrame instance as parameters to MainMenuPage
            MainFrame.Navigate(typeof(MainMenuPage), MainFrame);

        }

    }
}
