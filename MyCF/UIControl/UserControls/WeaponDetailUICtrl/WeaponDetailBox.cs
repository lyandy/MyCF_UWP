//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.UIControl.UserControls.WeaponDetailUICtrl
//类名称:       WeaponDetailBox
//创建时间:     2015/8/17 星期一 18:36:19
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using GalaSoft.MvvmLight.Threading;
using MyCF.Base;
using MyCF.Model.Ency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Primitives;

namespace MyCF.UIControl.UserControls.WeaponDetailUICtrl
{
    public class WeaponDetailBox : ClassBase<WeaponDetailBox>
    {
        public WeaponDetailBox() : base() { }

        Popup popup = null;


        public async void ShowWeaponDetail(WeaponModel weaponModel)
        {
            await DispatcherHelper.RunAsync(() =>
            {
                try
                {
                    //HideRetry();

                    WeaponDetailUIControl wduc = new WeaponDetailUIControl();

                    wduc.weaponModel = weaponModel;

                    if (popup == null)
                    {
                        popup = new Popup();
                    }

                    if (!popup.IsOpen)
                    {
                        popup.Child = wduc;

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
