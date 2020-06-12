using ClaroNet.Helpers;
using ClaroNet.models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
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

        }


        public RelayCommand btnRecargar => new RelayCommand(Recargar);

        public async void Recargar()
        {
            try
            {
                if (!string.IsNullOrEmpty(Telefono) && (!string.IsNullOrEmpty(Monto)))
                    DependencyService.Get<IServiceCaller>()
                        .MakeCall(Telefono, Monto,Session.GetInstance().
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



    }
}
