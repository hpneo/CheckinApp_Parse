using System;

using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Parse;
using System.Linq;
using System.Linq.Expressions;

namespace CheckinApp_Parse
{
	[Activity (Label = "CheckinApp_Parse", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private ListView listViewMovies;
		private ArrayAdapter adapter;
		private EditText editTextNewMovie;
		private Button buttonAddMovie;

		async protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			ParseClient.Initialize ("8PQYEEFi68imGkbpgRtmXgfZsB0glHFSldEZ4LnN", "OsaC6UwdHQTFvPNnWoXm2uvV59k1D8sin6npDn33");

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			listViewMovies = FindViewById<ListView> (Resource.Id.listViewMovies);
			adapter = new ArrayAdapter (this, Resource.Layout.MovieItem, new string[] { });

			listViewMovies.Adapter = adapter;

			editTextNewMovie = FindViewById<EditText> (Resource.Id.editTextNewMovie);
			buttonAddMovie = FindViewById<Button> (Resource.Id.buttonAddMovie);

			buttonAddMovie.Click += async (object sender, EventArgs e) => {
				string newMovie = editTextNewMovie.Text.Trim();

				if (newMovie != "") {
					ParseObject movie = new ParseObject("Movie");
					movie["Title"] = newMovie;
					await movie.SaveAsync();

					adapter.Add(movie["Title"].ToString());
					editTextNewMovie.Text = "";
				}
			};

			var query = ParseObject.GetQuery ("Movie");

			IEnumerable<ParseObject> results = await query.FindAsync();

			for (var i = 0; i < results.Count (); i++) {
				adapter.Add (results.ElementAt<ParseObject> (i) ["Title"].ToString());
			}
		}
	}
}


