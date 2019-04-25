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
	public partial class ProviderPage : ContentPage
	{
		public ProviderPage ()
		{
			InitializeComponent ();
		}

        public ProviderPage(Provider provider) : this()
        {
            FillInProviderInfo(provider.provider_info);
        }

        private void FillInProviderInfo(ProviderInfo providerInfo)
        {
            nameLabel.Text = providerInfo.provider_name;
            addressLabel.Text = providerInfo.provider_address;
            phoneNumberLabel.Text = providerInfo.provider_phone_number;
            emailLabel.Text = providerInfo.provider_email;
        }
	}
}