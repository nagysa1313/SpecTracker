using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecTracker.Model
{
    public class Project : ObservableObject
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { Set(() => ID, ref _id, value); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { Set(() => Name, ref _name, value); }
        }
    }
}