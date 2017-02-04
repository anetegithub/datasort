using BubbleSorter;
using DataSorting.Core;
using DataSorting.Core.Interfaces;
using JsonDataHandler;
using NewLineDataHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataSorting.Data;

namespace DataSorting.Client.datasorting
{
    public class DataSortingUtility
    {
        byte[] file;
        Action notify;
        
        public DataSortingUtility(byte[] file, Action Notify)
        {
            this.file = file;
            this.notify = Notify;
        }

        public string Sort(string handler, string type, string sorting)
        {
            if (type == "0")
                return handler<int>(handler, sorting, (a, b) => { return a > b; });
            else
                return handler<double>(handler, sorting, (a, b) => { return a > b; });
        }

        private string handler<T>(string handler, string sortingclass, Func<T, T, bool> isHighest)
        {
            if (handler == "0")
                return sorting(sortingclass,
                    new NewLineDataHandler<T>(),
                    isHighest);
            else
                return sorting(sortingclass,
                    new JsonDataHandler<T>(),
                    isHighest);
        }

        private string sorting<T>(string sorting, IDataHandler<T, byte[]> handler, Func<T, T, bool> isHighest)
        {
            var bubbleSorter = new BubbleSorter<T>(this.notify, isHighest);

            using (var process = new Processor<T, byte[]>(handler,bubbleSorter))
            {
                var data = process.Sort(this.file);
                TimeSpend = bubbleSorter.LastSortTime;
                return data.ToJson();
            }
        }

        public long TimeSpend;
    }
}