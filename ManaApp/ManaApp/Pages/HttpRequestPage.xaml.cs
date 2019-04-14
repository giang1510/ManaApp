﻿using ManaApp.InterfaceCrossPlatform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ManaApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HttpRequestPage : ContentPage
	{
		public HttpRequestPage ()
		{
			InitializeComponent ();
		}

        private async void HttpRequestClicked(Object sender, EventArgs e)
        {
            IRestService service = new RestService();
            string result = await service.RefreshDataAsync();
            httpResponseView.Text = result;
        }

        private async void ToPageClicked(Object sender, EventArgs e)
        {
            IRestService service = new RestService();
            string result = await service.GetPage(relativePath.Text);
            httpResponseView.Text = result;
        }
    }
}