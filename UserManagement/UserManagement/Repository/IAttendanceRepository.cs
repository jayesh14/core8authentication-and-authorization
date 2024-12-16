using UserManagement.Model;

namespace UserManagement.Repository
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> GetAttendancesAsync();
        Task<Attendance> GetAttendanceByIdAsync(int id);
        Task AddAttendanceAsync(Attendance attendance);

        Task UpdateAttendanceAsync(Attendance attendance);

        Task DeleteAttendanceAsync(int id);
    }
}
