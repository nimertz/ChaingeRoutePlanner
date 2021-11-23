using System;
using System.Threading.Tasks;
using ChaingeRoutePlanner.Models.VROOM.Input;
using ChaingeRoutePlanner.Models.VROOM.Output;

namespace ChaingeRoutePlanner.VroomClient
{
    public interface IVroomApiClient : IDisposable
    {
        Task<VroomOutput> PerformRequest(VroomInput vroomInput);
        Task<bool> IsHealthy();
    }
}