using DataSorting.Core.Abstract;
using DataSorting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSorting.Core
{
    public class Processor<T, D> : IDisposable
    {
        IDataHandler<T, D> dHandler;
        ISorter<T> sorter;
        public Processor(IDataHandler<T, D> dHandler, NotifySorter<T> sorter)
        {
            this.dHandler = dHandler;
            this.sorter = sorter;
        }

        IList<T> HandledData;
        public IList<T> Sort(D Data)
        {
            HandledData = dHandler.Handle(Data);
            return sorter.Sort(HandledData);
        }
        
        public void Dispose()
        {
            dHandler = null;
            sorter = null;
        }

        ~Processor()
        {
            HandledData = null;
        }
    }
}