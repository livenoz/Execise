using System;
using System.Collections.Generic;

namespace Execise
{
    public enum Area
    {
        Europe = 1,
        Usa = 2,
        Japan = 3,
        China = 4
    }

    public class TaxCarModel
    {
        public TaxCarModel()
        {
            Tax = DataDefault.NoValue;
        }
        public Area Area { get; set; }
        public double FromCapacity { get; set; }
        public double ToCapacity { get; set; }
        public double Tax { get; set; }
    }

    public class CarModel
    {
        public Area Area { get; set; }
        public string Name { get; set; }
        public double Capacity { get; set; }
        public double PriceUsd { get; set; }
        public double PricePeso
        {
            get { return PriceUsd*DataDefault.PesoRate; }
        }
    }

    public class CarBaseViewModel: CarModel
    {
        public CarBaseViewModel() { }
        public CarBaseViewModel(CarModel car, double tax)
        {
            Area = car.Area;
            Name = car.Name;
            Capacity = car.Capacity;
            PriceUsd = car.PriceUsd;
            Tax = tax;
        }
        public double Tax { get; set; }
        public double? Total
        {
            get
            {
                var retults = new[] {null, (double?)TotalValue };
                return retults[CheckHaveTax];
            }
        }
        public string MessageResult
        {
            get
            {
                var results = new[] {"Invalid data", "No have tax", TotalValue.ToString("##,###.##")};
                return results[CheckInvalidData + CheckHaveTax];
            }
        }
        private int CheckInvalidData
        {
            get { return Convert.ToInt32(Capacity > 0 && PriceUsd >= 0); }
        }
        private int CheckHaveTax
        {
            get { return Convert.ToInt32(CheckInvalidData == 1 && Tax >= 0); }
        }
        private double TotalValue
        {
            get { return PricePeso*(1 + Tax)*(1 + DataDefault.Vat); }
        }
    }

    public class DataDefault
    {
        public const int PesoRate = 47;
        public const double Vat = 0.12;
        public const double NoValue = -1;
        public static List<TaxCarModel> TaxCars = new List<TaxCarModel>
        {
            new TaxCarModel { Area = Area.Europe, Tax = 1, FromCapacity = double.MinValue, ToCapacity = 2 },
            new TaxCarModel { Area = Area.Europe, Tax = 1.2, FromCapacity = 2, ToCapacity = 5 },
            new TaxCarModel { Area = Area.Europe, Tax = 2, FromCapacity = 5, ToCapacity = double.MaxValue },
            new TaxCarModel { Area = Area.Usa, Tax = 0.75, FromCapacity = double.MinValue, ToCapacity = 2 },
            new TaxCarModel { Area = Area.Usa, Tax = 0.9, FromCapacity = 2, ToCapacity = 5 },
            new TaxCarModel { Area = Area.Usa, Tax = 1.5, FromCapacity = 5, ToCapacity = double.MaxValue },
            new TaxCarModel { Area = Area.Japan, Tax = 0.7, FromCapacity = double.MinValue, ToCapacity = 2 },
            new TaxCarModel { Area = Area.Japan, Tax = 0.8, FromCapacity = 2, ToCapacity = 5 },
            new TaxCarModel { Area = Area.Japan, Tax = 1.35, FromCapacity = 5, ToCapacity = double.MaxValue }
        };
        public static List<CarModel> Cars = new List<CarModel>
        {
            new CarModel { Area = Area.Europe, Capacity = 6, PriceUsd = 217900, Name = "Benz G65 from Germany"},
            new CarModel { Area = Area.Japan, Capacity = 1.5, PriceUsd = 19490, Name = "Honda Jazz"},
            new CarModel { Area = Area.Usa, Capacity = 3.6, PriceUsd = 36995, Name = "Jeep wrangler"},
            new CarModel { Area = Area.Usa, Capacity = 0, PriceUsd = 36995, Name = "Jeep wrangler"},
            new CarModel { Area = Area.China, Capacity = 1, PriceUsd = 6000, Name = "Chery QQ 1.0L"}
        };
    }

}
