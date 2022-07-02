using Npgsql;
using System.ComponentModel.DataAnnotations;


namespace FriendsFinanceApi.Repository.Models
{

    public class Activity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int OwnerId { get; set; }
        public User? Owner { get; set; }

        public int Sum { get; set; }

        public List<ActivityMember>? Members { get; set; }

    }

    public class ActivityMember
    {
        public int Id { get; set; }
        public Activity? Activity { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
        public int ActivityId { get; set; }

        public bool ActivityAcceptedByUser { get; set; }
    }

}
