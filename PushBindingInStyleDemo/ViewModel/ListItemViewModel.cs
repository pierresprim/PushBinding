using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PushBindingInStyleDemo.ViewModel
{
    public class ListItemViewModel : ViewModelBase
    {
        private string m_name;
        public string Name
        {
            get { return m_name; }
            set
            {
                m_name = value;
                OnPropertyChanged("Name");
            }
        }

        private double m_width;
        public double Width
        {
            get { return m_width; }
            set
            {
                m_width = value;
                OnPropertyChanged("Width");
            }
        }

        private double m_height;
        public double Height
        {
            get { return m_height; }
            set
            {
                m_height = value;
                OnPropertyChanged("Height");
            }
        }

        private bool m_focused;
        public bool Focused
        {
            get { return m_focused; }
            set
            {
                m_focused = value;
                OnPropertyChanged("Focused");
            }
        }
    }
}
