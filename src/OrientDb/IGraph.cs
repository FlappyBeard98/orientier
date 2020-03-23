using System.Collections.Generic;

namespace OrientDb
{
    public interface IGraph
    {
        IEnumerable<T> V<T>() where T : V;
        IEnumerable<T> E<T>() where T : E;
    }
}