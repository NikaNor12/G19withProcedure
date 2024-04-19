using Microsoft.Data.SqlClient;
using System.Data;

namespace G19_ProductImport
{
    public class DatabaseImporter
    {
        private readonly string _connectionString;

        public DatabaseImporter(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void ImportData(IEnumerable<Category> categories)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                foreach (var category in categories)
                {
                    foreach (var product in category.Products)
                    {
                        using (SqlCommand command = new SqlCommand("[dbo].[sp_ImportProduct]", connection))
                        {

                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@CategoryName", category.Name);
                            command.Parameters.AddWithValue("@CategoryIsActive", category.IsActive);
                            AddProducts(command, product.Code, product.Name, product.Price, product.IsActive);
                            //command.Parameters.AddWithValue("@ProductCode", product.Code);
                            //command.Parameters.AddWithValue("@ProductName", product.Name);
                            //command.Parameters.AddWithValue("@ProductPrice", product.Price);
                            //command.Parameters.AddWithValue("@ProductIsActive", product.IsActive);
                            command.ExecuteNonQuery();
                        }
                    }
                }

                Console.WriteLine("Data inserted successfully.");
            }
        }
        public void AddProducts(SqlCommand sqlCommand, string productCode, string productName, decimal productPrice, bool isActive)
        {
            sqlCommand.Parameters.AddWithValue("@ProductCode", productCode);
            sqlCommand.Parameters.AddWithValue("@ProductName", productName);
            sqlCommand.Parameters.AddWithValue("@ProductPrice", productPrice);
            sqlCommand.Parameters.AddWithValue("@ProductIsActive", isActive);
        }
    }
}