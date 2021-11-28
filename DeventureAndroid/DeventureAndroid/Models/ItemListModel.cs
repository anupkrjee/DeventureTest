using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DeventureAndroid.Models
{
    public class ItemListModel
    {
        public DataAll data { get; set; }
        public bool success { get; set; }
        public int statusCode { get; set; }
        public object message { get; set; }
    }

    public class Datum
    {
        public int id { get; set; }
        public string title { get; set; }
        public string imageUrl { get; set; }
        public string description { get; set; }
        public double rating { get; set; }
        public double price { get; set; }
        public List<string> allergens { get; set; }
        public List<object> menuCategories { get; set; }
    }

    public class MenuItemsFirstPage
    {
        public List<Datum> data { get; set; }
        public int totalPages { get; set; }
        public int currentPage { get; set; }
        public int activeIndex { get; set; }
        public int totalElementsCount { get; set; }
    }
    public class DataAll
    {
        public MenuItemsFirstPage menuItemsFirstPage { get; set; }
    }

    public class ItemListModelLocal
    {
        public int id { get; set; }
        public string title { get; set; }
        public string imageUrl { get; set; }
        public string description { get; set; }
        public double rating { get; set; }
        public double price { get; set; }
        public string searchText { get; set; }
    }
}
