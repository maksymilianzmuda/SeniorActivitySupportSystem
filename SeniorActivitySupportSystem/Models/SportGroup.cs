﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SeniorActivitySupportSystem.Data.Enum;

namespace SeniorActivitySupportSystem.Models
{
    public class SportGroup
    {

        [Key]
        public int Id { get; set; }    
        public string Name { get; set; }   
        public string Description { get; set; }   
        public string Image { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public SportGroupCategory SportGroupCategory { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
