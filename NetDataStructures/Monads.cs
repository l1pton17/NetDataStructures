using System;

namespace NetDataStructures
{
    internal static class Monads
    {
        public static TResult With<TInput, TResult>
            (this TInput o, Func<TInput, TResult> evaluator)
            where TInput : class
            where TResult : class
        {
            if (o == null) return null;
            return evaluator(o);
        }

        public static TResult Return<TInput, TResult>
            (this TInput o, Func<TInput, TResult> evaluator,
                Func<TResult> failureValueFactory)
            where TInput : class
        {
            if (o == null) return failureValueFactory();
            return evaluator(o);
        }

        public static TResult Return<TInput, TResult>
            (this TInput o, Func<TInput, TResult> evaluator,
                TResult failureValue)
            where TInput : class
        {
            if (o == null) return failureValue;
            return evaluator(o);
        }

        public static TResult Return<TInput, TResult>
            (this TInput o, Func<TInput, TResult> evaluator)
            where TInput : class
        {
            if (o == null) return default(TResult);
            return evaluator(o);
        }

        public static bool ReturnSuccess<TInput>(this TInput o)
        {
            return o != null;
        }

        public static TInput If<TInput>(this TInput o, Predicate<TInput> evaluator)
            where TInput : class
        {
            if (o == null) return null;
            return evaluator(o) ? o : null;
        }

        public static TInput Do<TInput>(this TInput o, Action<TInput> action)
            where TInput : class
        {
            if (o == null) return null;
            action(o);
            return o;
        }
    }
}