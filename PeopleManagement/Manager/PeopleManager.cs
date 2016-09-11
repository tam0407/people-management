using PeopleManagement.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace PeopleManagement.Manager
{
    public class PeopleManager
    {
        private SQLiteConnection myDbconnection;
        public const string connectionString = "Data Source=PeopleDB.db;version=3;new=False;datetimeformat=CurrentCulture";

        private static PeopleManager currentObject = null;
        public static PeopleManager getInstance()
        {
            if (currentObject == null)
                currentObject = new PeopleManager();
            return currentObject;
        }

        public List<Person> PersonList;
        public List<Motobike> MotobikeList;

        private PeopleManager()
        {
            myDbconnection = new SQLiteConnection(connectionString);
            myDbconnection.Open();
            PersonList = GetAllPeople();
        }

        public void CloseDB()
        {
            myDbconnection.Close();
        }

        public List<Person> GetAllPeople()
        {
            try
            {
                string sql = "select * from People";
                SQLiteCommand cmd = new SQLiteCommand(sql, myDbconnection);

                List<Person> list = new List<Person>();
                SQLiteDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(CreatePerson(dr));
                }
                return list;
            }
            catch
            {
                return null;
            }
        }

        public Person GetPersonById(int Id)
        {
            return PersonList.FirstOrDefault(p => p.Id == Id);
        }

        public Motobike GetBikeById(int Id)
        {
            return MotobikeList.FirstOrDefault(b => b.Id == Id);
        }

        public List<Person> SearchPeople(string name, string idNum, string addInfo, string bikeId)
        {
            IEnumerable<Person> result = new List<Person>(PersonList);
            if (!string.IsNullOrEmpty(name))
                result = result.Where(p => p.FullName.ToLower().Contains(name.ToLower()));
            if (!string.IsNullOrEmpty(idNum))
                result = result.Where(p => p.IDCardNumber.Contains(idNum));
            if (!string.IsNullOrEmpty(addInfo))
                result = result.Where(p => p.AdditionalInfo.ToLower().Contains(addInfo.ToLower()));
            if (!string.IsNullOrEmpty(bikeId))
                result = result.Where(p => p.Motobikes.Any(m => m.IDNumber.ToLower().Contains(bikeId.ToLower())));

            return result.ToList();
        }

        public Person IsPersonExisted(string idNumber)
        {
            return PersonList.FirstOrDefault(p => p.IDCardNumber == idNumber);
        }

        public void AddMotobike(Motobike motobike)
        {
            try
            {
                string sql = "insert into Motobike (TicketNumber,Type,IDNumber,OwnerName,PaperAddress,CurrentAddress,RegisterNumber,RegisterDate,LostDate,LostID) " +
                                        "values (@ticketNumber,@type,@idNumber,@ownerName,@paperAdd,@currentAdd,@registerNumber,@registerDate,@lostDate,@lostID)";

                SQLiteCommand cmd = new SQLiteCommand(sql, myDbconnection);
                cmd.Parameters.AddWithValue("@ticketNumber", motobike.TicketNumber);
                cmd.Parameters.AddWithValue("@type", motobike.Type);
                cmd.Parameters.AddWithValue("@idNumber", motobike.IDNumber);
                cmd.Parameters.AddWithValue("@ownerName", motobike.OwnerName);
                cmd.Parameters.AddWithValue("@paperAdd", motobike.PaperAddress);
                cmd.Parameters.AddWithValue("@currentAdd", motobike.CurrentAddress);
                cmd.Parameters.AddWithValue("@registerNumber", motobike.RegisterNumber);
                cmd.Parameters.AddWithValue("@registerDate", motobike.RegisterDate);
                cmd.Parameters.AddWithValue("@lostDate", motobike.LostDate);
                cmd.Parameters.AddWithValue("@lostID", motobike.LostID);

                cmd.ExecuteNonQuery();
                //MotobikeList.Add(motobike);
            }
            catch
            {
                
            }
        }

        public int AddPerson(Person person)
        {
            try
            {
                string sql = "insert into People (FullName,Birth,IDCardNumber,PhoneNumber,AdditionalInfo,Address,AdditionalAddress) " +
                                        "values (@name,@birth,@idNumber,@phoneNumber,@additionalInfo,@address,@additionalAdd)";

                SQLiteCommand cmd = new SQLiteCommand(sql, myDbconnection);
                cmd.Parameters.AddWithValue("@name", person.FullName);
                cmd.Parameters.AddWithValue("@birth", person.Birth);
                cmd.Parameters.AddWithValue("@idNumber", person.IDCardNumber);
                cmd.Parameters.AddWithValue("@phoneNumber", person.PhoneNumber);
                cmd.Parameters.AddWithValue("@additionalInfo", person.AdditionalInfo);
                cmd.Parameters.AddWithValue("@address", person.Address);
                cmd.Parameters.AddWithValue("@additionalAdd", person.AdditionalAddress);

                cmd.ExecuteNonQuery();

                SQLiteCommand cmd2 = new SQLiteCommand("select Id from People where IDCardNumber='" + person.IDCardNumber + "'", myDbconnection);
                SQLiteDataReader reader = cmd2.ExecuteReader();
                reader.Read();
                int Id = Convert.ToInt32(reader["Id"].ToString());
                person.Id = Id;
                PersonList.Add(person);
                return Id;
            }
            catch
            {
                return -1;
            }
        }

        public List<Motobike> GetBikeList(int PersonID)
        {
            try
            {
                string sql = "select * from Motobike where LostID = \'" + PersonID + "\'";
                SQLiteCommand cmd = new SQLiteCommand(sql, myDbconnection);

                List<Motobike> list = new List<Motobike>();
                SQLiteDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(CreateMotobike(dr));
                }
                return list;
            }
            catch
            {
                return null;
            }
        }

        public void DeletePerson(int id)
        {
            try
            {
                string sql = "delete from People where Id=" + id.ToString();
                SQLiteCommand cmd = new SQLiteCommand(sql, myDbconnection);
                cmd.ExecuteNonQuery();

                PersonList.Remove(PersonList.First(p => p.Id == id));
            }
            catch
            {
                
            }
        }

        public void DeleteBike(int id, int lostID)
        {
            try
            {
                string sql = "delete from Motobike where Id=" + id.ToString();
                SQLiteCommand cmd = new SQLiteCommand(sql, myDbconnection);
                cmd.ExecuteNonQuery();

                Person person = GetPersonById(lostID);
                person.Motobikes.Remove(person.Motobikes.First(m => m.Id == id));
            }
            catch
            {

            }
        }

        private Person CreatePerson(SQLiteDataReader reader)
        {
            Person person = new Person();
            person.Id = int.Parse(reader["Id"].ToString());
            person.FullName = reader["FullName"].ToString();
            person.Address = reader["Address"].ToString();
            person.PhoneNumber = reader["PhoneNumber"].ToString();
            person.AdditionalInfo = reader["AdditionalInfo"].ToString();
            person.AdditionalAddress = reader["AdditionalAddress"].ToString();
            person.Birth = Convert.ToDateTime(reader["Birth"].ToString());
            person.IDCardNumber = reader["IDCardNumber"].ToString();
            person.Motobikes = GetBikeList(person.Id);
            return person;
        }

        private Motobike CreateMotobike(SQLiteDataReader reader)
        {
            Motobike bike = new Motobike();
            bike.Id = int.Parse(reader["Id"].ToString());
            bike.IDNumber = reader["IdNumber"].ToString();
            bike.CurrentAddress = reader["CurrentAddress"].ToString();
            bike.OwnerName = reader["OwnerName"].ToString();
            bike.PaperAddress = reader["PaperAddress"].ToString();
            bike.RegisterNumber = reader["RegisterNumber"].ToString();
            bike.TicketNumber = reader["TicketNumber"].ToString();
            bike.Type = reader["Type"].ToString();
            bike.RegisterDate = Convert.ToDateTime(reader["RegisterDate"].ToString());
            bike.LostID = int.Parse(reader["LostID"].ToString());
            bike.LostDate = Convert.ToDateTime(reader["LostDate"].ToString());
            return bike;
        }
    }
}
