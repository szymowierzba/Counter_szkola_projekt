
using System;
using System.Collections.Generic;
using Counter.Models;
using Counter.Services;

namespace Counter.Views
{
    public partial class MainPage : ContentPage
    {
        List<CounterData> counters = new List<CounterData>();

        CounterStorage storage = new CounterStorage();

        public MainPage()
        {
            InitializeComponent();
            LoadCounters();
        }

        private void AddCounter_Clicked(object sender, EventArgs e)
        {
            string name = CounterNameEntry.Text;

            if (string.IsNullOrWhiteSpace(name))
            {
                DisplayAlert("Błąd", "Podaj nazwę licznika", "OK");
                return;
            }

            CounterData counter = new CounterData();
            counter.Name = name;
            counter.Value = 0;

            AddCounterToScreen(counter);

            CounterNameEntry.Text = "";
        }

        private void AddCounterToScreen(CounterData counter)
        {
            counters.Add(counter);

            Label nameLabel = new Label
            {
                Text = counter.Name,
                TextColor = Colors.Black,
                FontSize = 18,
                VerticalOptions = LayoutOptions.Center
            };

            Label valueLabel = new Label
            {
                Text = counter.Value.ToString(),
                TextColor = Colors.Black,
                FontSize = 18,
                VerticalOptions = LayoutOptions.Center
            };

            Button minusButton = new Button
            {
                Text = "-",
                TextColor = Colors.Black,
                BackgroundColor = Colors.LightGray
            };

            Button plusButton = new Button
            {
                Text = "+",
                TextColor = Colors.Black,
                BackgroundColor = Colors.LightGray
            };

            minusButton.Clicked += (s, e) =>
            {
                counter.Value--;
                valueLabel.Text = counter.Value.ToString();
            };

            plusButton.Clicked += (s, e) =>
            {
                counter.Value++;
                valueLabel.Text = counter.Value.ToString();
            };

            HorizontalStackLayout row = new HorizontalStackLayout
            {
                Spacing = 10,
                Children =
                {
                    nameLabel,
                    minusButton,
                    valueLabel,
                    plusButton
                }
            };

            CountersLayout.Children.Add(row);
        }

        private void SaveCounters_Clicked(object sender, EventArgs e)
        {
            storage.SaveCounters(counters);

            DisplayAlert("Zapisano", "Liczniki zostały zapisane!", "OK");
        }

        private void LoadCounters()
        {
            List<CounterData> savedCounters = storage.LoadCounters();

            foreach (CounterData counter in savedCounters)
            {
                AddCounterToScreen(counter);
            }
        }
    }
}
