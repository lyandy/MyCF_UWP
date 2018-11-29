//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Api.ApiRoot
//类名称:       ApiVideoRoot
//创建时间:     2015/8/14 星期五 11:56:23
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Base;
using MyCF.Config;
using MyCF.Const;
using MyCF.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.Api.ApiRoot
{
    public class ApiVideoRoot : ClassBase<ApiVideoRoot>
    {
        public ApiVideoRoot() : base() { }

        private static int pageSize = AppEnvironment.IsPhone ? 20 : 30;

        private string videoChampionBaseUrl = "http://apps.game.qq.com/cf/wmp/search.php?pagesize=" + pageSize + "&p0=cf&p1=2";
        private string videoActBaseUrl = "http://apps.game.qq.com/cf/wmp/search.php?pagesize=" + pageSize + "&p0=cf&p1=3";
        private string videoSuperBaseUrl = "http://apps.game.qq.com/cf/wmp/search.php?pagesize=" + pageSize + "&p0=cf&p1=6";
        private string videoTeachBaseUrl = "http://apps.game.qq.com/cf/wmp/search.php?pagesize=" + pageSize + "&p0=cf&p1=5";
        private string videoOfficialBaseUrl = "http://apps.game.qq.com/cf/wmp/search.php?pagesize=" + pageSize + "&p0=cf&p1=1";
        private string videoPlayBaseUrl = "http://apps.game.qq.com/cf/wmp/search.php?pagesize=" + pageSize + "&p0=cf&p1=4";

        #region 比赛
        public string VideoChampionCFPLUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_CHAMPION_CFPL_PAGE_INDEX_CONST, 1);
                return videoChampionBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoChampionEnum.CFPL;
            }
        }

        public string VideoChampionHundredsOfCityUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_CHAMPION_HUNDREDSOFCITY_PAGE_INDEX_CONST, 1);
                return videoChampionBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoChampionEnum.HUNDREDSOFCITY;
            }
        }

        public string VideoChampionAllPeopleUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_CHAMPION_ALLPEOPLE_PAGE_INDEX_CONST, 1);
                return videoChampionBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoChampionEnum.ALLPEOPLE;
            }
        }

        public string VideoChampionCFDLUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_CHAMPION_CFDL_PAGE_INDEX_CONST, 1);
                return videoChampionBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoChampionEnum.CFDL;
            }
        }

        public string VideoChampionCFSUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_CHAMPION_CFS_PAGE_INDEX_CONST, 1);
                return videoChampionBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoChampionEnum.CFS;
            }
        }

        public string VideoChampionOtherUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_CHAMPION_OTHER_PAGE_INDEX_CONST, 1);
                return videoChampionBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoChampionEnum.OTHER;
            }
        }

        public enum VideoChampionEnum
        {
            CFPL = 10,
            HUNDREDSOFCITY = 11,
            ALLPEOPLE = 12,
            CFDL = 13,
            CFS = 14,
            OTHER = 39
        }

        #endregion

        #region 节目

        public string VideoActSuperMehtodUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_ACT_SUPERMETHOD_PAGE_INDEX_CONST, 1);
                return videoActBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoActEnum.SUPERMETHOD;
            }
        }

        public string VideoActSuperRsenalUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_ACT_SUPERARSENAL_PAGE_INDEX_CONST, 1);
                return videoActBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoActEnum.SUPERARSENAL;
            }
        }

        public string VideoActSuperTimeUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_ACT_SUPERTIME_PAGE_INDEX_CONST, 1);
                return videoActBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoActEnum.SUPERTIME;
            }
        }

        public string VideoActFireUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_ACT_FIRE_PAGE_INDEX_CONST, 1);
                return videoActBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoActEnum.FIRE;
            }
        }

        public string VideoActOtherUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_ACT_OTHER_PAGE_INDEX_CONST, 1);
                return videoActBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoActEnum.OTHER;
            }
        }

        public string VideoActBrotherUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_ACT_BROTHER_PAGE_INDEX_CONST, 1);
                return videoActBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoActEnum.BROTHER;
            }
        }

        public enum VideoActEnum
        {
            SUPERMETHOD = 24,
            SUPERARSENAL = 25,
            SUPERTIME = 26,
            FIRE = 27,
            OTHER = 40,
            BROTHER = 56
        }

        #endregion

        #region 高手

        public string VideoSuperTGASTARUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_SUPER_TGASTAR_PAGE_INDEX_CONST, 1);
                return videoSuperBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoSuperEnum.TGASTAR;
            }
        }

        public string VideoSuperProfressionalUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_SUPER_PROFRESSIONAL_PAGE_INDEX_CONST, 1);
                return videoSuperBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoSuperEnum.PROFRESSIONAL;
            }
        }

        public string VideoSuperPeopleUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_SUPER_PEOPELE_PAGE_INDEX_CONST, 1);
                return videoSuperBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoSuperEnum.PEOPLE;
            }
        }

        public string VideoSuperOtherUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_SUPER_OTHER_PAGE_INDEX_CONST, 1);
                return videoSuperBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoSuperEnum.OTHER;
            }
        }

        public string VideoSuperForeignerUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_SUPER_FOREIGNER_PAGE_INDEX_CONST, 1);
                return videoSuperBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoSuperEnum.FOREIGNER;
            }
        }

        public enum VideoSuperEnum
        {
            TGASTAR = 18,
            PROFRESSIONAL = 19,
            PEOPLE = 20,
            OTHER = 43,
            FOREIGNER = 62
        }

        #endregion

        #region 教学

        public string VideoTeachGunUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_TEACH_GUN_PAGE_INDEX_CONST, 1);
                return videoTeachBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoTeachEnum.GUN;
            }
        }

        public string VideoTeachMapUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_TEACH_MAP_PAGE_INDEX_CONST, 1);
                return videoTeachBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoTeachEnum.MAP;
            }
        }

        public string VideoTeachSkillUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_TEACH_SKILL_PAGE_INDEX_CONST, 1);
                return videoTeachBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoTeachEnum.SKILL;
            }
        }

        public string VideoTeachOtherUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_TEACH_OTHER_PAGE_INDEX_CONST, 1);
                return videoTeachBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoTeachEnum.OTHER;
            }
        }

        public enum VideoTeachEnum
        {
            GUN = 15,
            MAP = 16,
            SKILL = 17,
            OTHER = 41
        }

        #endregion

        #region 官方

        public string VideoOfficialVcrUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_OFFICIAL_VCR_PAGE_INDEX_CONST, 1);
                return videoOfficialBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoOfficialEnum.VCR;
            }
        }

        public string VideoOfficialGameCGUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_OFFICIAL_GAMECG_PAGE_INDEX_CONST, 1);
                return videoOfficialBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoOfficialEnum.GAMECG;
            }
        }

        public string VideoOfficialVersionUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_OFFICIAL_VERSION_PAGE_INDEX_CONST, 1);
                return videoOfficialBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoOfficialEnum.VERSION;
            }
        }

        public string VideoOfficialOtherUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_OFFICIAL_OTHER_PAGE_INDEX_CONST, 1);
                return videoOfficialBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoOfficialEnum.OTHER;
            }
        }

        public enum VideoOfficialEnum
        {
            VCR = 7,
            GAMECG = 8,
            VERSION = 9,
            OTHER = 38
        }

        #endregion

        #region 娱乐

        public string VideoPlayFunUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_PLAY_FUN_PAGE_INDEX_CONST, 1);
                return videoPlayBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoPlayEnum.FUN;
            }
        }

        public string VideoPlayMovieUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_PLAY_MOVIE_PAGE_INDEX_CONST, 1);
                return videoPlayBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoPlayEnum.MOVIE;
            }
        }

        public string VideoPlaySongUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.VIDEO_PLAY_SONG_PAGE_INDEX_CONST, 1);
                return videoPlayBaseUrl + "&page=" + pageIndex + "&p2=" + (int)VideoPlayEnum.SONG;
            }
        }

        public enum VideoPlayEnum
        {
            FUN = 21,
            MOVIE = 22,
            SONG = 23
        }
        #endregion
    }
}
