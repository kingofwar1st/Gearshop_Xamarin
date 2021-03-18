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
	public partial class AddGear : ContentPage
	{
        public AddGear()
        {
            InitializeComponent();
            TapGestureRecognizer tap= new TapGestureRecognizer();
            tap.Tapped += (object sender, EventArgs e) =>
            {
                Navigation.PushAsync(new BanGear());
            };
            cpu.GestureRecognizers.Add(tap);
            TapGestureRecognizer tap1 = new TapGestureRecognizer();
            tap1.Tapped += (object sender, EventArgs e) =>
            {
                Navigation.PushAsync(new BanHeadPhone());
            };
            hp.GestureRecognizers.Add(tap1);
            TapGestureRecognizer tap2 = new TapGestureRecognizer();
            tap2.Tapped += (object sender, EventArgs e) =>
            {
                Navigation.PushAsync(new BanKeyBoard());
            };
            kb.GestureRecognizers.Add(tap2);
         }
	}
}