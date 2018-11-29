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
using MyCF.Helper;
using MyCF.Config;
using MyCF.Base;
using System.Threading.Tasks;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MyCF.UIControl.UserControls.VideoPlayUICtrl
{
    public sealed partial class VideoPlayUIControl : UIControlBase
    {
        public VideoPlayUIControl()
        {
            this.InitializeComponent();

            this.grid.Width = Window.Current.Bounds.Width;
            this.grid.Height = Window.Current.Bounds.Height;
        }

        public string vid { get; set; }
        public string title { get; set; }

        public string aipaiVideoUrl { get; set; }

        private void HidePhoneVideoButton()
        {
            this.videoMediaPlayer.IsTimeElapsedVisible = false;
            this.videoMediaPlayer.IsDurationVisible = false;
            this.videoMediaPlayer.IsVolumeVisible = false;
            this.videoMediaPlayer.IsFullScreenVisible = false;
            this.videoMediaPlayer.IsResolutionIndicatorVisible = false;
        }

        private void SHowPhoneVideoButton()
        {
            this.videoMediaPlayer.IsTimeElapsedVisible = true;
            this.videoMediaPlayer.IsDurationVisible = true;
            this.videoMediaPlayer.IsVolumeVisible = true;
            this.videoMediaPlayer.IsFullScreenVisible = true;
            this.videoMediaPlayer.IsResolutionIndicatorVisible = true;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            #region 背景及视频布局
            this.gridBeneath.AnimateAsync(new FadeInAnimation());

            if (AppEnvironment.IsPhone)
            {
                HidePhoneVideoButton();

                this.videoMediaPlayer.Width = AppEnvironment.ScreenPortraitWith - 5;

                this.tbBam.FontSize = 15;
            }
            else
            {
                if (Window.Current.Bounds.Width < 700)
                {
                    this.videoMediaPlayer.Width = Window.Current.Bounds.Width;
                    this.videoMediaPlayer.Margin = new Thickness(2.5, 0, 2.5, 0);
                    this.tbBam.FontSize = 20;
                }
                else
                {
                    this.videoMediaPlayer.Width = Window.Current.Bounds.Width * 2 / 3;
                    this.videoMediaPlayer.Margin = new Thickness(0, 0, 0, 0);
                    this.tbBam.FontSize = 25;

                }
            }
            this.videoMediaPlayer.Height = this.videoMediaPlayer.Width * 9 / 16;
            //确保是16 : 9比例
            this.videoMediaPlayer.Height = this.videoMediaPlayer.Width * 9 / 16;
            #endregion

            #region 视频解码
            try
            {
                if (!string.IsNullOrEmpty(aipaiVideoUrl))
                {
                    this.videoMediaPlayer.Source = new Uri(aipaiVideoUrl, UriKind.RelativeOrAbsolute);
                }
                else
                {
                    var decodeResult = await QVideoHelper.Instance.DecodeVid(vid);
                    if (decodeResult != null)
                    {

                        var videoUrl = decodeResult.vd.vi[0].url;
                        //这里要注意是本地路径是否会异常
                        if (Uri.IsWellFormedUriString(videoUrl, UriKind.RelativeOrAbsolute))
                        {
                            this.videoMediaPlayer.Source = new Uri(videoUrl, UriKind.RelativeOrAbsolute);
                        }
                    }
                }
            }
            catch
            {
                this.tbNotice.Visibility = Visibility.Visible;
            }
            #endregion

            #region 对RelativePanel及其按钮的布局
            if (AppEnvironment.IsPhone)
            {
                this.relPanel.VerticalAlignment = VerticalAlignment.Bottom;
            }
            else
            {
                this.relPanel.VerticalAlignment = VerticalAlignment.Top;
            }

            if (AppEnvironment.IsPhone)
            {
                await this.appCloseBtn.AnimateAsync(new FadeInUpAnimation());
                await this.appFullScreenBtn.AnimateAsync(new FadeInUpAnimation());
                await this.appVolumeBtn.AnimateAsync(new FadeInUpAnimation());
            }
            else
            {
                await this.appCloseBtn.AnimateAsync(new FadeInDownAnimation());
            }

            if (this.tbNotice.Visibility == Visibility.Collapsed)
            { 
                this.tbBam.VerticalAlignment = VerticalAlignment.Top;
                this.tbBam.HorizontalAlignment = HorizontalAlignment.Left;

                double marginLeft = Window.Current.Bounds.Width / 2 - this.videoMediaPlayer.Width / 2;
                double marginTop = Window.Current.Bounds.Height / 2 - this.videoMediaPlayer.Height / 2 - (AppEnvironment.IsPhone ? 20 : 40);

                this.tbBam.Margin = new Thickness(marginLeft, marginTop, 0, 5);

                this.tbBam.Text = title;
                await tbBam.AnimateText(RandomAnimationHelper.Instance.GetAnimation(), 0.1);

                this.appFullScreenBtn.Visibility = Visibility.Visible;
                this.appVolumeBtn.Visibility = Visibility.Visible;
            }
            else
            {
                this.appFullScreenBtn.Visibility = Visibility.Collapsed;
                this.appVolumeBtn.Visibility = Visibility.Collapsed;
            }

            #endregion
        }

        public bool ReleaeVideo()
        {
            //如果现在是全屏，则先关闭全屏
            if (this.videoMediaPlayer.IsFullScreen)
            {
                this.videoMediaPlayer.IsFullScreen = false;

                return false;
            }

            this.videoMediaPlayer.Stop();
            this.videoMediaPlayer.Source = null;
            return true;
        }


        private async void videoMediaPlayer_IsFullScreenChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {

            if (this.videoMediaPlayer.IsFullScreen)
            {
                this.tbBam.Visibility = Visibility.Collapsed;
            }

            if (!AppEnvironment.IsPhone)
            {
                var appView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
                if (this.videoMediaPlayer.IsFullScreen)
                {
                    appView.TryEnterFullScreenMode();

                }
                else
                {
                    appView.ExitFullScreenMode();
                }
            }
            else
            {
                if (AppEnvironment.IsPhone)
                {
                    if (this.videoMediaPlayer.IsFullScreen)
                    {
                        this.videoMediaPlayer.RotateAsync(0.2, 90);

                        this.videoMediaPlayer.Width = Window.Current.Bounds.Height;
                        this.videoMediaPlayer.Height = Window.Current.Bounds.Width;

                        double margin = (Window.Current.Bounds.Height - Window.Current.Bounds.Width) / 2;

                        //这句话一定要写啊，妈的隔壁这行代码的出现我研究了一个多小时才知道原来还要设置Margin。微软一坑逼。
                        this.videoMediaPlayer.Margin = new Thickness(-margin, 0, -margin, 0);

                        SHowPhoneVideoButton();

                        await this.appCloseBtn.AnimateAsync(new FadeOutDownAnimation());
                        await this.appFullScreenBtn.AnimateAsync(new FadeOutDownAnimation());
                        await this.appVolumeBtn.AnimateAsync(new FadeOutDownAnimation());

                    }
                    else
                    {
                        this.videoMediaPlayer.RotateAsync(0.2, 0);
                        this.videoMediaPlayer.Width = AppEnvironment.ScreenPortraitWith - 10;
                        this.videoMediaPlayer.Height = this.videoMediaPlayer.Width * 9 / 16;
                        this.videoMediaPlayer.Margin = new Thickness(0, 0, 0, 0);

                        HidePhoneVideoButton();

                        this.tbBam.Visibility = Visibility.Visible;
                        tbBam.AnimateText(RandomAnimationHelper.Instance.GetAnimation(), 0.1);

                        await this.appCloseBtn.AnimateAsync(new FadeInUpAnimation());
                        await this.appFullScreenBtn.AnimateAsync(new FadeInUpAnimation());
                        await this.appVolumeBtn.AnimateAsync(new FadeInUpAnimation());
                    }
                }
            }

            if (!this.videoMediaPlayer.IsFullScreen && !AppEnvironment.IsPhone)
            {
                this.tbBam.Visibility = Visibility.Visible;
                await tbBam.AnimateText(RandomAnimationHelper.Instance.GetAnimation(), 0.1);
            }

        }

        protected async override void OnRootFrameSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.grid.Width = Window.Current.Bounds.Width;
            this.grid.Height = Window.Current.Bounds.Height;

            if (this.videoMediaPlayer.IsFullScreen)
            {
                //这里判断是手机是用来出来当应用由后台恢复的时候全屏能够正确布局视频尺寸
                if (AppEnvironment.IsPhone)
                {
                    //this.videoMediaPlayer.Width = Window.Current.Bounds.Height;
                    //this.videoMediaPlayer.Height = Window.Current.Bounds.Width;
                }
                else
                {
                    this.videoMediaPlayer.Width = Window.Current.Bounds.Width;
                    this.videoMediaPlayer.Height = Window.Current.Bounds.Height;

                    await this.appCloseBtn.AnimateAsync(new FadeOutUpAnimation());
                }
            }
            else
            {
                //这里判断是手机是用来出来当应用由后台恢复的时候非全屏能够正确布局视频尺寸
                if (AppEnvironment.IsPhone)
                {
                    this.videoMediaPlayer.Width = AppEnvironment.ScreenPortraitWith - 10;
                }
                else
                {
                    if (Window.Current.Bounds.Width < 700)
                    {
                        this.videoMediaPlayer.Width = Window.Current.Bounds.Width;
                        this.videoMediaPlayer.Margin = new Thickness(2.5, 0, 2.5, 0);
                        this.tbBam.FontSize = 10;
                    }
                    else
                    {
                        this.videoMediaPlayer.Width = Window.Current.Bounds.Width * 2 / 3;
                        this.videoMediaPlayer.Margin = new Thickness(0, 0, 0, 0);
                        this.tbBam.FontSize = 25;

                    }
                }
                this.videoMediaPlayer.Height = this.videoMediaPlayer.Width * 9 / 16;

                if (AppEnvironment.IsPhone)
                {
                    await this.appCloseBtn.AnimateAsync(new FadeInUpAnimation());
                }
                else
                {

                    this.tbBam.VerticalAlignment = VerticalAlignment.Top;
                    this.tbBam.HorizontalAlignment = HorizontalAlignment.Left;

                    double marginLeft = Window.Current.Bounds.Width / 2 - this.videoMediaPlayer.Width / 2;
                    double marginTop = Window.Current.Bounds.Height / 2 - this.videoMediaPlayer.Height / 2 - (AppEnvironment.IsPhone ? 20 : 40);

                    this.tbBam.Margin = new Thickness(marginLeft, marginTop, 0, 5);

                    if (this.appCloseBtn.Opacity == 0)
                    {
                        await this.appCloseBtn.AnimateAsync(new FadeInDownAnimation());
                    }
                }
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            VideoPlayBox.Instance.HideVideo();
        }

        private void UIControlBase_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (this.videoMediaPlayer.IsFullScreen && e.Key == Windows.System.VirtualKey.Escape)
            {
                this.videoMediaPlayer.IsFullScreen = false;
            }
        }

        private void appVolumeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.videoMediaPlayer.IsMuted = !this.videoMediaPlayer.IsMuted;
        }

        private void appFullScreenBtn_Click(object sender, RoutedEventArgs e)
        {
            this.videoMediaPlayer.IsFullScreen = true;
        }
    }
}
