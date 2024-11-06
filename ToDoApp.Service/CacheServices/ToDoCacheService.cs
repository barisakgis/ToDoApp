

using Core.Exceptions;
using Microsoft.Extensions.Caching.Distributed;
using ToDoApp.Models.ToDos;
using System.Text.Json;

namespace ToDoApp.Service.CacheServices;

public sealed class ToDoCacheService(IDistributedCache cache)
{
    public async Task<ToDoResponseDto> GetToDoByIdAsync(Guid id)
    {
        string cacheKey = $"ToDo({id})";
        string cachedToDo = await cache.GetStringAsync(cacheKey);


        if (string.IsNullOrEmpty(cachedToDo))
        {
            throw new BusinessException("İlgili toDo Cache de yok");
        }

        ToDoResponseDto toDo = JsonSerializer.Deserialize<ToDoResponseDto>(cachedToDo);
        return toDo;
    }

    public async Task<ToDoResponseDto> CreateToDoAsync(ToDoResponseDto toDo)
    {
        string cacheKey = $"ToDo({toDo.Id})";
        var serializeToDo = JsonSerializer.Serialize(toDo);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
        };

        await cache.SetStringAsync(cacheKey,serializeToDo,options);

        return toDo;
    }


    // toDo(1): {'id': değeri, 'title': değeri }
    public async Task DeleteAsync(Guid id)
    {
        string cacheKey = $"ToDo({id})";
        await cache.RemoveAsync(cacheKey);

    }

}
