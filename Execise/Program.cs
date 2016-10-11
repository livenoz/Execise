using System;
using System.Collections.Generic;
using System.Linq;

namespace Execise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var result = from car in DataDefault.Cars
                join tax in DataDefault.TaxCars on car.Area equals tax.Area into lTax
                from tax in lTax.DefaultIfEmpty(new TaxCarModel())
                where tax.Tax.Equals(DataDefault.NoValue)
                    || (car.Capacity > tax.FromCapacity && car.Capacity <= tax.ToCapacity)
                select new CarBaseViewModel(car, tax.Tax);

            foreach (var car in result)
            {
                Console.WriteLine("{0}: {1}", car.Name, car.MessageResult);
            }
            Console.ReadLine();
        }
    }
}
