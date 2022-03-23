using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarService.Core.Models.DTOs
{
    public class EventDTO 
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public long Time { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Members { get; set; }
        [Required]
        public string Organizer { get; set; }
        public long Id { get; set; }

    }
}
