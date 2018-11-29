using Brain.Animate;
using MyCF.Base;
using MyCF.Config;
using MyCF.Model.Map;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MyCF.UIControl.UserControls.MapDetailFlipUICtrl
{
    public sealed partial class MapDetailFlipUIControl : UIControlBase
    {
        public MapDetailFlipUIControl()
        {
            this.InitializeComponent();

            this.grid.Width = Window.Current.Bounds.Width;
            this.grid.Height = Window.Current.Bounds.Height;

            if (AppEnvironment.IsPhone)
            {
                this.spInfo.Width = Window.Current.Bounds.Width - 6;
                this.spInfo.MaxHeight = 300;
                this.spInfo.VerticalAlignment = VerticalAlignment.Bottom;
                this.spInfo.HorizontalAlignment = HorizontalAlignment.Center;

                //针对4英寸屏幕的Padding
                if (Window.Current.Bounds.Width == 320)
                {
                    this.flipViewImages.Padding = new Thickness(0, 0, 0, 200);
                }
                else
                {
                    this.flipViewImages.Padding = new Thickness(0, 0, 0, 150);
                }

                this.tbPicDesc.FontSize = 13;
            }
            else
            {
                this.spInfo.Width = 395;
                this.spInfo.Height = this.Height;
            }
        }

        public Datum da { get; set; }

        private async void grid_Loaded(object sender, RoutedEventArgs e)
        {
            #region 背景及视频布局
            this.gridBeneath.AnimateAsync(new FadeInAnimation());

            this.flipViewImages.ItemsSource = da.purls; 
            this.tbTitle.Text = da.p_name;
            this.tbPicDesc.Text = da.p_desc;

            if (AppEnvironment.IsPhone)
            {
                this.grid.Tapped += Grid_Tapped;
            }
            else
            {
                this.grid.Tapped -= Grid_Tapped;
                this.appCloseBtn.AnimateAsync(new FadeInDownAnimation());
            }

            await this.spInfo.AnimateAsync(new FadeInUpAnimation());

            #endregion
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MapDetailFlipBox.Instance.HideMapDetailFlip();
        }

        protected override void OnRootFrameSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.grid.Width = Window.Current.Bounds.Width;
            this.grid.Height = Window.Current.Bounds.Height;
        }
        private void flipViewImages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.runPicIndex.Text = this.flipViewImages.SelectedIndex + 1 + "";

            this.runPicCount.Text = this.da.purls.Count + "";
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            MapDetailFlipBox.Instance.HideMapDetailFlip();
        }
    }
}
