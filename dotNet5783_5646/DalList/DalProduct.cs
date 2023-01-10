
using System;
using static Dal.DataSource;
using DalApi;
using DO;
using System.Runtime.CompilerServices;

namespace Dal;

internal class DalProduct : IProduct
{
    public int Add(DO.Product prod) //Add a product
    {
        List<string> words = new List<string>() { "aa", "bb", "cc", "ab", "ac", "ba" };

        var groups = words.GroupBy(word => word[0]);




        //We will go through all the existing products to check if the new product already exists

        var check = (from p in productList
                     select p?.Id).Where(temp => temp == prod.Id);


        if (check.Count() == 0)
        {
            DataSource.productList.Add(prod); //We will add the new product to the list
            return prod.Id;
        } //If it already exists we will throw an exception
        throw new DO.TheIDAlreadyExistsInTheDatabase("The product already exists");
    }




    //Function to delete a product
    public void Delete(int ID)
    {     
        var check = (from p in productList
                     let temp = p?.Id
                     select p?.Id).Where(temp => temp == ID);

        if (check.Count() == 1)
        {
            DataSource.productList.Remove(Get(ID)); //We will remove the product from the list
        }
        else//If we did not delete = the member did not exist, we will throw an exception
            throw new DO.TheIdentityCardDoesNotExistInTheDatabase("The product does not exist");
    }


    // A function that updates a product
    public void Update(DO.Product product)
    {
        var temp = productList.FirstOrDefault(p => p?.Id == product.Id);
        int i = productList.IndexOf(temp);
        if (i != -1) 
        productList[i] = product;
        else //If we didn't update any product = it didn't exist
            throw new DO.TheIdentityCardDoesNotExistInTheDatabase("The product does not exist");
    }


    // A function that returns a product according to the ID
    public DO.Product Get(int id)
    {
        var product = productList.FirstOrDefault(p => p?.Id == id);
        if (product != null)
            return (DO.Product)product;
        //If the product does not exist
        throw new DO.TheIdentityCardDoesNotExistInTheDatabase("The product does not exist");
    }



    //A function that returns a list of all products
    public IEnumerable<DO.Product?> GetList(Func<DO.Product?, bool>? func)
    {
        if (func == null) //Should I return everything?
        {
            var list = (IEnumerable<DO.Product?>)productList.Select(p => p).OrderBy(p => p?.Id).ToList();
            return list;
        }
        else 
        {
            var list = productList.Select(p=> p).Where(temp => func(temp)).ToList();
            return list ;

        }
    }
    //A helper function that returns the size of the list
    public int Leangth()
    {
        return productList.Capacity;
    }

    //A function that returns a product according to a certain filter
    DO.Product ICrud<DO.Product>.GetByDelegate(Func<DO.Product?, bool>? func)
    {
        if (func != null)
        {
            var p = productList.FirstOrDefault(p => func(p));
            if (p != null)
                return (Product)p;
        } 
        //If we did not find the product that meets the filter requirements
        throw new DO.TheIdentityCardDoesNotExistInTheDatabase("The product does not exist");
    }

   
}