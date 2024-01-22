using System.Windows;

namespace AdvancedCalculator.Models
{
    public class BasicCalculatorModel
    {
        public double Operand1 { get; set; }
        public double Operand2 { get; set; }
        public string? Operation { get; set; }

        public double Calculate()
        {
            switch (Operation)
            {
                case "+":
                    return Operand1 + Operand2;

                case "-":
                    return Operand1 - Operand2;

                case "×":
                    return Operand1 * Operand2;

                case "÷":
                    if (Operand2 == 0)
                    {
                        MessageBox.Show("Error: you can't divide by 0!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return 0;
                    }
                    return Operand1 / Operand2;

                default:
                    return 0;
            }
        }

        public double CalculateUnary(string operation, double number)
        {
            switch (operation)
            {
                case "%":
                    return number / 100;

                case "pow2":
                    return Math.Pow(number, 2);

                case "sqrt":
                    return Math.Sqrt(number);

                case "reciprocal":
                    if (number == 0)
                    {
                        MessageBox.Show("Error: you can't divide by 0!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return 0;
                    }
                    return 1 / number;

                default:
                    return 0;
            }
        }

        public override string ToString()
        {
            return $"{Operand1}{Operation}{Operand2}";
        }
    }
}