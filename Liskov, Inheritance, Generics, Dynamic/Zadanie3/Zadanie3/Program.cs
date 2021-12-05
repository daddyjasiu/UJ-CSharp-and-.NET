using System;
using System.Collections;
using System.Linq;

namespace Zadanie3
{
    public class Program
    {
        static void Method(Rectangle r)
        {
            r.Width = 5;
            r.Height = 10;
            int area = r.GetArea();
            Console.WriteLine(area);
        }

        static void Main(string[] args)
        {
            // Method(new Rectangle());
            Queue1 q1 = new Queue1();
            
            q1.Enqueue(1);
            q1.Enqueue(2);
            q1.Enqueue(3);
            q1.Dequeue();

            Queue2 q2 = new Queue2();
            q2.Enqueue(10);
            q2.Enqueue(20);
            q2.Enqueue(30);
            q2.Dequeue();
            
            var a = new Complex<int, int>(5, 5);
            Complex<int, int>[,] array1 = {{a,a,a}, {a,a,a}, {a,a,a}};
            int[,] array2 = {{1,1,1}, {1,1,1}, {1,1,1}};
            
            Matrix<Complex<int, int>> matrix1 = new Matrix<Complex<int, int>>(3, 3);
            Matrix<Complex<int, int>> matrix2 = new Matrix<Complex<int, int>>(3, 3);
            Matrix<int> matrix3 = new Matrix<int>(3, 3);
            Matrix<int> matrix4 = new Matrix<int>(3, 3);
            
            matrix1.SetMatrix(array1);
            matrix2.SetMatrix(array1);
            matrix3.SetMatrix(new [,] {{1,1,1}, {1,1,1}, {1,1,1}});
            matrix4.SetMatrix(new [,] {{1,1,1}, {1,1,1}, {1,1,1}});
            array1 = matrix1.AddMatrix(matrix2);
            
            for (int i = 0; i < array2.GetLength(0); i++)
            {
                for (int j = 0; j < array2.GetLength(1); j++)
                {
                    Console.Write(array2[i,j] + "," + array2[i,j] + " ");
                }
                Console.WriteLine();
            }
            
            Console.WriteLine();
            
            for (int i = 0; i < array1.GetLength(0); i++)
            {
                for (int j = 0; j < array1.GetLength(1); j++)
                {
                    Console.Write(array1[i,j].GetRealNumber() + "," + array1[i,j].GetImaginaryNumber() + " ");
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}