using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserManagementApp.Models;
using UserManagementApp.Services;

namespace UserManagementApp.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private readonly IAuthService _authService;
        private readonly IAlertService _alertService;

        public ObservableCollection<Category> Categories { get; }
        public ObservableCollection<FoodItem> AllFoodItems { get; }
        public ObservableCollection<FoodItem> FilteredFoodItems { get; }
            = new ObservableCollection<FoodItem>();

        // 1) new collection for the second row
        public ObservableCollection<FoodItem> TrendingNow { get; }
            = new ObservableCollection<FoodItem>();


        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory == value) return;
                _selectedCategory = value;
                OnPropertyChanged();
                RefreshFilter();
            }
        }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText == value) return;
                _searchText = value;
                OnPropertyChanged();
                RefreshFilter();
            }
        }

        public HomeViewModel(IAuthService authService, IAlertService alertService)
        {
            _authService = authService;
            _alertService = alertService;
            LogoutCommand = new Command(async () => await LogOutAsync());

            Categories = new ObservableCollection<Category>
            {
                new Category { Name = "All" },
                new Category { Name = "Biryani" },
                new Category { Name = "Pizza" },
                new Category { Name = "Chicken" },
                new Category { Name = "Burger" },
            };

            _selectedCategory = Categories.First();

            AllFoodItems = new ObservableCollection<FoodItem>
            {
                new FoodItem {
                    Name = "The Cluckinator",
                    Price = 355,
                    OfferPrice = 295,
                    Rating = 4.3,
                    DeliveryTime = 80,
                    DistanceKm = 17.6,
                    ImageUrl = "https://via.placeholder.com/300x200.png?text=Burger",
                    Tags = new List<string> { "Frequently Reordered", "Low Plastic Packaging" }
                },
                new FoodItem {
                    Name = "Chicken Bucket",
                    Price = 200,
                    Rating = 4.1,
                    DeliveryTime = 45,
                    DistanceKm = 5.2,
                    ImageUrl = "https://via.placeholder.com/300x200.png?text=Chicken",
                    Tags = new List<string> { "New", "Popular" }
                },
                // ...add more items per category
            };

            // 2) populate TrendingNow
            TrendingNow.Add(new FoodItem
            {
                Name = "Fiery Paneer Pizza",
                Price = 499,
                Rating = 4.5,
                DeliveryTime = 30,
                DistanceKm = 3.4,
                ImageUrl = "https://via.placeholder.com/300x200.png?text=Paneer+Pizza",
                Tags = new List<string> { "Veg", "Hot & Spicy" }
            });
            TrendingNow.Add(new FoodItem
            {
                Name = "Veggie Supreme Burger",
                Price = 250,
                Rating = 4.2,
                DeliveryTime = 25,
                DistanceKm = 2.1,
                ImageUrl = "https://via.placeholder.com/300x200.png?text=Veg+Burger",
                Tags = new List<string> { "Vegetarian", "Healthy" }
            });

            RefreshFilter();
        }

        void RefreshFilter()
        {
            var filtered = AllFoodItems.Where(fi =>
                (SelectedCategory.Name == "All" ||
                 fi.Tags.Any(t => t.Equals(SelectedCategory.Name, StringComparison.OrdinalIgnoreCase)) ||
                 fi.Name.Contains(SelectedCategory.Name, StringComparison.OrdinalIgnoreCase))
                && (string.IsNullOrWhiteSpace(SearchText) ||
                    fi.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));

            FilteredFoodItems.Clear();
            foreach (var item in filtered)
                FilteredFoodItems.Add(item);
        }

        public ICommand LogoutCommand { get; }
       
        public async Task LogOutAsync()
        {
            var refreshToken = await SecureStorage.GetAsync("refresh_token");
            var logOutRequest = new LogoutRequest { RefreshToken = refreshToken };

            bool isLoggedOut = await _authService.LogoutAsync(logOutRequest);
            if (isLoggedOut)
            {
                await Shell.Current.GoToAsync("///LoginPage");
            }
            else
            {
                await _alertService.ShowAlertAsync("Error!", "Failed to Logout!", "OK");
            }

        }    

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
