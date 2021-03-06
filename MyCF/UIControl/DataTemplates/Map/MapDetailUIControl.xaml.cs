﻿using Brain.Animate;
using MyCF.Base;
using MyCF.Config;
using MyCF.Model.Map;
using MyCF.UIControl.UserControls.MapDetailFlipUICtrl;
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
    public sealed partial class MapDetailUIControl : UIControlBase
    {
        public MapDetailUIControl()
        {
            this.InitializeComponent();

            if (AppEnvironment.IsPhone)
            {
                if (AppEnvironment.IsPortrait)
                {
                    this.grid.Width = Window.Current.Bounds.Width - 20;
                    this.tbType.Margin = new Thickness(0, 0, 3, 0);
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
            base.OnRootFrameSizeChanged(sender, e);

            if (AppEnvironment.IsPhone)
            {
                if (AppEnvironment.IsPortrait)
                {
                    this.grid.Width = Window.Current.Bounds.Width - 20;
                    this.tbType.Margin = new Thickness(0, 0, 3, 0);
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

        private void grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var grid = sender as Grid;
            if (grid != null)
            {
                var model = grid.DataContext as Datum;
                if (model != null)
                {
                    MapDetailFlipBox.Instance.ShowMapDetailFlip(model);
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
