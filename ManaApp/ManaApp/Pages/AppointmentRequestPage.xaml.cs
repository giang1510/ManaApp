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
    public partial class AppointmentRequestPage : ContentPage
    {
        private DateTime startTime;
        private DateTime endTime;
        private string providerID;

        public AppointmentRequestPage()
        {
            InitializeComponent();
        }

        public AppointmentRequestPage(DateTime startTime, DateTime endTime, string providerID) : this()
        {
            this.startTime = startTime;
            this.endTime = endTime;
            this.providerID = providerID;
            dateLabel.Text = startTime.ToString();
        }

        private async void OnSenRequestClicked(object sender, EventArgs e)
        {
            IRestService service = new RestService();
            var appointment = new ProviderAppointment
            {
                appointment_name = "appointment",
                start_date = startTime,
                end_date = endTime,
                user_info = new UserInfo
                {
                    user_name = nameEntry.Text,
                    user_phone_number = phoneNumberEntry.Text,
                    user_email = emailEntry.Text
                }
            };
            ProviderAppointmetRequestResult result = await service.RequestAppointment(appointment, providerID);
            DisplayAlert("Request", result.success.ToString(), "OK");
        }
    }
}