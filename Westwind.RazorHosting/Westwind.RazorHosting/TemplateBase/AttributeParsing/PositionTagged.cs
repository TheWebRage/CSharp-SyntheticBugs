#pragma warning disable CS1591
// From ASP.Net Web Stack [http://aspnetwebstack.codeplex.com/]
// Used under the Apache License

using System;
using System.Diagnostics;

namespace Westwind.RazorHosting
{
    [DebuggerDisplay("({Position})\"{Value}\"")]
    public class PositionTagged<T>
    {
        private PositionTagged()
        {
            Position = 0;
            Value = default(T);
        }

        public PositionTagged(T value, int offset)
        {
            Position = offset;
            Value = value;
        }

        public int Position { get; private set; }
        public T Value { get; private set; }

        public override bool Equals(object obj)
        {
            PositionTagged<T> other = obj as PositionTagged<T>;
            return other != null &&
                   other.Position == Position &&
                   Equals(other.Value, Value);
        }

        public override int GetHashCode()
        {
            return HashCodeCombiner.Start()
                .Add(Position)
                .Add(Value)
                .CombinedHash;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator T(PositionTagged<T> value)
        {
            return value.Value;
        }

        public static implicit operator PositionTagged<T>(Tuple<T, int> value)
        {
            return new PositionTagged<T>(value.Item1, value.Item2);
        }

        public static bool operator ==(PositionTagged<T> left, PositionTagged<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PositionTagged<T> left, PositionTagged<T> right)
        {
            return !Equals(left, right);
        }
    }
}
