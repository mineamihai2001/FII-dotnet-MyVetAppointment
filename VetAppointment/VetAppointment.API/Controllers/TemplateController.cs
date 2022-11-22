using Microsoft.AspNetCore.Mvc;

namespace VetAppointment.API.Controllers
{
    public class TemplateController<IRepository, Model>:Controller
    {
        private IRepository rep;
        private Model model;
        public TemplateController(IRepository rep, Model model)
        {
            this.rep = rep;
            this.model = model;
        }

        public IActionResult Get(int id)
        {
            return Ok(rep.GetAll();
        }



    }
}
