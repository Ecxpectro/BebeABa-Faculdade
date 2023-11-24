using System;

namespace Shared.Models
{
    public class ForumAnswerModel
    {
        public long ForumAnswerId { get; set; }
        public long UserId { get; set; }
        public string ForumAnswer1 { get; set; }
        public DateTime? ForumAnswerDate { get; set; }
        public UserModel User { get; set; }


        //essa propriedade se utiliza para guardar a relação entre tabelas
        public long MainForumId { get; set; }
    }
}
