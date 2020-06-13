using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ClaroNet.Helpers
{
    public class ServiceSms
    {
        private static string Mensaje { get; set; }
        public static void ListenToSmsRetriever()
        {
            DependencyService.Get<IListenToSmsRetriever>()?.ListenToSmsRetriever();

        }

        private static void ServiceSms_MensajeNotification(object sender, EventArgs e)
        {
            Mensaje = (string)sender;
        }


        public event EventHandler Evento;

        public void EventoRealizado()
        {
            Evento?.Invoke(Mensaje, new EventArgs());
        }
    }


    public interface IListenToSmsRetriever
    {
        void ListenToSmsRetriever();
    }
}
