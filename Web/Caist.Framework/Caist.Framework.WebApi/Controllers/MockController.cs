using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.WebApi.Controllers
{
    /// <summary>
    /// 接口说明：前端ant pro 用到的mock数据，如果没有这两个接口的数据前端目前会报错，这种是临时的解决办法，
    /// 要彻底解决还需要前端调整。
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class MockController : ControllerBase
    {

        [HttpGet]
        public string currentUser()
        {
            return "{\"name\":\"Serati Ma\",\"avatar\":\"" + GlobalContext.SystemConfig.WebUrI + "/image/portrait.png\",\"userid\":\"00000001\",\"email\":\"antdesign@alipay.com\",\"signature\":\"海纳百川，有容乃大\",\"title\":\"交互专家\",\"group\":\"蚂蚁金服－某某某事业群－某某平台部－某某技术部－UED\",\"tags\":[{\"key\":\"0\",\"label\":\"很有想法的\"},{\"key\":\"1\",\"label\":\"专注设计\"},{\"key\":\"2\",\"label\":\"辣~\"},{\"key\":\"3\",\"label\":\"大长腿\"},{\"key\":\"4\",\"label\":\"川妹子\"},{\"key\":\"5\",\"label\":\"海纳百川\"}],\"notice\":[{\"id\":\"xxx1\",\"title\":\"Alipay\",\"logo\":\"https://gw.alipayobjects.com/zos/rmsportal/WdGqmHpayyMjiEhcKoVE.png\",\"description\":\"那是一种内在的东西，他们到达不了，也无法触及的\",\"updatedAt\":\"2020-06-07T04:07:11.987Z\",\"member\":\"科学搬砖组\",\"href\":\"\",\"memberLink\":\"\"},{\"id\":\"xxx2\",\"title\":\"Angular\",\"logo\":\"https://gw.alipayobjects.com/zos/rmsportal/zOsKZmFRdUtvpqCImOVY.png\",\"description\":\"希望是一个好东西，也许是最好的，好东西是不会消亡的\",\"updatedAt\":\"2017-07-24T00:00:00.000Z\",\"member\":\"全组都是吴彦祖\",\"href\":\"\",\"memberLink\":\"\"},{\"id\":\"xxx3\",\"title\":\"Ant Design\",\"logo\":\"https://gw.alipayobjects.com/zos/rmsportal/dURIMkkrRFpPgTuzkwnB.png\",\"description\":\"城镇中有那么多的酒馆，她却偏偏走进了我的酒馆\",\"updatedAt\":\"2020-06-07T04:07:11.987Z\",\"member\":\"中二少女团\",\"href\":\"\",\"memberLink\":\"\"},{\"id\":\"xxx4\",\"title\":\"Ant Design Pro\",\"logo\":\"https://gw.alipayobjects.com/zos/rmsportal/sfjbOqnsXXJgNCjCzDBL.png\",\"description\":\"那时候我只会想自己想要什么，从不想自己拥有什么\",\"updatedAt\":\"2017-07-23T00:00:00.000Z\",\"member\":\"程序员日常\",\"href\":\"\",\"memberLink\":\"\"},{\"id\":\"xxx5\",\"title\":\"Bootstrap\",\"logo\":\"https://gw.alipayobjects.com/zos/rmsportal/siCrBXXhmvTQGWPNLBow.png\",\"description\":\"凛冬将至\",\"updatedAt\":\"2017-07-23T00:00:00.000Z\",\"member\":\"高逼格设计天团\",\"href\":\"\",\"memberLink\":\"\"},{\"id\":\"xxx6\",\"title\":\"React\",\"logo\":\"https://gw.alipayobjects.com/zos/rmsportal/kZzEzemZyKLKFsojXItE.png\",\"description\":\"生命就像一盒巧克力，结果往往出人意料\",\"updatedAt\":\"2017-07-23T00:00:00.000Z\",\"member\":\"骗你来学计算机\",\"href\":\"\",\"memberLink\":\"\"}],\"notifyCount\":12,\"unreadCount\":11,\"country\":\"China\",\"geographic\":{\"province\":{\"label\":\"浙江省\",\"key\":\"330000\"},\"city\":{\"label\":\"杭州市\",\"key\":\"330100\"}},\"address\":\"西湖区工专路 77 号\",\"phone\":\"0752-268888888\"}";
        }

        [HttpGet]
        public string Users()
        {
            return "[{key':'1','name':'John Brown','age':32,'address':'New York No. 1 Lake Park'},{'key':'2','name':'Jim Green','age':42,'address':'London No. 1 Lake Park'},{'key':'3','name':'Joe Black','age':32,'address':'Sidney No. 1 Lake Park'}]";
        }
    }
}
