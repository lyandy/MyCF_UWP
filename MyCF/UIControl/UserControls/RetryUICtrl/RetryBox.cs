//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.UIControl.UserControls.RetryUICtrl
//类名称:       RetryBox
//创建时间:     2015/8/11 星期二 15:16:32
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using GalaSoft.MvvmLight.Threading;
using MyCF.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Primitives;

namespace MyCF.UIControl.UserControls.RetryUICtrl
{
    class RetryBox : ClassBase<RetryBox>
    {
        public RetryBox() : base() { }

        Popup popup = null;


        public async void ShowRetry(string msg, Type fromType, string method, object[] parameters)
        {
            await DispatcherHelper.RunAsync(() =>
            {
                try
                {
                    //HideRetry();

                    RetryUIControl ruc = new RetryUIControl();
                    ruc.msg = msg;
                    ruc.fromType = fromType;
                    ruc.method = method;
                    ruc.parameters = parameters;


                    if (popup == null)
                    {
                        popup = new Popup();
                    }

                    if (!popup.IsOpen)
                    {
                        popup.Child = ruc;

                        popup.IsLightDismissEnabled = false;
                        //应当占据NavigationRootPage的rootFrame区域，此区域距离左和上的距离都为48，在加上Pivot的Header头高度为45（已取消）
                        //popup.VerticalOffset = 48;
                        //if (!AppEnvironment.IsPhone)
                        //{
                        //    popup.HorizontalOffset = 48;
                        //}

                        popup.IsOpen = true;
                    }
                }
                catch { }
            });
        }

        public async void HideRetry()
        {
            try
            {
                await DispatcherHelper.RunAsync(() =>
                {
                    try
                    {
                        if (popup != null)
                        {
                            if (popup.IsOpen)
                            {
                                popup.IsOpen = false;
                                popup.Child = null;
                                popup = null;
                            }
                        }
                    }
                    catch { }
                });
            }
            catch
            {
                string s = string.Empty;
            }
        }
    }
}
