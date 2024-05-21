using AdvancedCalculator.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MathNet.Numerics.LinearAlgebra;
using System.Text;

namespace AdvancedCalculator.ViewModels
{
    public partial class MatrixCalculatorViewModel : ObservableObject
    {
        [ObservableProperty]
        private MatrixModel _matrixA;

        [ObservableProperty]
        private MatrixModel _matrixB;

        [ObservableProperty]
        private string _matrixStringA;

        [ObservableProperty]
        private string _matrixStringB;

        [ObservableProperty]
        private string? _resultString;

        [ObservableProperty]
        private double? _scalarA;

        [ObservableProperty]
        private double? _scalarB;

        [ObservableProperty]
        private int? _powerA;

        [ObservableProperty]
        private int? _powerB;

        public MatrixCalculatorViewModel()
        {
            MatrixA = new MatrixModel(3, 3);
            MatrixB = new MatrixModel(3, 3);
            MatrixStringA = MatrixToString(MatrixA.Data);
            MatrixStringB = MatrixToString(MatrixB.Data);
        }

        [RelayCommand]
        private void AddMatrices()
        {
            UpdateMatrices();
            var result = MatrixA.Data + MatrixB.Data;
            ResultString = MatrixToString(result);
        }

        [RelayCommand]
        private void SubtractMatrices()
        {
            UpdateMatrices();
            var result = MatrixA.Data - MatrixB.Data;
            ResultString = MatrixToString(result);
        }

        [RelayCommand]
        private void MultiplyMatrices()
        {
            UpdateMatrices();
            var result = MatrixA.Data * MatrixB.Data;
            ResultString = MatrixToString(result);
        }

        [RelayCommand]
        private void PointwiseMultiplyMatrices()
        {
            UpdateMatrices();
            var result = MatrixA.Data.PointwiseMultiply(MatrixB.Data);
            ResultString = MatrixToString(result);
        }

        [RelayCommand]
        private void MultiplyByScalar(string name)
        {
            UpdateMatrix(name);

            if (name == nameof(MatrixA))
            {
                MatrixA.Data *= ScalarA ?? 0;
                MatrixStringA = MatrixToString(MatrixA.Data);
                ResultString = MatrixToString(MatrixA.Data);
            }
            else if (name == nameof(MatrixB))
            {
                MatrixB.Data *= ScalarB ?? 0;
                MatrixStringB = MatrixToString(MatrixB.Data);
                ResultString = MatrixToString(MatrixB.Data);
            }
        }

        [RelayCommand]
        private void RaiseToPower(string name)
        {
            UpdateMatrix(name);

            if (name == nameof(MatrixA))
            {
                MatrixA.Data = MatrixA.Data.Power(PowerA ?? 1);
                MatrixStringA = MatrixToString(MatrixA.Data);
                ResultString = MatrixToString(MatrixA.Data);
            }
            else if (name == nameof(MatrixB))
            {
                MatrixB.Data = MatrixB.Data.Power(PowerB ?? 1);
                MatrixStringB = MatrixToString(MatrixB.Data);
                ResultString = MatrixToString(MatrixB.Data);
            }
        }

        [RelayCommand]
        private void DecomposeLU(string name)
        {
            UpdateMatrix(name);

            if (name == nameof(MatrixA))
            {
                var decomposition = MatrixA.Data.LU();
                MatrixA.Data = decomposition.L;
                MatrixB.Data = decomposition.U;
            }
            else if (name == nameof(MatrixB))
            {
                var decomposition = MatrixB.Data.LU();
                MatrixA.Data = decomposition.L;
                MatrixB.Data = decomposition.U;
            }

            MatrixStringA = MatrixToString(MatrixA.Data);
            MatrixStringB = MatrixToString(MatrixB.Data);
            ResultString = "Matrix A = L, Matrix B = U";
        }

        [RelayCommand]
        private void DecomposeQR(string name)
        {
            UpdateMatrix(name);

            if (name == nameof(MatrixA))
            {
                var decomposition = MatrixA.Data.QR();
                MatrixA.Data = decomposition.Q;
                MatrixB.Data = decomposition.R;
            }
            else if (name == nameof(MatrixB))
            {
                var decomposition = MatrixB.Data.QR();
                MatrixA.Data = decomposition.Q;
                MatrixB.Data = decomposition.R;
            }

            MatrixStringA = MatrixToString(MatrixA.Data);
            MatrixStringB = MatrixToString(MatrixB.Data);
            ResultString = "Matrix A = Q, Matrix B = R";
        }

        [RelayCommand]
        private void Transpose(string name)
        {
            UpdateMatrix(name);

            if (name == nameof(MatrixA))
            {
                MatrixA = new MatrixModel(MatrixA.Data.Transpose());
                MatrixStringA = MatrixToString(MatrixA.Data);
                ResultString = MatrixToString(MatrixA.Data);
            }
            else if (name == nameof(MatrixB))
            {
                MatrixB = new MatrixModel(MatrixB.Data.Transpose());
                MatrixStringB = MatrixToString(MatrixB.Data);
                ResultString = MatrixToString(MatrixB.Data);
            }
        }

        [RelayCommand]
        private void CalculateInverse(string name)
        {
            UpdateMatrix(name);

            if (name == nameof(MatrixA))
            {
                MatrixA = new MatrixModel(MatrixA.Data.Inverse());
                MatrixStringA = MatrixToString(MatrixA.Data);
                ResultString = MatrixToString(MatrixA.Data);
            }
            else if (name == nameof(MatrixB))
            {
                MatrixB = new MatrixModel(MatrixB.Data.Inverse());
                MatrixStringB = MatrixToString(MatrixB.Data);
                ResultString = MatrixToString(MatrixB.Data);
            }
        }

        [RelayCommand]
        private void CalculateDeterminant(string name)
        {
            UpdateMatrix(name);

            if (name == nameof(MatrixA))
            {
                ResultString = MatrixA.Data.Determinant().ToString();
            }
            else if (name == nameof(MatrixB))
            {
                ResultString = MatrixB.Data.Determinant().ToString();
            }
        }

        [RelayCommand]
        private void CalculateEigenvalues(string name)
        {
            UpdateMatrix(name);

            if (name == nameof(MatrixA))
            {
                ResultString = MatrixToString(Matrix<double>.Build.DenseOfRowVectors(MatrixA.Data.Evd().EigenValues.Real()));
            }
            else if (name == nameof(MatrixB))
            {
                ResultString = MatrixToString(Matrix<double>.Build.DenseOfRowVectors(MatrixB.Data.Evd().EigenValues.Real()));
            }
        }

        [RelayCommand]
        private void Clear()
        {
            MatrixA = new MatrixModel(3, 3);
            MatrixB = new MatrixModel(3, 3);
            MatrixStringA = MatrixToString(MatrixA.Data);
            MatrixStringB = MatrixToString(MatrixB.Data);
            ResultString = null;
            ScalarA = null;
            ScalarB = null;
            PowerA = null;
            PowerB = null;
        }

        [RelayCommand]
        private void Swap()
        {
            (MatrixA, MatrixB, MatrixStringA, MatrixStringB) = (MatrixB, MatrixA, MatrixStringB, MatrixStringA);
        }

        private void UpdateMatrix(string name)
        {
            if (name == nameof(MatrixA))
            {
                MatrixA = new MatrixModel(StringToMatrix(MatrixStringA));
            }
            else if (name == nameof(MatrixB))
            {
                MatrixB = new MatrixModel(StringToMatrix(MatrixStringB));
            }
        }

        private void UpdateMatrices()
        {
            MatrixA = new MatrixModel(StringToMatrix(MatrixStringA));
            MatrixB = new MatrixModel(StringToMatrix(MatrixStringB));
        }

        private static string MatrixToString(Matrix<double> matrix)
        {
            var rows = matrix.RowCount;
            var cols = matrix.ColumnCount;
            var result = new StringBuilder();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result.Append(Math.Round(matrix[i, j], 2));

                    if (j < cols - 1)
                    {
                        result.Append(" ");
                    }
                }

                if (i < rows - 1)
                {
                    result.AppendLine();
                }
            }

            return result.ToString();
        }

        private static Matrix<double> StringToMatrix(string input)
        {
            var rows = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var rowCount = rows.Length;
            var colCount = rows[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
            var matrix = Matrix<double>.Build.Dense(rowCount, colCount);

            for (int i = 0; i < rowCount; i++)
            {
                var cols = rows[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < colCount; j++)
                {
                    matrix[i, j] = Convert.ToDouble(cols[j]);
                }
            }

            return matrix;
        }
    }
}