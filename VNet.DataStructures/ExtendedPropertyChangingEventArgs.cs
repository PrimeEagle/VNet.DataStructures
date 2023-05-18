using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace VNet.DataStructures
{
    public class ExtendedPropertyChangingEventArgs<T> : PropertyChangingEventArgs, IExtendedPropertyChangingEventArgs<T>
    {
        [NotNull]
        public new string PropertyName { get; init; }
        public T OldValue { get; init; }
        public T NewValue { get; init; }

        public ExtendedPropertyChangingEventArgs(string propertyName, T oldValue, T newValue)
            : base(propertyName)
        {
            PropertyName = propertyName;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public event PropertyChangingEventHandler? PropertyChanging;
    }
}