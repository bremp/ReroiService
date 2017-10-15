using System;

namespace Reroi.Model.Entities
{
    public class Property : IEntityBase
    {
        private static readonly int MonthsInYear = 12;
        public int Id { get; set; }
        public int Mls { get; set; }
        public double NetOperatingIncome { get; set; }
        public double PurchasePrice { get; set; }        
        public double Roi
        {
            get
            {                
                return Math.Round(((this.NetOperatingIncome * MonthsInYear) / this.PurchasePrice) * 100, 2);
            }
            private set { }
        }
    }
}
