using System.Collections.Generic;
using System.Linq;

namespace OrientDb
{
    public class V : GraphItem
    {
        internal List<E> InE { get; set; } = new List<E>();
        internal List<E> OutE { get;set; }= new List<E>();

        public IEnumerable<E> Both()=> InE.Union(OutE);
        public IEnumerable<T> In<T>() where T : E => InE?.OfType<T>();
        public IEnumerable<T> Out<T>()  where T : E=> OutE?.OfType<T>();
        

    }
}