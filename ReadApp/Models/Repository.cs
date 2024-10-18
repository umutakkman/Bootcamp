using Microsoft.VisualBasic;

namespace ReadApp.Models
{

    public class Repository
    {
        private static readonly List<Product> _products = new();
        private static readonly List<Category> _categories = new();

        static Repository()
        {
            _categories.Add(new Category { CategoryID = 1, Name = "Kazak" });
            _categories.Add(new Category { CategoryID = 2, Name = "Pantolon" });
            _categories.Add(new Category { CategoryID = 3, Name = "Ceket" });

            _products.Add(new Product { ProductID = 1, Name = "Kadın Gri Yumuşak Dokulu Triko Kazak", Price = 1200, Stock = 5, Image = "kazak.png", CategoryID = 1 });
            _products.Add(new Product { ProductID = 2, Name = "Erkek Lacivert Slim Fit Dar Kesim Pamuklu Pantolon", Price = 1600, Stock = 15, Image = "pantolon.png", CategoryID = 2 });
            _products.Add(new Product { ProductID = 3, Name = "Kapüşonlu Gri Ceket", Price = 600, Stock = 8, Image = "ceket.png", CategoryID = 3 });
        }

        public static List<Product> Products { get { return _products; } }
        public static void CreateProduct(Product entity){
            _products.Add(entity);
        }
        public static void EditProduct(Product updateProduct){
            var entity = _products.FirstOrDefault(p => p.ProductID == updateProduct.ProductID);
            if (entity != null){
                entity.Name = updateProduct.Name;
                entity.Price = updateProduct.Price;
                entity.Stock = updateProduct.Stock;
                entity.Image = updateProduct.Image;
                entity.CategoryID = updateProduct.CategoryID;
            }
        }
        public static void DeleteProduct(Product entity){
            var prdEntity =_products.FirstOrDefault(p => p.ProductID == entity.ProductID);

            if (prdEntity != null){
                _products.Remove(prdEntity);
            }
        }
        public static List<Category> Categories { get { return _categories; } }
    }
}