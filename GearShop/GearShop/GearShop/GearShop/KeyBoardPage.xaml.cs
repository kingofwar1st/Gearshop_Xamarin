using GearShop.ClassGear;
using GearShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GearShop
{
    public partial class KeyBoardPage : ContentPage
    {
        public GearList viewModels;
        public KeyBoardPage()
        {
            InitializeComponent();
            this.BindingContext = viewModels = new GearList();
            CreateButton();
        }
        public async void CreateButton()
        {
            await viewModels.GetBrand();
            for (int i = 0; i < viewModels.Brands.Count() - 1; i++)
            {
                Button bt = new Button();
                bt.CommandParameter = viewModels.Brands[i].Id;
                bt.Text = viewModels.Brands[i].Name;
                bt.HeightRequest = 25;
                bt.WidthRequest = 120;
                bt.BorderWidth = 1;
                bt.Margin = new Thickness(10, 0, 0, 0);
                bt.BackgroundColor = Color.White;
                bt.BorderColor = Color.Gray;
                bt.TextColor = Color.Gray;
                bt.Clicked += Bam;
                Brand.Children.Add(bt);
            }
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
            bt.HorizontalOptions = LayoutOptions.CenterAndExpand;
            await scroll.ScrollToAsync((Button)bt, ScrollToPosition.Center, true);
            Guid brandId = Guid.Parse(bt.CommandParameter.ToString());
            viewModels.keyBoards.Clear();
            viewModels.guid = brandId;
            await viewModels.GetKeyBoard();
        }
        private async void Timkiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)FindByName("Timkiem");
            string keyword = searchBar.Text.ToLower();
            if (string.IsNullOrEmpty(keyword) == false)
            {
                viewModels.keyBoards.Clear();
                viewModels.keyword = keyword;
                await viewModels.GetKeyBoard();
            }
        }
        private async void Loc_Clicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Lọc theo", "Hủy", null, "Giá thấp dần", "Giá tăng dần", "Tên (A-Z)", "Tên (Z-A)");
            switch (action)
            {
                case "Hủy":
                    break;
                case "Giá thấp dần":
                    {
                        KeyBoardlist.ItemsSource = viewModels.keyBoards.OrderByDescending(x => x.Price);
                        break;
                    }
                case "Giá tăng dần":
                    {
                        KeyBoardlist.ItemsSource = viewModels.keyBoards.OrderBy(x => x.Price);
                        break;
                    }
                case "Tên (A-Z)":
                    {
                        KeyBoardlist.ItemsSource = viewModels.keyBoards.OrderBy(x => x.Name);
                        break;
                    }
                case "Tên (Z-A)":
                    {
                        KeyBoardlist.ItemsSource = viewModels.keyBoards.OrderByDescending(x => x.Name);
                        break;
                    }
            }
        }
        private void Buy_Clicked(object sender, EventArgs e)
        {
            var buttonSender = (Button)sender;
            KeyBoard keyBoard = buttonSender.CommandParameter as KeyBoard;
            viewModels.keyBoardsChoose.Add(keyBoard);
        }
        private async void GotoGH_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GioHang(viewModels.keyBoardsChoose));
        }
        private async void CPUlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            KeyBoard selected = e.Item as KeyBoard;
            await Navigation.PushAsync(new KeyBoardInfo(selected));
        }
    }
}