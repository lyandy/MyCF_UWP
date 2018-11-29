//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.ViewModel.Video
//类名称:       VideoCategoryViewModel
//创建时间:     2015/8/9 星期日 17:56:35
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Model.Video;
using MyCF.View.Video;
using MyCF.ViewModelAttribute.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.ViewModel.Video
{
    public class VideoCategoryViewModel : VideoCategoryViewModelAttribute
    { 
        /// <summary>
        /// 获取视频类别集合
        /// </summary>
        public void InitVideoCategory()
        {
            //比赛
            VideoCategoryChampionCollection.Clear();
            VideoCategoryChampionCollection.Add(new VideoCategoryModel() { CategoryName = "CFPL", color= "#7022B14C", ClassType=typeof(ChampionPage) });
            VideoCategoryChampionCollection.Add(new VideoCategoryModel() { CategoryName = "百城联赛", color = "#7022B14C", ClassType = typeof(ChampionPage) });
            VideoCategoryChampionCollection.Add(new VideoCategoryModel() { CategoryName = "全民联赛", color = "#7022B14C", ClassType = typeof(ChampionPage) });
            VideoCategoryChampionCollection.Add(new VideoCategoryModel() { CategoryName = "CFDL", color = "#7022B14C", ClassType = typeof(ChampionPage) });
            VideoCategoryChampionCollection.Add(new VideoCategoryModel() { CategoryName = "CFS", color = "#7022B14C", ClassType = typeof(ChampionPage) });
            VideoCategoryChampionCollection.Add(new VideoCategoryModel() { CategoryName = "其他", color = "#7022B14C", ClassType = typeof(ChampionPage) });

            //节目
            VideoCategoryActCollection.Clear();
            VideoCategoryActCollection.Add(new VideoCategoryModel() { CategoryName = "大神支招", color = "#70FF0000", ClassType = typeof(ActPage) });
            VideoCategoryActCollection.Add(new VideoCategoryModel() { CategoryName = "大神军火库", color = "#70FF0000", ClassType = typeof(ActPage) });
            VideoCategoryActCollection.Add(new VideoCategoryModel() { CategoryName = "穿越了没", color = "#70FF0000", ClassType = typeof(ActPage) });
            VideoCategoryActCollection.Add(new VideoCategoryModel() { CategoryName = "开火", color = "#70FF0000", ClassType = typeof(ActPage) });
            VideoCategoryActCollection.Add(new VideoCategoryModel() { CategoryName = "其他", color = "#70FF0000", ClassType = typeof(ActPage) });
            VideoCategoryActCollection.Add(new VideoCategoryModel() { CategoryName = "火线兄弟", color = "#70FF0000", ClassType = typeof(ActPage) });

            //教学
            VideoCategoryTeachCollection.Clear();
            VideoCategoryTeachCollection.Add(new VideoCategoryModel() { CategoryName = "枪械教学", color = "#705522B1", ClassType = typeof(TeachPage) });
            VideoCategoryTeachCollection.Add(new VideoCategoryModel() { CategoryName = "地图教学", color = "#705522B1", ClassType = typeof(TeachPage) });
            VideoCategoryTeachCollection.Add(new VideoCategoryModel() { CategoryName = "技巧教学", color = "#705522B1", ClassType = typeof(TeachPage) });
            VideoCategoryTeachCollection.Add(new VideoCategoryModel() { CategoryName = "其他", color = "#705522B1", ClassType = typeof(TeachPage) });

            //高手
            VideoCategorySuperCollection.Clear();
            VideoCategorySuperCollection.Add(new VideoCategoryModel() { CategoryName = "TGASTAR", color = "#700B6CF8", ClassType = typeof(SuperPage) });
            VideoCategorySuperCollection.Add(new VideoCategoryModel() { CategoryName = "职业选手", color = "#700B6CF8", ClassType = typeof(SuperPage) });
            VideoCategorySuperCollection.Add(new VideoCategoryModel() { CategoryName = "民间高手", color = "#700B6CF8", ClassType = typeof(SuperPage) });
            VideoCategorySuperCollection.Add(new VideoCategoryModel() { CategoryName = "其他", color = "#700B6CF8", ClassType = typeof(SuperPage) });
            VideoCategorySuperCollection.Add(new VideoCategoryModel() { CategoryName = "国外选手", color = "#700B6CF8", ClassType = typeof(SuperPage) });

            //官方
            VideoCategoryOfficialCollection.Clear();
            VideoCategoryOfficialCollection.Add(new VideoCategoryModel() { CategoryName = "宣传片", color = "#70A99C02", ClassType = typeof(OfficialPage) });
            VideoCategoryOfficialCollection.Add(new VideoCategoryModel() { CategoryName = "游戏CG", color = "#70A99C02", ClassType = typeof(OfficialPage) });
            VideoCategoryOfficialCollection.Add(new VideoCategoryModel() { CategoryName = "版本视频", color = "#70A99C02", ClassType = typeof(OfficialPage) });
            VideoCategoryOfficialCollection.Add(new VideoCategoryModel() { CategoryName = "其他", color = "#70A99C02", ClassType = typeof(OfficialPage) });

            //娱乐
            VideoCategoryPlayCollection.Clear();
            VideoCategoryPlayCollection.Add(new VideoCategoryModel() { CategoryName = "搞笑视频", color = "#70CD7B55", ClassType = typeof(PlayPage) });
            VideoCategoryPlayCollection.Add(new VideoCategoryModel() { CategoryName = "游戏电影", color = "#70CD7B55", ClassType = typeof(PlayPage) });
            VideoCategoryPlayCollection.Add(new VideoCategoryModel() { CategoryName = "游戏翻唱", color = "#70CD7B55", ClassType = typeof(PlayPage) });
        }
    }
}
