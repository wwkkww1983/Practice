using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Api
{
    /// <summary>
    /// API输入参数集合
    /// ljl@2018
    /// </summary>
    public class ApiArgs<T>
    {
        int _page = 1;
        int _rows = 30;
        /// <summary>
        /// 页码（1开始）
        /// </summary>
        public int Page { get { return _page < 1 ? 1 : _page; } set { _page = value; } }
        /// <summary>
        /// 页码大小（默认30）
        /// </summary>
        public int Rows { get { return _rows < 1 ? 30 : _rows; } set { _rows = value; } }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; }
        /// <summary>
        /// 排序方式
        /// </summary>
        public string Order { get; set; }
        /// <summary>
        /// 过滤条件
        /// </summary>
        public List<ApiQueryFilter> Filters { get; set; }
        /// <summary>
        /// 传入对象
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 分页之前where需要过滤的参数集合
        /// </summary>
        public List<string> BeforePagingFilters { get; set; }
        /// <summary>
        /// 遍历Filters后的参数索引值
        /// </summary>
        public int ParamIndex { get; set; }

        System.Text.RegularExpressions.Regex _rxEndOrder = new System.Text.RegularExpressions.Regex("\\s+(desc|asc)\\*$", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        public ApiQueryArgs Query()
        {
            string sql = "";
            List<object> args = new List<object>();
            if (Filters != null)
            {
                List<string> andSqls = new List<string>();
                foreach (var f in Filters)
                {
                    List<string> names = new List<string>();
                    if (!string.IsNullOrEmpty(f.OrNames))
                    {
                        names = f.OrNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                    }
                    else
                    {
                        names.Add(f.Name);
                    }
                    string value = "";
                    if (f.Type == "is null" || f.Type == "is not null")
                    {
                        value = f.Type;
                    }
                    else if (f.Type == "like" || f.Type == "not like")
                    {
                        value = f.Type + " @" + args.Count;
                        args.Add("%" + f.Value + "%");
                    }
                    else if (f.Type == "in" || f.Type == "not in")
                    {
                        value = f.Type + " (@" + args.Count + ")";
                        args.Add(f.Value.Split(','));
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(f.Type)) f.Type = "=";
                        value = f.Type + " @" + args.Count;
                        args.Add(f.Value);
                    }

                    List<string> orSqls = new List<string>();
                    foreach (var name in names)
                    {
                        orSqls.Add($"{name} {value}");
                    }
                    if (orSqls.Count > 1)
                        andSqls.Add("(" + string.Join(" or ", orSqls) + ")");
                    else
                        andSqls.Add(orSqls[0]);
                }
                sql += string.Join(" and ", andSqls);
            }
            if (BeforePagingFilters != null && BeforePagingFilters.Count > 0)
            {
                ParamIndex = args.Count;
                foreach (var str in BeforePagingFilters)
                {
                    args.Add(str);
                }
            }
            return new ApiQueryArgs
            {
                Sql = sql,
                Args = args.ToArray(),
                OrderBy = string.IsNullOrEmpty(Sort) ? "" : _rxEndOrder.IsMatch(Sort) ? Sort : Sort + " " + Order
            };
        }
    }

    public class ApiQueryArgs
    {
        /// <summary>
        /// 查询SQL
        /// </summary>
        public string Sql { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public object[] Args { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public string OrderBy { get; set; }
    }

    /// <summary>
    /// 过滤条件
    /// </summary>
    public class ApiQueryFilter
    {
        /// <summary>
        /// 条件类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 条件字段名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 条件值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// name1 or name2 ... 
        /// </summary>
        public string OrNames { get; set; }
    }

}
