using Syncfusion.Maui.Charts;
using Android.App;
using Android.OS;
using Syncfusion.Maui.Core.Hosting;
using Microsoft.Maui.Platform;
using Microsoft.Maui.Controls.Embedding;

namespace AndroidEmbeddedApp
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder.UseMauiEmbeddedApp<Microsoft.Maui.Controls.Application>();
            builder.ConfigureSyncfusionCore();

            MauiApp mauiApp = builder.Build();
            MauiContext _mauiContext = new MauiContext(mauiApp.Services, this);

            SfCartesianChart chart = new SfCartesianChart();

            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Title = new ChartAxisTitle
            {
                Text = "Name",
            };
            chart.XAxes.Add(primaryAxis);

            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Title = new ChartAxisTitle
            {
                Text = "Height(in cm)",
            };
            secondaryAxis.Maximum = 220;
            chart.YAxes.Add(secondaryAxis);

            ColumnSeries series = new ColumnSeries();
            series.ShowDataLabels = true;
            series.ItemsSource = (new ViewModel()).Data;
            series.XBindingPath = "Name";
            series.YBindingPath = "Height";

            chart.Series.Add(series);

            Android.Views.View mauiView = chart.ToPlatform(_mauiContext);
            SetContentView(mauiView);

        }
    }
}