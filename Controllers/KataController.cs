using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CodeWarsBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KataController : ControllerBase
    {
        private readonly KataService _data;
        public KataController(KataService dataFromService){
            _data = dataFromService;
        }

        [HttpPost]
        [Route("AddKataItem")]
        public bool AddKataItem( KataModel newKataItem){
            return _data.AddKataItem(newKataItem);
        }

        [HttpGet]
        [Route("GetAllKataItems")]
        public IEnumerable<KataModel> GetAllKataItems(){
            return _data.GetAllKataItems();
        }

        [HttpPost]
        [Route("UpdateKataItem")]
        public bool UpdateKataItem(KataModel KataUpdate){
            return _data.UpdateKataItem(KataUpdate);
        }

        [HttpGet]
        [Route("GetKataByCompleted")]
        public IEnumerable<KataModel> GetKataByCompleted(){
            return _data.GetKataByCompleted();
        }

        [HttpGet]
        [Route("GetKataByPending")]
        public IEnumerable<KataModel> GetKataByPending(){
            return _data.GetKataByPending();
        }

        [HttpGet]
        [Route("GetItemsByUserAssigned/{UserAssigned}")]
        public IEnumerable<KataModel> GetItemsByUserAssigned(string UserAssigned){
            return _data.GetItemsByUserAssigned(UserAssigned);
        }
    }
}