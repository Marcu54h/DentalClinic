using System;

namespace DentalClinic.Windows
{
    public interface IPesel
    {
        bool Check(string numPESEL);
        DateTime? DateOfBirth(string numPESEL);
        bool? IsMale(string numPESEL);
    }
}