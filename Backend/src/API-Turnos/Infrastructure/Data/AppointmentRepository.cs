using System.Security.Cryptography.X509Certificates;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class AppointmentRepository : EfRepository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(ApplicationContext context) : base(context)
    {
    }

    public override List<Appointment> GetAll()
    {
        return _context.Appointments
        .Include(a => a.Medic)
            .ThenInclude(m => m.Specialties)
        .Include(a => a.Patient)
        .Include(a => a.MedicalCenter)
        .ToList();
    }

    public bool Exists(DateTime appointmentDateTime, int medicId, int medicalCenterId)
    {
        return _context.Appointments
            .Any(a => a.AppointmentDateTime == appointmentDateTime &&
                      a.Medic.Id == medicId &&
                      a.MedicalCenter.Id == medicalCenterId);
    }
}
