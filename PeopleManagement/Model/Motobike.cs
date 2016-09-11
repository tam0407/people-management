namespace PeopleManagement.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Motobike
    {
        public int Id { get; set; }

        public string TicketNumber { get; set; }

        public string Type { get; set; }

        public string IDNumber { get; set; }

        public string OwnerName { get; set; }

        public string PaperAddress { get; set; }

        public string CurrentAddress { get; set; }

        public string RegisterNumber { get; set; }

        public DateTime? RegisterDate { get; set; }

        public DateTime? LostDate { get; set; }

        public int? LostID { get; set; }
    }
}
