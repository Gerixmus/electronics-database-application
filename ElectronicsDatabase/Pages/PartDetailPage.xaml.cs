using ElectronicsDatabase.Models.ViewModels;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Json;

namespace ElectronicsDatabase.Pages;

public partial class PartDetailPage : ContentPage
{
	public PartDetailPage()
	{
        InitializeComponent();    
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
/*
        var partDetailViewModel = new PartDetailViewModel
        {
            id = 0,
            name = "Loading",
            amount = 0,
            description = "Loading"
        };

        BindingContext = partDetailViewModel;

        await GetPartDetailsAsync();*/
    }
    private async Task GetPartDetailsAsync()
    {
        try
        {
            Debug.WriteLine("Sending request");

            var apiUrl = $"http://localhost:53432/api/Part/1";
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();

                Debug.WriteLine(jsonContent);

                // Deserialize the JSON string to PartDetailViewModel
                var partDetailViewModel = JsonConvert.DeserializeObject<PartDetailViewModel>(jsonContent);

                // Set the deserialized PartDetailViewModel as the binding context
                BindingContext = partDetailViewModel;

                Debug.WriteLine("Response deserialized successfully");
            }
            else
            {
                Debug.WriteLine($"HTTP request failed with status code: {response.StatusCode}");
            }
        }
        catch (HttpRequestException ex)
        {
            Debug.WriteLine($"HTTP request failed. Exception details: {ex.Message}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}