using GearShop.ClassGear;
using GearShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GearShop
{
    public partial class CPUPage : ContentPage
    {
        public GearList viewModels;
        public CPUPage()
        {
            InitializeComponent();
            this.BindingContext = viewModels = new GearList();
            CPUlist.ItemsSource = viewModels.cPUs;
            CreateButton();
        }

        private async void Timkiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)FindByName("Timkiem");
            string keyword = searchBar.Text.ToLower();
            if (string.IsNullOrEmpty(keyword) == false)
            {
                viewModels.cPUs.Clear();
                viewModels.keyword = keyword;
                await viewModels.GetCPU();
            }
        }
        public async void CreateButton()
        {
            await viewModels.GetBrand();          
                    Button bt = new Button();
                    bt.CommandParameter = viewModels.Brands[1].Id;
                    bt.Text = viewModels.Brands[1].Name;
                    bt.HeightRequest = 25;
                    bt.WidthRequest = 120;
                    bt.BorderWidth = 1;
                    bt.Margin = new Thickness(10, 0, 0, 0);
                    bt.BackgroundColor = Color.White;
                    bt.BorderColor = Color.Gray;
            bt.Clicked += Bam;
            bt.TextColor = Color.Gray;
                    Brand.Children.Add(bt);
            Button bt1 = new Button();
            bt1.CommandParameter = viewModels.Brands[5].Id;
            bt1.Text = viewModels.Brands[5].Name;
            bt1.HeightRequest = 25;
            bt1.WidthRequest = 120;
            bt1.BorderWidth = 1;
            bt1.Margin = new Thickness(10, 0, 0, 0);
            bt1.BackgroundColor = Color.White;
            bt1.BorderColor = Color.Gray;
            bt1.Clicked += Bam;
            bt1.TextColor = Color.Gray;
            Brand.Children.Add(bt1);
        }
        async void Bam(object sender, EventArgs e)
        {
            var bt = sender as Button;
            var bt1 = Brand.Children;
            viewModels.page = 1;
            foreach (Button button in bt1)
            {
                button.BackgroundColor = Color.White;
                button.TextColor = Color.Gray;

            }
            bt.BackgroundColor = Color.Black;
            bt.TextColor = Color.White;
            //bt.HorizontalOptions = LayoutOptions.CenterAndExpand;
            //await scroll.ScrollToAsync((Button)bt, ScrollToPosition.Center, true);
            Guid brandId = Guid.Parse(bt.CommandParameter.ToString());
            viewModels.cPUs.Clear();
            viewModels.guid = brandId;
            await viewModels.GetCPU();
        }

        private async void Loc_Clicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Lọc theo", "Hủy", null, "Giá thấp dần", "Giá tăng dần", "Tên (A-Z)", "Tên (Z-A)");
            switch(action)
            {
                case "Hủy":
                    break;
                case "Giá thấp dần":
                    {
                        CPUlist.ItemsSource = viewModels.cPUs.OrderByDescending(x => x.Price);                     
                        break;
                    }
                case "Giá tăng dần":
                    {
                        CPUlist.ItemsSource = viewModels.cPUs.OrderBy(x => x.Price);
                        break;
                    }
                case "Tên (A-Z)":
                    {
                        CPUlist.ItemsSource = viewModels.cPUs.OrderBy(x => x.Name);                   
                        break;
                    }
                case "Tên (Z-A)":
                    {
                        CPUlist.ItemsSource = viewModels.cPUs.OrderByDescending(x => x.Name);
                        break;
                    }
            }
        }

        private  void Buy_Clicked(object sender, EventArgs e)
        {
            var buttonSender = (Button)sender;
            CPU cPU = (buttonSender.CommandParameter as CPU);
            viewModels.cPUsChoose.Add(cPU);          
        }

        private async void GotoGH_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GioHang(viewModels.cPUsChoose));
        }

        private async void CPUlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            CPU selected = e.Item as CPU;
            await Navigation.PushAsync(new CPUInfo(selected));
        }
    }
}
