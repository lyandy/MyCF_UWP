//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.ViewModel.Video
//类名称:       VideoListViewModel
//创建时间:     2015/8/14 星期五 16:42:12
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Config;
using MyCF.Const;
using MyCF.Extension.CommonObjectEx;
using MyCF.Helper;
using MyCF.Model.Video;
using MyCF.UIControl.UserControls.RetryUICtrl;
using MyCF.ViewModelAttribute.Video;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.ViewModel.Video
{
    public class VideoListViewModel : VideoListViewModelAttribute
    {
        public async void GetVideoList(ObservableCollection<VideoModel> collection, string videoUrl, string pageIndexConst, string cacheFileName = null)
        {
            //取出当前的分页索引记录
            int currentPageIndex = DicStore.GetValueOrDefault<int>(pageIndexConst, 1);
            Debug.WriteLine(currentPageIndex);
            if (currentPageIndex <= AppCommonConst.PAGE_LOAD_DATA_MAX_INDEX)
            {
                IsBusy = true;

                var backJson = await WebDataHelper.Instance.GetFromUrlWithAuthReturnString(videoUrl, null, 3);
                if (backJson != null)
                {
                    string json = string.Empty;
                    try
                    {
                        json = backJson.Replace("var searchObj=", "").Replace("};", "}");
                    }
                    catch { }

                    var result = JsonConvertHelper.Instance.DeserializeObject<VideoListModel>(json);
                    if (result != null)
                    {
                        if (result.msg.result.Count != 0)
                        {
                            result.msg.result.ForEach(A =>
                            {
                                collection.Add(A);
                            });
                            //及时更改类别的分页索引记录，如果没有加载成功就不会被自增记录
                            DicStore.AddOrUpdateValue<int>(pageIndexConst, ++currentPageIndex);

                            //只存第一页的数据
                            if (cacheFileName != null)
                            {
                                //将第一页数据缓存到本地
                                FileHelper.Instance.SaveTextToFile(CacheConfig.Instance.VideoListFileCacheRelativePath, cacheFileName, json);
                            }
                        }
                        else
                        {
                            //判断是不是分页索引第一页，如果是第一页并且获取到的条目个数为0个，则此时要求再次获取。此时提示“没有获取到数据，请重试”
                            if (currentPageIndex == 1)
                            {
                                //这里使用反射
                                RetryBox.Instance.ShowRetry(AppNetworkMessageConst.COLLECTION_ITEM_IS_ZERO, typeof(MyCF.ViewModel.Video.VideoListViewModel), "GetVideoList", new object[] { collection, videoUrl, pageIndexConst, cacheFileName });
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
                            var localJson = await FileHelper.Instance.ReadTextFromFile(CacheConfig.Instance.VideoListFileCacheRelativePath, cacheFileName);
                            if (localJson != null)
                            {
                                var localResult = JsonConvertHelper.Instance.DeserializeObject<VideoListModel>(localJson);
                                if (localResult != null)
                                {
                                    //因为如果第一页获取到的数据为0条时不会写到本地的，所以，从本地获取到的数据条目数一定不为0
                                    if (localResult.msg.result.Count != 0)
                                    {
                                        localResult.msg.result.ForEach(A =>
                                        {
                                            collection.Add(A);
                                        });

                                        //及时更改类别的分页索引记录，如果没有加载成功就不会被自增记录。加载成功则为联网第二页做准备
                                        DicStore.AddOrUpdateValue<int>(pageIndexConst, ++currentPageIndex);
                                    }
                                }
                                //虽然错误的数据是不会写到本地的，但如果反序列化失败一样会出错
                                else
                                {
                                    //如果此时还有网络，说明加载过程出错，提示信息为“加载数据出错，请重试。”
                                    if (AppEnvironment.IsInternetAccess)
                                    {
                                        RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWOTK_IS_ERROR, typeof(MyCF.ViewModel.Video.VideoListViewModel), "GetVideoList", new object[] { collection, videoUrl, pageIndexConst, cacheFileName });
                                    }
                                    //如果没有网络，说明数据加载失败是因为没有网络造成的。提示信息为“没有网络，请确认网络连接。”
                                    else
                                    {
                                        RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_ERROR, typeof(MyCF.ViewModel.Video.VideoListViewModel), "GetVideoList", new object[] { collection, videoUrl, pageIndexConst, cacheFileName });
                                    }
                                }
                            }
                            else
                            {
                                //如果此时还有网络，说明加载过程出错，提示信息为“加载数据出错，请重试。”
                                if (AppEnvironment.IsInternetAccess)
                                {
                                    RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWOTK_IS_ERROR, typeof(MyCF.ViewModel.Video.VideoListViewModel), "GetVideoList", new object[] { collection, videoUrl, pageIndexConst, cacheFileName });
                                }
                                //如果没有网络，说明数据加载失败是因为没有网络造成的。提示信息为“没有网络，请确认网络连接。”
                                else
                                {
                                    RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_NULL, typeof(MyCF.ViewModel.Video.VideoListViewModel), "GetVideoList", new object[] { collection, videoUrl, pageIndexConst, cacheFileName });
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
                        if (AppEnvironment.IsInternetAccess)
                        {
                            RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWOTK_IS_ERROR, typeof(MyCF.ViewModel.Video.VideoListViewModel), "GetVideoList", new object[] { collection, videoUrl, pageIndexConst, cacheFileName });
                        }
                        //如果没有网络，说明数据加载失败是因为没有网络造成的。提示信息为“没有网络，请确认网络连接。”
                        else
                        {
                            //加载本地数据
                            var localJson = await FileHelper.Instance.ReadTextFromFile(CacheConfig.Instance.VideoListFileCacheRelativePath, cacheFileName);
                            if (localJson != null)
                            {
                                var localResult = JsonConvertHelper.Instance.DeserializeObject<VideoListModel>(localJson);
                                if (localResult != null)
                                {
                                    //因为如果第一页获取到的数据为0条时不会写到本地的，所以，从本地获取到的数据条目数一定不为0
                                    if (localResult.msg.result.Count != 0)
                                    {
                                        localResult.msg.result.ForEach(A =>
                                        {
                                            collection.Add(A);
                                        });

                                        //及时更改类别的分页索引记录，如果没有加载成功就不会被自增记录。加载成功则为联网第二页做准备
                                        DicStore.AddOrUpdateValue<int>(pageIndexConst, ++currentPageIndex);
                                    }
                                    else
                                    {
                                        RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_NULL, typeof(MyCF.ViewModel.Video.VideoListViewModel), "GetVideoList", new object[] { collection, videoUrl, pageIndexConst, cacheFileName });
                                    }
                                }
                                //虽然错误的数据是不会写到本地的，但如果反序列化失败一样会出错，又没有网络
                                else
                                {
                                    RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_ERROR, typeof(MyCF.ViewModel.Video.VideoListViewModel), "GetVideoList", new object[] { collection, videoUrl, pageIndexConst, cacheFileName });
                                }
                            }
                            else
                            {
                                RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_NULL, typeof(MyCF.ViewModel.Video.VideoListViewModel), "GetVideoList", new object[] { collection, videoUrl, pageIndexConst, cacheFileName });
                            }
                        }
                    }
                    //如果不是第一页就不用管了
                    else { }
                }

                IsBusy = false;
            }
        }

        #region 清理ViewModel
        public override void Cleanup()
        {
            base.Cleanup();

            #region 比赛
            VideoChampionCFPLCollection.ForEach(o => o.Dispose());
            VideoChampionCFPLCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_CHAMPION_CFPL_PAGE_INDEX_CONST);

            VideoChampionHundredsOfCityCollection.ForEach(o => o.Dispose());
            VideoChampionHundredsOfCityCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_CHAMPION_HUNDREDSOFCITY_PAGE_INDEX_CONST);

            VideoChampionAllPeopleCollection.ForEach(o => o.Dispose());
            VideoChampionAllPeopleCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_CHAMPION_ALLPEOPLE_PAGE_INDEX_CONST);

            VideoChampionCFDLCollection.ForEach(o => o.Dispose());
            VideoChampionCFDLCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_CHAMPION_CFDL_PAGE_INDEX_CONST);

            VideoChampionCFSCollection.ForEach(o => o.Dispose());
            VideoChampionCFSCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_CHAMPION_CFS_PAGE_INDEX_CONST);

            VideoChampionOtherCollection.ForEach(o => o.Dispose());
            VideoChampionOtherCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_CHAMPION_OTHER_PAGE_INDEX_CONST);
            #endregion

            #region 节目

            VideoActSuperMethodCollection.ForEach(o => o.Dispose());
            VideoActSuperMethodCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_ACT_SUPERMETHOD_PAGE_INDEX_CONST);

            VideoActSuperRsenalCollection.ForEach(o => o.Dispose());
            VideoActSuperRsenalCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_ACT_SUPERARSENAL_PAGE_INDEX_CONST);

            VideoActSuperTimeCollection.ForEach(o => o.Dispose());
            VideoActSuperTimeCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_ACT_SUPERTIME_PAGE_INDEX_CONST);

            VideoActFireCollection.ForEach(o => o.Dispose());
            VideoActFireCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_ACT_FIRE_PAGE_INDEX_CONST);

            VideoActOtherCollection.ForEach(o => o.Dispose());
            VideoActOtherCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_ACT_OTHER_PAGE_INDEX_CONST);

            VideoActBrotherCollection.ForEach(o => o.Dispose());
            VideoActBrotherCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_ACT_BROTHER_PAGE_INDEX_CONST);

            #endregion

            #region 高手

            VideoSuperTGASTARCollection.ForEach(o => o.Dispose());
            VideoSuperTGASTARCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_SUPER_TGASTAR_PAGE_INDEX_CONST);

            VideoSuperProfressionalCollection.ForEach(o => o.Dispose());
            VideoSuperProfressionalCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_SUPER_PROFRESSIONAL_PAGE_INDEX_CONST);

            VideoSuperPeopleCollection.ForEach(o => o.Dispose());
            VideoSuperPeopleCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_SUPER_PEOPELE_PAGE_INDEX_CONST);

            VideoSuperOtherCollection.ForEach(o => o.Dispose());
            VideoSuperOtherCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_SUPER_OTHER_PAGE_INDEX_CONST);

            VideoSuperForeignerCollection.ForEach(o => o.Dispose());
            VideoSuperForeignerCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_SUPER_FOREIGNER_PAGE_INDEX_CONST);

            #endregion

            #region 教学

            VideoTeachGunCollection.ForEach(o => o.Dispose());
            VideoTeachGunCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_TEACH_GUN_PAGE_INDEX_CONST);

            VideoTeachMapCollection.ForEach(o => o.Dispose());
            VideoTeachMapCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_TEACH_MAP_PAGE_INDEX_CONST);

            VideoTeachSkillCollection.ForEach(o => o.Dispose());
            VideoTeachSkillCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_TEACH_SKILL_PAGE_INDEX_CONST);

            VideoTeachOtherCollection.ForEach(o => o.Dispose());
            VideoTeachOtherCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_TEACH_OTHER_PAGE_INDEX_CONST);

            #endregion

            #region 官方

            VideoOfficialVCRCollection.ForEach(o => o.Dispose());
            VideoOfficialVCRCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_OFFICIAL_VCR_PAGE_INDEX_CONST);

            VideoOfficialGAMECGCollection.ForEach(o => o.Dispose());
            VideoOfficialGAMECGCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_OFFICIAL_GAMECG_PAGE_INDEX_CONST);

            VideoOfficialVersionCollection.ForEach(o => o.Dispose());
            VideoOfficialVersionCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_OFFICIAL_GAMECG_PAGE_INDEX_CONST);

            VideoOfficialOtherCollection.ForEach(o => o.Dispose());
            VideoOfficialOtherCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_OFFICIAL_OTHER_PAGE_INDEX_CONST);

            #endregion

            #region 娱乐

            VideoPlayFunCollection.ForEach(o => o.Dispose());
            VideoPlayFunCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_PLAY_FUN_PAGE_INDEX_CONST);

            VideoPlayMovieCollection.ForEach(o => o.Dispose());
            VideoPlayMovieCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_PLAY_MOVIE_PAGE_INDEX_CONST);

            VideoPlaySongCollection.ForEach(o => o.Dispose());
            VideoPlaySongCollection.Clear();
            DicStore.RemoveKey(AppPageIndexConst.VIDEO_PLAY_SONG_PAGE_INDEX_CONST);

            #endregion

        }
        #endregion
    }
}
