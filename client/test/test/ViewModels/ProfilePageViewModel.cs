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
using System;
using System.Threading.Tasks;
using test.Models;

namespace test.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        #region Attributes and Properties

        User _user;

        public User User
        {
            get { return _user; }
            set { SetValue(ref _user, value); }
        }

        #endregion

        public ProfilePageViewModel(User userLogged)
        {
            User = userLogged;
        }

        public async Task UpdateUserInfo()
        {
            OnLoadingStarted(EventArgs.Empty);

            await App.UserService.PUT(User);
            User = await App.UserService.GETId(User.Id);

            OnLoadingEnded(EventArgs.Empty);
        }
    }
}