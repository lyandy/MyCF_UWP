using Brain.Animate;
using MyCF.Base;
using MyCF.Config;
using MyCF.Const;
using MyCF.Helper;
using MyCF.Model.Map;
using MyCF.View.Map;
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
    public sealed partial class MapListUIControl : UIControlBase
    {
        public MapListUIControl()
        {
            this.InitializeComponent();

            this.gridImg.Height = 100;

            if (AppEnvironment.IsPhone)
            {
                if (AppEnvironment.IsPortrait)
                {
                    //this.grid.Width = AppEnvironment.ScreenPortraitWith;
                    this.grid.Width = (Window.Current.Bounds.Width - 28) / 2;

                    this.gridImg.Height = 80;

                    this.grid.Height = 106;

                    if (AppEnvironment.ScreenPortraitWith == 320)
                    {
                        this.tbMapName.MaxWidth = 75;
                    }
                    else
                    {
                        this.tbMapName.MaxWidth = 80;
                    }
                    
                }
                else
                {
                    this.grid.Width = ((Window.Current.Bounds.Width - 48) - 33) / 2;
                }
            }
            else
            {
                var width = Window.Current.Bounds.Width - 48;

                if (width < 550)
                {
                    this.grid.Width = width - 1 * 12;
                }
                else if (width >= 550 && width < 900)
                {
                    this.grid.Width = (width - 2 * 12) / 2;
                }
                else if (width >= 900 && width < 1190)
                {
                    this.grid.Width = (width - 3 * 12) / 3;

                }
                else if (width >= 1190 && width < 1490)
                {
                    this.grid.Width = (width - 4 * 12) / 4;
                }
                else
                {
                    this.grid.Width = (width - 6 * 12) / 5;
                }
            }
        }

        protected override void OnRootFrameSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.gridImg.Height = 100;

            if (AppEnvironment.IsPhone)
            {
                if (AppEnvironment.IsPortrait)
                {
                    //this.grid.Width = AppEnvironment.ScreenPortraitWith;
                    this.grid.Width = (Window.Current.Bounds.Width - 28) / 2;

                    this.gridImg.Height = 80;
                }
                else
                {
                    this.grid.Width = ((Window.Current.Bounds.Width - 48) - 33) / 2;
                }
            }
            else
            {
                if (e.NewSize.Width < 550)
                {
                    this.grid.Width = e.NewSize.Width - 1 * 12;
                }
                else if (e.NewSize.Width >= 550 && e.NewSize.Width < 900)
                {
                    this.grid.Width = (e.NewSize.Width - 2 * 12) / 2;
                }
                else if (e.NewSize.Width >= 900 && e.NewSize.Width < 1190)
                {
                    this.grid.Width = (e.NewSize.Width - 3 * 12) / 3;

                }
                else if (e.NewSize.Width >= 1190 && e.NewSize.Width < 1490)
                {
                    this.grid.Width = (e.NewSize.Width - 4 * 12) / 4;
                }
                else
                {
                    this.grid.Width = (e.NewSize.Width - 6 * 12) / 5;
                }
            }
        }

        private async void grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var grid = sender as Grid;
            if (grid != null)
            {
                var model = grid.DataContext as MapModel;
                if (model != null)
                {
                    var animationGrid = CommonHelper.Instance.GetCurrentAnimationGrid();
                    if (animationGrid != null)
                    {
                        if (AppEnvironment.IsPhone)
                        {
                            await animationGrid.AnimateAsync(new FadeOutLeftAnimation() { Duration = 0.3, Distance = 400 });
                        }
                        else
                        {
                            await animationGrid.AnimateAsync(new FadeOutLeftAnimation() { Duration = 0.13, Distance = 600 });
                        }
                    }

                    DicStore.AddOrUpdateValue<int>(AppCommonConst.CURRENT_MAP_ID, Convert.ToInt32(model.id));

                    CommonHelper.Instance.NavigateWithOverride(typeof(MapDetailPage));

                    if (!AppEnvironment.IsPhone)
                    {
                        var tb = CommonHelper.Instance.GetRootPageSubTitle();
                        if (tb != null)
                        {
                            tb.Text = " / " + model.map_name;
                        }
                    }

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
