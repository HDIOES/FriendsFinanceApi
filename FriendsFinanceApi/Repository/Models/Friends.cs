namespace FriendsFinanceApi.Repository.Models
{
    public class FriendModel
    {
        public int Id { get; set; }

        public int RequestedById { get; set; }
        public User RequestedBy { get; set; }
        
        public int FriendId { get; set; }
        public User Friend { get; set; }
       
    }
}