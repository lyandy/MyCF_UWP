using MyCF.Common;
using MyCF.Config;
using MyCF.UIControl.UserControls.WelcomeUICtrl;
using MyCF.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace MyCF
{
    /// <summary>
    /// 提供特定于应用程序的行为，以补充默认的应用程序类。
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// 初始化单一实例应用程序对象。这是执行的创作代码的第一行，
        /// 已执行，逻辑上等同于 main() 或 WinMain()。
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// 在应用程序由最终用户正常启动时进行调用。
        /// 将在启动应用程序以打开特定文件等情况下使用。
        /// </summary>
        /// <param name="e">有关启动请求和过程的详细信息。</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            await ShowWindow(e); 
        }

        private async Task ShowWindow(LaunchActivatedEventArgs e)
        {
            //获取并记录屏幕宽度，一定要在程序启动的时候记录，这样记录到的数据才是竖屏下的屏幕高度。屏幕旋转Window.Current.Bounds.With宽高会互换
            AppEnvironment.ScreenPortraitWith = Window.Current.Bounds.Width;
            //初始化线程
            GalaSoft.MvvmLight.Threading.DispatcherHelper.Initialize();

            NavigationRootPage rootPage = Window.Current.Content as NavigationRootPage;

            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootPage == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootPage = new NavigationRootPage();

                Frame rootFrame = (Frame)rootPage.FindName("rootFrame");

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 从之前挂起的应用程序加载状态
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                        //Something went wrong restoring state.
                        //Assume there is no state and continue
                    }
                }

                // 将框架放在当前窗口中
                Window.Current.Content = rootPage;

                //初始化手机和平板的背景颜色资源
                InitColors();

                // 确保当前窗口处于活动状态
                Window.Current.Activate();

                WelcomeBox.Instance.ShowWelcome();
                await Task.Delay(2150);

                if (rootFrame.Content == null)
                {
                    // 当导航堆栈尚未还原时，导航到第一页，
                    // 并通过将所需信息作为导航参数传入来配置
                    // 参数
                    rootFrame.Navigate(typeof(NewsPage), "情报站");
                }
            }
        }

        private void InitColors()
        {
            if (AppEnvironment.IsPhone)
            {
                (Application.Current.Resources["CommonBackgroundBrush"] as SolidColorBrush).Color = Colors.White;
                (Application.Current.Resources["pivotHeaderBackgroundBrush"] as SolidColorBrush).Color = Colors.White;
            }
        }

        public static BitmapImage EncyWeaponBg
        {
            get;
            set;
        }

        /// <summary>
        /// 导航到特定页失败时调用
        /// </summary>
        ///<param name="sender">导航失败的框架</param>
        ///<param name="e">有关导航失败的详细信息</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// 在将要挂起应用程序执行时调用。  在不知道应用程序
        /// 无需知道应用程序会被终止还是会恢复，
        /// 并让内存内容保持不变。
        /// </summary>
        /// <param name="sender">挂起的请求的源。</param>
        /// <param name="e">有关挂起请求的详细信息。</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: 保存应用程序状态并停止任何后台活动
            deferral.Complete();
        }
    }
}
