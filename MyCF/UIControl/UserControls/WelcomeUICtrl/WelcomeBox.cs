//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.UIControl.UserControls.WelcomeUICtrl
//类名称:       WelcomeBox
//创建时间:     2015/8/20 星期四 15:02:18
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

namespace MyCF.UIControl.UserControls.WelcomeUICtrl
{
    public class WelcomeBox : ClassBase<WelcomeBox>
    {
        public WelcomeBox() : base() { }

        Popup popup = null;


        public async void ShowWelcome()
        {
            await DispatcherHelper.RunAsync(() =>
            {
                try
                {
                    //HideRetry();

                    WelcomeUIControl wuc = new WelcomeUIControl();

                    if (popup == null)
                    {
                        popup = new Popup();
                    }

                    if (!popup.IsOpen)
                    {
                        popup.Child = wuc;

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

        public async void HideWeaponDetail()
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

        public bool IsWeaponDetailShow
        {
            get
            {
                return popup == null ? false : true;
            }
        }
    }
}
