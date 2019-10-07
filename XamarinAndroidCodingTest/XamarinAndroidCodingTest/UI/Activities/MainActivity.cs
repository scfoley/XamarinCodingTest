using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using XamarinAndroidCodingTest.Entity;
using XamarinAndroidCodingTest.LocalStorage.Contracts;
using XamarinAndroidCodingTest.LocalStorage.Repositories;
using XamarinAndroidCodingTest.UI.ListViewAdapters;
using XamarinAndroidCodingTest.UI.Activities;

namespace XamarinAndroidCodingTest.UI
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        // Repo
        public IUserRepository UserRepository { get; set; }

        // Data
        public List<User> Users { get; set; }

        // UI
        public ListView UsersList { get; set; }
        public Button AddUserButton { get; set; }
        public UserListViewAdapter ListViewAdapter { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            // Repo
            IDatabaseRepository databaseRepository = new DatabaseRepository();
            UserRepository = new UserRepository(databaseRepository);

            // Data
            Users = UserRepository.GetUsers();

            // UI
            UsersList = FindViewById<ListView>(Resource.Id.userListView);
            AddUserButton = FindViewById<Button>(Resource.Id.addUserButton);
            ListViewAdapter = new UserListViewAdapter(this, Users);

            InstantiateUI();
        }

        protected override void OnResume()
        {
            base.OnResume();

            Users.Clear();
            Users.AddRange(UserRepository.GetUsers());

            ListViewAdapter.NotifyDataSetChanged();
        }

        public void InstantiateUI()
        {
            UsersList.Adapter = ListViewAdapter;

            // Add user button
            AddUserButton = FindViewById<Button>(Resource.Id.addUserButton);

            AddUserButton.Click += AddUserButton_Click;
        }

        private void AddUserButton_Click(object sender, System.EventArgs e)
        {
            this.StartActivity(typeof(AddUserActivity));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}