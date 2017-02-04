using DataSorting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSorting.Core.Abstract
{
    public abstract class NotifySorter<T> :ISorter<T>
    {
        protected readonly Action Notify;
        public NotifySorter(Action Notify)
        {
            this.Notify = Notify;
        }

        public abstract IList<T> Sort(IList<T> Data);
        protected long _lastSortInMilliseconds;
        public long LastSortTime
        {
            get { return _lastSortInMilliseconds; }
        }
    }
}
