/* 
* Generated by
* 
*      _____ _          __  __      _     _
*     / ____| |        / _|/ _|    | |   | |
*    | (___ | | ____ _| |_| |_ ___ | | __| | ___ _ __
*     \___ \| |/ / _` |  _|  _/ _ \| |/ _` |/ _ \ '__|
*     ____) |   < (_| | | | || (_) | | (_| |  __/ |
*    |_____/|_|\_\__,_|_| |_| \___/|_|\__,_|\___|_|
*
* The code generator that works in many programming languages
*
*			https://www.skaffolder.com
*
*
* You can generate the code from the command-line
*       https://npmjs.com/package/skaffolder-cli
*
*       npm install -g skaffodler-cli
*
*   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *
*
* To remove this comment please upgrade your plan here: 
*      https://app.skaffolder.com/#!/upgrade
*
* Or get up to 70% discount sharing your unique link:
*       https://beta.skaffolder.com/#!/register?friend=5cbfd8ad0b7cd3340031067f
*
* You will get 10% discount for each one of your friends
* 
*/
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using test.ViewModels;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace test.Extensions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChangePasswordPopUp : PopupPage
	{
        ChangePasswordPopUpViewModel _viewModel
        {
            get { return BindingContext as ChangePasswordPopUpViewModel; }
            set { BindingContext = value; }
        }

		public ChangePasswordPopUp()
		{
            InitializeComponent();

            _viewModel = new ChangePasswordPopUpViewModel();

            _viewModel.LoadingStartedEvent += (sender, e) => { loading_view.IsVisible = true; };
            _viewModel.LoadingEndedEvent += (sender, e) => { loading_view.IsVisible = false; };
        }

        void Dismiss_PopUp_Handler(object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        async Task Change_Password_Handler(object sender, System.EventArgs e)
        {
            string oldPassword = entry_old_password.Text;
            string newPassword = entry_new_password.Text;
            string confirmedPassword = entry_confirm_new_password.Text;

            // check if any field is empty
            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmedPassword))
                label_message.Text = "One or more fields are empty";

            // check if OldPassword is equals to UserPassword
            else if (!oldPassword.Equals(Settings.Password))
                label_message.Text = "Old password is not correct";

            // check if NewPassword and ConfirmedPassword are equals
            else if (!newPassword.Equals(confirmedPassword))
                label_message.Text = "Passwords do not match";

            // if old password and new password are identical
            else if (newPassword.Equals(oldPassword) && newPassword.Equals(confirmedPassword))
                label_message.Text = "Old password and new password are identical";

            // if everything is correct
            else {
                // try to save
                if (await _viewModel.ChangePassword(oldPassword, newPassword))
                {
                    label_message.Text = "Password successfully changed";
                }
                else
                    label_message.Text = "An error has occured, please try again";
            }
        }
    }
}