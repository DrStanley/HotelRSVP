using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using PartyInvites.Models;

namespace PartyInvites.Services
{
	public class DbChecks
	{
		int RsvpCount=0;
		static SqlConnection con = new SqlConnection(ConnectionString.ConnStr());

		public static int CheckCustomer(GuestResponseModel responseModel)
		{
			// checks if customer exist. if true return the CustomerCode else return 0
			int hasIt = 0;
			try
			{
				con.Close();

				SqlCommand cmd = SelectIt(responseModel);
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				bool i = reader.HasRows;
				if (i)
				{
					reader.Read();

					hasIt = reader.GetInt32(4);
					Console.WriteLine(hasIt);
				}
				con.Close();
				Console.WriteLine(i);

			}
			catch (Exception e)
			{
				ErrorViewModel x = new ErrorViewModel();
				x.Message = e.Message;
				Console.WriteLine(e.Message);
			}

			return hasIt;
		}
		public static int CheckCode()
		{
			//Checks if the code generated exist, if it doesnt return it else run again
			System.Random random = new System.Random();
			int CustomerCode = random.Next(10, 1000);
			try
			{
				con.Close();
				string query = "select * from Customers where CustomerCode = '" + CustomerCode + "'";
				SqlCommand cmd = new SqlCommand(query, con);
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				bool i = reader.HasRows;
				if (i)
				{
					CheckCode();
				}

				con.Close();

			}
			catch (Exception e)
			{
				ErrorViewModel x = new ErrorViewModel();
				x.Message = e.Message;
				Console.WriteLine(e.Message);
			}


			return CustomerCode;
		}
		public static int UpdateRSVPCount(GuestResponseModel guestResponseModel, int CustomerCode)
		{
			//Updates the number of times a customer have done Reservation
			int RsvpCode = 0;
			try
			{
				con.Close();

				SqlCommand cmd1 = SelectIt(guestResponseModel);
				con.Open();
				SqlDataReader reader1 = cmd1.ExecuteReader();
				bool i = reader1.HasRows;
				if (i)
				{
					RsvpCode = reader1.GetInt32(5);
				}
				con.Close();

				RsvpCode++;

				string query = "UPDATE Customers SET NumberOfRSVP="+RsvpCode+" where CustomerCode = '" + CustomerCode + "'";
				SqlCommand cmd = new SqlCommand(query, con);
				con.Open();
				cmd.ExecuteNonQuery();
				con.Close();

			}
			catch (Exception e)
			{
				ErrorViewModel x = new ErrorViewModel();
				x.Message = e.Message;
				Console.WriteLine(e.Message);
			}


			return CustomerCode;
		}

		public static SqlCommand SelectIt(GuestResponseModel responseModel)
		{
			string query = "select * from Customers where Name = '" + responseModel.Name + "' and Email = '" + responseModel.Email + "'";

			SqlCommand cmd = new SqlCommand(query, con);
			return cmd;

		}


	}
}
