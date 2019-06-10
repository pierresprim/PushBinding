/*
 Original source code: Fredrik Hedblad (https://meleak.wordpress.com/2011/08/28/onewaytosource-binding-for-readonly-dependency-property/)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PushBindingExtension
{
    public class PushBindingCollection : FreezableCollection<PushBinding>
    {

        public PushBindingCollection() { }

        public PushBindingCollection(DependencyObject targetObject) => TargetObject = targetObject;

        public DependencyObject TargetObject { get; private set; }

    }
}
