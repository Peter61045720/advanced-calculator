using MathNet.Numerics.LinearAlgebra;

namespace AdvancedCalculator.Models
{
    public class MatrixModel
    {
        public Matrix<double> Data { get; set; }

        public MatrixModel(int rows, int columns)
        {
            Data = Matrix<double>.Build.Dense(rows, columns);
        }

        public MatrixModel(Matrix<double> data)
        {
            Data = data;
        }
    }
}