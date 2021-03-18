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
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (object sender, EventArgs e) =>
            {
                Navigation.PushAsync(new Chat());
            };
            chat.GestureRecognizers.Add(tap);
        }

        private async void Keyboard_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new KeyBoardPage());
        }
        private async void Headphone_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HeadPhonePage());
        }
        private async void CPU_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CPUPage());
        }

    }
}