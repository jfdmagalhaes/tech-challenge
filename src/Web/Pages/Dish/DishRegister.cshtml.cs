using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Models;
using Web.ServiceClients;

namespace Web.Pages.Dish
{
    public class DishRegisterModel : PageModel
    {
        private readonly IDishesServiceClient _dishesServiceClient;

        public DishRegisterModel(IDishesServiceClient dishesServiceClient)
        {
            _dishesServiceClient = dishesServiceClient ?? throw new ArgumentNullException(nameof(dishesServiceClient));
        }

        [BindProperty]
        public Models.Dish DishRegister { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dishRegisterData = new Models.Dish()
            {
                Description = DishRegister.Description,
                Name = DishRegister.Name,
                Value = DishRegister.Value,
                FileStream = DishRegister.FileStream
            };

            var registerId = await _dishesServiceClient.DishRegister(dishRegisterData);

            var message = string.Empty;
            if (registerId != null)
            {
                message = $"O prato foi cadastrado com sucesso! {registerId} ";
            }
            else
            {
                message = "Ocorreu um erro no cadastro do seu prato!";
            }

            TempData["Message"] = message;
            return RedirectToAction("DishRegister");
        }
    }
}