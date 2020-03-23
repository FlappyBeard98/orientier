using System.Collections.Generic;
using System.Linq;

namespace OrientDb
{
    public static class GraphNavigationExtensions
    {

        public static IEnumerable<T> E<T>(this IEnumerable<V> vs)
            where T : E =>
            vs?.SelectMany(p => p.Both()).OfType<T>();

        public static IEnumerable<T> E<T>(this V vs)
            where T : E =>
            new[] {vs}.E<T>();

        public static IEnumerable<T> V<T>(this IEnumerable<E> es)
            where T : V =>
            es?.SelectMany(p => new[] {p.FromV, p.ToV}).OfType<T>();

        public static IEnumerable<T> Traverse<T>(this IEnumerable<E> es)
            where T : V =>
            es?.SelectMany(p => new[] {p.FromV, p.ToV}).OfType<T>();

        public static IEnumerable<T> Traverse<T>(this IEnumerable<V> vs)
            where T : V =>
            vs?.SelectMany(q => q.Both()).SelectMany(p => new[] {p.FromV, p.ToV}).OfType<T>().Distinct();


        public static IEnumerable<T> Traverse<T>(this E es)
            where T : V =>
            new[] {es}.Traverse<T>();

        public static IEnumerable<T> Traverse<T>(this V vs)
            where T : V =>
            new[] {vs}.Traverse<T>();

        public static IEnumerable<TTo> To<TFrom, TTo>(this IEnumerable<E<TFrom, TTo>> es)
            where TFrom : V where TTo : V =>
            es?.Select(p => p.ToV).OfType<TTo>();

        public static IEnumerable<TFrom> From<TFrom, TTo>(this IEnumerable<E<TFrom, TTo>> es)
            where TFrom : V where TTo : V =>
            es?.Select(p => p.FromV).OfType<TFrom>();

        public static IEnumerable<TTo> To<TFrom, TTo>(this E<TFrom, TTo> es)
            where TFrom : V where TTo : V =>
            new[] {es}.To();

        public static IEnumerable<TFrom> From<TFrom, TTo>(this E<TFrom, TTo> es)
            where TFrom : V where TTo : V =>
            new[] {es}.From();

        public static IEnumerable<TFrom> In<TTo,TFrom >(this IEnumerable<TTo> vs)
            where TFrom : V where TTo : V =>
            vs?.SelectMany(p => p.InE?.Where(q=>q.FromV is TFrom).Select(q=>q.FromV).OfType<TFrom>());
        
        public static IEnumerable<TTo>  Out<TFrom, TTo>(this IEnumerable<TFrom> vs)
            where TFrom : V where TTo : V  =>
            vs?.SelectMany(p => p.OutE?.Where(q => q.ToV is TTo).Select(q => q.ToV).OfType<TTo>());
    
        public static IEnumerable<TFrom> In<TTo,TFrom >(this TTo vs)
            where TFrom : V where TTo : V =>
            new []{vs}.In<TTo,TFrom>();
        
        public static IEnumerable<TTo>  Out<TFrom, TTo>(this TFrom vs)
            where TFrom : V where TTo : V  =>
            new []{vs}.Out<TFrom,TTo>();



    }
}