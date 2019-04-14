
using Android.App;
using Android.Widget;
using ManaApp.InterfaceCrossPlatform;

[assembly: Xamarin.Forms.Dependency(typeof(FormApp2.Droid.TextToSpeech))]
namespace FormApp2.Droid
{
    class TextToSpeech : ITextToSpeech
    {
        public void speak(string text)
        {
            Toast.MakeText(Application.Context, text, ToastLength.Long).Show();
        }
    }
}