using System;
using System.Collections.Generic;
using System.Linq;

namespace Execise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var listResult = new List<CarBaseViewModel>();
            listResult.AddRange(DataDefault.Cars.Where(c => c.Capacity <= 0 || c.PriceUsd < 0)
                .Select(c => new CarInvalidViewModel { Name = c.Name, PricePeso = c.PricePeso }));
            var query = (from car in DataDefault.Cars.Where(c => c.Capacity > 0 && c.PriceUsd >= 0)
                         join tax in DataDefault.TaxCars on car.Area equals tax.Area into lTax
                         from tax in lTax.DefaultIfEmpty()
                         where tax == null
                               || ((!tax.FromCapacity.HasValue || car.Capacity > tax.FromCapacity)
                                   && (!tax.ToCapacity.HasValue || car.Capacity <= tax.ToCapacity))
                         select new { car.Name, car.PricePeso, TaxCar = tax }).ToList();
            listResult.AddRange(query.Where(c => c.TaxCar == null)
                .Select(c => new CarNoTaxViewModel { Name = c.Name, PricePeso = c.PricePeso }));
            listResult.AddRange(query.Where(c => c.TaxCar != null)
                .Select(c => new CarValidViewModel { Name = c.Name, PricePeso = c.PricePeso, Tax = c.TaxCar.Tax }));
            foreach (var car in listResult)
            {
                Console.WriteLine("{0}: {1}", car.Name, car.MessageResult);
            }
            Console.ReadLine();
        }
    }
}
