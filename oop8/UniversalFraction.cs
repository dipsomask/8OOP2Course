using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace oop8
{

    /// <summary>
    /// Класс предоставляющий функционал для работы со сложными дробями, в которых могут присутствовать полиномы.
    /// </summary>
    //internal class UniversalFraction<type> where type : Complex
    //{
    //    /// <summary>
    //    /// Числитель сложной дроби.
    //    /// </summary>
    //    public type Num { get; set; }

    //    /// <summary>
    //    /// Знаменатель сложной дроби.
    //    /// </summary>
    //    public type Denum { get; set; }

    //    /// <summary>
    //    /// Целая часть сложной дроби. По умолчанию = 0.
    //    /// </summary>
    //    public type IntPart { get; set; }

    //    /// <summary>
    //    /// Режим автоматического сокращения сложной дроби. 
    //    /// </summary>
    //    public bool Cutting { get; set; }

    //    /// <summary>
    //    /// Идентификатор сложной дроби.
    //    /// </summary>
    //    public int Id { get; private set; }

    //    /// <summary>
    //    /// Статический идентификатор для запоминания значения последнего.
    //    /// </summary>
    //    private static int NextId = 1;

    //    /// <summary>
    //    /// Конструктор дроби по умолчанию: числитель = 0, знаменатель = 1, целая часть = 0, режим сокращения дроби = False.
    //    /// </summary>
    //    public UniversalFraction()
    //    {
    //        Num = 0;
    //        Denum = 1;
    //        IntPart = 0;
    //        Cutting = false;
    //        Id = NextId++;
    //    }

    //    /// <summary>
    //    /// Конструктор сложной дроби по переданным целой части, числителю и знаменателю. Исключение вызывается при denum = 0.
    //    /// </summary>
    //    /// <param name="intpart">Целая часть дроби</param>
    //    /// <param name="num">Числитель</param>
    //    /// <param name="denum">Знаменатель</param>
    //    /// <param name="cutting">Режим автоматического сокращения дроби</param>
    //    /// <exception cref="ArgumentException"></exception>
    //    public UniversalFraction(type intpart, type num, type denum, bool cutting = false)
    //    {
    //        if (denum == 0)
    //        {
    //            throw new ArgumentException("Знаменатель не может быть равным 0...");
    //        }

    //        Id = NextId++;

    //        if (intpart < 0)
    //        {
    //            if (num < 0)
    //            {
    //                if (denum < 0)
    //                {
    //                    Num = num * -1;
    //                    Denum = denum * -1;
    //                    IntPart = intpart;
    //                }
    //                if (denum > 0)
    //                {
    //                    Num = num * -1;
    //                    Denum = denum;
    //                    IntPart = intpart;
    //                }
    //            }
    //            if (num > 0)
    //            {
    //                if (denum < 0)
    //                {
    //                    Num = num;
    //                    Denum = denum * -1;
    //                    IntPart = intpart;
    //                }
    //                if (denum > 0)
    //                {
    //                    Num = num;
    //                    Denum = denum;
    //                    IntPart = intpart;
    //                }
    //            }
    //        }

    //        if (intpart > 0)
    //        {
    //            if (num < 0)
    //            {
    //                if (denum < 0)
    //                {
    //                    Num = num * -1;
    //                    Denum = denum * -1;
    //                    IntPart = intpart * -1;
    //                }
    //                if (denum > 0)
    //                {
    //                    Num = num * -1;
    //                    Denum = denum;
    //                    IntPart = intpart * -1;
    //                }
    //            }
    //            if (num > 0)
    //            {
    //                if (denum < 0)
    //                {
    //                    Num = num;
    //                    Denum = denum * -1;
    //                    IntPart = intpart * -1;
    //                }
    //                if (denum > 0)
    //                {
    //                    Num = num;
    //                    Denum = denum;
    //                    IntPart = intpart;
    //                }
    //            }
    //        }

    //        if (intpart == 0)
    //        {
    //            IntPart = 0;

    //            if (num < 0)
    //            {
    //                if (denum < 0)
    //                {
    //                    Num = num * -1;
    //                    Denum = denum * -1;
    //                }
    //                if (denum > 0)
    //                {
    //                    Num = num;
    //                    Denum = denum;
    //                }
    //            }
    //            if (num > 0)
    //            {
    //                if (denum < 0)
    //                {
    //                    Num = num * -1;
    //                    Denum = denum * -1;
    //                }
    //                if (denum > 0)
    //                {
    //                    Num = num;
    //                    Denum = denum;
    //                }
    //            }
    //        }

    //        Cutting = cutting;

    //        Cut();
    //    }

    //    /// <summary>
    //    /// Конструктор сложной дроби по переданному числителю (знаменатель = 1).
    //    /// </summary>
    //    /// <param name="value">Числитель</param>
    //    /// <param name="cutting">Режим сокращения дроби</param>
    //    public UniversalFraction(type value, bool cutting = false)
    //    {
    //        Num = value;
    //        Denum = 1;
    //        IntPart = 0;
    //        Cutting = cutting;
    //        Id = NextId++;
    //    }

    //    /// <summary>
    //    /// Конструктор для копирования одной дроби в другую.
    //    /// </summary>
    //    /// <param name="other">Передаваемая дробь</param>
    //    public UniversalFraction(UniversalFraction<type> other)
    //    {
    //        Num = other.Num;
    //        Denum = other.Denum;
    //        Id = NextId++;
    //    }

    //}

    internal abstract class Complex: ICloneable, IEnumerable, IComparable, IFormattable
    {
        public abstract object Clone();

        public abstract int CompareTo(object obj);

        public abstract IEnumerator GetEnumerator();

        public abstract override string ToString();

        public abstract string ToString(string format, IFormatProvider formatProvider);
    }

    internal class Int : Complex
    {

        public int Value {  get; set; }

        public Int() => Value = 0;

        public Int(int value)
        {
            Value = value;
        }

        public static implicit operator int(Int x) => x.Value;

        public static implicit operator double(Int x) => (double)x.Value;

        public static implicit operator Int(int x) => new Int(x);

        public static implicit operator Int(double x) => new Int((int)x);

        public override string ToString()
        {
            return Value.ToString();
        }

        public override object Clone()
        {
            Int CloneInt = new Int(Value);

            return CloneInt;
        }

        public override int CompareTo(object obj)
        {
            if(obj is Int number)
            {
                if(number.Value == Value)
                {
                    return 0;
                }
                else if(number.Value < Value)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                throw new ArgumentException("Сранить объекты не предоставляется возможным...");
            }
        }

        public override IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public string ToString(string format)
        {

            return ToString(format, CultureInfo.CurrentCulture);

        }

        public override string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrWhiteSpace(format))
            {

                format = "S";

            }

            if (formatProvider == null)
            {

                formatProvider = CultureInfo.CurrentCulture;

            }

            switch (format.ToUpperInvariant())
            {

                case "S": // стандартный вывод в строчку
                    return ToString();

                default:
                    throw new FormatException($"Не поддерживается формат: '{format}'...");

            }
        }
    }

    internal class Double : Complex
    {

        public double Value { get; set; }

        public Double() => Value = 0;

        public Double(double value)
        {
            Value = value;
        }

        public static implicit operator int(Double x) => (int)x.Value;

        public static implicit operator double(Double x) => x.Value;

        public static implicit operator Double(int x) => new Double((double)x);

        public static implicit operator Double(double x) => new Double(x);

        public override string ToString()
        {
            return Value.ToString();
        }

        public override object Clone()
        {
            Double CloneInt = new Double(Value);

            return CloneInt;
        }

        public override int CompareTo(object obj)
        {
            if (obj is Double number)
            {
                if (number.Value == Value)
                {
                    return 0;
                }
                else if (number.Value < Value)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                throw new ArgumentException("Сранить объекты не предоставляется возможным...");
            }
        }

        public override IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public string ToString(string format)
        {

            return ToString(format, CultureInfo.CurrentCulture);

        }

        public override string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrWhiteSpace(format))
            {

                format = "S";

            }

            if (formatProvider == null)
            {

                formatProvider = CultureInfo.CurrentCulture;

            }

            switch (format.ToUpperInvariant())
            {

                case "S": // стандартный вывод в строчку
                    return ToString();

                default:
                    throw new FormatException($"Не поддерживается формат: '{format}'...");

            }
        }
    }

}
