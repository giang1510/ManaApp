using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ManaApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage ()
		{
			InitializeComponent ();
		}

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            //if (!checkEntries()) return;

            ////Send user data to server
            //IRestService service = new RestService();
            //User user = new User
            //{
            //    username = usernameEntry.Text,
            //    name = nameEntry.Text,
            //    email = emailEntry.Text,
            //    password = passwordEntry.Text
            //};
            //string result = await service.register(user);
            //messageLabel.Text = result;
        }

        //Check whether entries are empty or have incorrect format
        private bool CheckEntries()
        {
            //if (string.IsNullOrWhiteSpace(usernameEntry.Text))
            //{
            //    messageLabel.Text = "Please enter the username!";
            //    return false;
            //}
            //if (string.IsNullOrWhiteSpace(nameEntry.Text))
            //{
            //    messageLabel.Text = "Please enter your name!";
            //    return false;
            //}
            //if (string.IsNullOrWhiteSpace(emailEntry.Text))
            //{
            //    messageLabel.Text = "Please enter your email!";
            //    return false;
            //}
            //if (IsValidEmail(emailEntry.Text))
            //{
            //    messageLabel.Text = "Your email is not valid!";
            //    return false;
            //}
            //if (string.IsNullOrWhiteSpace(passwordEntry.Text))
            //{
            //    messageLabel.Text = "Please enter a password!";
            //    return false;
            //}
            //if (string.IsNullOrWhiteSpace(confirmPasswordEntry.Text))
            //{
            //    messageLabel.Text = "Please confirm the password!";
            //    return false;
            //}
            //if (!confirmPasswordEntry.Text.Equals(passwordEntry.Text))
            //{
            //    messageLabel.Text = "The passwords do not match!";
            //    return false;
            //}
            return true;
        }

        public bool IsValidEmail(string email)
        {
            string emailRegex = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            return Regex.IsMatch(email, emailRegex);
        }
    }
}