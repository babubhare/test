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
using System.Collections.Generic;
using System.Threading.Tasks;
using test.Models;
using test.ViewModels;
using test.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        MasterPageViewModel _viewModel
        {
            get { return BindingContext as MasterPageViewModel; }
            set { BindingContext = value; }
        }

        public MasterPage()
        {
            InitializeComponent();
            _viewModel = new MasterPageViewModel();

            listView.ItemsSource = GetItemSource();
            listView.ItemSelected += OnItemSelected;

            Title = "MasterPage";
            Detail = new NavigationPage(new HomePage());
            IsPresented = false;
        }

        List<MasterPageItem> GetItemSource() {
            List<MasterPageItem> list = new List<MasterPageItem>();
            
            list.Insert(0, new MasterPageItem { Title = "HomePage", TargetType = new HomePage().GetType() });
            
            if (_viewModel.IsAllowed)
                list.Add(new MasterPageItem { Title = "Users", TargetType = new UsersListStatic().GetType() });

            return list;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;

            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                listView.SelectedItem = null;
                IsPresented = false;
            }
        }

        async Task Button_Profile_Clicked(object sender, EventArgs e)
        {
            User user = await _viewModel.GetUserById();

            (Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new Profile(user));
            IsPresented = false;
        }

        void Button_Logout_Clicked(object sender, EventArgs e)
        {
            Settings.AuthenticationToken = "";
            Settings.CurrentUserRole = "";
            Settings.UserId = "";
            Settings.Password = "";

            Application.Current.MainPage = new LoginPage();
        }
    }
}
