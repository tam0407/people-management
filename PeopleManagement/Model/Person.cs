namespace PeopleManagement.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Person
    {
        public Person()
        {
            Motobikes = new List<Motobike>();
        }

        public int Id { get; set; }

        public string FullName { get; set; }

        public DateTime? Birth { get; set; }

        public string IDCardNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string AdditionalInfo { get; set; }

        public string Address { get; set; }

        public string AdditionalAddress { get; set; }

        public List<Motobike> Motobikes { get; set; }
    }
}
