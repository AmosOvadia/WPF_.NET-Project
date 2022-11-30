using DO;
using System;
using static Dal.DataSource;
using DalApi;

namespace Dal;

internal class DalProduct : IProduct
{
    public int Add(Product prod)
    {
        foreach (Product prodcut in DataSource.productList)
        {
            if (prod.Id == prodcut.Id)
                throw new TheIDAlreadyExistsInTheDatabase("The product already exists");

        }
        DataSource.productList.Add(prod);     
        return prod.Id;
    }

    public void Delete(int ID)
    {
        bool check = false; 
        foreach (Product productTemp in productList)
        {
            if (productTemp.Id == ID)
            {
                check = true;
                productList.Remove(productTemp); 
                break;
            }
        }   
            if (!check)
                throw new TheIdentityCardDoesNotExistInTheDatabase("The product does not exist");
    }

    public void Update(Product product)
    {
        int index = 0;
        bool check = false;
        foreach (Product productTemp in productList)
        {

            if (productTemp.Id == product.Id)
            {
                check = true;
                productList[index] = product;
                break;
            }
            index++;
        }
        if (!check)
        {
            throw new TheIdentityCardDoesNotExistInTheDatabase("The product does not exist");
        }
    }

    public Product Get(int id)
    {
        foreach (Product productTemp in productList)
        {

            if (productTemp.Id == id)
            { 
                return productTemp;
            }
        } 
        throw new TheIdentityCardDoesNotExistInTheDatabase("The product does not exist");
    }

    public  IEnumerable<Product> GetList()
    {
        
        return productList;
    }

    public int Leangth()
    {
        return productList.Capacity;
    }
}