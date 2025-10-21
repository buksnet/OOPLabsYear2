using System;
using System.Text;

namespace Lab9
{
    public class Diapason
    {
        #region PARAMETERS

        private double _x, _y;
        private static int _objectCount = 0;

        #endregion

        #region PROPERTIES

        public double Start
        {
            get => _x;
            set
            {
                if (value > _y && _y != 0)
                {
                    _x = _y;
                    _y = value;
                }
                else
                {
                    _x = value;
                }
            }
        }

        public double End
        {
            get => _y;
            set
            {
                if (value < _x && _x != 0)
                {
                    _y = _x;
                    _x = value;
                }
                else
                {
                    _y = value;
                }
            }
        }

        public static int ObjectCount => _objectCount;

        #endregion

        #region CONSTRUCTORS

        public Diapason()
        {
            _x = 0;
            _y = 0;
            _objectCount++;
        }

        /// <summary>
        /// Конструктор класса, обязывающий пользователя к 
        /// установке значений x и y для работы с классом
        /// </summary>
        /// <param name="x"> Начало диапазона </param>
        /// <param name="y"> Конец диапазона </param>
        public Diapason(double x, double y)
        {
            Start = x;
            End = y;
            _objectCount++;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Позволяет считывать текущие значения диапазона
        /// </summary>
        /// <returns> 
        /// Массив типа double, содержащий значения начала и конца диапазона
        /// </returns>
        public double[] GetRangeBorders()
        {
            return [_x, _y];
        }

        /// <summary>
        /// Проверяет, входит ли число в заданный диапазон
        /// </summary>
        /// <param name="value"> Число для проверки на принадлежность диапазону </param>
        /// <returns> Возвращает результат в виде булевой переменной</returns>
        public bool IsInRange(double value)
        {
            return value >= _x && value <= _y;
        }

        #endregion

        #region OPERATOR_OVERLOADS

        public override string ToString()
        {
            return $"Diapason[Start:{Start:F2}, End:{End:F2}, Length:{!this:F2}]";
        }

        public static Diapason operator ++(Diapason diapason)
        {
            diapason._x++;
            diapason._y++;
            return diapason;
        }

        public static Diapason operator --(Diapason diapason)
        {
            diapason._x--;
            diapason._y--;
            return diapason;
        }

        public static double operator !(Diapason diapason)
        {
            return diapason._y - diapason._x;
        }

        public static explicit operator int(Diapason diapason)
        {
            return (int)diapason._x;
        }

        public static implicit operator double(Diapason diapason)
        {
            return diapason._y;
        }

        public static Diapason operator +(Diapason diapason, int value)
        {
            return new Diapason(diapason._x + value, diapason._y + value);
        }

        public static bool operator <(Diapason diapason, double value)
        {
            return diapason._x <= value && value <= diapason._y;
        }

        public static bool operator >(Diapason diapason, double value)
        {
            return !(diapason < value);
        }

        #endregion
    }
}