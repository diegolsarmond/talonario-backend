using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TalonarioTests.InfrastructureTests.Fakes
{
    internal class FakeDbConnection : DbConnection
    {
        private ConnectionState _state = ConnectionState.Closed;

        public List<FakeDbCommandExecution> ExecutedCommands { get; } = new();

        public override string ConnectionString { get; set; } = string.Empty;

        public override string Database => "Fake";

        public override string DataSource => "Fake";

        public override string ServerVersion => "1.0";

        public override ConnectionState State => _state;

        public override void ChangeDatabase(string databaseName)
        {
        }

        public override void Close()
        {
            _state = ConnectionState.Closed;
        }

        public override void Open()
        {
            _state = ConnectionState.Open;
        }

        public override Task OpenAsync(CancellationToken cancellationToken)
        {
            Open();
            return Task.CompletedTask;
        }

        public override Task CloseAsync()
        {
            Close();
            return Task.CompletedTask;
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            return new FakeDbTransaction(this, isolationLevel);
        }

        public override Task<DbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken)
        {
            return Task.FromResult<DbTransaction>(new FakeDbTransaction(this, isolationLevel));
        }

        public override ValueTask<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            return new ValueTask<DbTransaction>(new FakeDbTransaction(this, IsolationLevel.Unspecified));
        }

        protected override DbCommand CreateDbCommand()
        {
            return new FakeDbCommand(this);
        }

        internal void RecordExecution(string commandText, IReadOnlyList<DbParameter> parameters)
        {
            ExecutedCommands.Add(new FakeDbCommandExecution(commandText, parameters));
        }

        public override ValueTask DisposeAsync()
        {
            Close();
            return ValueTask.CompletedTask;
        }
    }

    internal sealed class FakeDbTransaction : DbTransaction
    {
        private readonly FakeDbConnection _connection;

        public FakeDbTransaction(FakeDbConnection connection, IsolationLevel isolationLevel)
        {
            _connection = connection;
            IsolationLevel = isolationLevel;
        }

        public override IsolationLevel IsolationLevel { get; }

        protected override DbConnection DbConnection => _connection;

        public override void Commit()
        {
        }

        public override void Rollback()
        {
        }

        public override Task CommitAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public override Task RollbackAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public override ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }
    }

    internal sealed class FakeDbCommand : DbCommand
    {
        private readonly FakeDbConnection _connection;
        private readonly FakeDbParameterCollection _parameters = new();

        public FakeDbCommand(FakeDbConnection connection)
        {
            _connection = connection;
        }

        public override string CommandText { get; set; } = string.Empty;

        public override int CommandTimeout { get; set; } = 30;

        public override CommandType CommandType { get; set; } = CommandType.Text;

        protected override DbConnection DbConnection
        {
            get => _connection;
            set { }
        }

        protected override DbParameterCollection DbParameterCollection => _parameters;

        protected override DbTransaction DbTransaction { get; set; }
        
        public override bool DesignTimeVisible { get; set; }

        public override UpdateRowSource UpdatedRowSource { get; set; }

        public override void Cancel()
        {
        }

        public override int ExecuteNonQuery()
        {
            _connection.RecordExecution(CommandText, _parameters.ToArray());
            return 1;
        }

        public override object ExecuteScalar()
        {
            _connection.RecordExecution(CommandText, _parameters.ToArray());
            return 0;
        }

        public override void Prepare()
        {
        }

        protected override DbParameter CreateDbParameter()
        {
            return new FakeDbParameter();
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            throw new NotSupportedException();
        }

        public override Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
        {
            _connection.RecordExecution(CommandText, _parameters.ToArray());
            return Task.FromResult(1);
        }

        public override Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
        {
            _connection.RecordExecution(CommandText, _parameters.ToArray());
            return Task.FromResult<object>(0);
        }

        protected override ValueTask<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        public override ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }
    }

    internal sealed class FakeDbParameterCollection : DbParameterCollection
    {
        private readonly List<DbParameter> _parameters = new();

        public override int Count => _parameters.Count;

        public override object SyncRoot => ((System.Collections.ICollection)_parameters).SyncRoot;

        public override int Add(object value)
        {
            _parameters.Add((DbParameter)value);
            return _parameters.Count - 1;
        }

        public override void AddRange(Array values)
        {
            foreach (var value in values)
            {
                Add(value);
            }
        }

        public override void Clear()
        {
            _parameters.Clear();
        }

        public override bool Contains(object value)
        {
            return _parameters.Contains((DbParameter)value);
        }

        public override bool Contains(string value)
        {
            return _parameters.Any(p => string.Equals(p.ParameterName, value, StringComparison.Ordinal));
        }

        public override void CopyTo(Array array, int index)
        {
            ((System.Collections.ICollection)_parameters).CopyTo(array, index);
        }

        public override System.Collections.IEnumerator GetEnumerator()
        {
            return _parameters.GetEnumerator();
        }

        public override int IndexOf(object value)
        {
            return _parameters.IndexOf((DbParameter)value);
        }

        public override int IndexOf(string parameterName)
        {
            return _parameters.FindIndex(p => string.Equals(p.ParameterName, parameterName, StringComparison.Ordinal));
        }

        public override void Insert(int index, object value)
        {
            _parameters.Insert(index, (DbParameter)value);
        }

        public override void Remove(object value)
        {
            _parameters.Remove((DbParameter)value);
        }

        public override void RemoveAt(int index)
        {
            _parameters.RemoveAt(index);
        }

        public override void RemoveAt(string parameterName)
        {
            var index = IndexOf(parameterName);
            if (index >= 0)
            {
                _parameters.RemoveAt(index);
            }
        }

        protected override DbParameter GetParameter(int index)
        {
            return _parameters[index];
        }

        protected override DbParameter GetParameter(string parameterName)
        {
            var index = IndexOf(parameterName);
            if (index < 0)
            {
                throw new IndexOutOfRangeException($"Parameter '{parameterName}' was not found.");
            }

            return _parameters[index];
        }

        protected override void SetParameter(int index, DbParameter value)
        {
            _parameters[index] = value;
        }

        protected override void SetParameter(string parameterName, DbParameter value)
        {
            var index = IndexOf(parameterName);
            if (index >= 0)
            {
                _parameters[index] = value;
            }
            else
            {
                _parameters.Add(value);
            }
        }

        internal IReadOnlyList<DbParameter> ToArray()
        {
            return _parameters.ToArray();
        }

        public override bool IsFixedSize => false;

        public override bool IsReadOnly => false;

        public override bool IsSynchronized => false;

        public override object this[int index]
        {
            get => _parameters[index];
            set => _parameters[index] = (DbParameter)value;
        }

        public override object this[string parameterName]
        {
            get => GetParameter(parameterName);
            set => SetParameter(parameterName, (DbParameter)value);
        }
    }

    internal sealed class FakeDbParameter : DbParameter
    {
        public override DbType DbType { get; set; }

        public override ParameterDirection Direction { get; set; } = ParameterDirection.Input;

        public override bool IsNullable { get; set; }

        public override string ParameterName { get; set; } = string.Empty;

        public override string SourceColumn { get; set; } = string.Empty;

        public override object Value { get; set; }
        
        public override bool SourceColumnNullMapping { get; set; }

        public override int Size { get; set; }

        public override DataRowVersion SourceVersion { get; set; } = DataRowVersion.Current;

        public override void ResetDbType()
        {
        }
    }

    internal sealed record FakeDbCommandExecution(string CommandText, IReadOnlyList<DbParameter> Parameters);
}
