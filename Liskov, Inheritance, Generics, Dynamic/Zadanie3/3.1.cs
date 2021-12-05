using System;

namespace Zadanie3
{
    public abstract class Shape
    {
        public abstract int GetArea();
    }

    class Rectangle : Shape
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public override int GetArea()
        {
            return Width * Height;
        }
    }

    class Square : Shape
    {
        public int Side;

        public override int GetArea()
        {
            return Side * Side;
        }
    }
}