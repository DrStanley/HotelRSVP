using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartyInvites.Models
{
	public class GuestResponseModel
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string RSVpType { get; set; }
		public int Number { get; set; }
		public int NumberKids { get; set; }
		public DateTime GetDateTimeIn { get; set; }
		public DateTime GetDateTimeOut { get; set; }
	}
}
