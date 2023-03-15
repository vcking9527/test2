using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ES.Web.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class MonitorTicketFilterAttribute : IAsyncActionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public ILogger<MonitorTicketFilterAttribute> logger { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public MonitorTicketFilterAttribute(ILogger<MonitorTicketFilterAttribute> logger)
        {
            this.logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            DateTime startTime = DateTime.Now;

            await next();
            MonitorLog log = new MonitorLog(
                 context.ActionDescriptor.DisplayName,
                 startTime,
                 DateTime.Now,
                 context.HttpContext.User.Claims,
                 context.ActionArguments,
                 context.Result
                 );

            logger.LogInformation(log.ToString());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MonitorLog
    {
        /// <summary>
        /// 
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ExecuteStartTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ExecuteEndTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, object> Claims { get; set; } = new Dictionary<string, object>();
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, object> Args { get; set; } = new Dictionary<string, object>();
        /// <summary>
        /// 
        /// </summary>
        public object Response { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MonitorLog(string actionName, DateTime startTime, DateTime endTime, IEnumerable<Claim> claims, IDictionary<string, object> args, object response)
        {
            this.ActionName = actionName;
            this.ExecuteStartTime = startTime;
            this.ExecuteEndTime = endTime;
            this.Response = response;
            if (args != null)
                args.ToList().ForEach(a => this.Args.Add(a.Key, a.Value ?? ""));
            if (claims != null)
            {
                claims.ToList().ForEach(a => this.Claims.Add(a.Type, a.Value ?? ""));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string ToString()
        {
            return $@"
    API 监控：
    action：{ActionName}
    时间：{ExecuteStartTime.ToString("yyyy-MM-dd HH:mm:ss.fff")} ~ {ExecuteEndTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}
    耗时：{(ExecuteEndTime - ExecuteStartTime).TotalMilliseconds}
    商家：{JsonConvert.SerializeObject(Claims)}
    参数：{JsonConvert.SerializeObject(Args)}
    响应：{JsonConvert.SerializeObject(Response)}";
        }

    }

}
