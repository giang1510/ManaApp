using System;
using System.Collections.Generic;
using System.Text;

namespace ManaApp.InterfaceCrossPlatform
{
    interface ICalendarFunctions
    {
        string showAllCalendars();
        string getEventsFromDevice(DateTime beginTime, DateTime endTime);
        //ScheduleAppointmentCollection getAppList(DateTime beginTime, DateTime endTime);
    }
}
