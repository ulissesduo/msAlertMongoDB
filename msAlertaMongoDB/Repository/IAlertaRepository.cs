using msAlertaMongoDB.Entity;

namespace msAlertaMongoDB.Repository
{
    public interface IAlertaRepository
    {
        Task<List<Alerta>> ListaAlertas();
        Task<Alerta> AlertaById(string id);
        Task<Alerta> CreateAlerta(Alerta alerta);
        Task<Alerta> UpdateAlerta(string id, Alerta alerta);
        Task<bool> DeleteAlerta(string id);
    }
}
