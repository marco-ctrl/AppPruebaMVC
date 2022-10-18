using AppPruebaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

using System.Data;
using System.Data.SqlClient;

namespace AppPruebaMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly string cadenaSQL;

        public HomeController(IConfiguration configuration)
        {
            cadenaSQL = configuration.GetConnectionString("DBConexion");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult BusquedaPersona( string busqueda )
        {
            List<BusquedaPersona> busquedaPersonaList = new List<BusquedaPersona>();
            using (var conn = new SqlConnection(cadenaSQL))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_busqueda_persona", conn);
                cmd.Parameters.AddWithValue("busqueda", busqueda);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        busquedaPersonaList.Add(
                            new BusquedaPersona()
                            {
                                value = Convert.ToInt32(reader["papscodper"]),
                                label = reader["persona"].ToString(),
                                cedula = reader["capscedul"].ToString(),
                                nombre = reader["capsnomper"].ToString(),
                                apePaterno = reader["capsapepat"].ToString(),
                                apeMaterno = reader["capsapemat"].ToString()
                            });
                    }
                }
            }
            return Json(busquedaPersonaList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}