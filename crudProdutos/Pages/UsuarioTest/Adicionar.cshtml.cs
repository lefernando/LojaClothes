using crudProdutos.Data;
using crudProdutos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace crudProdutos.Pages.UsuarioTest
{
    public class AdicionarModel : PageModel
    {

        [BindProperty] //Os dados preenchidos j� s�o enviados para esta classe por conta desta nota��o.
        public Usuario Usuario { get; set; }

        private readonly ApplicationDbContext _context;

        public AdicionarModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet() //Quando houver alguma resposta, ele ir� retornar a pr�pria p�gina
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() //Colocando "OnPostAsync", ele j� intercepta o retorno de um forms.
        {
            var usuario = new Usuario();
            Usuario.Endereco = new Endereco();
            //Novos Usu�rios sem iniciam com essa situa��o!
            usuario.Situacao = Usuario.SituacaoUsuario.Cadastrado;

            if(await TryUpdateModelAsync(usuario, Usuario.GetType(), nameof(Usuario)))
            {
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Listar");
            }
            return Page();
        }
    }
}
