using FactWebsiteApp.Models.Business;
using System.ComponentModel.DataAnnotations;

using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace FactWebsiteApp.Models.ViewModel
{
    public class clsProductViewModel
    {
        public int productID { get; set; }

        [Required(ErrorMessage = "Serial Number is required")]
        public string serialNumber {  get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string name {  get; set; }


        [Required(ErrorMessage = "Description is required")]
        public string description { get; set; }


        [Required(ErrorMessage = "Price is required")]
        public decimal price { get; set; }



        public List<clsProduct> productsList = clsProduct.getProductsAsList();

        public void updateProductsList()
        {
            this.productsList = clsProduct.getProductsAsList();
        }



        public static HttpClient httpClient = new HttpClient();
        public async Task getProductsFromAPI()
        {
            httpClient.BaseAddress = new Uri("https://localhost:7231/api/FACT/");

            try
            {
                Console.WriteLine("Fetching Products From API");
                Console.WriteLine("---------------------------");

                var products = await httpClient.GetFromJsonAsync<List<clsProduct>>("Products");

                if(products != null)
                {
                    this.productsList = products;
                }


            } catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }


        }


    }
}
