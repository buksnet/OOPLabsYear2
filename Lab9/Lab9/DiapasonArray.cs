using System;
using System.Text;

namespace Lab9
{
    public class DiapasonArray
    {
        #region PARAMETERS

        private Diapason[] _arr;
        private static int _objectCount = 0;

        #endregion

        #region PROPERTIES

        public Diapason[] Array
        {
            get => _arr;
            set => _arr = value ?? throw new ArgumentNullException(nameof(value));
        }

        public int Length => _arr?.Length ?? 0;

        public static int ObjectCount => _objectCount;

        #endregion

        #region CONSTRUCTORS

        public DiapasonArray()
        {
            _arr = new Diapason[1];
            _arr[0] = new Diapason();
            _objectCount++;
        }

        public DiapasonArray(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Количество элементов должно быть положительным числом", nameof(amount));

            _arr = new Diapason[amount];
            var rng = new Random();

            for (int i = 0; i < amount; i++)
            {
                _arr[i] = new Diapason(rng.NextDouble() * 100, rng.NextDouble() * 100);
            }

            _objectCount++;
        }
        // отключение ненужного предупреждения
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
        public DiapasonArray(Diapason[] arr)
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
        {
            Array = arr ?? throw new ArgumentNullException(nameof(arr));
            _objectCount++;
        }

        #endregion

        #region METHODS

        public override string ToString()
        {
            if (_arr == null || _arr.Length == 0)
                return "DiapasonArray: {}";

            StringBuilder sb = new();
            sb.Append("DiapasonArray: {");
            sb.Append(_arr[0].ToString());

            for (int i = 1; i < _arr.Length; i++)
            {
                sb.Append(", ");
                sb.Append(_arr[i].ToString());
            }
            sb.Append("};");

            return sb.ToString();
        }

        #endregion

        #region INDEXERS

        public Diapason this[int index]
        {
            get
            {
                if (index < 0 || index >= Length)
                    throw new ArgumentOutOfRangeException(nameof(index),
                        $"Индекс {index} выходит за пределы коллекции. Допустимый диапазон: 0-{Length - 1}");

                return _arr[index];
            }
            set
            {
                if (index < 0 || index >= Length)
                    throw new ArgumentOutOfRangeException(nameof(index),
                        $"Индекс {index} выходит за пределы коллекции. Допустимый диапазон: 0-{Length - 1}");

                _arr[index] = value ?? throw new ArgumentNullException(nameof(value), "Нельзя присвоить null элементу коллекции");
            }
        }

        #endregion
    }
}
