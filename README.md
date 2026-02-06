# Android Native Embedding for .NET MAUI Chart
To create a [.NET MAUI Cartesian chart](https://www.syncfusion.com/maui-controls/maui-cartesian-charts) in a native embedded Android application, you need to follow a series of steps. This article will guide you through the process.
### Step 1: Create a new .NET Android application
Create a new .NET Android application in Visual Studio.
Syncfusion .NET MAUI components are available in [nuget.org](https://www.nuget.org/). To add SfCartesianChart to your project, open the NuGet package manager in Visual Studio and search for Syncfusion.Maui.Charts, then install it.
### Step 2: Enable .NET MAUI support
First, enable .NET MAUI support in the native app's project file. Enable support by adding `<usemaui>` true `</usemaui>` to the `<propertygroup>` node in the project file.

**[XAML]**
```
<PropertyGroup>
 . . .
   <Nullable>enable</Nullable>
   <UseMaui>true</UseMaui>
   <ImplicitUsings>enable</ImplicitUsings>
 . . .
</PropertyGroup>
```
### Step 3: Initialize .NET MAUI
The pattern for initializing .NET MAUI in a native app project is to create a MauiAppBuilder object and invoke the UseMauiEmbeddedApp method. Additionally, configure it to set up SyncfusionCore components within the .NET MAUI app.

**[C#]**
```
protected override void OnCreate(Bundle? savedInstanceState)
{
     base.OnCreate(savedInstanceState);
     MauiAppBuilder builder = MauiApp.CreateBuilder();
     builder.UseMauiEmbeddedApp<microsoft.maui.controls.application>();
     builder.ConfigureSyncfusionCore();
}
```
Then, create a MauiApp object by invoking the Build() method on the MauiAppBuilder object. In addition, a MauiContext object should be made from the MauiApp object. The MauiContext object will be used when converting .NET MAUI controls to native types.

**[C#]**
```
protected override void OnCreate(Bundle? savedInstanceState)
{
     base.OnCreate(savedInstanceState);
     MauiAppBuilder builder = MauiApp.CreateBuilder();
     builder.UseMauiEmbeddedApp<microsoft.maui.controls.application>();
     builder.ConfigureSyncfusionCore();
     MauiApp mauiApp = builder.Build();
     MauiContext _mauiContext = new MauiContext(mauiApp.Services, this);
}
```
### Step 4: Initialize cartesian chart
Now, let us define a simple data model representing a data point in the chart.

**[C#]**
```
public class Person
{
    public string Name { get; set; }
    public double Height { get; set; }
}
 
```
Next, create a view model class and initialize a list of Person objects as shown in the following.


**[C#]**
```
public class ViewModel
{
    public List<person> Data { get; set; }
 
    public ViewModel()
    {
        Data = new List<person>()
        {
            new Person { Name = "David", Height = 170 },
            new Person { Name = "Michael", Height = 96 },
            new Person { Name = "Steve", Height = 65 },
            new Person { Name = "Joel", Height = 182 },
            new Person { Name = "Bob", Height = 134 }
        };
    }
}
```
[ChartAxis](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.ChartAxis.html) is used to locate the data points inside the chart area. The [XAxes](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.SfCartesianChart.html?tabs=tabid-1#Syncfusion_Maui_Charts_SfCartesianChart_XAxes) and [YAxes](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.SfCartesianChart.html#Syncfusion_Maui_Charts_SfCartesianChart_YAxes) collection of the chart is used to initialize the axis for the chart.

**[C#]**
```
public class MainActivity : Activity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
     ...
        SfCartesianChart chart = new SfCartesianChart();
 
        CategoryAxis primaryAxis = new CategoryAxis();
        chart.XAxes.Add(primaryAxis);
 
        NumericalAxis secondaryAxis = new NumericalAxis();
        chart.YAxes.Add(secondaryAxis);
   }
}
 
```
As we are going to visualize the comparison of heights in the data model, add [ColumnSeries](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.ColumnSeries.html) to [Series](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.SfCartesianChart.html#Syncfusion_Maui_Charts_SfCartesianChart_Series) property of chart, and then bind the Data property of the above `ViewModel` to the `ColumnSeries.ItemsSource` as follows.

**[C#]**
```
public class MainActivity : Activity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
     ...
        SfCartesianChart chart = new SfCartesianChart();
 
        CategoryAxis primaryAxis = new CategoryAxis();
        chart.XAxes.Add(primaryAxis);
 
        NumericalAxis secondaryAxis = new NumericalAxis();
        chart.YAxes.Add(secondaryAxis);
        
        ColumnSeries series = new ColumnSeries();
        series.Label = "Height";
        series.ShowDataLabels = true;
        series.ItemsSource = (new ViewModel()).Data;
        series.XBindingPath = "Name";
        series.YBindingPath = "Height";
 
        chart.Series.Add(series);
     ...
   }
}
```
### Step 5: Converts the .NET MAUI control to an Android View object
Converts the chart to an Android platform-specific view using the ToPlatform method. Set the content view of the current activity with the mauiView.

**[C#]**
```
protected override void OnCreate(Bundle? savedInstanceState)
{
 ...
     Android.Views.View mauiView = chart.ToPlatform(_mauiContext);
     SetContentView(mauiView);
}
```
### Output
![embedded application android .png](https://support.syncfusion.com/kb/agent/attachment/article/14260/inline?token=eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjE0MzQ0Iiwib3JnaWQiOiIzIiwiaXNzIjoic3VwcG9ydC5zeW5jZnVzaW9uLmNvbSJ9.N2zAabw-dNJeVwEr7DWQgmlYWrjQ9CXi6Tyra4DW5cE)
