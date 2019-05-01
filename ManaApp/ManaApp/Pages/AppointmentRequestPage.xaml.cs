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

        public AppointmentRequestPage()
        {
            InitializeComponent();
        }

        public AppointmentRequestPage(DateTime startTime, DateTime endTime) : this()
        {
            this.startTime = startTime;
            this.endTime = endTime;
            dateLabel.Text = startTime.ToString();
        }

        private void OnSenRequestClicked(object sender, EventArgs e)
        {

        }
    }
}