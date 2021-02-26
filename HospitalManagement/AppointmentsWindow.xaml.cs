using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HospitalManagement
{
    /// <summary>
    /// Interaction logic for AppointmentsWindow.xaml
    /// </summary>
    public partial class AppointmentsWindow : Window
    {
        public AppointmentsWindow()
        {
            InitializeComponent();

            HospitalManagementDBEntities db = new HospitalManagementDBEntities();

            //Inner join
            var result = from a in db.Appointment
                         select new
                         {
                             a.DoctorID,
                             a.Doctor.Name,
                             a.Doctor.Specialization,
                             a.PatientID,
                             a.Patient.FirstName,
                             a.Patient.SecondName,
                             a.Patient.PhoneNumber,
                             a.AppointmentDate
                         };

            //Outer join
            var resultOuterDoctor = from d in db.Doctor
                                    from a in db.Appointment.DefaultIfEmpty()
                                    select new
                                    {
                                        d.Name,
                                        a.Id,
                                        a.AppointmentDate,
                                        Patient = a.Patient.FirstName
                                    };

            this.gridAppointments.ItemsSource = resultOuterDoctor.ToList();
        }
    }
}
