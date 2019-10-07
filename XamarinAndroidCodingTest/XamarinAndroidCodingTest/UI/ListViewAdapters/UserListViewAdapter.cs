using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using XamarinAndroidCodingTest.Entity;
using XamarinAndroidCodingTest.Helpers.Extensions;

namespace XamarinAndroidCodingTest.UI.ListViewAdapters
{
    public class UserListViewAdapter : BaseAdapter<User>
    {
        private readonly Context context;
        private readonly List<User> users;

        public UserListViewAdapter(Context context, List<User> users)
        {
            this.context = context;
            this.users = users;
        }

        public override User this[int position] => users[position];

        public override int Count => users.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if(row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.userlist_row, null, false);
            }

            TextView userNameFirstInitialTextView = row.FindViewById<TextView>(Resource.Id.userNameFirstLetterTextView);
            TextView userName = row.FindViewById<TextView>(Resource.Id.userNameTextView);

            userNameFirstInitialTextView.Text = users[position].UserName.GetFirstCharacterAsString().ToUpper();
            userName.Text = users[position].UserName;

            return row;
        }
    }
}