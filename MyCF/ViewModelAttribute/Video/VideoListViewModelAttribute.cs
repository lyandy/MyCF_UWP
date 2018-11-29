//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.ViewModelAttribute.Video
//类名称:       VideoListViewModelAttribute
//创建时间:     2015/8/14 星期五 15:35:48
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
    public class VideoListViewModelAttribute : ViewModelAttributeBase
    {
        #region 比赛
        public ObservableCollection<VideoModel> _VideoChampionCFPLCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoChampionCFPLCollection
        {
            get
            {
                return _VideoChampionCFPLCollection;
            }
            set
            {
                if (_VideoChampionCFPLCollection != value)
                {
                    _VideoChampionCFPLCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoChampionHundredsOfCityCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoChampionHundredsOfCityCollection
        {
            get
            {
                return _VideoChampionHundredsOfCityCollection;
            }
            set
            {
                if (_VideoChampionHundredsOfCityCollection != value)
                {
                    _VideoChampionHundredsOfCityCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoChampionAllPeopleCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoChampionAllPeopleCollection
        {
            get
            {
                return _VideoChampionAllPeopleCollection;
            }
            set
            {
                if (_VideoChampionAllPeopleCollection != value)
                {
                    _VideoChampionAllPeopleCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoChampionCFDLCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoChampionCFDLCollection
        {
            get
            {
                return _VideoChampionCFDLCollection;
            }
            set
            {
                if (_VideoChampionCFDLCollection != value)
                {
                    _VideoChampionCFDLCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoChampionCFSCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoChampionCFSCollection
        {
            get
            {
                return _VideoChampionCFSCollection;
            }
            set
            {
                if (_VideoChampionCFSCollection != value)
                {
                    _VideoChampionCFSCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoChampionOtherCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoChampionOtherCollection
        {
            get
            {
                return _VideoChampionOtherCollection;
            }
            set
            {
                if (_VideoChampionOtherCollection != value)
                {
                    _VideoChampionOtherCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region 节目

        public ObservableCollection<VideoModel> _VideoActSuperMethodCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoActSuperMethodCollection
        {
            get
            {
                return _VideoActSuperMethodCollection;
            }
            set
            {
                if (_VideoActSuperMethodCollection != value)
                {
                    _VideoActSuperMethodCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoActSuperRsenalCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoActSuperRsenalCollection
        {
            get
            {
                return _VideoActSuperRsenalCollection;
            }
            set
            {
                if (_VideoActSuperRsenalCollection != value)
                {
                    _VideoActSuperRsenalCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoActSuperTimeCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoActSuperTimeCollection
        {
            get
            {
                return _VideoActSuperTimeCollection;
            }
            set
            {
                if (_VideoActSuperTimeCollection != value)
                {
                    _VideoActSuperTimeCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoActFireCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoActFireCollection
        {
            get
            {
                return _VideoActFireCollection;
            }
            set
            {
                if (_VideoActFireCollection != value)
                {
                    _VideoActFireCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoActOtherCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoActOtherCollection
        {
            get
            {
                return _VideoActOtherCollection;
            }
            set
            {
                if (_VideoActOtherCollection != value)
                {
                    _VideoActOtherCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoActBrotherCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoActBrotherCollection
        {
            get
            {
                return _VideoActBrotherCollection;
            }
            set
            {
                if (_VideoActBrotherCollection != value)
                {
                    _VideoActBrotherCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region 高手

        public ObservableCollection<VideoModel> _VideoSuperTGASTARCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoSuperTGASTARCollection
        {
            get
            {
                return _VideoSuperTGASTARCollection;
            }
            set
            {
                if (_VideoSuperTGASTARCollection != value)
                {
                    _VideoSuperTGASTARCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoSuperProfressionalCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoSuperProfressionalCollection
        {
            get
            {
                return _VideoSuperProfressionalCollection;
            }
            set
            {
                if (_VideoSuperProfressionalCollection != value)
                {
                    _VideoSuperProfressionalCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoSuperPeopleCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoSuperPeopleCollection
        {
            get
            {
                return _VideoSuperPeopleCollection;
            }
            set
            {
                if (_VideoSuperPeopleCollection != value)
                {
                    _VideoSuperPeopleCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoSuperOtherCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoSuperOtherCollection
        {
            get
            {
                return _VideoSuperOtherCollection;
            }
            set
            {
                if (_VideoSuperOtherCollection != value)
                {
                    _VideoSuperOtherCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoSuperForeignerCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoSuperForeignerCollection
        {
            get
            {
                return _VideoSuperForeignerCollection;
            }
            set
            {
                if (_VideoSuperForeignerCollection != value)
                {
                    _VideoSuperForeignerCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region 教学

        public ObservableCollection<VideoModel> _VideoTeachGunCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoTeachGunCollection
        {
            get
            {
                return _VideoTeachGunCollection;
            }
            set
            {
                if (_VideoTeachGunCollection != value)
                {
                    _VideoTeachGunCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoTeachMapCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoTeachMapCollection
        {
            get
            {
                return _VideoTeachMapCollection;
            }
            set
            {
                if (_VideoTeachMapCollection != value)
                {
                    _VideoTeachMapCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoTeachSkillCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoTeachSkillCollection
        {
            get
            {
                return _VideoTeachSkillCollection;
            }
            set
            {
                if (_VideoTeachSkillCollection != value)
                {
                    _VideoTeachSkillCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoTeachOtherCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoTeachOtherCollection
        {
            get
            {
                return _VideoTeachOtherCollection;
            }
            set
            {
                if (_VideoTeachOtherCollection != value)
                {
                    _VideoTeachOtherCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region 官方
        public ObservableCollection<VideoModel> _VideoOfficialVCRCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoOfficialVCRCollection
        {
            get
            {
                return _VideoOfficialVCRCollection;
            }
            set
            {
                if (_VideoOfficialVCRCollection != value)
                {
                    _VideoOfficialVCRCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoOfficialGAMECGCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoOfficialGAMECGCollection
        {
            get
            {
                return _VideoOfficialGAMECGCollection;
            }
            set
            {
                if (_VideoOfficialGAMECGCollection != value)
                {
                    _VideoOfficialGAMECGCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoOfficialVersionCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoOfficialVersionCollection
        {
            get
            {
                return _VideoOfficialVersionCollection;
            }
            set
            {
                if (_VideoOfficialVersionCollection != value)
                {
                    _VideoOfficialVersionCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoOfficialOtherCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoOfficialOtherCollection
        {
            get
            {
                return _VideoOfficialOtherCollection;
            }
            set
            {
                if (_VideoOfficialOtherCollection != value)
                {
                    _VideoOfficialOtherCollection = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region 娱乐
        public ObservableCollection<VideoModel> _VideoPlayFunCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoPlayFunCollection
        {
            get
            {
                return _VideoPlayFunCollection;
            }
            set
            {
                if (_VideoPlayFunCollection != value)
                {
                    _VideoPlayFunCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoPlayMovieCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoPlayMovieCollection
        {
            get
            {
                return _VideoPlayMovieCollection;
            }
            set
            {
                if (_VideoPlayMovieCollection != value)
                {
                    _VideoPlayMovieCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<VideoModel> _VideoPlaySongCollection = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> VideoPlaySongCollection
        {
            get
            {
                return _VideoPlaySongCollection;
            }
            set
            {
                if (_VideoPlaySongCollection != value)
                {
                    _VideoPlaySongCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

    }
}
