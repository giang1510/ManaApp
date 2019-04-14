using ManaApp.InterfaceCrossPlatform;
using ManaApp.Model;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ManaApp
{
    class ScheduleViewModel
    {
        private static ScheduleAppointmentCollection AppCol { get; set; }
        private static List<string> currentDayMeetings;
        private static List<Color> color_collection;

        public ScheduleViewModel()
        {

        }

        public static ScheduleAppointmentCollection GetAppointments(DateTime startTime, DateTime endTime)
        {
            ICalendarFunctions calFunc = DependencyService.Get<ICalendarFunctions>();
            AppCol = new ScheduleAppointmentCollection();
            if (calFunc != null)
            {
                
                AppCol = calFunc.getAppList(startTime, endTime);
                
            }
            return AppCol;
        }

    }
}
