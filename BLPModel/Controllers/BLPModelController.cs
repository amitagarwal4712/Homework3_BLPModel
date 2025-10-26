using BLPModel.Features;
using BLPModel.Model;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace BLPModel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BLPModelController : ControllerBase
    {

        private readonly ILogger<BLPModelController> _logger;

        public BLPModelController(ILogger<BLPModelController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTestResult")]
        public IEnumerable<BLPTestModelResponse> Get(List<BLPTestModelRequest> request)
        {
            var blpFunction = new Features.BLPFunction();
            var subjectModel = new List<SubjectModel>{
                blpFunction.addSubject("alice", SecurityLevelEnum.S, SecurityLevelEnum.U),
                blpFunction.addSubject("bob", SecurityLevelEnum.C, SecurityLevelEnum.C),
                blpFunction.addSubject("eve", SecurityLevelEnum.U, SecurityLevelEnum.U),
            };
            var objectModel = new List<ObjectModel>{
                blpFunction.addObject("pub.txt", SecurityLevelEnum.U),
                blpFunction.addObject("emails.txt", SecurityLevelEnum.C),
                blpFunction.addObject("username.txt", SecurityLevelEnum.S),
                blpFunction.addObject("password.txt", SecurityLevelEnum.TS),
            };
            List<BLPTestModelResponse> res = new List<BLPTestModelResponse>();
            foreach (var item in request) { 
                if(item.testType == TestType.read)
                {
                    var result = blpFunction.read(item.subjectName, item.ObjectName);
                    res.Add(new BLPTestModelResponse() { TestResult = result });
                }
            }
            return res;

            
        }
    }
}
