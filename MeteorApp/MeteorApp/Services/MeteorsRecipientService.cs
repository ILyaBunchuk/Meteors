using MeteorApp.Models;
using Newtonsoft.Json;
using AutoMapper;
using MeteorApp.Interfaces;

namespace MeteorApp.Services
{
    public class MeteorsRecipientService : IMeteorsRecipientService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public MeteorsRecipientService(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        private async Task<string> GetMeteorsAsync()
        {
            using var httpClient = new HttpClient();
            using HttpResponseMessage response = await httpClient.GetAsync("https://data.nasa.gov/resource/y77d-th95.json");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"{jsonResponse}\n");

            return jsonResponse;
        }

        public async Task GetMeteorsAndSaveInDBAsync()
        {
            try
            {
                string jsonResponse = await GetMeteorsAsync();

                if (jsonResponse == null) throw new Exception("Got empty data from resource");

                var meteors = JsonConvert.DeserializeObject<List<Meteor>>(jsonResponse);

                if (meteors == null) throw new Exception("Can't deserialize response");

                var meteorsDB = _mapper.Map<IEnumerable<MeteorDB>>(meteors);

                await _repository.CreateOrUpdateRangeAsync(meteorsDB);
                await _repository.SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}\n");
            }
        }
    }
}
