using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace AzureKeyVault.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var client = new SecretClient(new Uri(ConfigurationManager.AppSettings["Azure.KeyVaultUri"]), new DefaultAzureCredential());
            var secret = await client.GetSecretAsync("mysupersecretsecret");
            
            return View("Index", secret.Value);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}