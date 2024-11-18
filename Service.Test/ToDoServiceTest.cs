//using AutoMapper;
//using Core.Exceptions;
//using Microsoft.Extensions.Logging;
//using Moq;
//using ToDoApp.Models.Entities;
//using ToDoApp.Models.ToDos;
//using ToDoApp.Repository.Repositories.Abstracts;
//using ToDoApp.Service.Concretes;
//using ToDoApp.Service.Rules;
//using ToDoApp.Service.Constants;

//namespace ToDoApp.Tests.Services
//{
//    [TestFixture]
//    public class ToDoServiceTests
//    {
//        private Mock<IToDoRepository> _mockToDoRepository;
//        private Mock<IMapper> _mockMapper;
//        private Mock<ILogger<ToDoBusinessRules>> _mockLogger;
//        private ToDoBusinessRules _toDoBusinessRules;
//        private ToDoService _toDoService;

//        [SetUp]
//        public void Setup()
//        {
//            _mockToDoRepository = new Mock<IToDoRepository>();
//            _mockMapper = new Mock<IMapper>();
//            _mockLogger = new Mock<ILogger<ToDoBusinessRules>>();

//            // ToDoBusinessRules sınıfını, mocklanan repository ile başlatıyoruz
//            _toDoBusinessRules = new ToDoBusinessRules(_mockToDoRepository.Object);

//            // ToDoService'i, ToDoBusinessRules ile başlatıyoruz
//            _toDoService = new ToDoService(_mockToDoRepository.Object, _mockMapper.Object, _toDoBusinessRules);
//        }

//        [Test]
//        public void ToDoService_WhenToDoAdded_ReturnSuccess()
//        {
//            // Arrange
//            var requestDto = new CreateToDoRequestDto("Yeni Görev", "Açıklama", DateTime.Now, DateTime.Now.AddDays(1), DateTime.Now, Priority.High, 1, false, "User123");
//            var toDo = new ToDo { Id = Guid.NewGuid(), Title = requestDto.Title, UserId = requestDto.UserId };
//            var responseDto = new ToDoResponseDto { Id = toDo.Id, Title = requestDto.Title, UserId = requestDto.UserId };

//            _mockToDoRepository.Setup(x => x.Add(It.IsAny<ToDo>())).Returns(toDo);
//            _mockMapper.Setup(m => m.Map<ToDo>(requestDto)).Returns(toDo);
//            _mockToDoRepository.Setup(repo => repo.Add(It.IsAny<ToDo>())).Returns(toDo);
//            _mockMapper.Setup(m => m.Map<ToDoResponseDto>(toDo)).Returns(responseDto);

//            // Act
//            var result = _toDoService.Add(requestDto, requestDto.UserId);

//            // Assert
//            Assert.IsTrue(result.IsCompletedSuccessfully);
//            Assert.AreEqual( , result.Status);
//            Assert.AreEqual("ToDo eklendi.", result.Message);
//        }

//        [Test]
//        public void ToDoService_WhenToDoTitleNotUnique_ThrowsBusinessException()
//        {
//            // Arrange
//            var title = "Mevcut Görev";
//            _mockToDoRepository.Setup(x => x.GetAll(It.IsAny<Func<ToDo, bool>>())).Returns(new List<ToDo>
//            {
//                new ToDo { Title = title }
//            });

//            // Act & Assert
//            Assert.Throws<BusinessException>(() => _toDoBusinessRules.ToDoTitleMustBeUnique(title));
//        }

//        [Test]
//        public void ToDoService_WhenToDoTitleIsUnique_DoesNotThrowException()
//        {
//            // Arrange
//            var title = "Benzersiz Görev";
//            _mockToDoRepository.Setup(x => x.GetAll(It.IsAny<Func<ToDo, bool>>())).Returns(new List<ToDo>());

//            // Act & Assert
//            Assert.DoesNotThrow(() => _toDoBusinessRules.ToDoTitleMustBeUnique(title));
//        }

//        [Test]
//        public void ToDoService_WhenToDoNotFound_ThrowsNotFoundException()
//        {
//            // Arrange
//            var toDoId = Guid.NewGuid();
//            _mockToDoRepository.Setup(x => x.GetById(toDoId)).Returns((ToDo)null);

//            // Act & Assert
//            Assert.Throws<NotFoundException>(() => _toDoBusinessRules.ToDoIsPresent(toDoId));
//        }

//        [Test]
//        public void ToDoService_WhenToDoFound_ReturnsTrue()
//        {
//            // Arrange
//            var toDoId = Guid.NewGuid();
//            var toDo = new ToDo { Id = toDoId, Title = "Mevcut Görev" };
//            _mockToDoRepository.Setup(x => x.GetById(toDoId)).Returns(toDo);

//            // Act
//            var result = _toDoBusinessRules.ToDoIsPresent(toDoId);

//            // Assert
//            Assert.IsTrue(result);
//        }

//        [Test]
//        public void ToDoService_WhenToDoAdded_ThrowsException()
//        {
//            // Arrange
//            var requestDto = new CreateToDoRequestDto("Yeni Görev", "Açıklama", DateTime.Now, DateTime.Now.AddDays(1), DateTime.Now, Priority.High, 1, false, "User123");
//            _toDoBusinessRules.Setup(x => x.ToDoTitleMustBeUnique(requestDto.Title)).Throws(new BusinessException("Başlık zaten mevcut"));

//            // Act & Assert
//            Assert.ThrowsAsync<BusinessException>(async () => await _toDoService.Add(requestDto, requestDto.UserId));
//        }

//        [Test]
//        public void ToDoService_WhenToDoDeleted_ReturnSuccess()
//        {
//            // Arrange
//            var toDoId = Guid.NewGuid();
//            var toDo = new ToDo { Id = toDoId, Title = "Test Görev" };

//            _toDoBusinessRules.Setup(x => x.ToDoIsPresent(toDoId));
//            _mockToDoRepository.Setup(repo => repo.GetById(toDoId)).Returns(toDo);
//            _mockToDoRepository.Setup(repo => repo.Delete(toDo)).Returns(toDo);

//            // Act
//            var result = _toDoService.Delete(toDoId);

//            // Assert
//            Assert.IsTrue(result.Success);
//            Assert.AreEqual(204, result.Status);
//            Assert.AreEqual("ToDo silindi", result.Message);
//        }

//        [Test]
//        public void ToDoService_WhenToDoDeleted_ThrowsException()
//        {
//            // Arrange
//            var toDoId = Guid.NewGuid();
//            _toDoBusinessRules.Setup(x => x.ToDoIsPresent(toDoId)).Throws(new NotFoundException("ToDo bulunamadı"));

//            // Act & Assert
//            Assert.Throws<NotFoundException>(() => _toDoService.Delete(toDoId));
//        }

//        [Test]
//        public void ToDoService_WhenToDoUpdated_ReturnSuccess()
//        {
//            // Arrange
//            var updateDto = new UpdateToDoRequestDto(Guid.NewGuid(), "Güncellenmiş Görev", "Güncellenmiş Açıklama", DateTime.Now, DateTime.Now.AddDays(2), DateTime.Now, Priority.Normal, 1, false, "User123");
//            var toDo = new ToDo { Id = updateDto.Id, Title = updateDto.Title, UserId = updateDto.UserId };

//            _toDoBusinessRules.Setup(x => x.ToDoIsPresent(updateDto.Id));
//            _mockToDoRepository.Setup(repo => repo.GetById(updateDto.Id)).Returns(toDo);
//            _mockToDoRepository.Setup(repo => repo.Update(toDo)).Returns(toDo);
//            _mockMapper.Setup(m => m.Map<ToDoResponseDto>(toDo)).Returns(new ToDoResponseDto { Id = toDo.Id, Title = toDo.Title });

//            // Act
//            var result = _toDoService.Update(updateDto);

//            // Assert
//            Assert.IsTrue(result.Success);
//            Assert.AreEqual(200, result.Status);
//            Assert.AreEqual("ToDo Güncellendi.", result.Message);
//        }

//        [Test]
//        public void ToDoService_WhenToDoUpdated_ThrowsException()
//        {
//            // Arrange
//            var updateDto = new UpdateToDoRequestDto(Guid.NewGuid(), "Güncellenmiş Görev", "Güncellenmiş Açıklama", DateTime.Now, DateTime.Now.AddDays(2), DateTime.Now, Priority.Normal, 1, false, "User123");
//            _toDoBusinessRules.Setup(x => x.ToDoIsPresent(updateDto.Id)).Throws(new NotFoundException("ToDo bulunamadı"));

//            // Act & Assert
//            Assert.Throws<NotFoundException>(() => _toDoService.Update(updateDto));
//        }
//    }
//}
