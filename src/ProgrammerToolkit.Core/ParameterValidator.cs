using System.Globalization;

namespace ProgrammerToolkit.Core
{
    public static class ParameterValidator
    {
        public static void ValidatePositiveNumber(int number)
        {
            if (number <= 0)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, $"Invalid argument {number}. It must be a positive number."));
            }
        }
    }
}