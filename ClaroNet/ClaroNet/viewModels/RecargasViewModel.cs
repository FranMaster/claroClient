using ClaroNet.Helpers;
using ClaroNet.models;
using ClaroNet.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ClaroNet.viewModels
{
    public class RecargasViewModel : BaseViewModel
    {
        private string _telefono;

        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; onPropetyChanged(nameof(Telefono)); }
        }

        private string _Monto;

        public string Monto
        {
            get { return _Monto; }
            set { _Monto = value; onPropetyChanged(nameof(Monto)); }
        }



        public RecargasViewModel()
        {
            mensajeAnterior = string.Empty;
            this.Subscribe<string>(Events.SmsRecieved, code =>
            {
                Notificacion(code);
            });
        }


        public RelayCommand btnRecargar => new RelayCommand(Recargar);

        public async void Recargar()
        {
            try
            {
                if (!string.IsNullOrEmpty(Telefono) && (!string.IsNullOrEmpty(Monto)))
                    DependencyService.Get<IServiceCaller>()
                        .MakeCall(Telefono, Monto, Session.GetInstance().
                        UsuarioLogueado
                        .Data
                        .Pcr
                        .Pin
                        .ToString());
                else
                    await App.Current.MainPage.DisplayAlert("Error", $"Lene todos los campos", "Accept");

            }
            catch (Exception ed)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"{ed.Message}.", "Accept");

            }
        }
        public string mensajeAnterior { get; set; }
        public async void Notificacion(string codigo)
        {
            if (mensajeAnterior.Equals(codigo))
                 return;     
            var mensajeDescompuesto = codigo.Split('\n');
            if (mensajeDescompuesto.Count() <= 0 && !mensajeDescompuesto.First().Contains("RecargaCLR"))
                return;
                      

            var resp= await new ClaroBackendService().SaveRecargas(new Services.RecargasRquest.RecargasRequest
            {
                Pcr=new Services.RecargasRquest.Pcr
                {
                    Direccion=Session.GetInstance().UsuarioLogueado.Data.Pcr.Direccion,
                    NombreDelPunto= Session.GetInstance().UsuarioLogueado.Data.Pcr.NombreDelPunto
                },
                Data = codigo,
                Fecha=DateTime.Now.ToShortTimeString()
            });
            if (resp.Success)
            {
                await App.Current.MainPage.DisplayAlert("Claro Mensaje", "Actualiza Recargas", "Accept");
            }
            mensajeAnterior = codigo;

        }
    }
}
