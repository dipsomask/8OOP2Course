using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace oop8
{
    /// <summary>
    /// Вспомогательный класс, являющийся полем полинома. Представляет элемент полинома.
    /// </summary>
    class PolinomMember: IComparable
    {

        /// <summary>
        /// Степень члена полинома.
        /// </summary>
        public int Power {  get; set; }

        /// <summary>
        /// Значение члена полинома.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Следующий член полинома.
        /// </summary>
        public PolinomMember Next { get; set; }

        /// <summary>
        /// Предыдущий член полинома.
        /// </summary>
        public PolinomMember Before { get; set; }

        /// <summary>
        /// Конструктор члена полинома с параметрами степени и значения.
        /// </summary>
        /// <param name="power_">Степень члена полинома</param>
        /// <param name="value_">Значение члена полинома</param>
        public PolinomMember(int power_ = 0, double value_ = 0.0)
        {
            Power = power_;

            Value = value_;
        }

        /// <summary>
        /// Создаёт строковое представление члена полинома.
        /// </summary>
        /// <returns>Строковое представление члена полинома.</returns>
        public override string ToString()
        {
            string member = Math.Abs(Value).ToString();

            if (Power > 0)
            {

                member += "x";

            }
            if (Power > 1)
            {

                member += "^" + Power.ToString();

            }

            return member;
        }

        /// <summary>
        /// Сравнивает объект PolinomMember с переданныи объектом. (Выдаёт исключение ArgumentException если переданный объект не PolinomMember)
        /// </summary>
        /// <param name="obj">PolinomMember</param>
        /// <returns>0 если объекты равны, 1 если переданный объект больше текущего, -1 если переданный объект меньше текущего.</returns>
        /// <exception cref="ArgumentException"></exception>
        public int CompareTo(object obj)
        {
            if(obj is PolinomMember pm)
            {
                if(Power == pm.Power && Value == pm.Value)
                {
                    return 0;
                }
                else if(pm.Power > Power)
                {
                    return 1;
                }
                else if(pm.Power < Power)
                {
                    return -1;
                }
                else
                {
                    throw new ArgumentException("Сранить объекты не предоставляется возможным...");
                }
            }
            else
            {
                throw new ArgumentException("Сранить объекты не предоставляется возможным...");
            }
        }
    };

    /// <summary>
    /// Класс предоставляющий функционал для работы с полиномами.
    /// </summary>
    internal class Polinom: Complex
    {

        /// <summary>
        /// Первый член полинома.
        /// </summary>
        public PolinomMember Begin {  get; set; }

        /// <summary>
        /// Последний член полинома.
        /// </summary>
        public PolinomMember End { get; set; }

        /// <summary>
        /// Степень полинома.
        /// </summary>
        public int Degree { get; set; }

        /// <summary>
        /// Размер полинома.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Режим автосокращения полинома.
        /// </summary>
        public bool Cutting { get; set; }

        /// <summary>
        /// Идентификатор полинома.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Статический идентификатор для запоминания значения последнего.
        /// </summary>
        private static int NextId = 1;

        /// <summary>
        /// Помещает элемент типа PolinomMember в конец полинома.
        /// </summary>
        /// <param name="member">Элемент типа PolinomMember</param>
        /// <returns>Полином с добавленным элементом в конце.</returns>
        public Polinom PushBackMember(PolinomMember member)
        {

            if (End.Value == 0)
            {

                End.Value = member.Value;

                End.Power = member.Power;

                if (Degree < End.Power)
                {

                    Degree = End.Power;

                }

                return this;

            }

            End.Next = member;

            member.Before = End;

            End = member;

            if (Degree < End.Power)
            {

                Degree = End.Power;

            }

            Size++;

            return this;

        }

        /// <summary>
        /// Конструктор полинома без параметров/с режимом автосокращения.
        /// </summary>
        /// <param name="cutting">Режим автосокращения</param>
        public Polinom(bool cutting = false)
        {

            Id = NextId++;

            PolinomMember Elem = new PolinomMember();

            Begin = Elem;

            End = Elem;

            Elem.Before = Begin;

            Elem = End;

            Degree = 0;

            Size = 1;

            Cutting = cutting;
        }

        /// <summary>
        /// Конструктор полинома по переданным степени, размеру, элементами и режиму автосокращения.
        /// </summary>
        /// <param name="degree_">Степень</param>
        /// <param name="size_">Размер</param>
        /// <param name="mass_">Массив элементов</param>
        /// <param name="cutting">Режим автосокращения</param>
        /// <exception cref="ArgumentException"></exception>
        public Polinom(int degree_, int size_, double[] mass_, bool cutting = false)
        {

            if (size_ <= 0 || degree_ < 0)
            {

                throw new ArgumentException("Неверный размер или степень полинома...");

            }

            Cutting = cutting;

            Id = NextId++;

            Degree = degree_;

            Size = size_;

            PolinomMember Current;

            PolinomMember NewElem = new PolinomMember(degree_, mass_[Size - 1]);

            Begin = NewElem;

            Begin.Next = End;

            End = NewElem;

            End.Before = Begin;

            Current = NewElem;

            degree_--;

            for (int index = Size - 2; index >= 0; index--)
            {

                PolinomMember Temp = new PolinomMember(degree_, mass_[index]);

                Current.Before = Temp;

                Temp.Next = Current;

                Begin = Temp;

                Current = Temp;

                degree_--;

            }

            if(Cutting)
            {
                RemoveZeros();
            }

        }

        /// <summary>
        /// Конструктор копирования полинома.
        /// </summary>
        /// <param name="other">Другой полином.</param>
        public Polinom(Polinom other)
        {

            Id = NextId++;

            int degree = other.Degree;

            Degree = other.Degree;

            Size = other.Size;

            Cutting = other.Cutting;

            PolinomMember Current;

            PolinomMember otherCurrent = other.End;

            PolinomMember NewElem = new PolinomMember(degree, otherCurrent.Value);

            Begin = NewElem;

            Begin.Next = End;

            End = NewElem;

            End.Before = Begin;

            Current = NewElem;

            otherCurrent = otherCurrent.Before;

            degree--;

            for (int index = Size - 2; index >= 0; index--)
            {

                PolinomMember Temp = new PolinomMember(degree, otherCurrent.Value);

                Current.Before = Temp;

                Temp.Next = Current;

                Begin = Temp;

                Current = Temp;

                otherCurrent = otherCurrent.Before;

                degree--;

            }

            if (Cutting)
            {
                RemoveZeros();
            }

        }

        /// <summary>
        /// Удаляет нулевые члены полинома.
        /// </summary>
        /// <returns>Полином без нулевых членов.</returns>
        public Polinom RemoveZeros()
        {

            if (Begin.Next == End)
            {

                return this;

            }

            PolinomMember Current = Begin;

            PolinomMember Next = Current.Next;

            while (Current != null)
            {

                if (Current.Value == 0.0)
                {

                    Current.Power = 0;

                    Size--;

                    Degree = End.Power;

                    if (Current == Begin)
                    {

                        Current = Current.Next;

                        Current.Before.Next = null;

                        Current.Before = null;

                        Begin = Current;

                        Next = Current;

                    }

                    if (Current == End)
                    {

                        if (Current.Before != null)
                        {

                            Current = Current.Before;

                            Current.Next.Before = null;

                            Current.Next = null;

                            End = Current;

                        }

                        Current.Power = Degree;

                        return this;

                    }

                    if (Current != End && Current != Begin)
                    {

                        PolinomMember Before = Current.Before;

                        Next = Current.Next;

                        Current.Next = null;

                        Current.Before = null;

                        Before.Next = Next;

                        Next.Before = Before;

                    }

                }

                Current = Next;

                if (Current != null)
                {

                    Next = Current.Next;

                }

            }

            return this;

        }


        /// <summary>
        /// Получение и/или изменение члена полинома.
        /// </summary>
        /// <param name="index">Индекс члена полинома</param>
        /// <returns>Значение члена полинома.</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public double this[int index]
        {
            get
            {

                if (index >= Size || index < 0)
                {

                    throw new IndexOutOfRangeException($"Index out of range. Object id: {Id}.");

                }

                int count = 0;
                double temp = 0;
                foreach(PolinomMember Member in this)
                {
                    if(count == index)
                    {
                        temp =  Member.Value;
                    }
                    count++;
                }

                return temp;

            }

            set
            {

                if (index >= Size || index < 0)
                {

                    throw new IndexOutOfRangeException($"Index out of range. Object id: {Id}.");

                }

                int count = 0;

                foreach (PolinomMember Member in this)
                {
                    if (count == index)
                    {
                        Member.Value = value;
                    }
                    count++;
                }

            }
        }



        public static implicit operator Polinom(int x)
        {
            double[] mass = { (double)x };

            return new Polinom(0, 1, mass); 
        }

        public static implicit operator Polinom(double x)
        {
            double[] mass = { x };

            return new Polinom(0, 1, mass);
        }

        public static implicit operator Polinom(Fraction x)
        {
            double[] mass = { x };

            return new Polinom(0, 1, mass);
        }



        /// <summary>
        /// Суммирует один полином с другим.
        /// </summary>
        /// <param name="p1">Полином 1</param>
        /// <param name="p2">Полином 2</param>
        /// <returns>Полином - результат суммы аргументов.</returns>
        public static Polinom operator +(Polinom p1, Polinom p2)
        {
            bool SaveCutting = p1.Cutting;
            p1.Cutting = p1.Cutting && p2.Cutting;

            Polinom Result = new Polinom(p1);

            p1.Cutting = SaveCutting;

            if (Result.Degree == 0)
            {

                Result = p2;

                return Result;

            }

            PolinomMember Current1 = Result.Begin;

            PolinomMember Current2 = p2.Begin;

            bool flag = false;

            while (Current2 != null)
            {

                while (Current1 != null)
                {

                    if (Current2.Power == Current1.Power)
                    {

                        Current1.Value = Current1.Value + Current2.Value;

                        flag = true;

                    }

                    Current1 = Current1.Next;

                }

                if (!flag)
                {

                    PolinomMember Current3 = Result.Begin;

                    PolinomMember newMember = new PolinomMember(Current2.Power, Current2.Value);

                    if (Current2.Power > Result.Degree)
                    {

                        newMember.Before = Result.End.Before;

                        Result.End.Next = newMember;

                        Result.End = newMember;

                    }

                    if (Current2.Power <= Result.Degree)
                    {

                        while (Current3 != null)
                        {

                            if (Current3.Power < newMember.Power && Current3.Next.Power > newMember.Power)
                            {

                                newMember.Before = Current3;

                                newMember.Next = Current3.Next;

                                Current3.Next.Before = newMember;

                                Current3.Next = newMember;

                            }

                            Current3 = Current3.Next;

                        }

                    }

                    Result.Size++;

                    Result.Degree = p2.Degree;

                }

                Current1 = Result.Begin;

                flag = !flag;

                Current2 = Current2.Next;

            }

            if (Result.Degree < p2.Degree)
            {

                Result.Degree = p2.Degree;

            }

            if (Result.Cutting)
            {
                Result.RemoveZeros();
            }

            return Result;
        }

        /// <summary>
        /// Вычитает из одного полинома другой.
        /// </summary>
        /// <param name="p1">Полином 1</param>
        /// <param name="p2">Полином 2</param>
        /// <returns>Полином - результат вычитания аргументов.</returns>
        public static Polinom operator -(Polinom p1, Polinom p2)
        {
            bool SaveCutting = p1.Cutting;
            p1.Cutting = p1.Cutting && p2.Cutting;

            Polinom Result = new Polinom(p1);

            p1.Cutting = SaveCutting;

            if (Result.Degree == 0)
            {

                Result = p2;

                PolinomMember Current = Result.Begin;

                while (Current != null)
                {

                    Current.Value = -Current.Value;

                    Current = Current.Next;

                }

                if (Result.Cutting)
                {
                    Result.RemoveZeros();
                }

                return Result;

            }

            PolinomMember Current1 = Result.Begin;

            PolinomMember Current2 = p2.Begin;

            bool flag = false;

            while (Current2 != null)
            {

                while (Current1 != null)
                {

                    if (Current2.Power == Current1.Power)
                    {

                        Current1.Value = Current1.Value - Current2.Value;

                        flag = true;

                    }

                    Current1 = Current1.Next;

                }

                if (!flag)
                {

                    PolinomMember Current3 = Result.Begin;

                    PolinomMember newMember = new PolinomMember(Current2.Power, -Current2.Value);

                    if (Current2.Power > Result.Degree)
                    {

                        newMember.Before = Result.End.Before;

                        Result.End.Next = newMember;

                        Result.End = newMember;

                    }

                    if (Current2.Power <= Result.Degree)
                    {

                        while (Current3 != null)
                        {

                            if (Current3.Power < newMember.Power && Current3.Next.Power > newMember.Power)
                            {

                                newMember.Before = Current3;

                                newMember.Next = Current3.Next;

                                Current3.Next.Before = newMember;

                                Current3.Next = newMember;

                            }

                            Current3 = Current3.Next;

                        }

                    }

                    Result.Size++;

                    Result.Degree = p2.Degree;

                }

                Current1 = Result.Begin;

                flag = !flag;

                Current2 = Current2.Next;

            }

            if (Result.Degree < p2.Degree)
            {

                Result.Degree = p2.Degree;

            }

            if(Result.Cutting)
            {
                Result.RemoveZeros();
            }

            return Result;
        }

        /// <summary>
        /// Умножает два полинома.
        /// </summary>
        /// <param name="p1">Полином 1</param>
        /// <param name="p2">Полином 2</param>
        /// <returns>Полином - результат умножения аргументов.</returns>
        public static Polinom operator *(Polinom p1, Polinom p2)
        {
            bool SaveCutting = p1.Cutting;
            p1.Cutting = p1.Cutting && p2.Cutting;

            Polinom Result = new Polinom(p1);

            p1.Cutting = SaveCutting;

            if (Result.Degree == 0 && Result.Begin.Value == 0.0)
            {

                return Result;

            }

            if (p2.Degree == 0 && p2.Begin.Value == 0.0)
            {

                Result = p2;

                if(Result.Cutting)
                {
                    Result.RemoveZeros();
                }

                return Result;

            }

            Polinom saveThis = new Polinom(Result);

            Polinom temp = new Polinom(saveThis);

            PolinomMember Current1 = temp.Begin;

            PolinomMember Current2 = p2.Begin;

            while (Current2 != null)
            {

                while (Current1 != null)
                {

                    Current1.Power = Current1.Power + Current2.Power;

                    Current1.Value = Current1.Value * Current2.Value;

                    Current1 = Current1.Next;

                }

                if (Current2 == p2.Begin)
                {

                    Result = temp;

                }
                else
                {

                    Result += temp;

                }

                Current2 = Current2.Next;

                temp = saveThis;

                Current1 = temp.Begin;



            }

            if (Result.Cutting)
            {
                Result.RemoveZeros();
            }

            return Result;
        }

        /// <summary>
        /// Умножает полином на число.
        /// </summary>
        /// <param name="p1">Полином 1</param>
        /// <param name="p2">Полином 2</param>
        /// <returns>Полином - результат умножения аргументов.</returns>
        public static Polinom operator *(Polinom p1, double value_)
        {
            Polinom Result = new Polinom(p1);

            if (Result.Degree == 0)
            {

                Result.Begin.Value = Result.Begin.Value * value_;

                return Result;

            }

            PolinomMember Current = Result.Begin;

            while (Current != null)
            {

                Current.Value = Current.Value * value_;

                Current = Current.Next;

            }

            if (Result.Cutting)
            {
                Result.RemoveZeros();
            }

            return Result;
        }

        /// <summary>
        /// Делит два полинома.
        /// </summary>
        /// <param name="p1">Полином 1</param>
        /// <param name="p2">Полином 2</param>
        /// <returns>Остаток от деления полиномов.</returns>
        public static Polinom operator %(Polinom p1, Polinom p2)
        {
            bool SaveCutting = p1.Cutting;
            p1.Cutting = p1.Cutting && p2.Cutting;

            Polinom Result = new Polinom(p1);

            p1.Cutting = SaveCutting;

            if (Result.Degree < p2.Degree)
            {

                throw new ArgumentException("Степень делимого полинома должна быть больше делящего...");

            }

            if (Result.Size <= 0 || p2.Size <= 0)
            {

                throw new ArgumentException("Размер полиномов должен быть больше нуля...");

            }

            if (Result == p2)
            {

                Polinom temp = new Polinom(Result.Cutting);

                Result = temp;

                return Result;

            }


            PolinomMember thisB = Result.Begin;

            PolinomMember thisCurrent = thisB;

            int size = Result.Size;

            int tempP;

            double tempV;

            PolinomMember otherB;

            PolinomMember otherCurrent;

            while (size >= p2.Size && thisCurrent != null)
            {

                otherB = p2.Begin;

                otherCurrent = otherB;


                tempP = thisCurrent.Power - otherB.Power;

                tempV = thisCurrent.Value / otherB.Value;

                for (int tempS = 0; tempS < p2.Size && thisCurrent != null; tempS++)
                {

                    int power = thisCurrent.Power - otherCurrent.Power - tempP;

                    double value = thisCurrent.Value - otherCurrent.Value * tempV;

                    thisCurrent.Power = power;

                    thisCurrent.Value = value;

                    if (thisCurrent.Value == 0.0)
                    {

                        size--;

                    }

                    thisCurrent = thisCurrent.Next;

                    otherCurrent = otherCurrent.Next;

                }

            }

            if(Result.Cutting)
            {
                Result.RemoveZeros();
            }

            return Result;
        }

        /// <summary>
        /// Делит два полинома.
        /// </summary>
        /// <param name="p1">Полином 1</param>
        /// <param name="p2">Полином 2</param>
        /// <returns>Целая часть от деления полиномов.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static Polinom operator /(Polinom p1, Polinom p2)
        {
            bool SaveCutting = p1.Cutting;
            p1.Cutting = p1.Cutting && p2.Cutting;

            Polinom Result = new Polinom(p1);

            p1.Cutting = SaveCutting;

            if (Result.Degree < p2.Degree)
            {

                throw new ArgumentException("Степень делимого полинома должна быть больше делящего...");

            }

            if (Result.Size <= 0 || p2.Size <= 0)
            {

                throw new ArgumentException("Размер полиномов должен быть больше нуля...");

            }

            if (Result == p2)
            {

                Polinom Temp = new Polinom(Result.Cutting);

                PolinomMember newMember = new PolinomMember(0, 1.1);

                Temp.PushBackMember(newMember);

                Result = Temp;

                return Result;

            }


            PolinomMember thisB = Result.Begin;

            PolinomMember thisCurrent = thisB;

            int size = Result.Size;

            int tempP;

            double tempV;

            PolinomMember otherB;

            PolinomMember otherCurrent;

            Polinom temp = new Polinom(Result.Cutting);

            PolinomMember tempResult = new PolinomMember();

            while (size >= p2.Size && thisCurrent != null)
            {

                otherB = p2.Begin;

                otherCurrent = otherB;


                tempP = thisCurrent.Power - otherB.Power;

                tempV = thisCurrent.Value / otherB.Value;

                tempResult.Power = tempP;

                tempResult.Value = tempV;

                temp.PushBackMember(tempResult);

                for (int tempS = 0; tempS < p2.Size && thisCurrent != null; tempS++)
                {

                    int power = thisCurrent.Power - otherCurrent.Power - tempP;

                    double value = thisCurrent.Value - otherCurrent.Value * tempV;

                    thisCurrent.Power = power;

                    thisCurrent.Value = value;

                    if (thisCurrent.Value == 0.0)
                    {

                        size--;

                    }

                    thisCurrent = thisCurrent.Next;

                    otherCurrent = otherCurrent.Next;

                }

            }

            if(temp.Cutting)
            {
                temp.RemoveZeros();
            }

            Result = temp;

            return Result;
        }

        /// <summary>
        /// Интерфейс, член абстрактного класса.
        /// Создаёт строковое представление полинома.
        /// </summary>
        /// <returns>Строковое представление полинома.</returns>
        public override string ToString()
        {
            string polinom = string.Empty;

            PolinomMember Current = Begin;

            while (Current != End)
            {

                polinom += Current;

                if (Current.Next.Value >= 0)
                {

                    polinom += " + ";

                }

                if (Current.Next.Value < 0)
                {

                    polinom += " - ";

                }

                Current = Current.Next;

            }

            polinom += Current;

            return polinom;
        }

        /// <summary>
        /// Интерфейс, член абстрактного класса.
        /// Предоставляет интерфейс для перебора коллекции Polinom.
        /// </summary>
        /// <returns></returns>
        public override IEnumerator GetEnumerator()
        {
            List<PolinomMember> polinomMembers = new List<PolinomMember>();
            
            PolinomMember Current = Begin;
            while (Current != End)
            {
                polinomMembers.Add(Current);
                Current = Current.Next;
            }
            polinomMembers.Add(End);

            return polinomMembers.GetEnumerator();
        }

        /// <summary>
        /// Интерфейс, член абстрактного класса.
        /// Копирует полином.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            Polinom ClonePolinom = new Polinom(this);

            return ClonePolinom;
        }

        /// <summary>
        /// Интерфейс, член абстрактного класса.
        /// Сравнивает объект Polinom с переданныи объектом. (Выдаёт исключение ArgumentException если переданный объект не Polinom)
        /// </summary>
        /// <param name="obj">Polinom</param>
        /// <returns>0 если объекты равны, 1 если переданный объект больше текущего, -1 если переданный объект меньше текущего.</returns>
        /// <exception cref="ArgumentException"></exception>
        public override int CompareTo(object obj)
        {
            if(obj is Polinom p)
            {
                if(Degree < p.Degree)
                {
                    return 1;
                }
                else if(Degree > p.Degree)
                {
                    return -1;
                }
                else
                {
                    if(Size < p.Size)
                    {
                        return 1;
                    }
                    else if(Size > p.Size)
                    {
                        return -1;
                    }
                    else
                    {
                        for(int i = 0; i < Size; i++)
                        {
                            if (this[i] < p[i])
                            {
                                return -1;
                            }
                            else if (this[i] > p[i])
                            {
                                return 1;
                            }
                        }
                        return 0;
                    }
                }

            }
            else
            {
                throw new ArgumentException("Сранить объекты не предоставляется возможным...");
            }
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
        /// Интерфейс, член абстрактного класса. Выводит строковое представление полинома в нужном формате.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public override string ToString(string format, IFormatProvider formatProvider)
        {

            if (string.IsNullOrWhiteSpace(format))
            {

                format = "P";

            }

            if (formatProvider == null)
            {

                formatProvider = CultureInfo.CurrentCulture;

            }

            switch (format.ToUpperInvariant())
            {

                case "P": // стандартный вывод в строчку
                    return ToString();

                case "F": // вывод полей по оддельности
                    return string.Format(formatProvider,
                        "Поля объекта: Степень полинома={0}; Размер={1}; Формат автосокращения={2}, Идентификатор={3}", Degree, Size, Cutting, Id);

                default:
                    throw new FormatException($"Не поддерживается формат: '{format}'...{Id}");

            }

        }
    }
}
