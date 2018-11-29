using Brain.Animate;
using MyCF.Base;
using MyCF.Config;
using MyCF.Const;
using MyCF.Helper;
using MyCF.Model.News;
using MyCF.View.News;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MyCF.UIControl.DataTemplates
{
    public sealed partial class NewsActivityMobileUIControl : UIControlBase
    {
        public NewsActivityMobileUIControl()
        {
            this.InitializeComponent();

            if (AppEnvironment.IsPortrait)
            {
                this.grid.Width = AppEnvironment.ScreenPortraitWith;
            }
            else
            {
                this.grid.Width = Window.Current.Bounds.Width - 48;
            }
        }

        protected override void OnRootFrameSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (AppEnvironment.IsPortrait)
            {
                this.grid.Width = AppEnvironment.ScreenPortraitWith;
            }
            else
            {
                this.grid.Width = e.NewSize.Width;
            }
        }

        private async void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var grid = sender as Grid;
            if (grid != null)
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
    }
}
