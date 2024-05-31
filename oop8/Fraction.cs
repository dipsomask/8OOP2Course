using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace oop8
{
    /// <summary>
    /// Класс предоставляющий функционал для работы с дробями.
    /// </summary>
    internal class Fraction : IComparable, ICloneable, IFormattable
    {

        /// <summary>
        /// Числитель дроби
        /// </summary>
        public int Num {  get; set; }

        /// <summary>
        /// Знаменатель дроби
        /// </summary>
        public int Denum { get; set; }

        /// <summary>
        /// Целая часть дроби. По умолчанию = 0.
        /// </summary>
        public int IntPart { get; set; }

        /// <summary>
        /// Режим автоматического сокращения дроби. 
        /// </summary>
        public bool Cutting { get; set; }

        /// <summary>
        /// Идентификатор дроби.
        /// </summary>
        public int Id {  get; private set; }

        /// <summary>
        /// Статический идентификатор для запоминания значения последнего.
        /// </summary>
        private static int NextId = 1;

        /// <summary>
        /// Конструктор дроби по умолчанию: числитель = 0, знаменатель = 1, целая часть = 0, режим сокращения дроби = False.
        /// </summary>
        public Fraction()
        {
            Num = 0;
            Denum = 1;
            IntPart = 0;
            Cutting = false;
            Id = NextId++;
        }

        /// <summary>
        /// Конструктор дроби по переданным целой части, числителю и знаменателю. Исключение вызывается при denum = 0.
        /// </summary>
        /// <param name="intpart">Целая часть дроби</param>
        /// <param name="num">Числитель</param>
        /// <param name="denum">Знаменатель</param>
        /// <param name="cutting">Режим автоматического сокращения дроби</param>
        /// <exception cref="ArgumentException"></exception>
        public Fraction(int intpart, int num, int denum, bool cutting = false)
        {
            if(denum == 0)
            {
                throw new ArgumentException("Знаменатель не может быть равным 0...");
            }

            Id = NextId++;

            if(intpart < 0)
            {
                if (num < 0)
                {
                    if (denum < 0)
                    {
                        Num = -num;
                        Denum = -denum;
                        IntPart = intpart;
                    }
                    if (denum > 0)
                    {
                        Num = -num;
                        Denum = denum;
                        IntPart = intpart;
                    }
                }
                if (num > 0)
                {
                    if (denum < 0)
                    {
                        Num = num;
                        Denum = -denum;
                        IntPart = intpart;
                    }
                    if (denum > 0)
                    {
                        Num = num;
                        Denum = denum;
                        IntPart = intpart;
                    }
                }
            }

            if(intpart > 0)
            {
                if (num < 0)
                {
                    if (denum < 0)
                    {
                        Num = -num;
                        Denum = -denum;
                        IntPart = -intpart;
                    }
                    if (denum > 0)
                    {
                        Num = -num;
                        Denum = denum;
                        IntPart = -intpart;
                    }
                }
                if (num > 0)
                {
                    if (denum < 0)
                    {
                        Num = num;
                        Denum = -denum;
                        IntPart = -intpart;
                    }
                    if (denum > 0)
                    {
                        Num = num;
                        Denum = denum;
                        IntPart = intpart;
                    }
                }
            }

            if(intpart == 0)
            {
                IntPart = 0;

                if (num < 0)
                {
                    if (denum < 0)
                    {
                        Num = -num;
                        Denum = -denum;
                    }
                    if (denum > 0)
                    {
                        Num = num;
                        Denum = denum;
                    }
                }
                if (num > 0)
                {
                    if (denum < 0)
                    {
                        Num = -num;
                        Denum = -denum;
                    }
                    if (denum > 0)
                    {
                        Num = num;
                        Denum = denum;
                    }
                }
            }

            Cutting = cutting;

            Cut();
        }

        /// <summary>
        /// Конструктор дроби по переданному числителю (знаменатель = 1).
        /// </summary>
        /// <param name="value">Числитель</param>
        /// <param name="cutting">Режим сокращения дроби</param>
        public Fraction(int value, bool cutting = false)
        {
            Num = value;
            Denum = 1;
            IntPart = 0;
            Cutting = cutting;
            Id = NextId++;
        }

        /// <summary>
        /// Конструктор для копирования одной дроби в другую.
        /// </summary>
        /// <param name="other">Передаваемая дробь</param>
        public Fraction(Fraction other)
        {
            Num = other.Num;
            Denum = other.Denum;
            IntPart = other.IntPart;
            Cutting = other.Cutting;
            Id = NextId++;
        }

        /// <summary>
        /// Выделяет целую часть из дроби, если числитель больше знаменателя.
        /// </summary>
        /// <returns>Целая часть дроби (если числитель меньше знаменателя => 0).</returns>
        public int Decompose()
        {
            if(Num < Denum)
            {
                return 0;
            }

            IntPart = (Num - (Num % Denum))/Denum;

            Num %= Denum;

            return IntPart;
        }

        /// <summary>
        /// Сокращает дробь.
        /// </summary>
        /// <returns>Если Cutting = True => сокращённая дробь, иначе => исходная дробь.</returns>
        public Fraction Cut()
        {
            if(Cutting)
            {
                Num += IntPart * Denum;
                int SimplesNum = (int)Math.Sqrt(Num) + 1;
                int SimplesDenum = (int)Math.Sqrt(Denum) + 1;
                while (Num % Denum != 0 || SimplesNum > 1)
                {
                    while (Num % Denum != 0 || SimplesDenum > 1)
                    {
                        if (SimplesNum == SimplesDenum)
                        {
                            Num /= SimplesDenum;
                            Denum /= SimplesDenum;
                            SimplesDenum--;
                        }
                    }
                    SimplesNum--;
                }

                Decompose();
            }

            return this;
        }

        /// <summary>
        /// Складывает две дроби.
        /// </summary>
        /// <param name="arg1">Первая дробь</param>
        /// <param name="arg2">Вторая дробь</param>
        /// <returns>Результат суммы дробей.</returns>
        public static Fraction operator +(Fraction arg1, Fraction arg2)
        {
            int intpart = 0;
            int num = (arg1.Num + arg1.IntPart * arg1.Denum) * arg2.Denum + (arg2.Num + arg2.IntPart * arg2.Denum) * arg1.Denum;
            int denum = arg1.Denum * arg2.Denum;

            Fraction Result = new Fraction(intpart, num, denum, arg1.Cutting || arg2.Cutting);

            if(arg1.IntPart != 0 ||  arg2.IntPart != 0 )
            {
                Result.Decompose();
            }

            return Result;
        }

        /// <summary>
        /// Вычитает две дроби.
        /// </summary>
        /// <param name="arg1">Первая дробь</param>
        /// <param name="arg2">Вторая дробь</param>
        /// <returns>Результат вычитания дробей.</returns>
        public static Fraction operator -(Fraction arg1, Fraction arg2)
        {
            int intpart = 0;
            int num = (arg1.Num + arg1.IntPart* arg1.Denum) * arg2.Denum - (arg2.Num + arg2.IntPart * arg2.Denum) * arg1.Denum;
            int denum = arg1.Denum * arg2.Denum;

            Fraction Result = new Fraction(intpart, num, denum, arg1.Cutting || arg2.Cutting);

            if (arg1.IntPart != 0 || arg2.IntPart != 0)
            {
                Result.Decompose();
            }

            return Result;
        }

        /// <summary>
        /// Умножает две дроби.
        /// </summary>
        /// <param name="arg1">Первая дробь</param>
        /// <param name="arg2">Вторая дробь</param>
        /// <returns>Результат умножения дробей.</returns>
        public static Fraction operator *(Fraction arg1, Fraction arg2)
        {
            int num = ((arg1.IntPart * arg1.Denum) + arg1.Num) * ((arg2.IntPart * arg2.Denum) + arg2.Num);
            int denum = arg1.Denum * arg2.Denum;

            Fraction Result = new Fraction(0, num, denum, arg1.Cutting || arg2.Cutting);

            return Result;
        }

        /// <summary>
        /// Делит две дроби.
        /// </summary>
        /// <param name="arg1">Первая дробь</param>
        /// <param name="arg2">Вторая дробь</param>
        /// <returns>Результат деления дробей.</returns>
        public static Fraction operator /(Fraction arg1, Fraction arg2)
        {
            int intpart = 0;
            int num = (arg1.Num + arg1.IntPart * arg1.Denum) * (arg2.Denum + arg2.IntPart * arg2.Denum);
            int denum = arg1.Denum * arg2.Num;

            Fraction Result = new Fraction(intpart, num, denum, arg1.Cutting || arg2.Cutting);

            if (arg1.IntPart != 0 || arg2.IntPart != 0)
            {
                Result.Decompose();
            }

            return Result;
        }

        /// <summary>
        /// Приводит дробь к типу int.
        /// </summary>
        /// <param name="arg">Дробь</param>
        public static implicit operator int(Fraction arg)
        {
            if(arg.IntPart != 0)
            {
                return (arg.IntPart * arg.Denum + arg.Num) / arg.Denum;
            }

            return arg.Num / arg.Denum;
        }

        /// <summary>
        /// Приводит дробь к типу double.
        /// </summary>
        /// <param name="arg">Дробь</param>
        public static implicit operator double(Fraction arg)
        {
            if (arg.IntPart != 0)
            {
                return ((double)arg.IntPart * (double)arg.Denum + (double)arg.Num) / (double)arg.Denum;
            }

            return (double)arg.Num / (double)arg.Denum;
        }

        /// <summary>
        /// Приводит число типа int к дроби.
        /// </summary>
        /// <param name="arg">Число</param>
        public static implicit operator Fraction(int arg)
        {
            return new Fraction(arg);
        }

        /// <summary>
        /// Преобразует дробь в строку.
        /// </summary>
        /// <returns>Строковое представление дроби.</returns>
        public override string ToString()
        {
            string result = string.Empty;

            if(IntPart == 0)
            {
                return Num.ToString();
            }

            if(Num == 0)
            {
                return 0.ToString();
            }

            if (IntPart != 0)
            {
                result += IntPart.ToString();
            }

            result += $"({Num}/{Denum})";

            return result;

        }

        /// <summary>
        /// Интерфейс.
        /// Сравнивает дробь с переданным объектом. Выдаёт исключение если переданный объект не типа Fraction, int или double.
        /// </summary>
        /// <param name="obj">Должен быть дробью, числом типа int или double</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public int CompareTo(object obj)
        {
            if(obj is Fraction || obj is double)
            {
                double OtherObject = (double)obj;
                double ThisObject = (double)this;
                if (OtherObject > ThisObject)
                {
                    return 1;
                }
                else if(OtherObject == ThisObject)
                {
                    return 0;
                }

                return -1;
            }
            else if(obj is int argI)
            {
                int ThisObject = (int)this;
                if(argI > ThisObject)
                {
                    return 1;
                }
                else if(argI == ThisObject)
                {
                    return 0;
                }

                return -1;
            }
            else
            {
                throw new ArgumentException($"Переданный аргумент должен быть типа Fraction, int или double...{Id}");
            }
            
        }

        /// <summary>
        /// Интерфейс.
        /// Копирует дробь.
        /// </summary>
        /// <returns>Копия объекта.</returns>
        public object Clone()
        {
            Fraction Result = new Fraction(IntPart, Num, Denum, Cutting);

            return Result;
        }

        /// <summary>
        /// Вспомогательный метод интерфейса.
        /// </summary>
        /// <param name="format">Формат вывода</param>
        /// <returns></returns>
        public string ToString(string format)
        {

            return ToString(format, CultureInfo.CurrentCulture);

        }

        /// <summary>
        /// Интерфейс. Выводит строковое представление дроби в нужном формате.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public string ToString(string format, IFormatProvider formatProvider)
        {

            if (string.IsNullOrWhiteSpace(format))
            {

                format = "R";

            }

            if (formatProvider == null)
            {

                formatProvider = CultureInfo.CurrentCulture;

            }

            switch (format.ToUpperInvariant())
            {

                case "R": // стандартный вывод в строчку
                    return ToString();

                case "F": // вывод полей по оддельности
                    return string.Format(formatProvider,
                        "Поля объекта: Целая часть={0}; Числитель={1}; Знаменатель={2}, Идентификатор={3}", IntPart, Num, Denum, Id);

                default:
                    throw new FormatException($"Не поддерживается формат: '{format}'...{Id}");

            }

        }
    }
}
