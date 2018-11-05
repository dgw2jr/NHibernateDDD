using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDDD.WPF
{
    public class EmployeeViewModel
    {
        public ObservableCollection<Employee> Employees
        {
            get;
            set;
        }

        public void LoadEmployees()
        {
            var employees = new ObservableCollection<Employee>
            {
                Employee.Create("Don", "Woodford", new CEO()).Value
            };

            Employees = employees;
        }
    }
}
