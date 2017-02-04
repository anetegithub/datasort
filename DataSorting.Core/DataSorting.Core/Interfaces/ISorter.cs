using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSorting.Core.Interfaces
{
    public interface ISorter<T>
    {
        IList<T> Sort(IList<T> Data);
        long LastSortTime { get; }
    }
}