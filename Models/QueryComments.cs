using System.ComponentModel.DataAnnotations;

namespace mvc_query_hub.Models
{
    public class QueryComments
    {
        [Key]
        public int Id { get; set; }
        public int QueryId { get; set; }
        public string Message { get; set; }
        public int? UserId { get; set; }
        public DateTime? PostDate { get; set; }
        public DateTime? EditDate { get; set; }

    }
}
