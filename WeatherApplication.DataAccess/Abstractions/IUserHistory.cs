using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApplication.DataAccess.Abstractions
{
    public interface IUserHistory
    {
        Task<IEnumerable<T>> GetUserHistoryAsync<T, U>(string storedProcedure, U parameters);
        Task<bool> AddUserHistoryAsync<T>(string storedProcedure, T parameters);
    }
}
