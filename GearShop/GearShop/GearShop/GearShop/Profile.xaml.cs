using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
            hoten.Text = Settings.HoTen;
            Mail.Text = Settings.Email;
            Sdt.Text = Settings.Sdt;
            Pass.Text = Settings.Pass;
        }
        async void Handle_Clicked(object sender, EventArgs e)
        {
            MultipartFormDataContent form = new MultipartFormDataContent();
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported || !CrossMedia.Current.IsCameraAvailable)
            {
                await DisplayAlert("Thông báo", "Máy bạn không thể chọn ảnh", "Ok");
                return;
            }
            var option = await DisplayActionSheet("Thay đổi ảnh đại diện", "Hủy", null, "Chụp ảnh", "Chọn trong thư viện");
            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };
            if (option == "Hủy")
                return;
            if (option == "Chọn trong thư viện")
            {
                var selectedImage = await CrossMedia.Current.PickPhotoAsync(mediaOptions);
                if (SelectImage == null)
                {
                    await DisplayAlert("Thông báo", "Không thể chọn ảnh", "Ok");
                    return;
                }
                if (selectedImage != null)
                {
                    SelectImage.Source = ImageSource.FromStream(() => selectedImage.GetStream());
                }
            }
            else
            {
                var takeImage = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg"
                });
                if (takeImage != null)
                    SelectImage.Source = ImageSource.FromStream(() =>
                    {
                        var stream = takeImage.GetStream();
                        takeImage.Dispose();
                        return stream;
                    });
            }
        }

        private async void LogOut_Clicked(object sender, EventArgs e)
        {
            Settings.clearEverything();
            await Navigation.PushAsync(new Login());
        }
    }
}