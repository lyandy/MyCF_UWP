//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.ViewModel.Map
//类名称:       MapListViewModel
//创建时间:     2015/8/18 星期二 11:22:17
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Api.ApiRoot;
using MyCF.Config;
using MyCF.Const;
using MyCF.Helper;
using MyCF.Model.Map;
using MyCF.UIControl.UserControls.RetryUICtrl;
using MyCF.ViewModelAttribute.Map;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.ViewModel.Map
{
    public class MapListViewModel : MapListViewModelAttribute
    {
        public async void GetMapList()
        {

            IsBusy = true;

            List<MapModel> mapList = new List<MapModel>();

            #region  请求爆破、个竞、团队和生化数据
            //请求爆破数据
            var backBlastJson = await WebDataHelper.Instance.GetFromUrlWithAuthReturnString(ApiMapRoot.Instance.MapBlastUrl, null, 3);
            if (backBlastJson != null)
            {
                var resultBlast = JsonConvertHelper.Instance.DeserializeObject<List<MapModel>>(backBlastJson);
                if (resultBlast != null)
                {
                    resultBlast.ForEach(o =>
                    {
                        o.play_name = "爆破";
                        o.play_Background_Color = "#E38000";

                        mapList.Add(o);
                    });

                    
                }
            }

            //请求个竞数据
            var backPersonJson = await WebDataHelper.Instance.GetFromUrlWithAuthReturnString(ApiMapRoot.Instance.MapPersonUrl, null, 3);
            if (backPersonJson != null)
            {
                var resultPerson = JsonConvertHelper.Instance.DeserializeObject<List<MapModel>>(backPersonJson);
                if (resultPerson != null)
                {
                    resultPerson.ForEach(o =>
                    {
                        o.play_name = "个竞";
                        o.play_Background_Color = "#75BA24";

                        mapList.Add(o);
                    });
                }
            }

            //请求团队数据
            var backTeamJson = await WebDataHelper.Instance.GetFromUrlWithAuthReturnString(ApiMapRoot.Instance.MapTeamUrl, null, 3);
            if (backTeamJson != null)
            {
                var resultTeam = JsonConvertHelper.Instance.DeserializeObject<List<MapModel>>(backTeamJson);
                if (resultTeam != null)
                {
                    resultTeam.ForEach(o =>
                    {
                        o.play_name = "团队";
                        o.play_Background_Color = "#3F97E9";

                        mapList.Add(o);
                    });
                }
            }

            //请求生化数据
            var backZombieJson = await WebDataHelper.Instance.GetFromUrlWithAuthReturnString(ApiMapRoot.Instance.MapZombieUrl, null, 3);
            if (backZombieJson != null)
            {
                var resultZombie = JsonConvertHelper.Instance.DeserializeObject<List<MapModel>>(backZombieJson);
                if (resultZombie != null)
                {
                    resultZombie.ForEach(o =>
                    {
                        o.play_name = "生化";
                        o.play_Background_Color = "#CC3BED";

                        mapList.Add(o);
                    });
                }
            }

            #endregion

            if (mapList.Count != 0)
            {
                mapList.ForEach(o => MapListCollection.Add(o));
                
                var json = JsonConvert.SerializeObject(mapList);
                FileHelper.Instance.SaveTextToFile(CacheConfig.Instance.MapListFileCacheRelativePath, AppCacheNewsFileNameConst.CACHE_MAP_MAPLIST_FILENAME, json);

                mapList.Clear();
                mapList = null;
            }
            else
            {
                //加载本地数据
                var localJson = await FileHelper.Instance.ReadTextFromFile(CacheConfig.Instance.MapListFileCacheRelativePath, AppCacheNewsFileNameConst.CACHE_MAP_MAPLIST_FILENAME);
                if (localJson != null)
                {
                    var localResult = JsonConvertHelper.Instance.DeserializeObject<List<MapModel>>(localJson);
                    if (localResult != null)
                    {
                        //因为如果第一页获取到的数据为0条时不会写到本地的，所以，从本地获取到的数据条目数一定不为0
                        if (localResult.Count != 0)
                        {
                            localResult.ForEach(A =>
                            {
                                MapListCollection.Add(A);
                            });
                        }
                    }
                    //虽然错误的数据是不会写到本地的，但如果反序列化失败一样会出错
                    else
                    {
                        //如果此时还有网络，说明加载过程出错，提示信息为“加载数据出错，请重试。”
                        if (AppEnvironment.IsInternetAccess)
                        {
                            RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWOTK_IS_ERROR, typeof(MyCF.ViewModel.Map.MapListViewModel), "GetMapList", null);
                        }
                        //如果没有网络，说明数据加载失败是因为没有网络造成的。提示信息为“没有网络，请确认网络连接。”
                        else
                        {
                            RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_ERROR, typeof(MyCF.ViewModel.Map.MapListViewModel), "GetMapList", null);
                        }
                    }
                }
            }
            IsBusy = false;
        }
    }
}
