using DataSorting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSorting.Data;
using DataSorting.Core.Abstract;

namespace BubbleSorter
{
    public class BubbleSorter<T> : NotifySorter<T>
    {
        Func<T, T, bool> isHighest;

        public BubbleSorter(Action Notify, Func<T, T, bool> isHighest)
            : base(Notify)
        {
            this.isHighest = isHighest;
        }

        public override IList<T> Sort(IList<T> Data)
        {
            var data = Data.CloneJson();

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            for (int i = data.Count() - 1; i > 0; i--)
            {
                for (int j = 0; j <= i - 1; j++)
                {
                    if (isHighest(data[j], data[j + 1]))
                    {
                        Notify();

                        var highValue = data[j];

                        data[j] = data[j + 1];
                        data[j + 1] = highValue;
                    }
                }
            }
            sw.Stop();

            _lastSortInMilliseconds = sw.ElapsedMilliseconds;

            return data;
        }
    }
}