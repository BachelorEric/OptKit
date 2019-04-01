using OptKit.ComponentModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OptKit.Data
{
    /// <summary>
    /// 提供安全读取器以访问数据流结果集中的数据，对于结果集中的<see cref="DBNull"/>处理为数据类型的默认值
    /// </summary>
    public class SafeDataReader : DisposableBase, IDataReader
    {
        /// <summary>
        /// Get a reference to the underlying data reader
        /// object that actually contains the data from
        /// the data source.
        /// </summary>
        protected IDataReader DataReader { get; }

        /// <summary>
        /// Initializes the SafeDataReader object to use data from
        /// the provided DataReader object.
        /// </summary>
        /// <param name="dataReader">The source DataReader object containing the data.</param>
        public SafeDataReader(IDataReader dataReader)
        {
            DataReader = dataReader;
        }

        /// <summary>
        /// Gets a string value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns empty string for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public string GetString(string name)
        {
            return GetString(DataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a string value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns empty string for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual string GetString(int i)
        {
            if (DataReader.IsDBNull(i))
                return string.Empty;
            else
                return DataReader.GetString(i);
        }

        /// <summary>
        /// Gets a value of type <see cref="System.Object" /> from the datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        public object GetValue(string name)
        {
            return GetValue(DataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a value of type <see cref="System.Object" /> from the datareader.
        /// </summary>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual object GetValue(int i)
        {
            if (DataReader.IsDBNull(i))
                return null;
            else
                return DataReader.GetValue(i);
        }

        /// <summary>
        /// Gets an integer from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public int GetInt32(string name)
        {
            return GetInt32(DataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets an integer from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual int GetInt32(int i)
        {
            if (DataReader.IsDBNull(i))
                return 0;
            else
                return Convert.ToInt32(GetValue(i));
        }

        /// <summary>
        /// Gets a double from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public double GetDouble(string name)
        {
            return GetDouble(DataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a double from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual double GetDouble(int i)
        {
            if (DataReader.IsDBNull(i))
                return 0;
            else
                return Convert.ToDouble(GetValue(i));
        }

        #region Get Nullable Value
        public double? GetNullableDouble(string name)
        {
            return GetNullableDouble(DataReader.GetOrdinal(name));
        }
        public virtual double? GetNullableDouble(int i)
        {
            if (DataReader.IsDBNull(i))
                return null;
            else
                return Convert.ToDouble(GetValue(i));
        }
        public int? GetNullableInt32(string name)
        {
            return GetNullableInt32(DataReader.GetOrdinal(name));
        }
        public virtual int? GetNullableInt32(int i)
        {
            if (DataReader.IsDBNull(i))
                return null;
            else
                return Convert.ToInt32(GetValue(i));
        }
        public float? GetNullableFloat(string name)
        {
            return GetNullableFloat(DataReader.GetOrdinal(name));
        }
        public virtual float? GetNullableFloat(int i)
        {
            if (DataReader.IsDBNull(i))
                return null;
            else
                return Convert.ToSingle(DataReader.GetValue(i));
        }
        public short? GetNullableInt16(string name)
        {
            return GetNullableInt16(DataReader.GetOrdinal(name));
        }
        public virtual short? GetNullableInt16(int i)
        {
            if (DataReader.IsDBNull(i))
                return null;
            else
                return Convert.ToInt16(DataReader.GetValue(i));
        }
        public Int64? GetNullableInt64(string name)
        {
            return GetNullableInt64(DataReader.GetOrdinal(name));
        }
        public virtual Int64? GetNullableInt64(int i)
        {
            if (DataReader.IsDBNull(i))
                return null;
            else
                return Convert.ToInt64(DataReader.GetValue(i));
        }
        public decimal? GetNullableDecimal(string name)
        {
            return GetNullableDecimal(DataReader.GetOrdinal(name));
        }
        public virtual decimal? GetNullableDecimal(int i)
        {
            if (DataReader.IsDBNull(i))
                return null;
            else
                return Convert.ToDecimal(DataReader.GetValue(i));
        }
        public virtual DateTime? GetNullableDateTime(string name)
        {
            return GetNullableDateTime(DataReader.GetOrdinal(name));
        }
        public virtual DateTime? GetNullableDateTime(int i)
        {
            if (DataReader.IsDBNull(i))
                return null;
            else
                return DataReader.GetDateTime(i);
        }
        public bool? GetNullableBoolean(string name)
        {
            return GetNullableBoolean(DataReader.GetOrdinal(name));
        }
        public virtual bool? GetNullableBoolean(int i)
        {
            if (DataReader.IsDBNull(i))
                return null;
            else
                return Convert.ToBoolean(GetValue(i));
        }
        public byte? GetNullableByte(string name)
        {
            return GetNullableByte(DataReader.GetOrdinal(name));
        }
        public virtual byte? GetNullableByte(int i)
        {
            if (DataReader.IsDBNull(i))
                return null;
            else
                return DataReader.GetByte(i);
        }

        #endregion

        /// <summary>
        /// Gets a Guid value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns Guid.Empty for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public System.Guid GetGuid(string name)
        {
            return GetGuid(DataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a Guid value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns Guid.Empty for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual System.Guid GetGuid(int i)
        {
            if (DataReader.IsDBNull(i))
                return Guid.Empty;
            else
                return new Guid(DataReader.GetValue(i).ToString());
        }

        /// <summary>
        /// Reads the next row of data from the datareader.
        /// </summary>
        public bool Read()
        {
            return DataReader.Read();
        }

        /// <summary>
        /// Moves to the next result set in the datareader.
        /// </summary>
        public bool NextResult()
        {
            return DataReader.NextResult();
        }

        /// <summary>
        /// Closes the datareader.
        /// </summary>
        public void Close()
        {
            DataReader.Close();
        }

        /// <summary>
        /// Returns the depth property value from the datareader.
        /// </summary>
        public int Depth
        {
            get
            {
                return DataReader.Depth;
            }
        }

        /// <summary>
        /// Returns the FieldCount property from the datareader.
        /// </summary>
        public int FieldCount
        {
            get
            {
                return DataReader.FieldCount;
            }
        }

        /// <summary>
        /// Gets a boolean value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns <see langword="false" /> for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public bool GetBoolean(string name)
        {
            return GetBoolean(DataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a boolean value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns <see langword="false" /> for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual bool GetBoolean(int i)
        {
            if (DataReader.IsDBNull(i))
                return false;
            else
                return Convert.ToBoolean(GetValue(i));
        }

        /// <summary>
        /// Gets a byte value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public byte GetByte(string name)
        {
            return GetByte(DataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a byte value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual byte GetByte(int i)
        {
            if (DataReader.IsDBNull(i))
                return 0;
            else
                return DataReader.GetByte(i);
        }

        /// <summary>
        /// Invokes the GetBytes method of the underlying datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        /// <param name="buffer">Array containing the data.</param>
        /// <param name="bufferOffset">Offset position within the buffer.</param>
        /// <param name="fieldOffset">Offset position within the field.</param>
        /// <param name="length">Length of data to read.</param>
        public Int64 GetBytes(string name, Int64 fieldOffset,
          byte[] buffer, int bufferOffset, int length)
        {
            return GetBytes(DataReader.GetOrdinal(name), fieldOffset, buffer, bufferOffset, length);
        }

        /// <summary>
        /// Invokes the GetBytes method of the underlying datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        /// <param name="buffer">Array containing the data.</param>
        /// <param name="bufferOffset">Offset position within the buffer.</param>
        /// <param name="fieldOffset">Offset position within the field.</param>
        /// <param name="length">Length of data to read.</param>
        public virtual Int64 GetBytes(int i, Int64 fieldOffset,
          byte[] buffer, int bufferOffset, int length)
        {
            if (DataReader.IsDBNull(i))
                return 0;
            else
                return DataReader.GetBytes(i, fieldOffset, buffer, bufferOffset, length);
        }

        /// <summary>
        /// Gets a char value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns Char.MinValue for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public char GetChar(string name)
        {
            return GetChar(DataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a char value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns Char.MinValue for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual char GetChar(int i)
        {
            if (DataReader.IsDBNull(i))
                return char.MinValue;
            else
            {
                char[] myChar = new char[1];
                DataReader.GetChars(i, 0, myChar, 0, 1);
                return myChar[0];
            }
        }

        /// <summary>
        /// Invokes the GetChars method of the underlying datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        /// <param name="buffer">Array containing the data.</param>
        /// <param name="bufferOffset">Offset position within the buffer.</param>
        /// <param name="fieldOffset">Offset position within the field.</param>
        /// <param name="length">Length of data to read.</param>
        public Int64 GetChars(string name, Int64 fieldOffset,
          char[] buffer, int bufferOffset, int length)
        {
            return GetChars(DataReader.GetOrdinal(name), fieldOffset, buffer, bufferOffset, length);
        }

        /// <summary>
        /// Invokes the GetChars method of the underlying datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        /// <param name="buffer">Array containing the data.</param>
        /// <param name="bufferOffset">Offset position within the buffer.</param>
        /// <param name="fieldOffset">Offset position within the field.</param>
        /// <param name="length">Length of data to read.</param>
        public virtual Int64 GetChars(int i, Int64 fieldOffset,
          char[] buffer, int bufferOffset, int length)
        {
            if (DataReader.IsDBNull(i))
                return 0;
            else
                return DataReader.GetChars(i, fieldOffset, buffer, bufferOffset, length);
        }

        /// <summary>
        /// Invokes the GetData method of the underlying datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        public IDataReader GetData(string name)
        {
            return GetData(DataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Invokes the GetData method of the underlying datareader.
        /// </summary>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual IDataReader GetData(int i)
        {
            return DataReader.GetData(i);
        }

        /// <summary>
        /// Invokes the GetDataTypeName method of the underlying datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        public string GetDataTypeName(string name)
        {
            return GetDataTypeName(DataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Invokes the GetDataTypeName method of the underlying datareader.
        /// </summary>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual string GetDataTypeName(int i)
        {
            return DataReader.GetDataTypeName(i);
        }

        /// <summary>
        /// Gets a date value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns DateTime.MinValue for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public virtual DateTime GetDateTime(string name)
        {
            return GetDateTime(DataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a date value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns DateTime.MinValue for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual DateTime GetDateTime(int i)
        {
            if (DataReader.IsDBNull(i))
                return DateTime.MinValue;
            else
                return DataReader.GetDateTime(i);
        }

        /// <summary>
        /// Gets a decimal value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public decimal GetDecimal(string name)
        {
            return GetDecimal(DataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a decimal value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual decimal GetDecimal(int i)
        {
            if (DataReader.IsDBNull(i))
                return 0;
            else
                return Convert.ToDecimal(DataReader.GetValue(i));
        }

        /// <summary>
        /// Invokes the GetFieldType method of the underlying datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        public Type GetFieldType(string name)
        {
            return GetFieldType(DataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Invokes the GetFieldType method of the underlying datareader.
        /// </summary>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual Type GetFieldType(int i)
        {
            return DataReader.GetFieldType(i);
        }

        /// <summary>
        /// Gets a Single value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public float GetFloat(string name)
        {
            return GetFloat(DataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a Single value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual float GetFloat(int i)
        {
            if (DataReader.IsDBNull(i))
                return 0;
            else
                return Convert.ToSingle(DataReader.GetValue(i));
        }

        /// <summary>
        /// Gets a Short value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public short GetInt16(string name)
        {
            return GetInt16(DataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a Short value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual short GetInt16(int i)
        {
            if (DataReader.IsDBNull(i))
                return 0;
            else
                return Convert.ToInt16(DataReader.GetValue(i));
        }

        /// <summary>
        /// Gets a Long value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public Int64 GetInt64(string name)
        {
            return GetInt64(DataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a Long value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual Int64 GetInt64(int i)
        {
            if (DataReader.IsDBNull(i))
                return 0;
            else
                return Convert.ToInt64(DataReader.GetValue(i));
        }

        /// <summary>
        /// Invokes the GetName method of the underlying datareader.
        /// </summary>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual string GetName(int i)
        {
            return DataReader.GetName(i);
        }

        /// <summary>
        /// Gets an ordinal value from the datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        public int GetOrdinal(string name)
        {
            return DataReader.GetOrdinal(name);
        }

        /// <summary>
        /// Invokes the GetSchemaTable method of the underlying datareader.
        /// </summary>
        public DataTable GetSchemaTable()
        {
            return DataReader.GetSchemaTable();
        }

        /// <summary>
        /// Invokes the GetValues method of the underlying datareader.
        /// </summary>
        /// <param name="values">An array of System.Object to copy the values into.</param>
        public int GetValues(object[] values)
        {
            return DataReader.GetValues(values);
        }

        /// <summary>
        /// Returns the IsClosed property value from the datareader.
        /// </summary>
        public bool IsClosed
        {
            get
            {
                return DataReader.IsClosed;
            }
        }

        /// <summary>
        /// Invokes the IsDBNull method of the underlying datareader.
        /// </summary>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual bool IsDBNull(int i)
        {
            return DataReader.IsDBNull(i);
        }

        /// <summary>
        /// Invokes the IsDBNull method of the underlying datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        public virtual bool IsDBNull(string name)
        {
            int index = GetOrdinal(name);
            return IsDBNull(index);
        }

        /// <summary>
        /// Returns a value from the datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        public object this[string name]
        {
            get
            {
                object val = DataReader[name];
                if (DBNull.Value.Equals(val))
                    return null;
                else
                    return val;
            }
        }

        /// <summary>
        /// Returns a value from the datareader.
        /// </summary>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual object this[int i]
        {
            get
            {
                if (DataReader.IsDBNull(i))
                    return null;
                else
                    return DataReader[i];
            }
        }

        /// <summary>
        /// Returns the RecordsAffected property value from the underlying datareader.
        /// </summary>
        public int RecordsAffected
        {
            get
            {
                return DataReader.RecordsAffected;
            }
        }

        /// <summary>
        /// 执行清理，销毁对象
        /// </summary>
        protected override void Cleanup(bool disposing)
        {
            DataReader.Dispose();
        }
    }
}
