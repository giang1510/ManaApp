using ManaApp.InterfaceCrossPlatform;
using ManaApp.Model;
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
	public partial class ProviderResultPage : ContentPage
	{
        private string searchText;

		public ProviderResultPage ()
		{
			InitializeComponent ();
		}

        public ProviderResultPage(string searchText, ProviderSearchResult searchResult) : this()
        {
            this.searchText = searchText;
            searchTextLabel.Text += searchText;
            jsonResult.Text = searchResult.provider_infos[0].provider_id;
        }
	}
}