using ElectronicsDatabase.Models;
using ElectronicsDatabase.Models.ViewModels;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ElectronicsDatabase.Pages;

public partial class PartListPage : ContentPage
{
    private PartsViewModel partsViewModel;
    public PartListPage()
	{
		InitializeComponent();
        partsViewModel = new PartsViewModel();
        BindingContext = new PartsViewModel();

	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            await partsViewModel.GetPartDetailsAsync();

            // Now, check the size of the Parts collection
            Debug.WriteLine("Count from Init");
            Debug.WriteLine(partsViewModel.Parts.Count);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
		var part = (Part)e.Item;
		var partDetailViewModel = new PartDetailViewModel { Part = part};
		var partDetailPage = new PartDetailPage();
		partDetailPage.BindingContext = partDetailViewModel;

		Navigation.PushAsync(partDetailPage);

    }
}