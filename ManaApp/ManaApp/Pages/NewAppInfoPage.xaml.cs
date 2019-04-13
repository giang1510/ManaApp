using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ManaApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewAppInfoPage : ContentPage
	{
		public NewAppInfoPage ()
		{
			InitializeComponent ();
		}

        private void NewAppClicked(object sender, EventArgs e)
        {
            //TODO
        }

        private void ToCalendarPageClicked(object sender, EventArgs e)
        {
            //TODO
            Navigation.PushAsync(new CalendarPage());
        }
    }
}