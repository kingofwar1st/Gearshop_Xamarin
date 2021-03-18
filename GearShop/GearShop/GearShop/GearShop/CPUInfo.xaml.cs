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
	public partial class CPUInfo : ContentPage
	{
        public GearViewModel viewModel;
		public CPUInfo ()
		{
            InitializeComponent();
        }
        public CPUInfo(CPU cpu)
        {
            InitializeComponent();
            this.BindingContext = viewModel = new GearViewModel();
            Init(cpu);
        }
        public async void Init(CPU _cpu)
        {
            viewModel.CPU = _cpu;
            await viewModel.GetBrand();           
            viewModel.CPU.Brands = viewModel.Brandss.Single(x => x.Id == _cpu.BrandId);
        }
    }
}