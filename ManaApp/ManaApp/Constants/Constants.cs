using ManaApp.InterfaceCrossPlatform;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ManaApp
{
    public static class Constants
    {
        //Constants for schedule view
        public static List<string> viewNames = new List<string>{ "Day View", "Week View", "Month View" };
        public const int dayViewPos = 0;
        public const int weekViewPos = 1;
        public const int monthViewPos = 2;

        //User constants
        public const string USER_USERNAME = "username";
        public const string USER_NAME = "name";
        public const string USER_EMAIL = "email";
        public const string USER_PASSWORD = "password";
    }
}
