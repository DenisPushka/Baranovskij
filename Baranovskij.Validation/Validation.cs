using System;
using System.Linq;

namespace Baranovskij
{
    /// <summary>
    /// Различные проверки типов данных.
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// Проверка, что в строке находится только число.
        /// </summary>
        /// <param name="text">Проверяемая строка.</param>
        public static void CheckDigitalOnly(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException(text);
            }

            var havePoint = false;
            var haveMinus = false;

            foreach (var c in text)
            {
                if (c == '-')
                {
                    if (haveMinus)
                    {
                        throw new ArgumentOutOfRangeException("Введено несколько знаков '-'.");
                    }

                    haveMinus = true;
                    continue;
                }

                if (c == ',')
                {
                    if (havePoint)
                    {
                        throw new ArgumentOutOfRangeException(nameof(text),
                            $"Введенная строка = '{text}', содержит в себе несколько знаков препинания! Или проверьте знак разделитель, это должна быть запятая.");
                    }

                    havePoint = true;
                    continue;
                }

                if (c < '0' || c > '9')
                {
                    throw new ArgumentOutOfRangeException(nameof(text),
                        $"Введенная строка = '{text}', содержит в себе не только цифры!");
                }
            }
        }

        /// <summary>
        /// Проверка, на отсутствие в строке знаков препинания и пробела.
        /// </summary>
        public static void CheckPunctuation(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException(text);
            }

            if (text.Any(symbol => char.IsPunctuation(symbol) || char.IsWhiteSpace(symbol)))
            {
                throw new ArgumentOutOfRangeException($"Строка - '{text}',  содержит знаки препинания или пробел!");
            }
        }

        /// <summary>
        /// Проверка строки на null и на пустоту.
        /// </summary>
        /// <param name="text">Входная строка.</param>
        public static void CheckNullOrEmptyString(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("Входная строка null или ничего не имеет.");
            }
        }

        /// <summary>
        /// Проверка, что число положительное - [0..int.maxValue()].
        /// </summary>
        /// <param name="number">Проверяемое число.</param>
        public static void CheckPositiveNumber(int number)
        {
            if (number < 0)
            {
                throw new ArgumentOutOfRangeException($"Число {number} отрицательное!");
            }
        }

        /// <summary>
        /// Проверка на null.
        /// </summary>
        /// <param name="checkObject">Проверяемый объект.</param>
        public static void CheckObjectForNull(object checkObject)
        {
            if (checkObject is null)
            {
                throw new ArgumentNullException("Объект имеет значение null!");
            }
        }
    }
}