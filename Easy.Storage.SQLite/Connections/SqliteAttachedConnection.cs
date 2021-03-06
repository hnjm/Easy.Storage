﻿namespace Easy.Storage.SQLite.Connections
{
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.IO;
    using System.Threading.Tasks;
    using Easy.Common;
    using Easy.Storage.Common.Extensions;

    /// <summary>
    /// A wrapper around the <see cref="SQLiteConnection"/> for a simpler usage of <c>Attached</c> databases.
    /// </summary>
    public sealed class SQLiteAttachedConnection : SQLiteConnectionBase
    {
        private readonly string _attachCommands;

        /// <summary>
        /// Creates an instance of the <see cref="SQLiteAttachedConnection"/>.
        /// </summary>
        /// <param name="dbFiles">Database files to attached where the key is the alias to be used for the database.</param>
        public SQLiteAttachedConnection(IReadOnlyDictionary<string, FileInfo> dbFiles) 
            : base(SQLiteConnectionStringProvider.GetInMemoryConnectionString())
        {
            FilesToAttach = dbFiles ?? throw new ArgumentNullException(nameof(dbFiles));

            if (dbFiles.Count == 0)
            {
                throw new ArgumentException(nameof(dbFiles) + " cannot be empty.");
            }
            
            var cmdBuilder = StringBuilderCache.Acquire();
            foreach (var pair in dbFiles)
            {
                cmdBuilder.AppendFormat("ATTACH DATABASE '{0}' AS '{1}';\r\n", 
                    pair.Value.FullName, pair.Key);
            }

            _attachCommands = StringBuilderCache.GetStringAndRelease(cmdBuilder);
        }

        /// <summary>
        /// Gets the database files that should be attached to the connection.
        /// </summary>
        public IReadOnlyDictionary<string, FileInfo> FilesToAttach { get; }

        /// <summary>
        /// Opens the connection and runs the command to attach the <see cref="FilesToAttach"/>.
        /// </summary>
        public override void Open()
        {
            Connection.Open();
            Connection.Execute(_attachCommands);
        }

        /// <summary>
        /// Opens the connection and runs the command to attach the <see cref="FilesToAttach"/>.
        /// </summary>
        public override Task OpenAsync()
        {
            Connection.Open();
            return Connection.ExecuteAsync(_attachCommands);
        }

        /// <summary>
        /// Disposes and finalizes the connection, if applicable.
        /// </summary>
        public override void Dispose() => Connection.Dispose();
    }
}