using FactWebsiteApp.Models.DataAccess;
using System.Data;

namespace FactWebsiteApp.Models.Business
{
    public class clsProduct
    {
        public enum enMode
        {
            AddNew = 1 , Update = 2, Delete = 3
        }

        public int productID { get; set; }
        public string serialNumber { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public enMode mode { get; set; } 

        public clsProduct()
        {
            this.productID = -1;
            this.serialNumber = "";
            this.name = "";
            this.description = "";
            this.price = 0.0M;
            this.mode = enMode.AddNew;
        }

        private clsProduct(int productID, string serialNumber, string name, string description, decimal price)
        {
            this.productID = productID;
            this.serialNumber = serialNumber;
            this.name = name;
            this.description = description;
            this.price = price;
            this.mode = enMode.Update;
        }

        public bool save()
        {
            switch(this.mode)
            {
                case enMode.AddNew:
                    {
                        return false;
                    }

                case enMode.Update:
                    {
                        return updateProductByProductID(this.productID,this.serialNumber,this.name,this.description,this.price);
                    }

                case enMode.Delete:
                    {
                        return deleteProductByProductID(this.productID);
                    }

            }

            return false;
        }


        public static DataTable getProductsAsDataTable()
        {
            return clsProductDataAccess.getProducts();
        }

        public static List<clsProduct> getProductsAsList()
        {
            DataTable dataTable = getProductsAsDataTable();

            List<clsProduct> productsList = new List<clsProduct>();

            foreach(DataRow row in dataTable.Rows)
            {
                clsProduct product = new clsProduct();

                product.productID = Convert.ToInt32(row["ProductID"]); 
                product.serialNumber = row["SerialNumber"].ToString();
                product.name = row["Name"].ToString();
                product.description = row["Description"].ToString();
                product.price = Convert.ToDecimal(row["Price"]);

                productsList.Add(product);

            }

            return productsList;
        }

        public static bool updateProductByProductID(int productID , string serialNumber, string name ,string description,decimal price)
        {
            return clsProductDataAccess.updateProduct(productID, serialNumber, name, description, price);
        }

        public static clsProduct getProductByProductID(int productID)
        {
            string serialNumber = "", name = "", desription = "";
            decimal price = 0.0M;

            if(clsProductDataAccess.getProductByProductID(productID,ref serialNumber,ref name,ref desription,ref price))
            {
                return new clsProduct(productID,serialNumber,name,desription,price);
            }


            return null;
        }

        public static bool deleteProductByProductID(int productID)
        {
            return clsProductDataAccess.deleteProductByProductID(productID);
        }
    }
}
