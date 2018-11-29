using MyCF.Base;
using MyCF.Config;
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
using System.Threading.Tasks;
using MyCF.Helper;
using MyCF.View.News;
using MyCF.Model.News;
using MyCF.Const;
using MyCF.Extension.DependencyObjectEx;
using MyCF.UIControl.UserControls.VideoPlayUICtrl;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MyCF.UIControl.DataTemplates
{
    public sealed partial class NewsCommonMobileUIControl : UIControlBase
    {
        public NewsCommonMobileUIControl()
        {
            this.InitializeComponent();

            if (AppEnvironment.IsPortrait)
            {
                this.grid.Width = AppEnvironment.ScreenPortraitWith;

                this.tbTitle.Width = this.grid.Width - 112;
            }
            else
            {
                this.grid.Width = Window.Current.Bounds.Width - 48;
                this.tbTitle.Width = this.grid.Width - 112;
            }

            this.tbTitle.Loaded -= TbTitle_Loaded;
            this.tbTitle.Loaded += TbTitle_Loaded;
        }

        private async void TbTitle_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.tbTitle.ActualHeight >= 35.0)
            {
                this.tbSummary.VerticalAlignment = VerticalAlignment.Top;
            }
            else
            {
                this.tbSummary.VerticalAlignment = VerticalAlignment.Center;
            }

            await this.WaitForLayoutUpdateAsync();
            this.UpdateLayout();
        }

        protected override void OnRootFrameSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (AppEnvironment.IsPortrait)
            {
                this.grid.Width = AppEnvironment.ScreenPortraitWith;

                this.tbTitle.Width = this.grid.Width - 112;
            }
            else
            {
                this.grid.Width = e.NewSize.Width;
                this.tbTitle.Width = this.grid.Width - 112;
            }

            //再次调节设定summary描述的位置
            TbTitle_Loaded(null, null);
        }

        bool isLoaded = false;

        private async void grid_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            //虚拟化会重新执行此方法绑定数据
            //await grid.AnimateAsync(new FadeInLeftAnimation());
            //await grid.AnimateAsync(new BounceInDownAnimation());


            var g = sender as Grid;
            if (g != null && !isLoaded)
            {
                await Task.Delay(500);

                var animationName = new FadeInDownAnimation();
                animationName.Distance = 100;
                g.AnimateAsync(animationName);

                isLoaded = true;
            }
        }

        private async void grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var grid = sender as Grid;
            if (grid != null)
            {
                var videoNewsModel = grid.DataContext as NewsModel;
                if (videoNewsModel != null && videoNewsModel.IsVideo)
                {
                    var videoInfo = videoNewsModel.backup3;
                    if (videoInfo != null)
                    {
                        VideoPlayBox.Instance.ShowVideo(videoInfo.vid, videoNewsModel.title, null);
                    }
                }
                else
                {
                    var model = grid.DataContext as NewsModelPropertyBase;
                    if (model != null)
                    {
                        var animationGrid = CommonHelper.Instance.GetCurrentAnimationGrid();
                        if (animationGrid != null)
                        {
                            //if (AppEnvironment.IsPhone)
                            //{
                            //    await animationGrid.AnimateAsync(new FadeOutLeftAnimation() { Distance = 300 });
                            //}
                            //else
                            //{
                            await animationGrid.AnimateAsync(new FadeOutLeftAnimation() { Duration = 0.3, Distance = 400 });
                            //}
                        }

                        //记录当前的模型Model以便在网页成功加载后能够更改条目的标题颜色
                        DicStore.AddOrUpdateValue<NewsModelPropertyBase>(AppCommonConst.CURRENT_NEWS_MODEL, model);

                        CommonHelper.Instance.NavigateWithOverride(typeof(NewsDetailPage));

                        await animationGrid.AnimateAsync(new ResetAnimation());
                    }
                }
            }
        }
    }
}
