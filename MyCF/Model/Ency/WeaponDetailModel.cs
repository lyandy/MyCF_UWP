//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Model.Ency
//类名称:       WeaponDetailModel
//创建时间:     2015/8/17 星期一 20:33:18
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.Model.Ency
{
    public class WeaponDetailModel
    {
        //唯一id
        public string id { get; set; }
        //枪械类型
        public string type2 { get; set; }
        //子弹数
        public string bullet_num { get; set; }
        //武器描述
        public string goods_desc { get; set; }
        //距离
        public string distance { get; set; }
        //范围
        public string field { get; set; }
        //威力
        public string power { get; set; }
        //准确度
        public string accuracy { get; set; }
        //射速
        public string speed { get; set; }
        //这个用来计算稳定性
        public string recoil { get; set; }
        //重量
        public string weight { get; set; }
        //枪名称
        public string name { get; set; }
        //武器购买类型：CF点、CG点、FP点
        public string cointype { get; set; }
        //武器代码
        public string code { get; set; }
        //弹匣数，一梭子弹多少发
        public string magazine { get; set; }
        //标签，保留字段
        public string tag { get; set; }

        private List<Reattr> _reattr = new List<Reattr>();
        public List<Reattr> reattr
        {
            get
            {
                return _reattr;
            }
            set
            {
                _reattr = value;

                _reattr.ForEach(o => o.coinType = cointype);
            }
        }
    }

    public class Reattr
    {
        //武器购买类型：CF点、CG点、FP点
        public string coinType { get; set; }
        //购买仓库id
        public string goods_id { get; set; }
        //价格
        public string price { get; set; }
        //过期天数
        public string expire { get; set; }
    }

}
