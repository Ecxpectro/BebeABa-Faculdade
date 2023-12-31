﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DB.Models
{
    public partial class Children
    {
        public Children()
        {
            ChildrenTimeLine = new HashSet<ChildrenTimeLine>();
        }

        public long ChildrenId { get; set; }
        public long UserId { get; set; }
        public string ChildrenName { get; set; }
        public string ImgPath { get; set; }
        public string ChildrenFatherName { get; set; }
        public string ChildrenMotherName { get; set; }
        public DateTime BirthDate { get; set; }
        public int? ChildSex { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<ChildrenTimeLine> ChildrenTimeLine { get; set; }
    }
}