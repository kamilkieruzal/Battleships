using Battleships.Core;
using Battleships.Core.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Battleships
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private int clickCount = 0;
        private Game game;
        private IDictionary<FieldType, Brush> visualDict = new Dictionary<FieldType, Brush>
            {
                { FieldType.Hit, Brushes.MediumVioletRed },
                { FieldType.Destroyed, Brushes.DarkRed },
                { FieldType.Empty, Brushes.RoyalBlue }
            };

        public GameWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();

            game = new Game(new GameObjectsPool());

            for(var i = 0; i < GameConsts.SizeOfArena; i++)
            {
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
                GameGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (var i = 0; i < GameConsts.SizeOfArena; i++)
            {
                for (var j = 0; j < GameConsts.SizeOfArena; j++)
                {
                    var border = CreateBorder();
                    var label = CreateColorLabel(FieldType.Empty);
                    label.MouseLeftButtonDown += Label_MouseLeftButtonDown;
                    border.Child = label;

                    GameGrid.Children.Add(border);
                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);
                }
            }
        }

        private void Label_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            clickCount++;

            var label = (Label)sender;
            var border = (Border) label.Parent;
            var x = (int) border.GetValue(Grid.ColumnProperty);
            var y = (int) border.GetValue(Grid.RowProperty);

            var field = game.Hit(new Coordinates(x, y));
            sender = CreateColorLabel(field.FieldType);
            border.Child = (Label)sender;

            if (field.FieldType == FieldType.Destroyed)
            {
                var shipField = (ShipField)game.GameArea[x][y];
                var ship = shipField.PartOf;

                foreach (var destroyedField in ship.ShipFields)
                {
                    foreach (var element in GameGrid.Children.OfType<Border>())
                    {
                        if (Grid.GetRow(element) == destroyedField.Coordinates.Y && Grid.GetColumn(element) == destroyedField.Coordinates.X)
                        {
                            var newLabel = CreateColorLabel(FieldType.Destroyed);
                            ((Border)element).Child = newLabel;
                        }
                    }
                }
            }

            if (!game.AnyShipsLeft())
            {
                MessageBox.Show($"You won and clicked {clickCount} times!");
                Environment.Exit(0);
            }
        }

        private static Border CreateBorder()
        {
            return new Border
            {
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.Black
            };
        }

        private Label CreateColorLabel(FieldType fieldType)
        {
            return new Label
            {
                FontSize = 20,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                Background = visualDict[fieldType],
            };
        }

    }
}
