using HelixService.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace HelixServiceUI.BinaryHandler
{
    [Serializable]
    public class Blob
    {
        #region " Properties "

        private Int32 _binary_id;
        private Byte[] _binary_data;
        private String _binary_name;
        private String _binary_mime_type;
        private Int32 _binary_size;

        /// <summary>
        /// The identity ID of a file from a database.
        /// </summary>
        public Int32 ID
        {
            get { return this._binary_id; }
            set
            {
                if (this.State == ObjectState.Unchanged && this._binary_id != value) { this.State = ObjectState.ToBeUpdated; }
                this._binary_id = value;
            }
        }

        /// <summary>
        /// The bytes that make up a file.
        /// </summary>
        public Byte[] BinaryData
        {
            get { return this._binary_data; }
            set
            {
                if (this.State == ObjectState.Unchanged && this._binary_data != value) { this.State = ObjectState.ToBeUpdated; }
                this._binary_data = value;
            }
        }

        /// <summary>
        /// The name of the file.
        /// </summary>
        public String Name
        {
            get { return this._binary_name; }
            set
            {
                if (this.State == ObjectState.Unchanged && this._binary_name != value) { this.State = ObjectState.ToBeUpdated; }
                this._binary_name = value;
            }
        }

        /// <summary>
        /// The mime type of a file.
        /// </summary>
        public String MimeType
        {
            get { return this._binary_mime_type; }
            set
            {
                if (this.State == ObjectState.Unchanged && this._binary_mime_type != value) { this.State = ObjectState.ToBeUpdated; }
                this._binary_mime_type = value;
            }
        }

        /// <summary>
        /// The size of a file in bytes.
        /// </summary>
        public Int32 Size
        {
            get { return this._binary_size; }
            set
            {
                if (this.State == ObjectState.Unchanged && this._binary_size != value) { this.State = ObjectState.ToBeUpdated; }
                this._binary_size = value;
            }
        }

        /// <summary>
        /// The state of the object for SQL-related transactions.
        /// </summary>
        public ObjectState State { get; set; }

        #endregion

        #region " Constructors "

        /// <summary>
        /// A blob with empty values.
        /// </summary>
        public Blob()
        {
            this.BinaryData = new Byte[0];
            this.Name = String.Empty;
            this.MimeType = String.Empty;
            this.Size = 0;
            this.State = ObjectState.ToBeInserted;
        }

        /// <summary>
        /// A blob created from a file uploaded by a client.
        /// </summary>
        /// <param name="file">A file uploaded by a client.</param>
        public Blob(HttpPostedFile file)
        {
            this.Name = HString.SafeTrim(Path.GetFileName(file.FileName));
            this.BinaryData = HBinary.GetBytes(file.InputStream);
            this.Size = HNumeric.GetSafeInteger(file.ContentLength);
            this.MimeType = HString.SafeTrim(file.ContentType);
            this.State = ObjectState.ToBeInserted;
        }

        /// <summary>
        /// A blob loaded from the database.
        /// </summary>
        /// <param name="dr">The data row to populate the Blob object.</param>
        /// <param name="IncludeBinaryData">Flag that determines whether to load the binary data or not.</param>
        public Blob(DataRow dr, Boolean IncludeBinaryData)
        {
            this.ID = HNumeric.GetSafeInteger(dr["binary_id"]);
            if (IncludeBinaryData) { this.BinaryData = (Byte[])dr["binary_data"]; }
            this.Name = HString.SafeTrim(dr["binary_name"]);
            this.MimeType = HString.SafeTrim(dr["binary_mime_type"]);
            this.Size = HNumeric.GetSafeInteger(dr["binary_size"]);
            this.State = ObjectState.Unchanged;
        }

        #endregion

        #region " Commit Methods "

        /// <summary>
        /// Commit a transaction to the database.
        /// </summary>
        /// <param name="connectionString">The connection string to the database to which transactions will be made.</param>
        public void Commit()
        {
            using (SqlConnection cn = new SqlConnection(HConfig.DBConnectionString))
            {
                cn.Open();

                switch (this.State)
                {
                    case ObjectState.ToBeInserted:
                        this.InsertObject(cn);
                        break;
                    case ObjectState.ToBeUpdated:
                        this.UpdateObject(cn);
                        break;
                    case ObjectState.ToBeDeleted:
                        this.DeleteObject(cn);
                        break;
                    default:
                        break;
                }

                cn.Close();
            }
        }

        /// <summary>
        /// Inserts a new blob to the database.
        /// </summary>
        private void InsertObject(SqlConnection cn)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Binary_Master (binary_data, binary_name, binary_mime_type, binary_size) VALUES (@binary_data, @binary_name, @binary_mime_type, @binary_size)", cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@binary_data", SqlDbType.VarBinary) { Value = this.BinaryData });
                    cmd.Parameters.Add(new SqlParameter("@binary_name", SqlDbType.NVarChar) { Value = this.Name});
                    cmd.Parameters.Add(new SqlParameter("@binary_mime_type", SqlDbType.NVarChar) { Value = this.MimeType});
                    cmd.Parameters.Add(new SqlParameter("@binary_size", SqlDbType.Int) { Value = this.Size });
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Blob.InsertObject", ex);
            }
        }

        /// <summary>
        /// Updates blob with new data.
        /// </summary>
        private void UpdateObject(SqlConnection cn)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Binary_Master SET binary_id=@binary_id, binary_data=@binary_data, binary_name=@binary_name, binary_mime_type=@binary_mime_type, binary_size=@binary_size WHERE binary_id=@binary_id", cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@binary_id", SqlDbType.Int) { Value = this.ID });
                    cmd.Parameters.Add(new SqlParameter("@binary_data", SqlDbType.VarBinary) { Value = this.BinaryData });
                    cmd.Parameters.Add(new SqlParameter("@binary_name", SqlDbType.NVarChar) { Value = this.Name});
                    cmd.Parameters.Add(new SqlParameter("@binary_mime_type", SqlDbType.NVarChar) { Value = this.MimeType});
                    cmd.Parameters.Add(new SqlParameter("@binary_size", SqlDbType.Int) { Value = this.Size });
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Blob.UpdateObject", ex);
            }
        }

        /// <summary>
        /// Deletes a blob by its ID.
        /// </summary>
        private void DeleteObject(SqlConnection cn)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Binary_Master WHERE binary_id=@binary_id", cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@binary_id", SqlDbType.Int) { Value = this.ID });
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Blob.DeleteObject", ex);
            }
        }

        #endregion

        #region " Load Methods "

        /// <summary>
        /// Load a single blob.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <param name="filter">The filter used to select blobs.</param>
        /// <returns></returns>
        public static Blob Load(BlobFilter filter)
        {
            List<Blob> blobs = Blob.LoadCollection(filter);
            return blobs.Count > 0 ? blobs[0] : null;
        }

        /// <summary>
        /// Load a list of blobs from the database.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <param name="filter">The filter used to select blobs.</param>
        /// <returns></returns>
        public static List<Blob> LoadCollection(BlobFilter filter)
        {
            List<Blob> blobs = new List<Blob>();
            SqlCommand cmd = new SqlCommand();
            StringBuilder select = new StringBuilder(1000);

            if (filter.ID > 0)
            {
                select.Append("BM.binary_id=@binary_id");
                cmd.Parameters.Add(new SqlParameter("@binary_id", SqlDbType.Int) { Value = filter.ID });
            }

            if (!String.IsNullOrEmpty(filter.Name))
            {
                if (select.Length > 0) { select.Append(" AND "); }
                select.Append("BM.binary_name LIKE '%' + @binary_name + '%'");
                cmd.Parameters.Add(new SqlParameter("@binary_name", SqlDbType.NVarChar) { Value = filter.Name });
            }

            if (!String.IsNullOrEmpty(filter.MimeType))
            {
                if (select.Length > 0) { select.Append(" AND "); }
                select.Append("BM.binary_mime_type LIKE '%' + @binary_mime_type + '%'");
                cmd.Parameters.Add(new SqlParameter("@binary_mime_type", SqlDbType.NVarChar) { Value = filter.MimeType });
            }

            if (filter.SizeGreaterThan > 0)
            {
                if (select.Length > 0) { select.Append(" AND "); }
                select.Append("BM.binary_size >= @binary_size");
                cmd.Parameters.Add(new SqlParameter("@binary_size", SqlDbType.Int) { Value = filter.SizeGreaterThan });
            }

            if (filter.SizeLessThan > 0)
            {
                if (select.Length > 0) { select.Append(" AND "); }
                select.Append("BM.binary_size <= @binary_size");
                cmd.Parameters.Add(new SqlParameter("@binary_size", SqlDbType.Int) { Value = filter.SizeLessThan });
            }

            select.Insert(0, String.Format("SELECT * FROM Binary_Master BM {0} ", cmd.Parameters.Count > 0 ? "WHERE" : String.Empty));
            select.Append(" ORDER BY BM.binary_name");
            cmd.CommandText = select.ToString();

            using (SqlConnection cn = new SqlConnection(HConfig.DBConnectionString))
            {
                using (DataTable dt = HDatabase.FillDataTable(cn, cmd))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        blobs.Add(new Blob(dr, filter.IncludeBinaryData));
                    }
                }
            }

            return blobs;
        }

        #endregion
    }

    #region " Blob Filter "

    /// <summary>
    /// A filter object to be used for load methods.
    /// </summary>
    [Serializable]
    public class BlobFilter
    {
        public Int32 ID { get; set; }
        public Boolean IncludeBinaryData { get; set; }
        public String Name { get; set; }
        public String MimeType { get; set; }
        public Int32 SizeGreaterThan { get; set; }
        public Int32 SizeLessThan { get; set; }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public BlobFilter()
        {
        }
    }

    #endregion
}