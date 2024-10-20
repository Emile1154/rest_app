using rest_server.models.dto;

namespace rest_server.models.dto{
    public class MsgDTO{
        public int id {get; set;}
        public string fullName {get; set;}
        public List<string> answers {get; set;} = new List<string>();
    }
}