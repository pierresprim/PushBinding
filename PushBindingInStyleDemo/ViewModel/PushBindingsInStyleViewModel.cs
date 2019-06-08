using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace PushBindingInStyleDemo.ViewModel
{
    public class PushBindingsInStyleViewModel : ViewModelBase
    {
        public PushBindingsInStyleViewModel()
        {
            DisplayName = "PushBinding in Style Examples";

            MyItems = new ObservableCollection<ListItemViewModel>();

            for (int i = 1; i < 6; i++)

                MyItems.Add(new ListItemViewModel { Name = $"ListItem {i.ToString()}" });
        }

        #region Properties

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

        private ObservableCollection<ListItemViewModel> m_myItems;

        public ObservableCollection<ListItemViewModel> MyItems
        {
            get => m_myItems;

            set
            {
                m_myItems = value;
                OnPropertyChanged(nameof(MyItems));
            }
        }

        #endregion // Properties
    }
}
