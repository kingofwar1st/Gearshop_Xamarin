using GearShop.ClassGear;
using GearShop.Configuration;
using GearShop.ViewModel;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GearShop
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BanHeadPhone : ContentPage
    {
        public GearViewModel viewModels;
        public BanHeadPhone()
        {
            InitializeComponent();
            this.BindingContext = viewModels = new GearViewModel();
            viewModels.headPhone = new HeadPhone();
            Init();
        }
        public async void Init()
        {
            await viewModels.GetBrand();
        }
        private async void LuuSanPham_Clicked(object sender, EventArgs e)
        {
            bool HasError = false;
            if (Hang.SelectedItem == null && string.IsNullOrWhiteSpace(Ten.Text) && string.IsNullOrWhiteSpace(Gia.Text) && string.IsNullOrWhiteSpace(Tinhtrang.Text) && string.IsNullOrWhiteSpace(Baohanh.Text))
            {
                HasError = true;
            }
            if (Hang.SelectedItem == null)
                HasError = true;
            if (string.IsNullOrWhiteSpace(Ten.Text))
                HasError = true;
            if (string.IsNullOrWhiteSpace(Gia.Text))
                HasError = true;
            if (string.IsNullOrWhiteSpace(Tinhtrang.Text))
                HasError = true;
            if (string.IsNullOrWhiteSpace(Baohanh.Text))
                HasError = true;
            if (HasError)
                return;
            var httpClient = new HttpClient();
            viewModels.headPhone.BrandId = viewModels.headPhone.Brands.Id;
            httpClient.BaseAddress = new Uri(ApiConfig.API_URL);
            var Json = JsonConvert.SerializeObject(viewModels.headPhone);
            var content = new StringContent(Json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(string.Format($"api/headphone/addheadphone"), content);
            await Navigation.PopAsync();
        }
        private async void Hinh_Clicked(object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ApiConfig.API_URL);
            MultipartFormDataContent form = new MultipartFormDataContent();

            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported || !CrossMedia.Current.IsCameraAvailable)
            {
                await DisplayAlert("Thông báo", "Máy bạn không thể chọn ảnh", "Ok");
                return;
            }
            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };
            var option = await DisplayActionSheet("Thay đổi ảnh đại diện", "Hủy", null, "Chụp ảnh", "Chọn ảnh trong thư viện");
            if (option == "Hủy")
                return;
            if (option == "Chọn ảnh trong thư viện")
            {
                var selectedImage = await CrossMedia.Current.PickPhotoAsync(mediaOptions);
                if (selectedImage == null)
                {
                    await DisplayAlert("Thông báo", "Không thể chọn ảnh", "Ok");
                    return;
                }
                if (selectedImage != null)
                {
                    Guid imgName = Guid.NewGuid();
                    var stream = selectedImage.GetStream();
                    var content = new StreamContent(stream);
                    content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "files",
                        FileName = $"{imgName}.jpg"
                    };

                    form.Add(content);
                    HttpResponseMessage response = await httpClient.PostAsync("api/headphone/image/upload", form);
                    string body = await response.Content.ReadAsStringAsync();
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        var imgLink = apiResponse.Content.ToString();
                        viewModels.headPhone.Hinh = imgLink;
                    }
                }
            }
            else
            {
                var takeImage = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg"
                });
                if (takeImage == null)
                {
                    await DisplayAlert("Thông báo", "Không thể chụp ảnh", "Ok");
                    return;
                }
                if (takeImage != null)
                {
                    Guid imgName = Guid.NewGuid();
                    var stream = takeImage.GetStream();
                    takeImage.Dispose();

                    var content = new StreamContent(stream);
                    content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "files",
                        FileName = $"{imgName}.jpg"
                    };

                    form.Add(content);
                    HttpResponseMessage response = await httpClient.PostAsync("api/headphone/image/upload", form);
                    string body = await response.Content.ReadAsStringAsync();
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        var imgLink = apiResponse.Content.ToString();
                        viewModels.headPhone.Hinh = imgLink;
                    }
                }
            }
        }
    }
}