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
		/// <summary>
		/// Populated the properties that we will use to draw the Header in the PDF
		/// </summary>
		/// <param name="poNumber">string</param>
		/// <returns>PurchasesOrder</returns>
		public PurchasesOrder GetPurchasesHeader(string poNumber)
		{
			PurchasesOrder po = new PurchasesOrder();
			SqlConnection cnn = new SqlConnection(Helper.GetConnectionString("pemacDevDB"));
			SqlDataReader poDetails = null;
			try
			{
				using (cnn)
				{
					cnn.Open();
					SqlCommand cmd = new SqlCommand(Helper.GetStoredProcedure("spGetPDFHeader"), cnn);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@PONumber", poNumber);

					using (poDetails = cmd.ExecuteReader())
					{
						foreach (DbDataRecord item in poDetails)
						{
							po.poNumber = item[0].ToString();
							po.poDate = item[1].ToString();
							po.poSupplier = item[2].ToString();
							po.poSite = item[3].ToString();
							po.poOriginator = item[4].ToString();
							po.poTotalValue = item[5].ToString();
							po.poComments = item[6].ToString();
						}
					}
				}
				return po;
			}
			catch
			{
				return po = null;
			}
			finally
			{
				cnn.Close();
				po = null;
				poDetails.Close();
			}
		}

	}
}
