//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Model.Ency
//类名称:       EncyModel
//创建时间:     2015/8/17 星期一 11:57:55
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Async;
using MyCF.Base;
using MyCF.Config;
using MyCF.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MyCF.Model.Ency
{
    public class EncyModel
    {
        private List<WeaponModel> _weapons = new List<WeaponModel>();
        public List<WeaponModel> weapons
        {
            get
            {
                return _weapons;
            }
            set
            {
                _weapons = value;
            }
        }
    }

    public class WeaponModel : ModelPropertyBase
    {
        //武器代码，用来获取更详细的武器信息以及获取武器图片信息
        private string _code = "C0001";
        public string code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;

                codeImageUrl = "http://ossweb-img.qq.com/images/qtalk/cf_weapon/" + _code + ".png";
            }
        }

        private string codeImageUrl { get; set; }


        private AsyncProperty<object> _ConvertCodeImg;
        public AsyncProperty<object> ConvertCodeImg
        {
            get
            {
                //解决集合数据不是通过反序列化生成导致图片资源无法显示的Bug，比如通过ForEach循环Add的数据集合。并且一定要判空，不然会死循环。
                if (_ConvertCodeImg == null)
                {
                    if (!string.IsNullOrEmpty(code))
                    {
                        GetConverCodeImg();
                    }
                }

                return _ConvertCodeImg;
            }
            set
            {
                if (_ConvertCodeImg != value)
                {
                    _ConvertCodeImg = value;

                    OnPropertyChanged();
                }
            }
        }
        public void GetConverCodeImg()
        {
            ConvertCodeImg = new AsyncProperty<object>(async () =>
            {
                try
                {
                    var imageName = code + ".png";
                    var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(CacheConfig.Instance.ImageThumbnailCacheRelativePath, CreationCollisionOption.OpenIfExists);
                    var file = await folder.TryGetItemAsync(imageName);
                    if (file != null)
                    {
                        return folder.Path + "\\" + imageName;
                    }
                    else
                    {
                        WebDownFileHelper.Instance.SaveAsyncWithHttp(codeImageUrl, imageName, folder);

                        return codeImageUrl;
                    }
                }
                catch
                {
                    return codeImageUrl;
                }
            }, null);
        }

        //武器名称
        public string name { get; set; }
        //武器伤害值
        public object power { get; set; }
        //武器点类型：CF点、GP点、FP点
        public string cointype { get; set; }
        //武器是否为为“new” ，如果为新，则为"new"
        private string _tag = "";
        public string tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
                if (tag.ToLower() == "new")
                {
                    IsTagVisible = true;
                }
                else
                {
                    IsTagVisible = false;
                }
            }
        }

        private bool _IsTagVisible = false;
        public bool IsTagVisible
        {
            get
            {
                return _IsTagVisible;
            }
            set
            {
                if (_IsTagVisible != value)
                {
                    _IsTagVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        //武器类型：步枪、近战武器、机枪、狙击枪、霰弹枪、冲锋枪、副武器、投掷武器
        private string _type2 = "步枪";
        public string type2
        {
            get
            {
                return _type2;
            }
            set
            {
                _type2 = value;
                if (_type2 == "散弹枪")
                {
                    _type2 = "霰弹枪";
                }
                WeaponMarkWith = _type2.Length * 10;

                switch (_type2)
                {
                    case "步枪":
                        WeaponMarkColor = "#FF9036";
                        break;
                    case "近战武器":
                        WeaponMarkColor = "#FE492F";
                        break;
                    case "机枪":
                        WeaponMarkColor = "#FFCD06";
                        break;
                    case "狙击枪":
                        WeaponMarkColor = "#18C1A3";
                        break;
                    case "霰弹枪":
                        WeaponMarkColor = "#18A3C1";
                        break;
                    case "冲锋枪":
                        WeaponMarkColor = "#FF6102";
                        break;
                    case "副武器":
                        WeaponMarkColor = "#18C1A3";
                        break;
                    case "投掷武器":
                        WeaponMarkColor = "#B95EFF";
                        break;
                }

            }
        }

        private string _WeaponMarkColor = "#FF9036";

        public string WeaponMarkColor
        {
            get
            {
                return _WeaponMarkColor;
            }
            set
            {
                if (_WeaponMarkColor != value)
                {
                    _WeaponMarkColor = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _WeaponMarkWith = 30;
        public double WeaponMarkWith
        {
            get
            {
                return _WeaponMarkWith;
            }
            set
            {
                if (_WeaponMarkWith != value)
                {
                    _WeaponMarkWith = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}
