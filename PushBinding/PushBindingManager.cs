/*
 Original source code: Fredrik Hedblad (https://meleak.wordpress.com/2011/08/28/onewaytosource-binding-for-readonly-dependency-property/)
 */

using System;
using System.Windows;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections;

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

            PushBindingCollection pushBindings = null;

            if (obj.GetValue(PushBindingsProperty) == null)

            {

                pushBindings = new PushBindingCollection(obj);

                SetPushBindings(obj, pushBindings);

            }

            return pushBindings;

        }

        private static void SetPushBindings(DependencyObject obj, PushBindingCollection value)
        {
            obj.SetValue(PushBindingsPropertyKey, value);

            ((INotifyCollectionChanged)value).CollectionChanged += CollectionChanged;
        }

        static int GetId(PushBindingCollection sourceCollection) => sourceCollection.Count == 1 ? 1 : sourceCollection[sourceCollection.Count - 1].Id + 1;

        private static void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            PushBindingCollection pushBindings = (PushBindingCollection)sender;

            switch (e.Action)

            {

                case NotifyCollectionChangedAction.Add:

                    if (e.NewItems != null)

                        foreach (PushBinding item in e.NewItems)

                        {

                            item.TargetObject = pushBindings.TargetObject;

                            item.Id = GetId(pushBindings);

                            item.SetupTargetBinding();

                        }

                    break;

                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Reset:

                    if (e.OldItems != null)

                        TryRemoveStylePushBindings(GetStylePushBindings(pushBindings.TargetObject), e.OldItems, true);

                    break;

                case NotifyCollectionChangedAction.Replace:

                    TryReplaceStylePushBindings(GetStylePushBindings(pushBindings.TargetObject), e.OldItems, e.NewItems, true);

                    break;

                case NotifyCollectionChangedAction.Move:
                    if (e.OldStartingIndex == e.NewStartingIndex) break;
                    int difference = e.OldStartingIndex - e.NewStartingIndex;
                    int id;
                    FreezableCollection<PushBinding> styleBehaviors = GetStylePushBindings(pushBindings.TargetObject);
                    foreach (PushBinding item in e.OldItems)
                    {
                        id = item.Id;
                        item.Id -= difference;
                        foreach (PushBinding styleItem in styleBehaviors)
                            if (styleItem.Id == id)
                                styleItem.Id = item.Id;
                    }
                    void updateId(int startIndex, int length)
                    {
                        int count = length + startIndex;
                        for (int i = startIndex; i < count; i++)
                        {
                            id = pushBindings[startIndex].Id;
                            pushBindings[startIndex].Id += startIndex + 1;
                            foreach (PushBinding styleItem in styleBehaviors)
                                if (styleItem.Id == id)
                                    styleItem.Id = pushBindings[startIndex].Id;
                        }
                    }
                    int _startIndex;
                    if (e.NewStartingIndex < e.OldStartingIndex)
                    {
                        _startIndex = e.NewStartingIndex + e.OldItems.Count;
                        updateId(_startIndex, e.OldStartingIndex + e.OldItems.Count - _startIndex + 1);
                    }
                    else
                    {
                        _startIndex = e.OldStartingIndex;
                        updateId(_startIndex, e.NewStartingIndex - _startIndex + 1);
                    }
                    break;
                default:
                    break;
            }

        }

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

                if (e.OldValue != null)

                {

                    ((INotifyCollectionChanged)e.OldValue).CollectionChanged -= StyleCollectionChanged;

                    TryRemoveStylePushBindings(GetPushBindings(target), e.OldValue as FreezableCollection<PushBinding>, false);

                }

                if (e.NewValue != null)

                {

                    ((INotifyCollectionChanged)e.NewValue).CollectionChanged += StyleCollectionChanged;

                    AddStylePushBindings(GetPushBindings(target), e.NewValue as FreezableCollection<PushBinding>);

                }

            }

        }

        private static void AddStylePushBindings(PushBindingCollection pushBindings, IList pushBindingsToAdd)

        {

            foreach (PushBinding pushBinding in pushBindingsToAdd)

            {

                PushBinding _pushBinding = pushBinding.Clone() as PushBinding;

                pushBindings.Add(_pushBinding as PushBinding);

                pushBinding.Id = _pushBinding.Id;

            }

        }

        private static void TryRemoveStylePushBindings(PushBindingCollection pushBindings, IList pushBindingsToRemove, bool disposePushBindings)

        {

            if (disposePushBindings)

                foreach (PushBinding pushBinding in pushBindingsToRemove)

                    pushBinding.Dispose();

            if (pushBindings != null)

                for (int i = 0; i < pushBindingsToRemove.Count; i++)

                    for (int j = 0; j < pushBindings.Count; j++)

                        if (((PushBinding)pushBindingsToRemove[i]).Id == pushBindings[j].Id)

                            pushBindings.RemoveAt(j);

        }

        private static void TryReplaceStylePushBindings(PushBindingCollection pushBindings, IList oldPushBindings, IList newPushBindings, bool disposePushBindings)

        {

            for (int i = 0; i < oldPushBindings.Count; i++)

            {

                PushBinding oldItem = (PushBinding)oldPushBindings[i];
                PushBinding newItem = (PushBinding)newPushBindings[i];
                PushBinding clonedPushBinding = newItem.Clone() as PushBinding;

                newItem.TargetObject = clonedPushBinding.TargetObject = pushBindings.TargetObject;
                newItem.Id = oldItem.Id;

                if (disposePushBindings)

                    oldItem.Dispose();

                for (int j = 0; j < pushBindings.Count; j++)

                    if (pushBindings[j].Id == oldItem.Id)

                        pushBindings[j] = clonedPushBinding;

            }

        }

        static void StyleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            PushBindingCollection pushBindings = (PushBindingCollection)sender;
            switch (e.Action)
            {
                //when an item(s) is added we need to set the Owner property implicitly
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)

                        AddStylePushBindings(GetPushBindings(pushBindings.TargetObject), e.NewItems);

                    break;

                //when an item(s) is removed we should Dispose the BehaviorBinding
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Reset:
                    if (e.OldItems != null)

                        TryRemoveStylePushBindings(GetPushBindings(pushBindings.TargetObject), e.OldItems, false);

                    break;

                //here we have to set the owner property to the new item and unregister the old item
                case NotifyCollectionChangedAction.Replace:

                    TryReplaceStylePushBindings(GetPushBindings(pushBindings.TargetObject), e.OldItems, e.NewItems, false);

                    break;

                case NotifyCollectionChangedAction.Move:

                    throw new InvalidOperationException("Move is not supported for style behaviors");

                default:
                    break;
            }
        }
    }
}
