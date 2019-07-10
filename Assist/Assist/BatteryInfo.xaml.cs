using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Assist
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BatteryInfo : ContentPage
	{
		public BatteryInfo ()
		{
			InitializeComponent ();
            var button = new Button
            {
                Text = "Wyświetl stan baterii",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            button.Clicked += (sender, e) => {
                var bat = DependencyService.Get<IBattery>();

                switch (bat.PowerSource)
                {
                    case PowerSource.Battery:
                        button.Text = "Bateria - ";
                        break;
                    case PowerSource.Ac:
                        button.Text = "Podłączony - ";
                        break;
                    case PowerSource.Usb:
                        button.Text = "USB - ";
                        break;
                    case PowerSource.Wireless:
                        button.Text = "Nie Podłączony - ";
                        break;
                    case PowerSource.Other:
                    default:
                        button.Text = "Inne - ";
                        break;
                }
                switch (bat.Status)
                {
                    case BatteryStatus.Charging:
                        button.Text += "Ładowanie";
                        break;
                    case BatteryStatus.Discharging:
                        button.Text += "Rozładowywanie";
                        break;
                    case BatteryStatus.NotCharging:
                        button.Text += "Nie ładuje";
                        break;
                    case BatteryStatus.Full:
                        button.Text += "Pełna Bateria";
                        break;
                    case BatteryStatus.Unknown:
                    default:
                        button.Text += "Nie wiem";
                        break;
                }
            };
            Content = button;
        }


	}
}