using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAspNetCoreCQRS.Business.Criterias
{
    public abstract class BaseCriteria
    {
        public string SearchText { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
    }
}
