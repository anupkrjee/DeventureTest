using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using DeventureAndroid.RAdapter;
using AndroidX.RecyclerView.Widget;
using DeventureAndroid.Services;
using System.Net.Http;
using Newtonsoft.Json;
using DeventureAndroid.Models;
using Android.Widget;
using System.Linq;
using Android.Content;
using System.Collections.Generic;
using Google.Android.Material.BottomNavigation;
using Android.Content.Res;

namespace DeventureAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        [Obsolete]
        ProgressDialog progress;
        LinearLayout cardBurger,cardAppetizer,cardDrink,cardDessert;
        EditText editTextSearch;
        TextView txtClear,txtEmptyText;
        public List<ItemListModelLocal> listModelLocal=new List<ItemListModelLocal>();
        RecyclerView recyclerView;
        string filterKey = "";
        public static BottomNavigationView bottomnavigation;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
           // APIManager.GetInitRestaurantMenu();
            SetContentView(Resource.Layout.activity_main);

            bottomnavigation = (BottomNavigationView)FindViewById(Resource.Id.bottom_navigation);
            bottomnavigation.ItemSelected += (s, e) => {

                switch (e.Item.TitleFormatted.ToString())
                {
                    case "Menu":
                        Toast.MakeText(this, "Menu is Selected", ToastLength.Long).Show();
                        break;
                    case "Cart":
                        Toast.MakeText(this, "Cart is Selected", ToastLength.Long).Show();
                        break;
                    case "Tab":
                        Toast.MakeText(this, "Tab is Selected", ToastLength.Long).Show();
                        break;
                    case "Account":
                        Toast.MakeText(this, "Account is Selected", ToastLength.Long).Show();
                        break;

                }
            };

            GetItemLists();

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            recyclerView = FindViewById<RecyclerView>(Resource.Id.rcViewItemList);

            cardBurger = FindViewById<LinearLayout>(Resource.Id.cardBurger);
            cardBurger.Click += CardBurger_Click;

            cardAppetizer = FindViewById<LinearLayout>(Resource.Id.cardAppetizer);
            cardAppetizer.Click += CardAppetizer_Click;

            cardDrink = FindViewById<LinearLayout>(Resource.Id.cardDrink);
            cardDrink.Click += CardDrink_Click;

            cardDessert = FindViewById<LinearLayout>(Resource.Id.cardDessert);
            cardDessert.Click += CardDessert_Click;

            editTextSearch = FindViewById<EditText>(Resource.Id.editTextSerch);
            editTextSearch.TextChanged += EditTextSearch_TextChanged;

            txtClear = FindViewById<TextView>(Resource.Id.txtClear);
            txtClear.Click += TxtClear_Click;

            txtEmptyText = FindViewById<TextView>(Resource.Id.txtEmpty);
        }

        private void TxtClear_Click(object sender, EventArgs e)
        {
            editTextSearch.Text = "";
            filterKey = "";
            cardBurger.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            cardDessert.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            cardAppetizer.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            cardDrink.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            var newList = listModelLocal;
            RecyclerLayout(newList);
        }

        private void EditTextSearch_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(editTextSearch.Text))
            {
                var newList = listModelLocal.Where(x => x.searchText.Contains(editTextSearch.Text.ToLower())).ToList();
                RecyclerLayout(newList);
            }
            else
            {
                var newList = listModelLocal;
                RecyclerLayout(newList);
            }
            cardBurger.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            cardDessert.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            cardAppetizer.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            cardDrink.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
        }

        private void CardDessert_Click(object sender, EventArgs e)
        {
            cardBurger.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            cardDessert.SetBackgroundColor(Android.Graphics.Color.ParseColor("#2CA56E"));
            cardAppetizer.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            cardDrink.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));

            filterKey = "Dessert";
            FilteredItemList();

        }

        private void CardDrink_Click(object sender, EventArgs e)
        {
            cardBurger.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            cardDessert.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            cardAppetizer.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            cardDrink.SetBackgroundColor(Android.Graphics.Color.ParseColor("#2CA56E"));

            filterKey = "Drink";
            FilteredItemList();
        }

        private void CardAppetizer_Click(object sender, EventArgs e)
        {
            cardBurger.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            cardDessert.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            cardAppetizer.SetBackgroundColor(Android.Graphics.Color.ParseColor("#2CA56E"));
            cardDrink.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));

            filterKey = "Appetizer";
            FilteredItemList();
        }

        private void CardBurger_Click(object sender, EventArgs e)
        {
            cardBurger.SetBackgroundColor(Android.Graphics.Color.ParseColor("#2CA56E"));
            cardDessert.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            cardAppetizer.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            cardDrink.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));

            filterKey = "Burger";
            FilteredItemList();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return base.OnOptionsItemSelected(item);
        }


        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            Android.Content.Intent intent = new Android.Content.Intent(this, typeof(orderActivity));
            StartActivity(intent);
            //View view = (View)sender;
            //Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
            //    .SetAction("Action", (View.IOnClickListener)null).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        //Api for list of product
        [Obsolete]
        public async void GetItemLists()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    progress = new Android.App.ProgressDialog(this);
                    progress.Indeterminate = true;
                    progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
                    progress.SetMessage("Loading is Progress...");
                    progress.SetCancelable(false);
                    progress.Show();
                    var uri = BaseUrlClass.MainUrl() + BaseUrlClass.GetMenuList();
                    HttpResponseMessage response = await client.GetAsync(uri);
                    var result = await response.Content.ReadAsStringAsync();
                    var listModel = JsonConvert.DeserializeObject<ItemListModel>(result);
                    for (int i = 0; i < listModel.data.menuItemsFirstPage.data.Count; i++)
                    {
                        string txtSearch = listModel.data.menuItemsFirstPage.data[i].title + " " + listModel.data.menuItemsFirstPage.data[i].description;
                        if (!string.IsNullOrEmpty(txtSearch)) { txtSearch = txtSearch.ToLower(); }

                        listModelLocal.Add(new ItemListModelLocal
                        {
                            description = listModel.data.menuItemsFirstPage.data[i].description,
                            id = listModel.data.menuItemsFirstPage.data[i].id,
                            imageUrl = listModel.data.menuItemsFirstPage.data[i].imageUrl,
                            price = listModel.data.menuItemsFirstPage.data[i].price,
                            rating = listModel.data.menuItemsFirstPage.data[i].rating,
                            title = listModel.data.menuItemsFirstPage.data[i].title,
                            searchText = txtSearch
                        });
                    }
                    RecyclerLayout(listModelLocal);
                    progress.Hide();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Something went wrong. Please try again", ToastLength.Long).Show();
            }

        }
        private void FilteredItemList()
        {
            if(!string.IsNullOrEmpty(filterKey))
            {
                var newList = listModelLocal.Where(x => x.title.Contains(filterKey)).ToList();
                RecyclerLayout(newList);
            }
            else
            {
                var newList = listModelLocal;
                RecyclerLayout(newList);
            }
            
        }

        private void RecyclerLayout(List<ItemListModelLocal> items)
        {
            var gridLayoutManager = new GridLayoutManager(this, 1);
            recyclerView.SetLayoutManager(gridLayoutManager);
            recyclerView.HasFixedSize = true;
            recyclerView.SetAdapter(new ItemListAdapter(items));
            if(items.Count==0)
            {
                txtEmptyText.Visibility = ViewStates.Visible;
            }
            else
            {
                txtEmptyText.Visibility = ViewStates.Gone;
            }
        }
    }
}
