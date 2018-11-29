//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.ViewModel.Map
//类名称:       MapDetailViewModel
//创建时间:     2015/8/18 星期二 15:51:10
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Api.ApiRoot;
using MyCF.Config;
using MyCF.Const;
using MyCF.Extension.CommonObjectEx;
using MyCF.Helper;
using MyCF.Model.Map;
using MyCF.UIControl.UserControls.RetryUICtrl;
using MyCF.ViewModelAttribute.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.ViewModel.Map
{
    public class MapDetailViewModel : MapDetailViewModelAttribute
    {
        public async void GetMapDetail()
        {

            IsBusy = true;

            var backJson = await WebDataHelper.Instance.GetFromUrlWithAuthReturnString(ApiMapRoot.Instance.MapDetailUrl, null, 3);
            if (backJson != null)
            {
                var result = JsonConvertHelper.Instance.DeserializeObject<MapDetailModel>(backJson);
                if (result != null)
                {
                    if (result.data.Count != 0)
                    {
                        result.data.ForEach(A =>
                        {
                            //这里要特殊处理的
                            if (A.urls.Count != 0)
                            {
                                MapDetailCollection.Add(A);
                            }
                        });

                        FileHelper.Instance.SaveTextToFile(CacheConfig.Instance.MapListFileCacheRelativePath, AppCacheNewsFileNameConst.CACHE_MAP_DETAIL_ID_FILENAME, backJson);
                    }
                    else
                    {
                        //这里使用反射
                        RetryBox.Instance.ShowRetry(AppNetworkMessageConst.COLLECTION_ITEM_IS_ZERO, typeof(MyCF.ViewModel.Map.MapDetailViewModel), "GetMapDetail", null);
                    }
                }
                else
                {
                    //加载本地数据
                    var localJson = await FileHelper.Instance.ReadTextFromFile(CacheConfig.Instance.MapListFileCacheRelativePath, AppCacheNewsFileNameConst.CACHE_MAP_DETAIL_ID_FILENAME);
                    if (localJson != null)
                    {
                        var localResult = JsonConvertHelper.Instance.DeserializeObject<MapDetailModel>(localJson);
                        if (localResult != null)
                        {
                            //因为如果第一页获取到的数据为0条时不会写到本地的，所以，从本地获取到的数据条目数一定不为0
                            if (localResult.data.Count != 0)
                            {
                                localResult.data.ForEach(A =>
                                {
                                    //这里要特殊处理的
                                    if (A.urls.Count != 0)
                                    {
                                        MapDetailCollection.Add(A);
                                    }
                                });
                            }
                        }
                        //虽然错误的数据是不会写到本地的，但如果反序列化失败一样会出错
                        else
                        {
                            //如果此时还有网络，说明加载过程出错，提示信息为“加载数据出错，请重试。”
                            if (AppEnvironment.IsInternetAccess)
                            {
                                RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWOTK_IS_ERROR, typeof(MyCF.ViewModel.Map.MapDetailViewModel), "GetMapDetail", null);
                            }
                            //如果没有网络，说明数据加载失败是因为没有网络造成的。提示信息为“没有网络，请确认网络连接。”
                            else
                            {
                                RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_ERROR, typeof(MyCF.ViewModel.Map.MapDetailViewModel), "GetMapDetail", null);
                            }
                        }
                    }
                    else
                    {
                        //如果此时还有网络，说明加载过程出错，提示信息为“加载数据出错，请重试。”
                        if (AppEnvironment.IsInternetAccess)
                        {
                            RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWOTK_IS_ERROR, typeof(MyCF.ViewModel.Map.MapDetailViewModel), "GetMapDetail", null);
                        }
                        //如果没有网络，说明数据加载失败是因为没有网络造成的。提示信息为“没有网络，请确认网络连接。”
                        else
                        {
                            RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_NULL, typeof(MyCF.ViewModel.Map.MapDetailViewModel), "GetMapDetail", null);
                        }
                    }
                }
            }
            else
            {
                //如果此时还有网络，说明加载过程出错，提示信息为“加载数据出错，请重试。”
                if (AppEnvironment.IsInternetAccess)
                {
                    RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWOTK_IS_ERROR, typeof(MyCF.ViewModel.Ency.EncyViewModel), "GetEncyWeapons", null);
                }
                //如果没有网络，说明数据加载失败是因为没有网络造成的。提示信息为“没有网络，请确认网络连接。”
                else
                {
                    //加载本地数据
                    var localJson = await FileHelper.Instance.ReadTextFromFile(CacheConfig.Instance.MapListFileCacheRelativePath, AppCacheNewsFileNameConst.CACHE_MAP_DETAIL_ID_FILENAME);
                    if (localJson != null)
                    {
                        var localResult = JsonConvertHelper.Instance.DeserializeObject<MapDetailModel>(localJson);
                        if (localResult != null)
                        {
                            //因为如果第一页获取到的数据为0条时不会写到本地的，所以，从本地获取到的数据条目数一定不为0
                            if (localResult.data.Count != 0)
                            {
                                localResult.data.ForEach(A =>
                                {
                                    //这里要特殊处理的
                                    if (A.urls.Count != 0)
                                    {
                                        MapDetailCollection.Add(A);
                                    }
                                });
                            }
                            else
                            {
                                RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_NULL, typeof(MyCF.ViewModel.Map.MapDetailViewModel), "GetMapDetail", null);
                            }
                        }
                        //虽然错误的数据是不会写到本地的，但如果反序列化失败一样会出错，又没有网络
                        else
                        {
                            RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_ERROR, typeof(MyCF.ViewModel.Map.MapDetailViewModel), "GetMapDetail", null);
                        }
                    }
                    else
                    {
                        RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_NULL, typeof(MyCF.ViewModel.Map.MapDetailViewModel), "GetMapDetail", null);
                    }
                }
            }

            IsBusy = false;
        }

        #region 清理ViewModel
        public override void Cleanup()
        {
            base.Cleanup();

            MapDetailCollection.ForEach(o =>
                {
                    o.purls.ForEach(A => A.Dispose());
                    o.Dispose();
                });

            MapDetailCollection.Clear();

            DicStore.RemoveKey(AppCommonConst.CURRENT_MAP_ID);
        }
        #endregion
    }
}
