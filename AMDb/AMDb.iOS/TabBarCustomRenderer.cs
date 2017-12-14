using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using AMDb;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabBarCustomRenderer))]
namespace AMDb
{
    public class TabBarCustomRenderer : TabbedRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            // Set Text Font for unselected tab states
            UITextAttributes normalTextAttributes = new UITextAttributes();
            normalTextAttributes.Font = UIFont.FromName("BanglaSangamMN-Bold", 10.0F); // unselected
            UITabBarItem.Appearance.SetTitleTextAttributes(normalTextAttributes, UIControlState.Normal);
            TabBar.TintColor = UIColor.Red;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            var popular = UIImage.FromFile("popularIcon.png");
            int tabID = 0;
            foreach (UIViewController viewController in base.ViewControllers)
            {
                if (tabID == 0) { viewController.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search, 1); }
                if (tabID == 1) { viewController.TabBarItem = new UITabBarItem(UITabBarSystemItem.TopRated, 1); }
                if (tabID == 2) { viewController.TabBarItem = new UITabBarItem("Popular", popular, 1); }
                tabID++;
            }
        }

        public override UIViewController SelectedViewController
        {
            get
            {
                UITextAttributes selectedTextAttributes = new UITextAttributes();
                selectedTextAttributes.Font = UIFont.FromName("BanglaSangamMN-Bold", 12.0F);
                if (base.SelectedViewController != null)
                {
                    base.SelectedViewController.TabBarItem.SetTitleTextAttributes(selectedTextAttributes, UIControlState.Normal);
                }
                
                return base.SelectedViewController;
            }
            set
            {
                base.SelectedViewController = value;

                foreach (UIViewController viewController in base.ViewControllers)
                {
                    UITextAttributes normalTextAttributes = new UITextAttributes();
                    normalTextAttributes.Font = UIFont.FromName("BanglaSangamMN-Bold", 10.0F); // unselected
                    viewController.TabBarItem.SetTitleTextAttributes(normalTextAttributes, UIControlState.Normal);
                }
            }
        }
    }
}