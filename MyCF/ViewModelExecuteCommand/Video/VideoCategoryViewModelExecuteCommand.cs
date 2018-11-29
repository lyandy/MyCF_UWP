//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.ViewModelExecuteCommand.Video
//类名称:       VideoCategoryViewModelExecuteCommand
//创建时间:     2015/8/17 星期一 10:06:04
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using GalaSoft.MvvmLight.Command;
using MyCF.UIControl.UserControls.RetryUICtrl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml.Controls;

namespace MyCF.ViewModelExecuteCommand.Video
{
    public class VideoCategoryViewModelExecuteCommand
    {
        private RelayCommand<Pivot> _VideoCategoryPivotLoadedCommand;
        public RelayCommand<Pivot> VideoCategoryPivotLoadedCommand
        {
            get
            {
                return _VideoCategoryPivotLoadedCommand
                    ?? (_VideoCategoryPivotLoadedCommand = new RelayCommand<Pivot>(o =>
                    {
                        //隐藏加载错误提示
                        RetryBox.Instance.HideRetry();

                        foreach (var ite in o.Items)
                        {
                            var piItem = ite as PivotItem;
                            if (piItem != null)
                            {
                                var tb = piItem.Header as TextBlock;
                                if (tb != null)
                                {
                                    if (piItem == o.SelectedItem)
                                    {
                                        tb.FontWeight = FontWeights.Bold;
                                    }
                                    else
                                    {
                                        tb.FontWeight = FontWeights.Normal;
                                    }
                                }
                            }
                        }
                    }
                ));
            }
        }
    }
}
