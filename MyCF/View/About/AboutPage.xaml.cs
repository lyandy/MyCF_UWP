using MyCF.Common;
using MyCF.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Brain.Animate;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace MyCF.View.About
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AboutPage : Page
    {
        private NavigationHelper navigationHelper;
        public AboutPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);

            var with = Window.Current.Bounds.Width * 2  / 3;

            if (with <= 320)
            {
                this.grid.Width = Window.Current.Bounds.Width;
            }
            else
            {
                this.grid.Width = with;
            }

        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var with = Window.Current.Bounds.Width  * 2 / 3;

            if (with <= 320)
            {
                this.grid.Width = Window.Current.Bounds.Width;
            }
            else
            {
                this.grid.Width = with;
            }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);

            this.pathMark.RotateAsync(0.5, 360);
            this.pathMark.AnimateAsync(new BounceInAnimation() { Duration = 0.5 });
            await this.pathMark.AnimateAsync(new FadeInAnimation() { Duration = 0.5 });

            this.tbName.AnimateAsync(new FadeInRightAnimation() { Distance = 600, Duration = 0.25 });
            await this.tbVersion.AnimateAsync(new FadeInLeftAnimation() { Distance = 600, Duration = 0.25 });

            this.tbAuthor.AnimateAsync(new FlipInXAnimation());

            await this.btnReview.AnimateAsync(new FadeInDownAnimation());
            this.btnReview.AnimateAsync(new TadaAnimation() { Forever = true });

            await this.cooperateImage.AnimateAsync(new FadeInUpAnimation());
        }

        private async void cooperateImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ayywin://"));
        }

        //protected 
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CommonHelper.Instance.RateApp();
        }
    }
}
