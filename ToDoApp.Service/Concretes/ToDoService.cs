using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using ToDoApp.Models.Entities;
using ToDoApp.Models.ToDos;
using ToDoApp.Repository.Repositories.Abstracts;
using ToDoApp.Service.Abstract;
using ToDoApp.Service.CacheServices;
using ToDoApp.Service.Constants;
using ToDoApp.Service.Rules;

namespace ToDoApp.Service.Concretes;


// void ToDoService_WhenToDoAdded_ReturnSuccess
// void ToDoService_WhenToDoAdded_ReturnFailed
// void ToDoService_WhenToDoAdded_ThrowsException
public sealed class ToDoService : IToDoService
{
    private readonly IToDoRepository _toDoRepository;
    private readonly IMapper _mapper;
    private readonly ToDoBusinessRules _businessRules;
   

    public ToDoService(IToDoRepository toDoRepository, IMapper mapper, ToDoBusinessRules businessRules)
    {
        _toDoRepository = toDoRepository;
        _mapper = mapper;
        _businessRules = businessRules;
  
    }

    
    public async Task<ReturnModel<ToDoResponseDto>> Add(CreateToDoRequestDto dto, string userId)
    {
        _businessRules.ToDoTitleMustBeUnique(dto.Title);

        ToDo createdToDo = _mapper.Map<ToDo>(dto);
        createdToDo.Id = Guid.NewGuid();
        createdToDo.UserId = userId;

       

        ToDo toDo = _toDoRepository.Add(createdToDo);

     

        ToDoResponseDto response = _mapper.Map<ToDoResponseDto>(toDo);
    
        return new ReturnModel<ToDoResponseDto>
        {
            Data = response,
            Message = "ToDo eklendi.",
            Status = 200,
            Success = true
        };
    }

    public ReturnModel<string> Delete(Guid id)
    {
        _businessRules.ToDoIsPresent(id);



        ToDo? toDo = _toDoRepository.GetById(id);
        ToDo deletedToDo = _toDoRepository.Delete(toDo);

        return new ReturnModel<string>
        {
            Data = $"ToDo Başlığı : {deletedToDo.Title}",
            Message = Messages.ToDoDeletedMessage,
            Status = 204,
            Success = true
        };
    }

    public ReturnModel<List<ToDoResponseDto>> GetAll()
    {
        var toDos = _toDoRepository.GetAll();
        List<ToDoResponseDto> responses = _mapper.Map<List<ToDoResponseDto>>(toDos);
        return new ReturnModel<List<ToDoResponseDto>>
        {
            Data = responses,
            Message = string.Empty,
            Status = 200,
            Success = true
        };
    }

    public ReturnModel<List<ToDoResponseDto>> GetAllByUserId(string userId)
    {
        List<ToDo> toDos = _toDoRepository.GetAll(p=>p.UserId== userId);
        List<ToDoResponseDto> responses = _mapper.Map<List<ToDoResponseDto>>(toDos);

        return new ReturnModel<List<ToDoResponseDto>>
        {
            Data = responses,
            Message = $"Kullanıcı Id sine göre ToDolar listelendi : Yazar Id: {userId}",
            Status = 200,
            Success = true
        };
        
    }

    public ReturnModel<List<ToDoResponseDto>> GetAllByCategoryId(int id)
    {
        List<ToDo> toDos = _toDoRepository.GetAll(x=>x.CategoryId==id);
        List<ToDoResponseDto> responses = _mapper.Map<List<ToDoResponseDto>>(toDos);
        return new ReturnModel<List<ToDoResponseDto>>
        {
            Data = responses,
            Message = $"Kategori Id sine göre ToDolar listelendi : Kategori Id: {id}",
            Status = 200,
            Success = true
        };
    }

    public ReturnModel<List<ToDoResponseDto>> GetAllByTitleContains(string text)
    {
        var toDos = _toDoRepository.GetAll(x=> x.Title.Contains(text));
        var responses = _mapper.Map<List<ToDoResponseDto>>(toDos);
        return new ReturnModel<List<ToDoResponseDto>>
        {
            Data = responses,
            Message = string.Empty,
            Status = 200,
            Success = true 
        };
    }

    public ReturnModel<ToDoResponseDto> GetById(Guid id)
    {
            _businessRules.ToDoIsPresent(id);

            var toDo = _toDoRepository.GetById(id);
            var response = _mapper.Map<ToDoResponseDto>(toDo);
            return new ReturnModel<ToDoResponseDto>
            {
                Data = response,
                Message = "İlgili toDo gösterildi",
                Status = 200,
                Success = true
            };
    }

    public ReturnModel<ToDoResponseDto> Update(UpdateToDoRequestDto dto)
    {
   
            _businessRules.ToDoIsPresent(dto.Id);
            //_businessRules.ToDoTitleMustBeUnique(dto.Title);

            ToDo toDo = _toDoRepository.GetById(dto.Id);

            toDo.Title = dto.Title;
            toDo.Description = dto.Description;

            _toDoRepository.Update(toDo);

            ToDoResponseDto response = _mapper.Map<ToDoResponseDto>(toDo);

            return new ReturnModel<ToDoResponseDto>
            {
                Data = response,
                Message = "ToDo Güncellendi.",
                Status = 200,
                Success = true
            };

        }
    }

