namespace DentalClinic.Wpf
{

    using System;
    using System.Globalization;
    using System.Windows.Controls;

    /// <summary>
    /// 
    /// </summary>
    public class WorkHourRule : ValidationRule
    {
        private int min;
        private int max;

        public WorkHourRule()
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
            int workHour = 0;
            try
            {
                if (((string)value).Length > 0)
                    workHour = int.Parse((string)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Niedopuszczony znak lub " + e.Message);
            }

            if ((workHour < Min) || (workHour > Max))
            {
                return new ValidationResult(false, "Godziny pracy muszą mieścić się w przedziale <" + Min + ", " + Max + ">.");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }

    }
}
