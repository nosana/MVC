using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Division
    {
        public Division()
        {

        }
        public Division(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
