using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace PushBindingInStyleDemo.ViewModel
{
    public class PushBindingsViewModel : ViewModelBase
    {
        public PushBindingsViewModel()
        {
            DisplayName = "PushBinding Examples";

            MyItems = new ObservableCollection<ListItemViewModel>();
            MyItems.Add(new ListItemViewModel { Name = "ListItem 1" });
            MyItems.Add(new ListItemViewModel { Name = "ListItem 2" });
            MyItems.Add(new ListItemViewModel { Name = "ListItem 3" });
            MyItems.Add(new ListItemViewModel { Name = "ListItem 4" });
            MyItems.Add(new ListItemViewModel { Name = "ListItem 5" });
        }

        #region Properties

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

        private ObservableCollection<ListItemViewModel> m_myItems;
        public ObservableCollection<ListItemViewModel> MyItems
        {
            get { return m_myItems; }
            set
            {
                m_myItems = value;
                OnPropertyChanged("MyItems");
            }
        }

        #endregion // Properties
    }
}
