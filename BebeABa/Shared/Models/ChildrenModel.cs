using System;

namespace Shared.Models
{
    public class ChildrenModel
    {
        public long ChildrenId { get; set; }
        public long UserId { get; set; }
        public string ChildrenName { get; set; }
        public string ImgPath { get; set; }
        public string ChildrenFatherName { get; set; }
        public string ChildrenMotherName { get; set; }
        public DateTime BirthDate { get; set; }
        public int ChildSex { get; set; }
    }
}
