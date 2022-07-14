using System.ComponentModel.DataAnnotations;


namespace FriendsFinanceApi.Models
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int DebtSum { get; set; }
    }

}
