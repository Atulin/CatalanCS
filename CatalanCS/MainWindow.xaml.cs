using System;
using System.Collections.Generic;
using System.Windows;
using System.Numerics;
using System.IO;
using System.Diagnostics;
using System.Windows.Input;

namespace CatalanCS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Init
        bool isKeyPressed = false;
        public MainWindow()
        {
            InitializeComponent();

            this.PreviewKeyDown += (s1, e1) => {
                if (e1.Key == Key.LeftCtrl)
                {
                    isKeyPressed = true;
                    Overlay.Visibility = Visibility.Visible;
                }
            };

            this.PreviewKeyUp += (s2, e2) => {
                if (e2.Key == Key.LeftCtrl)
                {
                    isKeyPressed = false;
                    Overlay.Visibility = Visibility.Collapsed;
                }
            };

            this.PreviewMouseLeftButtonDown += (s, e) => { if (isKeyPressed) DragMove(); };
        }

        // Handle calculate button click
        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            // Start stopwatch
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // Run code
            List<BigInteger> catalan = Catalan((int)Amount.Value);
            // Stop stopwatch
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            Output.Text = "";

            int counter = 1;

            foreach (var v in catalan)
            {
                Output.Text += counter + ": " + v.ToString() + Environment.NewLine;
                counter++;
            }

            Timer.Text = "Calculating " + (int)Amount.Value + " Catalan numbers took " + elapsedMs.ToString() + " ms";
        }

        // Handle save button click
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\output.txt";

            File.WriteAllText(path, Output.Text);

            Process.Start(path);
        }

        // Factorial method
        public BigInteger Factorial(int number)
        {
            if (number == 1)
                return 1;
            else
                return number * Factorial(number - 1);
        }

        // Catalan calculation
        public List<BigInteger> Catalan(int amount)
        {
            int n = amount;
            BigInteger CatalanN = 1;
            int i = 1;

            List<BigInteger> result = new List<BigInteger>();

            while (n-- > 0)
            {
                CatalanN = Factorial(2 * i) / (Factorial(i + 1) * Factorial(i));
                result.Add(CatalanN);
                i++;
            }

            return result;
        }

        // Drag window
        private void Drag(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        // Min/max window
        private void MinMax_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Minimized;
            else
                this.WindowState = WindowState.Normal;
        }

        // Close window
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
