using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using rest_server.models;
namespace rest_server.models
{   
    [Table("Msgs")]
    public class Msg{
        [Key]
        public int id {get; set;}
        
        public ICollection<Answer> answers {get; set;} 
        
        
        public int usrid {get; set;}
        [ForeignKey("Usr")]
        public Usr usr {get; set;}
    }
}