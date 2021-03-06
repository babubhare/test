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
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace test.Extensions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomAlertPopUp : PopupPage
	{
        #region Attributes and Properties

        public static BindableProperty AlertTitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(ContentView), "Alert", BindingMode.OneWay);

        public static BindableProperty MessageProperty =
            BindableProperty.Create("Message", typeof(string), typeof(ContentView), null, BindingMode.OneWay);

        public static BindableProperty ButtonTextProperty =
            BindableProperty.Create("Button", typeof(string), typeof(ContentView), "OK", BindingMode.OneWay);

        public string AlertTitle
        {
            get { return (string)GetValue(AlertTitleProperty); }
            set { SetValue(AlertTitleProperty, value); }
        }

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        #endregion

        #region Events

        public event EventHandler ButtonClickedEvent;
        public event EventHandler DismissTappedEvent;

        #endregion

        public CustomAlertPopUp()
		{
			InitializeComponent();
            BindingContext = this;
		}

        public CustomAlertPopUp(string message, string title = "Alert", string button = "OK") : this() 
        {
            Message = message;
            AlertTitle = title;
            ButtonText = button;
        }


        void Dismiss_PopUp(object sender, EventArgs e)
        {
            OnDismissTappedEvent(EventArgs.Empty);
            PopupNavigation.Instance.PopAsync();
        }

        void ButtonClicked_PopUp(object sender, EventArgs e)
        {
            OnConfirmButtonClicked(EventArgs.Empty);
            PopupNavigation.Instance.PopAsync();
        }

        protected virtual void OnDismissTappedEvent(EventArgs e)
        {
            DismissTappedEvent?.Invoke(this, e);
        }

        protected virtual void OnConfirmButtonClicked(EventArgs e)
        {
            ButtonClickedEvent?.Invoke(this, e);
        }
    }
}