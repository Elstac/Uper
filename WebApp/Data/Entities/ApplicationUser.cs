using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data
{
    /// <summary>
    /// 0 - User 1 - Moderator 2 - Admin
    /// </summary>
    public enum UserRole
    {
        User,
        Moderator,
        Admin
    }
    /// <typeparam name="Rating">Current Rating of an User 0-5</typeparam>
    /// <typeparam name="NumOfVotes">Number of Votes already cast</typeparam>
    /// <typeparam name="Role">0 - User 1 - Moderator 2 - Admin</typeparam>
    public class ApplicationUser : IdentityUser
    {
        public float Rating { get; set; }
        public int NumOfVote { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public UserRole Role { get; set; }
    } 
}

