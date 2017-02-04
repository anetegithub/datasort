using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSorting.Core.Interfaces;

namespace NewLineDataHandler
{
    public class NewLineDataHandler<T> : IDataHandler<T, byte[]>
    {
        public IList<T> Handle(byte[] Data)
        {
            var strData = System.Text.Encoding.UTF8.GetString(Data);
            return strData.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(x =>
                {
                    try
                    {
                        return (T)Convert.ChangeType(x, typeof(T));
                    }
                    catch { return default(T); }
                }).ToList();
        }
    }
}