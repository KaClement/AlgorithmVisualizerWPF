using Microsoft.UI.Xaml;
using WinRT.Interop;

namespace AlgorithmVisualizerApp
{
    public partial class App : Application
    {
        public static Window? AppWindow { get; private set; }
       
        
       
        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            AppWindow = new MainWindow();
            AppWindow.Activate();
        }






    }
}
