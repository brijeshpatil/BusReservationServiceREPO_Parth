using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace BusReservationService
{
    /// <summary>
    /// Summary description for BusReservation
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BusReservation : System.Web.Services.WebService
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string RegisterNewUser(string FirstName, string LastName, string EID, string MobileNo, string Address, string Password, string ConfirmPassword)
        {
            cmd = new SqlCommand("insert into RegisteredUser values(@Fname,@Lname,@EID,@MNO,@Address,'false',@Pass,@CPass)", con);
            cmd.Parameters.AddWithValue("@Fname", FirstName);
            cmd.Parameters.AddWithValue("@Lname", LastName);
            cmd.Parameters.AddWithValue("@EID", EID);
            cmd.Parameters.AddWithValue("@MNO", MobileNo);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Pass", Password);
            cmd.Parameters.AddWithValue("@CPass", ConfirmPassword);
            con.Open();
            string asd = "";
            asd = cmd.ExecuteNonQuery().ToString();
            con.Close();
            if (asd != "")
            {
                return "Record Saved";
            }
            else
            {
                return "Error";
            }

        }

        [WebMethod]
        public DataSet CheckUserLogin(string EID, string Password)
        {
            da = new SqlDataAdapter("select * from RegisteredUser Where Eid = @EID and Password = @Pass", con);
            da.SelectCommand.Parameters.AddWithValue("@EID", EID);
            da.SelectCommand.Parameters.AddWithValue("@Pass", Password);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet SearchBusInfo(string CityTO, string CityFrom)
        {
            da = new SqlDataAdapter("select * from BusInfo Where Cityto = @CT and CityFrom = @CF", con);
            da.SelectCommand.Parameters.AddWithValue("@CT", CityTO);
            da.SelectCommand.Parameters.AddWithValue("@CF", CityFrom);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet GetTravelInfo(string BusName)
        {
            da = new SqlDataAdapter("select * from TravelInfo Where BusName = @BName", con);
            da.SelectCommand.Parameters.AddWithValue("@BName", BusName);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public string ChangePassword(string EID, string Password)
        {
            cmd = new SqlCommand("update RegisteredUser set Password = @Pass , [ConfirmPassword] = @Pass Where Eid = @EID", con);
            cmd.Parameters.AddWithValue("@Pass", Password);
            cmd.Parameters.AddWithValue("@EID", EID);
            con.Open();
            string asd = "";
            asd = cmd.ExecuteNonQuery().ToString();
            con.Close();
            if (asd != "")
            {
                return "Password Changed";
            }
            else
            {
                return "Error";
            }
        }
    }
}
