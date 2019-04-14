using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace ManaApp.Pages.Popup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopUpTimePicker : PopupPage
	{
        private CalendarPage pageCalendar;
        public PopUpTimePicker ()
		{
			InitializeComponent ();
		}

        public PopUpTimePicker(CalendarPage pageC)
        {
            InitializeComponent();
            pageCalendar = pageC;
        }

        private void OnClose(object sender, EventArgs e)
        {
            //Start = StartTime.Time.Hours;
            pageCalendar.closeCount++;
            pageCalendar.setDebugText("Close count: " + pageCalendar.closeCount);
            pageCalendar.AddTimeSlotSuggestion(StartTime.Time, EndTime.Time);
            PopupNavigation.PopAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return Content.FadeTo(1);
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return Content.FadeTo(1);
        }
    }
}