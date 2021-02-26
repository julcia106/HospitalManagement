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
    /// Interaction logic for SubWindow.xaml
    /// </summary>
    public partial class SubWindow : Window
    {
        public SubWindow()
        {
            InitializeComponent();

            HospitalManagementDBEntities db = new HospitalManagementDBEntities();

            var diagnosis = from d in db.Diagnosis
                            select new
                            {
                              Patient_ID = d.Id,
                              Disease_Name = d.DiseaseName,
                              Disease_Code = d.DiseaseCode,
                              Date_Of_Diagnosis = d.DiagnosisDate,
                              Patient_Medicines = d.Medicines
                          };

            this.gridDiagnosis.ItemsSource = diagnosis.ToList();
        }
    }
}
