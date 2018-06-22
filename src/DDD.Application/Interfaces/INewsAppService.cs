using DDD.Application.Dtos;

namespace DDD.Application.Interfaces
{
    public interface INewsAppService
    {
        NewsDetail GetDetail(int id);
    }
}