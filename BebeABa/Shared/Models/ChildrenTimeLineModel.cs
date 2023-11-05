using System;

namespace Shared.Models
{
    public class ChildrenTimeLineModel
    {
        public long ChildrenTimeLineId { get; set; }
        public long ChildrenId { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public DateTime TimeLineDate { get; set; }
        public string Vaccine { get; set; }
        public string TreatmentType { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
    }
}
