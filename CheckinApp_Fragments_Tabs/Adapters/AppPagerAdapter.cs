using System;
using System.Collections.Generic;

using Android.Support.V4.App;

namespace CheckinApp_Fragments_Tabs
{
	public class AppPagerAdapter : FragmentPagerAdapter
	{
		private List<Android.Support.V4.App.Fragment> fragments = new List<Android.Support.V4.App.Fragment>();
		public AppPagerAdapter (FragmentManager fragmentManager) : base(fragmentManager)
		{
			fragments.Add (new MoviesFragment ());
			fragments.Add (new MoviesFragment ());
		}

		public void AddFragment(Android.Support.V4.App.Fragment fragment) {
			fragments.Add (fragment);
		}

		public override Android.Support.V4.App.Fragment GetItem(int position) {
			Android.Support.V4.App.Fragment fragment = fragments [position];

			/*if (position == 0) {
				((MoviesFragment) fragment).populate(CheckinAppCache.Movies);
			} else if (position == 1) {
				((MoviesFragment) fragment).populate(CheckinAppCache.Popular);
			}*/

			return fragment;
		}

		public override int Count
		{
			get { return fragments.Count; }
		}
	}
}

