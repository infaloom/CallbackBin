using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace CallbackBin.Models
{
    public class CallbackStoreModel
    {
        public IDictionary<string, StringValues> Headers { get; set; }
        public object Body { get; set; }
    }
}