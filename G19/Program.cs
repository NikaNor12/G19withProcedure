using System.Diagnostics;

namespace G19_ProductImport
{
    internal class Program
    {
        static void Main()
        {

            Stopwatch stopwatch = new Stopwatch();
            const string filePath = @"D:\products.txt";
            const string connectionString = "Server=DESKTOP-FGUKH5T; Database=FirstAdonet; Integrated Security=true; TrustServerCertificate=true";

            DatabaseImporter importer = new DatabaseImporter(connectionString);


            int i = 1;
            try
            {

                var categories = FileManager.ReadData(filePath);
                stopwatch.Start();
                foreach (var category in categories)
                {
                    Console.WriteLine($"{i++}) {category}");
                    foreach (var product in category.Products)
                    {
                        //Console.WriteLine($"\t{product}");
                        stopwatch.Stop();

                    }
                    importer.ImportData(categories);
                    //Console.WriteLine("Data is imported");
                }
                Console.WriteLine($"Time : {stopwatch.ElapsedMilliseconds}");

                //importer.ImportData(categories);
                //var importer = new DatabaseImporter(connectionString);
                //Console.WriteLine("Data imported.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}