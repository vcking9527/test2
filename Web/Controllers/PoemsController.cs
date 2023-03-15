using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ES.Business.IService;
using ES.Models;
using ES.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ES.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PoemsController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public IFamousPoemsService famousPoemsService { get; set; }

        /// <summary>
        /// 插入单条数据
        /// </summary>
        /// <param name="poems"></param>
        /// <returns></returns>
        [HttpPost("Poems/InsertAsync")]
        public async Task<IActionResult> InsertAsync([FromBody]FamousPoemsModel poems)
        {
            return Content(JsonConvert.SerializeObject(await famousPoemsService.InsertAsync(poems)));
        }
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="poems"></param>
        /// <returns></returns>
        [HttpPost("Poems/InsertRangeAsync")]
        public async Task<ActionResult> InsertRangeAsync([FromBody]List<FamousPoemsModel> poems)
        {
            return Content(JsonConvert.SerializeObject(await famousPoemsService.InsertRangeAsync(poems)));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("Poems/DeleteAsync")]
        public async Task<ActionResult> DeleteAsync(string Id)
        {
            return Content(JsonConvert.SerializeObject(await famousPoemsService.DeleteAsync(Id)));
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="vcTitle"></param>
        /// <returns></returns>
        [HttpPost("Poems/UpdateAsync")]
        public async Task<ActionResult> UpdateAsync(string Id, string vcTitle)
        {
            return Content(JsonConvert.SerializeObject(await famousPoemsService.UpdateAsync(Id, vcTitle)));
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="vcAuthor"></param>
        /// <returns></returns>
        [HttpPost("Poems/UpdateByAuthorAsync")]
        public async Task<ActionResult> UpdateByAuthorAsync(string vcAuthor)
        {
            return Content(JsonConvert.SerializeObject(await famousPoemsService.UpdateByAuthorAsync(vcAuthor)));
        }
        /// <summary>
        /// 根据相关条件获取分页数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(FamousPoemsModel), 200)]
        [HttpPost("Poems/GetPageAsync")]
        public async Task<ActionResult> GetPageAsync([FromBody]ParameterModel dto)
        {
            return Content(JsonConvert.SerializeObject(await famousPoemsService.GetPageAsync(dto)));
        }
    }
}