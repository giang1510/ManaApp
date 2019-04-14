using System;

using Android.App;
using Android.Content;
using Android.Provider;
using static Android.Provider.CalendarContract;
using Android.Accounts;
using Android.Database;
using Java.Util;
using Android.Content.PM;
using Android.Support.V4.App;
using Android;
using Syncfusion.SfSchedule.XForms;
using ManaApp.InterfaceCrossPlatform;

[assembly: Xamarin.Forms.Dependency(typeof(ManaApp.Droid.CalendarFunctions))]
namespace ManaApp.Droid
{
    class CalendarFunctions : ICalendarFunctions
    {
        //Return a appointment's list
        public ScheduleAppointmentCollection getAppList(DateTime beginTimeInput, DateTime endTimeInput)
        {
            //Projection for query with the help of the calendar-provider
            string[] INSTANCE_PROJECTION = new string[]{
                Instances.EventId,
                Instances.Begin,
                Instances.End,
                Instances.InterfaceConsts.Title,
                Instances.InterfaceConsts.EventLocation
            };

            // The indices for the projection array above.
            int PROJECTION_ID_INDEX = 0;
            int PROJECTION_BEGIN_INDEX = 1;
            int PROJECTION_END_INDEX = 2;
            int PROJECTION_TITLE_INDEX = 3;
            int PROJECTION_EVENT_LOCATION = 4;

            // Specify the date range you want to search for 
            // event instances
            Calendar beginTime = Calendar.Instance;
            Calendar endTime = Calendar.Instance;
            if (beginTimeInput.Day == endTimeInput.Day)
            {
                //Without this, the appointment will occur in 2 days cause 24 o'clock will be 
                //treated like 0 o'clock of the next day
                beginTime.Set(
                    beginTimeInput.Year,
                    beginTimeInput.Month - 1,
                    beginTimeInput.Day,
                    0,
                    0,
                    0
                );

                endTime.Set(
                        endTimeInput.Year,
                        endTimeInput.Month - 1,
                        endTimeInput.Day,
                        23,
                        59,
                        59
                    );
            }
            else
            {
                beginTime.Set(
                    beginTimeInput.Year,
                    beginTimeInput.Month - 1,
                    beginTimeInput.Day,
                    beginTimeInput.Hour,
                    beginTimeInput.Minute,
                    beginTimeInput.Second
                );

                endTime.Set(
                        endTimeInput.Year,
                        endTimeInput.Month - 1,
                        endTimeInput.Day,
                        endTimeInput.Hour,
                        endTimeInput.Minute,
                        endTimeInput.Second
                    );
            }

            long startMillis = beginTime.TimeInMillis;
            long endMillis = endTime.TimeInMillis;

            // Construct the query with the desired date range.
            Android.Net.Uri.Builder builder = Instances.ContentUri.BuildUpon();
            ContentUris.AppendId(builder, startMillis);
            ContentUris.AppendId(builder, endMillis);

            // Submit the query
            ICursor cur = null;
            cur = Application.Context.ContentResolver.Query(builder.Build(),
                INSTANCE_PROJECTION,
                null,
                null,
                null);

            ScheduleAppointmentCollection appCol = new ScheduleAppointmentCollection();
            while (cur.MoveToNext())
            {
                string title = "";
                string location = "";
                long eventID = 0;
                long beginVal = 0;
                long endVal = 0;

                // Get the field values
                eventID = cur.GetLong(PROJECTION_ID_INDEX);
                beginVal = cur.GetLong(PROJECTION_BEGIN_INDEX);
                endVal = cur.GetLong(PROJECTION_END_INDEX);
                title = cur.GetString(PROJECTION_TITLE_INDEX);
                location = cur.GetString(PROJECTION_EVENT_LOCATION);
                TimeSpan beginSpan = TimeSpan.FromMilliseconds(beginVal);
                TimeSpan endSpan = TimeSpan.FromMilliseconds(endVal);
                DateTime startDT = new DateTime(1970, 1, 1) + beginSpan;
                DateTime endDT = new DateTime(1970, 1, 1) + endSpan;

                bool allDay = false;
                if (isAllDayEvent(startDT, endDT))
                {
                    endDT = startDT;
                    allDay = true;
                }

                //Convert event instance into appointment and add it to a list
                appCol.Add(new ScheduleAppointment
                {
                    StartTime = startDT,
                    EndTime = endDT,
                    Subject = title,
                    Location = location,
                    IsAllDay = allDay
                });
            }
            return appCol;
        }

        private bool isStartAndEndOfDay(DateTime dt)
        {
            if (dt.Hour == 0 && dt.Minute == 0 && dt.Second == 0) return true;
            return false;
        }

        private bool isAllDayEvent(DateTime start, DateTime end)
        {
            return (isStartAndEndOfDay(start) && isStartAndEndOfDay(end));
        }

        public string getEventsFromDevice(DateTime beginTimeInput, DateTime endTimeInput)
        {
            string result = "";
            result += "BeginMonth: " + beginTimeInput.Month + "\n"
                        + "EndMonth: " + endTimeInput.Month + "\n";

            string[] INSTANCE_PROJECTION = new string[]{
                Instances.EventId,      // 0
                Instances.Begin,         // 1
                Instances.End,
                Instances.InterfaceConsts.Title          // 2
            };

            // The indices for the projection array above.
            int PROJECTION_ID_INDEX = 0;
            int PROJECTION_BEGIN_INDEX = 1;
            int PROJECTION_END_INDEX = 2;
            int PROJECTION_TITLE_INDEX = 3;

            // Specify the date range you want to search for recurring
            // event instances
            Calendar beginTime = Calendar.Instance;
            beginTime.Set(
                    beginTimeInput.Year,
                    beginTimeInput.Month - 1,
                    beginTimeInput.Day,
                    beginTimeInput.Hour,
                    beginTimeInput.Minute,
                    beginTimeInput.Second
                );
            //beginTime.Set(2011, 9, 23, 8, 0);
            long startMillis = beginTime.TimeInMillis;
            Calendar endTime = Calendar.Instance;
            endTime.Set(
                    endTimeInput.Year,
                    endTimeInput.Month - 1,
                    endTimeInput.Day,
                    endTimeInput.Hour,
                    endTimeInput.Minute,
                    endTimeInput.Second
                );
            //endTime.Set(2011, 10, 24, 8, 0);
            long endMillis = endTime.TimeInMillis;

            // The ID of the recurring event whose instances you are searching
            // for in the Instances table
            String selection = Instances.EventId + " = ?";
            String[] selectionArgs = new String[] { "207" };

            // Construct the query with the desired date range.
            Android.Net.Uri.Builder builder = Instances.ContentUri.BuildUpon();
            ContentUris.AppendId(builder, startMillis);
            ContentUris.AppendId(builder, endMillis);

            // Submit the query
            ICursor cur = null;
            cur = Application.Context.ContentResolver.Query(builder.Build(),
                INSTANCE_PROJECTION,
                null,
                null,
                null);

            while (cur.MoveToNext())
            {
                String title = null;
                long eventID = 0;
                long beginVal = 0;
                long endVal = 0;

                // Get the field values
                eventID = cur.GetLong(PROJECTION_ID_INDEX);
                beginVal = cur.GetLong(PROJECTION_BEGIN_INDEX);
                endVal = cur.GetLong(PROJECTION_END_INDEX);
                title = cur.GetString(PROJECTION_TITLE_INDEX);

                TimeSpan beginSpan = TimeSpan.FromMilliseconds(beginVal);
                TimeSpan endSpan = TimeSpan.FromMilliseconds(endVal);

                DateTime StartTime = new DateTime(1970, 1, 1) + beginSpan;
                DateTime EndTime = new DateTime(1970, 1, 1) + endSpan;

                // Do something with the values.
                result += "ID: " + eventID + " Title: " + title + "\n"
                   +"Start: "+dateTimeToString(StartTime)+" End: "+dateTimeToString(EndTime)+"\n"
                   +"All Day: "+isAllDayEvent(StartTime,EndTime);
            }


            return result;
        }

        private string dateTimeToString(DateTime dt)
        {
            return dt.Hour + " " + dt.Minute + " " + dt.Second;
        }

        //Show all calendars from Device
        public string showAllCalendars()
        {
            string accsString = "";
            Account[] accounts = AccountManager.Get(Application.Context).GetAccounts();
            foreach (Account acc in accounts)
            {
                accsString += acc.Name + "\n";
            }

            // Projection array. Creating indices for this array instead of doing
            // dynamic lookups improves performance.
            string[] EVENT_PROJECTION = new string[] {
                BaseColumns.Id,                           // 0
                SyncColumns.AccountName,                  // 1
                CalendarColumns.CalendarDisplayName,         // 2
                CalendarColumns.OwnerAccount                  // 3
            };

            // The indices for the projection array above.
            int PROJECTION_ID_INDEX = 0;
            int PROJECTION_ACCOUNT_NAME_INDEX = 1;
            int PROJECTION_DISPLAY_NAME_INDEX = 2;
            int PROJECTION_OWNER_ACCOUNT_INDEX = 3;



            ContentResolver cr = Application.Context.ContentResolver;
            Android.Net.Uri uri = Calendars.ContentUri;
            //String selection = "((" + Calendars.ACCOUNT_NAME + " = ?) AND ("
            //                        + Calendars.ACCOUNT_TYPE + " = ?) AND ("
            //                        + Calendars.OWNER_ACCOUNT + " = ?))";
            String[] selectionArgs = new String[] {"hera@example.com", "com.example",
        "hera@example.com"};
            // Submit the query and get a Cursor object back.



            // Construct event details
            long startMillis = 0;
            long endMillis = 0;
            Calendar beginTime = Calendar.Instance;
            beginTime.Set(2017, 8, 25, 8, 30);
            startMillis = beginTime.TimeInMillis;
            Calendar endTime = Calendar.Instance;
            endTime.Set(2017, 8, 25, 8, 45);
            endMillis = endTime.TimeInMillis;


            // Insert Event
            ContentValues values = new ContentValues();
            Java.Util.TimeZone timeZone = Java.Util.TimeZone.Default;
            values.Put(CalendarContract.EventsColumns.Dtstart, startMillis);
            values.Put(CalendarContract.EventsColumns.Dtend, endMillis);
            values.Put(CalendarContract.EventsColumns.EventTimezone, timeZone.ID);
            values.Put(CalendarContract.EventsColumns.Title, "Poooowww");
            values.Put(CalendarContract.EventsColumns.Description, "Yeah Baby!");
            values.Put(CalendarContract.EventsColumns.CalendarId, 1);

            values.Put(CalendarContract.EventsColumns.HasAlarm, 1);
            if (ActivityCompat.CheckSelfPermission(Application.Context, Manifest.Permission.WriteCalendar) != Permission.Granted)
            {
                // TODO: Consider calling
                //    ActivityCompat#requestPermissions
                // here to request the missing permissions, and then overriding
                //   public void onRequestPermissionsResult(int requestCode, String[] permissions,
                //                                          int[] grantResults)
                // to handle the case where the user grants the permission. See the documentation
                // for ActivityCompat#requestPermissions for more details.
                ActivityCompat.RequestPermissions((Activity)Application.Context, new String[] { Manifest.Permission.WriteCalendar }, 0);

                //            return;
            }
            if (ActivityCompat.CheckSelfPermission(Application.Context, Manifest.Permission.ReadCalendar) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions((Activity)Application.Context, new String[] { Manifest.Permission.ReadCalendar }, 1);
            }
            uri = cr.Insert(CalendarContract.Events.ContentUri, values);

            //List all calendars
            ICursor cur = null;
            cur = Application.Context.ContentResolver.Query(Calendars.ContentUri, EVENT_PROJECTION, null, null, null);

            if (cur.MoveToFirst())
            {
                do
                {
                    long calID = 0;
                    String displayName = null;
                    String accountName = null;
                    String ownerName = null;

                    // Get the field values
                    calID = cur.GetLong(PROJECTION_ID_INDEX);
                    displayName = cur.GetString(PROJECTION_DISPLAY_NAME_INDEX);
                    accountName = cur.GetString(PROJECTION_ACCOUNT_NAME_INDEX);
                    ownerName = cur.GetString(PROJECTION_OWNER_ACCOUNT_INDEX);

                    accsString += calID.ToString() + " " + displayName + " " + accountName + " " + ownerName + "\n";
                } while (cur.MoveToNext());
            }
            return accsString;
        }
    }
}