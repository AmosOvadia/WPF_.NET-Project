using DO;
using System;
using static Dal.DataSource;

namespace Dal;

public class DalProduct
{
    public DalProduct()
    {
        DataSource.s_Initialize();
    }

    public int addProduct(Prodcut prod)
    {
        foreach (Prodcut prodcut in DataSource.productArr)
        {
            if (prod.Id == prodcut.Id)
                throw new Exception("The product already exists");

        }
        DataSource.productArr[DataSource.Config.indexProduct++] = prod;
        return prod.Id;
    }


    public void deleteProdoct(int ID)
    {
        {
            int i;
            for (i = 0; i < DataSource.Config.indexProduct; i++)
            {
                if (productArr[i].Id == ID)
                    break;
            }
            if (i == DataSource.Config.indexProduct)
                throw new Exception("The product does not exist");
            else
            {
                for (; i < DataSource.Config.indexProduct; i++)
                {
                    productArr[i].Id = productArr[i++].Id;
                }
                DataSource.Config.indexProduct--;
            }
        }
    }


    public void updateProduct(Prodcut product)
    {
        int i;
        bool check = false;
        for (i = 0; (i < DataSource.Config.indexProduct) && (!check); i++)
        {
            if (productArr[i].Id == product.Id)
            {
                productArr[i] = product;
                check = true;
                break;
            }
        }
        if (!check)
        {
            throw new Exception("The product does not exist");
        }
    }

    public Prodcut getProdcut(int id)
    {
        for (int i = 0; i < DataSource.Config.indexProduct; i++)
        {
            if (productArr[i].Id == id)
                return productArr[i];
        }
        throw new Exception("The product does not exist");
    }

    public static Prodcut[] getProductArray()
    {
        Prodcut[] arr = new Prodcut[50];
        for (int i = 0; i < DataSource.Config.indexProduct; i++)
        {
            arr[i] = DataSource.productArr[i];
        }
        return arr;
    }

    public int productLeangth()
    {
        return DataSource.Config.indexProduct;
    }
}