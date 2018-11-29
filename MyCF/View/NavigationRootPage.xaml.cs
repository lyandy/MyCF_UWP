using GalaSoft.MvvmLight.Ioc;
using MyCF.Config;
using MyCF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Brain.Animate;
using System.Diagnostics;
using MyCF.Model;
using Windows.Graphics.Display;
using MyCF.ViewModel.Video;
using System.Threading.Tasks;
using MyCF.Model.Video;
using System.Reflection;
using MyCF.Locator;
using Windows.UI;
using MyCF.Helper;
using MyCF.UIControl.UserControls.RetryUICtrl;
using MyCF.ViewModel.Ency;
using MyCF.Model.Ency;
using MyCF.Const;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace MyCF.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class NavigationRootPage : Page
    {
        private NavigationRootViewModel navigationRootViewModel;
        public NavigationRootPage()
        {
            this.InitializeComponent();

            if (navigationRootViewModel == null)
            {
                if (!SimpleIoc.Default.IsRegistered<NavigationRootViewModel>())
                {
                    SimpleIoc.Default.Register<NavigationRootViewModel>();
                }

                navigationRootViewModel = SimpleIoc.Default.GetInstance<NavigationRootViewModel>();
            }

            this.DataContext = navigationRootViewModel;

            this.Loaded += NavigationRootPage_Loaded;

            //注册物理或者虚拟后退键 - 已无用
            //SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;

            //DisplayInformation.GetForCurrentView().OrientationChanged += NavigationRootPage_OrientationChanged;
            //如果是手机，则默认全屏模式
            if (AppEnvironment.IsPhone)
            {
                ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
                //ApplicationView.GetForCurrentView().TitleBar
            }
            else
            {
                // DisplayProperties.CurrentOrientation 发生变化时触发的事件
                
                //设置平板窗口的最小宽高
                ApplicationView.GetForCurrentView().SetPreferredMinSize(AppEnvironment.DesktopSize);
            }
        }

        private void NavigationRootPage_OrientationChanged(DisplayInformation sender, object args)
        {
            //Debug.WriteLine(sender.CurrentOrientation);
        }

        private async void NavigationRootPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (navigationRootViewModel != null)
            {
                await navigationRootViewModel.InitCategoryPngs();
                //获取类别展示数据
                navigationRootViewModel.GetCFCommonCollection();

                //获取底部展示数据
                navigationRootViewModel.GetCFBottomCollection();

                //默认选中第一条
                lvCommonCategory.SelectedIndex = 0;

            }
        }

        #region back页面后退的处理

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            bool ignored = false;
            ViewModelLocator.Instance.NavigateBack(ref ignored);
        }
        #endregion

        #region 页面堆栈维护
        private List<Page> listPageStack = new List<Page>();
        #endregion

        #region 页面导航的处理
        //页面导航完成后
        private void OnFrameNavigated(object sender, NavigationEventArgs e)
        {
            //页面跳转传递参数的处理
            var parameter = e.Parameter as string;
            if (parameter != null)
            {
                this.tbTitle.Text = parameter;
            }

            //如果点击了左侧的类别，则隐藏返回按钮，同时清空导航堆栈
            if (isItemClicked)
            {
                this.backButton.Visibility = Visibility.Collapsed;
                this.rootFrame.BackStack.Clear();
                listPageStack.Clear();
            }

            //一定要放在清理的下面，顺序不可调换
            if (e.NavigationMode == NavigationMode.New)
            {
                listPageStack.Add(this.rootFrame.Content as Page);
            }

            //如果是手机，则直接隐藏后退键
            if (AppEnvironment.IsPhone)
            {
                this.backButton.Visibility = Visibility.Collapsed;   
            }
            //如果不是手机则处理后退键的显隐性
            else
            {
                this.backButton.Visibility = this.rootFrame.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
            }

            isItemClicked = false;
        }

        private void rootFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            //页面一旦发生导航跳转都要隐藏重试提示
            RetryBox.Instance.HideRetry();

            if (isItemClicked)
            {
                foreach (var lps in listPageStack)
                {
                    lps.NavigationCacheMode = NavigationCacheMode.Disabled;
                }

                (this.rootFrame.Content as Page).NavigationCacheMode = NavigationCacheMode.Disabled;
            }
        }

        //是否点击了ListView
        bool isItemClicked = false;
        private async void lv_ItemClick(object sender, ItemClickEventArgs e)
        {
            //this.topbar.SecondaryCommands

            var model = e.ClickedItem as NavigationRootModel;
            if (model != null)
            {
                isItemClicked = true;
                //取消上下栏ListView的选择
                var listview = sender as ListView;
                if (listview != null)
                {
                    switch (listview.Tag.ToString())
                    {
                        case "lvCommon":
                            lvBottomCategory.SelectedIndex = -1;
                            break;
                        case "lvBottom":
                            lvCommonCategory.SelectedIndex = -1;
                            break;
                    }
                }

                //及时关闭SpliteView
                if (rootSplitView.IsPaneOpen)
                {
                    rootSplitView.IsPaneOpen = false;
                }

                if (rootFrame.BackStack.Count == 0)
                {
                    if (rootFrame.SourcePageType == model.ClassType)
                    {
                        isItemClicked = false;
                        Debug.WriteLine("0我不导");
                        return;
                    }
                }
                else if (rootFrame.BackStack.Count > 0)
                {
                    if (rootFrame.BackStack[0].SourcePageType == model.ClassType)
                    {
                        isItemClicked = false;
                        Debug.WriteLine("非0我也不导");
                        return;
                    }
                }

                //页面一旦发生导航跳转都要隐藏重试提示
                RetryBox.Instance.HideRetry();

                var animationGrid = CommonHelper.Instance.GetCurrentAnimationGrid();
                if (animationGrid != null)
                {
                    if (AppEnvironment.IsPhone)
                    {
                        await animationGrid.AnimateAsync(new FadeOutRightAnimation() { Duration = 0.25, Distance = 400 });
                    }
                    else
                    {
                        await animationGrid.AnimateAsync(new FadeOutRightAnimation() { Duration = 0.13, Distance = 600 });
                    }
                }

                //重置火线百科数据状态
                resetWeaponCategory();

                rootFrame.Navigate(model.ClassType, model.Title);

                isItemClicked = false;
            }
        }

        private void lvCommonCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.gridCommandCover.Visibility = this.lvCommonCategory.SelectedIndex == 2 ? Visibility.Collapsed : Visibility.Visible;
        }
        #endregion

        #region 汉堡菜单的打开和关闭
        private void splitViewToggleButton_Click(object sender, RoutedEventArgs e)
        {
            this.rootSplitView.IsPaneOpen = !this.rootSplitView.IsPaneOpen;

            this.splitViewToggleButtonIcon.RotateAsync(0.2, 90);
        }

        private void rootSplitView_PaneClosing(SplitView sender, SplitViewPaneClosingEventArgs args)
        {
            this.splitViewToggleButtonIcon.RotateAsync(0.2, 0);
        }
        #endregion

        #region 处理状态改变
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //处理CommandBar
            if (AppEnvironment.IsPhone)
            { 
                if (AppEnvironment.IsPortrait)
                {
                    this.topbar.Margin = new Thickness(0, 0, 0, 0);
                    this.headerRoot.Margin = new Thickness(48, 0, 0, 0);
                }
                else
                {
                    this.topbar.Margin = new Thickness(48, 0, 0, 0);
                    this.headerRoot.Margin = new Thickness(0, 0, 0, 0);
                }
            }
            else
            {
                this.topbar.Margin = new Thickness(48, 0, 0, 0);
                this.headerRoot.Margin = new Thickness(0, 0, 0, 0);
            }

            //处理SplitView的状态
            if (AppEnvironment.IsPortrait)
            {
                if (AppEnvironment.IsPhone)
                {
                    this.rootSplitView.DisplayMode = SplitViewDisplayMode.Overlay;
                }
            }
            else
            {
                this.rootSplitView.DisplayMode = SplitViewDisplayMode.CompactOverlay;
            }
        }
        #endregion

        #region 火线百科数据排序
        bool isPowerSort = false;
        private void AppBarToggleButton_Click(object sender, RoutedEventArgs e)
        {
            isPowerSort = this.appBarToggleBtn.IsChecked.Value;

            SortWeaponCollection();
        }

        string currentCoinType = "";

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            var appBarButton = sender as AppBarButton;
            switch (appBarButton.Tag.ToString())
            {
                case "All":
                    currentCoinType = "";
                    break;
                case "CF":
                    currentCoinType = "CF";
                    break;
                case "GP":
                    currentCoinType = "GP";
                    break;
                case "FP":
                    currentCoinType = "FP";
                    break;
            }

            SortWeaponCollection();
        }

        private async void SortWeaponCollection()
        {
            if (!AppEnvironment.IsPhone)
            {
                this.tbSubTitle.Text = " / " + (currentCoinType == "" ? "全部货币" : (currentCoinType + "点"));
            }
            else
            {
                if (currentCoinType != "")
                {
                    this.tbSubTitle.Text = " / " + (currentCoinType + "点");
                }
                else
                {
                    this.tbSubTitle.Text = "";
                }
            }

            if (!SimpleIoc.Default.IsRegistered<EncyViewModel>())
            {
                SimpleIoc.Default.Register<EncyViewModel>();
            }

            var encyVideModel = SimpleIoc.Default.GetInstance<EncyViewModel>();

            var selectCollection = from weapon in encyVideModel.WeaponCollectionCopy
                                   where weapon.cointype.Contains(currentCoinType)
                                   select weapon;

            encyVideModel.WeaponCollection = await Task.Run(() =>
            {
                return new ObservableCollection<WeaponModel>(isPowerSort ? selectCollection.ToList().OrderByDescending(o => Convert.ToDouble(string.IsNullOrEmpty(o.power.ToString()) ? 0 : o.power)) : selectCollection);
            });
        }

        private void resetWeaponCategory()
        {
            if (!AppEnvironment.IsPhone)
            {
                this.tbSubTitle.Text = this.lvCommonCategory.SelectedIndex == 2 ? " / 全部货币" : "";
            }
            else
            {
                this.tbSubTitle.Text = "";
            }
            currentCoinType = this.lvCommonCategory.SelectedIndex == 2 ? "" : "";

            this.appBarToggleBtn.IsChecked = false;
            isPowerSort = false;
        }
        #endregion
    }
}
