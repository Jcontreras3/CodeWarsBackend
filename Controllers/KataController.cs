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
        [Route("AddArticleItem")]
        public bool AddArtcleItem( ArticleItemModel newArticelItem){
            return _data.AddArtcleItem(newArticelItem);
        }

        [HttpGet]
        [Route("GetAllArticleItems")]
        public IEnumerable<ArticleItemModel> GetAllArticleItems(){
            return _data.GetAllArticleItems();
        }
    }
}