using System;
using MySql.Data.MySqlClient;
using Persistance;

namespace DAL
{
    public class StaffDAL
    {
        public Staff Login(Staff staff)
        {
            try
            {
                // Mysqlconnection connection = DBHelper.Get
            }
            catch (System.Exception)
            {

                throw;
            }
            return staff;
        }
    }

}