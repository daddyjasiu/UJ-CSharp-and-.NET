using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Microsoft.VisualBasic.CompilerServices;

namespace Zadanie3
{
    public class Matrix <T> where T : IComparable, IFormattable
    {
        private T[,] _matrix;
        private readonly int _rows;
        private readonly int _columns;

        public Matrix(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            _matrix = new T[_rows, _columns];
        }

        public void SetMatrix(T[,] numbers)
        {
            if (numbers.GetLength(0) != _rows || numbers.GetLength(1) != _columns)
                return;
            
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    _matrix[i, j] = numbers[i, j];
                }
            }
        }

        public T[,] AddMatrix(Matrix<T> matrix)
        {
            T[,] result = new T[_rows, _columns];
            
            if (matrix._rows != _rows || matrix._columns != _columns)
                return _matrix;

            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    dynamic a = _matrix[i, j];
                    dynamic b = matrix._matrix[i, j];
                    result[i, j] = a + b;
                }
            }
            return result;
        }

        public T[,] MultiplyMatrix(Matrix<T> matrix)
        {
            T[,] result = new T[_rows, _columns];
            
            if (matrix._rows != _columns)
                return _matrix;
            
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < matrix._columns; j++)
                {
                    for (int k = 0; k < matrix._rows; k++)
                    {
                        dynamic a = _matrix[i, k];
                        dynamic b = matrix._matrix[k, j];
                        result[i,j] += a * b;
                    }
                }
            }
            return result;
        }

        public void PrintMatrix()
        {
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    Console.Write(_matrix[i,j] + " ");
                }
                Console.WriteLine();
            }
        }
    }

    public class SquareMatrix <T> : Matrix<T> where T: IComparable, IFormattable
    {
        private readonly int _rows;
        private readonly int _columns;
        private T [,] _matrix;
        
        public SquareMatrix(int rows, int columns) : base(rows, columns)
        {
            _rows = columns;
            _columns = columns;
            _matrix = new T[_rows, _columns];
        }
        public void SetMatrix(T[,] numbers)
        {
            if (numbers.GetLength(0) != _rows || numbers.GetLength(1) != _columns)
                return;
            
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    _matrix[i, j] = numbers[i, j];
                }
            }
        }
        public bool IsDiagonal()
        {
            dynamic zero = 0;
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    if ((i != j) && (_matrix[i, j] != zero))
                        return false;
                }
            }
            return true;
        }
    }
}