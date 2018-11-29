//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.ViewModelAttribute.Video
//类名称:       VideoCategoryViewModelAttribute
//创建时间:     2015/8/9 星期日 17:57:07
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Base;
using MyCF.Model.Video;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.ViewModelAttribute.Video
{
    public class VideoCategoryViewModelAttribute : ViewModelAttributeBase
    {
        /// <summary>
        /// 比赛视频
        /// </summary>
        public ObservableCollection<VideoCategoryModel> _VideoCategoryChampionCollection = new ObservableCollection<VideoCategoryModel>();
        public ObservableCollection<VideoCategoryModel> VideoCategoryChampionCollection
        {
            get
            {
                return _VideoCategoryChampionCollection;
            }
            set
            {
                if (_VideoCategoryChampionCollection != value)
                {
                    _VideoCategoryChampionCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// 节目视频
        /// </summary>
        public ObservableCollection<VideoCategoryModel> _VideoCategoryActCollection = new ObservableCollection<VideoCategoryModel>();
        public ObservableCollection<VideoCategoryModel> VideoCategoryActCollection
        {
            get
            {
                return _VideoCategoryActCollection;
            }
            set
            {
                if (_VideoCategoryActCollection != value)
                {
                    _VideoCategoryActCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// 教学视频
        /// </summary>
        public ObservableCollection<VideoCategoryModel> _VideoCategoryTeachCollection = new ObservableCollection<VideoCategoryModel>();
        public ObservableCollection<VideoCategoryModel> VideoCategoryTeachCollection
        {
            get
            {
                return _VideoCategoryTeachCollection;
            }
            set
            {
                if (_VideoCategoryTeachCollection != value)
                {
                    _VideoCategoryTeachCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// 高手视频
        /// </summary>
        public ObservableCollection<VideoCategoryModel> _VideoCategorySuperCollection = new ObservableCollection<VideoCategoryModel>();
        public ObservableCollection<VideoCategoryModel> VideoCategorySuperCollection
        {
            get
            {
                return _VideoCategorySuperCollection;
            }
            set
            {
                if (_VideoCategorySuperCollection != value)
                {
                    _VideoCategorySuperCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// 官方视频
        /// </summary>
        public ObservableCollection<VideoCategoryModel> _VideoCategoryOfficialCollection = new ObservableCollection<VideoCategoryModel>();
        public ObservableCollection<VideoCategoryModel> VideoCategoryOfficialCollection
        {
            get
            {
                return _VideoCategoryOfficialCollection;
            }
            set
            {
                if (_VideoCategoryOfficialCollection != value)
                {
                    _VideoCategoryOfficialCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// 娱乐视频
        /// </summary>
        public ObservableCollection<VideoCategoryModel> _VideoCategoryPlayCollection = new ObservableCollection<VideoCategoryModel>();
        public ObservableCollection<VideoCategoryModel> VideoCategoryPlayCollection
        {
            get
            {
                return _VideoCategoryPlayCollection;
            }
            set
            {
                if (_VideoCategoryPlayCollection != value)
                {
                    _VideoCategoryPlayCollection = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
