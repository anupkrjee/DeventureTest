using System;
namespace DeventureAndroid.Services
{
    public static class BaseUrlClass
    {
        //Main url of API
        public static string MainUrl()
        {
            string url = "https://restly.deventure.ro/api/Product/";
            return url;
        }

        //API Name
        public static string GetMenuList()
        {
            return "InitRestaurantMenu";
        }

        public static string GetProductById()
        {
            return "GetProductById?productId=";
        }
    }
}
