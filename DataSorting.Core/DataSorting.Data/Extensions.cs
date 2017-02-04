using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSorting.Data
{
    public static class Extensions
    {
        public static T CloneJson<T>(this T source)
        {
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
        }
        public static string ToJson<T>(this T Object)
        {
            return JsonConvert.SerializeObject(Object);
        }
        public static T FromJson<T>(this string JsonString)
        {
            return JsonConvert.DeserializeObject<T>(JsonString);
        }
    }
}
