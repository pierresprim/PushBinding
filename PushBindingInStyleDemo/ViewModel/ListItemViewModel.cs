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
            get => m_name;

            set
            {
                m_name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private double m_width;

        public double Width
        {
            get => m_width;

            set
            {
                m_width = value;
                OnPropertyChanged(nameof(Width));
            }
        }

        private double m_height;

        public double Height
        {
            get => m_height;

            set
            {
                m_height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        private bool m_focused;

        public bool Focused
        {
            get => m_focused;

            set
            {
                m_focused = value;
                OnPropertyChanged(nameof(Focused));
            }
        }
    }
}
