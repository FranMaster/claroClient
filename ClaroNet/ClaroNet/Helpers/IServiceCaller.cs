using System;
using System.Collections.Generic;
using System.Text;

namespace ClaroNet.Helpers
{
    public interface IServiceCaller
    {
        void MakeCall(string phone, string monto, string pin);
    }
}
