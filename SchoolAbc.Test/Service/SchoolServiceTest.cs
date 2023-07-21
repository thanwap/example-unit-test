using System;
using Moq;
using SchoolAbc.Application.Repository;
using SchoolAbc.Application.Service;

namespace SchoolAbc.Test.Service
{
    public class SchoolServiceTest
    {
        private readonly Mock<ISchoolRepository> _mockSchoolRepository = new();
        private readonly ISchoolService _service;

        public SchoolServiceTest()
        {
            _service = new SchoolService(_mockSchoolRepository.Object);
        }

        [Fact]
        public void GetCurrentStudents_Should_Return_Students()
        {
            //Arrange
            // มีนักเรียน 2 คน
            // - Bow
            // - Bas
            _mockSchoolRepository.Setup(x => x.GetAllHuman())
                .Returns(new List<Human>
                {
                    new Human { Id = 1, Name = "Bow", HumanType = "Student" },
                    new Human { Id = 2, Name = "Bas", HumanType = "Student" }
                });

            //Act
            var students = _service.GetCurrentStudents();

            //Assert
            Assert.Equal(2, students.Count);

            Assert.Equal(1, students[0].Id);
            Assert.Equal("Bow", students[0].Name);

            Assert.Equal(2, students[1].Id);
            Assert.Equal("Bas", students[1].Name);
        }

        [Fact]
        public void GetCurrentStudents_Should_Return_Students_When_SchoolHaveStudentsAndTeachers()
        {
            //Arrange
            _mockSchoolRepository.Setup(x => x.GetAllHuman())
            .Returns(new List<Human>
            {
                new Human { Id = 1, Name = "Bow", HumanType = "Student" },
                new Human { Id = 2, Name = "Bas", HumanType = "Student" },
                new Human { Id = 2, Name = "LookGade", HumanType = "Teacher" },
            });

            //Act
            var students = _service.GetCurrentStudents();

            //Assert
            Assert.Equal(2, students.Count);
        }

        [Fact]
        public void GetCurrentStudents_Should_Return_CurrentStudents_When_SchoolHaveResignStudents()
        {
            //Arrange
            _mockSchoolRepository.Setup(x => x.GetAllHuman())
            .Returns(new List<Human>
            {
                new Human { Id = 1, Name = "Bow", HumanType = "Student", DelFlag = false },
                new Human { Id = 2, Name = "Bas", HumanType = "Student", DelFlag = true },
                new Human { Id = 2, Name = "LookGade", HumanType = "Teacher", DelFlag = false },
            });

            //Act
            var students = _service.GetCurrentStudents();

            //Assert
            Assert.Single(students);
        }

        [Fact]
        public void GetCurrentStudents_Should_Return_EmptyStudents_When_SchoolHaveNobody()
        {
            //Arrange
            _mockSchoolRepository.Setup(x => x.GetAllHuman())
            .Returns(new List<Human>());

            //Act
            var students = _service.GetCurrentStudents();

            //Assert
            Assert.Empty(students);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void GetStudentById_Should_Return_StudentById(int id, int expected)
        {
            //Arrange
            _mockSchoolRepository.Setup(x => x.GetAllHuman())
            .Returns(new List<Human> {
                new Human { Id = 1, Name = "Bow", HumanType = "Student", DelFlag = false },
                new Human { Id = 2, Name = "Bas", HumanType = "Student", DelFlag = false },
            });

            //Act
            var student = _service.GetStudentById(id);

            //Assert
            Assert.Equal(expected, student!.Id);
        }
    }
}
