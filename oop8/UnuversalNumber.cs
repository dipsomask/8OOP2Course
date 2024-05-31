using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop8
{
    //internal class UnuversalNumber<type> where type: Complex
    //{

    //}

    internal abstract class Complex : ICloneable, IEnumerable, IComparable, IFormattable
    {

        protected Int int_ {  get; set; }

        protected Double double_ { get; set; }

        protected Complex()
        {
            int_ = 0;

            double_ = 0.0;
        }

        public abstract object Clone();

        public abstract int CompareTo(object obj);

        public abstract IEnumerator GetEnumerator();

        public abstract override string ToString();

        public abstract string ToString(string format, IFormatProvider formatProvider);
    }

    internal class Int : Complex
    {

        public int Value { get; set; }

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
            if (obj is Int number)
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
