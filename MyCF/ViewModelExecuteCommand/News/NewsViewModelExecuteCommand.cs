//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.ViewModelExecuteCommand.News
//类名称:       NewsViewModelExecuteCommand
//创建时间:     2015/8/10 星期一 16:29:02
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using MyCF.Api.ApiRoot;
using MyCF.Const;
using MyCF.Helper;
using MyCF.UIControl.UserControls.RetryUICtrl;
using MyCF.ViewModel.News;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml.Controls;

namespace MyCF.ViewModelExecuteCommand.News
{
    public class NewsViewModelExecuteCommand
    {
        /// <summary>
        /// 首页加载Pivot的Loaded事件处理
        /// </summary>
        private RelayCommand<Pivot> _PivotLoadedCommand;
        public RelayCommand<Pivot> PivotLoadedCommand
        {
            get
            {
                return _PivotLoadedCommand
                    ?? (_PivotLoadedCommand = new RelayCommand<Pivot>( o =>
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

                        if (!SimpleIoc.Default.IsRegistered<NewsViewModel>())
                        {
                            SimpleIoc.Default.Register<NewsViewModel>(false);
                        }
                        var newsViewModel = SimpleIoc.Default.GetInstance<NewsViewModel>();

                        switch (o.SelectedIndex)
                        {
                            case 0:
                                if (newsViewModel.FirstNewsCollection.Count == 0)
                                {
                                    newsViewModel.GetCommonNews(newsViewModel.FirstNewsCollection, ApiNewsRoot.Instance.FirstNewsUrl, AppPageIndexConst.NEWS_FIRSTNEWS_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_NEWS_FIRSTNEWS_FILENAME, newsViewModel.FirstNewsAdCollection);
                                }
                                break;
                            case 1:
                                if (newsViewModel.ChampionCollection.Count == 0)
                                {
                                    newsViewModel.GetCommonNews(newsViewModel.ChampionCollection, ApiNewsRoot.Instance.ChampionUrl, AppPageIndexConst.NEWS_CHAMPION_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_NEWS_CHAMPION_FILENAME);
                                }
                                break;
                            //活动
                            case 2:
                                if (newsViewModel.ActivityCollection.Count == 0)
                                {
                                    newsViewModel.GetActivityNews(newsViewModel.ActivityCollection, ApiNewsRoot.Instance.ActivityUrl, AppPageIndexConst.NEWS_ACTIVITY_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_NEWS_ACTIVITY_FILENAME);
                                }
                                break;
                            //case 2:
                            //    if (newsViewModel.CartoonCollection.Count == 0)
                            //    {
                            //        newsViewModel.GetCommonNews(newsViewModel.CartoonCollection, ApiNewsRoot.Instance.CartoonUrl, AppPageIndexConst.NEWS_CARTOON_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_NEWS_CARTOON_FILENAME);
                            //    }
                            //    break;
                            case 3:
                                if (newsViewModel.StrategyCollection.Count == 0)
                                {
                                    newsViewModel.GetCommonNews(newsViewModel.StrategyCollection, ApiNewsRoot.Instance.StrategyUrl, AppPageIndexConst.NEWS_STRATEGY_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_NEWS_STRATEGY_FILENAME);
                                }
                                break;
                        }
                    }, o => { return o == null ? false : true; }
                ));
            }
        }

        /// <summary>
        /// ListView下拉刷新的处理
        /// </summary>
        private RelayCommand<Pivot> _ListViewRefreshCommand;
        public RelayCommand<Pivot> ListViewRefreshCommand
        {
            get
            {
                return _ListViewRefreshCommand
                    ?? (_ListViewRefreshCommand = new RelayCommand<Pivot>(o =>
                    {
                        //隐藏加载错误提示
                        RetryBox.Instance.HideRetry();

                        if (!SimpleIoc.Default.IsRegistered<NewsViewModel>())
                        {
                            SimpleIoc.Default.Register<NewsViewModel>(false);
                        }
                        var newsViewModel = SimpleIoc.Default.GetInstance<NewsViewModel>();

                        switch (o.SelectedIndex)
                        {
                            case 0:
                                DicStore.AddOrUpdateValue<int>(AppPageIndexConst.NEWS_FIRSTNEWS_PAGE_INDEX_CONST, 1);
                                newsViewModel.GetCommonNews(newsViewModel.FirstNewsCollection, ApiNewsRoot.Instance.FirstNewsUrl, AppPageIndexConst.NEWS_FIRSTNEWS_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_NEWS_FIRSTNEWS_FILENAME, newsViewModel.FirstNewsAdCollection, true);
                                break;
                            case 1:
                                DicStore.AddOrUpdateValue<int>(AppPageIndexConst.NEWS_CHAMPION_PAGE_INDEX_CONST, 1);
                                newsViewModel.GetCommonNews(newsViewModel.ChampionCollection, ApiNewsRoot.Instance.ChampionUrl, AppPageIndexConst.NEWS_CHAMPION_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_NEWS_CHAMPION_FILENAME, null, true);
                                break;
                            //活动
                            case 2:
                                DicStore.AddOrUpdateValue<int>(AppPageIndexConst.NEWS_ACTIVITY_PAGE_INDEX_CONST, 1);
                                newsViewModel.GetActivityNews(newsViewModel.ActivityCollection, ApiNewsRoot.Instance.ActivityUrl, AppPageIndexConst.NEWS_ACTIVITY_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_NEWS_ACTIVITY_FILENAME, null, true);
                                break;
                            //case 2:
                            //    if (newsViewModel.CartoonCollection.Count == 0)
                            //    {
                            //        newsViewModel.GetCommonNews(newsViewModel.CartoonCollection, ApiNewsRoot.Instance.CartoonUrl, AppPageIndexConst.NEWS_CARTOON_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_NEWS_CARTOON_FILENAME);
                            //    }
                            //    break;
                            case 3:
                                DicStore.AddOrUpdateValue<int>(AppPageIndexConst.NEWS_STRATEGY_PAGE_INDEX_CONST, 1);
                                newsViewModel.GetCommonNews(newsViewModel.StrategyCollection, ApiNewsRoot.Instance.StrategyUrl, AppPageIndexConst.NEWS_STRATEGY_PAGE_INDEX_CONST, AppCacheNewsFileNameConst.CACHE_NEWS_STRATEGY_FILENAME, null, true);
                                break;
                        }
                    }, o => 
                    {
                        if (!SimpleIoc.Default.IsRegistered<NewsViewModel>())
                        {
                            SimpleIoc.Default.Register<NewsViewModel>(false);
                        }
                        var newsViewModel = SimpleIoc.Default.GetInstance<NewsViewModel>();

                        if (!newsViewModel.IsBusy && o != null)
                        {
                            
                            return true;
                        }
                        else
                        {
                            if (newsViewModel.IsBusy)
                            {
                                Debug.WriteLine("正在忙取消此次刷新");
                            }

                            if (o == null)
                            {
                                Debug.WriteLine("收到参数o为null，取消此次刷新");
                            }

                            return false;
                        }

                        
                    }
                ));
            }
        }

        /// <summary>
        /// ListView加载更多
        /// </summary>
        private RelayCommand _ListViewLoadMoreCommand;
        public RelayCommand ListViewLoadMoreCommand
        {
            get
            {
                return _ListViewLoadMoreCommand
                    ?? (_ListViewLoadMoreCommand = new RelayCommand(() =>
                    {
                        if (!SimpleIoc.Default.IsRegistered<NewsViewModel>())
                        {
                            SimpleIoc.Default.Register<NewsViewModel>(false);
                        }
                        var newsViewModel = SimpleIoc.Default.GetInstance<NewsViewModel>();

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
                                    newsViewModel.GetCommonNews(newsViewModel.FirstNewsCollection, ApiNewsRoot.Instance.FirstNewsUrl, AppPageIndexConst.NEWS_FIRSTNEWS_PAGE_INDEX_CONST);
                                    break;
                                case 1:
                                    newsViewModel.GetCommonNews(newsViewModel.ChampionCollection, ApiNewsRoot.Instance.ChampionUrl, AppPageIndexConst.NEWS_CHAMPION_PAGE_INDEX_CONST);
                                    break;
                                case 2:
                                    newsViewModel.GetActivityNews(newsViewModel.ActivityCollection, ApiNewsRoot.Instance.ActivityUrl, AppPageIndexConst.NEWS_ACTIVITY_PAGE_INDEX_CONST);
                                    break;
                                //case 2:
                                //    newsViewModel.GetCommonNews(newsViewModel.CartoonCollection, ApiNewsRoot.Instance.CartoonUrl, AppPageIndexConst.NEWS_CARTOON_PAGE_INDEX_CONST);
                                //    break;
                                case 3:
                                    newsViewModel.GetCommonNews(newsViewModel.StrategyCollection, ApiNewsRoot.Instance.StrategyUrl, AppPageIndexConst.NEWS_STRATEGY_PAGE_INDEX_CONST);
                                    break;
                            }
                        }
                    }, () =>
                    {
                        if (!SimpleIoc.Default.IsRegistered<NewsViewModel>())
                        {
                            SimpleIoc.Default.Register<NewsViewModel>(false);
                        }
                        var newsViewModel = SimpleIoc.Default.GetInstance<NewsViewModel>();

                        return newsViewModel.IsBusy ? false : true;
                    }
                ));
            }
        }
    }
}
