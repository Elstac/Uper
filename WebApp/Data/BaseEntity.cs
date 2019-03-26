using System.ComponentModel.DataAnnotations;

namespace WebApp.Data
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
