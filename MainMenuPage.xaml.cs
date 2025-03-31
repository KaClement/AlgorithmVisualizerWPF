using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AlgorithmVisualizerApp
{

    public sealed partial class MainMenuPage : Page
    {
        private Frame _rootFrame; // Store the Frame

        public MainMenuPage()
        {
            this.InitializeComponent();
        }


        // Reception of the instance MainFrame, we store it into _rootFrame (private field)
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // Receive the Frame from the parameter
            _rootFrame = (Frame)e.Parameter!;
        }

        private void SortingButton_Click(object sender, RoutedEventArgs e)
        {

            _rootFrame.Navigate(typeof(SortingPage), _rootFrame);


        }

        private void PathfindingButton_Click(object sender, RoutedEventArgs e)
        {
            _rootFrame.Navigate(typeof(PathfindingPage), _rootFrame);
        }
    }
}
