using GalaSoft.MvvmLight.Ioc;
using MyCF.Common;
using MyCF.ViewModel.Video;
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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace MyCF.View.Video
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class OfficialPage : Page
    {
        private NavigationHelper navigationHelper;
        private VideoListViewModel videoListViewModel;
        public OfficialPage()
        {
            this.InitializeComponent();

            if (videoListViewModel == null)
            {
                if (!SimpleIoc.Default.IsRegistered<VideoListViewModel>())
                {
                    SimpleIoc.Default.Register<VideoListViewModel>();
                }

                videoListViewModel = SimpleIoc.Default.GetInstance<VideoListViewModel>();

                this.DataContext = videoListViewModel;
            }

            this.navigationHelper = new NavigationHelper(this);

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        //protected 
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);

            if (e.NavigationMode == NavigationMode.Back)
            {
                if (videoListViewModel != null)
                {
                    videoListViewModel.Cleanup();

                    SimpleIoc.Default.Unregister<VideoListViewModel>();
                }
            }
        }
    }
}
