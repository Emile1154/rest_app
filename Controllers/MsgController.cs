using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rest_server.models;
using rest_server.repo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rest_server.models.dto;
namespace rest_server.Controllers
{
    [Route("survey")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly DataContext _dataContext;
    
        public SurveyController(DataContext dataContext)
        {
            _dataContext    = dataContext;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSurvey([FromBody] MsgDTO jsonMsg)
        {      
            List<string> errors = new List<string>();
            if(jsonMsg == null){
                errors.Add("Запрос не может быть пустым");
                return SendError(errors.ToArray());
            }
            //check user is exist 
            Usr _usr = _dataContext.GetUserByName(jsonMsg.fullName);
            if(_usr == null){
                errors.Add("Пользователя с таким именем не существует");
            }
            //check id for that survey
            if( _dataContext.GetMsgById(jsonMsg.id) != null){
                errors.Add("Сообщение с таким id уже существует");
            }

            if(errors.Count > 0){
                return SendError(errors.ToArray());
            }

            Msg newMsg = new Msg();
            newMsg.id    = jsonMsg.id;
            newMsg.usr   = _usr;
            newMsg.usrid = _usr.id; 

            _dataContext.Msgs.Add(newMsg);
            // _dataContext.SaveChanges();
            
            foreach(string s in jsonMsg.answers){
                Answer ans = new Answer();
                ans.text  = s;
                ans.msgid = newMsg.id;
                ans.msg   = newMsg;
                _dataContext.Answers.Add(ans);
                // _dataContext.SaveChanges();
                newMsg.answers.Add(ans);
            }
            // _dataContext.SaveChanges();
            
            _usr.msgs.Add(newMsg);
            _dataContext.SaveChanges();
            return Ok();
        }

        [HttpGet("find/all")]
        public async Task<IActionResult> FindAllSurveys()
        {
            if(_dataContext.Msgs.Count() == 0){
                return SendError( new string[] { "Нет ни одного опросника" } );
            }
            var surveys = _dataContext.Msgs
                .Include(m => m.answers)
                .Select(m => new 
                {
                    m.id,
                    m.usrid,
                    Answers = m.answers.Select(a => new 
                    {
                        a.id,
                        a.text,
                    }).ToList()
                }).ToList();
            return Ok(surveys);
        }

        [HttpGet("find/user")]
        public async Task<IActionResult> FindSurveysByUser([FromQuery] string fullName)
        {
            IQueryable<Msg> msgs = _dataContext.GetMsgsByUserName(fullName);
            if(msgs.Count() == 0){
                return SendError( new string[] { "Нет ни одного опросника c таким именем пользователя" } );                
            }
            var surveys = msgs
                .Include(m => m.answers) 
                .Select(m => new 
                {
                    m.id,
                    m.usrid,
                    Answers = m.answers.Select(a => new 
                    {
                        a.id,
                        a.text,
                    }).ToList()
                }).ToList();
            return Ok(surveys);
        }

        [HttpGet("find/text")]
        public async Task<IActionResult> FindSurveysByText([FromQuery] string keyword)
        {
            IQueryable<Msg> msgs = _dataContext.GetMsgsByAnswerText(keyword);
            if(msgs.Count() == 0){
                return SendError( new string[] { "Нет ни одного опросника c таким текстом" } );                
            }
            var surveys = msgs
                .Include(m => m.answers) 
                .Select(m => new 
                {
                    m.id,
                    m.usrid,
                    Answers = m.answers.Select(a => new 
                    {
                        a.id,
                        a.text,
                    }).ToList()
                }).ToList();
            return Ok(surveys);
        }


        private IActionResult SendError(string[] _errors)
        {
            return BadRequest(new 
            { 
                msg = "Ошибка", 
                errors = _errors
            });
        }

    }
}
