//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Model.Map
//类名称:       MapModel
//创建时间:     2015/8/18 星期二 10:44:59
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Async;
using MyCF.Base;
using MyCF.Config;
using MyCF.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MyCF.Model.Map
{

    //public class Rootobject
    //{
    //    private List<MapModel> _maps = new List<MapModel>();
    //    public List<MapModel> maps
    //    {
    //        get
    //        {
    //            return _maps;
    //        }
    //        set
    //        {
    //            _maps = value;
    //        }
    //    }
    //}

    public class MapModel : ModelPropertyBase
    {
        //地图id，用来获取具体点位信息
        public string id { get; set; }
        //点位地图名称
        public string map_name { get; set; }
        //模式名称
        public string play_name { get; set; }
        //模式名称背景色
        public string play_Background_Color { get; set; }
        //点位图片
        private string _title_url = "";
        public string title_url
        {
            get
            {
                return _title_url;
            }
            set
            {
                _title_url = value;

                if (!string.IsNullOrEmpty(_title_url))
                {
                    GetConvertTitleUrl();
                }

            }
        }

        private AsyncProperty<object> _ConvertTitleUrl;
        [JsonIgnore]
        public AsyncProperty<object> ConvertTitleUrl
        {
            get
            {
                //解决集合数据不是通过反序列化生成导致图片资源无法显示的Bug，比如通过ForEach循环Add的数据集合。并且一定要判空，不然会死循环。
                if (_ConvertTitleUrl == null)
                {
                    if (!string.IsNullOrEmpty(_title_url))
                    {
                        GetConvertTitleUrl();
                    }
                }

                return _ConvertTitleUrl;
            }
            set
            {
                if (_ConvertTitleUrl != value)
                {
                    _ConvertTitleUrl = value;

                    OnPropertyChanged();
                }
            }
        }
        public void GetConvertTitleUrl()
        {
            ConvertTitleUrl = new AsyncProperty<object>(async () =>
            {
                try
                {
                    var imageName = title_url.Split('/').Last();
                    var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(CacheConfig.Instance.ImageThumbnailCacheRelativePath, CreationCollisionOption.OpenIfExists);
                    var file = await folder.TryGetItemAsync(imageName);
                    if (file != null)
                    {
                        return folder.Path + "\\" + imageName;
                    }
                    else
                    {
                        WebDownFileHelper.Instance.SaveAsyncWithHttp(title_url, imageName, folder);

                        return title_url;
                    }
                }
                catch
                {
                    return title_url;
                }
            }, null);
        }

        //暂时无用
        public string type { get; set; }
        //点位数
        public string pnum { get; set; }
    }
}
