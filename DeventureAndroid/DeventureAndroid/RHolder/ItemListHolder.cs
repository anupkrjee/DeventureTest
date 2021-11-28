using System;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;

namespace DeventureAndroid.RHolder
{
    public class ItemListHolder : RecyclerView.ViewHolder
    {
        public TextView itemName { get; private set; }
        public TextView itemRating { get; private set; }
        public TextView itemDescription { get; private set; }
        public TextView itemPrice { get; private set; }
        public TextView itemText1 { get; private set; }
        public TextView itemText2 { get; private set; }
        public ImageView itemImage { get; private set; }
        public ImageView starImage { get; private set; }


        public ItemListHolder(View itemView) : base(itemView)
        {
            itemName = (TextView)itemView.FindViewById(Resource.Id.txtItemName);
            itemRating = (TextView)itemView.FindViewById(Resource.Id.txtItemRating);
            itemDescription = (TextView)itemView.FindViewById(Resource.Id.txtItemDescription);
            itemText1 = (TextView)itemView.FindViewById(Resource.Id.txtItemName);
            itemText2 = (TextView)itemView.FindViewById(Resource.Id.txtItemName);
            itemImage = (ImageView)itemView.FindViewById(Resource.Id.listImage);
            starImage = (ImageView)itemView.FindViewById(Resource.Id.listImage);
            itemPrice = (TextView)itemView.FindViewById(Resource.Id.txtItemPrice);
        }
    }
}
