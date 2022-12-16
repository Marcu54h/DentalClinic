namespace DentalClinic.Wpf
{

    using System;
    using System.Globalization;
    using System.Windows.Controls;

    /// <summary>
    /// 
    /// </summary>
    public class ScheduleIntervalRule : ValidationRule
    {
        private int min;
        private int max;

        public ScheduleIntervalRule()
        {

        }

        public int Min
        {
            get { return min; }
            set { min = value; }
        }

        public int Max
        {
            get { return max; }
            set { max = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int interval = 0;
            try
            {
                if (((string)value).Length > 0)
                    interval = int.Parse((string)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Niedopuszczony znak lub " + e.Message);
            }

            
            if (interval == 0 || (interval < Min) || (interval > Max) || 60 % interval != 0)
            {
                return new ValidationResult(false, "Interwał terminarza musi mieścić się w przedziale <" + Min + ", " + Max + "> oraz dzielić 60 bez reszty.");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }

    }
}
