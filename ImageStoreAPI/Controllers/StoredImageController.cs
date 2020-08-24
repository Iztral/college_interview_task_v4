using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;

namespace ImageStoreAPI.Controllers
{
    public class StoredImageController : ApiController
    {
        [Route("api/storedimage")]
        public Dictionary<string, string> GetAllImages()
        {
            Dictionary<string, string> imageList = new Dictionary<string, string>();
            using SqlConnection sqlconnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;" + @"AttachDbFilename = |DataDirectory|\ImageGalleryDatabase.mdf; Integrated Security = True");
            string selectQuery = string.Format(@"Select Id, Title From [StoredImages]");
            sqlconnection.Open();
            SqlDataReader reader = new SqlCommand(selectQuery, sqlconnection).ExecuteReader();
            if (reader.Read())
            {
                foreach (var readerRowData in reader)
                {
                    imageList.Add(Convert.ToString(reader[0]), (string)reader[1]);
                }

                return imageList;
            }
            else
            {
                return null;
            }
        }


        [Route("api/storedimage/{id}")]
        [HttpGet]
        public byte[] GetImage(int id)
        {
            using SqlConnection sqlconnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;" + @"AttachDbFilename = |DataDirectory|\ImageGalleryDatabase.mdf; Integrated Security = True");
            string selectQuery = string.Format(@"Select [ImageByteArray] From [StoredImages] Where Id={0}", id);
            sqlconnection.Open();
            SqlDataReader reader = new SqlCommand(selectQuery, sqlconnection).ExecuteReader();
            if (reader.Read())
            {
                return (byte[])reader[0];
            }
            else
            {
                return null;
            }

        }
    }
}
