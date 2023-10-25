using System;

namespace SiphoinUnityHelpers.XNodeExtensions.Math.Compute
{
    public static class ComputeHelper
    {



        private static bool TryParseComparingObjects (object first, object second, out double numberFirst, out double numberSecond)
        {
            numberFirst = double.NaN;

            numberSecond = double.NaN;

            return double.TryParse(first.ToString(), out numberFirst) && double.TryParse(second.ToString(), out numberSecond);
        }

        public static bool Compute (object first, object second, ComputeType computeType)
        {
            double numberFirst = double.NaN;

            double numberSecond = double.NaN;

            switch (computeType)
            {
                case ComputeType.Equals:
                    return first.Equals(second);
                case ComputeType.NotEquals:
                    return !first.Equals(second);

                case ComputeType.More:

                    if (TryParseComparingObjects(first, second, out numberFirst, out numberSecond))
                    {
                        return numberFirst > numberSecond;
                    }

                    else if (first.GetType() == typeof(string) && second.GetType() == typeof(string))
                    {
                        string firstString = first.ToString();

                        string secondString = second.ToString();

                        return firstString.Length > secondString.Length;
                    }

                    break;

                case ComputeType.Lesser:

                    if (TryParseComparingObjects(first, second, out numberFirst, out numberSecond))
                    {
                        return numberFirst < numberSecond;
                    }

                    else if (first.GetType() == typeof(string) && second.GetType() == typeof(string))
                    {
                        string firstString = first.ToString();

                        string secondString = second.ToString();

                        return firstString.Length < secondString.Length;
                    }

                    break;


                case ComputeType.LesserOrEquals:

                    if (TryParseComparingObjects(first, second, out numberFirst, out numberSecond))
                    {
                        return numberFirst <= numberSecond;
                    }

                    else if (first.GetType() == typeof(string) && second.GetType() == typeof(string))
                    {
                        string firstString = first.ToString();

                        string secondString = second.ToString();

                        return firstString.Length <= secondString.Length;
                    }
                    break;
                case ComputeType.MoreOrEquals:

                    if (TryParseComparingObjects(first, second, out numberFirst, out numberSecond))
                    {
                        return numberFirst >= numberSecond;
                    }

                    else if (first.GetType() == typeof(string) && second.GetType() == typeof(string))
                    {
                        string firstString = first.ToString();

                        string secondString = second.ToString();

                        return firstString.Length >= secondString.Length;
                    }
                    break;
            }

            return false;
        }

        public static bool Compute(object a, object b, ComputeObjectsType type)
        {
            switch (type)
            {
                case ComputeObjectsType.Equals:
                    return a == b;
                case ComputeObjectsType.NotEquals:
                    return a != b;
            }

            return false;
        }
    }
}
