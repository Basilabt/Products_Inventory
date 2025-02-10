namespace FACT_API.Model
{
    public class clsUserInventory
    {

        public int inventoryID { get; set; }
        public string serialNumber { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal unitPrice { get; set; }
        public int quantity { get; set; }
        public decimal total { get; set; }


    }
}
