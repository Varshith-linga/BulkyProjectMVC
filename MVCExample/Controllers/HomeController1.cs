using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace MVCExample.Controllers
{
    public class HomeController1 : Controller
    {
        public string Index()
        {
            return "This is my <b>default</b>  action";
        }
        public string welcome(string name,int ID=1)
        {
            return HttpUtility.HtmlEncode("Name : "+name+" ID : "+ID);
        }
    }
}
