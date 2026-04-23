using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace LojaEcommerce.Controllers
{
    public class BancoTesteController : Controller
    {
        public IActionResult Index()
        {
            string conexao = "server=localhost;database=dbProjetoLoja;uid=root;pwd=vitor230908@;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexao)) 
                {
                    conn.Open();
                    ViewBag.Mensagem = "Conectado com Sucesso ao Banco!";
                }
            }
            catch (Exception ex)
            { 
                ViewBag.Mensagem = "Erro na Conexão" + ex.Message;
            }
            return View();
        }
    }
}

