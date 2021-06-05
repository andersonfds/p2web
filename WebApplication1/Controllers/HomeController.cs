using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Estados = new List<string>
            {
                "Acre",
                "Alagoas",
                "Amapá",
                "Amazonas",
                "Bahia",
                "Ceará",
                "Distrito Federal",
                "Espírito Santo",
                "Goiás",
                "Maranhão",
                "Mato Grosso",
                "Mato Grosso do Sul",
                "Minas Gerais",
                "Pará",
                "Paraíba",
                "Paraná",
                "Pernambuco",
                "Piauí",
                "Rio de Janeiro",
                "Rio Grande do Norte",
                "Rio Grande do Sul",
                "Rondônia",
                "Roraima",
                "Santa Catarina",
                "São Paulo",
                "Sergipe",
                "Tocantins",
            };

            ViewBag.Cargos = new List<string>
            {
                "Analista de Sistemas",
                "DBA",
                "Programador ASP.NET",
                "Programador C#",
                "Programador PHP",
            };

            return View();
        }

        public ActionResult Exibir()
        {
            var cookie = Request.Cookies["curriculos"];
            var data = JsonConvert.DeserializeObject<List<dynamic>>(cookie?.Value ?? "[]");

            ViewBag.Curriculos = data;
            return View();
        }

        [HttpPost]
        public ActionResult Salvar(string nome, string endereco, string cidade, string estado, string ocupacao, string cargo, string resumo)
        {
            var cookie = Request.Cookies["curriculos"];

            var data = JsonConvert.DeserializeObject<List<dynamic>>(cookie?.Value ?? "[]");

            var id = Guid.NewGuid().ToString();

            data.Add(new
            {
                id,
                nome,
                endereco,
                cidade,
                estado,
                ocupacao,
                cargo,
                resumo,
            });

            var json = JsonConvert.SerializeObject(data);

            var httpResponseCookie = new HttpCookie("curriculos", json);
            httpResponseCookie.Expires = DateTime.UtcNow.AddDays(30);
            Response.Cookies.Add(httpResponseCookie);

            return View();
        }

        [HttpGet]
        public ActionResult Deletar(string id)
        {
            var cookie = Request.Cookies["curriculos"];
            var data = JsonConvert.DeserializeObject<List<dynamic>>(cookie?.Value ?? "[]");

            var filtered = data.Where(x => x.id != id).ToList();
            var json = JsonConvert.SerializeObject(filtered);

            var httpResponseCookie = new HttpCookie("curriculos", json);
            httpResponseCookie.Expires = DateTime.UtcNow.AddDays(30);
            Response.Cookies.Add(httpResponseCookie);

            return Redirect("/home/exibir");
        }
    }
}