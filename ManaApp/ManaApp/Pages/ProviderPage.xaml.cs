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
	public partial class ProviderPage : ContentPage
	{
        private Provider provider;

		public ProviderPage ()
		{
			InitializeComponent ();
		}

        public ProviderPage(Provider provider) : this()
        {
            FillInProviderInfo(provider.provider_info);
            this.provider = provider;
        }

        private void FillInProviderInfo(ProviderInfo providerInfo)
        {
            nameLabel.Text = providerInfo.provider_name;
            addressLabel.Text = providerInfo.provider_address;
            phoneNumberLabel.Text = providerInfo.provider_phone_number;
            emailLabel.Text = providerInfo.provider_email;
        }

        private async void MakeAppointment(object sender, EventArgs e)
        {
            IRestService service = new RestService();
            var providerPublicResult = await service.GetProviderPublicInfo(provider.provider_id);
            Navigation.PushModalAsync(new CalendarPage(provider, providerPublicResult.provider_appointments));
        }
	}
}