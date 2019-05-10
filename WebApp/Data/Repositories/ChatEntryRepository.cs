using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;

namespace WebApp.Data.Repositories
{
    public interface IChatEntryRepository : IRepository<ChatEntry, int>
    {
    }
    public class ChatEntryRepository : BaseRepository<ChatEntry, int>, IChatEntryRepository
    {
        public ChatEntryRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
