using OpsMax.Models;

namespace OpsMax.ViewModels
{
    public class PaymentSourceDetailsVM

    {
        public PaymentSource Header { get; set; }
        public List<PaymentSourceDocument> Documents { get; set; } = new();
    }

}
