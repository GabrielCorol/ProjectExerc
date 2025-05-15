using Microsoft.AspNetCore.Mvc;
using ProjectExerc.Repositorio;
using ProjectExerc.Models;

namespace ProjectExerc.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoRepositorio _produtoRepositorio;

        public ProdutoController(ProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }
        public IActionResult Index()
        {
            return View(_produtoRepositorio.TodosProdutos());
        }
        public IActionResult CadastrarProduto()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastrarProduto(Produto produto)
        {
            _produtoRepositorio.Cadastrar(produto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditarProduto(int id)
        {
            var produto = _produtoRepositorio.ObterProduto(id);
            if (produto != null)
            {
                return NotFound();
            }
            return View(produto);
        }

        [HttpPost]
        public IActionResult EditarProduto(int id, [Bind("Id,Nome,Descricao,Quantidade,Preco")]Produto produto)
        {
            if(id != produto.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (_produtoRepositorio.Atualizar(produto))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao atualizar o produto, tente novamente!");
                    return View(produto);
                }
            }
            return View(produto);
        }
        public IActionResult ExcluirProduto(int id)
        {
            _produtoRepositorio.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
