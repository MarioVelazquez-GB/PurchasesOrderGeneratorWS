using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
	public class PurchasesOrder
	{
		/// <summary>
		/// Purchases Orders Properties
		/// </summary>
		#region Properties 
		public string poNumber { get; set; }
        public string poDate { get; set; }
        public string poSupplier { get; set; }
        public string poSite { get; set; }
        public string poOriginator { get; set; }
        public string poTotalValue { get; set; }
        public string poComments { get; set; }
		#endregion
		/// <summary>
		/// Get the List of all the new POs Approved in PEMAC
		/// </summary>
		/// <param name="site">string</param>
		/// <returns>DataTable</returns>
		public DataTable GetPurchasesOrders(string site)
        {

            SqlConnection cnn = new SqlConnection(Helper.GetConnectionString("pemacDevDB"));
            DataTable newPOs = new DataTable();
            try
            {
                using (cnn)
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand(Helper.GetStoredProcedure("spGetNewSharepointPOs"), cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Site", site);

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(newPOs);
                    }
                }
                return newPOs;
            }
            catch
            {
                return null;
            }
            finally
            {
                cnn.Close();
            }
        }
    }
}
