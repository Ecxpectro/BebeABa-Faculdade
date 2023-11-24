using System;
using System.Collections.Generic;

namespace Shared.Models
{
	public class MainForumModel
	{
		public long MainForumId { get; set; }
		public long UserId { get; set; }
		public string MainForumTitle { get; set; }
		public string MainForumMessage { get; set; }
		public DateTime? MainForumDate { get; set; }

        public List<ForumAnswerModel> ForumAnswers { get; set; }
        public UserModel User { get; set; }
    }
}
