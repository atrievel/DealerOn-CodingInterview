using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.IO
{
    public interface IDataReader
    {
        Task<IList<LineItem>> ReadDataAsync();
    }
}
