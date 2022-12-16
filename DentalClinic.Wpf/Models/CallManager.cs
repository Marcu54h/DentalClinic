namespace DentalClinic.Wpf
{

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Controls;
    using Unity;

    /// <summary>
    /// 
    /// </summary>
    public class CallManager : IKnowWhoCall
    {
        private ICollection<Type> queue = new Collection<Type>();

        public void Call<TReceiver, TCaller>()
            where TReceiver : UserControl
            where TCaller : UserControl
        {
            queue.Add(typeof(TCaller));

            App.container.Resolve<IDisplayControls>().DisplayControl(App.container.Resolve<TReceiver>());
        }

        public void ClearQueue()
        {
            queue.Clear();
        }

        public void WhoCalledMe()
        {
            App.container.Resolve<IDisplayControls>().DisplayControl((UserControl)App.container.Resolve(queue.Last()));
            queue.Remove(queue.Last());
        }
    }
}
