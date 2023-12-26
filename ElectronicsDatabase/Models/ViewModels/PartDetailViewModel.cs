using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsDatabase.Models.ViewModels
{
    internal partial class PartDetailViewModel : ObservableObject
    {
        [ObservableProperty]
        private Part part;
    }
}
