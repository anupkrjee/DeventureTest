using System;
using System.Collections.Generic;

namespace DeventureAndroid.Models
{
    public class ProductModel
    {
        public ItemData data { get; set; }
        public bool success { get; set; }
        public int statusCode { get; set; }
        public object message { get; set; }
    }

    public class ItemData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public List<object> allergens { get; set; }
        public List<object> options { get; set; }
        public List<object> frequentlyBoughtProducts { get; set; }
    }
}
