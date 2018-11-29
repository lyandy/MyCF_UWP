﻿//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Extension.DependencyObjectEx
//类名称:       FrameworkElementEx
//创建时间:     2015/8/8 星期六 12:02:14
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace MyCF.Extension.DependencyObjectEx
{
    public static class FrameworkElementEx
    {
        public static IEnumerable<T> GetDescendantsOfType<T>(this DependencyObject start) where T : DependencyObject
        {
            return start.GetDescendants().OfType<T>();
        }
        /// <summary>
        /// Waits for the element to load (construct and add to the main object tree).
        /// </summary>
        public static async Task WaitForLoadedAsync(this FrameworkElement frameworkElement)
        {
            if (frameworkElement.IsInVisualTree())
                return;

            await EventAsync.FromRoutedEvent(
                eh => frameworkElement.Loaded += eh,
                eh => frameworkElement.Loaded -= eh);
        }

        /// <summary>
        /// Waits for the element to unload (disconnect from the main object tree).
        /// </summary>
        public static async Task WaitForUnloadedAsync(this FrameworkElement frameworkElement)
        {
            await EventAsync.FromRoutedEvent(
                eh => frameworkElement.Unloaded += eh,
                eh => frameworkElement.Unloaded -= eh);
        }

        /// <summary>
        /// Waits for the next layout update event.
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        /// <returns></returns>
        public static async Task WaitForLayoutUpdateAsync(this FrameworkElement frameworkElement)
        {
            await EventAsync.FromEvent<object>(
                eh => frameworkElement.LayoutUpdated += eh,
                eh => frameworkElement.LayoutUpdated -= eh);
        }

        /// <summary>
        /// Waits for the size of the element to become non-zero.
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        /// <returns></returns>
        public static async Task WaitForNonZeroSizeAsync(this FrameworkElement frameworkElement)
        {
            while (frameworkElement.ActualWidth == 0 && frameworkElement.ActualHeight == 0)
            {
                var tcs = new TaskCompletionSource<object>();

                SizeChangedEventHandler sceh = null;

                sceh = (s, e) =>
                {
                    frameworkElement.SizeChanged -= sceh;
                    tcs.SetResult(e);
                };

                frameworkElement.SizeChanged += sceh;

                await tcs.Task;

                //await EventAsync.FromEvent<object>(
                //    eh => frameworkElement.LayoutUpdated += eh,
                //    eh => frameworkElement.LayoutUpdated -= eh);
            }
        }

        /// <summary>
        /// Waits for all the image sources in the visual tree to complete loading (useful to call before a page transition).
        /// </summary>
        /// <remarks>
        /// Note that it does not take popups into account.
        /// </remarks>
        /// <param name="frameworkElement">The framework element.</param>
        /// <param name="millisecondsTimeout">The timeout in milliseconds.</param>
        /// <returns></returns>
        public static async Task WaitForImagesToLoad(this FrameworkElement frameworkElement, int millisecondsTimeout = 0)
        {
            //TODO: See if finding popups would be possible too.

            foreach (var image in frameworkElement.GetDescendantsOfType<Image>())
            {
                if (image.Source != null)
                {
                    var bi = image.Source as BitmapImage;

                    if (bi != null)
                    {
                        await bi.WaitForLoadedAsync(millisecondsTimeout);
                    }
                    else
                    {
                        var wb = image.Source as WriteableBitmap;

                        if (wb != null)
                        {
                            await wb.WaitForLoadedAsync(millisecondsTimeout);
                        }
                    }
                }
            }
        }
    }
}
