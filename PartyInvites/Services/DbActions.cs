using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PartyInvites.Models;

namespace PartyInvites.Services
{
	public class DbActions

	{


		public static int CustomerRVSP(GuestResponseModel responseModel)
		{
			int code = DbChecks.CheckCustomer(responseModel);
			if (code == 0)
			{
				//create new customer
				DateTime dateTime = DateTime.Now;
				int CustomerCode = DbChecks.CheckCode();
				SqlConnection con = new SqlConnection(ConnectionString.ConnStr());
				string query = "INSERT INTO Customers (Name, Email, PhoneNumber, CustomerCode, NumberOfRSVP, Date_Created)" +
					" values ('" + responseModel.Name + "','" + responseModel.Email + "','" + responseModel.Phone
					+ "','" + CustomerCode + "','" + 0 + "','" + dateTime + "')";
				SqlCommand cmd = new SqlCommand(query, con);
				con.Open();
				int i = cmd.ExecuteNonQuery();
				con.Close();
				AddRVSP(responseModel, CustomerCode);
				return i;
			}
			else
			{
				return AddRVSP(responseModel, code);

			}


		}

		public static int AddRVSP(GuestResponseModel responseModel, int CustomerCode)
		{
			//create new customer
			DateTime dateTime = DateTime.Now;
			long r= new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
			string RsvpCode = "RSVP_" + r;
			SqlConnection con = new SqlConnection(ConnectionString.ConnStr());
			string query = "INSERT INTO Reservarions (RoomType, RoomCount, Kids, ArriveDate, LeaveDate, CustomerCode, RsvpCode, Date_Created)" +
				" values ('" + responseModel.RSVpType + "','" + responseModel.Number + "','" + responseModel.NumberKids
				+ "','" + responseModel.GetDateTimeIn + "','" + responseModel.GetDateTimeOut +"','" + CustomerCode + "','" + RsvpCode + "','" + dateTime + "')";
			SqlCommand cmd = new SqlCommand(query, con);
			con.Open();
			int i = cmd.ExecuteNonQuery();
			con.Close();
			return i;
		}
	}
}
