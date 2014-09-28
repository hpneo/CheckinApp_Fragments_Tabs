using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using System.IO;
using System.Net;
using System.Threading.Tasks;

using Android.Support.V4.View;
using Android.Support.V4.App;

using Newtonsoft.Json.Linq;

namespace CheckinApp_Fragments_Tabs
{
	[Activity (Label = "CheckinApp Fragments", MainLauncher = true, Icon = "@drawable/icon", Theme="@android:style/Theme.Holo.Light")]
	public class MainActivity : FragmentActivity
	{
		private ViewPager appViewPager;

		async protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			appViewPager = FindViewById<ViewPager> (Resource.Id.appViewPager);

			ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

			AppPagerAdapter appViewPagerAdapter = new AppPagerAdapter (SupportFragmentManager);

			appViewPager.Adapter = appViewPagerAdapter;
			appViewPager.SetOnPageChangeListener (new ViewPageListenerForActionBar(ActionBar));

			var tabMovies = ActionBar.NewTab();
			tabMovies.SetText ("Películas");
			tabMovies.TabSelected += (object sender, ActionBar.TabEventArgs e) => {
				appViewPager.CurrentItem = ActionBar.SelectedNavigationIndex;
			};

			ActionBar.AddTab (tabMovies);

			var tabPopular = ActionBar.NewTab();
			tabPopular.SetText ("Popular");
			tabPopular.TabSelected += (object sender, ActionBar.TabEventArgs e) => {
				appViewPager.CurrentItem = ActionBar.SelectedNavigationIndex;
			};

			ActionBar.AddTab (tabPopular);

			TMDB api = new TMDB ();
			Task<object> moviesTask = api.searchMovies ("Batman");
			Task<object> popularTask = api.searchMovies ("Harry Potter");

			JObject moviesResults = await moviesTask as JObject;
			JArray moviesArray = (JArray)moviesResults["results"];

			JObject popularResults = await popularTask as JObject;
			JArray popularArray = (JArray)popularResults["results"];

			CheckinAppCache.Movies.AddRange (moviesArray);
			CheckinAppCache.Popular.AddRange (popularArray);

			Console.WriteLine (appViewPagerAdapter.Count + " pages");

			((MoviesFragment)appViewPagerAdapter.GetItem (0)).populate (CheckinAppCache.Movies);
			((MoviesFragment)appViewPagerAdapter.GetItem (1)).populate (CheckinAppCache.Popular);

			tabMovies.SetText ("Películas (" + CheckinAppCache.Movies.Count + ")");
			tabPopular.SetText ("Popular (" + CheckinAppCache.Popular.Count + ")");
		}
	}

	public class ViewPageListenerForActionBar : ViewPager.SimpleOnPageChangeListener
	{
		private ActionBar actionBar;
		public ViewPageListenerForActionBar(ActionBar bar)
		{
			actionBar = bar;
		}
		public override void OnPageSelected(int position)
		{
			actionBar.SetSelectedNavigationItem(position);
		}
	}
}


