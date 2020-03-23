using System;
using System.Threading.Tasks;

namespace OrientDb.Mutable
{
    public interface IMutableGraph : IGraph, IDisposable
    {
        void CreateVertex<T>(T item) where T : V;
        ISetter<T> UpdateVertex<T>(T item) where T :V;
        ISetter<T> UpdateEdge<T>(T item) where T :E;
        void DeleteVertex<T>(T item) where T : V;
        void DeleteEdge<T>(T item) where T : E;

        void CreateEdge<TE, TOut, TIn>(TOut @from, TE edge, TIn @to)
            where TOut : V where TIn : V where TE : E<TOut, TIn>;

        void Commit();

    }
}