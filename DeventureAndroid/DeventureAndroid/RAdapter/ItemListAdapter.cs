using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using DeventureAndroid.Models;
using DeventureAndroid.RHolder;
using FFImageLoading;
using static AndroidX.RecyclerView.Widget.RecyclerView;

namespace DeventureAndroid.RAdapter
{
    public class ItemListAdapter: RecyclerView.Adapter
    {
        private List<ItemListModelLocal> model;
        public ItemListAdapter(List<ItemListModelLocal> list)
        {
            model = list;
        }

        public override int ItemCount
        {
            get
            {
                return model.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            byte[] imageBytes = null;
            Bitmap bmp = null;
            var holder = viewHolder as ItemListHolder;
            holder.itemName.Text = model[position].title;
            holder.itemRating.Text = model[position].rating.ToString();
            holder.itemDescription.Text = model[position].description;
            holder.itemPrice.Text = "$ " + model[position].price;
            GetImageUrl(model[position].imageUrl, holder.itemImage);
            //using (var webClient = new WebClient())
            //{
            //    try
            //    {
            //        imageBytes = webClient.DownloadData(model[position].imageUrl);
            //        bmp = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
            //    }
            //    catch { }
            //}
            //holder.itemImage.SetImageBitmap(bmp);
            holder.itemImage.Click += delegate
            {
                Intent orderIntent = new Intent(holder.itemImage.Context, typeof(orderActivity));
                holder.itemImage.Context.StartActivity(orderIntent);

            };

            // holder.itemImage.SetImageResource(Resource.Drawable.test);
        }
        void GetImageUrl(string url,ImageView imageView)
        {
            ImageService.Instance.LoadUrl(url)
                .Retry(3, 200)
                .DownSample(400, 400)
                .Into(imageView);

        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
            Inflate(Resource.Layout.layout_itemList, parent, false);
            ItemListHolder th = new ItemListHolder(itemView);
            return th;
        }
    }
}
