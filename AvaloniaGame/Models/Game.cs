using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvaloniaGame.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Relese { get; set; }
        public int Copy { get; set; }
        public Studio Studio { get; set; }
    }
}
