using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #region Nullable

        public static Nullable<TResult> With<TInput, TResult>
            (this TInput o, Func<TInput, Nullable<TResult>> evaluator)
            where TInput : class
            where TResult : struct
        {
            if (o == null) return null;
            return evaluator(o);
        }

        public static TResult With<TInput, TResult>
            (this Nullable<TInput> o, Func<TInput, TResult> evaluator)
            where TInput : struct
            where TResult : class
        {
            if (!o.HasValue) return null;

            return evaluator(o.Value);
        }

        public static Nullable<TResult> With<TInput, TResult>
            (this Nullable<TInput> o, Func<TInput, Nullable<TResult>> evaluator)
            where TInput : struct
            where TResult : struct
        {
            if (!o.HasValue) return null;

            return evaluator(o.Value);
        }

        public static TResult Return<TInput, TResult>
            (this Nullable<TInput> o, Func<TInput, TResult> evaluator,
            TResult failureValue)
            where TInput : struct
        {
            if (!o.HasValue) return failureValue;

            return evaluator(o.Value);
        }

        public static TResult Return<TInput, TResult>
            (this Nullable<TInput> o, Func<TInput, TResult> evaluator)
            where TInput : struct
        {
            if (!o.HasValue) return default(TResult);

            return evaluator(o.Value);
        }

        public static Nullable<TInput> If<TInput>(this Nullable<TInput> o, Predicate<TInput> evaluator)
            where TInput : struct
        {
            if (!o.HasValue) return null;

            return evaluator(o.Value) ? o : null;
        }

        public static Nullable<TInput> Do<TInput>(this Nullable<TInput> o, Action<TInput> action)
            where TInput : struct
        {
            if (!o.HasValue) return null;

            action(o.Value);
            return o;
        }

        #endregion
    }
}
