using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace PushBindingInStyleDemo.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private string m_displayName;
        public string DisplayName
        {
            get { return m_displayName; }
            set
            {
                m_displayName = value;
                OnPropertyChanged("DisplayName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
