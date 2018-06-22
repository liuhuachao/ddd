using DDD.Application.Dtos;

namespace DDD.Application.Interfaces
{
    public interface IVideosAppService
    {
        VideoDetail GetDetail(int id);
    }
}