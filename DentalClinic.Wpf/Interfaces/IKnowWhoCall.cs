namespace DentalClinic.Wpf
{

    using System;
    using System.Windows.Controls;

    public interface IKnowWhoCall
    {
        void Call<TReceiver, TCaller>()
            where TReceiver : UserControl
            where TCaller : UserControl;

        void ClearQueue();

        void WhoCalledMe();
    }
}
