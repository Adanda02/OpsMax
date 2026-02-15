namespace OpsMax.ViewModels
{
    public class PaymentSourceCreatePageVM
    {
        public PaymentSourceCreateVM Form { get; set; }

        public List<SupplierGRVVM> Suppliers { get; set; } = new();
        public List<GLAccountVM> GlAccounts { get; set; } = new();
    }
}
