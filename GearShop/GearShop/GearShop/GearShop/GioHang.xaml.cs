using GearShop.ClassGear;
using GearShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GearShop
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GioHang : ContentPage
	{
        public GearViewModel viewModel;
        public decimal? Tong { get; } = 0;

        public GioHang ()
		{
			InitializeComponent ();
       
        }
        public GioHang(ObservableCollection<KeyBoard> keyboards)
        {
            InitializeComponent();
            this.BindingContext = viewModel = new GearViewModel();
            Init2(keyboards);
            Listcpu.ItemsSource = viewModel.KeyBoards;
            for(int i=0;i<viewModel.KeyBoards.Count;i++)
            {
                Tong = Tong + viewModel.KeyBoards[i].Price;
            }
            All.Text = String.Format("{0:0,0 đ}", (decimal)Tong);
        }

        public void Init2(ObservableCollection<KeyBoard> keyboard)
        {
            viewModel.KeyBoards = keyboard;
        }

        public GioHang(ObservableCollection<HeadPhone> headphones)
        {
            InitializeComponent();
            this.BindingContext = viewModel = new GearViewModel();
            Init1(headphones);
            Listcpu.ItemsSource = viewModel.HeadPhones;
            for (int i = 0; i < viewModel.HeadPhones.Count; i++)
            {
                Tong = Tong + viewModel.HeadPhones[i].Price;
            }
            All.Text = String.Format("{0:0,0 đ}", (decimal)Tong);
        }

        public async void Init1(ObservableCollection<HeadPhone> headPhones)
        {
            viewModel.HeadPhones = headPhones;
        }

        public GioHang(ObservableCollection<CPU> cpus)
        {
            InitializeComponent();
            this.BindingContext = viewModel = new GearViewModel();
            Init(cpus);
            Listcpu.ItemsSource = viewModel.CPUs;
            for (int i = 0; i < viewModel.CPUs.Count; i++)
            {
                Tong = Tong + viewModel.CPUs[i].Price;
            }
            All.Text = String.Format("{0:0,0 đ}", (decimal)Tong);
        }
        public async void Init(ObservableCollection<CPU> cPU)
        {
            viewModel.CPUs = cPU;
        }
    }
}