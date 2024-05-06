namespace LLDProblems.StandardProblems.ShoppingCoupon
{
    public class ShoppingCouponApply
    {
        Product item1 = new Product1(1, "Fan", ProductType.Household, 2000);
        Product item2 = new Product2(2, "Laptop", ProductType.Electronics, 50000);
        ShoppingCart cart = new ShoppingCart();
        public void PerformAction()
        {
            cart.AddProduct(item1);
            cart.AddProduct(item2);
        }

        public int FindSum()
        {
            int sum = 0;
            foreach (var product in cart.products)
                sum += product.GetProductPrice();

            return sum;
        }
        
    }

    public class ShoppingCart
    {
        public IList<Product> products = new List<Product>();

        public void AddProduct(Product p)
        {
            products.Add(new ProductPercentageDiscountDecorator(p, 10));
        }
    }

    public abstract class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public ProductType ProductType { get; set; }
        public int Price { get; set; }
        public Product(int id, string name, ProductType type, int price)
        {
            ProductId = id;
            ProductName = name;
            ProductType = type;
            Price = price;
        }

        public abstract int GetProductPrice();
    }

    public class Product1 : Product
    {
        public Product1(int id, string name, ProductType type, int price) : base(id, name, type, price) { }

        public override int GetProductPrice()
        {
            return Price;
        }
    }

    public class Product2 : Product
    {
        public Product2(int id, string name, ProductType type, int price) : base(id, name, type, price) { }

        public override int GetProductPrice()
        {
            return Price;
        }
    }

    public abstract class CouponDecorator : Product
    {
        public CouponDecorator(Product product) : base(product.ProductId, product.ProductName, product.ProductType, product.Price) { }
    }

    public class ProductPercentageDiscountDecorator : CouponDecorator
    {
        Product Product { get; set; }
        int Percentage { get; set; }
        public ProductPercentageDiscountDecorator(Product product, int percentage) : base(product)
        {
            Product = product;
            Percentage = percentage;
        }

        public override int GetProductPrice()
        {
            int oldPrice = Product.GetProductPrice();
            int newPrice = oldPrice - (oldPrice * Percentage) / 100;
            return newPrice;
        }
    }

    public enum ProductType
    {
        Electronics,
        Household,
        Office,
        Cloth,
        Footwear
    }
}
