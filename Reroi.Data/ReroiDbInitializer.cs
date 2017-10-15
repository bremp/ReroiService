using Reroi.Model.Entities;
using System;
using System.Linq;

namespace Reroi.Data
{
    public class ReroiDbInitializer
    {
        private static ReroiContext context;
        public static void Initialize(IServiceProvider serviceProvider)
        {
            context = (ReroiContext)serviceProvider.GetService(typeof(ReroiContext));

            InitializeReroi();
        }

        private static void InitializeReroi()
        {
            if (!context.Countries.Any())
            {
                Country c1 = new Country { Name = "Switzerland", EpiIndex = 87.67 };
                Country c2 = new Country { Name = "Luxembourg", EpiIndex = 83.29 };
                Country c3 = new Country { Name = "Australia", EpiIndex = 82.4 };
                Country c4 = new Country { Name = "Singapore", EpiIndex = 81.78 };
                Country c5 = new Country { Name = "Czech Republic", EpiIndex = 81.47 };
                Country c6 = new Country { Name = "Germany", EpiIndex = 80.47 };
                Country c7 = new Country { Name = "Spain", EpiIndex = 79.09 };
                Country c8 = new Country { Name = "Austria", EpiIndex = 78.32 };
                Country c9 = new Country { Name = "Sweden", EpiIndex = 78.09 };
                Country c10 = new Country { Name = "Norway", EpiIndex = 78.04 };

                context.Countries.Add(c1); context.Countries.Add(c2);
                context.Countries.Add(c3); context.Countries.Add(c4);
                context.Countries.Add(c5); context.Countries.Add(c6);
                context.Countries.Add(c7); context.Countries.Add(c8);
                context.Countries.Add(c9); context.Countries.Add(c10);

                context.SaveChanges();
            }

            if (!context.Properties.Any())
            {
                Property c1 = new Property { Mls = 201512981, NetOperatingIncome = 680.35, PurchasePrice = 190000 };
                Property c2 = new Property { Mls = 201612981, NetOperatingIncome = 1036.03, PurchasePrice = 190000 };
                Property c3 = new Property { Mls = 201712981, NetOperatingIncome = 1075, PurchasePrice = 195000 };
                Property c4 = new Property { Mls = 201709996, NetOperatingIncome = 783.49, PurchasePrice = 212000 };
                
            
                context.Properties.Add(c1); context.Properties.Add(c2);
                context.Properties.Add(c3); context.Properties.Add(c4);                

                context.SaveChanges();
            }
        }
    }
}
