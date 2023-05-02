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
    }
}