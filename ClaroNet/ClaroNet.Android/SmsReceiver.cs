using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Telephony;
using Android.Widget;
using BurgerSpot.Droid;
using ClaroNet.Helpers;
using System;
using System.Text;
using Xamarin.Forms;
[assembly: Dependency(typeof(SMSReceiver))]
namespace BurgerSpot.Droid
{

    [BroadcastReceiver(Label = "SMS Receiver")]
    [IntentFilter(new string[] { "android.provider.Telephony.SMS_RECEIVED" })]
    public class SMSReceiver : BroadcastReceiver
    {
        public static readonly string IntentAction = "android.provider.Telephony.SMS_RECEIVED";

        public string MensajeDeTEXTO { get; set; }

    
        public override void OnReceive(Context context, Intent intent)
        {
            try
            {
                if (intent.Action != IntentAction) return;

                var bundle = intent.Extras;
                if (bundle == null) return;
                var pdus = bundle.Get("pdus");
                var castedPdus = JNIEnv.GetArray<Java.Lang.Object>(pdus.Handle);
                var msgs = new SmsMessage[castedPdus.Length];
                var sb = new StringBuilder();
                String sender = null;
                for (var i = 0; i < msgs.Length; i++)
                {
                    var bytes = new byte[JNIEnv.GetArrayLength(castedPdus[i].Handle)];
                    JNIEnv.CopyArray(castedPdus[i].Handle, bytes);

                    msgs[i] = SmsMessage.CreateFromPdu(bytes);
                    if (sender == null)
                        sender = msgs[i].OriginatingAddress;
                    sb.Append(string.Format("SMS From: {0}{1}Body: {2}{1}", msgs[i].OriginatingAddress,
                        System.Environment.NewLine, msgs[i].MessageBody));

                    Toast.MakeText(context, sb.ToString(), ToastLength.Long).Show();
                }

                MensajeDeTEXTO = sb.ToString();
                Utilities.Notify(Events.SmsRecieved, MensajeDeTEXTO);
                //ListenToSmsRetriever();
            }
            catch (Exception ex)
            {
                Toast.MakeText(context, ex.Message, ToastLength.Long).Show();
            }
        }



    }
}