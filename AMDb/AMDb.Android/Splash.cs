using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AMDb.Droid
{
    [Activity(Label = "AMDb", Theme = "@style/splashTheme", MainLauncher = true, Icon = "@drawable/icon")]
    public class InitializerActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.StartActivity(typeof(MainActivity));
            this.Finish();
        }
    }
}