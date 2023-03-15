using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ES.Business.ElasticSearch.Mapping;
using Nest;

namespace ES.Business.ElasticSearch
{
    /// <summary>
    /// 
    /// </summary>
    public class ExpressionContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mappingIndex"></param>
        public ExpressionContext(MappingIndex mappingIndex)
        {
            Mapping = mappingIndex;
        }
        /// <summary>
        /// 
        /// </summary>
        private MappingIndex Mapping { get; }
        /// <summary>
        /// 
        /// </summary>
        public QueryContainer QueryContainer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ExpressionType LastOperator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastFiled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object LastValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public QueryBase LastQueryBase { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void SetQuery()
        {
            HandleField();

            switch (LastQueryBase)
            {
                case TermQuery termQuery:
                    termQuery.Field = LastFiled;
                    termQuery.Value = LastValue;
                    break;
                case BoolQuery boolQuery:
                    boolQuery.MustNot = new List<QueryContainer>
                    {
                        new TermQuery
                        {
                            Field = LastFiled,
                            Value = LastValue
                        }
                    };
                    break;
                case TermRangeQuery termRangeQuery:
                    termRangeQuery.Field = LastFiled;
                    termRangeQuery.GreaterThan = !string.IsNullOrWhiteSpace(termRangeQuery.GreaterThan) ? LastValue.ToString() : null;
                    termRangeQuery.GreaterThanOrEqualTo = !string.IsNullOrWhiteSpace(termRangeQuery.GreaterThanOrEqualTo) ? LastValue.ToString() : null;
                    termRangeQuery.LessThan = !string.IsNullOrWhiteSpace(termRangeQuery.LessThan) ? LastValue.ToString() : null;
                    termRangeQuery.LessThanOrEqualTo = !string.IsNullOrWhiteSpace(termRangeQuery.LessThanOrEqualTo) ? LastValue.ToString() : null;
                    break;
                case MatchPhraseQuery matchPhraseQuery:
                    matchPhraseQuery.Field = LastFiled;
                    matchPhraseQuery.Query = LastValue.ToString();
                    break;
                case QueryStringQuery queryStringQuery:
                    queryStringQuery.Fields = new[] { LastFiled };
                    queryStringQuery.Query = "*" + LastValue + "*";
                    break;
            }

            if (QueryContainer == null)
            {
                QueryContainer = LastQueryBase;
            }
            else
            {
                switch (LastOperator)
                {
                    case ExpressionType.And:
                    case ExpressionType.AndAlso:
                        QueryContainer &= LastQueryBase;
                        break;
                    case ExpressionType.Or:
                    case ExpressionType.OrElse:
                        QueryContainer |= LastQueryBase;
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void HandleField()
        {
            LastFiled = Mapping.Columns.FirstOrDefault(x => x.PropertyName == LastFiled)?.SearchName ?? LastFiled;
        }
    }
}