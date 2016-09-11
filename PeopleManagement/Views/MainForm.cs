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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        
        private PeopleManager ppManager = PeopleManager.getInstance();
        private PrintManager printManager = PrintManager.getInstance();
        private BindingSource PeopleSource;
        private BindingSource BikeSource;

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshAll();

            SearchPanel.Enabled = false;
        }

        #region Init Components
        private void InitPeopleData(List<Person> source)
        {
            try
            {
                PeopleSource = new BindingSource();
                //People Data Grid View
                foreach (Person item in source)
                {
                    PeopleSource.Add(item);
                }
                PeopleGridView.DataSource = PeopleSource;
                PeopleSource.AllowNew = false;

                PeopleGridView.AllowDrop = false;
                PeopleGridView.AllowUserToAddRows = false;
                PeopleGridView.AllowUserToDeleteRows = false;
                PeopleGridView.AllowUserToOrderColumns = false;

                PeopleGridView.Columns[1].Width = 150;
                PeopleGridView.Columns[3].Width = 100;
                PeopleGridView.Columns[0].Visible = false;
                PeopleGridView.Columns[2].Visible = false;
                PeopleGridView.Columns[4].Visible = false;
                PeopleGridView.Columns[5].Visible = false;
                PeopleGridView.Columns[6].Visible = false;
                PeopleGridView.Columns[7].Visible = false;
                PeopleGridView.Columns[8].Visible = false;
            }
            catch
            {

            }
            //for (int i=0;i < PeopleGridView.Columns.Count; ++i)
            //{
            //    PeopleGridView.Columns[i].Visible = false;
            //}
            //if (PeopleGridView.Columns[2])
        }

        private void InitPeopleBinding()
        {
            ClearPeopleBinding();
            //Detail
            PpIdTxt.DataBindings.Add("Text", PeopleSource, "Id");
            NameTxt.DataBindings.Add("Text", PeopleSource, "FullName");
            IDNumberTxt.DataBindings.Add("Text", PeopleSource, "IDCardNumber");
            DayTxt.DataBindings.Add("Text", PeopleSource, "Birth.Day");
            MonthTxt.DataBindings.Add("Text", PeopleSource, "Birth.Month");
            YearTxt.DataBindings.Add("Text", PeopleSource, "Birth.Year");
            PhoneTxt.DataBindings.Add("Text", PeopleSource, "PhoneNumber");
            AddInfoTxt.DataBindings.Add("Text", PeopleSource, "AdditionalInfo");
            AddTxt.DataBindings.Add("Text", PeopleSource, "Address");
            AdditionalAddTxt.DataBindings.Add("Text", PeopleSource, "AdditionalAddress");
        }

        private void ClearPeopleBinding()
        {
            PpIdTxt.DataBindings.Clear();
            NameTxt.DataBindings.Clear();
            IDNumberTxt.DataBindings.Clear();
            DayTxt.DataBindings.Clear();
            MonthTxt.DataBindings.Clear();
            YearTxt.DataBindings.Clear();
            PhoneTxt.DataBindings.Clear();
            AddInfoTxt.DataBindings.Clear();
            AddTxt.DataBindings.Clear();
            AdditionalAddTxt.DataBindings.Clear();
        }

        private void InitBikeData()
        {
            try
            {
                BikeSource = new BindingSource();
                //Bike Data Grid View
                BikeSource.DataSource = PeopleSource;
                BikeSource.DataMember = "Motobikes";
                BikeListBox.DataSource = BikeSource;
                BikeListBox.DisplayMember = "LostDate";

                ClearBikeBinding();
                BikeIdTxt.DataBindings.Add("Text", BikeSource, "Id");
                LostDayTxt.DataBindings.Add("Text", BikeSource, "LostDate.Day");
                LostMonthTxt.DataBindings.Add("Text", BikeSource, "LostDate.Month");
                LostYearTxt.DataBindings.Add("Text", BikeSource, "LostDate.Year");
                TicketNumberTxt.DataBindings.Add("Text", BikeSource, "TicketNumber");
                TypeTxt.DataBindings.Add("Text", BikeSource, "Type");
                BikeID.DataBindings.Add("Text", BikeSource, "IDNumber");
                OwnerNameTxt.DataBindings.Add("Text", BikeSource, "OwnerName");
                BikeAddTxt.DataBindings.Add("Text", BikeSource, "PaperAddress");
                BikeAdd2Txt.DataBindings.Add("Text", BikeSource, "CurrentAddress");
                RegisterNumerTxt.DataBindings.Add("Text", BikeSource, "RegisterNumber");
                RegisterDayTxt.DataBindings.Add("Text", BikeSource, "RegisterDate.Day");
                RegisterMonthTxt.DataBindings.Add("Text", BikeSource, "RegisterDate.Month");
                RegisterYearTxt.DataBindings.Add("Text", BikeSource, "RegisterDate.Year");
            }
            catch
            {

            }
        }

        private void ClearBikeBinding()
        {
            BikeIdTxt.DataBindings.Clear();
            LostDayTxt.DataBindings.Clear();
            LostMonthTxt.DataBindings.Clear();
            LostYearTxt.DataBindings.Clear();
            TicketNumberTxt.DataBindings.Clear();
            TypeTxt.DataBindings.Clear();
            BikeID.DataBindings.Clear();
            OwnerNameTxt.DataBindings.Clear();
            BikeAddTxt.DataBindings.Clear();
            BikeAdd2Txt.DataBindings.Clear();
            RegisterNumerTxt.DataBindings.Clear();
            RegisterDayTxt.DataBindings.Clear();
            RegisterMonthTxt.DataBindings.Clear();
            RegisterYearTxt.DataBindings.Clear();
        }

        private void RefreshAll()
        {
            InitPeopleData(ppManager.PersonList);
            InitPeopleBinding();
            InitBikeData();
        }
        #endregion

        #region
        
        #endregion

        #region Function method
        private AddForm CurrentAddForm;
        private void AddPeopleBtn_Click(object sender, EventArgs e)
        {
            CurrentAddForm = new AddForm();
            CurrentAddForm.AddSuccessfulEvent += newForm_AddSuccessfulEvent;
            CurrentAddForm.ShowDialog();
        }

        private void newForm_AddSuccessfulEvent(AddForm sender)
        {
            CurrentAddForm.AddSuccessfulEvent -= newForm_AddSuccessfulEvent;
            RefreshAll();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            SearchPanel.Enabled = !SearchPanel.Enabled;
        }

        private void AddBikeBtn_Click(object sender, EventArgs e)
        {
            if (PeopleGridView.CurrentRow == null)
            {
                MessageBox.Show("Hãy chọn 1 người mất xe trước khi thêm");
                return;
            }
            int row = PeopleGridView.CurrentRow.Index;
            int id = Convert.ToInt32(PeopleGridView[0, row].Value);
            Person person = ppManager.GetPersonById(id);
            CurrentAddForm = new AddForm(person);
            CurrentAddForm.AddSuccessfulEvent += newForm_AddSuccessfulEvent;
            CurrentAddForm.ShowDialog();
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (PeopleGridView.CurrentRow == null)
            {
                MessageBox.Show("Hãy chọn 1 người mất xe trước khi thêm");
                return;
            }
            int row = PeopleGridView.CurrentRow.Index;
            int id = Convert.ToInt32(PeopleGridView[0, row].Value);
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa người này khỏi danh sách", "Xóa", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                ppManager.DeletePerson(id);
                RefreshAll();
            }
        }

        private void DelBikeBtn_Click(object sender, EventArgs e)
        {
            if (BikeListBox.SelectedItem == null)
            {
                MessageBox.Show("Hãy chọn 1 người mất xe trước khi thêm");
                return;
            }
            Motobike bike = (Motobike) BikeListBox.SelectedItem;
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa người này khỏi danh sách", "Xóa", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                ppManager.DeleteBike(bike.Id, bike.LostID.Value);
                RefreshAll();
            }
        }
        #endregion

        #region Other Event
        private void SearchTxt_TextChanged(object sender, EventArgs e)
        {
            string name = SearchNameTxt.Text;
            string idNum = SearchIdNumberTxt.Text;
            string addInfo = SearchAddInfoTxt.Text;
            string bikeId = SearchBikeIdTxt.Text;

            //PeopleGridView.DataSource = null;
            //PeopleGridView.Rows.Clear();
            InitPeopleData(ppManager.SearchPeople(name, idNum, addInfo, bikeId));
        }

        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ppManager.CloseDB();
        }

        #endregion

        private void UpdateBtn_Click(object sender, EventArgs e)
        {

        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            printManager.Print(int.Parse(PpIdTxt.Text), int.Parse(BikeIdTxt.Text));
            //printManager.Print(this.CreateGraphics(), this.Size, this.Location);
        }
    }
}
