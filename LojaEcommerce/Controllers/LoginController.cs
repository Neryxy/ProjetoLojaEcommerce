using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace LojaEcommerce.Controllers
{
    public class LoginController : Controller
    {
        // 🔵 ABRE A TELA DE LOGIN
        public IActionResult Index()
        {
            return View();
        }

        // 🟢 RECEBE O FORM (LOGIN)
        [HttpPost]
        public IActionResult Index(string email, string senha)
        {
            string conexao = "server=localhost;database=dbProjetoLoja;uid=root;pwd=vitor230908;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    conn.Open();

                    string sql = @"SELECT * FROM tb_Usuario 
                                   WHERE Email = @Email 
                                   AND Senha = @Senha";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Senha", senha);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var nivelLogin = reader["Nivel"].ToString();

                                HttpContext.Session.SetString("UsuarioLogado", "true");
                                HttpContext.Session.SetString("NivelUsuario", nivelLogin);

                                if (nivelLogin == "Admin")
                                {
                                    return RedirectToAction("ViewAdmin", "Login");
                                }
                                // LOGIN OK
                                ViewBag.Mensagem = "Login realizado com sucesso!";
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                // LOGIN FALHOU
                                ViewBag.Mensagem = "Email ou senha inválidos!";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = "Erro: " + ex.Message;
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove ("UsuarioLogado");
            HttpContext.Session.Remove ("NivelUsuario");
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        public IActionResult CadastrarUsuario()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarUsuario(string nome, string email, string senha)
        {
            string conexao = "server=localhost;database=dbProjetoLoja;uid=root;pwd=vitor230908;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    string nivel = "Cliente";
                    conn.Open();

                    bool emailExiste = false;

                    string sqlInserir = @"INSERT INTO tb_Usuario 
                                    (Nome, Email, Senha, Nivel)VALUES(@Nome, @Email, @Senha, @Nivel)";

                    string sqlVerificar = @"SELECT Email FROM tb_Usuario WHERE Email = @Email";

                    using (MySqlCommand cmd = new MySqlCommand(sqlVerificar, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                emailExiste = true;
                            }
                        }if(emailExiste == true)
                        {
                            TempData["EmailJaUtilizado"] = "Esse Email Já Esta Cadastrado";
                            return RedirectToAction("CadastrarUsuario", "Login");
                        }
                        else
                            {
                                using (MySqlCommand cmd2 = new MySqlCommand(sqlInserir, conn))
                                {
                                    cmd2.Parameters.AddWithValue("@Nome", nome);
                                    cmd2.Parameters.AddWithValue("@Email", email);
                                    cmd2.Parameters.AddWithValue("@Senha", senha);
                                    cmd2.Parameters.AddWithValue("@Nivel", nivel);
                                    TempData["CadastroUsuario"] = "Cadastro Realizado Com Sucesso Agora Faça Login";
                                    cmd2.ExecuteNonQuery();
                                }
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = "Erro: " + ex.Message;
            }
            return RedirectToAction("Index", "Login");
        }
        public IActionResult ViewAdmin()
        {
            return View();
        }
    }
}