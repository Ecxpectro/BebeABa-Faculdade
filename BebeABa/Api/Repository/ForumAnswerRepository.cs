using Api.Repository.Interfaces;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Api.Repository
{
    public class ForumAnswerRepository : IForumAnswerRepository
    {
        private readonly BebeaBaContext _context;

        public ForumAnswerRepository(BebeaBaContext context)
        {
            _context = context;
        }
        public async Task<ForumAnswer> CreateAnswer(ForumAnswer forumAnswer, long mainForumId)
        {
            if (forumAnswer != null && mainForumId > 0) 
            {
             
                await _context.ForumAnswer.AddAsync(forumAnswer);
                await _context.SaveChangesAsync();
                ForumRelation relation = new ForumRelation
                {
                    ForumRelationId = 0,
                    MainForumId = mainForumId,
                    ForumAnswerId = forumAnswer.ForumAnswerId
                };
                await CreateRelation(relation);
                
                return forumAnswer;
            }
            return null;
        }

        public async Task CreateRelation(ForumRelation relation)
        {
            await _context.ForumRelation.AddAsync(relation);
            await _context.SaveChangesAsync();
        }
    }
}
