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
	public partial class ProviderSearchPage : ContentPage
	{
		public ProviderSearchPage ()
		{
			InitializeComponent ();
		}

        private void LoadProviderSearchPage(object sender, EventArgs e)
        {
            //Navigation.PushModalAsync(new ProviderSearchPage());
        }
    }
}