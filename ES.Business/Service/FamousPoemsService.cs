using ES.Business.IService;
using ES.Core;
using ES.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.Business.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class FamousPoemsService : IFamousPoemsService, ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IEsUpdateService _updateService;
        /// <summary>
        /// 
        /// </summary>
        private readonly IEsClientService _clientService;
        /// <summary>
        /// 
        /// </summary>
        private readonly IEsIndexService _indexService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateService"></param>
        /// <param name="clientService"></param>
        /// <param name="indexService"></param>
        public FamousPoemsService(IEsUpdateService updateService, IEsClientService clientService, IEsIndexService indexService)
        {
            _updateService = updateService;
            _clientService = clientService;
            _indexService = indexService;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<long> DeleteAsync(string Id)
        {
            var mustFilters = new List<Func<QueryContainerDescriptor<FamousPoemsModel>, QueryContainer>>();
            mustFilters.Add(t => t.Match(m => m.Field(f => f.Id).Query(Id)));

            var result = await _clientService.Client.DeleteByQueryAsync<FamousPoemsModel>(x => x.Index("famouspoemsmodel")
            .Query(q => q.Bool(b => b.Must(mustFilters))));

            return result.Deleted;
        }
        /// <summary>
        /// 根据相关条件获取分页数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageModel<FamousPoemsModel>> GetPageAsync(ParameterModel dto)
        {
            PageModel<FamousPoemsModel> pageModel = new PageModel<FamousPoemsModel>();
            var mustFilters = new List<Func<QueryContainerDescriptor<FamousPoemsModel>, QueryContainer>>();

            if (!string.IsNullOrEmpty(dto.vcTitle))
            {
                mustFilters.Add(t => t.Match(m => m.Field(f => f.vcTitle).Query(dto.vcTitle)));
            }

            if (dto.authors.Count() > 0)
            {
                mustFilters.Add(t => t.Terms(m => m.Field(f => f.vcAuthor).Terms(dto.authors.ToArray())));
            }

            var total = await _clientService.Client.CountAsync<FamousPoemsModel>(x => x.Index("famouspoemsmodel")
             .Query(q => q.Bool(b => b.Must(mustFilters))));

            if (total.Count == 0)
            {
                return pageModel;
            }

            pageModel.nDataCount = Convert.ToInt32(total.Count);
            pageModel.nPageCount = (int)Math.Ceiling((double)pageModel.nDataCount / dto.nPageSize);
            dto.nPageIndex = ((dto.nPageIndex < 1 ? 1 : dto.nPageIndex) - 1) * dto.nPageSize;

            var data = await _clientService.Client.SearchAsync<FamousPoemsModel>(x => x.Index("famouspoemsmodel")
     .Query(q => q.Bool(b => b.Must(mustFilters))).Sort(s => s.Ascending(d => d.dtCreateTime)).From(dto.nPageIndex).Size(dto.nPageSize));

            pageModel.data = (List<FamousPoemsModel>)data.Documents;
            return pageModel;
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="poems"></param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(FamousPoemsModel poems)
        {
            return await _indexService.InsertAsync(poems);
        }
        /// <summary>
        /// 新增批量数据
        /// </summary>
        /// <param name="poems"></param>
        /// <returns></returns>
        public async Task<bool> InsertRangeAsync(List<FamousPoemsModel> poems)
        {
            return await _indexService.InsertRangeAsync(poems);
        }
        /// <summary>
        /// 通过主键局部更新数据
        /// </summary>
        /// <param name="nId"></param>
        /// <param name="vcTitle"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(string nId, string vcTitle)
        {
            FamousPoemsModel dto = new FamousPoemsModel();
            dto.vcTitle = vcTitle;
            dto.dtUpdateTime = DateTime.Now;
            var result = _updateService.Update<FamousPoemsModel>(nId, dto);
            return result.IsValid;
        }
        /// <summary>
        /// 通过条件批量局部更新某个字段数据
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateByAuthorAsync(string vcAuthor)
        {
            var mustFilters = new List<Func<QueryContainerDescriptor<FamousPoemsModel>, QueryContainer>>();
            mustFilters.Add(t => t.Match(m => m.Field(f => f.vcAuthor).Query(vcAuthor)));

            var result = await _clientService.Client.UpdateByQueryAsync<FamousPoemsModel>(q => q.Index("famouspoemsmodel")
   .Query(q => q.Bool(t => t.Must(mustFilters)))
.Script(script => script.Source("ctx._source.dtUpdateTime=" + DateTime.Now + ";")));

            return result.IsValid;

        }
    }
}
