using HelixService.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace HelixServiceUI.XMLSerializer
{
    [Serializable()]
    public class ChangeLog
    {

        #region " Properties "

        private String _owner_id;
        private String _owner_name;
        private String _change_log_type;
        private DateTime _timestamp;

        public String OwnerID
        {
            get { return this._owner_id; }
            set { this._owner_id = value; }
        }

        public String OwnerName
        {
            get { return this._owner_name; }
            set { this._owner_name = value; }
        }

        public String Type
        {
            get { return this._change_log_type; }
            set { this._change_log_type = value; }
        }

        public DateTime Timestamp
        {
            get { return this._timestamp; }
            set { this._timestamp = value; }
        }

        #endregion

        #region " Constructors "

        /// <summary>
        /// Create a blank change log.
        /// </summary>
        /// <remarks></remarks>
        public ChangeLog()
        {
            this.OwnerID = String.Empty;
            this.OwnerName = String.Empty;
            this.Type = String.Empty;
            this.Timestamp = DateTime.UtcNow;
        }

        /// <summary>
        /// Load change log from a data row.
        /// </summary>
        /// <param name="dr">Data row containing the data to load.</param>
        /// <remarks></remarks>
        public ChangeLog(DataRow dr)
        {
            this.OwnerID = HString.SafeTrim(dr["owner_id"]);
            this.OwnerName = HString.SafeTrim(dr["owner_name"]);
            this.Type = HString.SafeTrim(dr["log_type"]);
            this.Timestamp = HDateTime.GetDateTime(dr["timestamp"]);
        }

        #endregion

        #region " Object Methods "

        /// <summary>
        /// Get a list of property changes from an object.
        /// </summary>
        /// <param name="prevObj">The previous object.</param>
        /// <param name="newObj">The new object.</param>
        /// <returns></returns>
        public List<ChangeLogItem> GetChangeLogItems(Object prevObj, Object newObj)
        {
            List<ChangeLogItem> changes = new List<ChangeLogItem>();

            if (this.IsEqualType(prevObj, newObj))
            {
                //Initialize the type for each Object, which should be the same.
                Type typeOld = prevObj.GetType();
                Type typeNew = newObj.GetType();

                //Initialize property list to process.
                List<PropertyInfo> properties = new List<PropertyInfo>(typeNew.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly));

                //Find Object state property ("ObjectState" or "State").
                PropertyInfo objState = typeNew.GetProperty("ObjectState", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                objState = objState == null ? typeNew.GetProperty("State", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly) : objState;

                foreach (PropertyInfo pi in properties)
                {
                    if (objState != null)
                    {
                        //Get Object state value.
                        ObjectState objStateValue = (ObjectState)objState.GetValue(newObj, null);

                        //Ignore the Object state property

                        if (!Object.ReferenceEquals(pi.PropertyType, typeof(ObjectState)))
                        {
                            switch (objStateValue)
                            {
                                case ObjectState.ToBeInserted:
                                    Object insertValue = pi.GetValue(newObj, null);

                                    //Log inserts as previous values being empty/default.
                                    ChangeLogItem insertItem = GetChangeLogItem(null, insertValue, objStateValue, typeNew, pi);
                                    if (insertItem != null)
                                    {
                                        changes.Add(insertItem);
                                    }
                                    break;
                                case ObjectState.ToBeUpdated:
                                    //Get the current property from the previous Object.
                                    PropertyInfo oldProperty = typeOld.GetProperty(pi.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                                    //Get values from their respective properties' list.
                                    Object prevValue = oldProperty.GetValue(prevObj, null);
                                    Object newValue = pi.GetValue(newObj, null);

                                    //Log updates as differences between previous and new values.
                                    ChangeLogItem updateItem = GetChangeLogItem(prevValue, newValue, objStateValue, typeNew, pi);
                                    if (updateItem != null)
                                    {
                                        changes.Add(updateItem);
                                    }
                                    break;
                                case ObjectState.ToBeDeleted:
                                    Object currentValue = pi.GetValue(newObj, null);

                                    //Log deletes as new values being empty/default.
                                    ChangeLogItem deleteItem = GetChangeLogItem(currentValue, null, objStateValue, typeNew, pi);
                                    if (deleteItem != null)
                                    {
                                        changes.Add(deleteItem);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            return changes;
        }

        /// <summary>
        /// Get any differences between the old and new property values.
        /// </summary>
        /// <param name="prevValue">Prevous property value.</param>
        /// <param name="newValue">New property value.</param>
        /// <param name="ObjectState">The state of the object.</param>
        /// <param name="ObjectType">The type of the object.</param>
        /// <param name="propertyInfo">The property being evaluated.</param>
        /// <returns></returns>
        private ChangeLogItem GetChangeLogItem(Object prevValue, Object newValue, ObjectState ObjectState, Type ObjectType, PropertyInfo propertyInfo)
        {
            ChangeLogItem lItem = new ChangeLogItem();
            lItem.OwnerID = this.OwnerID;
            lItem.OwnerName = ObjectType.Name;
            lItem.PropertyName = propertyInfo.Name;
            lItem.Type = ObjectState.ToString().Replace("ToBe", String.Empty); // Get rid of future tense from state name.

            if (Object.ReferenceEquals(propertyInfo.PropertyType, typeof(String)))
            {
                // COMPARE STRINGS

                String logPrevious = HString.SafeTrim(prevValue);
                String logNew = HString.SafeTrim(newValue);

                if (!logNew.Equals(logPrevious))
                {
                    lItem.PreviousValue = logPrevious;
                    lItem.NewValue = logNew;
                    return lItem;
                }
            }
            else if (Object.ReferenceEquals(propertyInfo.PropertyType, typeof(Int32)))
            {
                // COMPARE INTEGERS

                Int32 logPrevious = HNumeric.GetSafeInteger(prevValue);
                Int32 logNew = HNumeric.GetSafeInteger(newValue);

                if (!logNew.Equals(logPrevious))
                {
                    lItem.PreviousValue = logPrevious.ToString();
                    lItem.NewValue = logNew.ToString();
                    return lItem;
                }
            }
            else if (Object.ReferenceEquals(propertyInfo.PropertyType, typeof(Decimal)))
            {
                // COMPARE DECIMALS

                Decimal logPrevious = HNumeric.GetSafeDecimal(prevValue);
                Decimal logNew = HNumeric.GetSafeDecimal(newValue);

                if (!logNew.Equals(logPrevious))
                {
                    lItem.PreviousValue = logPrevious.ToString();
                    lItem.NewValue = logNew.ToString();
                    return lItem;
                }
            }
            else if (Object.ReferenceEquals(propertyInfo.PropertyType, typeof(DateTime)))
            {
                // COMPARE DATETIMES

                DateTime logPrevious = HDateTime.GetDateTime(prevValue);
                DateTime logNew = HDateTime.GetDateTime(newValue);

                if (!logNew.Equals(logPrevious))
                {
                    lItem.PreviousValue = logPrevious.ToString();
                    lItem.NewValue = logNew.ToString();
                    return lItem;
                }
            }
            else if (propertyInfo.PropertyType.IsEnum)
            {
                // COMPARE ENUMS

                Int32 logPrevious = Convert.ToInt32(prevValue);
                Int32 logNew = Convert.ToInt32(newValue);

                if (!logNew.Equals(logPrevious))
                {
                    lItem.PreviousValue = logPrevious.ToString();
                    lItem.NewValue = logNew.ToString();
                    return lItem;
                }
            }

            return null;
        }

        /// <summary>
        /// Compare the type of two objects.
        /// </summary>
        /// <param name="prevObj">First object for comparison.</param>
        /// <param name="newObj">Second object to compare against first object.</param>
        /// <returns></returns>
        private bool IsEqualType(Object prevObj, Object newObj)
        {
            return prevObj.GetType().Equals(newObj.GetType());
        }

        #endregion

    }

    [Serializable()]
    public class ChangeLogItem : ChangeLog
    {

        #region " Properties "

        private int _change_log_master_id;
        private string _property_name;
        private string _prev_value;
        private string _new_value;

        public int ID
        {
            get { return this._change_log_master_id; }
            set
            {
                if (this.State == ObjectState.Unchanged && !(this._change_log_master_id == value))
                    this.State = ObjectState.ToBeUpdated;
                this._change_log_master_id = value;
            }
        }

        public string PropertyName
        {
            get { return this._property_name; }
            set
            {
                if (this.State == ObjectState.Unchanged && !(this._property_name == value))
                    this.State = ObjectState.ToBeUpdated;
                this._property_name = value;
            }
        }

        public string PreviousValue
        {
            get { return this._prev_value; }
            set
            {
                if (this.State == ObjectState.Unchanged && !(this._prev_value == value))
                    this.State = ObjectState.ToBeUpdated;
                this._prev_value = value;
            }
        }

        public string NewValue
        {
            get { return this._new_value; }
            set
            {
                if (this.State == ObjectState.Unchanged && !(this._new_value == value))
                    this.State = ObjectState.ToBeUpdated;
                this._new_value = value;
            }
        }

        public ObjectState State { get; set; }

        #endregion

        #region " Constructors "

        /// <summary>
        /// Create a blank change log item.
        /// </summary>
        /// <remarks></remarks>
        public ChangeLogItem()
            : base()
        {
            this.ID = 0;
            this.PropertyName = string.Empty;
            this.PreviousValue = string.Empty;
            this.NewValue = string.Empty;
            this.State = ObjectState.ToBeInserted;
        }

        /// <summary>
        /// Load a change log item from a data row. 
        /// </summary>
        /// <param name="dr">Data row containing the data to load.</param>
        /// <remarks></remarks>
        public ChangeLogItem(DataRow dr)
            : base(dr)
        {
            this.ID = HNumeric.GetSafeInteger(dr["change_log_master_id"]);
            this.PropertyName = HString.SafeTrim(dr["property_name"]);
            this.PreviousValue = HString.SafeTrim(dr["prev_value"]);
            this.NewValue = HString.SafeTrim(dr["new_value"]);
            this.State = ObjectState.Unchanged;
        }

        #endregion

        #region " Commit Methods "

        /// <summary>
        /// Persist this Object to the database.
        /// </summary>
        /// <remarks></remarks>
        public void Commit()
        {
            using (SqlConnection cn = new SqlConnection(HConfig.DBConnectionString))
            {
                cn.Open();
                this.Commit(cn, null);
            }
        }

        /// <summary>
        /// Persist this Object to the database using an existing connection and transaction.
        /// </summary>
        /// <param name="cn">Connection to the database.</param>
        /// <param name="tn">Optional transaction to run this SQL command.</param>
        /// <remarks></remarks>
        private void Commit(SqlConnection cn, SqlTransaction tn = null)
        {
            switch (this.State)
            {
                case ObjectState.ToBeInserted:
                    this.InsertObject(cn, tn);
                    break;
                case ObjectState.ToBeUpdated:
                    this.UpdateObject(cn, tn);
                    break;
                case ObjectState.ToBeDeleted:
                    this.DeleteObject(cn, tn);
                    break;
                case ObjectState.Unchanged:
                    break;
            }
        }

        /// <summary>
        /// Insert a new record to the database.
        /// </summary>
        /// <param name="cn">Connection to the database.</param>
        /// <param name="tn">Optional transaction to run this SQL command.</param>
        /// <remarks></remarks>
        private void InsertObject(SqlConnection cn, SqlTransaction tn = null)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Change_Log_Master(owner_id,owner_name,log_type,timestamp,property_name,prev_value,new_value) VALUES(@owner_id,@owner_name,@log_type,@timestamp,@property_name,@prev_value,@new_value)", cn, tn))
                {
                    //Insert
                    cmd.Parameters.Add(new SqlParameter("@owner_id", this.OwnerID) { SqlDbType = SqlDbType.NVarChar });
                    cmd.Parameters.Add(new SqlParameter("@owner_name", this.OwnerName) { SqlDbType = SqlDbType.NVarChar });
                    cmd.Parameters.Add(new SqlParameter("@log_type", this.Type) { SqlDbType = SqlDbType.NVarChar });
                    cmd.Parameters.Add(new SqlParameter("@timestamp", this.Timestamp) { SqlDbType = SqlDbType.DateTime });
                    cmd.Parameters.Add(new SqlParameter("@property_name", this.PropertyName) { SqlDbType = SqlDbType.NVarChar });
                    cmd.Parameters.Add(new SqlParameter("@prev_value", this.PreviousValue) { SqlDbType = SqlDbType.NVarChar });
                    cmd.Parameters.Add(new SqlParameter("@new_value", this.NewValue) { SqlDbType = SqlDbType.NVarChar });
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update an existing record in the database.
        /// </summary>
        /// <param name="cn">Connection to the database.</param>
        /// <param name="tn">Optional transaction to run this SQL command.</param>
        /// <remarks></remarks>
        private void UpdateObject(SqlConnection cn, SqlTransaction tn = null)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Change_Log_Master SET owner_id=@owner_id,owner_name=@owner_name,log_type=@log_type,timestamp=@timestamp,property_name=@property_name,prev_value=@prev_value,new_value=@new_value WHERE change_log_master_id=@change_log_master_id", cn, tn))
                {
                    //Update
                    cmd.Parameters.Add(new SqlParameter("@change_log_master_id", this.ID) { SqlDbType = SqlDbType.Int });
                    cmd.Parameters.Add(new SqlParameter("@owner_id", this.OwnerID) { SqlDbType = SqlDbType.NVarChar });
                    cmd.Parameters.Add(new SqlParameter("@owner_name", this.OwnerName) { SqlDbType = SqlDbType.NVarChar });
                    cmd.Parameters.Add(new SqlParameter("@log_type", this.Type) { SqlDbType = SqlDbType.NVarChar });
                    cmd.Parameters.Add(new SqlParameter("@timestamp", this.Timestamp) { SqlDbType = SqlDbType.DateTime });
                    cmd.Parameters.Add(new SqlParameter("@property_name", this.PropertyName) { SqlDbType = SqlDbType.NVarChar });
                    cmd.Parameters.Add(new SqlParameter("@prev_value", this.PreviousValue) { SqlDbType = SqlDbType.NVarChar });
                    cmd.Parameters.Add(new SqlParameter("@new_value", this.NewValue) { SqlDbType = SqlDbType.NVarChar });
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete an existing record in the database.
        /// </summary>
        /// <param name="cn">Connection to the database.</param>
        /// <param name="tn">Optional transaction to run this SQL command.</param>
        /// <remarks></remarks>
        private void DeleteObject(SqlConnection cn, SqlTransaction tn = null)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Change_Log_Master WHERE change_log_master_id=@change_log_master_id", cn, tn))
                {
                    //Delete
                    cmd.Parameters.Add(new SqlParameter("@change_log_master_id", this.ID) { SqlDbType = SqlDbType.Int });
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region " Load Methods "

        /// <summary>
        /// Find a single change that matches the filters provided.
        /// </summary>
        /// <param name="filter">Find a single changes with the given filter parameters.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static ChangeLogItem Load(ChangeLogItemFilter filter)
        {
            List<ChangeLogItem> lResults = ChangeLogItem.LoadCollection(filter);
            return lResults.Count == 1 ? lResults[0] : null;
        }

        /// <summary>
        /// Find all changes matching the filters provided.
        /// </summary>
        /// <param name="filter">Find all changes matching the filters provided.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static List<ChangeLogItem> LoadCollection(ChangeLogItemFilter filter)
        {
            List<ChangeLogItem> results = new List<ChangeLogItem>();
            SqlCommand cmd = new SqlCommand();
            StringBuilder select = new StringBuilder(1000);

            if (filter.ID != null)
            {
                select.Append("CLM.change_log_master_id=@change_log_master_id");
                cmd.Parameters.Add(new SqlParameter("@change_log_master_id", SqlDbType.Int) { Value = filter.ID });
            }

            if (filter.OwnerID != null)
            {
                if ((select.Length > 0))
                    select.Append(" AND ");
                select.Append("CLM.owner_id=@owner_id");
                cmd.Parameters.Add(new SqlParameter("@owner_id", SqlDbType.Int) { Value = filter.OwnerID });
            }

            if (filter.OwnerName != null)
            {
                if ((select.Length > 0))
                    select.Append(" AND ");
                select.Append("CLM.owner_name=@owner_name");
                cmd.Parameters.Add(new SqlParameter("@owner_name", SqlDbType.NVarChar) { Value = filter.OwnerName });
            }

            if (filter.Type != null)
            {
                if ((select.Length > 0))
                    select.Append(" AND ");
                select.Append("CLM.log_type=@log_type");
                cmd.Parameters.Add(new SqlParameter("@log_type", SqlDbType.NVarChar) { Value = filter.Type });
            }

            if (filter.Timestamp != null)
            {
                if ((select.Length > 0))
                    select.Append(" AND ");
                select.Append("CLM.timestamp=@timestamp");
                cmd.Parameters.Add(new SqlParameter("@timestamp", SqlDbType.DateTime) { Value = filter.Timestamp });
            }

            if (filter.PropertyName != null)
            {
                if ((select.Length > 0))
                    select.Append(" AND ");
                select.Append("CLM.property_name=@property_name");
                cmd.Parameters.Add(new SqlParameter("@property_name", SqlDbType.NVarChar) { Value = filter.PropertyName });
            }

            select.Insert(0, "SELECT * FROM Change_Log_Master CLM {0}");

            using (SqlConnection cn = new SqlConnection(HConfig.DBConnectionString))
            {
                using (DataTable dt = HDatabase.FillDataTable(cn, cmd))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        results.Add(new ChangeLogItem(dr));
                    }
                }
            }

            return results;
        }

        #endregion

    }

    [Serializable()]
    public class ChangeLogItemFilter
    {

        #region " Properties "

        public Int32? ID { get; set; }
        public String OwnerID { get; set; }
        public String OwnerName { get; set; }
        public String Type { get; set; }
        public DateTime Timestamp { get; set; }
        public String PropertyName { get; set; }


        #endregion

        #region " Constructors "

        /// <summary>
        /// Empty constructor
        /// </summary>
        /// <remarks></remarks>
        public ChangeLogItemFilter()
        {
        }

        #endregion

    }
}