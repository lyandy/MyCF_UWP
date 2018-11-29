//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Helper
//类名称:       CommonHelper
//创建时间:     2015/8/8 星期六 11:13:31
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using GalaSoft.MvvmLight.Threading;
using MyCF.Base;
using MyCF.Extension;
using MyCF.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media.Imaging;

namespace MyCF.Helper
{
    public class CommonHelper : ClassBase<CommonHelper>
    {
        public CommonHelper() : base() { }

        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
        }

        public Grid GetCurrentAnimationGrid()
        {
            NavigationRootPage rootPage = Window.Current.Content as NavigationRootPage;

            if (rootPage != null)
            {
                Frame rootFrame = (Frame)rootPage.FindName("rootFrame");

                if (rootFrame != null)
                {
                    Grid grid = VisualTreeHelperEx.FindVisualChildByName<Grid>(rootFrame.Content as Page, "AnimationGrid");
                    if (grid != null)
                    {
                        return grid;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public TextBlock GetRootPageSubTitle()
        {
            NavigationRootPage rootPage = Window.Current.Content as NavigationRootPage;

            if (rootPage != null)
            {
                var tb = (TextBlock)rootPage.FindName("tbSubTitle");

                return tb;
            }
            else
            {
                return null;
            }
        }

        public void NavigateWithOverride(Type t, object parameter = null)
        {
            NavigationRootPage rootPage = Window.Current.Content as NavigationRootPage;

            if (rootPage != null)
            {
                Frame rootFrame = (Frame)rootPage.FindName("rootFrame");

                if (rootFrame != null)
                {
                    if (parameter != null)
                    {
                        rootFrame.Navigate(t, parameter);
                    }
                    else
                    {
                        rootFrame.Navigate(t);
                    }
                }
            }
        }

        public void ClearFrameBackStack()
        {
            NavigationRootPage rootPage = Window.Current.Content as NavigationRootPage;

            if (rootPage != null)
            {
                Frame rootFrame = (Frame)rootPage.FindName("rootFrame");
                if (rootFrame != null)
                {
                    rootFrame.BackStack.Clear();
                }
            }
        }

        public async Task<BitmapImage> LoadImageSource(string imagePath)
        {
            //BitmapImage比较奇怪，必须在UI线程进行操作
            BitmapImage img = null;
            //最好不要使用Windows.UI.Core.CoreDispatcherPriority.Low，因为会发生线程的优先级反转，即低优先级的阻塞了高优先级的线程
            await DispatcherHelper.UIDispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                using (var stream = await (await StorageFile.GetFileFromApplicationUriAsync(new Uri(imagePath, UriKind.RelativeOrAbsolute))).OpenAsync(FileAccessMode.Read))
                {
                    img = new BitmapImage();
                    using (stream)
                    {
                        await img.SetSourceAsync(stream);
                    }

                    //设置解析图像的宽和高
                    //try
                    //{
                    //    img.DecodePixelHeight = Convert.ToInt32(Window.Current.Bounds.Height);
                    //    img.DecodePixelWidth = Convert.ToInt32(Window.Current.Bounds.Width);
                    //}
                    //catch (Exception ex)
                    //{
                    //    LogHelper.WriteLog(LogType.Theme, "主题背景图片宽高设置失败。详细：" + ex.Message, true);
                    //}
                }
            });

            while (img == null)
            {
                await Task.Delay(1);
            }

            return img;
        }

        public async void RateApp()
        {
            Uri uri = new Uri(string.Format("ms-windows-store:{0}?appid={1}", "reviewapp", CurrentApp.AppId));
            await Launcher.LaunchUriAsync(uri);
        }

    }
}
