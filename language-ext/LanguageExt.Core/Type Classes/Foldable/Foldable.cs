using System;
using System.Diagnostics.Contracts;
using LanguageExt.Attributes;

namespace LanguageExt.TypeClasses
{
    [Typeclass("Fold*")]
    public interface Foldable<FA, A> : Foldable<Unit, FA, A>
    {
    }

    [Typeclass("Fold*")]
    public interface Foldable<Env, FA, A> : Typeclass
    {
        /// <summary>
        /// In the case of lists, 'Fold', when applied to a binary
        /// operator, a starting value(typically the left-identity of the operator),
        /// and a list, reduces the list using the binary operator, from left to
        /// right.
        /// 
        /// Note that, since the head of the resulting expression is produced by
        /// an application of the operator to the first element of the list,
        /// 'Fold' can produce a terminating expression from an infinite list.
        /// </summary>
        /// <typeparam name="S">Aggregate state type</typeparam>
        /// <param name="state">Initial state</param>
        /// <param name="f">Folder function, applied for each item in fa</param>
        /// <returns>The aggregate state</returns>
        [Pure]
        Func<Env, S> Fold<S>(FA fa, S state, Func<S, A, S> f);

        /// <summary>
        /// In the case of lists, 'FoldBack', when applied to a binary
        /// operator, a starting value(typically the left-identity of the operator),
        /// and a list, reduces the list using the binary operator, from right to
        /// left.
        /// 
        /// Note that to produce the outermost application of the operator the
        /// entire input list must be traversed. 
        /// </summary>
        /// <typeparam name="S">Aggregate state type</typeparam>
        /// <param name="state">Initial state</param>
        /// <param name="f">Folder function, applied for each item in fa</param>
        /// <returns>The aggregate state</returns>
        [Pure]
        Func<Env, S> FoldBack<S>(FA fa, S state, Func<S, A, S> f);

        /// <summary>
        /// Number of items in the foldable
        /// </summary>
        /// <returns>Total number of items </returns>
        [Pure]
        Func<Env, int> Count(FA fa);
    }
}
