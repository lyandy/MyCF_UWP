//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.ViewModelAttribute.News
//类名称:       NewsViewModelAttribute
//创建时间:     2015/8/10 星期一 11:49:49
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Base;
using MyCF.Model.News;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.ViewModelAttribute.News
{
    public class NewsViewModelAttribute : ViewModelAttributeBase
    {
        /// <summary>
        /// 资讯数据集合
        /// </summary>
        public ObservableCollection<NewsModel> _FirstNewsAdCollection = new ObservableCollection<NewsModel>();
        public ObservableCollection<NewsModel> FirstNewsAdCollection
        {
            get
            {
                return _FirstNewsAdCollection;
            }
            set
            {
                if (_FirstNewsAdCollection != value)
                {
                    _FirstNewsAdCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// 资讯数据集合
        /// </summary>
        public ObservableCollection<NewsModel> _FirstNewsCollection = new ObservableCollection<NewsModel>();
        public ObservableCollection<NewsModel> FirstNewsCollection
        {
            get
            {
                return _FirstNewsCollection;
            }
            set
            {
                if (_FirstNewsCollection != value)
                {
                    _FirstNewsCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// 赛事数据集合
        /// </summary>
        public ObservableCollection<NewsModel> _ChampionCollection = new ObservableCollection<NewsModel>();
        public ObservableCollection<NewsModel> ChampionCollection
        {
            get
            {
                return _ChampionCollection;
            }
            set
            {
                if (_ChampionCollection != value)
                {
                    _ChampionCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// 活动数据集合
        /// </summary>
        public ObservableCollection<ActivityModel> _ActivityCollection = new ObservableCollection<ActivityModel>();
        public ObservableCollection<ActivityModel> ActivityCollection
        {
            get
            {
                return _ActivityCollection;
            }
            set
            {
                if (_ActivityCollection != value)
                {
                    _ActivityCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// 动漫数据集合
        /// </summary>
        public ObservableCollection<NewsModel> _CartoonCollection = new ObservableCollection<NewsModel>();
        public ObservableCollection<NewsModel> CartoonCollection
        {
            get
            {
                return _CartoonCollection;
            }
            set
            {
                if (_CartoonCollection != value)
                {
                    _CartoonCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// 攻略数据集合
        /// </summary>
        public ObservableCollection<NewsModel> _StrategyCollection = new ObservableCollection<NewsModel>();
        public ObservableCollection<NewsModel> StrategyCollection
        {
            get
            {
                return _StrategyCollection;
            }
            set
            {
                if (_StrategyCollection != value)
                {
                    _StrategyCollection = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
