//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.ViewModelAttribute
//类名称:       MainViewModelAttribute
//创建时间:     2015/8/8 星期六 11:38:44
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Base;
using MyCF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace MyCF.ViewModelAttribute
{
    public class NavigationRootViewModelAttribute : ViewModelAttributeBase
    {
        #region 左侧类别数据集合
        public ObservableCollection<NavigationRootModel> _CFCommonCollection = new ObservableCollection<NavigationRootModel>();
        public ObservableCollection<NavigationRootModel> CFCommonCollection
        {
            get
            {
                return _CFCommonCollection;
            }
            set
            {
                if (_CFCommonCollection != value)
                {
                    _CFCommonCollection = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region 底部分割线一下关于集合
        public ObservableCollection<NavigationRootModel> _CFBottomCollection = new ObservableCollection<NavigationRootModel>();
        public ObservableCollection<NavigationRootModel> CFBottomCollection
        {
            get
            {
                return _CFBottomCollection;
            }
            set
            {
                if (_CFBottomCollection != value)
                {
                    _CFBottomCollection = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region 左侧图片集合
        public BitmapImage NewsPNG
        {
            get;
            set;
        }

        public BitmapImage VideoPNG
        {
            get;
            set;
        }

        public BitmapImage SetPNG
        {
            get;
            set;
        }

        public BitmapImage MapPNG
        {
            get;
            set;
        }

        public BitmapImage AboutPNG
        {
            get;
            set;
        }
        #endregion
    }
}
