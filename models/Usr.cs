using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using rest_server.models;

namespace rest_server.models
{
    [Table("Usrs")]
    public class Usr{
        [Key]
        public int id {get; set;}
        [Required]
        public string name {get; set;}

        public ICollection<Msg> msgs {get; set;}
        
    }
}