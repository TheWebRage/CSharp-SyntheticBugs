using LanguageExt.Common;

namespace LanguageExt
{
    /// <summary>
    /// The Try monad captures exceptions and uses them to cancel the
    /// computation.  Primarily useful for expression based processing
    /// of errors.
    /// </summary>
    /// <remarks>To invoke directly, call x.Try()</remarks>
    /// <returns>A value that represents the outcome of the computation, either
    /// Some, None, or Failure</returns>
    public delegate OptionalResult<A> TryOption<A>();
}
