using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace VNet.DataStructures
{
    public class ExtendedPropertyChangedEventArgs<T> : PropertyChangedEventArgs, IExtendedPropertyChangedEventArgs<T>
    {
        [NotNull]
        public new string PropertyName { get; init; }
        public T OldValue { get; init; }
        public T NewValue { get; init; }

        public ExtendedPropertyChangedEventArgs(string propertyName, T oldValue, T newValue)
            : base(propertyName)
        {
            PropertyName = propertyName;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}