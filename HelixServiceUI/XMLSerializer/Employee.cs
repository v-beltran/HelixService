﻿using HelixService.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HelixServiceUI.XMLSerializer
{
    [Serializable]
    public class Employee
    {

        #region " Properties "

        private Guid _employee_guid;
        private String _employee_first_name;
        private String _employee_last_name;
        private String _employee_email;
        private String _employee_phone;
        private String _employee_state;
        private String _employee_city;
        private String _employee_street;
        private String _employee_zip;
        private Employee _unchanged_employee;

        public Guid Guid
        {
            get { return this._employee_guid; }
            set { this._employee_guid = value; }
        }

        public String FirstName
        {
            get { return this._employee_first_name; }
            set
            {
                if (this.ObjectState == ObjectState.Unchanged && this._employee_first_name != value) { this.ObjectState = ObjectState.ToBeUpdated; }
                this._employee_first_name = value;
            }
        }

        public String LastName
        {
            get { return this._employee_last_name; }
            set
            {
                if (this.ObjectState == ObjectState.Unchanged && this._employee_last_name != value) { this.ObjectState = ObjectState.ToBeUpdated; }
                this._employee_last_name = value;
            }
        }

        public String Email
        {
            get { return this._employee_email; }
            set
            {
                if (this.ObjectState == ObjectState.Unchanged && this._employee_email != value) { this.ObjectState = ObjectState.ToBeUpdated; }
                this._employee_email = value;
            }
        }

        public String Phone
        {
            get { return this._employee_phone; }
            set
            {
                if (this.ObjectState == ObjectState.Unchanged && this._employee_phone != value) { this.ObjectState = ObjectState.ToBeUpdated; }
                this._employee_phone = value;
            }
        }

        public String State
        {
            get { return this._employee_state; }
            set
            {
                if (this.ObjectState == ObjectState.Unchanged && this._employee_state != value) { this.ObjectState = ObjectState.ToBeUpdated; }
                this._employee_state = value;
            }
        }

        public String City
        {
            get { return this._employee_city; }
            set
            {
                if (this.ObjectState == ObjectState.Unchanged && this._employee_city != value) { this.ObjectState = ObjectState.ToBeUpdated; }
                this._employee_city = value;
            }
        }

        public String Street
        {
            get { return this._employee_street; }
            set
            {
                if (this.ObjectState == ObjectState.Unchanged && this._employee_street != value) { this.ObjectState = ObjectState.ToBeUpdated; }
                this._employee_street = value;
            }
        }

        public String Zip
        {
            get { return this._employee_zip; }
            set
            {
                if (this.ObjectState == ObjectState.Unchanged && this._employee_zip != value) { this.ObjectState = ObjectState.ToBeUpdated; }
                this._employee_zip = value;
            }
        }

        public XElement Xml
        {
            get
            {
                return HXml.SerializeToXml<Employee>(this);  
            }
        }

        /// <summary>
        /// The state of the object before any modifications were made.
        /// </summary>
        public Employee UnchangedEmployee
        {
            get { return this._unchanged_employee; }
            set
            {
                this._unchanged_employee = new Employee(value);
            }
        }

        /// <summary>
        /// The state of the object for SQL-related transactions.
        /// </summary>
        public ObjectState ObjectState { get; set; }


        #endregion

        #region " Constructors "

        /// <summary>
        /// An employee with empty values.
        /// </summary>
        public Employee()
        {
            this.Guid = Guid.NewGuid();
            this.FirstName = String.Empty;
            this.LastName = String.Empty;
            this.Email = String.Empty;
            this.Phone = String.Empty;
            this.State = String.Empty;
            this.City = String.Empty;
            this.Street = String.Empty;
            this.Zip = String.Empty;
            this.ObjectState = ObjectState.ToBeInserted;
            this.UnchangedEmployee = this;
        }

        /// <summary>
        /// An employee with empty values.
        /// </summary>
        public Employee(Employee employee)
        {
            this.Guid = employee.Guid;
            this.FirstName = employee.FirstName;
            this.LastName = employee.LastName;
            this.Email = employee.Email;
            this.Phone = employee.Phone;
            this.State = employee.State;
            this.City = employee.City;
            this.Street = employee.Street;
            this.Zip = employee.Zip;
            this.ObjectState = employee.ObjectState;
        }

        /// <summary>
        /// An employee loaded from the database.
        /// </summary>
        /// <param name="dr">The data row to populate the Employee object.</param>
        public Employee(DataRow dr)
        {
            String employeeXml = HString.SafeTrim(dr["employee_xml"]);

            if (!String.IsNullOrEmpty(employeeXml))
            {
                // NEW WAY

                Employee employee = HXml.DeserializeFromXml<Employee>(employeeXml);
                if (employee != null)
                {
                    this.Guid = employee.Guid;
                    this.FirstName = employee.FirstName;
                    this.LastName = employee.LastName;
                    this.Email = employee.Email;
                    this.Phone = employee.Phone;
                    this.State = employee.State;
                    this.City = employee.City;
                    this.Street = employee.Street;
                    this.Zip = employee.Zip;
                    this.ObjectState = ObjectState.Unchanged;
                }
            }
            else
            {
                // OLD WAY 

                this.Guid = Guid.Parse(HString.SafeTrim(dr["employee_master_guid"]));
                this.FirstName = HString.SafeTrim(dr["employee_first_name"]);
                this.LastName = HString.SafeTrim(dr["employee_last_name"]);
                this.Email = HString.SafeTrim(dr["employee_email"]);
                this.Phone = HString.SafeTrim(dr["employee_phone"]);
                this.State = HString.SafeTrim(dr["employee_state"]);
                this.City = HString.SafeTrim(dr["employee_city"]);
                this.Street = HString.SafeTrim(dr["employee_street"]);
                this.Zip = HString.SafeTrim(dr["employee_zip"]);
                this.ObjectState = ObjectState.Unchanged;
            }

            this.UnchangedEmployee = this;
        }

        #endregion

        #region " Commit Methods "

        /// <summary>
        /// Commit a transaction to the database.
        /// </summary>        
        public void Commit()
        {
            using (SqlConnection cn = new SqlConnection(HConfig.DBConnectionString))
            {
                cn.Open();

                switch (this.ObjectState)
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

            ChangeLog log = new ChangeLog() { OwnerID = this.Guid.ToString() };
            List<ChangeLogItem> logItems = log.GetChangeLogItems(this.UnchangedEmployee, this);
            logItems.ForEach(li => li.Commit());
        }

        /// <summary>
        /// Inserts a new employee to the database.
        /// </summary>
        private void InsertObject(SqlConnection cn)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Employee_Master (employee_master_guid, employee_first_name, employee_last_name, employee_email, employee_phone, employee_state, employee_city, employee_street, employee_zip, employee_xml) VALUES (@employee_master_guid, @employee_first_name, @employee_last_name, @employee_email, @employee_phone, @employee_state, @employee_city, @employee_street, @employee_zip, @employee_xml)", cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@employee_master_guid", SqlDbType.UniqueIdentifier) { Value = this.Guid });
                    cmd.Parameters.Add(new SqlParameter("@employee_first_name", SqlDbType.NVarChar) { Value = this.FirstName });
                    cmd.Parameters.Add(new SqlParameter("@employee_last_name", SqlDbType.NVarChar) { Value = this.LastName });
                    cmd.Parameters.Add(new SqlParameter("@employee_email", SqlDbType.NVarChar) { Value = this.Email });
                    cmd.Parameters.Add(new SqlParameter("@employee_phone", SqlDbType.NVarChar) { Value = this.Phone });
                    cmd.Parameters.Add(new SqlParameter("@employee_state", SqlDbType.NVarChar) { Value = this.State });
                    cmd.Parameters.Add(new SqlParameter("@employee_city", SqlDbType.NVarChar) { Value = this.City });
                    cmd.Parameters.Add(new SqlParameter("@employee_street", SqlDbType.NVarChar) { Value = this.Street });
                    cmd.Parameters.Add(new SqlParameter("@employee_zip", SqlDbType.NVarChar) { Value = this.Zip });
                    cmd.Parameters.Add(new SqlParameter("@employee_xml", SqlDbType.Xml) { Value = new SqlXml(this.Xml.CreateReader()) });
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Employee.InsertObject", ex);
            }
        }

        /// <summary>
        /// Updates the current employee.
        /// </summary>
        private void UpdateObject(SqlConnection cn)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Employee_Master SET employee_master_guid=@employee_master_guid, employee_first_name=@employee_first_name, employee_last_name=@employee_last_name, employee_email=@employee_email, employee_phone=@employee_phone, employee_state=@employee_state, employee_city=@employee_city, employee_street=@employee_street, employee_zip=@employee_zip, employee_xml=@employee_xml WHERE employee_master_guid=@employee_master_guid", cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@employee_master_guid", SqlDbType.UniqueIdentifier) { Value = this.Guid });
                    cmd.Parameters.Add(new SqlParameter("@employee_first_name", SqlDbType.NVarChar) { Value = this.FirstName });
                    cmd.Parameters.Add(new SqlParameter("@employee_last_name", SqlDbType.NVarChar) { Value = this.LastName });
                    cmd.Parameters.Add(new SqlParameter("@employee_email", SqlDbType.NVarChar) { Value = this.Email });
                    cmd.Parameters.Add(new SqlParameter("@employee_phone", SqlDbType.NVarChar) { Value = this.Phone });
                    cmd.Parameters.Add(new SqlParameter("@employee_state", SqlDbType.NVarChar) { Value = this.State });
                    cmd.Parameters.Add(new SqlParameter("@employee_city", SqlDbType.NVarChar) { Value = this.City });
                    cmd.Parameters.Add(new SqlParameter("@employee_street", SqlDbType.NVarChar) { Value = this.Street });
                    cmd.Parameters.Add(new SqlParameter("@employee_zip", SqlDbType.NVarChar) { Value = this.Zip });
                    cmd.Parameters.Add(new SqlParameter("@employee_xml", SqlDbType.Xml) { Value = new SqlXml(this.Xml.CreateReader()) });
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Employee.UpdateObject", ex);
            }
        }

        /// <summary>
        /// Deletes an employee by their GUID.
        /// </summary>
        private void DeleteObject(SqlConnection cn)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Employee_Master WHERE employee_master_guid=@employee_master_guid", cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@employee_master_guid", SqlDbType.UniqueIdentifier) { Value = this.Guid });
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Employee.DeleteObject", ex);
            }
        }

        #endregion

        #region " Load Methods "

        /// <summary>
        /// Load a single employee.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <param name="filter">The filter used to select employees.</param>
        /// <returns></returns>
        public static Employee Load(EmployeeFilter filter)
        {
            List<Employee> employees = Employee.LoadCollection(filter);
            return employees.Count > 0 ? employees[0] : null;
        }

        /// <summary>
        /// Load a list of employees from the database.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <param name="filter">The filter used to select employees.</param>
        /// <returns></returns>
        public static List<Employee> LoadCollection(EmployeeFilter filter)
        {
            List<Employee> employees = new List<Employee>();
            SqlCommand cmd = new SqlCommand();
            StringBuilder select = new StringBuilder(1000);

            if (!String.IsNullOrEmpty(filter.Keyword))
            {
                // Search all columns by keyword.

                select.Append("SELECT * FROM Employee_Master EM WHERE EM.employee_first_name LIKE '%' + @employee_first_name + '%'");
                select.Append(" OR EM.employee_last_name LIKE '%' + @employee_last_name + '%'");
                select.Append(" OR EM.employee_email LIKE '%' + @employee_email + '%'");
                select.Append(" OR EM.employee_phone LIKE '%' + @employee_phone + '%'");
                select.Append(" OR EM.employee_state LIKE '%' + @employee_state + '%'");
                select.Append(" OR EM.employee_city LIKE '%' + @employee_city + '%'");
                select.Append(" OR EM.employee_street LIKE '%' + @employee_street + '%'");
                select.Append(" OR EM.employee_zip LIKE '%' + @employee_zip + '%'");
                select.Append(" ORDER BY EM.employee_last_name, EM.employee_first_name");
                cmd.Parameters.Add(new SqlParameter("@employee_first_name", SqlDbType.NVarChar) { Value = filter.Keyword });
                cmd.Parameters.Add(new SqlParameter("@employee_last_name", SqlDbType.NVarChar) { Value = filter.Keyword });
                cmd.Parameters.Add(new SqlParameter("@employee_email", SqlDbType.NVarChar) { Value = filter.Keyword });
                cmd.Parameters.Add(new SqlParameter("@employee_phone", SqlDbType.NVarChar) { Value = filter.Keyword });
                cmd.Parameters.Add(new SqlParameter("@employee_state", SqlDbType.NVarChar) { Value = filter.Keyword });
                cmd.Parameters.Add(new SqlParameter("@employee_city", SqlDbType.NVarChar) { Value = filter.Keyword });
                cmd.Parameters.Add(new SqlParameter("@employee_street", SqlDbType.NVarChar) { Value = filter.Keyword });
                cmd.Parameters.Add(new SqlParameter("@employee_zip", SqlDbType.NVarChar) { Value = filter.Keyword });
                cmd.CommandText = select.ToString();
            }
            else
            {
                if (filter.Guid != Guid.Empty)
                {
                    select.Append("EM.employee_master_guid=@employee_master_guid");
                    cmd.Parameters.Add(new SqlParameter("@employee_master_guid", SqlDbType.UniqueIdentifier) { Value = filter.Guid });
                }

                if (!String.IsNullOrEmpty(filter.FirstName))
                {
                    if (select.Length > 0) { select.Append(" AND "); }
                    select.Append("EM.employee_first_name LIKE '%' + @employee_first_name + '%'");
                    cmd.Parameters.Add(new SqlParameter("@employee_first_name", SqlDbType.NVarChar) { Value = filter.FirstName });
                }

                if (!String.IsNullOrEmpty(filter.LastName))
                {
                    if (select.Length > 0) { select.Append(" AND "); }
                    select.Append("EM.employee_last_name LIKE '%' + @employee_last_name + '%'");
                    cmd.Parameters.Add(new SqlParameter("@employee_last_name", SqlDbType.NVarChar) { Value = filter.LastName });
                }

                if (!String.IsNullOrEmpty(filter.Email))
                {
                    if (select.Length > 0) { select.Append(" AND "); }
                    select.Append("EM.employee_email LIKE '%' + @employee_email + '%'");
                    cmd.Parameters.Add(new SqlParameter("@employee_email", SqlDbType.NVarChar) { Value = filter.Email });
                }

                if (!String.IsNullOrEmpty(filter.Phone))
                {
                    if (select.Length > 0) { select.Append(" AND "); }
                    select.Append("EM.employee_phone LIKE '%' + @employee_phone + '%'");
                    cmd.Parameters.Add(new SqlParameter("@employee_phone", SqlDbType.NVarChar) { Value = filter.Phone });
                }

                if (!String.IsNullOrEmpty(filter.State))
                {
                    if (select.Length > 0) { select.Append(" AND "); }
                    select.Append("EM.employee_state LIKE '%' + @employee_state + '%'");
                    cmd.Parameters.Add(new SqlParameter("@employee_state", SqlDbType.NVarChar) { Value = filter.State });
                }

                if (!String.IsNullOrEmpty(filter.City))
                {
                    if (select.Length > 0) { select.Append(" AND "); }
                    select.Append("EM.employee_city LIKE '%' + @employee_city + '%'");
                    cmd.Parameters.Add(new SqlParameter("@employee_city", SqlDbType.NVarChar) { Value = filter.City });
                }

                if (!String.IsNullOrEmpty(filter.Street))
                {
                    if (select.Length > 0) { select.Append(" AND "); }
                    select.Append("EM.employee_street LIKE '%' + @employee_street + '%'");
                    cmd.Parameters.Add(new SqlParameter("@employee_street", SqlDbType.NVarChar) { Value = filter.Street });
                }

                if (!String.IsNullOrEmpty(filter.Zip))
                {
                    if (select.Length > 0) { select.Append(" AND "); }
                    select.Append("EM.employee_zip LIKE '%' + @employee_zip + '%'");
                    cmd.Parameters.Add(new SqlParameter("@employee_zip", SqlDbType.NVarChar) { Value = filter.Zip });
                }

                select.Insert(0, String.Format("SELECT * FROM Employee_Master EM {0} ", cmd.Parameters.Count > 0 ? "WHERE" : String.Empty));
                select.Append(" ORDER BY EM.employee_last_name, EM.employee_first_name");
                cmd.CommandText = select.ToString();
            }

            using (SqlConnection cn = new SqlConnection(HConfig.DBConnectionString))
            {
                using (DataTable dt = HDatabase.FillDataTable(cn, cmd))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        employees.Add(new Employee(dr));
                    }
                }
            }

            return employees;
        }

        #endregion
    }

    #region " Employee Filter "

    [Serializable]
    public class EmployeeFilter
    {
        public Guid Guid;
        public String FirstName;
        public String LastName;
        public String Email;
        public String Phone;
        public String State;
        public String City;
        public String Street;
        public String Zip;
        public String Keyword;

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public EmployeeFilter()
        {
        }
    }

    #endregion
}