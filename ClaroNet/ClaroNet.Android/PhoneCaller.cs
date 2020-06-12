using Android.Content;
using ClaroNet.Droid;
using ClaroNet.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneCaller))]
namespace ClaroNet.Droid
{
    public class PhoneCaller : IServiceCaller
    {
        //pin fran 2232
        public void MakeCall(string phone, string monto,string pin)
        {
            try
            {
                string Combinacion = $"*321*1*{phone}*{monto}*{pin}*1#";
                var uri = string.Empty;
                var ussd = string.Format("tel:{0}", Combinacion);
                foreach (char c in ussd.ToCharArray())
                {
                    if (c == '#')
                    {
                        uri += Android.Net.Uri.Encode("#");
                    }
                    else
                    {
                        uri += c;
                    }
                }
                var Uri = Android.Net.Uri.Parse(uri);
                var intent = new Intent(Intent.ActionCall, Uri);
                intent.SetFlags(ActivityFlags.NewTask);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                Android.App.Application.Context.StartActivity(intent);

            }
            catch (System.Exception er)
            {

                throw;
            }
        }
    }
}