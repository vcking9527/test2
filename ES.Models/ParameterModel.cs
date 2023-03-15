using System;
using System.Collections.Generic;
using System.Text;

namespace ES.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ParameterModel
    {
        /// <summary>
        /// 作者集合
        /// </summary>
        public List<string> authors { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string vcTitle { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int nPageIndex { get; set; } = 1;
        /// <summary>
        /// 每页数据
        /// </summary>
        public int nPageSize { get; set; } = 10;
    }
}
