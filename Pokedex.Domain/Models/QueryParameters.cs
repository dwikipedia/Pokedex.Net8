using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Domain.Models
{
    public class QueryParameters
    {
        const int _maxSize = 100;
        private int _size = 50;

        public int Page { get; set; } = 1;
        public int Size
        {
            get { return _size; }
            set { _size = Math.Min(_maxSize, value); }
        }

        public string? SortBy { get; set; }
        public bool SortDescending { get; set; } = false;
    }
}
