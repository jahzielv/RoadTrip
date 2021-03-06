using Foundation;
using System;
using UIKit;

using CoreGraphics;

using Facebook.LoginKit;
using Facebook.CoreKit;

using System.Collections.Generic;

namespace RoadTrip
{
    public partial class LoginScreen : UIViewController
    {
        public LoginScreen (IntPtr handle) : base (handle)
        {
        }
        List<string> readPermissions = new List<string> { "public_profile" };

        LoginButton loginView;
        Model.User user;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            if (AccessToken.CurrentAccessToken != null)
            {
                user = DataStorage.Instance.GetUser(AccessToken.CurrentAccessToken.UserID);
                continueButton.Hidden = false;
                continueButton.UserInteractionEnabled = true;
            }
            else
            {
                continueButton.Hidden = true;
                continueButton.UserInteractionEnabled = false;
            }

            continueButton.TouchUpInside += (sender, e) => {
                PerformSegue("returningUserSegue", this);
            };
            // Set the Read and Publish permissions you want to get
            loginView = new LoginButton(new CGRect(77, 288, 218, 46))
            {
                LoginBehavior = LoginBehavior.Native,
                ReadPermissions = readPermissions.ToArray()
            };

            // Handle actions once the user is logged in
            loginView.Completed += (sender, e) =>
            {
                if (e.Error != null || e.Result.IsCancelled)
                {
                    showLoginError();
                    return;
                }
                // Handle your successful login
                if (DataStorage.Instance.GetUser(AccessToken.CurrentAccessToken.UserID) == null) {
                    DataStorage.Instance.AddNewUser();
                }
                if (DataStorage.Instance.GetUser(AccessToken.CurrentAccessToken.UserID) != null) {
                    user = DataStorage.Instance.GetUser(AccessToken.CurrentAccessToken.UserID);
                    PerformSegue("returningUserSegue", this);
                } else {
                    showLoginError();
                }
            };

            // Handle actions once the user is logged out
            loginView.LoggedOut += (sender, e) =>
            {
                continueButton.Hidden = true;
                continueButton.UserInteractionEnabled = false;
                user = null;
            };

            // Add views to main view
            View.AddSubview(loginView);
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);
            if (user.name == "")
            {
                var request = new GraphRequest("me", null);
                request.Start((connection, result, error) =>
                {
                    var userInfo = result as Foundation.NSDictionary;
                    user.name = userInfo["name"].ToString();
                    DataStorage.Instance.UpdateUser(user);
                });
            }
            var returningUser = segue.DestinationViewController as NewTripController;
            returningUser.user = user;
            
        }

        private void showLoginError() {
            var alert = UIAlertController.Create("Error",
                                                         "Login failed. Please try again.",
                                                         UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Cancel, null));
            PresentViewController(alert, true, null);
        }
    }
}