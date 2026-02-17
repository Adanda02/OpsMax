using Microsoft.AspNetCore.Mvc.Rendering;

namespace OpsMax.Controllers
{
    internal class List : List<SelectListItem>
    {
        private object vendors;
        private string v1;
        private string v2;

        public List(global::System.Object vendors, string v1, string v2)
        {
            this.vendors = vendors;
            this.v1 = v1;
            this.v2 = v2;
        }
    }
}