namespace Reroi.Model.Entities
{
    public class Property : IEntityBase
    {
        public int Id { get; set; }
        public int Mls { get; set; }
        public double NetOperatingIncome { get; set; }
        public double PurchasePrice { get; set; }
        public double Roi { get; set; }
    }
}
