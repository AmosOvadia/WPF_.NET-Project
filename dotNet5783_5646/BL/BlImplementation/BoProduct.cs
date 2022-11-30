using BlApi;
using Dal;
using DalApi;
using static BO.Enums;
using static DO.Enums;
 
namespace BlImplementation;

//The implementation of the interface of the logical entity -Product

internal class BoProduct : BlApi.IProduct
{
    private IDal dal = new Dal.DalList1();

    //The function adds a product to the logical layer
    public void Add(BO.Product product)
    {

        if (product.Id < 0) // check if ID card is correct
        {   throw new BO.TheVariableIsLessThanTheNumberZero("Id is less than 0");
        }
        if (product.Name == null) //Check if the name exists
        {  throw new BO.VariableIsNull("The name is null");
        }
        if (product.Price < 0) // check if the price is not negative
        {  throw new BO.TheVariableIsLessThanTheNumberZero("the price is less than 0");
        }
        if (product.InStock < 0) //check if the quantity is negative
        { 
            throw new BO.TheVariableIsLessThanTheNumberZero("the stock is less than 0");
        }
        DO.Product Do_Product = new DO.Product();
        Do_Product.Id = product.Id;
        Do_Product.Name = product.Name;
        Do_Product.Price = product.Price;
        Do_Product.Category = (DO.Enums.ProdactCategory)product.Category;
        Do_Product.InStock = product.InStock;
        dal.Product.Add(Do_Product); //Add the product in the data layer
    }


    //The function deletes a product to the logical layer
    public void Delete(int id)
    {
        bool check = false; //If such a product does not exist
        List<DO.Product> products = new List<DO.Product>();
        products = (List<DO.Product>)dal.Product.GetList();
        foreach (var product in products) //Go through all the existing products in the data layer
        {
            if (id == product.Id) //Is the product in the list the one we are looking for
            {
                dal.Product.Delete(id); //Delete the product in the data layer
                check = true; //To know that we have found a member that we would like to delete
                break;
            }
        }
        if (!check) //If such a product does not exist
        {
             throw new BO.TheIdDoesNotExistInTheDatabase("there is no ptoduct to delete");
        }
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
            foreach (BO.OrderItem item in cart.Items) //Go through all items
            {
                if(id == item.ProductId) //Is this the ID of the product we are looking for?
                    productItem.Amount += item.Amount;//Add up the amount
            }
            return productItem;

        }
        throw  new BO.TheVariableIsLessThanTheNumberZero("the id is less than 0");
    }


    //The function returns all products
    public IEnumerable<BO.ProductForList> GetProducts()
    {
        int i = 0;
        List<BO.ProductForList> productsForList = new List<BO.ProductForList>();
        List<DO.Product> products = new List<DO.Product>();
        products = (List<DO.Product>)dal.Product.GetList();
        foreach (DO.Product product in products) //Go through all the products in the data layer
        {
            //add to the logical layer
            productsForList.Add(new BO.ProductForList
            {
                Id = products[i].Id,
                Category = (BO.Enums.ProdactCategory)products[i].Category,
                Name = products[i].Name,
                Price = products[i].Price
            });
            i++;
        }
        return productsForList;
    }

    //The function updates the logical entity
    public void Update(BO.Product product)
    {
        if (product.Id < 0) ////If the identity certificate is correct
        {
            throw new BO.TheVariableIsLessThanTheNumberZero("Id is less than 0");
        }
        if (product.Name == null) //Check if the name exists
        {
            throw new BO.VariableIsNull("The name is null");
        }
        if (product.Price < 0) // check if the price is not negative
        {
            throw new BO.TheVariableIsLessThanTheNumberZero("the price is less than 0");
        }
        if (product.InStock < 0) //check if the quantity is negative
        {
            throw new BO.TheVariableIsLessThanTheNumberZero("the stock is less than 0");
        }
        DO.Product Do_Product = new DO.Product();

        Do_Product.Id = (int)product.Id;
        Do_Product.Name = product.Name;
        Do_Product.Price = product.Price;
        Do_Product.Category = (DO.Enums.ProdactCategory)product.Category;
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