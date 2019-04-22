using ManaApp.Model;
using ManaApp.Pages.Popup;
using Rg.Plugins.Popup.Extensions;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ManaApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalendarPage : ContentPage
	{
        private List<ChosenTime> chosenTimeList = new List<ChosenTime>();
        private DateTime selectedDate = new DateTime();
        private ScheduleAppointmentCollection curCollection = new ScheduleAppointmentCollection();
        private Dictionary<string, List<string>> selDateLabelTxtCol = new Dictionary<string, List<string>>();

        //For debugging
        public int closeCount { get; set; }

        public CalendarPage ()
		{
			InitializeComponent ();

            closeCount = 0;

            SetupViewPicker();

            ConfigureWeekViewSettings();
        }

        private void SetupViewPicker()
        {
            foreach (string viewName in Constants.viewNames)
            {
                viewPicker.Items.Add(viewName);
            }
            //Default Value = "Day View"
            viewPicker.SelectedIndex = 0;
        }

        private void ConfigureWeekViewSettings()
        {
            WeekViewSettings weekViewSettings = new WeekViewSettings();
            weekViewSettings.AllDayAppointmentLayoutColor = Color.LightGray;
            weekViewSettings.ShowAllDay = true;
            schedule.WeekViewSettings = weekViewSettings;
        }

        public CalendarPage(AppointmentDesc appointmentDesc) : this()
        {
            // TODO
        }

        private void OnViewChanged(object sender, EventArgs e)
        {
            Picker viewPicker = sender as Picker;
            switch (viewPicker.SelectedIndex)
            {
                case Constants.dayViewPos:
                    schedule.ScheduleView = Syncfusion.SfSchedule.XForms.ScheduleView.DayView;
                    break;
                case Constants.weekViewPos:
                    schedule.ScheduleView = Syncfusion.SfSchedule.XForms.ScheduleView.WeekView;
                    break;
                case Constants.monthViewPos:
                    {
                        schedule.ScheduleView = Syncfusion.SfSchedule.XForms.ScheduleView.MonthView;
                        break;
                    }
                default:
                    Debug.WriteLine("Wrong View Input!");
                    break;
            }
        }

        private void PopulateScheduleWithData(VisibleDatesChangedEventArgs e)
        {
            //Get the range of visible dates of SFSchedule
            List<DateTime> visDates = e.visibleDates;
            DateTime startTime = visDates[0];
            DateTime endTime = visDates[visDates.Count - 1];

            ////Set dates for the new schedule view
            curCollection = ScheduleViewModel.GetAppointments(startTime, endTime);
            schedule.DataSource = curCollection;
        }

        //Schedule's Left-Right-Swipe Event
        private void OnVisibleDatesChanged(object sender, VisibleDatesChangedEventArgs e)
        {
            PopulateScheduleWithData(e);
        }

        private void CreateLabelList(CellTappedEventArgs e)
        {
            //Assign selected date
            selectedDate = e.Datetime;

            //Create list of labels for each selected date
            if (!selDateLabelTxtCol.ContainsKey(ConvertDatetimeToKey(selectedDate)))
            {
                selDateLabelTxtCol.Add(ConvertDatetimeToKey(selectedDate), new List<string>());
            }
        }

        private void OnCalendarCellTapped(object sender, CellTappedEventArgs e)
        {
            CreateLabelList(e);

            ShowLabelsSuggestion();
            if (Constants.toast != null) Constants.toast.speak("Cell Tapped!!");
        }



        private async void OnTimeSlotAdd(object sender, EventArgs e)
        {
            var timeAddPopup = new PopUpTimePicker(this);
            await Navigation.PushPopupAsync(timeAddPopup);
        }

        public void setDebugText(string text)
        {
            debugLabel.Text = text;
        }

        public void AddTimeSlotSuggestion(TimeSpan startTime, TimeSpan endTime)
        {
            //Suggestion as timeslot was added
            var appointment = new ScheduleAppointment
            {
                StartTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, startTime.Hours, startTime.Minutes, startTime.Seconds),
                EndTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, endTime.Hours, endTime.Minutes, endTime.Seconds),
                Subject = "Added Suggestion",
                Color = Color.Chocolate
            };

            //For sending data to server
            chosenTimeList.Add(new ChosenTime
            {
                Start = appointment.StartTime,
                End = appointment.EndTime
            });

            //For calendar-visualisation
            curCollection.Add(appointment);

            //For reusing
            selDateLabelTxtCol[ConvertDatetimeToKey(selectedDate)].Add(TimeToSuggestionStr(startTime, endTime));

            //Displaying suggestion labels
            ShowLabelsSuggestion();
        }

        private void ShowAddedTimeSlots()
        {
            List<string> selLabelList = selDateLabelTxtCol[ConvertDatetimeToKey(selectedDate)];
            if (selLabelList.Count > 0 && selLabelList != null)
            {
                int index = 0;
                foreach (string labelStr in selLabelList)
                {
                    //Label left
                    Label lb = new Label
                    {
                        Text = labelStr,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Start
                    };

                    //Delete-Btn right
                    Button delBtn = new Button
                    {
                        Text = "Delete",
                        HorizontalOptions = LayoutOptions.End,
                        ClassId = "" + index

                    };
                    delBtn.Clicked += OnDelLbClicked;

                    // Specify layout for the items within a time slot
                    StackLayout mainLayout = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal
                    };
                    mainLayout.Children.Add(lb);
                    mainLayout.Children.Add(new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand
                    });
                    mainLayout.Children.Add(delBtn);

                    // Border with color
                    Frame lbFrame = new Frame
                    {
                        Content = mainLayout,
                        OutlineColor = Color.Black
                    };
                    timeSlotLayout.Children.Add(lbFrame);

                    index++;
                }
            }
        }

        private void ShowTimeSlotAddBtn()
        {
            Button timeSlotAddBtn = new Button
            {
                Text = "Add Time",
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            timeSlotAddBtn.Clicked += OnTimeSlotAdd;
            timeSlotLayout.Children.Add(timeSlotAddBtn);
        }

        private void ShowLabelsSuggestion()
        {
            //Dynamic Add-Button and Suggestion-Labels
            timeSlotLayout.Children.Clear();

            ShowTimeSlotAddBtn();

            ShowAddedTimeSlots();
        }

        private void OnDelLbClicked(object sender, EventArgs e)
        {
            Button delBtn = sender as Button;
            int index = int.Parse(delBtn.ClassId);
            selDateLabelTxtCol[ConvertDatetimeToKey(selectedDate)].RemoveAt(index);
            ShowLabelsSuggestion();
        }

        private string TimeToSuggestionStr(TimeSpan startTime, TimeSpan endTime)
        {
            //For 2 digit-format
            var startHour = (startTime.Hours < 10) ? ("0" + startTime.Hours) : ("" + startTime.Hours);
            var startMinute = (startTime.Minutes < 10) ? ("0" + startTime.Minutes) : ("" + startTime.Minutes);
            var endHour = (endTime.Hours < 10) ? ("0" + endTime.Hours) : ("" + endTime.Hours);
            var endMinute = (endTime.Minutes < 10) ? ("0" + endTime.Minutes) : ("" + endTime.Minutes);

            var startTimeStr = startHour + ":" + startMinute;
            var endTimeStr = endHour + ":" + endMinute;
            return startTimeStr + " - " + endTimeStr;
        }

        //Key for Hash-Table lookup
        private string ConvertDatetimeToKey(DateTime d)
        {
            return "" + d.Year + d.Month + d.Day;
        }
    }
}