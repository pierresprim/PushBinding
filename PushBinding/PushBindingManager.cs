/*
 Original source code: Fredrik Hedblad (https://meleak.wordpress.com/2011/08/28/onewaytosource-binding-for-readonly-dependency-property/)
 */

using System;
using System.Windows;
using System.ComponentModel;

namespace PushBindingExtension
{
    public class PushBindingManager
    {

        private const string PushBindingsInternal = "PushBindingsInternal";

        private const string StylePushBindings = "StylePushBindings";

        private static DependencyPropertyKey PushBindingsPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(PushBindingsInternal,
                                                typeof(PushBindingCollection),
                                                typeof(PushBindingManager),
                                                new UIPropertyMetadata(null));

        public static DependencyProperty PushBindingsProperty = PushBindingsPropertyKey.DependencyProperty;

        public static PushBindingCollection GetPushBindings(DependencyObject obj)
        {

            if (obj.GetValue(PushBindingsProperty) == null)

                SetPushBindings(obj, new PushBindingCollection(obj));

            return (PushBindingCollection)obj.GetValue(PushBindingsProperty);

        }

        private static void SetPushBindings(DependencyObject obj, PushBindingCollection value) => obj.SetValue(PushBindingsPropertyKey, value);

        public static DependencyProperty StylePushBindingsProperty =
            DependencyProperty.RegisterAttached(StylePushBindings,
                                                typeof(PushBindingCollection),
                                                typeof(PushBindingManager),
                                                new UIPropertyMetadata(null, StylePushBindingsChanged));

        public static PushBindingCollection GetStylePushBindings(DependencyObject obj) => (PushBindingCollection)obj.GetValue(StylePushBindingsProperty);

        public static void SetStylePushBindings(DependencyObject obj, PushBindingCollection value) => obj.SetValue(StylePushBindingsProperty, value);

        private static void StylePushBindingsChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {

            if (target != null)

            {

                PushBindingCollection pushBindings = GetPushBindings(target);

                foreach (PushBinding pushBinding in e.NewValue as PushBindingCollection)

                    pushBindings.Add(pushBinding.Clone() as PushBinding);

            }

        }
    }
}
