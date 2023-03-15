using ES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ES.Business.IService
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFamousPoemsService
    {
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="poems"></param>
        /// <returns></returns>
        Task<bool> InsertAsync(FamousPoemsModel poems);
        /// <summary>
        /// 新增批量数据
        /// </summary>
        /// <param name="poems"></param>
        /// <returns></returns>
        Task<bool> InsertRangeAsync(List<FamousPoemsModel> poems);
        /// <summary>
        /// 根据相关条件获取分页数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PageModel<FamousPoemsModel>> GetPageAsync(ParameterModel dto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="vcTitle"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(string Id, string vcTitle);
        /// <summary>
        /// 批量更新创建时间
        /// </summary>
        /// <returns></returns>
        Task<bool> UpdateByAuthorAsync(string vcAuthor);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<long> DeleteAsync(string Id);
    }
}
