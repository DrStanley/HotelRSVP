using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartyInvites
{
	public class ConnectionString
	{
		public static string ConnStr()
		{
			return "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Vacation;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
		}
	}
}
