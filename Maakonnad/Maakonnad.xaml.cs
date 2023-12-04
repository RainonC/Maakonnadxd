using Android.Graphics;
using Java.Util.Concurrent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Path = System.IO.Path;

namespace Maakonnad
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Maakonnad : ContentPage
        
    {
        bool edited = true;
        string filename;
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public Maakond Maakond { get; set; }
        public Maakonnad(Maakond maakond)
        {
            InitializeComponent();

            Maakond = maakond;
            if (maakond == null)
            {
                Maakond = new Maakond();
                edited = false;
            }
            this.BindingContext = Maakond;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

            if (edited == false)
            {
                NavigationPage navPage = (NavigationPage)Application.Current.MainPage;
                IReadOnlyList<Page> navStack = navPage.Navigation.NavigationStack;
                MainPage homePage = navStack[navPage.Navigation.NavigationStack.Count - 1] as MainPage;
                if (homePage != null)
                {
                    homePage.AddMaakond(Maakond);
                }
            }
        }

        public async void Salvesta_failisse(object sender, EventArgs e)
        {
            filename = "Maakonnad.txt";
            if (String.IsNullOrEmpty(filename)) return;
            if (File.Exists(Path.Combine(folderPath, filename)))
            {
                bool isRewrited = await DisplayAlert("Kinnitus", "Fail on juba olemas, lisame andmeid sinna?", "Jah", "Ei");
                if (isRewrited == false) return;
            }
            //string text = nimetusEntry.Text + ' ' + pealinnEntry.Text + ' ' + arv_stepper.Value.ToString();
            //File.AppendAllLines(Path.Combine(folderPath, filename), text.Split('\n'));
        }

        private void Loe_failist(object sender, EventArgs e)
        {
            filename = "Maakonnad.txt";
            if (String.IsNullOrEmpty(filename)) return;
            if (filename != null)
            {
                //lbl.Text = File.ReadAllText(Path.Combine(folderPath, filename));
            }
        }

        private void Kustuta_faili(object sender, EventArgs e)
        {
            File.Delete(Path.Combine(folderPath, filename));
        }
    }

    
}