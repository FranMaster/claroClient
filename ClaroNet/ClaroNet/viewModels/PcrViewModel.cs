using ClaroNet.models;
using ClaroNet.Services;
using ClaroNet.Services.RecargasResponse;
using ClaroNet.views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace ClaroNet.viewModels
{
	public class PcrViewModel : BaseViewModel
	{
		public PcrViewModel()
		{
			NombreUsuario = $"{Session.GetInstance().UsuarioLogueado.Data.Datosusuario.Nombre} {Session.GetInstance().UsuarioLogueado.Data.Datosusuario.Apellido}";
			CargarRecargas();
			Session.GetInstance().CambiosEnSaldo += PcrViewModel_CambiosEnSaldo;
			VisibilidadSaldo = false;
			CargarSaldo();


		}


		public void CargarSaldo()
		{

			if (ListaRecientes != null && ListaRecientes.Count > 0)
			{
				var a = ListaRecientes.First().Data;
				var mensajeDescompuesto = a.Split('\n');
				if (mensajeDescompuesto.Count() <= 0 && !mensajeDescompuesto.First().Contains("RecargaCLR"))
					return;
				var MensajedeSaldo = mensajeDescompuesto[1].Split('.');
				var e = MensajedeSaldo[2].Split(' ');
				SaldoActual = $" $ {e.Last()}";
				VisibilidadSaldo = true;

			}
		
		}

		private void PcrViewModel_CambiosEnSaldo(object sender, EventArgs e)
		{
			CargarRecargas();
			SaldoActual = Session.GetInstance().SaldoActual;
			VisibilidadSaldo = true;
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
		private bool _VisibilidadSaldo;

		public bool VisibilidadSaldo
		{
			get { return _VisibilidadSaldo; }
			set { _VisibilidadSaldo = value; onPropetyChanged(nameof(VisibilidadSaldo)); }
		}


		private string saldoActual;

		public string SaldoActual
		{
			get { return saldoActual; }
			set { saldoActual = value; onPropetyChanged(nameof(SaldoActual)); }
		}

		public RelayCommand NuevaRecarga => new RelayCommand(btnNuevaRecarga);
		public RelayCommand stats => new RelayCommand(btnStats);
		public RelayCommand salir => new RelayCommand(btnSalir);


		private async void btnNuevaRecarga()
		{
			RecargasView vw = new RecargasView();
			vw.BindingContext = new RecargasViewModel();
			await Application.Current.MainPage.Navigation.PushAsync(vw);
		}
		private async void btnStats()
		{
			HistorialView vw = new HistorialView();
			await Application.Current.MainPage.Navigation.PushAsync(vw);
		}
		private async void btnSalir()
		{
			LoginPage vw = new LoginPage();
			Session.GetInstance().Logout();
			await Application.Current.MainPage.Navigation.PushAsync(vw);
		}

		public async void CargarRecargas()
		{
			var resp = await new ClaroBackendService()
				  .getRecargas(
				   Session.GetInstance()
				  .UsuarioLogueado.Data.Pcr);
			if (!resp.Success)
				return;
			resp.ObjectData.Data.Reverse();
			ListaRecientes = new ObservableCollection<DatosRecargas>(resp.ObjectData.Data);

		}
	}
}
