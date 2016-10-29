using System.Collections.ObjectModel;
using Android.Content;
using JetBrains.Annotations;

namespace Zacher.Model
{
    public class ForecastViewModel
    {
        private readonly bool _isMetric;

        public ObservableCollection<ForecastRow> Rows { get; } = new ObservableCollection<ForecastRow>();

        public ForecastViewModel(Context ctx)
        {
            var settings = new SettingsModel(ctx);
            this._isMetric = settings.IsMetric;
        }

        public void AddRow(string time, decimal temp, decimal rainChance, decimal rainAmount, bool inputIsMetric)
        {
            string tempStr, rainAmountStr;

            // convert?
            if (inputIsMetric && !this._isMetric)
            {
                // from metric to imperial
                temp = temp * 1.8m + 32m;
                rainAmount = rainAmount / 25.4m;
            }
            else if (!inputIsMetric && this._isMetric)
            {
                // from imperial to metric
                temp = (temp - 32m) * 5m / 9m;
                rainAmount = rainAmount * 25.4m;
            }

            // build the UI strings
            if (this._isMetric)
            {
                tempStr = $"{temp:0.#}°C";
                rainAmountStr = $"{rainAmount:0}mm";
            }
            else
            {
                tempStr = $"{temp:0.#}°F";
                rainAmountStr = $"{rainAmount:0.#}in";
            }

            // add the row
            this.Rows.Add(new ForecastRow
            {
                Time = time,
                Temperature = tempStr,
                RainChance = $"{rainChance:##0}%",
                RainAmount = rainAmountStr
            });
        }

        public class ForecastRow
        {
            public string Time { [UsedImplicitly] get; set; }
            public string Temperature { [UsedImplicitly] get; set; }
            public string RainChance { [UsedImplicitly] get; set; }
            public string RainAmount { [UsedImplicitly] get; set; }
        }
    }
}