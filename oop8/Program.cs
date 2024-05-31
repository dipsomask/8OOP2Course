using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop8
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Fraction
            Fraction a = new Fraction();

            Fraction b = new Fraction(0, -1, 2);

            Fraction c = new Fraction(0, 10, 3);

            Fraction d = new Fraction(0, -75, 1);

            Fraction e = new Fraction(0, -5, -7);

            Console.WriteLine($"\tДРОБИ:\nFraction a: {a}\nFraction b: {b}\nFraction c: {c}\nFraction d: {d}\nFraction e: {e}");

            Console.WriteLine($"a - b = {a - b}");

            Console.WriteLine($"c.Decompose(): {c.Decompose()}\nc = {c}");

            Console.WriteLine($"e * c = {e * c}");

            Console.WriteLine($"d / e = {d / e}");

            Console.WriteLine($"b + c = {b + c}");

            int int_b = b;
            double double_b = b;
            Fraction fraction_int_b = 3;
            Console.WriteLine($"(int)b = {int_b}");
            Console.WriteLine($"(double)b = {double_b}");
            Console.WriteLine($"(Fraction)int_b = {fraction_int_b}\n\n\n\tПОЛИНОМЫ:");


            // Polinom
            double[] mass1 = { 2, 2, 2, 2, 2 };

            double[] mass2 = { 1, 1 };

            double[] mass3 = { 2 };

            double[] mass4 = { 1, 1, 0, 1, 0, 0, 1 };

            Polinom A = new Polinom(4, 5, mass1);

            Polinom B = new Polinom(1, 2, mass2);

            Polinom C = new Polinom(0, 1, mass3);

            Polinom D = new Polinom(2, 1, mass3);

            Polinom E = new Polinom(6, 7, mass4, true);

            Console.WriteLine($"Polinom A: {A}\nPolinom B: {B}\nPolinom C: {C}\nPolinom D: {D}\nPolinom E: {E}");

            Console.WriteLine($"A + B = {A + B}");

            Console.WriteLine($"A - (B * C) = {A - (B * C)}");

            Console.WriteLine($"B * C = {B * C}");

            Console.WriteLine($"A % B = {A % B}");

            Console.WriteLine($"A / B = {A / B}");

            Console.WriteLine($"A[3]: {A[3]}");
            A[3] = 1000;
            Console.WriteLine($"A[3] = 1000 => A[3]: {A[3]}");


        }
    }
}
