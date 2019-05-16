using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CE.Model
{
    public class CeConfig
    {
        /// <summary>
        /// 当找到不指定的程序时弹出功能框
        /// </summary>
        public bool WhenNotFoundShowTip { get; set; }

        /// <summary>
        /// 推荐的相似程度
        /// </summary>
        public decimal FindAppSimilarityPercent { get; set; }

        /// <summary>
        /// 快捷键
        /// </summary>
        public string Shortcutkey { get; set; }

        /// <summary>
        /// 是否检查更新
        /// </summary>
        public bool CheckUpdate { get; set; }
    }
}
