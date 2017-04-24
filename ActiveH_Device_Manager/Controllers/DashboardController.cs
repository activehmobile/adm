using ActiveH_Device_Manager;
using ActiveH_Device_Manager.Models;
using System.Web.Mvc;


namespace ActiveH_Device_Manager.Controllers
{
    public class DashboardController : Controller
    {
        WebSocketConnectionManager wscm = new WebSocketConnectionManager();
        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            VmDashboard dummyData = getDummyData();
            return View(dummyData);
        }

        private VmDashboard getDummyData()
        {
            VmDashboard data = new VmDashboard();
            data.ConnectedSockets = wscm.GetAll().Count;
            return data;
        }
	}
}