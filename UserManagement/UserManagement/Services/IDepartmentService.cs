using UserManagement.Model;

namespace UserManagement.Services
{
    public interface IDepartmentService
    {
       public Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        public Task<Department> GetDepartmentByIdAsync(int id);
        public Task AddDepartmentAsync(Department department);
        public Task UpdateDepartmentAsync(Department department);
        public Task DeleteDepartmentAsync(int id);
    }
}
