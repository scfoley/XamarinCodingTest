using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using XamarinAndroidCodingTest.Entity;
using XamarinAndroidCodingTest.Helpers.Extensions;
using XamarinAndroidCodingTest.LocalStorage.Contracts;
using XamarinAndroidCodingTest.LocalStorage.Repositories;

namespace XamarinAndroidCodingTest.UI.Activities
{
    [Activity(Label = "AddUserActivity", Theme = "@style/AppTheme")]
    public class AddUserActivity : Activity
    {
        // Repo
        public UserRepository UserRepository { get; set; }

        // UI
        public EditText UserNameEditText { get; set; }
        public EditText PasswordEditText { get; set; }
        public TextView ErrorMessageTextView { get; set; }
        public Button AddUserButton { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.add_user);

            // Repo
            IDatabaseRepository databaseRepository = new DatabaseRepository();
            UserRepository = new UserRepository(databaseRepository);

            // UI Retrieval
            UserNameEditText = FindViewById<EditText>(Resource.Id.userNameEditText);
            PasswordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
            ErrorMessageTextView = FindViewById<TextView>(Resource.Id.errorMessageTextView);
            AddUserButton = FindViewById<Button>(Resource.Id.addUserButton);

            // Event Handling
            UserNameEditText.TextChanged += UserNameEditText_TextChanged;
            PasswordEditText.TextChanged += PasswordEditText_TextChanged;
            AddUserButton.Click += AddUserButton_Click;
        }

        private void UserNameEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var passwordText = PasswordEditText.Text;
            var userNameText = UserNameEditText.Text;
            ValidateInput(userNameText, passwordText);
        }

        private void PasswordEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var passwordText = PasswordEditText.Text;
            var userNameText = UserNameEditText.Text;
            ValidateInput(userNameText, passwordText);
        }

        private void AddUserButton_Click(object sender, EventArgs e)
        {
            var newUser = new User
            {
                UserName = UserNameEditText.Text,
                Password = PasswordEditText.Text
            };

            UserRepository.InsertUser(newUser);

            AlertDialog.Builder dialog = new AlertDialog.Builder(this);

            AlertDialog alert = dialog.Create();
            alert.SetTitle("User created!");
            alert.SetMessage($"User \"{newUser.UserName}\" has been created");
            alert.SetButton("OK", (c, ev) =>{});
            alert.DismissEvent += Alert_DismissEvent;
            alert.Show();
        }

        private void Alert_DismissEvent(object sender, EventArgs e)
        {
            Finish();
        }

        private void ValidateInput(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
            {
                HandleValidationFailed("User Name required");
                return;
            }

            if (UserRepository.UserNameExists(userName))
            {
                HandleValidationFailed("User Name already exists");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                HandleValidationFailed("Password required");
                return;
            }

            if (!password.HasMixtureOfLettersAndNumericalDigitsOnly())
            {
                HandleValidationFailed("Password must be a mixture of letters and numerical digits only, with at least one of each.");
                return;
            }

            if (!password.WithinCharacterCount(5, 12))
            {
                HandleValidationFailed("Password must be between 5 and 12 characters in length");
                return;
            }

            if (password.HasRepeatingSequenceOfCharacters())
            {
                HandleValidationFailed("Password must not contain any sequence of characters immediately followed by the same sequence");
                return;
            }

            AddUserButton.Enabled = true;
            ErrorMessageTextView.Visibility = ViewStates.Invisible;
        }

        private void HandleValidationFailed(string errorText)
        {
            ErrorMessageTextView.Text = errorText;
            ErrorMessageTextView.Visibility = ViewStates.Visible;
            AddUserButton.Enabled = false;
        }
    }
}