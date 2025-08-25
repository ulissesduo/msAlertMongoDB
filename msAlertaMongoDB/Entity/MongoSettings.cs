namespace msAlertaMongoDB.Entity
{
    public class MongoSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string AlertaCollectionName { get; set; } = "Alerta";
    }
}
