using System.IO;

namespace G19_ProductImport
{
    public static class FileManager
    {
        public static IEnumerable<Category> ReadData(string filePath)
        {
            if (filePath == null) throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath)) throw new FileNotFoundException("File not found.", filePath);

            //var categories = new List<Category>();
            var categories = new Dictionary<string, Category>();
            var products = new Dictionary<string, Product>();
            using var reader = new StreamReader(filePath);
            while (!reader.EndOfStream)
            {
                string[] parts = reader.ReadLine()!.Split('\t');
                Category category = GetCategoryFromList(Category.GetCategory(parts), categories);
                Product product = GetProductFromList(Product.GetProduct(parts), products);
                category.Products.Add(product);
            }

            return categories.Values;
        }
   
        private static Category GetCategoryFromList(Category category, Dictionary<string, Category> categories)
        {
            if (categories.ContainsKey(category.Name))
            {
                return categories[category.Name];
            }

            categories.Add(category.Name, category);
            return categories[category.Name];
        }

        private static Product GetProductFromList(Product product, Dictionary<string, Product> products)
        {
            if (products.ContainsKey(product.Name))
            {
                return products[product.Name];
            }
            products.Add(product.Name, product);
            return products[product.Name];
        }
    }
}