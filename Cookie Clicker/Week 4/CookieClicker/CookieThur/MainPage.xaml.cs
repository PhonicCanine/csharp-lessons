using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace CookieThur
{
    public partial class MainPage : ContentPage
    {
        int autoClickerLevel = 0;
        const double ticks = 100.0;
        public MainPage()
        {
            InitializeComponent();
            var t = new Task(() =>
            {
                while (true)
                {
                    DateTime start = DateTime.Now;
                    while ((DateTime.Now - start).TotalSeconds < 1.0/ticks)
                    {
                        //do nothing
                    }
                    cookies = cookies + autoClickerLevel / ticks;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        UpdateDisplay();
                        SaveGame();
                    });
                }
            });
            t.Start();
            LoadGame();
        }

        double cookies = 0;
        int cookiesPerClick = 1;

        void SaveGame()
        {
            Preferences.Set("cookies", cookies);
            Preferences.Set("cookiesPerClick", cookiesPerClick);
            Preferences.Set("autoclickerLevel", autoClickerLevel);
        }

        void LoadGame()
        {
            cookies = Preferences.Get("cookies", 0.0);
            cookiesPerClick = Preferences.Get("cookiesPerClick", 1);
            autoClickerLevel = Preferences.Get("autoclickerLevel", 0);
            UpdateDisplay();
        }

        void UpdateDisplay()
        {
            UpgradeButton.Text = "Upgrade (" + (5 * cookiesPerClick).ToString() + ")";
            AutoClickerButton.Text = "Buy Autoclicker (" + (5 * autoClickerLevel * autoClickerLevel) + ")";
            CookieLabel.Text = Math.Truncate(cookies).ToString();
        }

        void CookieClicked(object sender, System.EventArgs e)
        {
            cookies = cookies + cookiesPerClick;
            UpdateDisplay();
            SaveGame();
        }

        void UpgradeClicked(object sender, System.EventArgs e)
        {
            if (cookies >= 5 * cookiesPerClick)
            {
                cookies = cookies - 5 * cookiesPerClick;
                cookiesPerClick = cookiesPerClick + 1;
                UpdateDisplay();
            }
        }

        void AutoclickerUpgradeClicked(object sender, System.EventArgs e)
        {
            if (cookies > 5 * autoClickerLevel * autoClickerLevel)
            {
                cookies = cookies - 5 * autoClickerLevel * autoClickerLevel;
                autoClickerLevel += 1;
                UpdateDisplay();
            }
        }

        void ResetClicked(object sender, System.EventArgs e)
        {
            cookies = 0;
            cookiesPerClick = 1;
            autoClickerLevel = 0;
            UpdateDisplay();
        }
    }
}
