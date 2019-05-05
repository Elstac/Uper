using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebApp.Data.Entities;

namespace WebApp.Data.Repositories
{
    public interface IRatesAndCommentRepository : IRepository<RatesAndComment, int>
    {
    }
    public class RatesAndCommentRepository : BaseRepository<RatesAndComment, int>, IRatesAndCommentRepository
    {
        public RatesAndCommentRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
