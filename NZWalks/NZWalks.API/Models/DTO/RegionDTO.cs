﻿namespace NZWalks.API.Models.DTO
{
    public class RegionDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Longt { get; set; }
        public double Population { get; set; }
    }
}
