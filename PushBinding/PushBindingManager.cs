/*
 Original source code: Fredrik Hedblad (https://meleak.wordpress.com/2011/08/28/onewaytosource-binding-for-readonly-dependency-property/)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.ComponentModel;
using System.Collections;

namespace PushBindingExtension
{
    public class PushBindingManager
    {

        private const string PushBindingsInternal = "PushBindingsInternal";

        public static DependencyProperty PushBindingsProperty =
            DependencyProperty.RegisterAttached(PushBindingsInternal,
                                                typeof(PushBindingCollection),
                                                typeof(PushBindingManager),
                                                new UIPropertyMetadata(null));

        public static PushBindingCollection GetPushBindings(DependencyObject obj)
        {
            if (obj.GetValue(PushBindingsProperty) == null)

                obj.SetValue(PushBindingsProperty, new PushBindingCollection(obj));

            return (PushBindingCollection)obj.GetValue(PushBindingsProperty);
        }

        public static void SetPushBindings(DependencyObject obj, PushBindingCollection value) => obj.SetValue(PushBindingsProperty, value);

        public static DependencyProperty StylePushBindingsProperty =
            DependencyProperty.RegisterAttached("StylePushBindings",
                                                typeof(PushBindingCollection),
                                                typeof(PushBindingManager),
                                                new UIPropertyMetadata(null, StylePushBindingsChanged));

        public static PushBindingCollection GetStylePushBindings(DependencyObject obj) => (PushBindingCollection)obj.GetValue(StylePushBindingsProperty);

        public static void SetStylePushBindings(DependencyObject obj, PushBindingCollection value) => obj.SetValue(StylePushBindingsProperty, value);

        public static void StylePushBindingsChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {

            if (target != null)

                foreach (PushBinding pushBinding in e.NewValue as PushBindingCollection)

                    GetPushBindings(target).Add(pushBinding.Clone() as PushBinding);

        }
    }
}
