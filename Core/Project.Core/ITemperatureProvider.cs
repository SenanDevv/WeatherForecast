using System;
using System.Threading.Tasks;

namespace Project.Core
{
    public interface ITemperatureProvider
    {
        Task<double?> GetTemperatureDataAsync(DateTime startDate, double latitude, double longitude);
    }
}
