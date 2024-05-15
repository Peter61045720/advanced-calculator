using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Numerics;

namespace AdvancedCalculator.ViewModels
{
    public partial class ComplexCalculatorViewModel : ObservableObject
    {
        private Complex _leftOperand;
        private Complex _rightOperand;

        [ObservableProperty]
        private Complex _result;

        [ObservableProperty]
        private double? _leftOperandReal;

        [ObservableProperty]
        private double? _leftOperandImag;

        [ObservableProperty]
        private double? _rightOperandReal;

        [ObservableProperty]
        private double? _rightOperandImag;

        public ComplexCalculatorViewModel()
        {
            _leftOperand = Complex.Zero;
            _rightOperand = Complex.Zero;
            Result = Complex.Zero;
        }

        [RelayCommand]
        public void Add()
        {
            Update();
            Result = _leftOperand + _rightOperand;
        }

        [RelayCommand]
        public void Subtract()
        {
            Update();
            Result = _leftOperand - _rightOperand;
        }

        [RelayCommand]
        public void Multiply()
        {
            Update();
            Result = _leftOperand * _rightOperand;
        }

        [RelayCommand]
        public void Divide()
        {
            Update();
            Result = _leftOperand / _rightOperand;
        }

        [RelayCommand]
        public void Abs(string buttonId)
        {
            Update();
            if (buttonId == "z1")
            {
                Result = Complex.Abs(_leftOperand);
                LeftOperandReal = Result.Real;
                LeftOperandImag = Result.Imaginary;
            }
            else if (buttonId == "z2")
            {
                Result = Complex.Abs(_rightOperand);
                RightOperandReal = Result.Real;
                RightOperandImag = Result.Imaginary;
            }
        }

        [RelayCommand]
        public void Conjugate(string buttonId)
        {
            Update();
            if (buttonId == "z1")
            {
                Result = Complex.Conjugate(_leftOperand);
                LeftOperandReal = Result.Real;
                LeftOperandImag = Result.Imaginary;
            }
            else if (buttonId == "z2")
            {
                Result = Complex.Conjugate(_rightOperand);
                RightOperandReal = Result.Real;
                RightOperandImag = Result.Imaginary;
            }
        }

        [RelayCommand]
        public void Clear()
        {
            _leftOperand = Complex.Zero;
            _rightOperand = Complex.Zero;
            Result = Complex.Zero;
            LeftOperandReal = null;
            LeftOperandImag = null;
            RightOperandReal = null;
            RightOperandImag = null;
        }

        [RelayCommand]
        public void Swap()
        {
            (LeftOperandReal, RightOperandReal) = (RightOperandReal, LeftOperandReal);
            (LeftOperandImag, RightOperandImag) = (RightOperandImag, LeftOperandImag);
        }

        public void Update()
        {
            _leftOperand = new Complex(LeftOperandReal ?? 0, LeftOperandImag ?? 0);
            _rightOperand = new Complex(RightOperandReal ?? 0, RightOperandImag ?? 0);
        }
    }
}