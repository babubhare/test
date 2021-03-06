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
using test.Extensions;
using test.Rest;
using test.Rest.Security;
using test.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace test
{
    public partial class App : Application
    {
        #region Constants

        readonly static string POPUP_TOKEN_EXPIRED = "Looks like your token has expired, please log in again.";

        #endregion
        
        #region Services
        
        public static UserRestService UserService = new UserRestService();
        public static LoginRestService LoginService = new LoginRestService();
        
        #endregion

        public App()
        {
            InitializeComponent();

            ShowLoginPage();
        }

        protected override void OnStart()
        {
            // when the app starts, it subscribe to the client handler that check the presence of token
            MessagingCenter.Subscribe<TokenExpiredHandler, bool>(this, TokenExpiredHandler.TOKEN_EXPIRED_MESSAGE, async (arg1, arg2) =>
            {
                CustomAlertPopUp popup = new CustomAlertPopUp(POPUP_TOKEN_EXPIRED);
                popup.ButtonClickedEvent += (sender, e) => ShowLoginPage();
                popup.DismissTappedEvent += (sender, e) => ShowLoginPage();

                await PopupNavigation.Instance.PushAsync(popup);
            });
        }

        protected override async void OnResume()
        {
            if (!await LoginService.VerifyToken(Settings.AuthenticationToken))
                ShowLoginPage();
            else
                MainPage = new MasterPage();
        }

        void ShowLoginPage()
        {
            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
