using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;
using Windows.Media.Playback;
using Microsoft.UI;

namespace AlgorithmVisualizerApp
{
    public sealed partial class SortingPage : Page
    {
        private const int NumberOfBars = 85;
        private const int BarWidth = 10;
        private const int MaxBarHeight = 500;
        private List<int> barValues = new();
        private bool isSorting = false;
        private MediaPlayer mediaPlayer = new();

        private enum SortingAlgorithm
        {
            None,
            MergeSort,
            InsertionSort,
            QuickSort,
            BubbleSort
        }

        private SortingAlgorithm _selectedAlgorithm = SortingAlgorithm.None;

        public SortingPage()
        {
            this.InitializeComponent();
            GenerateBars();
        }

        private void GenerateBars()
        {
            if (isSorting) return;

            BarsContainer.Children.Clear();
            barValues.Clear();

            Random rand = new();
            for (int i = 0; i < NumberOfBars; i++)
            {
                int height = rand.Next(50, MaxBarHeight);
                barValues.Add(height);
                Rectangle bar = new()
                {
                    Width = BarWidth,
                    Height = height,
                    Fill = new SolidColorBrush(Color.FromArgb(255, 59, 130, 246)),
                    Margin = new Thickness(2, 0, 2, 0),
                    VerticalAlignment = VerticalAlignment.Bottom
                };
                BarsContainer.Children.Add(bar);
            }
        }

        private void UpdateBarHeight(int index, int height, Color color)
        {
            Rectangle bar = (Rectangle)BarsContainer.Children[index];
            bar.Height = height;
            bar.Fill = new SolidColorBrush(color);
        }

        private void HighlightBar(int index, Color color)
        {
            Rectangle bar = (Rectangle)BarsContainer.Children[index];
            bar.Fill = new SolidColorBrush(color);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var frame = this.Parent as Frame;
            if (frame != null && frame.CanGoBack)
            {
                frame.GoBack();
            }
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedAlgorithm == SortingAlgorithm.None || isSorting)
            {
                return;
            }

            isSorting = true;

            switch (_selectedAlgorithm)
            {
                case SortingAlgorithm.MergeSort:
                    await MergeSort(0, barValues.Count - 1);
                    break;
                case SortingAlgorithm.InsertionSort:
                    await InsertionSort();
                    break;
                case SortingAlgorithm.QuickSort:
                    await QuickSort();
                    break;
                case SortingAlgorithm.BubbleSort:
                    await BubbleSort();
                    break;
            }

            isSorting = false;
        }

        private void ShuffleButton_Click(object sender, RoutedEventArgs e)
        {
            if (isSorting)
            {
                isSorting = false; // Stop sorting immediately
            }
            GenerateBars();
        }

        private void AlgorithmsButton_Click(object sender, RoutedEventArgs e)
        {
            if (AlgorithmComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedAlgorithm = selectedItem.Content.ToString();
                _selectedAlgorithm = selectedAlgorithm switch
                {
                    "Merge Sort" => SortingAlgorithm.MergeSort,
                    "Insertion Sort" => SortingAlgorithm.InsertionSort,
                    "Quick Sort" => SortingAlgorithm.QuickSort,
                    "Bubble Sort" => SortingAlgorithm.BubbleSort,
                    _ => SortingAlgorithm.None
                };
            }
        }

        private async Task InsertionSort()
        {
            var bars = BarsContainer.Children.Cast<Rectangle>().ToList();

            for (int i = 1; i < bars.Count; i++)
            {
                int j = i;
                int currentHeight = (int)bars[i].Height;

                while (j > 0 && (int)bars[j - 1].Height > currentHeight)
                {
                    if (!isSorting) return;

                    barValues[j] = barValues[j - 1];
                    UpdateBarHeight(j, barValues[j - 1], Colors.Orange);
                    UpdateBarHeight(j - 1, currentHeight, Colors.Red);

                    await Task.Delay(20);

                    j--;
                }
                barValues[j] = currentHeight;
                UpdateBarHeight(j, currentHeight, Colors.Green);
                PlaySound();
                await Task.Delay(20);
            }
        }

        private async Task QuickSort()
        {
            await QuickSortHelper(0, barValues.Count - 1);
        }

        private async Task QuickSortHelper(int low, int high)
        {
            if (low < high && isSorting)
            {
                int pivotIndex = await Partition(low, high);
                await QuickSortHelper(low, pivotIndex - 1);
                await QuickSortHelper(pivotIndex + 1, high);
            }
        }

        private async Task<int> Partition(int low, int high)
        {
            int pivot = barValues[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (!isSorting) return low;

                if (barValues[j] < pivot)
                {
                    i++;
                    (barValues[i], barValues[j]) = (barValues[j], barValues[i]);
                    UpdateBarHeight(i, barValues[i], Colors.Orange);
                    UpdateBarHeight(j, barValues[j], Colors.Red);
                    PlaySound();
                    await Task.Delay(50);
                }
            }

            (barValues[i + 1], barValues[high]) = (barValues[high], barValues[i + 1]);
            UpdateBarHeight(i + 1, barValues[i + 1], Colors.Green);
            UpdateBarHeight(high, barValues[high], Colors.Blue);
            PlaySound();
            await Task.Delay(50);

            return i + 1;
        }

        private async Task BubbleSort()
        {
            for (int i = 0; i < barValues.Count - 1; i++)
            {
                for (int j = 0; j < barValues.Count - i - 1; j++)
                {
                    if (!isSorting) return;

                    if (barValues[j] > barValues[j + 1])
                    {
                        (barValues[j], barValues[j + 1]) = (barValues[j + 1], barValues[j]);
                        UpdateBarHeight(j, barValues[j], Colors.Orange);
                        UpdateBarHeight(j + 1, barValues[j + 1], Colors.Red);
                        await Task.Delay(20);
                    }
                }
                UpdateBarHeight(barValues.Count - 1 - i, barValues[barValues.Count - 1 - i], Colors.Green);
                PlaySound();
                await Task.Delay(20);

            }
            // Ensure the first element is also marked sorted
            UpdateBarHeight(0, barValues[0], Colors.Green);
            PlaySound();

        }
        private async Task MergeSort(int left, int right)
        {
            if (left >= right || !isSorting) return;

            int mid = (left + right) / 2;

            await MergeSort(left, mid);
            await MergeSort(mid + 1, right);
            await Merge(left, mid, right);
        }

        private async Task Merge(int left, int mid, int right)
        {
            List<int> temp = new();
            int i = left, j = mid + 1;

            while (i <= mid && j <= right)
            {
                if (!isSorting) return;

                if (barValues[i] <= barValues[j])
                {
                    temp.Add(barValues[i++]);
                }
                else
                {
                    temp.Add(barValues[j++]);
                }

                await Task.Delay(20);
            }

            while (i <= mid) temp.Add(barValues[i++]);
            while (j <= right) temp.Add(barValues[j++]);

            for (int k = 0; k < temp.Count; k++)
            {
                if (!isSorting) return;

                barValues[left + k] = temp[k];
                UpdateBarHeight(left + k, temp[k], Colors.Green);
                PlaySound();

                await Task.Delay(20);
            }
        }

        private void PlaySound()
        {
            var player = new MediaPlayer();
            player.Source = Windows.Media.Core.MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/pop.wav"));
            player.Play();
        }
    }
}
