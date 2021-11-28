
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DeventureAndroid.Models;
using DeventureAndroid.Services;
using FFImageLoading;
using Newtonsoft.Json;

namespace DeventureAndroid
{
    [Activity(Label = "orderActivity")]
    public class orderActivity : Activity
    {
        private ProductModel product;
        ProgressDialog progress;
        RadioButton rdbBarbecue, rdbSpicy, rdbFries, rdbWedges, rdbChips, rdbMedium, rdbLarge;
        CheckBox cbxSalad, cbxExtraring, cbxExtracheese, cbxPickles;
        LinearLayout cocacola, angelstraw, lavacake, hotcoockies;
        ImageView tickcocacola,tickangel,ticklavacake,tickhotcoockie;
        ImageView productImage;
        TextView txtProductName, txtProductPrice, txtDescription,txtOrderQty;
        Button btnAddToCart,btnRemoveItem,btnAddItem;
        double itemBasePrice = 7.5;
        double itemActualPrice = 0;
        int no_of_items = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.order_main);

            rdbBarbecue = FindViewById<RadioButton>(Resource.Id.rdbBarbecue);
            rdbBarbecue.CheckedChange += RdbBarbecue_CheckedChange;

            rdbSpicy = FindViewById<RadioButton>(Resource.Id.rdbSpicy);
            rdbSpicy.CheckedChange += RdbSpicy_CheckedChange; ;

            rdbFries = FindViewById<RadioButton>(Resource.Id.rdbFries);
            rdbFries.CheckedChange += RdbFries_CheckedChange;

            rdbWedges = FindViewById<RadioButton>(Resource.Id.rdbWedges);
            rdbWedges.CheckedChange += RdbWedges_CheckedChange;

            rdbChips = FindViewById<RadioButton>(Resource.Id.rdbChips);
            rdbChips.CheckedChange += RdbChips_CheckedChange;

            rdbMedium = FindViewById<RadioButton>(Resource.Id.rdbMedium);
            rdbBarbecue.CheckedChange += RdbBarbecue_CheckedChange;

            rdbBarbecue = FindViewById<RadioButton>(Resource.Id.rdbBarbecue);
            rdbMedium.CheckedChange += RdbMedium_CheckedChange;

            rdbLarge = FindViewById<RadioButton>(Resource.Id.rdbLarge);
            rdbLarge.CheckedChange += RdbLarge_CheckedChange;

            cbxSalad= FindViewById<CheckBox>(Resource.Id.cbxSalad);
            cbxSalad.CheckedChange += CbxSalad_CheckedChange;

            cbxExtraring = FindViewById<CheckBox>(Resource.Id.cbxExtraring);
            cbxExtraring.CheckedChange += CbxExtraring_CheckedChange;

            cbxExtracheese = FindViewById<CheckBox>(Resource.Id.cbxExtracheese);
            cbxExtracheese.CheckedChange += CbxExtracheese_CheckedChange;

            cbxPickles = FindViewById<CheckBox>(Resource.Id.cbxPickles);
            cbxPickles.CheckedChange += CbxPickles_CheckedChange;

            cocacola= FindViewById<LinearLayout>(Resource.Id.cocacola);
            cocacola.Click += Cocacola_Click;
            tickcocacola = FindViewById<ImageView>(Resource.Id.tickCocaCola);

            angelstraw = FindViewById<LinearLayout>(Resource.Id.itemStrawberry);
            angelstraw.Click += Angelstraw_Click;
            tickangel = FindViewById<ImageView>(Resource.Id.tickStrawberry);

            lavacake = FindViewById<LinearLayout>(Resource.Id.LavaCake);
            lavacake.Click += Lavacake_Click;
            ticklavacake = FindViewById<ImageView>(Resource.Id.tickLavacake);

            hotcoockies = FindViewById<LinearLayout>(Resource.Id.itemHotCake);
            hotcoockies.Click += Hotcoockies_Click;
            tickhotcoockie = FindViewById<ImageView>(Resource.Id.tickHotCookie);

            productImage = FindViewById<ImageView>(Resource.Id.prductImage);

            GetItemDetails();

            txtProductName = FindViewById<TextView>(Resource.Id.txtProductName);
            txtProductPrice = FindViewById<TextView>(Resource.Id.txtProductPrice);
            txtDescription = FindViewById<TextView>(Resource.Id.txtDescrption);
            txtOrderQty = FindViewById<TextView>(Resource.Id.txtOrderQty);

            btnAddToCart = FindViewById<Button>(Resource.Id.btnAddtoCart);
            btnAddToCart.Click += BtnAddToCart_Click;

            btnRemoveItem = FindViewById<Button>(Resource.Id.btnRemoveItem);
            btnRemoveItem.Click += BtnRemoveItem_Click;

            btnAddItem = FindViewById<Button>(Resource.Id.btnAddItem);
            btnAddItem.Click += BtnAddItem_Click;
        }

        private void BtnAddToCart_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "You have successfuly added in your cart.", ToastLength.Long).Show();
            OnBackPressed();
        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            if(no_of_items<5)
            {
                no_of_items = no_of_items + 1;
                itemBasePrice = itemBasePrice + itemActualPrice;
                btnAddToCart.Text = "Add to cart ($ " + itemBasePrice.ToString() + ")";
                txtOrderQty.Text = no_of_items.ToString();
            }
           else
            {
                Toast.MakeText(this, "You cannot add more than 5 same items.", ToastLength.Long).Show();

            }
        }

        private void BtnRemoveItem_Click(object sender, EventArgs e)
        {
            if (no_of_items > 1)
            {
                no_of_items = no_of_items - 1;
                itemBasePrice = itemBasePrice - itemActualPrice;
                btnAddToCart.Text = "Add to cart ($ " + itemBasePrice.ToString() + ")";
                txtOrderQty.Text = no_of_items.ToString();
            }
            else
            {
                Toast.MakeText(this, "You have minimum item.", ToastLength.Long).Show();
            }
        }

        private void Hotcoockies_Click(object sender, EventArgs e)
        {
            if (tickhotcoockie.Visibility == ViewStates.Visible)
            {
                hotcoockies.SetBackgroundColor(Android.Graphics.Color.ParseColor("#ffffff"));
                tickhotcoockie.Visibility = ViewStates.Gone;
                itemBasePrice = itemBasePrice - 4.50;
            }
            else
            {
                hotcoockies.SetBackgroundColor(Android.Graphics.Color.ParseColor("#2CA56E"));
                tickhotcoockie.Visibility = ViewStates.Visible;
                itemBasePrice = itemBasePrice + 4.50;
            }
            btnAddToCart.Text = "Add to cart ($ " + itemBasePrice.ToString()+")";
        }

        private void Lavacake_Click(object sender, EventArgs e)
        {
            if (ticklavacake.Visibility == ViewStates.Visible)
            {
                lavacake.SetBackgroundColor(Android.Graphics.Color.ParseColor("#ffffff"));
                ticklavacake.Visibility = ViewStates.Gone;
                itemBasePrice = itemBasePrice - 4.50;
            }
            else
            {
                lavacake.SetBackgroundColor(Android.Graphics.Color.ParseColor("#2CA56E"));
                ticklavacake.Visibility = ViewStates.Visible;
                itemBasePrice = itemBasePrice + 4.50;
            }
            btnAddToCart.Text = "Add to cart ($ " + itemBasePrice.ToString()+")";
        }

        private void Angelstraw_Click(object sender, EventArgs e)
        {
            if (tickangel.Visibility == ViewStates.Visible)
            {
                angelstraw.SetBackgroundColor(Android.Graphics.Color.ParseColor("#ffffff"));
                tickangel.Visibility = ViewStates.Gone;
                itemBasePrice = itemBasePrice - 4.50;
            }
            else
            {
                angelstraw.SetBackgroundColor(Android.Graphics.Color.ParseColor("#2CA56E"));
                tickangel.Visibility = ViewStates.Visible;
                itemBasePrice = itemBasePrice + 4.50;
            }
            btnAddToCart.Text = "Add to cart ($ " + itemBasePrice.ToString()+")";
        }

        private void Cocacola_Click(object sender, EventArgs e)
        {
            if(tickcocacola.Visibility==ViewStates.Visible)
            {
                cocacola.SetBackgroundColor(Android.Graphics.Color.ParseColor("#ffffff"));
                tickcocacola.Visibility = ViewStates.Gone;
                itemBasePrice = itemBasePrice - 4.22;
            }
            else
            {
                cocacola.SetBackgroundColor(Android.Graphics.Color.ParseColor("#2CA56E"));
                tickcocacola.Visibility = ViewStates.Visible;
                itemBasePrice = itemBasePrice + 4.22;
            }
            btnAddToCart.Text = "Add to cart ($ " + itemBasePrice.ToString()+")";


        }

        private void CbxPickles_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (cbxPickles.Checked)
            {
                itemBasePrice = itemBasePrice + 2.5;
            }
            else
            {
                itemBasePrice = itemBasePrice - 2.5;
            }
            btnAddToCart.Text = "Add to cart ($ " + itemBasePrice.ToString()+")";
        }

        private void CbxExtracheese_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (cbxExtracheese.Checked)
            {
                itemBasePrice = itemBasePrice + 2.0;
            }
            else
            {
                itemBasePrice = itemBasePrice - 2.0;
            }
            btnAddToCart.Text = "Add to cart ($ " + itemBasePrice.ToString()+")";
        }

        private void CbxExtraring_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (cbxExtraring.Checked)
            {
                itemBasePrice = itemBasePrice + 3.0;
            }
            else
            {
                itemBasePrice = itemBasePrice - 3.0;
            }
            btnAddToCart.Text = "Add to cart ($ " + itemBasePrice.ToString()+")";
        }

        private void CbxSalad_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if(cbxSalad.Checked)
            {
                itemBasePrice = itemBasePrice + 2.5;
            }
            else
            {
                itemBasePrice = itemBasePrice - 2.5;
            }
            btnAddToCart.Text = "Add to cart ($ " + itemBasePrice.ToString()+")";
        }

        private void RdbLarge_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (rdbLarge.Checked)
            {
                rdbMedium.Checked = false;
                itemBasePrice = itemBasePrice + 2.5;
                btnAddToCart.Text = "Add to cart ($ " + itemBasePrice.ToString()+")";
            } else { }
        }

        private void RdbMedium_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (rdbMedium.Checked) {
                rdbLarge.Checked = false;
                itemBasePrice = itemBasePrice - 2.5;
                btnAddToCart.Text = "Add to cart ($ " + itemBasePrice.ToString()+")";
            } else { }
        }

        private void RdbChips_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (rdbBarbecue.Checked) { rdbWedges.Checked = false; rdbFries.Checked = false; rdbBarbecue.Checked = true; } else { }
        }

        private void RdbWedges_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (rdbWedges.Checked)
            {
                rdbFries.Checked = false;
                rdbChips.Checked = false;
                rdbWedges.Checked=true;
               
            } else { }
        }

        private void RdbFries_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (rdbFries.Checked) {  rdbWedges.Checked = false; rdbChips.Checked = false; rdbFries.Checked = true; } else { }
        }

        private void RdbSpicy_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (rdbSpicy.Checked) { rdbBarbecue.Checked = false; } else { }
        }

        private void RdbBarbecue_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (rdbBarbecue.Checked) { rdbSpicy.Checked = false; } else { }
        }

        //Api for getting product deatils
        private async void GetItemDetails()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    progress = new Android.App.ProgressDialog(this);
                    progress.Indeterminate = true;
                    progress.SetProgressStyle(ProgressDialogStyle.Spinner);
                    progress.SetMessage("Loading is Progress...");
                    progress.SetCancelable(false);
                    progress.Show();
                    var uri = BaseUrlClass.MainUrl() + BaseUrlClass.GetProductById();
                    HttpResponseMessage response = await client.GetAsync(uri + "23");
                    var result = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<ProductModel>(result);
                    progress.Hide();

                    GetImageUrl(product.data.imageUrl, productImage);
                    itemActualPrice = product.data.price;
                    txtProductName.Text = product.data.name;
                    txtDescription.Text = product.data.description;
                    txtProductPrice.Text = "$ " + product.data.price.ToString();
                    no_of_items = 1;
                    txtOrderQty.Text = no_of_items.ToString();

                    itemBasePrice = itemActualPrice;
                    btnAddToCart.Text = "Add to cart ($ " + itemBasePrice.ToString() + ")";
                }
            }
           catch(Exception ex)
            {
                Toast.MakeText(this, "Something went wrong. Please try again",ToastLength.Long).Show();
            }
        }

        void GetImageUrl(string url, ImageView imageView)
        {
            ImageService.Instance.LoadUrl(url)
                .Retry(3, 200)
                .DownSample(400, 400)
                .Into(imageView);
        }
    }
}
