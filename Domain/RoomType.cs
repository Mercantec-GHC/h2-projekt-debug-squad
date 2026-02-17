using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class RoomType
    {
        //public enum QualityEnum {
        //    Low, Medium, High
        //}

        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public int Capacity { get; private set; }
        public decimal PricePerNight { get; private set; }
        //public QualityEnum Quality { get; private set; }

        private RoomType() { }
        public RoomType(string name, int capacity, decimal pricePerNight)
        {
            Name = name;
            Capacity = capacity;
            PricePerNight = pricePerNight;
            //Quality = quality;
        }
    }
}
