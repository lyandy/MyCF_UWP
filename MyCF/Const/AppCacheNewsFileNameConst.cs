//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Const
//类名称:       AppCacheNewsFileNameConst
//创建时间:     2015/8/8 星期六 11:43:01
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.Const
{
    public class AppCacheNewsFileNameConst
    {
        #region 情报站
        /// <summary>
        /// 情报站-资讯-分页
        /// </summary>
        public const string CACHE_NEWS_FIRSTNEWS_FILENAME = "News_FirstNews.json";

        /// <summary>
        /// 情报站-赛事-分页
        /// </summary>
        public const string CACHE_NEWS_CHAMPION_FILENAME = "News_Champion.json";

        /// <summary>
        /// 情报站-活动-分页
        /// </summary>
        public const string CACHE_NEWS_ACTIVITY_FILENAME = "News_Activity.json";

        /// <summary>
        /// 情报站-动漫-分页
        /// </summary>
        public const string CACHE_NEWS_CARTOON_FILENAME = "News_Cartoon.json";

        /// <summary>
        /// 情报站-攻略-分页
        /// </summary>
        public const string CACHE_NEWS_STRATEGY_FILENAME = "News_Strategy.json";
        #endregion

        #region 视频库

        #region 视频库-比赛
        /// <summary>
        /// 视频库-比赛-CFPL-分页
        /// </summary>
        public const string CACHE_VIDEO_CHAMPION_CFPL_FILENAME = "Video_Champion_CFPL.json";

        /// <summary>
        /// 视频库-比赛-百城联赛-分页
        /// </summary>
        public const string CACHE_VIDEO_CHAMPION_HUNDREDSOFCITY_FILENAME = "Video_Champion_HundredsOfCity.json";

        /// <summary>
        /// 视频库-比赛-全名联赛-分页
        /// </summary>
        public const string CACHE_VIDEO_CHAMPION_ALLPEOPLE_FILENAME = "Video_Champion_AllPeople.json";

        /// <summary>
        /// 视频库-比赛-CFDL-分页
        /// </summary>
        public const string CACHE_VIDEO_CHAMPION_CFDL_FILENAME = "Video_Champion_CFDL.json";

        /// <summary>
        /// 视频库-比赛-CFS-分页
        /// </summary>
        public const string CACHE_VIDEO_CHAMPION_CFS_FILENAME = "Video_Champion_CFS.json";

        /// <summary>
        /// 视频库-比赛-其他-分页
        /// </summary>
        public const string CACHE_VIDEO_CHAMPION_OTHER_FILENAME = "Video_Champion_Other.json";
        #endregion

        #region 视频库-节目
        /// <summary>
        /// 视频库-节目-大神支招-分页
        /// </summary>
        public const string CACHE_VIDEO_ACT_SUPERMETHOD_FILENAME = "Video_Act_SuperMethod.json";

        /// <summary>
        /// 视频库-节目-大神军火库-分页
        /// </summary>
        public const string CACHE_VIDEO_ACT_SUPERARSENAL_FILENAME = "Video_Act_SuperArsenal.json";

        /// <summary>
        /// 视频库-节目-穿越了没-分页
        /// </summary>
        public const string CACHE_VIDEO_ACT_SUPERTIME_FILENAME = "Video_Act_SuperTime.json";

        /// <summary>
        /// 视频库-节目-开火-分页
        /// </summary>
        public const string CACHE_VIDEO_ACT_FIRE_FILENAME = "Video_Act_Fire.json";

        /// <summary>
        /// 视频库-节目-其他-分页
        /// </summary>
        public const string CACHE_VIDEO_ACT_OTHER_FILENAME = "Video_Act_Other.json";

        /// <summary>
        /// 视频库-节目-火线兄弟-分页
        /// </summary>
        public const string CACHE_VIDEO_ACT_BROTHER_FILENAME = "Video_Act_Brother.json";
        #endregion

        #region 视频库-高手
        /// <summary>
        /// 视频库-高手-TGASTAR-分页
        /// </summary>
        public const string CACHE_VIDEO_SUPER_TGASTAR_FILENAME = "Video_Super_TGASTAR.json";

        /// <summary>
        /// 视频库-高手-职业选手-分页
        /// </summary>
        public const string CACHE_VIDEO_SUPER_PROFRESSIONAL_FILENAME = "Video_Super_Profressional.json";

        /// <summary>
        /// 视频库-高手-民间高手-分页
        /// </summary>
        public const string CACHE_VIDEO_SUPER_PEOPELE_FILENAME = "Video_Super_People.json";

        /// <summary>
        /// 视频库-高手-其他-分页
        /// </summary>
        public const string CACHE_VIDEO_SUPER_OTHER_FILENAME = "Video_Super_Other.json";

        /// <summary>
        /// 视频库-高手-国外选手-分页
        /// </summary>
        public const string CACHE_VIDEO_SUPER_FOREIGNER_FILENAME = "Video_Super_Foreigner.json";
        #endregion

        #region 视频库-教学
        /// <summary>
        /// 视频库-教学-枪械教学-分页
        /// </summary>
        public const string CACHE_VIDEO_TEACH_GUN_FILENAME = "Video_Teach_Gun.json";

        /// <summary>
        /// 视频库-教学-地图教学-分页
        /// </summary>
        public const string CACHE_VIDEO_TEACH_MAP_FILENAME = "Video_Teach_Map.json";

        /// <summary>
        /// 视频库-教学-技巧教学-分页
        /// </summary>
        public const string CACHE_VIDEO_TEACH_SKILL_FILENAME = "Video_Teach_Skill.json";

        /// <summary>
        /// 视频库-教学-其他-分页
        /// </summary>
        public const string CACHE_VIDEO_TEACH_OTHER_FILENAME = "Video_Teach_Other.json";
        #endregion

        #region 视频库-官方
        /// <summary>
        /// 视频库-官方-宣传片-分页
        /// </summary>
        public const string CACHE_VIDEO_OFFICIAL_VCR_FILENAME = "Video_Official_VCR.json";

        /// <summary>
        /// 视频库-官方-游戏CG-分页
        /// </summary>
        public const string CACHE_VIDEO_OFFICIAL_GAMECG_FILENAME = "Video_Official_GameCG.json";

        /// <summary>
        /// 视频库-官方-版本视频-分页
        /// </summary>
        public const string CACHE_VIDEO_OFFICIAL_VERSION_FILENAME = "Video_Official_Version.json";

        /// <summary>
        /// 视频库-官方-其他-分页
        /// </summary>
        public const string CACHE_VIDEO_OFFICIAL_OTHER_FILENAME = "Video_Official_Other.json";
        #endregion

        #region 视频库-娱乐
        /// <summary>
        /// 视频库-娱乐-搞笑视频-分页
        /// </summary>
        public const string CACHE_VIDEO_PLAY_FUN_FILENAME = "Video_Play_Fun.json";

        /// <summary>
        /// 视频库-娱乐-游戏电影-分页
        /// </summary>
        public const string CACHE_VIDEO_PLAY_MOVIE_FILENAME = "Video_Play_Movie.json";

        /// <summary>
        /// 视频库-娱乐-游戏翻唱-分页
        /// </summary>
        public const string CACHE_VIDEO_PLAY_SONG_FILENAME = "Video_Play_Song.json";
        #endregion

        #endregion


        #region 火线百科
        public const string CACHE_ENCY_WEAPONS_FILENAME = "Ency_Weapons.json";
        #endregion

        #region 地图点位
        public const string CACHE_MAP_MAPLIST_FILENAME = "Map_MapList.json";

        public static string CACHE_MAP_DETAIL_ID_FILENAME
        {
            get
            {
                return "Map_Detail_" + DicStore.GetValueOrDefault<int>(AppCommonConst.CURRENT_MAP_ID, 1) + ".json";
            }
        }

        #endregion
    }
}
