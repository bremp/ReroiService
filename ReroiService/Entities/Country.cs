namespace Reroi.Model.Entities
{
    public class Country : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double EpiIndex { get; set; }

    }
}
