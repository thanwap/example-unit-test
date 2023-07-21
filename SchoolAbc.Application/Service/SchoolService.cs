using SchoolAbc.Application.Repository;

namespace SchoolAbc.Application.Service
{
    /*
        โรงเรียนมีคนอยู่ 2 ประเภท
        - ครู
        - นักเรียน
    */
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;

        public SchoolService(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        // Get นักเรียนที่ยังไม่ลาออก
        // Repo ได้คนทุกคนมาจาก database
        public List<Student> GetCurrentStudents()
        {
            return _schoolRepository.GetAllHuman()
               .Where(x => x.HumanType == "Student" && x.DelFlag == false)
               .Select(x => new Student
               {
                   Id = x.Id,
                   Name = x.Name
               }).ToList();

        }

        public Student? GetStudentById(int id)
        {
            return _schoolRepository.GetAllHuman()
                .Where(x =>
                    x.HumanType == "Student"
                    && x.DelFlag == false
                    && x.Id == id)
                .Select(x => new Student
                {
                    Id = x.Id,
                    Name = x.Name
                })
               .FirstOrDefault();
        }
    }

    public interface ISchoolService
    {
        List<Student> GetCurrentStudents();
        Student? GetStudentById(int id);
    }

    public class Human
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string HumanType { get; set; } = ""; //Teacher, Student
        public bool DelFlag { get; set; } // true - ลาออก
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }
}
