using ManaApp.InterfaceCrossPlatform;
using ManaApp.Model;
using System;
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

        private async void LoadProviderSearchPage(object sender, EventArgs e)
        {
            ProviderSearchResult searchResult = await DoSearch();

            Navigation.PushModalAsync(new ProviderResultPage(providerSearchEntry.Text, searchResult));
        }

        private async Task<ProviderSearchResult> DoSearch()
        {
            var searchInput = new SearchInput
            {
                search_text = providerSearchEntry.Text
            };

            IRestService service = new RestService();
            return await service.SearchProvider(searchInput);
        }
    }
}