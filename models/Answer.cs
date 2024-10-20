using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using rest_server.models;
namespace rest_server.models
{
    [Table("Answers")]
    public class Answer{
        [Key]
        public int id {get; set;}
        [Required]
        public string text {get; set;}        

        
        public int msgid {get; set;}
        [ForeignKey("Msg")]
        public Msg msg {get; set;}

    }
}