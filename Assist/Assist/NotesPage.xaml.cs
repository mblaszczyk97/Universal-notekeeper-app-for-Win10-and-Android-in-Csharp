using Assist.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Assist
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotesPage : ContentPage
	{
        public string MyProperty { get; } = "MY TEXT";

        public NotesPage ()
		{
			InitializeComponent ();
        }




        protected override void OnAppearing()
        {
            base.OnAppearing();

            var notes = new List<Note>();

            var files = Directory.EnumerateFiles(App.FolderPath, "*.notes.txt");
            foreach (var filename in files)
            {
                notes.Add(new Note
                {
                    Filename = filename,
                    Text = File.ReadAllText(filename),
                    Date = File.GetCreationTime(filename)
                });
            }
            listView.ItemsSource = notes
                .OrderBy(d => d.Date)
                .ToList();

            var batteryInfo = new List<Note>();
            string batteryStatus;
                var bat = DependencyService.Get<IBattery>();

                switch (bat.PowerSource)
                {
                    case PowerSource.Battery:
                    batteryStatus = "Bateria - ";
                        break;
                    case PowerSource.Ac:
                    batteryStatus = "Podłączony - ";
                        break;
                    case PowerSource.Usb:
                    batteryStatus = "USB - ";
                        break;
                    case PowerSource.Wireless:
                    batteryStatus = "Nie Podłączony - ";
                        break;
                    case PowerSource.Other:
                    default:
                    batteryStatus = "Inne - ";
                        break;
                }
                switch (bat.Status)
                {
                    case BatteryStatus.Charging:
                    batteryStatus += "Ładowanie";
                        break;
                    case BatteryStatus.Discharging:
                    batteryStatus += "Rozładowywanie";
                        break;
                    case BatteryStatus.NotCharging:
                    batteryStatus += "Nie ładuje";
                        break;
                    case BatteryStatus.Full:
                    batteryStatus += "Pełna Bateria";
                        break;
                    case BatteryStatus.Unknown:
                    default:
                    batteryStatus += "Nie wiem";
                        break;
                }

            batteryInfo.Add(new Note
            {
                Filename = "1",
                Text = batteryStatus,
                Date = new DateTime()
            });
            batteryView.ItemsSource = batteryInfo
                .OrderBy(d => d.Date)
                .ToList();

        }

        async void OnNoteAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NoteEntryPage
            {
                BindingContext = new Note()
            });
        }

        async void OnHour(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClockPage
            {
                BindingContext = new Note()
            });
        }

        async void OnCurrency(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new KeypadPage
            {
                BindingContext = new Note()
            });
        }

        async void OnPerson(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PersonEntryPage
            {
                BindingContext = new PersonCollectionViewModel()
            });
        }

        async void OnPersonList(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Persons
            {
                BindingContext = new PersonViewModel()
            });
        }

        async void OnBatteryCheck(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BatteryInfo
            {
                BindingContext = new BatteryInfo()
            });
        }

        async void OnWeather(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WeatherCheck
            {
                BindingContext = new WeatherData()
            });
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new NoteEntryPage
                {
                    BindingContext = e.SelectedItem as Note
                });
            }
        }
    }
}