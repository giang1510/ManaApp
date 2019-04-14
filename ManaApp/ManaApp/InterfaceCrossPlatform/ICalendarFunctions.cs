using System;
using System.Collections.Generic;
using System.Text;

namespace ManaApp.InterfaceCrossPlatform
{
    public interface ICalendarFunctions
    {
        string showAllCalendars();
        string getEventsFromDevice(DateTime beginTime, DateTime endTime);
        //ScheduleAppointmentCollection getAppList(DateTime beginTime, DateTime endTime);
    }
}
