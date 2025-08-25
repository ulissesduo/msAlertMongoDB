using msAlertaMongoDB.Entity;

namespace msAlertaMongoDB.Service
{
    public interface IAlertaService
    {
        Task<List<Alerta>> GetAllAlertas();
        Task<Alerta> GetAlertaById(string id);
        Task<Alerta> CreateAlert(Alerta alerta);
        Task<Alerta> UpdateAlert(string id, Alerta alerta);
        Task<bool> DeleteAlert(string id);
    }
}
