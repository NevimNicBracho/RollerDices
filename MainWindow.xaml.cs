using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DiceRollingApp
{
    public partial class MainWindow : Window
    {
        private Random random = new Random();
        private int rollCount = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRoll_Click(object sender, RoutedEventArgs e)
        {
            int[] diceValues = RollDice();
            UpdateDiceVisuals(diceValues);
            SaveDiceHistory(diceValues);
        }

        private int[] RollDice()
        {
            int[] diceValues = new int[6];
            for (int i = 0; i < diceValues.Length; i++)
            {
                diceValues[i] = random.Next(1, 7);
            }
            return diceValues;
        }

        private void UpdateDiceVisuals(int[] diceValues)
        {
            DrawDice(dice1, diceValues[0]);
            DrawDice(dice2, diceValues[1]);
            DrawDice(dice3, diceValues[2]);
            DrawDice(dice4, diceValues[3]);
            DrawDice(dice5, diceValues[4]);
            DrawDice(dice6, diceValues[5]);
        }

        private void DrawDice(Canvas canvas, int value)
        {
            canvas.Children.Clear();

            // Positions for the dots on a dice face
            Point[][] dotPositions = new[]
            {
                new[] { new Point(30, 30) }, // 1
                new[] { new Point(10, 10), new Point(50, 50) }, // 2
                new[] { new Point(10, 10), new Point(30, 30), new Point(50, 50) }, // 3
                new[] { new Point(10, 10), new Point(10, 50), new Point(50, 10), new Point(50, 50) }, // 4
                new[] { new Point(10, 10), new Point(10, 50), new Point(30, 30), new Point(50, 10), new Point(50, 50) }, // 5
                new[] { new Point(10, 10), new Point(10, 30), new Point(10, 50), new Point(50, 10), new Point(50, 30), new Point(50, 50) } // 6
            };

            foreach (var pos in dotPositions[value - 1])
            {
                Ellipse dot = new Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Fill = Brushes.Black
                };
                Canvas.SetLeft(dot, pos.X - 5);
                Canvas.SetTop(dot, pos.Y - 5);
                canvas.Children.Add(dot);
            }
        }

        private void SaveDiceHistory(int[] diceValues)
        {
            rollCount++;
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dice_history.txt");
            string diceRoll = string.Join(", ", diceValues);
            string historyEntry = $"{rollCount}. {timestamp} - {diceRoll}";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(historyEntry);
            }
        }
    }
}
// Za jedna pane učiteli jak Nic :DD
// ale opravdu pls :D
//  !-- Tento kód byl sponzorován chagpt 4.o --!

