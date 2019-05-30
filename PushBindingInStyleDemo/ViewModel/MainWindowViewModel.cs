using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace PushBindingInStyleDemo.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            PushBindingExamples = new ObservableCollection<ViewModelBase>();
            PushBindingExamples.Add(new PushBindingsViewModel());
            PushBindingExamples.Add(new PushBindingsInStyleViewModel());
        }

        #region Properties

        private ObservableCollection<ViewModelBase> m_pushBindingExamples;
        public ObservableCollection<ViewModelBase> PushBindingExamples
        {
            get { return m_pushBindingExamples; }
            set
            {
                m_pushBindingExamples = value;
                OnPropertyChanged("PushBindingExamples");
            }
        }

        #endregion // Properties
    }
}
