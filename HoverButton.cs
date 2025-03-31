using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Windows.UI.Core;

namespace AlgorithmVisualizerApp
{
    public class HoverButton : Button
    {
        public HoverButton()
        {
            this.PointerEntered += HoverButton_PointerEntered;
            this.PointerExited += HoverButton_PointerExited;
        }

        private void HoverButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            this.ProtectedCursor = InputSystemCursor.Create(InputSystemCursorShape.Hand);
        }

        private void HoverButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            this.ProtectedCursor = InputSystemCursor.Create(InputSystemCursorShape.Arrow);
        }
    }

}
