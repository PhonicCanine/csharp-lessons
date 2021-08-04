using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TicTacToeWed
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            for (int x = 0; x < 3; x = x + 1)
            {
                for (int y = 0; y < 3; y = y + 1)
                {
                    var grid = new Grid
                    {
                        BackgroundColor = Color.Aquamarine
                    };
                    Grid.SetRow(grid, y);
                    Grid.SetColumn(grid, x);
                    GameGrid.Children.Add(grid);

                    var button = new Button
                    {
                        Text = "X",
                        FontSize = 25,
                        BackgroundColor = Color.Transparent
                    };
                    button.Clicked += getButtonHandler(x, y);
                    grid.Children.Add(button);
                }
            }
        }
        EventHandler getButtonHandler(int x, int y) => (object sender, EventArgs args) =>
        {
            DisplayAlert("Clicked", $"x:{x}, y:{y}", "Ok");
        };
    }
}
