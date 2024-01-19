using AdvancedCalculator.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace AdvancedCalculator.ViewModels
{
    public partial class BasicCalculatorViewModel : ObservableObject
    {
        private BasicCalculatorModel basicCalculator = new();

        [ObservableProperty]
        private double _operand1;

        [ObservableProperty]
        private double _operand2;

        [ObservableProperty]
        private string? _operation;

        [ObservableProperty]
        private double _result;

        [ObservableProperty]
        private string? _display;

        public BasicCalculatorViewModel()
        {
            Display = Result.ToString();
        }

        partial void OnOperand1Changed(double value)
        {
            this.basicCalculator.Operand1 = value;
        }

        partial void OnOperand2Changed(double value)
        {
            this.basicCalculator.Operand2 = value;
        }

        partial void OnOperationChanged(string? value)
        {
            this.basicCalculator.Operation = value;
        }

        partial void OnResultChanged(double value)
        {
            Result = Math.Round(Result, 14);
        }

        [RelayCommand]
        public void OnNumberPressed(string number)
        {
            if (string.IsNullOrEmpty(Operation))
            {
                Display += number;
                Operand1 = Convert.ToDouble(Display);
                Display = Operand1.ToString();
            }
            else
            {
                if (Operand2 == 0)
                {
                    Display = "";
                }
                //? Display = Operand2.ToString();
                Display += number;
                Operand2 = Convert.ToDouble(Display);
                Display = Operand2.ToString();
            }
        }

        [RelayCommand]
        public void OnDecimalPointPressed()
        {
            if (!Display!.Contains(','))
            {
                Display += ",";
            } 
        }

        [RelayCommand]
        public void OnOperationPressed(string operation)
        {
            Operation = operation;
        }

        [RelayCommand]
        public void OnUnaryOperatorPressed(string operation)
        {
            if (string.IsNullOrEmpty(Operation) || Operand2 == 0)
            {
                Result = basicCalculator.CalculateUnary(operation, Operand1);
                Operand1 = Result;
            }
            else
            {
                Result = basicCalculator.CalculateUnary(operation, Operand2);
                Operand2 = Result;
            }
            //? Result = basicCalculator.CalculateUnary(operation);
            //? Operand1 = Result;
            Display = Result.ToString();
        }

        [RelayCommand]
        public void OnChangeSingPressed()
        {
            if (string.IsNullOrEmpty(Operation) || Operand2 == 0)
            {
                Operand1 = -Operand1;
                Display = Operand1.ToString();
            }
            else
            {
                Operand2 = -Operand2;
                Display = Operand2.ToString();
            }
        }

        [RelayCommand]
        public void OnEqualsPressed()
        {
            Result = basicCalculator.Calculate();
            Display = Result.ToString();
            MessageBox.Show($"op1: {Operand1}\nop2: {Operand2}\noperation: {Operation}\nresult: {Result}");
            Operand1 = Result;
            Operand2 = 0;
        }

        [RelayCommand]
        public void OnClearPressed()
        {
            Operand1 = 0;
            Operand2 = 0;
            Operation = "";
            Display = "0";
        }
    }
}