using AvaloniaGame.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace AvaloniaGame.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly GameContext db = new();

        public ObservableCollection<Models.Game> Games { get; set; } = new();
        public ObservableCollection<Models.Studio> Studios { get; set; } = new();
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
                Debug.WriteLine(game.Name);
            }
        }
    }
}
