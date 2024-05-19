using MeteorApp.Models;

namespace MeteorApp.Interfaces
{
    public interface IMeteorsRecipientService
    {
        Task GetMeteorsAndSaveInDBAsync();
    }
}
