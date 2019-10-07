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
            UserNameEditText.TextChanged += EditText_TextChanged;
            PasswordEditText.TextChanged += EditText_TextChanged;
            AddUserButton.Click += AddUserButton_Click;
        }

        private void EditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var passwordText = PasswordEditText.Text;
            var userNameText = UserNameEditText.Text;
            ValidateInput(userNameText, passwordText);
        }

        private void AddUserButton_Click(object sender, EventArgs e)
        {
            // Create User
            var newUser = new User
            {
                UserName = UserNameEditText.Text,
                Password = PasswordEditText.Text
            };

            // Insert user into local storage
            UserRepository.InsertUser(newUser);

            // Create dialog
            AlertDialog.Builder dialog = new AlertDialog.Builder(this);

            // Setup dialog to notify user of successful user creation
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
            // Make sure user name is populated
            if (string.IsNullOrEmpty(userName))
            {
                HandleValidationFailed("User Name required");
                return;
            }

            // Make sure user name does not already exist
            if (UserRepository.UserNameExists(userName))
            {
                HandleValidationFailed("User Name already exists");
                return;
            }

            // Make sure password is populated
            if (string.IsNullOrEmpty(password))
            {
                HandleValidationFailed("Password required");
                return;
            }

            // Make sure password has a mixture of numerical and digits only, with at least one of each
            if (!password.HasMixtureOfLettersAndNumericalDigitsOnly())
            {
                HandleValidationFailed("Password must be a mixture of letters and numerical digits only, with at least one of each.");
                return;
            }

            // Make sure password meets length requirements
            if (!password.WithinCharacterCount(5, 12))
            {
                HandleValidationFailed("Password must be between 5 and 12 characters in length");
                return;
            }

            // Make sure password does not have repeating sequences of characters
            if (password.HasRepeatingSequenceOfCharacters())
            {
                HandleValidationFailed("Password must not contain any sequence of characters immediately followed by the same sequence");
                return;
            }

            // Enable add user button
            AddUserButton.Enabled = true;

            // Hide error message
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