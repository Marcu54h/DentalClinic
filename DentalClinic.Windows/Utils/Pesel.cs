namespace DentalClinic.Windows
{

    using System;

    /// <summary>
    /// 
    /// </summary>
    public class Pesel : IPesel
    {
        #region Fields



        #endregion // Fields

        #region Properties



        #endregion // Properties

        #region Constructors



        #endregion // Constructors

        #region Methods

        public bool Check(string numPESEL)
        {
            if (!(numPESEL is null) && numPESEL.Length == 11)
            {
                float A = Int32.Parse(numPESEL.Substring(0, 1)) * 9;
                float B = Int32.Parse(numPESEL.Substring(1, 1)) * 7;
                float C = Int32.Parse(numPESEL.Substring(2, 1)) * 3;
                float D = Int32.Parse(numPESEL.Substring(3, 1)) * 1;
                float E = Int32.Parse(numPESEL.Substring(4, 1)) * 9;
                float F = Int32.Parse(numPESEL.Substring(5, 1)) * 7;
                float G = Int32.Parse(numPESEL.Substring(6, 1)) * 3;
                float H = Int32.Parse(numPESEL.Substring(7, 1)) * 1;
                float I = Int32.Parse(numPESEL.Substring(8, 1)) * 9;
                float J = Int32.Parse(numPESEL.Substring(9, 1)) * 7;
                float result = (A + B + C + D + E + F + G + H + I + J) % 10;
                if ((Int32)result == Int32.Parse(numPESEL.Substring(10, 1)))
                    return true;
                else return false;
            }
            return false;
        }

        public DateTime? DateOfBirth(string numPESEL)
        {
            if (Check(numPESEL))
            {
                int y = 0; //Year of birth
                int m = Int32.Parse(numPESEL.Substring(2, 2)); //Month of birth
                if ((m > 80) && (m < 93)) y = Int32.Parse("18" + numPESEL.Substring(0, 2));
                if ((m > 0) && (m < 13)) y = Int32.Parse("19" + numPESEL.Substring(0, 2));
                if ((m > 20) && (m < 33)) y = Int32.Parse("20" + numPESEL.Substring(0, 2));
                if ((m > 40) && (m < 53)) y = Int32.Parse("21" + numPESEL.Substring(0, 2));
                if ((m > 60) && (m < 73)) y = Int32.Parse("22" + numPESEL.Substring(0, 2));
                while (m > 12) m -= 10;
                int d = Int32.Parse(numPESEL.Substring(4, 2)); //Day of birth
                if (d > 31) d = 1;
                return new DateTime(y, m, d);
            }
            else
                return null;
        }

        public bool? IsMale(string numPESEL)
        {
            if (Check(numPESEL))
            {
                if ((Int32.Parse(numPESEL.Substring(9, 1)) % 2) == 0)
                    return false;
                else
                    return true;
            }
            else
                return null;
        }

        #endregion // Methods
    }
}
