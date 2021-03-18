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
	public partial class HeadPhoneInfo : ContentPage
	{
        public GearViewModel viewModel;
        public HeadPhoneInfo ()
		{
			InitializeComponent ();
		}
        public HeadPhoneInfo(HeadPhone headPhone)
        {
            InitializeComponent();
            this.BindingContext = viewModel = new GearViewModel();
            Init(headPhone);
        }
        public async void Init(HeadPhone _headphone)
        {
            viewModel.headPhone = _headphone;
            await viewModel.GetBrand();
            viewModel.headPhone.Brands = viewModel.Brandss.Single(x => x.Id == _headphone.BrandId);
        }
    }
}