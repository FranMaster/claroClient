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

	}
}
