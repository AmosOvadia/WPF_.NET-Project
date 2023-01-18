using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class DalProduct : IProduct
{
    string productPath = @"Product";
    static XElement config = XmlTools.LoadConfig();
    static DO.Product? createProductfromXElement(XElement s)
    {
        return new DO.Product
        {
            Id = s.ToIntNullable("Id") ?? throw new FormatException("Id"),
            Name = (string?)s.Element("Name")!.Value,
            Category = s.ToEnumNullable<DO.Enums.ProdactCategory>("Category"),
            Price = (double)s.Element("Price")!,
            InStock = (int)s.Element("InStock")!
        };
    }
    public int Add(Product product)
    {
        XElement product_root = XmlTools.LoadListFromXMLElement(productPath);
        if (product.Id == 0)
        {
            product.Id = int.Parse(config.Element("Id")!.Value) + 1;
            XmlTools.SaveConfigXElement("Id", product.Id);
        }
        XElement? prod = (from st in product_root.Elements()
                          where st.ToIntNullable("ProductID") == product.Id
                          select st).FirstOrDefault();
        if (prod != null)
            throw new Exception("ID already exist");
        product_root.Add(new XElement("Product",
                                   new XElement("Id", product.Id),
                                   new XElement("Name", product.Name),
                                   new XElement("Category", product.Category),
                                   new XElement("Price", product.Price),
                                   new XElement("InStock", product.InStock)
                                   ));
        XmlTools.SaveListToXMLElement(product_root, productPath);
        return product.Id;
    }

    public void Delete(int prodId)
    {
        XElement product_root = XmlTools.LoadListFromXMLElement(productPath);

        XElement? prod = (from st in product_root.Elements()
                          where (int?)st.Element("Id") == prodId
                          select st).FirstOrDefault() ?? throw new Exception("Missing ID");
        prod.Remove();  //<==> Remove stud from studentsRootElem

        XmlTools.SaveListToXMLElement(product_root, productPath);

    }

    public IEnumerable<Product?> GetList(Func<Product?, bool>? func = null)
    {
        XElement? product_root = XmlTools.LoadListFromXMLElement(productPath);
        if (func != null)
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

    public Product GetByDelegate(Func<Product?, bool>? func)
    {
        if (func == null)
            throw new Exception("missing function");

        XElement product_root = XmlTools.LoadListFromXMLElement(productPath);
        return ((from p in product_root.Elements()
                 where func(p.ConvertProduct_Xml_to_D0())
                 select p.ConvertProduct_Xml_to_D0()).FirstOrDefault());
    }

    public Product Get(int productId)
    {
        List<DO.Product?> ListProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);

        var product = ListProduct.FirstOrDefault(x => x?.Id == productId);

        if (product == null)
            throw new DO.TheIdentityCardDoesNotExistInTheDatabase("Product Id not found");
        else
            return (DO.Product)product;
    }

    public int Leangth()
    {
        List<DO.Product?> ListProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);
        return ListProduct.Count;
    }

    public void Update(Product product)
    {
        List<DO.Product?> ListProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);

        bool found = false;
        var foundProduct = ListProduct.FirstOrDefault(prod => prod?.Id == product.Id);
        if (foundProduct != null)
        {
            found = true;
            int index = ListProduct.IndexOf(foundProduct);
            ListProduct[index] = product;
        }
        if (found == false)
            throw new DO.TheIdentityCardDoesNotExistInTheDatabase("order item id not found");
        XmlTools.SaveListToXMLSerializer(ListProduct, productPath);
    }
}
