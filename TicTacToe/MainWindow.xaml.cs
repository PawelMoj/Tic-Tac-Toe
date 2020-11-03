using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private bool _win;

        private Enumki[] enumki;

        private bool player1Turn;
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        private void NewGame()
        {
            enumki = new Enumki[9];

            for (int i = 0; i<enumki.Length; i++)
            {
                enumki[i] = Enumki.Free;
            }

            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            player1Turn = true;
            
            _win = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var button = (Button)sender;
            var row = Grid.GetRow(button);
            var column = Grid.GetColumn(button);

            int index = column + (row * 3);

            if (enumki[index] != Enumki.Free)
                return;

            enumki[index] = player1Turn ? Enumki.mX : Enumki.Circle;

            button.Content = player1Turn ? "X" : "O";

            button.Foreground = player1Turn ? Brushes.Blue : Brushes.Red;

            player1Turn ^= true;

            string winner = CheckForWin();
            if (_win)
            {
                MessageBox.Show($"Game is finished, result: {winner}");
                NewGame();
                return;
            }
        }

        // Function will check wheter somone has won or board is full
        private string CheckForWin()
        {
            string whoWin = "Noone wins";
            // First check Columns
            for (int i = 0; i < 3; ++i)
            {
                bool theSameColumn = ((enumki[i] & enumki[i + 3] & enumki[i + 6]) == enumki[i]) && (enumki[i] != Enumki.Free);
                if (theSameColumn)
                {
                    _win = true;
                    return whoWin = (enumki[i] == Enumki.Circle) ? "Circle wins" : "Crosses wins";                    
                }
            }
            // Check for Rows
            for (int i = 0; i < 8; i = i + 3)
            {
                bool theSameRow = ((enumki[i] & enumki[i + 1] & enumki[i + 2]) == enumki[i]) & (enumki[i] != Enumki.Free);
                if (theSameRow)
                {
                    _win = true;
                    return whoWin = (enumki[i] == Enumki.Circle) ? "Circle wins" : "Crosses wins";                    
                }
            }
            if((enumki[0] & enumki[4] & enumki[8]) == enumki[0] && enumki[0] != Enumki.Free)
            {
                _win = true;
                return whoWin = (enumki[0] == Enumki.Circle) ? "Circle wins" : "Crosses wins";
            }
            if ((enumki[2] & enumki[4] & enumki[6]) == enumki[2] && enumki[2] != Enumki.Free)
            {
                _win = true;
                return whoWin = (enumki[0] == Enumki.Circle) ? "Circle wins" : "Crosses wins";
            }
            //Check if board is full
            if (!enumki.Contains(Enumki.Free))
            {
                _win = true;
            }
            return whoWin;
        }



    }
}
