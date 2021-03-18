using GearShop.ClassGear;
using GearShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GearShop
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class KeyBoardInfo : ContentPage
	{
        public GearViewModel viewModel;
        public KeyBoardInfo ()
		{
			InitializeComponent ();
		}
        public KeyBoardInfo(KeyBoard keyBoard)
        {
            InitializeComponent();
            this.BindingContext = viewModel = new GearViewModel();
            Init(keyBoard);
        }
        public async void Init(KeyBoard _keyboard)
        {
            viewModel.keyBoard = _keyboard;
            await viewModel.GetBrand();
            viewModel.keyBoard.Brands = viewModel.Brandss.Single(x => x.Id == _keyboard.BrandId);
        }
    }
}