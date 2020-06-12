using ClaroNet.models;
using ClaroNet.Services;
using ClaroNet.views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ClaroNet.viewModels
{
    public class LoginViewModel:BaseViewModel
    {
		private string email;

		public string Email
		{
			get { return email; }
			set { email = value; onPropetyChanged(nameof(Email)); }
		}

		private string password;

		public string Password
		{
			get { return password; }
			set { password = value; onPropetyChanged(nameof(Password)); }
		}

		public RelayCommand btnLogin => new RelayCommand(login);

		public async void login()
		{
			var resp = await new ClaroBackendService().login(Email, Password);
			if (!resp.Success)
			{
				await Application.Current.MainPage.DisplayAlert("Error", $"{resp.MessageError}", "Salir");
				return;
			}
			Session.GetInstance().UsuarioLogueado = resp.ObjectData;
			var v = new PcrView();
     		v.BindingContext = new PcrViewModel();
			await Application.Current.MainPage.Navigation.PushAsync(v);
	    }


	}
}
