using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MyCF.Extension.DependencyObjectEx;
using MyCF.Helper;
using MyCF.UIControl.UserControls.ProgressUICtrl;
using MyCF.View.News;
using MyCF.ViewModel.News;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Brain.Animate;
using System.Threading.Tasks;
using MyCF.Common;
using MyCF.Locator;
using MyCF.UIControl.UserControls.WelcomeUICtrl;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace MyCF.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class NewsPage : Page
    {
        private NewsViewModel newsViewModel;

        private NavigationHelper navigationHelper;

        public NewsPage()
        {
            this.InitializeComponent();

            if (newsViewModel == null)
            {
                if (!SimpleIoc.Default.IsRegistered<NewsViewModel>())
                {
                    SimpleIoc.Default.Register<NewsViewModel>();
                }

                newsViewModel = SimpleIoc.Default.GetInstance<NewsViewModel>();
            }

            this.DataContext = newsViewModel;

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
        }
    }
}
