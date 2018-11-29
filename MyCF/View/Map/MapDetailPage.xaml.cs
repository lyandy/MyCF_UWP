using GalaSoft.MvvmLight.Ioc;
using MyCF.Common;
using MyCF.Config;
using MyCF.Const;
using MyCF.Helper;
using MyCF.ViewModel.Map;
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

namespace MyCF.View.Map
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MapDetailPage : Page
    {
        private MapDetailViewModel mapDetailViewModel;
        private NavigationHelper navigationHelper;
        public MapDetailPage()
        {
            this.InitializeComponent();

            if (mapDetailViewModel == null)
            {
                if (!SimpleIoc.Default.IsRegistered<MapDetailViewModel>())
                {
                    SimpleIoc.Default.Register<MapDetailViewModel>();
                }

                mapDetailViewModel = SimpleIoc.Default.GetInstance<MapDetailViewModel>();
            }

            this.DataContext = mapDetailViewModel;

            this.navigationHelper = new NavigationHelper(this);
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);

            if (mapDetailViewModel != null)
            {
                if (mapDetailViewModel.MapDetailCollection.Count == 0)
                {
                    mapDetailViewModel.GetMapDetail();
                }
            }
        }

        //protected 
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);

            if (!AppEnvironment.IsPhone)
            {
                var tb = CommonHelper.Instance.GetRootPageSubTitle();
                if (tb != null)
                {
                    tb.Text = "";
                }
            }

            if (e.NavigationMode == NavigationMode.Back)
            {
                if (mapDetailViewModel != null)
                {
                    mapDetailViewModel.Cleanup();

                    SimpleIoc.Default.Unregister<MapDetailViewModel>();
                }
            }
        }
    }
}
