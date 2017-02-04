using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSorting.Core.Interfaces
{
    public interface IDataHandler<T, D>
    {
        IList<T> Handle(D Data);
    }
}