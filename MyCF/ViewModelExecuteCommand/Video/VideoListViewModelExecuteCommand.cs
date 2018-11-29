//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.ViewModelExecuteCommand.Video
//类名称:       VideoListViewModelExecuteCommand
//创建时间:     2015/8/14 星期五 11:19:48
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using MyCF.Api.ApiRoot;
using MyCF.Const;
using MyCF.Helper;
using MyCF.UIControl.UserControls.RetryUICtrl;
using MyCF.ViewModel.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml.Controls;

namespace MyCF.ViewModelExecuteCommand.Video
{
    public class VideoListViewModelExecuteCommand
    {
        #region PivotLoadedCommand + ListViewLoadMoreCommand
        #region 比赛

        private RelayCommand<Pivot> _ChampionPivotLoadedCommand;
        public RelayCommand<Pivot> ChampionPivotLoadedCommand
        {
            get
            {
                return _ChampionPivotLoadedCommand
                    ?? (_ChampionPivotLoadedCommand = new RelayCommand<Pivot>(o =>
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

                        //记录当前选择的Pivot的Item的Index
                        DicStore.AddOrUpdateValue<int>(AppCommonConst.CUR_PIVOT_SELECTED_INDEX, o.SelectedIndex);

                        if (!SimpleIoc.Default.IsRegistered<VideoListViewModel>())
                        {
                            SimpleIoc.Default.Register<VideoListViewModel>(false);
                        }
                        var videoListViewModel = SimpleIoc.Default.GetInstance<VideoListViewModel>();

                        switch (o.SelectedIndex)
                        {
                            case 0:
                                if (videoListViewModel.VideoChampionCFPLCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoChampionCFPLCollection, ApiVideoRoot.Instance.VideoChampionCFPLUrl, AppPageIndexConst.VIDEO_CHAMPION_CFPL_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_CHAMPION_CFPL_FILENAME);
                                }
                                break;
                            case 1:
                                if (videoListViewModel.VideoChampionHundredsOfCityCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoChampionHundredsOfCityCollection, ApiVideoRoot.Instance.VideoChampionHundredsOfCityUrl, AppPageIndexConst.VIDEO_CHAMPION_HUNDREDSOFCITY_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_CHAMPION_HUNDREDSOFCITY_FILENAME);
                                }
                                break;
                            case 2:
                                if (videoListViewModel.VideoChampionAllPeopleCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoChampionAllPeopleCollection, ApiVideoRoot.Instance.VideoChampionAllPeopleUrl, AppPageIndexConst.VIDEO_CHAMPION_ALLPEOPLE_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_CHAMPION_ALLPEOPLE_FILENAME);
                                }
                                break;
                            case 3:
                                if (videoListViewModel.VideoChampionCFDLCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoChampionCFDLCollection, ApiVideoRoot.Instance.VideoChampionCFDLUrl, AppPageIndexConst.VIDEO_CHAMPION_CFDL_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_CHAMPION_CFDL_FILENAME);
                                }
                                break;
                            case 4:
                                if (videoListViewModel.VideoChampionCFSCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoChampionCFSCollection, ApiVideoRoot.Instance.VideoChampionCFSUrl, AppPageIndexConst.VIDEO_CHAMPION_CFS_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_CHAMPION_CFS_FILENAME);
                                }
                                break;
                            case 5:
                                if (videoListViewModel.VideoChampionOtherCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoChampionOtherCollection, ApiVideoRoot.Instance.VideoChampionOtherUrl, AppPageIndexConst.VIDEO_CHAMPION_OTHER_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_CHAMPION_OTHER_FILENAME);
                                }
                                break;
                        }
                    }, o => { return o == null ? false : true; }
                ));
            }
        }

        private RelayCommand _ChampionListViewLoadMoreCommand;
        public RelayCommand ChampionListViewLoadMoreCommand
        {
            get
            {
                return _ChampionListViewLoadMoreCommand
                    ?? (_ChampionListViewLoadMoreCommand = new RelayCommand(() =>
                    {
                        if (!SimpleIoc.Default.IsRegistered<VideoListViewModel>())
                        {
                            SimpleIoc.Default.Register<VideoListViewModel>(false);
                        }
                        var videoListViewModel = SimpleIoc.Default.GetInstance<VideoListViewModel>();

                        var pivotSelectedIndex = DicStore.GetValueOrDefault<int>(AppCommonConst.CUR_PIVOT_SELECTED_INDEX, -1);

                        if (pivotSelectedIndex == -1)
                        {
                            return;
                        }
                        else
                        {
                            switch (pivotSelectedIndex)
                            {
                                case 0:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoChampionCFPLCollection, ApiVideoRoot.Instance.VideoChampionCFPLUrl, AppPageIndexConst.VIDEO_CHAMPION_CFPL_PAGE_INDEX_CONST);
                                    break;
                                case 1:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoChampionHundredsOfCityCollection, ApiVideoRoot.Instance.VideoChampionHundredsOfCityUrl, AppPageIndexConst.VIDEO_CHAMPION_HUNDREDSOFCITY_PAGE_INDEX_CONST);
                                    break;
                                case 2:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoChampionAllPeopleCollection, ApiVideoRoot.Instance.VideoChampionAllPeopleUrl, AppPageIndexConst.VIDEO_CHAMPION_ALLPEOPLE_PAGE_INDEX_CONST);
                                    break;
                                case 3:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoChampionCFDLCollection, ApiVideoRoot.Instance.VideoChampionCFDLUrl, AppPageIndexConst.VIDEO_CHAMPION_CFDL_PAGE_INDEX_CONST);
                                    break;
                                case 4:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoChampionCFSCollection, ApiVideoRoot.Instance.VideoChampionCFSUrl, AppPageIndexConst.VIDEO_CHAMPION_CFS_PAGE_INDEX_CONST);
                                    break;
                                case 5:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoChampionOtherCollection, ApiVideoRoot.Instance.VideoChampionOtherUrl, AppPageIndexConst.VIDEO_CHAMPION_OTHER_PAGE_INDEX_CONST);
                                    break;
                            }
                        }
                    }, () =>
                    {
                        if (!SimpleIoc.Default.IsRegistered<VideoListViewModel>())
                        {
                            SimpleIoc.Default.Register<VideoListViewModel>(false);
                        }
                        var videoListViewModel = SimpleIoc.Default.GetInstance<VideoListViewModel>();

                        return videoListViewModel.IsBusy ? false : true;
                    }
                ));
            }
        }

        #endregion

        #region 节目

        private RelayCommand<Pivot> _ActPivotLoadedCommand;
        public RelayCommand<Pivot> ActPivotLoadedCommand
        {
            get
            {
                return _ActPivotLoadedCommand
                    ?? (_ActPivotLoadedCommand = new RelayCommand<Pivot>(o =>
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

                        //记录当前选择的Pivot的Item的Index
                        DicStore.AddOrUpdateValue<int>(AppCommonConst.CUR_PIVOT_SELECTED_INDEX, o.SelectedIndex);

                        if (!SimpleIoc.Default.IsRegistered<VideoListViewModel>())
                        {
                            SimpleIoc.Default.Register<VideoListViewModel>(false);
                        }
                        var videoListViewModel = SimpleIoc.Default.GetInstance<VideoListViewModel>();

                        switch (o.SelectedIndex)
                        {
                            case 0:
                                if (videoListViewModel.VideoActSuperMethodCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoActSuperMethodCollection, ApiVideoRoot.Instance.VideoActSuperMehtodUrl, AppPageIndexConst.VIDEO_ACT_SUPERMETHOD_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_ACT_SUPERMETHOD_FILENAME);
                                }
                                break;
                            case 1:
                                if (videoListViewModel.VideoActSuperRsenalCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoActSuperRsenalCollection, ApiVideoRoot.Instance.VideoActSuperRsenalUrl, AppPageIndexConst.VIDEO_ACT_SUPERARSENAL_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_ACT_SUPERARSENAL_FILENAME);
                                }
                                break;
                            case 2:
                                if (videoListViewModel.VideoActSuperTimeCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoActSuperTimeCollection, ApiVideoRoot.Instance.VideoActSuperTimeUrl, AppPageIndexConst.VIDEO_ACT_SUPERTIME_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_ACT_SUPERTIME_FILENAME);
                                }
                                break;
                            case 3:
                                if (videoListViewModel.VideoActFireCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoActFireCollection, ApiVideoRoot.Instance.VideoActFireUrl, AppPageIndexConst.VIDEO_ACT_FIRE_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_ACT_FIRE_FILENAME);
                                }
                                break;
                            case 4:
                                if (videoListViewModel.VideoActOtherCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoActOtherCollection, ApiVideoRoot.Instance.VideoActOtherUrl, AppPageIndexConst.VIDEO_ACT_OTHER_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_ACT_OTHER_FILENAME);
                                }
                                break;
                            case 5:
                                if (videoListViewModel.VideoActBrotherCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoActBrotherCollection, ApiVideoRoot.Instance.VideoActBrotherUrl, AppPageIndexConst.VIDEO_ACT_BROTHER_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_ACT_BROTHER_FILENAME);
                                }
                                break;
                        }
                    }, o => { return o == null ? false : true; }
                ));
            }
        }

        private RelayCommand _ActListViewLoadMoreCommand;
        public RelayCommand ActListViewLoadMoreCommand
        {
            get
            {
                return _ActListViewLoadMoreCommand
                    ?? (_ActListViewLoadMoreCommand = new RelayCommand(() =>
                    {
                        if (!SimpleIoc.Default.IsRegistered<VideoListViewModel>())
                        {
                            SimpleIoc.Default.Register<VideoListViewModel>(false);
                        }
                        var videoListViewModel = SimpleIoc.Default.GetInstance<VideoListViewModel>();

                        var pivotSelectedIndex = DicStore.GetValueOrDefault<int>(AppCommonConst.CUR_PIVOT_SELECTED_INDEX, -1);

                        if (pivotSelectedIndex == -1)
                        {
                            return;
                        }
                        else
                        {
                            switch (pivotSelectedIndex)
                            {
                                case 0:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoActSuperMethodCollection, ApiVideoRoot.Instance.VideoActSuperMehtodUrl, AppPageIndexConst.VIDEO_ACT_SUPERMETHOD_PAGE_INDEX_CONST);
                                    break;
                                case 1:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoActSuperRsenalCollection, ApiVideoRoot.Instance.VideoActSuperRsenalUrl, AppPageIndexConst.VIDEO_ACT_SUPERARSENAL_PAGE_INDEX_CONST);
                                    break;
                                case 2:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoActSuperTimeCollection, ApiVideoRoot.Instance.VideoActSuperTimeUrl, AppPageIndexConst.VIDEO_ACT_SUPERTIME_PAGE_INDEX_CONST);
                                    break;
                                case 3:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoActFireCollection, ApiVideoRoot.Instance.VideoActFireUrl, AppPageIndexConst.VIDEO_ACT_FIRE_PAGE_INDEX_CONST);
                                    break;
                                case 4:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoActOtherCollection, ApiVideoRoot.Instance.VideoActOtherUrl, AppPageIndexConst.VIDEO_ACT_OTHER_PAGE_INDEX_CONST);
                                    break;
                                case 5:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoActBrotherCollection, ApiVideoRoot.Instance.VideoActBrotherUrl, AppPageIndexConst.VIDEO_ACT_BROTHER_PAGE_INDEX_CONST);
                                    break;
                            }
                        }
                    }, () =>
                    {
                        if (!SimpleIoc.Default.IsRegistered<VideoListViewModel>())
                        {
                            SimpleIoc.Default.Register<VideoListViewModel>(false);
                        }
                        var videoListViewModel = SimpleIoc.Default.GetInstance<VideoListViewModel>();

                        return videoListViewModel.IsBusy ? false : true;
                    }
                ));
            }
        }

        #endregion

        #region 高手

        private RelayCommand<Pivot> _SuperPivotLoadedCommand;
        public RelayCommand<Pivot> SuperPivotLoadedCommand
        {
            get
            {
                return _SuperPivotLoadedCommand
                    ?? (_SuperPivotLoadedCommand = new RelayCommand<Pivot>(o =>
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

                        //记录当前选择的Pivot的Item的Index
                        DicStore.AddOrUpdateValue<int>(AppCommonConst.CUR_PIVOT_SELECTED_INDEX, o.SelectedIndex);

                        if (!SimpleIoc.Default.IsRegistered<VideoListViewModel>())
                        {
                            SimpleIoc.Default.Register<VideoListViewModel>(false);
                        }
                        var videoListViewModel = SimpleIoc.Default.GetInstance<VideoListViewModel>();

                        switch (o.SelectedIndex)
                        {
                            case 0:
                                if (videoListViewModel.VideoSuperTGASTARCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoSuperTGASTARCollection, ApiVideoRoot.Instance.VideoSuperTGASTARUrl, AppPageIndexConst.VIDEO_SUPER_TGASTAR_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_SUPER_TGASTAR_FILENAME);
                                }
                                break;
                            case 1:
                                if (videoListViewModel.VideoSuperProfressionalCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoSuperProfressionalCollection, ApiVideoRoot.Instance.VideoSuperProfressionalUrl, AppPageIndexConst.VIDEO_SUPER_PROFRESSIONAL_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_SUPER_PROFRESSIONAL_FILENAME);
                                }
                                break;
                            case 2:
                                if (videoListViewModel.VideoSuperPeopleCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoSuperPeopleCollection, ApiVideoRoot.Instance.VideoSuperPeopleUrl, AppPageIndexConst.VIDEO_SUPER_PEOPELE_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_SUPER_PEOPELE_FILENAME);
                                }
                                break;
                            case 3:
                                if (videoListViewModel.VideoSuperOtherCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoSuperOtherCollection, ApiVideoRoot.Instance.VideoSuperOtherUrl, AppPageIndexConst.VIDEO_SUPER_OTHER_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_SUPER_OTHER_FILENAME);
                                }
                                break;
                            case 4:
                                if (videoListViewModel.VideoSuperForeignerCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoSuperForeignerCollection, ApiVideoRoot.Instance.VideoSuperForeignerUrl, AppPageIndexConst.VIDEO_SUPER_FOREIGNER_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_SUPER_FOREIGNER_FILENAME);
                                }
                                break;
                        }
                    }, o => { return o == null ? false : true; }
                ));
            }
        }

        private RelayCommand _SuperListViewLoadMoreCommand;
        public RelayCommand SuperListViewLoadMoreCommand
        {
            get
            {
                return _SuperListViewLoadMoreCommand
                    ?? (_SuperListViewLoadMoreCommand = new RelayCommand(() =>
                    {
                        if (!SimpleIoc.Default.IsRegistered<VideoListViewModel>())
                        {
                            SimpleIoc.Default.Register<VideoListViewModel>(false);
                        }
                        var videoListViewModel = SimpleIoc.Default.GetInstance<VideoListViewModel>();

                        var pivotSelectedIndex = DicStore.GetValueOrDefault<int>(AppCommonConst.CUR_PIVOT_SELECTED_INDEX, -1);

                        if (pivotSelectedIndex == -1)
                        {
                            return;
                        }
                        else
                        {
                            switch (pivotSelectedIndex)
                            {
                                case 0:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoSuperTGASTARCollection, ApiVideoRoot.Instance.VideoSuperTGASTARUrl, AppPageIndexConst.VIDEO_SUPER_TGASTAR_PAGE_INDEX_CONST);
                                    break;
                                case 1:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoSuperProfressionalCollection, ApiVideoRoot.Instance.VideoSuperProfressionalUrl, AppPageIndexConst.VIDEO_SUPER_PROFRESSIONAL_PAGE_INDEX_CONST);
                                    break;
                                case 2:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoSuperPeopleCollection, ApiVideoRoot.Instance.VideoSuperPeopleUrl, AppPageIndexConst.VIDEO_SUPER_PEOPELE_PAGE_INDEX_CONST);
                                    break;
                                case 3:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoSuperOtherCollection, ApiVideoRoot.Instance.VideoSuperOtherUrl, AppPageIndexConst.VIDEO_SUPER_OTHER_PAGE_INDEX_CONST);
                                    break;
                                case 4:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoSuperForeignerCollection, ApiVideoRoot.Instance.VideoSuperForeignerUrl, AppPageIndexConst.VIDEO_SUPER_FOREIGNER_PAGE_INDEX_CONST);
                                    break;
                            }
                        }
                    }, () =>
                    {
                        if (!SimpleIoc.Default.IsRegistered<VideoListViewModel>())
                        {
                            SimpleIoc.Default.Register<VideoListViewModel>(false);
                        }
                        var videoListViewModel = SimpleIoc.Default.GetInstance<VideoListViewModel>();

                        return videoListViewModel.IsBusy ? false : true;
                    }
                ));
            }
        }

        #endregion

        #region 教学

        private RelayCommand<Pivot> _TeachPivotLoadedCommand;
        public RelayCommand<Pivot> TeachPivotLoadedCommand
        {
            get
            {
                return _TeachPivotLoadedCommand
                    ?? (_TeachPivotLoadedCommand = new RelayCommand<Pivot>(o =>
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

                        //记录当前选择的Pivot的Item的Index
                        DicStore.AddOrUpdateValue<int>(AppCommonConst.CUR_PIVOT_SELECTED_INDEX, o.SelectedIndex);

                        if (!SimpleIoc.Default.IsRegistered<VideoListViewModel>())
                        {
                            SimpleIoc.Default.Register<VideoListViewModel>(false);
                        }
                        var videoListViewModel = SimpleIoc.Default.GetInstance<VideoListViewModel>();

                        switch (o.SelectedIndex)
                        {
                            case 0:
                                if (videoListViewModel.VideoTeachGunCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoTeachGunCollection, ApiVideoRoot.Instance.VideoTeachGunUrl, AppPageIndexConst.VIDEO_TEACH_GUN_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_TEACH_GUN_FILENAME);
                                }
                                break;
                            case 1:
                                if (videoListViewModel.VideoTeachMapCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoTeachMapCollection, ApiVideoRoot.Instance.VideoTeachMapUrl, AppPageIndexConst.VIDEO_TEACH_MAP_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_TEACH_MAP_FILENAME);
                                }
                                break;
                            case 2:
                                if (videoListViewModel.VideoTeachSkillCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoTeachSkillCollection, ApiVideoRoot.Instance.VideoTeachSkillUrl, AppPageIndexConst.VIDEO_TEACH_SKILL_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_TEACH_SKILL_FILENAME);
                                }
                                break;
                            case 3:
                                if (videoListViewModel.VideoTeachOtherCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoTeachOtherCollection, ApiVideoRoot.Instance.VideoTeachOtherUrl, AppPageIndexConst.VIDEO_TEACH_OTHER_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_TEACH_OTHER_FILENAME);
                                }
                                break;
                        }
                    }, o => { return o == null ? false : true; }
                ));
            }
        }

        private RelayCommand _TeachListViewLoadMoreCommand;
        public RelayCommand TeachListViewLoadMoreCommand
        {
            get
            {
                return _TeachListViewLoadMoreCommand
                    ?? (_TeachListViewLoadMoreCommand = new RelayCommand(() =>
                    {
                        if (!SimpleIoc.Default.IsRegistered<VideoListViewModel>())
                        {
                            SimpleIoc.Default.Register<VideoListViewModel>(false);
                        }
                        var videoListViewModel = SimpleIoc.Default.GetInstance<VideoListViewModel>();

                        var pivotSelectedIndex = DicStore.GetValueOrDefault<int>(AppCommonConst.CUR_PIVOT_SELECTED_INDEX, -1);

                        if (pivotSelectedIndex == -1)
                        {
                            return;
                        }
                        else
                        {
                            switch (pivotSelectedIndex)
                            {
                                case 0:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoTeachGunCollection, ApiVideoRoot.Instance.VideoTeachGunUrl, AppPageIndexConst.VIDEO_TEACH_GUN_PAGE_INDEX_CONST);
                                    break;
                                case 1:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoTeachMapCollection, ApiVideoRoot.Instance.VideoTeachMapUrl, AppPageIndexConst.VIDEO_TEACH_MAP_PAGE_INDEX_CONST);
                                    break;
                                case 2:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoTeachSkillCollection, ApiVideoRoot.Instance.VideoTeachSkillUrl, AppPageIndexConst.VIDEO_TEACH_SKILL_PAGE_INDEX_CONST);
                                    break;
                                case 3:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoTeachOtherCollection, ApiVideoRoot.Instance.VideoTeachOtherUrl, AppPageIndexConst.VIDEO_TEACH_OTHER_PAGE_INDEX_CONST);
                                    break;
                            }
                        }
                    }, () =>
                    {
                        if (!SimpleIoc.Default.IsRegistered<VideoListViewModel>())
                        {
                            SimpleIoc.Default.Register<VideoListViewModel>(false);
                        }
                        var videoListViewModel = SimpleIoc.Default.GetInstance<VideoListViewModel>();

                        return videoListViewModel.IsBusy ? false : true;
                    }
                ));
            }
        }

        #endregion

        #region 官方

        private RelayCommand<Pivot> _OfficialPivotLoadedCommand;
        public RelayCommand<Pivot> OfficialPivotLoadedCommand
        {
            get
            {
                return _OfficialPivotLoadedCommand
                    ?? (_OfficialPivotLoadedCommand = new RelayCommand<Pivot>(o =>
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

                        //记录当前选择的Pivot的Item的Index
                        DicStore.AddOrUpdateValue<int>(AppCommonConst.CUR_PIVOT_SELECTED_INDEX, o.SelectedIndex);

                        if (!SimpleIoc.Default.IsRegistered<VideoListViewModel>())
                        {
                            SimpleIoc.Default.Register<VideoListViewModel>(false);
                        }
                        var videoListViewModel = SimpleIoc.Default.GetInstance<VideoListViewModel>();

                        switch (o.SelectedIndex)
                        {
                            case 0:
                                if (videoListViewModel.VideoOfficialVCRCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoOfficialVCRCollection, ApiVideoRoot.Instance.VideoOfficialVcrUrl, AppPageIndexConst.VIDEO_OFFICIAL_VCR_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_OFFICIAL_VCR_FILENAME);
                                }
                                break;
                            case 1:
                                if (videoListViewModel.VideoOfficialGAMECGCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoOfficialGAMECGCollection, ApiVideoRoot.Instance.VideoOfficialGameCGUrl, AppPageIndexConst.VIDEO_OFFICIAL_GAMECG_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_OFFICIAL_GAMECG_FILENAME);
                                }
                                break;
                            case 2:
                                if (videoListViewModel.VideoOfficialVersionCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoOfficialVersionCollection, ApiVideoRoot.Instance.VideoOfficialVersionUrl, AppPageIndexConst.VIDEO_OFFICIAL_VERSION_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_OFFICIAL_VERSION_FILENAME);
                                }
                                break;
                            case 3:
                                if (videoListViewModel.VideoOfficialOtherCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoOfficialOtherCollection, ApiVideoRoot.Instance.VideoOfficialOtherUrl, AppPageIndexConst.VIDEO_OFFICIAL_OTHER_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_OFFICIAL_OTHER_FILENAME);
                                }
                                break;
                        }
                    }, o => { return o == null ? false : true; }
                ));
            }
        }

        private RelayCommand _OfficialListViewLoadMoreCommand;
        public RelayCommand OfficialListViewLoadMoreCommand
        {
            get
            {
                return _OfficialListViewLoadMoreCommand
                    ?? (_OfficialListViewLoadMoreCommand = new RelayCommand(() =>
                    {
                        if (!SimpleIoc.Default.IsRegistered<VideoListViewModel>())
                        {
                            SimpleIoc.Default.Register<VideoListViewModel>(false);
                        }
                        var videoListViewModel = SimpleIoc.Default.GetInstance<VideoListViewModel>();

                        var pivotSelectedIndex = DicStore.GetValueOrDefault<int>(AppCommonConst.CUR_PIVOT_SELECTED_INDEX, -1);

                        if (pivotSelectedIndex == -1)
                        {
                            return;
                        }
                        else
                        {
                            switch (pivotSelectedIndex)
                            {
                                case 0:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoOfficialVCRCollection, ApiVideoRoot.Instance.VideoOfficialVcrUrl, AppPageIndexConst.VIDEO_OFFICIAL_VCR_PAGE_INDEX_CONST);
                                    break;
                                case 1:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoOfficialGAMECGCollection, ApiVideoRoot.Instance.VideoOfficialGameCGUrl, AppPageIndexConst.VIDEO_OFFICIAL_GAMECG_PAGE_INDEX_CONST);
                                    break;
                                case 2:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoOfficialVersionCollection, ApiVideoRoot.Instance.VideoOfficialVersionUrl, AppPageIndexConst.VIDEO_OFFICIAL_VERSION_PAGE_INDEX_CONST);
                                    break;
                                case 3:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoOfficialOtherCollection, ApiVideoRoot.Instance.VideoOfficialOtherUrl, AppPageIndexConst.VIDEO_OFFICIAL_OTHER_PAGE_INDEX_CONST);
                                    break;
                            }
                        }
                    }, () =>
                    {
                        if (!SimpleIoc.Default.IsRegistered<VideoListViewModel>())
                        {
                            SimpleIoc.Default.Register<VideoListViewModel>(false);
                        }
                        var videoListViewModel = SimpleIoc.Default.GetInstance<VideoListViewModel>();

                        return videoListViewModel.IsBusy ? false : true;
                    }
                ));
            }
        }

        #endregion

        #region 官方

        private RelayCommand<Pivot> _PlayPivotLoadedCommand;
        public RelayCommand<Pivot> PlayPivotLoadedCommand
        {
            get
            {
                return _PlayPivotLoadedCommand
                    ?? (_PlayPivotLoadedCommand = new RelayCommand<Pivot>(o =>
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

                        //记录当前选择的Pivot的Item的Index
                        DicStore.AddOrUpdateValue<int>(AppCommonConst.CUR_PIVOT_SELECTED_INDEX, o.SelectedIndex);

                        if (!SimpleIoc.Default.IsRegistered<VideoListViewModel>())
                        {
                            SimpleIoc.Default.Register<VideoListViewModel>(false);
                        }
                        var videoListViewModel = SimpleIoc.Default.GetInstance<VideoListViewModel>();

                        switch (o.SelectedIndex)
                        {
                            case 0:
                                if (videoListViewModel.VideoPlayFunCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoPlayFunCollection, ApiVideoRoot.Instance.VideoPlayFunUrl, AppPageIndexConst.VIDEO_PLAY_FUN_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_PLAY_FUN_FILENAME);
                                }
                                break;
                            case 1:
                                if (videoListViewModel.VideoPlayMovieCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoPlayMovieCollection, ApiVideoRoot.Instance.VideoPlayMovieUrl, AppPageIndexConst.VIDEO_PLAY_MOVIE_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_PLAY_MOVIE_FILENAME);
                                }
                                break;
                            case 2:
                                if (videoListViewModel.VideoPlaySongCollection.Count == 0)
                                {
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoPlaySongCollection, ApiVideoRoot.Instance.VideoPlaySongUrl, AppPageIndexConst.VIDEO_PLAY_SONG_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_VIDEO_PLAY_SONG_FILENAME);
                                }
                                break;
                        }
                    }, o => { return o == null ? false : true; }
                ));
            }
        }

        private RelayCommand _PlayListViewLoadMoreCommand;
        public RelayCommand PlayListViewLoadMoreCommand
        {
            get
            {
                return _PlayListViewLoadMoreCommand
                    ?? (_PlayListViewLoadMoreCommand = new RelayCommand(() =>
                    {
                        if (!SimpleIoc.Default.IsRegistered<VideoListViewModel>())
                        {
                            SimpleIoc.Default.Register<VideoListViewModel>(false);
                        }
                        var videoListViewModel = SimpleIoc.Default.GetInstance<VideoListViewModel>();

                        var pivotSelectedIndex = DicStore.GetValueOrDefault<int>(AppCommonConst.CUR_PIVOT_SELECTED_INDEX, -1);

                        if (pivotSelectedIndex == -1)
                        {
                            return;
                        }
                        else
                        {
                            switch (pivotSelectedIndex)
                            {
                                case 0:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoPlayFunCollection, ApiVideoRoot.Instance.VideoPlayFunUrl, AppPageIndexConst.VIDEO_PLAY_FUN_PAGE_INDEX_CONST);
                                    break;
                                case 1:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoPlayMovieCollection, ApiVideoRoot.Instance.VideoPlayMovieUrl, AppPageIndexConst.VIDEO_PLAY_MOVIE_PAGE_INDEX_CONST);
                                    break;
                                case 2:
                                    videoListViewModel.GetVideoList(videoListViewModel.VideoPlaySongCollection, ApiVideoRoot.Instance.VideoPlaySongUrl, AppPageIndexConst.VIDEO_PLAY_SONG_PAGE_INDEX_CONST);
                                    break;
                            }
                        }
                    }, () =>
                    {
                        if (!SimpleIoc.Default.IsRegistered<VideoListViewModel>())
                        {
                            SimpleIoc.Default.Register<VideoListViewModel>(false);
                        }
                        var videoListViewModel = SimpleIoc.Default.GetInstance<VideoListViewModel>();

                        return videoListViewModel.IsBusy ? false : true;
                    }
                ));
            }
        }

        #endregion
        #endregion
    }
}
