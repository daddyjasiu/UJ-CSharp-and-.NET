using System;

namespace Zadanie3
{
    public class Complex<realType, imaginaryType> : IComparable, IFormattable
    {
        private realType _real;
        private imaginaryType _imaginary;

        public Complex(realType real, imaginaryType imaginary)
        {
            _real = real;
            _imaginary = imaginary;
        }

        public void SetRealNumber(realType real)
        {
            _real = real;
        }
        public void SetImaginaryNumber(imaginaryType imaginary)
        {
            _imaginary = imaginary;
        }
        public realType GetRealNumber()
        {
            return _real;
        }
        public imaginaryType GetImaginaryNumber()
        {
            return _imaginary;
        }

        public static Complex<realType, imaginaryType> operator +(Complex<realType, imaginaryType> a,
            Complex<realType, imaginaryType> b)
        {
            dynamic b_real = b._real;
            dynamic b_imaginary = b._imaginary;
            a._real += b_real;
            a._imaginary += b_imaginary;
            
            return a;
        }

        public static Complex<realType, imaginaryType> operator -(Complex<realType, imaginaryType> a,
            Complex<realType, imaginaryType> b)
        {
            dynamic b_real = b._real;
            dynamic b_imaginary = b._imaginary;
            a._real -= b_real;
            a._imaginary -= b_imaginary;
            
            return a;
        }
        public static Complex<realType, imaginaryType> operator *(Complex<realType, imaginaryType> a,
            Complex<realType, imaginaryType> b)
        {
            dynamic b_real = b._real;
            dynamic b_imaginary = b._imaginary;
            a._real *= b_real;
            a._imaginary *= b_imaginary;
            
            return a;
        }

        public int CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            throw new NotImplementedException();
        }
    }
}