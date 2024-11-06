using NUnit.Framework;
using Moq;
using ToDoApp.Models.ToDos;
using ToDoApp.Service.Concretes;
using ToDoApp.Service.Abstract;
using ToDoApp.Service.Rules;
using ToDoApp.Repository.Repositories.Abstracts;
using AutoMapper;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.Exceptions;
using ToDoApp.Models.Entities;

namespace ToDoApp.Tests.Services
{
    [TestFixture]
    public class ToDoServiceTests
    {
        private Mock<IToDoRepository> _mockToDoRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<ToDoBusinessRules> _mockBusinessRules;
        private ToDoService _toDoService;

        [SetUp]
        public void Setup()
        {
            _mockToDoRepository = new Mock<IToDoRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockBusinessRules = new Mock<ToDoBusinessRules>();

            _toDoService = new ToDoService(_mockToDoRepository.Object, _mockMapper.Object, _mockBusinessRules.Object);
        }



        [Test]
        public async Task ToDoService_WhenToDoAdded_ReturnSuccess()
        {
            // Arrange
            var requestDto = new CreateToDoRequestDto("New Task", "Description", DateTime.Now, DateTime.Now.AddDays(1), DateTime.Now, Priority.High, 1, false, "User123");
            var toDo = new ToDo { Id = Guid.NewGuid(), Title = requestDto.Title, UserId = requestDto.UserId };
            var responseDto = new ToDoResponseDto { Id = toDo.Id, Title = requestDto.Title, UserId = requestDto.UserId };

            _mockBusinessRules.Setup(x => x.ToDoTitleMustBeUnique(requestDto.Title));
            _mockMapper.Setup(m => m.Map<ToDo>(requestDto)).Returns(toDo);
            _mockToDoRepository.Setup(repo => repo.Add(It.IsAny<ToDo>())).Returns(toDo);
            _mockMapper.Setup(m => m.Map<ToDoResponseDto>(toDo)).Returns(responseDto);

            // Act
            var result = await _toDoService.Add(requestDto, requestDto.UserId);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(200, result.Status);
            Assert.AreEqual("ToDo eklendi.", result.Message);
        }

        [Test]
        public void ToDoService_WhenToDoAdded_ThrowsException()
        {
            // Arrange
            var requestDto = new CreateToDoRequestDto("New Task", "Description", DateTime.Now, DateTime.Now.AddDays(1), DateTime.Now, Priority.High, 1, false, "User123");
            _mockBusinessRules.Setup(x => x.ToDoTitleMustBeUnique(requestDto.Title)).Throws(new BusinessException("Title already exists"));

            // Act & Assert
            Assert.ThrowsAsync<BusinessException>(async () => await _toDoService.Add(requestDto, requestDto.UserId));
        }


        [Test]
        public void ToDoService_WhenToDoDeleted_ReturnSuccess()
        {
            // Arrange
            var toDoId = Guid.NewGuid();
            var toDo = new ToDo { Id = toDoId, Title = "Test Task" };

            _mockBusinessRules.Setup(x => x.ToDoIsPresent(toDoId));
            _mockToDoRepository.Setup(repo => repo.GetById(toDoId)).Returns(toDo);
            _mockToDoRepository.Setup(repo => repo.Delete(toDo)).Returns(toDo);

            // Act
            var result = _toDoService.Delete(toDoId);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(204, result.Status);
            Assert.AreEqual("ToDo silindi", result.Message);
        }

        [Test]
        public void ToDoService_WhenToDoDeleted_ThrowsException()
        {
            // Arrange
            var toDoId = Guid.NewGuid();
            _mockBusinessRules.Setup(x => x.ToDoIsPresent(toDoId)).Throws(new NotFoundException("ToDo not found"));

            // Act & Assert
            Assert.Throws<NotFoundException>(() => _toDoService.Delete(toDoId));
        }

        [Test]
        public void ToDoService_WhenToDoUpdated_ReturnSuccess()
        {
            // Arrange
            var updateDto = new UpdateToDoRequestDto(Guid.NewGuid(), "Updated Task", "Updated Description", DateTime.Now, DateTime.Now.AddDays(2), DateTime.Now, Priority.Normal, 1, false, "User123");
            var toDo = new ToDo { Id = updateDto.Id, Title = updateDto.Title, UserId = updateDto.UserId };

            _mockBusinessRules.Setup(x => x.ToDoIsPresent(updateDto.Id));
            _mockToDoRepository.Setup(repo => repo.GetById(updateDto.Id)).Returns(toDo);
            _mockToDoRepository.Setup(repo => repo.Update(toDo)).Returns(toDo);
            _mockMapper.Setup(m => m.Map<ToDoResponseDto>(toDo)).Returns(new ToDoResponseDto { Id = toDo.Id, Title = toDo.Title });

            // Act
            var result = _toDoService.Update(updateDto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(200, result.Status);
            Assert.AreEqual("ToDo Güncellendi.", result.Message);
        }

        [Test]
        public void ToDoService_WhenToDoUpdated_ThrowsException()
        {
            // Arrange
            var updateDto = new UpdateToDoRequestDto(Guid.NewGuid(), "Updated Task", "Updated Description", DateTime.Now, DateTime.Now.AddDays(2), DateTime.Now, Priority.Normal, 1, false, "User123");
            _mockBusinessRules.Setup(x => x.ToDoIsPresent(updateDto.Id)).Throws(new NotFoundException("ToDo not found"));

            // Act & Assert
            Assert.Throws<NotFoundException>(() => _toDoService.Update(updateDto));
        }
    }
}
