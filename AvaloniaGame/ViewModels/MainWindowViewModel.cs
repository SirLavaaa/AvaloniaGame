using AvaloniaGame.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using AvaloniaGame.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaGame.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly GameContext db = new();

        private List<Game> _allGames = new();
        public ObservableCollection<Models.Game> Games { get; set; } = new();
        public ObservableCollection<Models.Studio> Studios { get; set; } = new();

        [ObservableProperty]
        private Studio _selectedStudio;

        [ObservableProperty]
        private string _searchText;

        [ObservableProperty]
        private string _selectedSort;
        public List<string> SortOptions { get; } = new()
        {
            "Reset",
            "Name >",
            "Name <",
            "Copies > ",
            "Copies <"
        };
        public MainWindowViewModel()
        {
            LoadData();
        }
        private async void LoadData()
        {
            Games.Clear();
            Studios.Clear();
            foreach (var game in await db.Games.ToListAsync())
            {
                Games.Add(game);
                _allGames.Add(game);
            }

            foreach (var studio in await db.Studios.ToListAsync())
            {
                Studios.Add(studio);
            }
        }
        partial void OnSearchTextChanged(string value)
        {
            ApplyFilters();
        }
        partial void OnSelectedStudioChanged(Studio value)
        {
            ApplyFilters();
        }
        partial void OnSelectedSortChanged(string value)
        {
            ApplyFilters();
        }
        private void ApplyFilters()
        {
            var query = _allGames.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                query = query.Where(g => g.Name.ToLower().Contains(SearchText));
            }

            if (SelectedStudio != null && SelectedStudio.Id != -1)
            {
                query = query.Where(g => g.Studio.Id == SelectedStudio.Id);
            }
            query = SelectedSort switch
            {
                "Name >" => query.OrderBy(g => g.Name),
                "Name <" => query.OrderByDescending(g => g.Name),
                "Copies >" => query.OrderBy(g => g.Copy),
                "Copies <" => query.OrderByDescending(g => g.Copy),
                _ => query
            };

            Games.Clear();

            foreach (var item in query)
            {
                Games.Add(item);
            }
        }
    }
}
