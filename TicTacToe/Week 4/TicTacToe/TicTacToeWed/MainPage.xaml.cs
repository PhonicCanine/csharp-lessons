using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TicTacToeWed
{
    public partial class MainPage : ContentPage
    {
        enum State : int
        {
            Unfilled = 0,
            X = 1,
            O = -1
        }
        public Button[,] buttons { get; set; } = new Button[3, 3];
        State[,] board { get; set; } = new State[3, 3];

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
                        Text = "",
                        FontSize = 25,
                        BackgroundColor = Color.Transparent
                    };
                    button.Clicked += getButtonHandler(x, y);
                    buttons[x, y] = button;
                    grid.Children.Add(button);
                }
            }
        }
        string getIcon(State s) {
            if (s == State.X)
            {
                return "X";
            } else if (s == State.O)
            {
                return "O";
            } else
            {
                return "";
            }
        }
        EventHandler getButtonHandler(int x, int y) => (object sender, EventArgs args) =>
        {
            if (board[x,y] == State.Unfilled)
            {
                board[x, y] = State.X;
                buttons[x, y].Text = getIcon(board[x, y]);
                if (checkForWin() == Result.NotOver)
                {
                    GetAIMove();
                } else
                {
                    DisplayAlert(
                        "Game Over",
                        "The game has ended",
                        "Ok");
                }
            } else
            {
                DisplayAlert(
                    "Already Filled!",
                    "Please pick another square",
                    "Ok");
            }
        };

        enum Result: int
        {
            NotOver,
            Tie,
            XWins,
            OWins
        }

        Result checkForWin()
        {

            //Check for a tie.
            bool allFilled = true;
            for (int x = 0; x < 3; x = x + 1)
            {
                for (int y = 0; y < 3; y = y + 1)
                {
                    if (board[x,y] == State.Unfilled)
                    {
                        allFilled = false;
                    }
                }
            }
            if (allFilled)
            {
                return Result.Tie;
            }

            return Result.NotOver;
        }


        void GetAIMove()
        {
            Random r = new Random();
            int x = r.Next() % 3;
            int y = r.Next() % 3;
            while (board[x,y] != State.Unfilled)
            {
                x = r.Next() % 3;
                y = r.Next() % 3;
            }
            board[x, y] = State.O;
            buttons[x, y].Text = getIcon(board[x, y]);
            if (checkForWin() != Result.NotOver)
            {
                DisplayAlert("Game Over", "Game Over", "Ok");
            }
        }
    }
}
