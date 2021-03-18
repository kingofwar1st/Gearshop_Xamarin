using GearShop.ClassGear;
using GearShop.Configuration;
using GearShop.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GearShop
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        public UserViewModel viewModel;
		public Login ()
		{
			InitializeComponent ();
            Label dn = (Label)FindByName("Dangky");
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (object sender, EventArgs e) =>
            {
                Navigation.PushAsync(new SignUp());
            };
            dn.GestureRecognizers.Add(tap);
            this.BindingContext = viewModel = new UserViewModel();
            viewModel.UserInformation = new UserInformation();
        }

        private async void DangNhap_Clicked(object sender, EventArgs e)
        {
            var buttonSender = (Button)sender;
            bool dk = false;
            if (string.IsNullOrWhiteSpace(Mail.Text) && string.IsNullOrWhiteSpace(PassW.Text))
            {
                dk = true;
            }
            if (string.IsNullOrWhiteSpace(Mail.Text))
            {
                dk = true;
            }
            if (string.IsNullOrWhiteSpace(PassW.Text))
            {
                dk = true;
            }
            if (dk == false)
            { 
                viewModel.UserInformation.Pass= PassW.Text;
                viewModel.UserInformation.Sdt = Mail.Text;
                viewModel.UserInformation.Email = Mail.Text;
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ApiConfig.API_URL);
                var Json = JsonConvert.SerializeObject(viewModel.UserInformation);
                var content = new StringContent(Json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(string.Format($"api/user/CheckUser"), content);
                ApiResponse users = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());               
                UserInformation userInformation = JsonConvert.DeserializeObject<UserInformation>(users.Content.ToString());
                Settings.Email = userInformation.Email;
                Settings.HoTen = userInformation.HoTen;
                Settings.Sdt = userInformation.Sdt;
                Settings.Pass = userInformation.Pass;
                if (users.Success == true)
                {
                    await Navigation.PushAsync(new Profile());
                }
                else
                {
                    await DisplayAlert("Thông báo", "Bạn đã đăng nhập thất bại", "Ok");
                }
            }
        
        }
    }
}