using DemoAPI01.Models.Domains;

namespace DemoAPI01.Repositories.Abstracts;

public interface IProductRepository
{
    bool Add(Product model);
    bool Update(Product model);
}