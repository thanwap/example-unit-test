using SchoolAbc.Application.Service;

namespace SchoolAbc.Application.Repository
{
    public class SchoolRepository : ISchoolRepository
    {
        public List<Human> GetAllHuman()
        {
            // using (var connection = new SqlConnection(connectionString))
            // {
            //     var sql = "SELECT * FROM Human";
            //     var students = connection.Query<Customer>(sql);
            // }

            throw new NotImplementedException();
        }
    }

    public interface ISchoolRepository
    {
        List<Human> GetAllHuman();
    }
}
