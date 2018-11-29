//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.ViewModel.Ency
//类名称:       EncyViewModel
//创建时间:     2015/8/17 星期一 12:25:26
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Api.ApiRoot;
using MyCF.Config;
using MyCF.Const;
using MyCF.Helper;
using MyCF.Model.Ency;
using MyCF.UIControl.UserControls.RetryUICtrl;
using MyCF.ViewModelAttribute.Ency;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.ViewModel.Ency
{
    public class EncyViewModel : EncyViewModelAttribute
    {
        public async void GetEncyWeapons()
        {

            IsBusy = true;

            var backJson = await WebDataHelper.Instance.GetFromUrlWithAuthReturnString(ApiEncyRoot.Instance.EncyUrl, null, 3);
            if (backJson != null)
            {
                string json = string.Empty;
                try
                {
                    json = backJson.Replace("[", "{\"weapons\":[").Replace("]", "]}");
                }
                catch { }

                var result = JsonConvertHelper.Instance.DeserializeObject<EncyModel>(json);
                if (result != null)
                {
                    if (result.weapons.Count != 0)
                    {
                        (result.weapons.OrderByDescending(o => o.tag).ToList()).ForEach(A =>
                        {
                            WeaponCollection.Add(A);
                        });

                        FileHelper.Instance.SaveTextToFile(CacheConfig.Instance.EncyListFileCacheRelativePath, AppCacheNewsFileNameConst.CACHE_ENCY_WEAPONS_FILENAME, json);
                    }
                    else
                    {
                        //这里使用反射
                        RetryBox.Instance.ShowRetry(AppNetworkMessageConst.COLLECTION_ITEM_IS_ZERO, typeof(MyCF.ViewModel.Ency.EncyViewModel), "GetEncyWeapons", null);
                    }
                }
                else
                {
                    //加载本地数据
                    var localJson = await FileHelper.Instance.ReadTextFromFile(CacheConfig.Instance.EncyListFileCacheRelativePath, AppCacheNewsFileNameConst.CACHE_ENCY_WEAPONS_FILENAME);
                    if (localJson != null)
                    {
                        var localResult = JsonConvertHelper.Instance.DeserializeObject<EncyModel>(localJson);
                        if (localResult != null)
                        {
                            //因为如果第一页获取到的数据为0条时不会写到本地的，所以，从本地获取到的数据条目数一定不为0
                            if (localResult.weapons.Count != 0)
                            {
                                (localResult.weapons.OrderByDescending(o => o.tag).ToList()).ForEach(A =>
                                {
                                    WeaponCollection.Add(A);
                                });
                            }
                        }
                        //虽然错误的数据是不会写到本地的，但如果反序列化失败一样会出错
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
                                RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_ERROR, typeof(MyCF.ViewModel.Ency.EncyViewModel), "GetEncyWeapons", null);
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
                            RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_NULL, typeof(MyCF.ViewModel.Ency.EncyViewModel), "GetEncyWeapons", null);
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
                    var localJson = await FileHelper.Instance.ReadTextFromFile(CacheConfig.Instance.EncyListFileCacheRelativePath, AppCacheNewsFileNameConst.CACHE_ENCY_WEAPONS_FILENAME);
                    if (localJson != null)
                    {
                        var localResult = JsonConvertHelper.Instance.DeserializeObject<EncyModel>(localJson);
                        if (localResult != null)
                        {
                            //因为如果第一页获取到的数据为0条时不会写到本地的，所以，从本地获取到的数据条目数一定不为0
                            if (localResult.weapons.Count != 0)
                            {
                                (localResult.weapons.OrderByDescending(o => o.tag).ToList()).ForEach(A =>
                                {
                                    WeaponCollection.Add(A);
                                });
                            }
                            else
                            {
                                RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_NULL, typeof(MyCF.ViewModel.Ency.EncyViewModel), "GetEncyWeapons", null);
                            }
                        }
                        //虽然错误的数据是不会写到本地的，但如果反序列化失败一样会出错，又没有网络
                        else
                        {
                            RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_ERROR, typeof(MyCF.ViewModel.Ency.EncyViewModel), "GetEncyWeapons", null);
                        }
                    }
                    else
                    {
                        RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_NULL, typeof(MyCF.ViewModel.Ency.EncyViewModel), "GetEncyWeapons", null);
                    }
                }
            }

            //建立副本为后续的搜索做准备
            WeaponCollection.ToList().ForEach(o => WeaponCollectionCopy.Add(o));

            IsBusy = false;
        }
    }
}
