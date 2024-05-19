using MeteorApp.Models;

namespace MeteorApp.Interfaces
{
    public interface IDataRecipientService
    {
        Task GetDataAndSaveInDBAsync();
    }
}
