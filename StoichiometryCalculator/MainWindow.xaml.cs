using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace StoichiometryCalculator
{
    public partial class MainWindow : Window
    {
        private readonly string[] titles = { "Mass (in Grams): ", "Molar Mass 1: ", "Molar Mass 2: ", "Coefficient 1: ", "Coefficient 2: " },
            outputTitles = { "Moles", "Mass", "Particles"},
            units = { "mol", "g", " particles" },
            formatting = { "{0:.0000}", "{0:.0000}", "{0:#.####E0}"};

        private readonly SolidColorBrush textColor = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
            primaryBackground = new SolidColorBrush(Color.FromRgb(30, 30, 30)),
            secondaryBackground = new SolidColorBrush(Color.FromRgb(45, 45, 45));
        public MainWindow()
        {
            InitializeComponent();

            RootGrid.Background = primaryBackground;

            MainGridInitializer(titles);

            button.Foreground = textColor;
            button.Background = secondaryBackground;

            output.Foreground = textColor;
        }

        public void MainGridInitializer(string[] input, int index = 0)
        {
            if (index == input.Length)
            {
                return;
            }
            if (RootGrid.Children[index] is Label)
            {
                Label label = RootGrid.Children[index] as Label;
                label.Content = input[index];
                label.Foreground = textColor;
            }
            index++;
            MainGridInitializer(input, index);
        }

        public double[] GetTextboxContent()
        {
            double[] output = new double[5];
            int j = 0;

            for(int i = 0; i < RootGrid.Children.Count; i++)
            {
                if (RootGrid.Children[i] is TextBox)
                {
                    string text = (RootGrid.Children[i] as TextBox).Text;
                    bool isSuccessful = double.TryParse(text, out double parse);
                    if (isSuccessful)
                    {
                        output[j] = parse;
                        j++;
                    }
                }
            }
            return output;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            double[] values = GetTextboxContent();
            string text = "";

            if(values.Length == 5)
            {
                Calculations.mass = values[0];
                Calculations.molarMasses = new double[2] { values[1], values[2] };
                Calculations.coefficients = new double[2] { values[3], values[4] };

                double[] outputs = Calculations.Calculate();

                for (int i = 0; i < outputs.Length; i++)
                {
                    string formattedNum = string.Format(formatting[i], outputs[i]);
                    text += $"{outputTitles[i]}: {formattedNum}{units[i]}\n";
                }
            }
            output.Content = text;
        }
    }
}