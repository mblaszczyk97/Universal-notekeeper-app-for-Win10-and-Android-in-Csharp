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
	public partial class Persons : ContentPage
	{
		public Persons ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var notes = new List<PersonViewModel>();

            var files = Directory.EnumerateFiles(App.FolderPath, "*.persons.txt");
            foreach (var filename in files)
            {
                notes.Add(new PersonViewModel
                {
                    Name = File.ReadAllText(filename)
                });
            }
            listView.ItemsSource = notes
                .ToList();


        }
    }
}