using System;
using System.Collections;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using Newtonsoft.Json.Linq;

namespace CheckinApp_Fragments_Tabs
{
	public class MoviesFragment : Android.Support.V4.App.Fragment
	{
		private ArrayAdapter adapter;
		private ListView listView;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View rootView = inflater.Inflate(Resource.Layout.MoviesFragment, container, false);
			listView = rootView.FindViewById<ListView> (Resource.Id.listViewMovies);

			adapter = new ArrayAdapter (Activity, Resource.Layout.MovieItem, new string[] { });

			listView.Adapter = adapter;

			return rootView;
		}

		public void populate(ArrayList list) {
			for (int i = 0; i < list.Count; i++) {
				JObject item = (JObject) list [i];
				if (item != null && item ["title"] != null) {
					adapter.Add (item ["title"].ToString ());
				}
			}
		}
	}
}

