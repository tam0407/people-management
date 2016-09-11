using PeopleManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagement.Helpers
{
    public static class PeopleInfoHelper
    {
        public static Person CreatePeople(string name, string year, string month, string day, string idNumber, string phone, string additionalInfo, string address, string address2)
        {
            try
            {
                if (String.IsNullOrEmpty(name))
                    return null;
                if (String.IsNullOrEmpty(idNumber))
                    return null;
                if (String.IsNullOrEmpty(year))
                    return null;
                if (String.IsNullOrEmpty(month))
                    return null;
                if (String.IsNullOrEmpty(day))
                    return null;
                if (String.IsNullOrEmpty(address))
                    return null;

                if (phone == null)
                    phone = "";
                if (additionalInfo == null)
                    additionalInfo = "";
                if (address2 == null)
                    address2 = "";

                return new Person()
                {
                    FullName = name,
                    Birth = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day)),
                    IDCardNumber = idNumber,
                    PhoneNumber = phone,
                    AdditionalInfo = additionalInfo,
                    Address = address,
                    AdditionalAddress = address2
                };
            }
            catch
            {
                return null;
            }
        }

        public static Motobike CreateBike(string ticketNumber, string type, string idNumber, 
            string ownerName, string paperAdd, string currentAdd, string registerNumber,
            string registerDay, string registerMonth, string registerYear, 
            string lostDay, string lostMonth, string lostYear)
        {
            try
            {
                if (String.IsNullOrEmpty(ticketNumber))
                    return null;
                if (String.IsNullOrEmpty(type))
                    return null;
                if (String.IsNullOrEmpty(idNumber))
                    return null;
                if (String.IsNullOrEmpty(ownerName))
                    return null;
                if (String.IsNullOrEmpty(paperAdd))
                    return null;
                if (String.IsNullOrEmpty(registerNumber))
                    return null;
                if (String.IsNullOrEmpty(registerDay))
                    return null;
                if (String.IsNullOrEmpty(registerMonth))
                    return null;
                if (String.IsNullOrEmpty(registerYear))
                    return null;
                if (String.IsNullOrEmpty(lostDay))
                    return null;
                if (String.IsNullOrEmpty(lostMonth))
                    return null;
                if (String.IsNullOrEmpty(lostYear))
                    return null;

                if (currentAdd == null)
                    currentAdd = "";

                return new Motobike()
                {
                    TicketNumber = ticketNumber,
                    Type = type,
                    IDNumber = idNumber,
                    OwnerName = ownerName,
                    PaperAddress = paperAdd,
                    CurrentAddress = currentAdd,
                    RegisterNumber = registerNumber,
                    RegisterDate = new DateTime(int.Parse(registerYear), int.Parse(registerMonth), int.Parse(registerDay)),
                    LostDate = new DateTime(int.Parse(lostYear), int.Parse(lostMonth), int.Parse(lostDay)),
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
