using System;

namespace ES.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class FamousPoemsModel
    {
        /// <summary>
        /// 
        /// </summary>
        public FamousPoemsModel()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        [Nest.Keyword]
        public string vcAuthor { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Nest.Keyword]
        public string vcContent { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Nest.Keyword]
        public string vcTitle { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime dtCreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime dtUpdateTime { get; set; }
    }
}
