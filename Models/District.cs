﻿namespace WebClothes.Models
{
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProvinceId {  get; set; }
        public List<Ward> Wards { get; set; }
    }
}
