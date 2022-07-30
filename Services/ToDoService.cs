using System.Data;
using AutoMapper;
using ToDoApplication.Data.Entities;
using ToDoApplication.Dto;
using ToDoApplication.Repositories.Interfaces;
using ToDoApplication.Services.Interfaces;

namespace ToDoApplication.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IToDoItemRepository _toDoItemRepository;
        private readonly IMapper _mapper;

        public ToDoService(IToDoListRepository toDoListRepository, IToDoItemRepository toDoItemRepository, IMapper mapper)
        {
            _toDoListRepository = toDoListRepository;
            _toDoItemRepository = toDoItemRepository;
            _mapper = mapper;
        }

        public async Task<ToDoListDto> GetToDoListByUser(string userId)
        {
            ToDoListDto model = new ToDoListDto();
            var list = await _toDoListRepository.GetToDoListByUser(userId);

            if (list != null)
            {
                model = _mapper.Map<ToDoListDto>(list);
            }
            return model;
        }

        public async  Task<ToDoItemDto>? InsertToDoItem(ToDoItemDto toDoItem)
        {
            var item = _mapper.Map<ToDoItem>(toDoItem);

            item.CreatedDate = DateTime.UtcNow;
            var result = await _toDoItemRepository.Insert(item);
            _toDoItemRepository.SaveChanges();
            return _mapper.Map<ToDoItemDto>(result);

        }

        public async Task<ToDoItemDto> EditToDoItem(ToDoItemDto toDoItem)
        {

            var item = _mapper.Map<ToDoItem>(toDoItem);
            var result = _toDoItemRepository.Update(item);
            _toDoItemRepository.SaveChanges();
            return _mapper.Map<ToDoItemDto>(result);
        }

        public async Task<bool> DeleteToDoItem(int? id)
        {
            bool deleted = false;
            var item  = await _toDoItemRepository.Get(id);
            if (item != null)
            {
                item.IsDeleted = true;
                _toDoItemRepository.Update(item);
                _toDoItemRepository.SaveChanges();
                deleted = true;
            }

            return deleted;
        }

        public async Task<ToDoListDto> EditToDoList(ToDoListDto toDoList)
        {
            var toDo = _mapper.Map<ToDoList>(toDoList);
            var result = _toDoListRepository.Update(toDo);
            _toDoListRepository.SaveChanges();

            return _mapper.Map<ToDoListDto>(result);
        }

        public async Task<ToDoItemDto> GetToDoItem(int? id)
        {
            var result = await _toDoItemRepository.Get(id);

            return _mapper.Map<ToDoItemDto>(result);
        }

        public async Task<List<ToDoItemDto>> GetToDoItemsByListId(int listId)
        {
            var dbList =  await _toDoItemRepository.GetToDoItemsByListId(listId);

            return _mapper.Map<List<ToDoItemDto>>(dbList);
        }
    }
}
