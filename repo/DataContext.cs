using Microsoft.EntityFrameworkCore;
using rest_server.models;
using rest_server.repo;
namespace rest_server.repo
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opts) : base(opts)
        {

        }

        public DbSet<Usr> Usrs {get;set;}
        public Usr? GetUserByName(string name){
            return Usrs.FirstOrDefault(u => u.name == name);
        }

        public DbSet<Msg> Msgs {get;set;}

        public Msg? GetMsgById(int id)
        {
            return Msgs.FirstOrDefault(m => m.id == id);

        }

        public IQueryable<Msg> GetMsgsByUserName(string name)
        {
            return Msgs.Where(m => m.usr.name == name);
        }
        public IQueryable<Msg> GetMsgsByAnswerText(string text)
        {
            return Msgs.Where(m => m.answers.Any(a => a.text.Contains(text)));
        }


        public DbSet<Answer> Answers {get;set;}

        public List<Answer> GetAnswersByLikeText(string text){
            return Answers.Where(a => a.text.Contains(text)).ToList();
        }


    }
}