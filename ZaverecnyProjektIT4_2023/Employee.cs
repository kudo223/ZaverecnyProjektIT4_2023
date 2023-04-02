using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZaverecnyProjektIT4_2023
{
    public class Employee
    {
        public int Id { get; }
        public int PersonalNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email{ get; set; }
        public string PhoneNumber { get; set; }
        SqlRepository sqlRepository = new SqlRepository();



        public Employee(int id, int personalNumber, string firstname, string lastname, DateTime birthDate, string email, string phoneNumber)
        {
            Id = id;
            Firstname = firstname;
            Lastname = firstname;
            BirthDate = birthDate;
            Email = email;
            PhoneNumber = phoneNumber;
        }
        public ListViewItem EmployeeToListViewItem()
        {
            return new ListViewItem(new string[] { Id.ToString(),PersonalNumber.ToString(), Firstname, Lastname, BirthDate.ToString("dd.MM.yyyy"), Email, PhoneNumber });
        }
    }
}
