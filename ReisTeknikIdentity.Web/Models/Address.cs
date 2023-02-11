using System.ComponentModel.DataAnnotations;

namespace ReisTeknikIdentity.Web.Models
{
    public class Address
    {

        public Address() { }


        [Key]
        public int Id { get; set; }


        public string? AdressRow { get; set; }

        public string? Street { get; set; }

        public string? District { get; set; }

        public string? City { get; set; }

        public string? Contry { get; set; }

        public string PhoneNumber { get; set; }


    }



}

