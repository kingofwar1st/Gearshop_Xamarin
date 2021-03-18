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
	public partial class SignUp : ContentPage
	{

        public UserViewModel viewModel;
        public SignUp()
        {
            InitializeComponent();
            Label dn = (Label)FindByName("Dangnhap");
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (object sender, EventArgs e) =>
            {
                Navigation.PushAsync(new Login());
            };
            dn.GestureRecognizers.Add(tap);
            this.BindingContext = viewModel = new UserViewModel();
            viewModel.UserInformation = new UserInformation();
        }
        private async void NutDangKy(object sender, EventArgs args)
        {
            var buttonSender = (Button)sender;
            bool dk = false;
            if (string.IsNullOrWhiteSpace(hoten.Text) && string.IsNullOrWhiteSpace(email.Text) && string.IsNullOrWhiteSpace(sdt.Text)&&string.IsNullOrWhiteSpace(pass.Text))
            {
                dk = true;
            }
            if (string.IsNullOrWhiteSpace(hoten.Text))
            {
                dk = true;
            }
            if (string.IsNullOrWhiteSpace(pass.Text))
            {
                dk = true;
            }
            if (string.IsNullOrWhiteSpace(sdt.Text))
            {
                dk = true;
            }
            if (string.IsNullOrWhiteSpace(email.Text))
            {
                dk = true;
            }
            if (dk == false)
            {
                viewModel.UserInformation.HoTen = hoten.Text;
                viewModel.UserInformation.Sdt = sdt.Text;
                viewModel.UserInformation.Pass = pass.Text;
                viewModel.UserInformation.Email = email.Text;
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ApiConfig.API_URL);
                var Json = JsonConvert.SerializeObject(viewModel.UserInformation);
                var content = new StringContent(Json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(string.Format($"api/user/AddUser"), content);
                var users = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());
                if (users.Success == true)
                {
                    await DisplayAlert("Thông báo", "Bạn đã đăng ký thành công", "Ok");
                    await this.Navigation.PushAsync(new Profile());
                }
                if (users.Success == false && users.Message == "Sdt&Email")
                    await DisplayAlert("Thông báo", "Bạn đã đăng ký thất bại vi email và số điện thoại đã trùng", "Ok");
                if (users.Success == false && users.Message == "Sdt")
                {
                    await DisplayAlert("Thông báo", "Bạn đã đăng ký thất bại vì số điện thoại đã trùng", "Ok");
                }
                if (users.Success == false && users.Message == "Email")
                {
                    await DisplayAlert("Thông báo", "Bạn đã đăng ký thất bại vì email đã trùng", "Ok");
                }
                if (users.Success == false && users.Message == "EmailFalse")
                {
                    await DisplayAlert("Thông báo", "Bạn đã đăng ký thất bại vì email không hợp lệ", "Ok");
                }
            }
        }
    }
}