using GalaSoft.MvvmLight.Ioc;
using MyCF.Common;
using MyCF.Config;
using MyCF.Helper;
using MyCF.Model.Ency;
using MyCF.ViewModel.Ency;
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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace MyCF.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class EncyPage : Page
    {
        private EncyViewModel encyVideModel;
        private NavigationHelper navigationHelper;
        public EncyPage()
        {
            this.InitializeComponent();

            if (encyVideModel == null)
            {
                if (!SimpleIoc.Default.IsRegistered<EncyViewModel>())
                {
                    SimpleIoc.Default.Register<EncyViewModel>();
                }

                encyVideModel = SimpleIoc.Default.GetInstance<EncyViewModel>();
            }

            this.DataContext = encyVideModel;

            this.navigationHelper = new NavigationHelper(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);

            if (!AppEnvironment.IsPhone)
            {
                if (e.NavigationMode == NavigationMode.New)
                {
                    var tb = CommonHelper.Instance.GetRootPageSubTitle();
                    if (tb != null)
                    {
                        tb.Text = " / " + "全部货币";
                    }
                }
            }

            if (encyVideModel != null)
            {
                if (encyVideModel.WeaponCollection.Count == 0)
                {
                    encyVideModel.GetEncyWeapons();
                }
            }
        }

        //protected 
        protected async override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);

            if (e.NavigationMode == NavigationMode.New)
            {
                if (encyVideModel != null)
                {
                    encyVideModel.WeaponCollection = await Task.Run(() =>
                    {
                        return new ObservableCollection<WeaponModel>(encyVideModel.WeaponCollectionCopy);
                    });
                }
            }
        }
    }
}
