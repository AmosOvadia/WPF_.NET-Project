


using BlApi;
using BO;
using DalApi;
using System.Security.Cryptography;
using static BO.Enums;
using static DO.Enums;
 
namespace BlImplementation;

//The implementation of the interface of the logical entity -Product

internal class BoProduct : BlApi.IProduct
{
    DalApi.IDal? dal = DalApi.Factory.Get();

    //The function adds a product to the logical layer
    public void Add(BO.Product product)
    {

        if (product.Id < 0) // check if ID card is correct
        {   throw new BO.TheVariableIsLessThanTheNumberZero("Id is less than 0");
        }
        if (product.Name == "") //Check if the name exists
        {
            throw new BO.VariableIsNull("The name is null");
        }
        if (product.Price <= 0) // check if the price is not negative
        {  throw new BO.TheVariableIsLessThanTheNumberZero("the price is less than 0");
        }
        if (product.InStock < 0) //check if the quantity is negative
        { 
            throw new BO.TheVariableIsLessThanTheNumberZero("the stock is less than 0");
        }
        if ((DO.Enums.ProdactCategory?)product.Category == null)
        {
            throw new BO.InputError("You need to enter a category");
        }
        DO.Product Do_Product = new DO.Product();
        Do_Product.Id = product.Id;
        Do_Product.Name = product.Name;
        Do_Product.Price = product.Price;
        Do_Product.Category = (DO.Enums.ProdactCategory?)product.Category;
        Do_Product.InStock = product.InStock;
        dal.Product.Add(Do_Product); //Add the product in the data layer
    }


    //The function deletes a product to the logical layer
    public void Delete(int id)
    {
        bool check = false; //If such a product does not exist
        List<DO.Product?> products;
        products = dal.Product.GetList().ToList();


        //Go through all the existing products in the data layer
        var c = (from p in products
                     select p?.Id).Where(temp => temp == id);

        if (c.Count() == 1)
        {
            dal.Product.Delete(id); //Delete the product in the data layer
        }
        else//If we did not delete = the member did not exist, we will throw an exception
            throw new DO.TheIdentityCardDoesNotExistInTheDatabase("The product does not exist");
    }


    //The function returns the logical entity - product, according to the id
    public BO.Product GetProductById(int id)
    {
        DO.Product DoProduct = new DO.Product();
        BO.Product? BoProduct = new BO.Product();
        if (id > 0) //If the identity certificate is correct
        {
            DoProduct = dal.Product.Get(id);
            BoProduct.Id = DoProduct.Id;
            BoProduct.Name = DoProduct.Name;
            BoProduct.Price = DoProduct.Price;
            BoProduct.Category = (BO.Enums.ProdactCategory)DoProduct.Category;
            BoProduct.InStock = DoProduct.InStock;
            return BoProduct;
        }
        throw new BO.TheVariableIsLessThanTheNumberZero("Id is les than 0");
    }


    //The function returns the product item
    public BO.ProductItem GetProductItem(int id, BO.Cart cart)
    {
        if (cart.Items == null) //Check if there are any items in the shopping basket
        {
            throw new BO.IsEmpty("The shopping cart is empty");
        }
        if (id >= 0) //If the identity certificate is correct
        {
            DO.Product product = new DO.Product();
            product = dal.Product.Get(id);
            BO.ProductItem productItem = new BO.ProductItem();
            productItem.Id = product.Id;
            productItem.Name = product.Name;
            productItem.Price = product.Price;
            productItem.Category = (BO.Enums.ProdactCategory)product.Category;

            productItem.Amount = 0;

            //productItem.Amount = cart.Items.Where(item => id == item?.ProductId)
            //    .Sum(item => item.Amount);
            return productItem;
        }
        throw  new BO.TheVariableIsLessThanTheNumberZero("the id is less than 0");
    }


    //The function returns all products
    public IEnumerable<BO.ProductForList?> GetProducts(Func<DO.Product?, bool>? func)
    {
        //List<BO.ProductForList?> productsForList = new List<BO.ProductForList?>();
        // List<DO.Product?> products = new List<DO.Product?>();
       var products = dal.Product.GetList();
        if (func != null)
        {
            var productsForList = from item in products
                    where func(item)
                    select new ProductForList
                    { Id = (int)(item?.Id)!, Category = (Enums.ProdactCategory?)(item?.Category), Name = item?.Name, Price = (double)(item?.Price)! };
            return productsForList;
        }
        else
        {
           // var v = products.Select(p => (p?.Id, p?.Category, p?.Name, p?.Price));

            var productsForList = from item in products
                    select new ProductForList
                    { Id = (int)(item?.Id)!, Category = (Enums.ProdactCategory?)(item?.Category), Name = item?.Name ,Price = (double)(item?.Price)! };
            return productsForList;
        }
    }
    public IEnumerable<BO.ProductItem?> GetProductItems(Func<DO.Product?, bool>? func)
    {
        //List<BO.ProductItem> productItems = new List<BO.ProductItem>();

        //List<DO.Product> products = (List<DO.Product>)dal.Product.GetList();
        //List<DO.OrderItem> orderItems = (List<DO.OrderItem>)dal.OrderItem.GetList();

        //productItems = products.Select(item => new BO.ProductItem
        //{
        //    Id = (int)item.Id,
        //    Category = (Enums.ProdactCategory)item.Category,
        //    Name = item.Name,
        //    Price = (double)item.Price,
        //    InStock = (int)item.InStock,
        //    Amount = orderItems.Where(temp => temp.ProductId == item.Id).Sum(temp => (int)temp.Amount)
        //}).ToList();

        //return (IEnumerable<BO.ProductItem?>)productItems;



        List<BO.ProductItem?> productItems = new List<BO.ProductItem?>();

        List<DO.Product?> products = dal.Product.GetList().ToList();
        List<DO.OrderItem?> orderItems = dal.OrderItem.GetList().ToList();

        if (func == null)
        {
            foreach (var item in products)
            {
                BO.ProductItem? productItem = new ProductItem();
                productItem.Id = (int)(item?.Id)!;
                productItem.Category = (Enums.ProdactCategory?)(item?.Category);
                productItem.Name = item?.Name;
                productItem.Price = (double)(item?.Price)!;
                productItem.InStock = ((int)item?.InStock!) > 0 ? true : false; 
                productItem.Amount = 0;
                //foreach (var temp in orderItems)
                //{
                //    if (temp?.ProductId == item?.Id)
                //    {
                //        productItem.Amount += (int)temp?.Amount!;
                //    }
                //}
                productItems.Add(productItem);
            }
        }
        else
        {
            foreach (var item in products)
            {
                if (func(item))
                {
                    BO.ProductItem? productItem = new ProductItem();
                    productItem.Id = (int)(item?.Id)!;
                    productItem.Category = (Enums.ProdactCategory?)(item?.Category);
                    productItem.Name = item?.Name;
                    productItem.Price = (double)(item?.Price)!;
                    productItem.InStock = ((int)item?.InStock!) > 0 ? true : false;
                    productItem.Amount = 0;
                    foreach (var temp in orderItems)
                    {
                        if (temp?.ProductId == item?.Id)
                        {
                            productItem.Amount += (int)temp?.Amount!;
                        }
                    }
                    productItems.Add(productItem);
                }
            }
        }
        return productItems;
    }

















    //The function updates the logical entity
    public void Update(BO.Product product)
    {
        if (product.Id < 0) ////If the identity certificate is correct
        {
            throw new BO.TheVariableIsLessThanTheNumberZero("Id is less than 0");
        }
        if (product.Name == "") //Check if the name exists
        {
            throw new BO.VariableIsNull("The name is null");
        }
        if (product.Price <= 0) // check if the price is not negative
        {
            throw new BO.TheVariableIsLessThanTheNumberZero("the price is less than 0");
        }
        if (product.InStock < 0) //check if the quantity is negative
        {
            throw new BO.TheVariableIsLessThanTheNumberZero("the stock is less than 0");
        }
        if ((DO.Enums.ProdactCategory?)product.Category == null)
        {
            throw new BO.InputError("You need to enter a category");
        }
        DO.Product Do_Product = new DO.Product();

        Do_Product.Id = (int)product.Id;
        Do_Product.Name = product.Name;
        Do_Product.Price = product.Price;
        Do_Product.Category = (DO.Enums.ProdactCategory?)product.Category;
        Do_Product.InStock = product.InStock;
        try
        {
            dal.Product.Update(Do_Product);
        }
        catch (BO.TheIdDoesNotExistInTheDatabase ex)
        {
            Console.WriteLine(ex);
        }
        catch (BO.TheVariableIsLessThanTheNumberZero ex)
        { 
            Console.WriteLine(ex);
        }
        catch (BO.VariableIsNull ex)
        {
            Console.WriteLine(ex);
        }  
    }
}