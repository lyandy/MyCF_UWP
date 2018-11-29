//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.ViewModel.News
//类名称:       NewsViewModel
//创建时间:     2015/8/10 星期一 11:50:32
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Api.ApiRoot;
using MyCF.Config;
using MyCF.Const;
using MyCF.Helper;
using MyCF.Model.News;
using MyCF.UIControl.UserControls.ProgressUICtrl;
using MyCF.UIControl.UserControls.RetryUICtrl;
using MyCF.ViewModelAttribute.News;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.ViewModel.News
{
    public class NewsViewModel : NewsViewModelAttribute
    {
        public async void GetCommonNews(ObservableCollection<NewsModel> collection, string newsUrl, string pageIndexConst, string cacheFileName = null, ObservableCollection<NewsModel> adCollection = null ,bool isRefresh = false)
        {
            //取出当前的分页索引记录
            int currentPageIndex = DicStore.GetValueOrDefault<int>(pageIndexConst, 1);
            Debug.WriteLine(currentPageIndex);
            if (currentPageIndex <= AppCommonConst.PAGE_LOAD_DATA_MAX_INDEX)
            {
                IsBusy = true;

                var backJson = await WebDataHelper.Instance.GetFromUrlWithAuthReturnString(newsUrl, null, 3);
                if (backJson != null)
                {
                    var result = JsonConvertHelper.Instance.DeserializeObject<NewsCommonModel>(backJson);
                    if (result != null)
                    {
                        if (result.news.Count != 0)
                        {
                            if (isRefresh)
                            {
                                collection.Clear();
                                if (adCollection != null)
                                {
                                    adCollection.Clear();
                                }

                                var pivotSelectedIndex = DicStore.GetValueOrDefault<int>(AppCommonConst.CUR_PIVOT_SELECTED_INDEX, 0);
                                switch(pivotSelectedIndex)
                                {
                                    case 0:
                                        FirstNewsCollection = new ObservableCollection<NewsModel>(result.news);
                                        FirstNewsAdCollection = new ObservableCollection<NewsModel>(result.ads);
                                        break;
                                    case 1:
                                        ChampionCollection = new ObservableCollection<NewsModel>(result.news);
                                        break;
                                    case 3:
                                        StrategyCollection = new ObservableCollection<NewsModel>(result.news);
                                        break;
                                }

                                //重要：一个集合绑定到xaml界面上不显示数据有两种情况：1、原有集合地址被再次更改 2、拿着集合的别名到处跑。下面这种写法就是拿着集合的别名到处跑的情况，原有的集合无法被更改。如果要正确处理那就是直接拿着集合去更改。那如果就要拿着集合别名到处跑也要有数据显示的话，就得一个一个去add
                            }
                            else
                            {

                                result.news.ForEach(A =>
                                {
                                    collection.Add(A);
                                });

                                //只有adCollection有数据并且是手机才会展示顶部banner幻灯
                                if (adCollection != null)
                                {
                                    result.ads.ForEach(B =>
                                    {
                                        adCollection.Add(B);
                                    });
                                }
                            }

                            //及时更改类别的分页索引记录，如果没有加载成功就不会被自增记录
                            DicStore.AddOrUpdateValue<int>(pageIndexConst, ++currentPageIndex);

                            //只存第一页的数据，只有加载第一页数据才会记录当前的刷新时间
                            if (cacheFileName != null)
                            {
                                //记录当前的刷新时间 - 已取消
                                //SettingsStore.AddOrUpdateValue<string>(AppCommonConst.LAST_UPDATE_TIME, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                //将第一页数据缓存到本地
                                FileHelper.Instance.SaveTextToFile(CacheConfig.Instance.NewsListFileCacheRelativePath, cacheFileName, backJson);
                            }
                        }
                        else
                        {
                            //判断是不是分页索引第一页，如果是第一页并且获取到的条目个数为0个，则此时要求再次获取。此时提示“没有获取到数据，请重试”
                            if (currentPageIndex == 1 && !isRefresh)
                            {
                                //这里使用反射
                                RetryBox.Instance.ShowRetry(AppNetworkMessageConst.COLLECTION_ITEM_IS_ZERO, typeof(MyCF.ViewModel.News.NewsViewModel), "GetCommonNews", new object[] { collection, newsUrl, pageIndexConst, cacheFileName, adCollection, isRefresh });
                            }
                            //如果不是第一页，就不用去管
                            else { }
                        }
                    }
                    else
                    {
                        //判断是不是分页索引第一页，且不是刷新，说明是第一次启动程序加载失败了,如果是第一页且加载失败了，则会加载本地的数据。以保证有数据显示
                        if (currentPageIndex == 1)
                        {
                            //加载本地数据
                            var localJson = await FileHelper.Instance.ReadTextFromFile(CacheConfig.Instance.NewsListFileCacheRelativePath, cacheFileName);
                            if (localJson != null)
                            {
                                var localResult = JsonConvertHelper.Instance.DeserializeObject<NewsCommonModel>(localJson);
                                if (localResult != null)
                                {
                                    //因为如果第一页获取到的数据为0条时不会写到本地的，所以，从本地获取到的数据条目数一定不为0
                                    if (localResult.news.Count != 0)
                                    {
                                        if (!isRefresh)
                                        {
                                            localResult.news.ForEach(A =>
                                            {
                                                collection.Add(A);
                                            });

                                            //只有adCollection有数据并且是手机才会展示顶部banner幻灯
                                            if (adCollection != null)
                                            {
                                                localResult.ads.ForEach(B =>
                                                {
                                                    adCollection.Add(B);
                                                });
                                            }
                                        }

                                        //及时更改类别的分页索引记录，如果没有加载成功就不会被自增记录。加载成功则为第二页做准备
                                        DicStore.AddOrUpdateValue<int>(pageIndexConst, ++currentPageIndex);
                                    }
                                }
                                //虽然错误的数据是不会写到本地的，但如果反序列化失败一样会出错
                                else
                                {
                                    //如果此时还有网络，说明加载过程出错，提示信息为“加载数据出错，请重试。”
                                    if (!isRefresh)
                                    {
                                        if (AppEnvironment.IsInternetAccess)
                                        {
                                            RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWOTK_IS_ERROR, typeof(MyCF.ViewModel.News.NewsViewModel), "GetCommonNews", new object[] { collection, newsUrl, pageIndexConst, cacheFileName, adCollection, isRefresh });
                                        }
                                        //如果没有网络，说明数据加载失败是因为没有网络造成的。提示信息为“没有网络，请确认网络连接。”
                                        else
                                        {
                                            RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_ERROR, typeof(MyCF.ViewModel.News.NewsViewModel), "GetCommonNews", new object[] { collection, newsUrl, pageIndexConst, cacheFileName, adCollection, isRefresh });
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //如果此时还有网络，说明加载过程出错，提示信息为“加载数据出错，请重试。”
                                if (!isRefresh)
                                {
                                    if (AppEnvironment.IsInternetAccess)
                                    {
                                        RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWOTK_IS_ERROR, typeof(MyCF.ViewModel.News.NewsViewModel), "GetCommonNews", new object[] { collection, newsUrl, pageIndexConst, cacheFileName, adCollection, isRefresh });
                                    }
                                    //如果没有网络，说明数据加载失败是因为没有网络造成的。提示信息为“没有网络，请确认网络连接。”
                                    else
                                    {
                                        RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_NULL, typeof(MyCF.ViewModel.News.NewsViewModel), "GetCommonNews", new object[] { collection, newsUrl, pageIndexConst, cacheFileName, adCollection, isRefresh });
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    //判断是不是分页索引第一页，如果是第一页的话则会弹出提示，重新加载。因为第一页没有加载成功，本条目下就没数据，此时就要弹出一个东西让其重新加载数据。如果不是第一页就不用去管了
                    if (currentPageIndex == 1)
                    {
                        //如果此时还有网络，说明加载过程出错，提示信息为“加载数据出错，请重试。”
                        if (AppEnvironment.IsInternetAccess && !isRefresh)
                        {
                            RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWOTK_IS_ERROR, typeof(MyCF.ViewModel.News.NewsViewModel), "GetCommonNews", new object[] { collection, newsUrl, pageIndexConst, cacheFileName, adCollection, isRefresh });
                        }
                        //如果没有网络，说明数据加载失败是因为没有网络造成的。提示信息为“没有网络，请确认网络连接。”
                        else
                        {
                            //加载本地数据
                            var localJson = await FileHelper.Instance.ReadTextFromFile(CacheConfig.Instance.NewsListFileCacheRelativePath, cacheFileName);
                            if (localJson != null)
                            {
                                var localResult = JsonConvertHelper.Instance.DeserializeObject<NewsCommonModel>(localJson);
                                if (localResult != null)
                                {
                                    //因为如果第一页获取到的数据为0条时不会写到本地的，所以，从本地获取到的数据条目数一定不为0
                                    if (localResult.news.Count != 0)
                                    {
                                        if (!isRefresh)
                                        {
                                            localResult.news.ForEach(A =>
                                            {
                                                collection.Add(A);
                                            });

                                            //只有adCollection有数据并且是手机才会展示顶部banner幻灯
                                            if (adCollection != null)
                                            {
                                                localResult.ads.ForEach(B =>
                                                {
                                                    adCollection.Add(B);
                                                });
                                            }
                                        }

                                        //及时更改类别的分页索引记录，如果没有加载成功就不会被自增记录。加载成功则为第二页做准备
                                        DicStore.AddOrUpdateValue<int>(pageIndexConst, ++currentPageIndex);
                                    }
                                    else
                                    {
                                        if (!isRefresh)
                                        {
                                            RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_NULL, typeof(MyCF.ViewModel.News.NewsViewModel), "GetCommonNews", new object[] { collection, newsUrl, pageIndexConst, cacheFileName, adCollection, isRefresh });
                                        }
                                    }
                                }
                                //虽然错误的数据是不会写到本地的，但如果反序列化失败一样会出错，又没有网络
                                else
                                {
                                    if (!isRefresh)
                                    {
                                        RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_ERROR, typeof(MyCF.ViewModel.News.NewsViewModel), "GetCommonNews", new object[] { collection, newsUrl, pageIndexConst, cacheFileName, adCollection, isRefresh });
                                    }
                                }
                            }
                            else
                            {
                                if (!isRefresh)
                                {
                                    RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_NULL, typeof(MyCF.ViewModel.News.NewsViewModel), "GetCommonNews", new object[] { collection, newsUrl, pageIndexConst, cacheFileName, adCollection, isRefresh });
                                }
                            }
                        }
                    }
                    //如果不是第一页就不用管了
                    else { }
                }

                IsBusy = false;
            }
        }

        public async void GetActivityNews(ObservableCollection<ActivityModel> collection, string newsUrl, string pageIndexConst, string cacheFileName = null, ObservableCollection<NewsModel> adCollection = null, bool isRefresh = false)
        {
            //取出当前的分页索引记录
            int currentPageIndex = DicStore.GetValueOrDefault<int>(pageIndexConst, 1);
            Debug.WriteLine(currentPageIndex);
            if (currentPageIndex <= AppCommonConst.PAGE_LOAD_DATA_MAX_INDEX)
            {
                IsBusy = true;

                var backJson = await WebDataHelper.Instance.GetFromUrlWithAuthReturnString(newsUrl, null, 3);
                if (backJson != null)
                {
                    var result = JsonConvertHelper.Instance.DeserializeObject<NewsActivityModel>(backJson);
                    if (result != null)
                    {
                        if (result.news.Count != 0)
                        {
                            if (isRefresh)
                            {
                                collection.Clear();

                                ActivityCollection = new ObservableCollection<ActivityModel>(result.news);
                            }
                            else
                            {
                                result.news.ForEach(A =>
                                {
                                    collection.Add(A);
                                });
                            }

                            //及时更改类别的分页索引记录，如果没有加载成功就不会被自增记录
                            DicStore.AddOrUpdateValue<int>(pageIndexConst, ++currentPageIndex);

                            //只存第一页的数据
                            if (cacheFileName != null)
                            {
                                //将第一页数据缓存到本地
                                FileHelper.Instance.SaveTextToFile(CacheConfig.Instance.NewsListFileCacheRelativePath, cacheFileName, backJson);
                            }
                        }
                        else
                        {
                            //判断是不是分页索引第一页，如果是第一页并且获取到的条目个数为0个，则此时要求再次获取。此时提示“没有获取到数据，请重试”
                            if (currentPageIndex == 1 && !isRefresh)
                            {
                                //这里使用反射
                                RetryBox.Instance.ShowRetry(AppNetworkMessageConst.COLLECTION_ITEM_IS_ZERO, typeof(MyCF.ViewModel.News.NewsViewModel), "GetActivityNews", new object[] { collection, newsUrl, pageIndexConst, cacheFileName, adCollection, isRefresh });
                            }
                            //如果不是第一页，就不用去管
                            else { }
                        }
                    }
                    else
                    {
                        //判断是不是分页索引第一页，如果是第一页的话则会弹出提示，重新加载。因为第一页没有加载成功，本条目下就没数据，此时就要弹出一个东西让其重新加载数据。如果不是第一页就不用去管了
                        if (currentPageIndex == 1)
                        {
                            //加载本地数据
                            var localJson = await FileHelper.Instance.ReadTextFromFile(CacheConfig.Instance.NewsListFileCacheRelativePath, cacheFileName);
                            if (localJson != null)
                            {
                                var localResult = JsonConvertHelper.Instance.DeserializeObject<NewsActivityModel>(localJson);
                                if (localResult != null)
                                {
                                    //因为如果第一页获取到的数据为0条时不会写到本地的，所以，从本地获取到的数据条目数一定不为0
                                    if (localResult.news.Count != 0)
                                    {
                                        if (!isRefresh)
                                        {
                                            localResult.news.ForEach(A =>
                                            {
                                                collection.Add(A);
                                            });
                                        }

                                        //及时更改类别的分页索引记录，如果没有加载成功就不会被自增记录。加载成功则为第二页做准备
                                        DicStore.AddOrUpdateValue<int>(pageIndexConst, ++currentPageIndex);
                                    }
                                }
                                //虽然错误的数据是不会写到本地的，但如果反序列化失败一样会出错
                                else
                                {
                                    if (!isRefresh)
                                    {
                                        //如果此时还有网络，说明加载过程出错，提示信息为“加载数据出错，请重试。”
                                        if (AppEnvironment.IsInternetAccess)
                                        {
                                            RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWOTK_IS_ERROR, typeof(MyCF.ViewModel.News.NewsViewModel), "GetActivityNews", new object[] { collection, newsUrl, pageIndexConst, cacheFileName, adCollection, isRefresh });
                                        }
                                        //如果没有网络，说明数据加载失败是因为没有网络造成的。提示信息为“没有网络，请确认网络连接。”
                                        else
                                        {
                                            RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_ERROR, typeof(MyCF.ViewModel.News.NewsViewModel), "GetActivityNews", new object[] { collection, newsUrl, pageIndexConst, cacheFileName, adCollection, isRefresh });
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!isRefresh)
                                {
                                    //如果此时还有网络，说明加载过程出错，提示信息为“加载数据出错，请重试。”
                                    if (AppEnvironment.IsInternetAccess)
                                    {
                                        RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWOTK_IS_ERROR, typeof(MyCF.ViewModel.News.NewsViewModel), "GetActivityNews", new object[] { collection, newsUrl, pageIndexConst, cacheFileName, adCollection, isRefresh });
                                    }
                                    //如果没有网络，说明数据加载失败是因为没有网络造成的。提示信息为“没有网络，请确认网络连接。”
                                    else
                                    {
                                        RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_NULL, typeof(MyCF.ViewModel.News.NewsViewModel), "GetActivityNews", new object[] { collection, newsUrl, pageIndexConst, cacheFileName, adCollection, isRefresh });
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    //判断是不是分页索引第一页，如果是第一页的话则会弹出提示，重新加载。因为第一页没有加载成功，本条目下就没数据，此时就要弹出一个东西让其重新加载数据。如果不是第一页就不用去管了
                    if (currentPageIndex == 1)
                    {
                        //如果此时还有网络，说明加载过程出错，提示信息为“加载数据出错，请重试。”
                        if (AppEnvironment.IsInternetAccess && !isRefresh)
                        {
                            RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWOTK_IS_ERROR, typeof(MyCF.ViewModel.News.NewsViewModel), "GetActivityNews", new object[] { collection, newsUrl, pageIndexConst, cacheFileName, adCollection, isRefresh });
                        }
                        //如果没有网络，说明数据加载失败是因为没有网络造成的。提示信息为“没有网络，请确认网络连接。”
                        else
                        {
                            //加载本地数据
                            var localJson = await FileHelper.Instance.ReadTextFromFile(CacheConfig.Instance.NewsListFileCacheRelativePath, cacheFileName);
                            if (localJson != null)
                            {
                                var localResult = JsonConvertHelper.Instance.DeserializeObject<NewsActivityModel>(localJson);
                                if (localResult != null)
                                {
                                    //因为如果第一页获取到的数据为0条时不会写到本地的，所以，从本地获取到的数据条目数一定不为0
                                    if (localResult.news.Count != 0)
                                    {
                                        if (!isRefresh)
                                        {
                                            localResult.news.ForEach(A =>
                                            {
                                                collection.Add(A);
                                            });
                                        }

                                        //及时更改类别的分页索引记录，如果没有加载成功就不会被自增记录。加载成功则为第二页做准备
                                        DicStore.AddOrUpdateValue<int>(pageIndexConst, ++currentPageIndex);
                                    }
                                    else
                                    {
                                        if (!isRefresh)
                                        {
                                            RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_NULL, typeof(MyCF.ViewModel.News.NewsViewModel), "GetActivityNews", new object[] { collection, newsUrl, pageIndexConst, cacheFileName, adCollection, isRefresh });
                                        }
                                    }
                                }
                                //虽然错误的数据是不会写到本地的，但如果反序列化失败一样会出错，又没有网络
                                else
                                {
                                    if (!isRefresh)
                                    {
                                        RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_ERROR, typeof(MyCF.ViewModel.News.NewsViewModel), "GetActivityNews", new object[] { collection, newsUrl, pageIndexConst, cacheFileName, adCollection, isRefresh });
                                    }
                                }
                            }
                            else
                            {
                                if (!isRefresh)
                                {
                                    RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_NULL, typeof(MyCF.ViewModel.News.NewsViewModel), "GetActivityNews", new object[] { collection, newsUrl, pageIndexConst, cacheFileName, adCollection, isRefresh });
                                }
                            }
                        }
                    }
                    //如果不是第一页就不用管了
                    else { }
                }

                IsBusy = false;
            }
        }
    }
}
