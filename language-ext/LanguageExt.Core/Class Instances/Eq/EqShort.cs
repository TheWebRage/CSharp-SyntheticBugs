using LanguageExt.TypeClasses;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace LanguageExt.ClassInstances
{
    /// <summary>
    /// Integer equality
    /// </summary>
    public struct EqShort : Eq<short>
    {
        public static readonly EqShort Inst = default(EqShort);

        /// <summary>
        /// Equality test
        /// </summary>
        /// <param name="x">The left hand side of the equality operation</param>
        /// <param name="y">The right hand side of the equality operation</param>
        /// <returns>True if x and y are equal</returns>
        [Pure]
        public bool Equals(short a, short b) =>
            a == b;

        /// <summary>
        /// Get hash code of the value
        /// </summary>
        /// <param name="x">Value to get the hash code of</param>
        /// <returns>The hash code of x</returns>
        [Pure]
        public int GetHashCode(short x) =>
            default(HashableShort).GetHashCode(x);
            
        [Pure]
        public Task<bool> EqualsAsync(short x, short y) =>
            Equals(x, y).AsTask();

        [Pure]
        public Task<int> GetHashCodeAsync(short x) =>
            GetHashCode(x).AsTask();    
    }
}
