using LojaEcommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.Win32.SafeHandles;
using MySqlConnector;
using System.ComponentModel;
using System.Text.Json;
using static System.Net.WebRequestMethods;


namespace LojaEcommerce.Controllers
{
    public class ProdutoController : Controller
    {

        // Listar Produtos

        [HttpGet]
        public IActionResult Produtos(string nome)
        {
            string conexao = "server=localhost;database=dbProjetoLoja;uid=root;pwd=vitor230908;";
            nome = "%" + nome + "%";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    conn.Open();
                    string sql = @"SELECT * FROM tb_Produtos 
                                   WHERE Nome LIKE  @Nome ";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nome", nome);
                        List<Produto> listaProdutos = new List<Produto>();


                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                Produto produto = new Produto();

                                produto.Id = Convert.ToInt32(reader["ID"]);
                                produto.Nome = reader["Nome"].ToString();
                                produto.Descricao = reader["Descricao"].ToString();
                                produto.Preco = Convert.ToDecimal(reader["Preco"]);
                                produto.Estoque = Convert.ToInt32(reader["Estoque"]);
                                produto.Imagem = reader["Imagem"].ToString();
                                listaProdutos.Add(produto);
                            }

                            ViewBag.Produtos = listaProdutos;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = "Erro: " + ex.Message;

            }
            return View();
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Produto produto)
        {
            string conexao = "server=localhost;database=dbProjetoLoja;uid=root;pwd=vitor230908;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    conn.Open();
                    string sql = @"INSERT INTO tb_Produtos(Nome, Descricao, Preco, Estoque, Imagem)VALUES(@Nome, @Descricao, @Preco, @Estoque, @Imagem)";


                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                        cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                        cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                        cmd.Parameters.AddWithValue("@Estoque", produto.Estoque);
                        cmd.Parameters.AddWithValue("@Imagem", produto.Imagem);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = "Erro: " + ex.Message;

            }
            return RedirectToAction("Gerenciar");
        }

        public IActionResult Gerenciar()
        {
            string conexao = "server=localhost;database=dbProjetoLoja;uid=root;pwd=vitor230908;";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    conn.Open();
                    string sql = @"SELECT * FROM tb_Produtos";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        List<Produto> listaProdutos = new List<Produto>();


                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                Produto produto = new Produto();

                                produto.Id = Convert.ToInt32(reader["ID"]);
                                produto.Nome = reader["Nome"].ToString();
                                produto.Descricao = reader["Descricao"].ToString();
                                produto.Preco = Convert.ToDecimal(reader["Preco"]);
                                produto.Estoque = Convert.ToInt32(reader["Estoque"]);
                                produto.Imagem = reader["Imagem"].ToString();
                                listaProdutos.Add(produto);
                            }

                            ViewBag.Produtos = listaProdutos;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = "Erro: " + ex.Message;

            }
            return View();
        }

        [HttpGet]
        public IActionResult Estoque()
        {
            string conexao = "server=localhost;database=dbProjetoLoja;uid=root;pwd=vitor230908;";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    conn.Open();
                    string sql = @"SELECT * FROM tb_Produtos";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        List<Produto> listaProdutos = new List<Produto>();


                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                Produto produto = new Produto();

                                produto.Id = Convert.ToInt32(reader["ID"]);
                                produto.Nome = reader["Nome"].ToString();
                                produto.Descricao = reader["Descricao"].ToString();
                                produto.Preco = Convert.ToDecimal(reader["Preco"]);
                                produto.Estoque = Convert.ToInt32(reader["Estoque"]);
                                produto.Imagem = reader["Imagem"].ToString();
                                listaProdutos.Add(produto);
                            }

                            ViewBag.Produtos = listaProdutos;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = "Erro: " + ex.Message;

            }
            return View();
        }
        [HttpPost]
        public IActionResult Estoque(int id, int estoque)
        {
            string conexao = "server=localhost;database=dbProjetoLoja;uid=root;pwd=vitor230908;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    conn.Open();
                    string sql = @"UPDATE tb_Produtos SET Estoque = @Estoque WHERE Id = @Id";


                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@Estoque", estoque);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = "Erro: " + ex.Message;

            }
            TempData["ProdutoEditado"] = "Produto editado com sucesso!";
            return RedirectToAction("Gerenciar");
        }

        public IActionResult Editar(int id)
        {
            string conexao = "server=localhost;database=dbProjetoLoja;uid=root;pwd=vitor230908;";

            Produto produto = new Produto();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    conn.Open();
                    string sql = @"SELECT * FROM tb_Produtos
                                    WHERE id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())

                        {

                            if (reader.Read())
                            {

                                produto.Id = Convert.ToInt32(reader["ID"]);
                                produto.Nome = reader["Nome"].ToString();
                                produto.Descricao = reader["Descricao"].ToString();
                                produto.Preco = Convert.ToDecimal(reader["Preco"]);
                                produto.Estoque = Convert.ToInt32(reader["Estoque"]);
                                produto.Imagem = reader["Imagem"].ToString();
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = "Erro: " + ex.Message;

            }
            return View(produto);
        }

        [HttpPost]
        public IActionResult Editar(Produto produto)
        {
            string conexao = "server=localhost;database=dbProjetoLoja;uid=root;pwd=vitor230908;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    conn.Open();
                    string sql = @"UPDATE tb_Produtos
                                    SET Nome = @Nome,
                                    Descricao = @Descricao,
                                    Preco = @Preco,
                                    Estoque = @Estoque,
                                    Imagem = @Imagem
                                    WHERE  id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", produto.Id);
                        cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                        cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                        cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                        cmd.Parameters.AddWithValue("@Estoque", produto.Estoque);
                        cmd.Parameters.AddWithValue("@Imagem", produto.Imagem);

                        cmd.ExecuteNonQuery();
                        return RedirectToAction("Gerenciar");

                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = "Erro: " + ex.Message;

            }
            return View();
        }

        public IActionResult Excluir(int id)
        {
            string conexao = "server=localhost;database=dbProjetoLoja;uid=root;pwd=vitor230908;";

            Produto produto = new Produto();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    conn.Open();
                    string sql = @"DELETE FROM tb_Produtos
                                    WHERE id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        {
                            return RedirectToAction("Gerenciar");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = "Erro: " + ex.Message;

            }
            return View();
        }

        public ActionResult AdicionarCarrinho(int id)
        {
            string conexao = "server=localhost;database=dbProjetoLoja;uid=root;pwd=vitor230908;";

            Carrinho carrinho = new Carrinho();
            Produto produto = new Produto();


            var itemEncontrado = false;

            string? carrinhoJson = HttpContext.Session.GetString("Carrinho");

            List<Carrinho> listaCarrinho;

            if (!string.IsNullOrEmpty(carrinhoJson))
            {
                listaCarrinho = JsonSerializer.Deserialize<List<Carrinho>>(carrinhoJson);
            }
            else
            {
                listaCarrinho = new List<Carrinho>();
            }

            try
            {
                var quantidade = 1;

                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    conn.Open();
                    string sql = @"SELECT * FROM tb_Produtos
                                    WHERE id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                carrinho.Id = Convert.ToInt32(reader["ID"]);
                                carrinho.Nome = reader["Nome"].ToString();
                                carrinho.Descricao = reader["Descricao"].ToString();
                                carrinho.Preco = Convert.ToDecimal(reader["Preco"]);
                                carrinho.Imagem = reader["Imagem"].ToString();
                                produto.Estoque = Convert.ToInt32(reader["Estoque"]);
                                carrinho.Quantidade = quantidade;

                                for (var i = 0; i < listaCarrinho.Count; i++)
                                {
                                    if (listaCarrinho[i].Id == id)
                                    {
                                        if (listaCarrinho[i].Quantidade < produto.Estoque)
                                        {
                                            itemEncontrado = true;
                                            listaCarrinho[i].Quantidade++;
                                            break;
                                        }
                                        else
                                        {
                                            itemEncontrado = true;
                                            TempData["ErroEstoque"] = "Quantidade máxima em estoque atingida";
                                        }
                                    }
                                }
                                if (itemEncontrado == false)
                                {
                                    listaCarrinho.Add(carrinho);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorInfo = ex.Message;
            }

            carrinhoJson = JsonSerializer.Serialize(listaCarrinho);
            HttpContext.Session.SetString("Carrinho", carrinhoJson);
            return RedirectToAction("Carrinho");
        }

        public IActionResult Carrinho()
        {
            string? carrinhoJson = HttpContext.Session.GetString("Carrinho");

            List<Carrinho> listaCarrinho = new List<Carrinho>();

            if (!string.IsNullOrEmpty(carrinhoJson))
            {
                listaCarrinho = JsonSerializer.Deserialize<List<Carrinho>>(carrinhoJson);
            }

            ViewBag.Carrinho = listaCarrinho;

            return View();
        }

        public IActionResult RemoverCarrinho(int id)
        {
            string? carrinhoJson = HttpContext.Session.GetString("Carrinho");

            List<Carrinho> listaCarrinho = new List<Carrinho>();

            if (!string.IsNullOrEmpty(carrinhoJson))
            {
                listaCarrinho = JsonSerializer.Deserialize<List<Carrinho>>(carrinhoJson);
            }

            for (var i = 0; i < listaCarrinho.Count; i++)
            {
                if (listaCarrinho[i].Id == id)
                {
                    listaCarrinho[i].Quantidade--;
                    if (listaCarrinho[i].Quantidade == 0)
                    {
                        listaCarrinho.Remove(listaCarrinho[i]);
                    }
                    break;
                }
            }
            carrinhoJson = JsonSerializer.Serialize(listaCarrinho);
            HttpContext.Session.SetString("Carrinho", carrinhoJson);

            return RedirectToAction("Carrinho");

        }

        public IActionResult ApagarCarrinho(int id)
        {
            string? carrinhoJson = HttpContext.Session.GetString("Carrinho");

            List<Carrinho> listaCarrinho = new List<Carrinho>();

            if (!string.IsNullOrEmpty(carrinhoJson))
            {
                listaCarrinho = JsonSerializer.Deserialize<List<Carrinho>>(carrinhoJson);
            }

            for (var i = 0; i < listaCarrinho.Count; i++)
            {
                if (listaCarrinho[i].Id == id)
                {
                    listaCarrinho[i].Quantidade = 0;
                    if (listaCarrinho[i].Quantidade == 0)
                    {
                        listaCarrinho.Remove(listaCarrinho[i]);
                    }
                    break;
                }
            }
            carrinhoJson = JsonSerializer.Serialize(listaCarrinho);
            HttpContext.Session.SetString("Carrinho", carrinhoJson);

            return RedirectToAction("Carrinho");

        }

        public IActionResult FinalizarCompra()
        {
            string conexao = "server=localhost;database=dbProjetoLoja;uid=root;pwd=vitor230908;";

            string? carrinhoJson = HttpContext.Session.GetString("Carrinho");

            Produto produto = new Produto();

            List<Carrinho> listaCarrinho = new List<Carrinho>();

            if (!string.IsNullOrEmpty(carrinhoJson))
            {
                listaCarrinho = JsonSerializer.Deserialize<List<Carrinho>>(carrinhoJson);
            }

            if (listaCarrinho.Count == 0)
            {
                ViewBag.CarrinhoVazio = "Carrinho está vazio";
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    conn.Open();
                    string sql = @"SELECT * FROM tb_Produtos
                                    WHERE id = @id";

                    string sql2 = @"UPDATE tb_Produtos
                                    SET Estoque = @estoque WHERE id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    using (MySqlCommand cmdUpdate = new MySqlCommand(sql2, conn))
                    {

                        {

                            var novoEstoque = 0;
                            for (var i = 0; i < listaCarrinho.Count; i++)
                            {
                                cmd.Parameters.AddWithValue("@id", listaCarrinho[i].Id);
                                using (MySqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        produto.Estoque = Convert.ToInt32(reader["Estoque"]);
                                        novoEstoque = produto.Estoque - listaCarrinho[i].Quantidade;
                                        cmdUpdate.Parameters.AddWithValue("@id", listaCarrinho[i].Id);
                                        cmdUpdate.Parameters.AddWithValue("@estoque", novoEstoque);
                                    }
                                }
                                if (listaCarrinho[i].Quantidade <= produto.Estoque)
                                {
                                    ViewBag.ProdutoFinalizado = "Compra Finalizada";
                                    
                                    cmdUpdate.ExecuteNonQuery();
                                }
                                else
                                {
                                    ViewBag.ProdutoCancelado = "Compra Cancelada, estoque insuficiente";
                                    break;
                                }
                                cmd.Parameters.Remove("@id");
                                cmd.Parameters.Clear();
                                cmdUpdate.Parameters.Clear();

                            }

                            HttpContext.Session.Remove("Carrinho");
                            return RedirectToAction("Carrinho");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                {
                    ViewBag.Mensagem = ex.Message;
                    return View("Carrinho");
                }
            }
        }
    }
}