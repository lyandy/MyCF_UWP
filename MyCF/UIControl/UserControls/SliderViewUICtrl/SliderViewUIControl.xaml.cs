using Brain.Animate;
using MyCF.Const;
using MyCF.Extension;
using MyCF.Helper;
using MyCF.Model.News;
using MyCF.UIControl.UserControls.VideoPlayUICtrl;
using MyCF.View.News;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MyCF.UIControl.UserControls.SliderViewUICtrl
{
    public sealed partial class SliderViewUIControl : UserControl
    {
        public SliderViewUIControl()
        {
            this.InitializeComponent();
        }

        private void rootGrid_Loaded(object sender, RoutedEventArgs e)
        {
            this.rootGrid.Width = (Window.Current.Bounds.Width);
            this.rootGrid.Height = (Window.Current.Bounds.Width) * 624 / 1242;
        }

        private void img_Loaded(object sender, RoutedEventArgs e)
        {
            Image img = sender as Image;
            img.Width = (Window.Current.Bounds.Width);
            img.Height = (Window.Current.Bounds.Width) * 624 / 1242;
        }

        DispatcherTimer timer = null;
        private void newsFlipView_Loaded(object sender, RoutedEventArgs e)
        {
            // 幻灯自动切换
            try
            {
                if (timer != null)
                {
                    timer.Stop();
                    timer = null;
                }

                if (timer == null)
                {
                    timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(5.0);
                    timer.Tick += (delegate (object s2, object e2)
                    {
                        if (newsFlipView.Items.Count > 1)
                        {
                            try
                            {
                                if (newsFlipView.SelectedIndex == newsFlipView.Items.Count - 1)
                                {
                                    newsFlipView.SelectedIndex = 0;
                                }
                                else
                                {
                                    newsFlipView.SelectedIndex += 1;
                                }
                            }
                            catch
                            {

                            }
                        }
                    });
                    timer.Start();
                }
            }
            catch
            {
                if (timer != null)
                {
                    timer.Stop();
                    timer = null;
                }
            }
        }

        bool isLoaded = false;
        private async void rootGrid_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
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

        private async void Grid_Tapped(object sender, TappedRoutedEventArgs e)
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
        
        //解决第一次显示Listbox右下端指示器的时候，第一个不指示的Bug
        private void newsFlipView_LayoutUpdated(object sender, object e)
        {
            try
            {
                listBox.SelectedIndex = -1;
                listBox.SelectedIndex = newsFlipView.SelectedIndex;
            }
            catch { }
        }
    }
}
