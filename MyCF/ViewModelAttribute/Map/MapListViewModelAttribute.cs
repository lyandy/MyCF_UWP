//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.ViewModelAttribute.Map
//类名称:       MapListViewModelAttribute
//创建时间:     2015/8/18 星期二 11:20:04
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Base;
using MyCF.Model.Map;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.ViewModelAttribute.Map
{
    public class MapListViewModelAttribute : ViewModelAttributeBase
    {
        public ObservableCollection<MapModel> _MapListCollection = new ObservableCollection<MapModel>();
        public ObservableCollection<MapModel> MapListCollection
        {
            get
            {
                return _MapListCollection;
            }
            set
            {
                if (_MapListCollection != value)
                {
                    _MapListCollection = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
