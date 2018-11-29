//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Helper
//类名称:       RandomAnimationHelper
//创建时间:     2015/8/16 星期日 12:06:00
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using Brain.Animate;
using MyCF.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.Helper
{
    public class RandomAnimationHelper : ClassBase<RandomAnimationHelper>
    {
        public RandomAnimationHelper() : base() { }

        public AnimationDefinition GetAnimation()
        {
            AnimationDefinition animationDefinition = null;

            var randomIndex = new Random().Next(0, 10);

            switch (randomIndex)
            {
                case 0:
                    animationDefinition = new FadeInUpAnimation();
                    break;
                case 1:
                    animationDefinition = new BounceInLeftAnimation();
                    break;
                case 2:
                    animationDefinition = new BounceInUpAnimation();
                    break;
                case 3:
                    animationDefinition = new BounceInDownAnimation();
                    break;
                case 4:
                    animationDefinition = new FadeInDownAnimation();
                    break;
                case 5:
                    animationDefinition = new FadeInLeftAnimation();
                    break;
                case 6:
                    animationDefinition = new FadeInRightAnimation();
                    break;
                case 7:
                    animationDefinition = new BounceInRightAnimation();
                    break;
                case 8:
                    animationDefinition = new FadeInAnimation();
                    break;
                case 9:
                    animationDefinition = new BounceInAnimation();
                    break;
            }

            return animationDefinition;
        }
    }
}
