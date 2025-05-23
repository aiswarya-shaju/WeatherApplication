using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApplication.Models.Models;

namespace WeatherApplication.DataAccess.Abstractions
{
    public interface IUserHistoryData
    {
        Task<bool> AddUserHistory(WeatherHistory userHistory);
        Task<IEnumerable<WeatherHistory>> GetUserHistory();
    }
}
