using System;

namespace databaze
{
    public class bezeckeboty
    {
        public int ID { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public int YearReleased { get; set; }
        public string Type { get; set; } = string.Empty // např. "Road", "Trail"
        ;
        public double Size { get; set; } // EU size or user choice
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }

        public bezeckeboty Clone() =>
            new bezeckeboty
            {
                ID = ID,
                Model = Model,
                Brand = Brand,
                YearReleased = YearReleased,
                Type = Type,
                Size = Size,
                Quantity = Quantity,
                IsAvailable = IsAvailable
            };
    }
}