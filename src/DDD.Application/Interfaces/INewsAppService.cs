using DDD.Application.Dtos;
using System.Threading.Tasks;

namespace DDD.Application.Interfaces
{
    public interface INewsAppService
    {
        NewsDetail GetDetail(int id);        
    }
}