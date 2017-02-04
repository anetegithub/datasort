using DataSorting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSorting.Data;

namespace JsonDataHandler
{
    public class JsonDataHandler<T> : IDataHandler<T, byte[]>
    {
        public IList<T> Handle(byte[] Data)
        {
            var strData = System.Text.Encoding.UTF8.GetString(Data);
            return strData.FromJson<IList<T>>();
        }
    }
}
