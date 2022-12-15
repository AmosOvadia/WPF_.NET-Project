using DO;
using System;
using static Dal.DataSource;
using DalApi;

namespace Dal;

internal class DalProduct : IProduct
{
    public int Add(Product prod) //Add a product
    {
        //We will go through all the existing products to check if the new product already exists
        foreach (Product? prodcut in DataSource.productList) 
        {
            if (prod.Id == prodcut?.Id) //If it already exists we will throw an exception
                throw new TheIDAlreadyExistsInTheDatabase("The product already exists");

        }
        DataSource.productList.Add(prod); //We will add the new product to the list
        return prod.Id;
    }

    //Function to delete a product
    public void Delete(int ID) 
    {
        bool check = false; //Check if we have deleted
        foreach (Product? productTemp in productList) 
        {
            if (productTemp?.Id == ID) // Is this the member we want to delete?
            {
                check = true; 
                productList.Remove(productTemp); //We will remove the member from the list
                break;
            }
        }
        if (!check)//If we did not delete = the member did not exist, we will throw an exception
            throw new TheIdentityCardDoesNotExistInTheDatabase("The product does not exist");
    }


   // A function that updates a product
    public void Update(Product product)
    {
        int index = 0;
        bool check = false; //Check if we have really updated
        foreach (Product? productTemp in productList)
        {

            if (productTemp?.Id == product.Id) //Is this the product we want to update?
            {
                check = true;
                productList[index] = product; //We will update the member in the list
                break;
            }
            index++;
        }
        if (!check) //If we didn't update any product = it didn't exist
        {
            throw new TheIdentityCardDoesNotExistInTheDatabase("The product does not exist");
        }
    }

   // A function that returns a product according to the ID
    public Product Get(int id)
    {
        foreach (Product? productTemp in productList)
        {

            if (productTemp?.Id == id) //Is this the product we want to return?
            {
                return (Product)productTemp;
            }
        }
        //If the product does not exist
        throw new TheIdentityCardDoesNotExistInTheDatabase("The product does not exist");
    }

    //A function that returns a list of all products
    public IEnumerable<Product?> GetList(Func<Product?, bool>? func)
    {
        if (func == null) //Should I return everything?
        {
            List<Product?> products = new List<Product?>();
            foreach (Product? product in productList)
            {
                if (product?.Id > 0) 
                    products.Add(product);
            }
            return products;
        }
        else 
        {
            List<Product?> products = new List<Product?>();
            foreach (Product? product in productList)
            {
                if (func(product)) //Filter the products
                    products.Add(product);
            }
            return products;

        }
    }
    //A helper function that returns the size of the list
    public int Leangth()
    {
        return productList.Capacity;
    }

    //A function that returns a product according to a certain filter
    Product ICrud<Product>.GetByDelegate(Func<Product?, bool>? func)
    {
        if (func != null)
        {
            foreach (Product productTemp in productList)
            {
                if (func(productTemp))
                {
                    return (Product)productTemp;
                }
            }
        }
        //If we did not find the product that meets the filter requirements
        throw new TheIdentityCardDoesNotExistInTheDatabase("The product does not exist");
    }

   
}