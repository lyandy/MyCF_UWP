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
using Windows.UI.ViewManagement;
using MyCF.Config;
using MyCF.Model.Video;
using MyCF.Helper;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MyCF.UIControl.DataTemplates
{
    public sealed partial class VideoCategoryPivotUIControl : UserControl
    {
        public VideoCategoryPivotUIControl()
        {
            this.InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var grid = sender as Grid;
            if (grid != null)
            {
                grid.Width = AppEnvironment.ScreenPortraitWith / 2 - 20;
            }
        }

        private async void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var grid = sender as Grid;
            if (grid != null)
            {
                var model = grid.DataContext as VideoCategoryModel;
                if (model != null)
                {
                    var animationGrid = CommonHelper.Instance.GetCurrentAnimationGrid();
                    if (animationGrid != null)
                    {
                        await animationGrid.AnimateAsync(new FadeOutLeftAnimation() { Duration = 0.3, Distance = 400 });
                    }

                    CommonHelper.Instance.NavigateWithOverride(model.ClassType);

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
