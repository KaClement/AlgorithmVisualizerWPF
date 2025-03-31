using System;
using System.Collections.Generic;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;
using Windows.UI.Core;

namespace AlgorithmVisualizerApp
{
    public struct GridPosition
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public GridPosition(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public override string ToString() => $"({Row}, {Col})";
    }

    public sealed partial class PathfindingPage : Page
    {
        private const int GridRows = 20;
        private const int GridCols = 30;
        private Rectangle[,] gridCells;
        private GridPosition? startPoint = null;
        private GridPosition? endPoint = null;
        private bool isSelectingWalls = false;
        private int tapCount = 0;

        public PathfindingPage()
        {
            this.InitializeComponent();
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            gridCells = new Rectangle[GridRows, GridCols];

            for (int i = 0; i < GridRows; i++)
            {
                GridContainer.RowDefinitions.Add(new RowDefinition());
            }

            for (int j = 0; j < GridCols; j++)
            {
                GridContainer.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < GridRows; i++)
            {
                for (int j = 0; j < GridCols; j++)
                {
                    Rectangle cell = new Rectangle
                    {
                        Fill = new SolidColorBrush(Colors.White),
                        Stroke = new SolidColorBrush(Colors.LightGray),
                        Width = 25,
                        Height = 25

                    };
                    int currentRow = i;
                    int currentCol = j;
                    cell.Tapped += (s, e) => Cell_Tapped(currentRow, currentCol);
                    Grid.SetRow(cell, i);
                    Grid.SetColumn(cell, j);
                    GridContainer.Children.Add(cell);
                    gridCells[i, j] = cell;
                }
            }
        }

        private async void Cell_Tapped(int row, int col)
        {
            tapCount++;
            if (tapCount == 1)
            {
                gridCells[row, col].Fill = new SolidColorBrush(Colors.Green);

            }else if (tapCount == 2)
            {
                gridCells[row, col].Fill = new SolidColorBrush(Colors.Red);


            }
            else
            {
                gridCells[row, col].Fill = new SolidColorBrush(Colors.Black);

            }
            var gridData = new { row = row, col = col };

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var frame = this.Parent as Frame;
            if (frame != null && frame.CanGoBack)
            {
                frame.GoBack();
            }
        }
    


        private void BackButton_PointerEntered(object sender, PointerRoutedEventArgs e)
            {
                // Change cursor to a hand when the pointer enters the button
                Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Hand, 1);
            }

        private void BackButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            // Change cursor back to arrow when the pointer leaves the button
            Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, 1);
        }

    }
}