using GalaSoft.MvvmLight.Ioc;
using MyCF.Common;
using MyCF.Config;
using MyCF.Const;
using MyCF.Helper;
using MyCF.ViewModel.Video;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace MyCF.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class VideoPage : Page
    {
        private NavigationHelper navigationHelper;
        private VideoCategoryViewModel videoCategoryViewModel;
        public VideoPage()
        {
            this.InitializeComponent();

            if (videoCategoryViewModel == null)
            {
                if (!SimpleIoc.Default.IsRegistered<VideoCategoryViewModel>())
                {
                    SimpleIoc.Default.Register<VideoCategoryViewModel>();
                }

                videoCategoryViewModel = SimpleIoc.Default.GetInstance<VideoCategoryViewModel>();
            }

            this.DataContext = videoCategoryViewModel;

            this.navigationHelper = new NavigationHelper(this);
            if (AppEnvironment.IsPhone)
            {
                this.hub.Visibility = Visibility.Collapsed;
                this.gridPI.Visibility = Visibility.Visible;
            }
            else
            {
                this.hub.Visibility = Visibility.Visible;
                this.gridPI.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 处理状态改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //if (AppEnvironment.IsPhone)
            //{
            //    if (AppEnvironment.IsPortrait)
            //    {
            //        //根据Hub所在的Section索引，更改Pivot的SelectionIndex
            //        this.pi.SelectedIndex = this.hub.Sections.IndexOf(this.hub.SectionsInView[0]);

            //        this.hub.Visibility = Visibility.Collapsed;
            //        this.gridPI.Visibility = Visibility.Visible;
            //    }
            //    else
            //    {
            //        //根据Pivot的SelectionIndex，更改Hub选中的Section
            //        this.hub.ScrollToSection(this.hub.Sections[this.pi.SelectedIndex]);

            //        this.hub.Visibility = Visibility.Visible;
            //        this.gridPI.Visibility = Visibility.Collapsed;
            //    }
            //}
            //else
            //{
            //    this.hub.Visibility = Visibility.Visible;
            //    this.gridPI.Visibility = Visibility.Collapsed;
            //}

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);

            if (videoCategoryViewModel != null)
            {
                if (videoCategoryViewModel.VideoCategoryChampionCollection.Count == 0)
                {
                    videoCategoryViewModel.InitVideoCategory();
                }
            }
        }

        //protected 
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        private void GridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var gridView = sender as GridView;
            if (gridView != null)
            {
                DicStore.AddOrUpdateValue<int>(AppCommonConst.CUR_PIVOT_SELECTED_INDEX, gridView.SelectedIndex);
            }
        }
    }
}
