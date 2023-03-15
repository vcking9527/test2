using System;
using System.Collections.Generic;
using System.Text;

namespace ES.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageModel<T>
    {
        /// <summary>
        /// 当前页标
        /// </summary>
        public int nPage { get; set; } = 1;
        /// <summary>
        /// 总页数
        /// </summary>
        public int nPageCount { get; set; } = 0;
        /// <summary>
        /// 数据总数
        /// </summary>
        public int nDataCount { get; set; } = 0;
        /// <summary>
        /// 每页大小
        /// </summary>
        public int nPageSize { set; get; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public List<T> data { get; set; }
    }
}
