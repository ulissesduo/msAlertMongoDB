using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using msAlertaMongoDB.Entity;
using System.ComponentModel;
using System.Threading.Tasks;

namespace msAlertaMongoDB.Repository
{
    public class AlertaRepository : IAlertaRepository
    {
        private readonly IMongoCollection<Alerta> _alertaCollection;

        public AlertaRepository(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString); // get connection string from configuration
            var database = client.GetDatabase(settings.Value.DatabaseName); // get database name
            _alertaCollection = database.GetCollection<Alerta>(settings.Value.AlertaCollectionName);
        }
        public async Task<Alerta> AlertaById(string id)
        {
            return await _alertaCollection.Find(x=>x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Alerta> CreateAlerta(Alerta alerta)
        {
            await _alertaCollection.InsertOneAsync(alerta);
            return alerta;

        }

        public async Task<bool> DeleteAlerta(string id)
        {
            var result =  await _alertaCollection.DeleteOneAsync(x=>x.Id==id);
            return result.DeletedCount > 0;
        }

        public async Task<List<Alerta>> ListaAlertas()
        {
            return await _alertaCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Alerta> UpdateAlerta(string id, Alerta alerta)
        {
            alerta.Id = id; // ensure the object keeps the same Id
            var result = await _alertaCollection.ReplaceOneAsync(x => x.Id == id, alerta);
            return result.MatchedCount > 0 ? alerta : null; 
        }
    }
}
