using System;
using System.Collections.Generic;
using System.Text;

namespace AvaloniaGame.Models
{
    public class Studio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Game> Games { get; set; }
    }
}
