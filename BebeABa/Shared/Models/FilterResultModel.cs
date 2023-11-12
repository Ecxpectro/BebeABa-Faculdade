using System.Collections.Generic;

namespace Shared.Models
{
    public class FilterResultModel<T>
    {
        public PagerModel Pager { get; set; }
        public List<T> List { get; set; }
    }
}
