using PeopleManagement.Helpers;
using PeopleManagement.Manager;
using PeopleManagement.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PeopleManagement.Views
{
    public partial class AddForm : Form
    {
        private PeopleManager ppManager = PeopleManager.getInstance();

        public delegate void AddSuccessfulHandler(AddForm sender);
        public event AddSuccessfulHandler AddSuccessfulEvent;

        private Person CurrentPerson;

        public AddForm(Person _person = null)
        {
            InitializeComponent();
            CurrentPerson = _person;
            InitPerson();
        }

        private void InitPerson()
        {
            if (CurrentPerson == null)
                return;
            NameTxt.Text = CurrentPerson.FullName;
            IDNumberTxt.Text = CurrentPerson.IDCardNumber;
            if (CurrentPerson.Birth.HasValue)
            {
                DateTime birth = CurrentPerson.Birth.Value;
                DayTxt.Text = birth.Day.ToString();
                MonthTxt.Text = birth.Month.ToString();
                YearTxt.Text = birth.Year.ToString();
            }
            PhoneTxt.Text = CurrentPerson.PhoneNumber;
            AddInfoTxt.Text = CurrentPerson.AdditionalInfo;
            AddTxt.Text = CurrentPerson.Address;
            AdditionalAddTxt.Text = CurrentPerson.AdditionalAddress;

            PeopleInfoGroup.Enabled = false;
        }

        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        #region Function method
        private void CheckInfoBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            

            Motobike motobike = PeopleInfoHelper.CreateBike(TicketNumberTxt.Text, TypeTxt.Text, 
                IDNumberTxt.Text, OwnerNameTxt.Text, BikeAddTxt.Text, BikeAdd2Txt.Text, RegisterNumerTxt.Text,
                RegisterDayTxt.Text, RegisterMonthTxt.Text, RegisterYearTxt.Text, 
                LostDayTxt.Text, LostMonthTxt.Text, LostYearTxt.Text);

            if (motobike == null)
            {
                MessageBox.Show("Xin nhập đủ thông tin xe bị mất phiếu");
                return;
            }

            if (CurrentPerson == null)
            {
                Person person = PeopleInfoHelper.CreatePeople(NameTxt.Text, YearTxt.Text, MonthTxt.Text, DayTxt.Text, IDNumberTxt.Text, PhoneTxt.Text, AddInfoTxt.Text, AddTxt.Text, AdditionalAddTxt.Text);
                if (person == null)
                {
                    MessageBox.Show("Xin nhập đủ thông tin người mất phiếu xe");
                    return;
                }
                int personID = ppManager.AddPerson(person);
                person.Motobikes.Add(motobike);
                motobike.LostID = personID;
            }
            else
            {
                CurrentPerson.Motobikes.Add(motobike);
                motobike.LostID = CurrentPerson.Id;
            }

            ppManager.AddMotobike(motobike);

            if (AddSuccessfulEvent != null)
            {
                AddSuccessfulEvent(this);
            }
            this.Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void IDNumberTxt_TextChanged(object sender, EventArgs e)
        {
            if (CurrentPerson != null)
                return;
            string idNumber = IDNumberTxt.Text;
            if (ppManager.IsPersonExisted(idNumber) != null)
            {
                NotifyLabel.Visible = true;
                AddBtn.Enabled = false;
                BikeInfoGroup.Enabled = false;
            }
            else
            {
                NotifyLabel.Visible = false;
                AddBtn.Enabled = true;
                BikeInfoGroup.Enabled = true;
            }
        }
        #endregion 
    }
}
