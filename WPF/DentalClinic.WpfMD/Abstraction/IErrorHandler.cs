using System;

namespace DentalClinic.WpfMD.Abstraction
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}