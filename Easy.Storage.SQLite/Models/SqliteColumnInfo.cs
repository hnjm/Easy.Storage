﻿// ReSharper disable InconsistentNaming
namespace Easy.Storage.SQLite.Models
{
    /// <summary>
    /// Represents the information relating to the columns of a <c>SQLite</c> table.
    /// </summary>
    public sealed class SQLiteColumnInfo
    {
        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        public string TableName { get; internal set; }

        /// <summary>
        /// Gets the Id of the column.
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        /// Gets the name of the column.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the type of the column.
        /// </summary>
        public SQLiteDataType Type { get; internal set; }

        /// <summary>
        /// Gets the flag indicating whether the column can be <c>NULL</c> or not.
        /// </summary>
        public bool NotNull { get; internal set; }

        /// <summary>
        /// Gets the default value of the column.
        /// </summary>
        public string DefaultValue { get; internal set; }

        /// <summary>
        /// Gets the flag indicating whether the column is a <c>Primary Key</c> or not.
        /// </summary>
        public bool IsPrimaryKey { get; internal set; }
    }
}