using MyCF.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Brain.Animate;
using MyCF.UIControl.UserControls.VideoPlayUICtrl;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MyCF.UIControl.DataTemplates
{
    public sealed partial class NewsCommonUIControl : UIControlBase
    {
        public NewsCommonUIControl()
        {
            this.InitializeComponent();

            var width = Window.Current.Bounds.Width - 48;

            if (width < 550)
            {
                this.grid.Width = width - 1 * 12;
                
                this.grid.Height = 190;
            }
            else if (width >= 550 && width < 900)
            {
                this.grid.Width = (width - 2 * 12) / 2;

                this.grid.Height = 230;
            }
            else if (width >= 900 && width < 1190)
            {
                this.grid.Width = (width - 3 * 12) / 3;

                this.grid.Height = 230;
            }
            else if (width >= 1190 && width < 1490)
            {
                this.grid.Width = (width - 4 * 12) / 4;

                this.grid.Height = 230;
            }
            else
            {
                this.grid.Width = (width - 6 * 12) / 5;

                this.grid.Height = 230;
            }

            this.tbSummary.Width = this.grid.Width - 170;
        }

        //protected override void Test()
        //{
        //    Debug.WriteLine("子类已经执行");
        //}
       
        //public void Test1()
        //{
        //    base.Test1();
        //    Debug.WriteLine("子类已经执行Test1");
        //}

        protected override void OnRootFrameSizeChanged(object sender, SizeChangedEventArgs e)
        {
            base.OnRootFrameSizeChanged(sender, e);

            if (e.NewSize.Width < 550)
            {
                this.grid.Width = e.NewSize.Width - 1 * 12;

                this.grid.Height = 190;
            }
            else if (e.NewSize.Width >= 550 && e.NewSize.Width < 900)
            {
                this.grid.Width = (e.NewSize.Width - 2 * 12) / 2;

                this.grid.Height = 230;
            }
            else if (e.NewSize.Width >= 900 && e.NewSize.Width < 1190)
            {
                this.grid.Width = (e.NewSize.Width - 3 * 12) / 3;

                this.grid.Height = 230;

            }
            else if (e.NewSize.Width >= 1190 && e.NewSize.Width < 1490)
            {
                this.grid.Width = (e.NewSize.Width - 4 * 12) / 4;

                this.grid.Height = 230;
            }
            else
            {
                this.grid.Width = (e.NewSize.Width - 6 * 12) / 5;

                this.grid.Height = 230;
            }

            this.tbSummary.Width = this.grid.Width - 170;

            //int count = (int)(e.NewSize.Width) / 400;

            //this.grid.Width = (e.NewSize.Width) / (count);
            //this.grid.Width -= 40;

            //Debug.WriteLine("count: " + count + "  width: " + this.grid.Width);
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
                            await animationGrid.AnimateAsync(new FadeOutLeftAnimation() { Duration = 0.13, Distance = 600 });
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
