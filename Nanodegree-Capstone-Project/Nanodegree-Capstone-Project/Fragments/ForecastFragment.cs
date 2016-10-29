using System;
using Android.OS;
using Android.App;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Zacher.Model;

namespace Zacher.Fragments
{
    public class ForecastFragment : Fragment, SwipeRefreshLayout.IOnRefreshListener
    {
        public const int Title = Resource.String.fragment_title_forecast;

        private SwipeRefreshLayout _swipeRefreshContainer;
        private TableLayout _forecastTable;
        private LinearLayout _forecastGraph;

        private ForecastViewModel _viewModel;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            this._viewModel = new ForecastViewModel(this.Context);

            this.FetchData();
        }

        // ReSharper disable once UnusedMember.Global
        public static ForecastFragment NewInstance()
        {
            return new ForecastFragment { Arguments = new Bundle() };
        }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.forecast_fragment, null);

            // get the child views
            this._swipeRefreshContainer = view.FindViewById<SwipeRefreshLayout>(Resource.Id.SwipeRefresh);
            this._forecastTable = view.FindViewById<TableLayout>(Resource.Id.ForecastTable);
            this._forecastGraph = view.FindViewById<LinearLayout>(Resource.Id.ForecastGraph);

            // listen for a swipe to refresh event
            this._swipeRefreshContainer.SetOnRefreshListener(this);

            this.RedrawForecastTable();

            return view;
        }

        private void RedrawForecastTable()
        {
            // clear the old rows
            this._forecastTable.RemoveAllViews();

            // add a header row
            var row = new TableRow(this.Context)
            {
                Background = new ColorDrawable(this.Resources.GetColor(Resource.Color.accent, this.Activity.Theme)),
            };
            var labels = new[]
            {
                this.GetString(Resource.String.forecast_table_header_time),
                this.GetString(Resource.String.forecast_table_header_temp),
                this.GetString(Resource.String.forecast_table_header_rain_chance),
                this.GetString(Resource.String.forecast_table_header_rain_amount)
            };
            foreach (var str in labels)
            {
                var text = new TextView(this.Context) {Text = str};
                text.SetTextAppearance(Android.Resource.Style.TextAppearanceMedium);
                row.AddView(text);
            }
            this._forecastTable.AddView(row);

            // get the display density
            var rowBorderPadding = (int)Math.Round(this.Resources.DisplayMetrics.Density * 4f + 0.5f);

            // build the forecast table
            var i = 0;
            foreach (var dataRow in this._viewModel.Rows)
            {
                if (i > 0)
                {
                    // add a nice separator line between rows
                    // don't do it on the first row so we get it inbetween all the rows
                    row = new TableRow(this.Context)
                    {
                        Background = new ColorDrawable(new Color(0xBD, 0xBD, 0xBD)),
                        LayoutParameters = new TableLayout.LayoutParams() { Height = 1 }
                    };

                    var v = new View(this.Context);
                    v.SetPadding(0, rowBorderPadding, 0, rowBorderPadding);
                    row.AddView(v, new TableRow.LayoutParams
                    {
                        Span = 4,
                        Height = 1
                    });

                    this._forecastTable.AddView(row);
                }
                i++;

                row = new TableRow(this.Context);

                // add all of the columns
                foreach (var property in dataRow.GetType().GetProperties())
                {
                    var text = new TextView(this.Context)
                    {
                        Text = (string)property.GetValue(dataRow)
                    };
                    
                    text.SetTextAppearance(Android.Resource.Style.TextAppearanceMedium);
                    row.AddView(text);
                }

                this._forecastTable.AddView(row);
            }
        }

        private void FetchData()
        {
            this._viewModel.Rows.Clear();

            // dummy data
            // todo - fetch data from service
            this._viewModel.AddRow("9am", 11, 100, 11, true);
            this._viewModel.AddRow("10am", 20, 90, 101, true);
            this._viewModel.AddRow("11am", 111, 11, 111, false);
            this._viewModel.AddRow("12am", 111, 11, 111, true);
            this._viewModel.AddRow("1pm", 1, 1, 1, true);
            this._viewModel.AddRow("2pm", 1, 1, 1, true);
            this._viewModel.AddRow("3pm", 1, 1, 1, true);
            this._viewModel.AddRow("4pm", 1, 1, 1, true);
            this._viewModel.AddRow("5pm", 1, 1, 1, true);
            this._viewModel.AddRow("6pm", 1, 1, 1, true);
            this._viewModel.AddRow("7pm", 1, 1, 1, true);
            this._viewModel.AddRow("8pm", 1, 1, 1, true);
            this._viewModel.AddRow("9pm", 1, 1, 1, true);
            this._viewModel.AddRow("10pm", 1, 1, 1, true);
            this._viewModel.AddRow("11pm", 1, 1, 1, true);

            if (this._swipeRefreshContainer != null)
            {
                this._swipeRefreshContainer.Refreshing = false;
            }
        }

        public void OnRefresh()
        {
            this.FetchData();
        }
    }
}