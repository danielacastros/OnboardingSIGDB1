using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Interfaces;

public interface IUnitOfWork
{
    Task Commit();
}