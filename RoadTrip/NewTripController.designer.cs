﻿// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace RoadTrip
{
    [Register ("NewTripController")]
    partial class NewTripController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton aboardButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField cityInput { get; set; }

        [Action ("AboardButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void AboardButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (aboardButton != null) {
                aboardButton.Dispose ();
                aboardButton = null;
            }

            if (cityInput != null) {
                cityInput.Dispose ();
                cityInput = null;
            }
        }
    }
}