using ClaroNet.models;
using ClaroNet.Services;
using ClaroNet.Services.RecargasResponse;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ClaroNet.viewModels
{
    public class PcrViewModel:BaseViewModel
    {
		public PcrViewModel()
		{
			NombreUsuario = $"{Session.GetInstance().UsuarioLogueado.Data.Datosusuario.Nombre} {Session.GetInstance().UsuarioLogueado.Data.Datosusuario.Apellido}";
			CargarRecargas();
		}

		private string nombreUsuario;
		public string NombreUsuario
		{
			get { return nombreUsuario; }
			set
			{
				nombreUsuario = value; 
				onPropetyChanged(nameof(NombreUsuario));
			}
		}
		private string _pcr;
		public string Pcr
		{
			get { return _pcr; }
			set { _pcr = value; onPropetyChanged(nameof(Pcr)); }
		}
		private ObservableCollection<DatosRecargas> _listaRecientes;

		public ObservableCollection<DatosRecargas> ListaRecientes
		{
			get { return _listaRecientes; }
			set
			{
				_listaRecientes = value; onPropetyChanged(nameof(ListaRecientes));
			}
		}
			   		 
		public RelayCommand NuevaRecarga => new RelayCommand(btnNuevaRecarga);
		public RelayCommand stats => new RelayCommand(btnStats);
		public RelayCommand salir => new RelayCommand(btnSalir);


		private void btnNuevaRecarga()
		{

		}
		private void btnStats()
		{

		}
		private void btnSalir()
		{

		}

		public async void CargarRecargas()
		{
		  var resp=	await new ClaroBackendService()
				.getRecargas(
				 Session.GetInstance()
				.UsuarioLogueado.Data.Pcr);
			if (!resp.Success)
				return;
			ListaRecientes = new ObservableCollection<DatosRecargas>( resp.ObjectData.Data);

		}
	}
}
