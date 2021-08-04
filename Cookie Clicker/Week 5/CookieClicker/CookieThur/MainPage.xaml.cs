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
        const double ticks = 100.0;

        List<Upgrade> upgrades = new List<Upgrade>
        {
            new Upgrade
            {
                Level = 0,
                Rate = 1,
                Price = 5,
                Name = "Autoclicker"
            },
            new Upgrade
            {
                Level = 0,
                Rate = 3,
                Price = 50,
                Name = "Grandma"
            },
        };

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
                    cookies = cookies + upgrades.Select(x => x.Level * x.Rate).Sum() / ticks;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        UpdateDisplay();
                        SaveGame();
                    });
                }
            });
            t.Start();
            LoadGame();
            DrawUpgradeButtons();
        }

        double cookies = 0;
        int cookiesPerClick = 1;

        int GetUpgradeCost(Upgrade upgrade)
        {
            return (upgrade.Level + 1) * (upgrade.Level + 1) * upgrade.Price;
        }

        void DrawUpgradeButtons()
        {
            UpgradeButtons.Children.Clear();
            foreach (var upgrade in upgrades)
            {
                Button b = new Button { Text = $"{upgrade.Name} ({upgrade.cost})"};
                UpgradeButtons.Children.Add(b);
                b.Clicked += GetEventHandler(upgrade);
            }
        }

        int add(int left, int right)
        {
            return left + right;
        }

        Func<int, int> Add(int left) => (int right) => left + right;

        EventHandler GetEventHandler(Upgrade upgrade) => (object sender, EventArgs e) =>
        {
            List<int> numbers = new List<int>();
            numbers.Select(x => x + 1);
            numbers.Select(Add(1));

            if (cookies >= GetUpgradeCost(upgrade))
            {
                cookies -= GetUpgradeCost(upgrade);
                upgrade.Level += 1;
                DrawUpgradeButtons();
            }
        };

        void SaveGame()
        {
            Preferences.Set("cookies", cookies);
            Preferences.Set("cookiesPerClick", cookiesPerClick);
            foreach (var upgrade in upgrades) Preferences.Set(upgrade.Name, upgrade.Level);
        }

        void LoadGame()
        {
            cookies = Preferences.Get("cookies", 0.0);
            cookiesPerClick = Preferences.Get("cookiesPerClick", 1);
            UpdateDisplay();
            foreach (var upgrade in upgrades) upgrade.Level = Preferences.Get(upgrade.Name, 0);
        }

        void ResetClicked(object sender, System.EventArgs e)
        {
            foreach (var upgrade in upgrades) upgrade.Level = 0;
            cookies = 0;
            cookiesPerClick = 1;
            UpdateDisplay();
            DrawUpgradeButtons();
        }

        void UpdateDisplay()
        {
            UpgradeButton.Text = "Upgrade (" + (5 * cookiesPerClick).ToString() + ")";
            CookieLabel.Text = Math.Truncate(cookies).ToString();
        }

        void CookieClicked(object sender, System.EventArgs e)
        {
            cookies = cookies + cookiesPerClick;
            (sender as ImageButton).ScaleTo(1, 30);

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

        void ImageButton_Pressed(System.Object sender, System.EventArgs e)
        {
            int a = 1;
            (sender as ImageButton).ScaleTo(0.8, 30);
        }
    }
}
