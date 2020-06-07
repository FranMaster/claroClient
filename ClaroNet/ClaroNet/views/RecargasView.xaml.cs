using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClaroNet.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecargasView : ContentPage
    {
        public RecargasView()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushAsync(new PortalClaro());
        }
    }
}