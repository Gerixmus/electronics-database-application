using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsDatabase.Models.ViewModels
{
    internal partial class PartsViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Part> parts = new();

        [ObservableProperty]
        private Part part = new();

        [RelayCommand]
        public async void Add()
        { 
            await PostObjectAsync(Part);
            Part = new();
            await GetPartDetailsAsync();
        }

        public async Task GetPartDetailsAsync()
        {
            try
            {
                Debug.WriteLine("Sending request");

                var apiUrl = $"http://localhost:53432/api/Part";
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonContent = await response.Content.ReadAsStringAsync();

                    Debug.WriteLine(jsonContent);

                    var partsList = JsonConvert.DeserializeObject<List<Part>>(jsonContent);

                    Debug.WriteLine(partsList);

                    Parts.Clear();

                    foreach (Part part in partsList)
                    {
                        Debug.WriteLine(part.name);
                        Parts.Add(part);
                    }

                    Debug.WriteLine(Parts.Count);

                    Debug.WriteLine("Parts retrieved successfully");

                    
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

        private async Task PostObjectAsync(Part part)
        {
            try
            {
                HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:53432/api/") };

                var partWithoutId = new
                {
                    Name = part.name,
                    Amount = part.amount,
                    Description = part.description
                };

                // Convert your object to JSON
                string jsonContent = JsonConvert.SerializeObject(partWithoutId);

                Debug.WriteLine(jsonContent);

                // Create StringContent with JSON data
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Make POST request
                var response = await client.PostAsync("Part", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Object posted successfully");
                }
                else
                {
                    Debug.WriteLine($"HTTP request failed with status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
