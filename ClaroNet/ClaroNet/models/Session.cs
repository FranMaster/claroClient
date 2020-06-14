using ClaroNet.Services.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClaroNet.models
{
    public class Session
    {
        private static Session instance;
        public static Session GetInstance()
        {  if (instance == null)
                instance = new Session();
            return instance;
        }

        public LoginResponse UsuarioLogueado { get; set; }

        public string Saldo { get; set; }

        public string SaldoActual => $" $ {Saldo}";

        public event EventHandler CambiosEnSaldo;

        public void CambioRealizado()
        {
            CambiosEnSaldo?.Invoke(this,new EventArgs());
        }

    }
}
