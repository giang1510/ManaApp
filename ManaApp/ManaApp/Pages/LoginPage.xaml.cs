using ManaApp.InterfaceCrossPlatform;
using ManaApp.Model;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ManaApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

        private void SignUp(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new RegisterPage());
        }

        private async void Login(object sender, EventArgs e)
        {
            IRestService service = new RestService();
            var user = new User
            {
                username = usernameEntry.Text,
                password = passwordEntry.Text
            };
            LoginResponse loginResponse = await service.Login(user);
            messageLabel.Text = loginResponse.message;
        }
    }
}