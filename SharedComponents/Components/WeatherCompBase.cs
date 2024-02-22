using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using MudBlazor;

namespace SharedComponents.Components;

public class WeatherCompBase: ComponentBase
{
    [Inject]
    public ILogger<WeatherCompBase>? Logger { get; set; }

    [Inject]
    public ISnackbar? Snackbar { get; set; }     
    
    public string SearchString { get; set; } = "";

    protected bool FilterFunc(WeatherForecast item) => FilterFuncSearch(item, SearchString);
    protected bool FilterFuncSearch(WeatherForecast item, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (item.Summary is not null && item.Summary.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (item.TemperatureC.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (item.TemperatureF.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (item.Date.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if ($"{item.Date} {item.TemperatureC} {item.TemperatureF}".Contains(searchString))
            return true;
        return false;
    }

    protected List<WeatherForecast>? Collection { get; set; } = new();
    public bool IsLoading { get; set; } = false;
    protected int ItemsToShowCount { get; set; } = 500;

    /// <summary>
    /// Main method is called when the component is first rendered.
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        Logger?.LogInformation("OnInitializedAsync started");
        await LoadAsync();
    }

    protected async Task RefreshAsync()
    {        
        SearchString = "";
        await LoadAsync();
    }

    protected async Task LoadAsync()
    {
        Logger?.LogInformation("LoadAsync started with {1} items to load", ItemsToShowCount);
        Collection = new();
        IsLoading = true;
       // StateHasChanged(); // Not always needed, but it's a good practice to call this method after changing the state of the component
        // Simulate asynchronous loading to demonstrate streaming rendering
        await Task.Delay(ItemsToShowCount);

        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
        Collection = Enumerable.Range(1, ItemsToShowCount).Select(index => new WeatherForecast
        {
            Date = startDate.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        }).ToList();

        IsLoading = false;
        Snackbar?.Add($"Loaded {ItemsToShowCount} items", Severity.Success);
        //StateHasChanged(); // Not always needed, but it's a good practice to call this method after changing the state of the component
    }
}

public class WeatherForecast
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
