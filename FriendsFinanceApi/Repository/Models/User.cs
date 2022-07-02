using System.ComponentModel.DataAnnotations;


namespace FriendsFinanceApi.Repository.Models
{
    public class User
    {
          
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }


    }

}
