using EmployeeApi.Abstractions;

namespace EmployeeApi
{
    public class EmployeeRepository:IRepository<Employee>
    {
        private readonly List<Employee> _employees = new();

        public Employee? GetById(int id)
        {
            return _employees.FirstOrDefault(e => e.id == id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employees;
        }

        public void Create(Employee entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entity.id = _employees.Select(e => e.id).DefaultIfEmpty(0).Max() + 1;
            _employees.Add(entity);
        }

        public void Update(Employee entity)
        {
            if(entity==null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var existingEmployee = GetById(entity.id);
            if(existingEmployee!=null)
            {
                existingEmployee.FirstName = entity.FirstName;
                existingEmployee.LastName = entity.LastName;

            }
        }

        public void Delete(Employee entity)
        {
            if(entity==null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _employees.Remove(entity);
        }
    }
}
