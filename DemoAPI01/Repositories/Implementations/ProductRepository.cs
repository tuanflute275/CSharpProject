using DemoAPI01.Data;
using DemoAPI01.Models.Domains;
using DemoAPI01.Repositories.Abstracts;

namespace DemoAPI01.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public bool Add(Product model)
    {
        try
        {
            _context.Products.Add(model);
            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public bool Update(Product model)
    {
        try
        {
            _context.Products.Update(model);
            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}