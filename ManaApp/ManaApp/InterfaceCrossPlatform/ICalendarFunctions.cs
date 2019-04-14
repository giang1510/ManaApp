using Syncfusion.SfSchedule.XForms;
using System;

namespace ManaApp.InterfaceCrossPlatform
{
    public interface ICalendarFunctions
    {
        string showAllCalendars();
        string getEventsFromDevice(DateTime beginTime, DateTime endTime);
        ScheduleAppointmentCollection getAppList(DateTime beginTime, DateTime endTime);
    }
}
