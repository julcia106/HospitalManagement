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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            HospitalManagementDBEntities db = new HospitalManagementDBEntities();

            var docs = from d in db.Doctor
                       select new
                       {
                           DoctorName = d.Name,
                           Speciality = d.Specialization,
                       };

            this.gridDoctors.ItemsSource = docs.ToList();
        }

        // Add - do the insertion operation
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            HospitalManagementDBEntities db = new HospitalManagementDBEntities();

            Doctor doctorObject = new Doctor()
            {
                Name = txtName.Text,
                Qualification = txtQualification.Text,
                Specialization = txtSpecialization.Text,
                Age= int.Parse(txtAge.Text)
            };

            db.Doctor.Add(doctorObject);
            db.SaveChanges();
        }

        // Load, refresh the database
        private void btnLoadDoctors_Click(object sender, RoutedEventArgs e)
        {
            HospitalManagementDBEntities db = new HospitalManagementDBEntities();

            this.gridDoctors.ItemsSource = db.Doctor.ToList();
        }

        // Track the Doctor id which I want to select and update
        private int updatingDoctorId = 0;

        // When record is selected - he will appear in update section
        private void gridDoctors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridDoctors.SelectedIndex >= 0)
            {
                if (this.gridDoctors.SelectedItems.Count >= 0)
                {
                    if (this.gridDoctors.SelectedItems[0].GetType() == typeof(Doctor))
                    {
                        Doctor d = (Doctor)this.gridDoctors.SelectedItems[0];

                        this.txtName2.Text = d.Name;
                        this.txtSpecialization2.Text = d.Specialization;
                        this.txtQualification2.Text = d.Qualification;
                        this.txtAge2.Text = d.Age.ToString();

                        this.updatingDoctorId = d.Id;
                    }
                }
            }
        }

        // Update the record
        private void btnUpdateDoctor_Click(object sender, RoutedEventArgs e)
        {
            HospitalManagementDBEntities db = new HospitalManagementDBEntities();

            var r = from d in db.Doctor
                    where d.Id == this.updatingDoctorId
                    select d;

            Doctor obj = r.SingleOrDefault();

            if (obj != null)
            {
                obj.Name = this.txtName2.Text;
                obj.Specialization = this.txtSpecialization2.Text;
                obj.Qualification = this.txtQualification2.Text;
                obj.Age = int.Parse(this.txtAge2.Text);

                db.SaveChanges();
            }

        }

        // Delete the record
        private void btnDeleteDoctor_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show("Are you sure you want to delete?",
                "Delete Doctor",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.No
            );

            if (msgBoxResult == MessageBoxResult.Yes)
            {
                HospitalManagementDBEntities db = new HospitalManagementDBEntities();

                var r = from d in db.Doctor
                        where d.Id == this.updatingDoctorId
                        select d;

                Doctor obj = r.SingleOrDefault();

                if (obj != null)
                {
                    db.Doctor.Remove(obj);
                    db.SaveChanges();
                }
            }
        }
    }
}
