using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Telephony;
using PrawkoAndroid.Droid;
using Xamarin.Forms;
using Uri = Android.Net.Uri;
using Android.Support.Design.Widget;
using Plugin.CurrentActivity;


[assembly: Dependency(typeof(Tost_Droid))]
namespace PrawkoAndroid.Droid
{
    class Tost_Droid : Interfejsy.ITost
    {
       public void Long(string s)
        {
            Activity activity = CrossCurrentActivity.Current.Activity;
            Android.Views.View activityRootView = activity.FindViewById(Android.Resource.Id.Content);
            Snackbar.Make(activityRootView, s, Snackbar.LengthLong).Show();

        }
        public void Short(string s)
        {
            Toast.MakeText( Forms.Context, s, ToastLength.Long).Show();
        }
    }
}