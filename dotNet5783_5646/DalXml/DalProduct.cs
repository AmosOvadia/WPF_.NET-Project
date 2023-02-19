using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class DalProduct : IProduct
{
    string productPath = @"Product";
    static XElement config = XmlTools.LoadConfig();
    static DO.Product? createProductfromXElement(XElement product)
    {
        return new DO.Product
        {
            Id = product.ToIntNullable("Id") ?? throw new FormatException("Id"),
            Name = (string?)product.Element("Name")!.Value,
            Category = product.ToEnumNullable<DO.Enums.ProdactCategory>("Category"),
            Price = (double)product.Element("Price")!,
            InStock = (int)product.Element("InStock")!
        };
    }

    //Function to add a product
    public int Add(Product product)
    {
        XElement product_root = XmlTools.LoadListFromXMLElement(productPath);
        //if (product.Id == 0)
        //{
        //    product.Id = int.Parse(config.Element("Id")!.Value) + 1;
        //    XmlTools.SaveConfigXElement("Id", product.Id);
        //}
        XElement? prod = (from st in product_root.Elements()
                          where st.ToIntNullable("Id") == product.Id
                          select st).FirstOrDefault();
        if (prod != null) //If it already exists we will throw an exception
            throw new DO.TheIDAlreadyExistsInTheDatabase("ID already exist");
        product_root.Add(new XElement("Product",
                                   new XElement("Id", product.Id),
                                   new XElement("Name", product.Name),
                                   new XElement("Category", product.Category),
                                   new XElement("Price", product.Price),
                                   new XElement("InStock", product.InStock)
                                   ));   //We will add the new product to the list
        XmlTools.SaveListToXMLElement(product_root, productPath);
        return product.Id;
    }

    //Function to delete a product
    public void Delete(int prodId)
    {
        XElement product_root = XmlTools.LoadListFromXMLElement(productPath);

        XElement? prod = (from st in product_root.Elements()
                          where (int?)st.Element("Id") == prodId
                          select st).FirstOrDefault() ?? throw new Exception("Missing ID"); // If we did not delete = the member did not exist, we will throw an exception
        prod.Remove();  //<==> Remove stud from studentsRootElem

        XmlTools.SaveListToXMLElement(product_root, productPath);

    }

   //A function that returns a list of all products
    public IEnumerable<Product?> GetList(Func<Product?, bool>? func = null)
    {
        XElement? product_root = XmlTools.LoadListFromXMLElement(productPath);
        if (func != null) //Should I return everything?
        {
            return from s in product_root.Elements()
                   let prod = createProductfromXElement(s)
                   where func(prod)
                   select prod;
        }
        else
        {
            return from s in product_root.Elements()
                   select createProductfromXElement(s);
        }
    }

    //A function that returns a product according to a certain filter
    public Product GetByDelegate(Func<Product?, bool>? func)
    {
        if (func == null)
            throw new Exception("missing function");

        XElement product_root = XmlTools.LoadListFromXMLElement(productPath);
        return ((from p in product_root.Elements()
                 where func(p.ConvertProduct_Xml_to_D0())
                 select p.ConvertProduct_Xml_to_D0()).FirstOrDefault());
    }

    // A function that returns a product according to the ID
    public Product Get(int productId)
    {
        List<DO.Product?> ListProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);

        var product = ListProduct.FirstOrDefault(x => x?.Id == productId);

        if (product == null)  //If the product does not exist
            throw new DO.TheIdentityCardDoesNotExistInTheDatabase("Product Id not found");
        else
            return (DO.Product)product;
    }

    //A helper function that returns the size of the list
    public int Leangth()
    {
        List<DO.Product?> ListProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);
        return ListProduct.Count;
    }


    // A function that updates a product
    public void Update(Product product)
    {
        //Get the list of products
        List<DO.Product?> ListProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);
  
        var foundProduct = ListProduct.FirstOrDefault(prod => prod?.Id == product.Id);
        if (foundProduct != null) // Check if the product even exists
        {
            int index = ListProduct.IndexOf(foundProduct);
            ListProduct[index] = product;
        }
        else//If we didn't update any product = it didn't exist
            throw new DO.TheIdentityCardDoesNotExistInTheDatabase("order item id not found");
        XmlTools.SaveListToXMLSerializer(ListProduct, productPath);
    }
}
