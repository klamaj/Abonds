using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;

namespace Infrastructure.Data.GoogleService.Interfaces
{
    public interface IGoogleService
    {
        Event CreateEvent();
        Events GetEvent();
    }
}