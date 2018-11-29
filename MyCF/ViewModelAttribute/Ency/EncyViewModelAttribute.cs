//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.ViewModelAttribute.Ency
//类名称:       EncyViewModelAttribute
//创建时间:     2015/8/17 星期一 12:20:27
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Base;
using MyCF.Model.Ency;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.ViewModelAttribute.Ency
{
    public class EncyViewModelAttribute : ViewModelAttributeBase
    {
        public ObservableCollection<WeaponModel> _WeaponCollection = new ObservableCollection<WeaponModel>();
        public ObservableCollection<WeaponModel> WeaponCollection
        {
            get
            {
                return _WeaponCollection;
            }
            set
            {
                if (_WeaponCollection != value)
                {
                    _WeaponCollection = value;
                    RaisePropertyChanged();
                }
            }
        }


        public ObservableCollection<WeaponModel> _WeaponCollectionCopy = new ObservableCollection<WeaponModel>();
        public ObservableCollection<WeaponModel> WeaponCollectionCopy
        {
            get
            {
                return _WeaponCollectionCopy;
            }
            set
            {
                if (_WeaponCollectionCopy != value)
                {
                    _WeaponCollectionCopy = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
