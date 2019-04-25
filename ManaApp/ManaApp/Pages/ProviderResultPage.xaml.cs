using ManaApp.InterfaceCrossPlatform;
using ManaApp.Model;
using ManaApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private Provider[] providerList;
        private ObservableCollection<ProviderResultViewModel> providers { get; set; }

        public ProviderResultPage ()
		{
			InitializeComponent ();
		}

        public ProviderResultPage(string searchText, ProviderSearchResult searchResult) : this()
        {
            providerList = searchResult.provider_infos;

            searchTextLabel.Text += searchText;
            if (searchResult.provider_infos.Length <= 0)
            {
                jsonResult.IsVisible = true;
            }

            PopulateResultListView(searchResult.provider_infos);
        }

        private void PopulateResultListView(Provider[] providerInfos)
        {
            providers = new ObservableCollection<ProviderResultViewModel>();
            providerResultListView.ItemsSource = providers;
            foreach (var provider in providerInfos)
            {
                providers.Add(new ProviderResultViewModel()
                {
                    name = provider.provider_info.provider_name,
                    address = provider.provider_info.provider_address
                });
            }
        }

        private void OnTap(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushModalAsync(new ProviderPage(providerList[e.ItemIndex]));
        }
    }
}