using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchesGameWPF
{
    public class Match : ViewModelBase
    {
        public int Id { get; set; }
        public int RowNumber { get; set; }
        public int PlayerId { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; OnPropertyChanged(); }
        }
    }
}
