using GalaSoft.MvvmLight.Ioc;
using MyCF.Common;
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

namespace MyCF.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MapPage : Page
    {

        private MapListViewModel mapListVideModel;
        private NavigationHelper navigationHelper;
        public MapPage()
        {
            this.InitializeComponent();

            if (mapListVideModel == null)
            {
                if (!SimpleIoc.Default.IsRegistered<MapListViewModel>())
                {
                    SimpleIoc.Default.Register<MapListViewModel>();
                }

                mapListVideModel = SimpleIoc.Default.GetInstance<MapListViewModel>();
            }

            this.DataContext = mapListVideModel;

            this.navigationHelper = new NavigationHelper(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);

            if (mapListVideModel != null)
            {
                if (mapListVideModel.MapListCollection.Count == 0)
                {
                    mapListVideModel.GetMapList();
                }
            }
        }

        //protected 
        protected async override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }
    }
}
