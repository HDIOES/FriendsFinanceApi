using System.ComponentModel.DataAnnotations;


namespace FriendsFinanceApi.Models
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Dolghen { get; set; }
    }

}
