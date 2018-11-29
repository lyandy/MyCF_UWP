//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Base
//类名称:       ViewModelAttributeBase
//创建时间:     2015/8/8 星期六 10:55:36
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using GalaSoft.MvvmLight;
using MyCF.UIControl.UserControls.ProgressUICtrl;
using MyCF.UIControl.UserControls.RetryUICtrl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.Base
{
    public class ViewModelAttributeBase : ViewModelBase
    {
        #region IsBusy
        /// <summary>
        /// 只是是否正在加载数据
        /// </summary>
        private bool _IsBusy = false;
        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }

            set
            {
                if (_IsBusy != value)
                {
                    _IsBusy = value;

                    if (_IsBusy)
                    {
                        //及时隐藏数据加载不成功的提示
                        RetryBox.Instance.HideRetry();
                        ProgressBox.Instance.ShowProgress();
                    }
                    else
                    {
                        ProgressBox.Instance.HideProgress();
                    }

                    RaisePropertyChanged();
                }
            }
        }
        #endregion
    }
}
