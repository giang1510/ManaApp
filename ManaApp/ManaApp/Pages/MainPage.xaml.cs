using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ManaApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        //private void ShowAllCalendarsClicked(object sender, EventArgs e)
        //{
        //    ICalendarFunctions calendarFunctions = DependencyService.Get<ICalendarFunctions>();
        //    if (calendarFunctions != null)
        //    {
        //        textView.Text = calendarFunctions.showAllCalendars();
        //    }
        //}

        //private void ShowEventsClicked(object sender, EventArgs e)
        //{
        //    ICalendarFunctions calendarFunctions = DependencyService.Get<ICalendarFunctions>();
        //    if (calendarFunctions != null)
        //    {
        //        textView.Text = calendarFunctions.getEventsFromDevice(new DateTime(2017, 12, 1, 0, 0, 1), new DateTime(2017, 12, 31, 23, 59, 59));
        //    }
        //}

        //private void LoadHttpPageClicked(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new PageHttpRequest());
        //}

        //private void LoadCalendarPageClicked(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new PageCalendar());
        //}

        //private void LoadLoginPageClicked(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new LoginPage());
        //}

        //private void LoadRegisterPageClicked(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new PageRegister());
        //}

        //private void LoadNewAppInfoPageClicked(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new PageNewAppInfo());
        //}

    }
}
