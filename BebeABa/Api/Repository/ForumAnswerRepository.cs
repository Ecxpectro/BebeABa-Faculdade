using Api.Repository.Interfaces;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<bool> DeleteAnswer(ForumAnswer forumAnswer)
        {
            bool isOk = false;
            try
            {
                if (forumAnswer is not null)
                {
                    await DeleteRelationById(forumAnswer.ForumAnswerId);
                    _context.ForumAnswer.Remove(forumAnswer);
                    isOk = true;
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception)
            {
            }
            return isOk;
        }
        public async Task DeleteRelationById(long relationId)
        {

            var relationToDelete = await _context.ForumRelation.FirstOrDefaultAsync(x => x.ForumAnswerId == relationId);

            if (relationToDelete != null)
            {
                _context.ForumRelation.Remove(relationToDelete);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<ForumAnswer> GetById(long id) => await _context.ForumAnswer.FirstOrDefaultAsync(x => x.ForumAnswerId == id);
    }
}
