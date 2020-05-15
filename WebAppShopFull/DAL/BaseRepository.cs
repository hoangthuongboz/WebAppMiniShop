using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public abstract class BaseRepository<T>
    {
        protected IDbConnection connection;
        internal BaseRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        //get obj  
        protected abstract T Fetch(IDataReader reader);
        protected T Fetch(IDbCommand command)
        {
            using(IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return Fetch(reader);
                }
                return default(T);
            }
        }
        protected List<T> FetchAll(IDataReader reader)
        {
            List<T> list = new List<T>();
            while (reader.Read())
            {
                list.Add(Fetch(reader));
            }
            return list;
        }
        protected List<T> FetchAll(IDbCommand command)
        {
            using(IDataReader reader = command.ExecuteReader())
            {
                return FetchAll(reader);
            }
        }
        //gan gia tri cho tham so
        protected static void SetParameter(IDbCommand command, Parameter parameter)
        {
            IDataParameter dataParameter = command.CreateParameter();
            dataParameter.ParameterName = parameter.Name;
            dataParameter.Value = parameter.Value;
            dataParameter.DbType = parameter.DbType;
            //neu ton tai thi gan gia tri
            if (Enum.IsDefined(typeof(ParameterDirection), parameter.Direction))
            {
                dataParameter.Direction = parameter.Direction;
            }
            command.Parameters.Add(dataParameter);
        }
        //gan nhieu gia tri cho tham so
        protected static void SetParameter(IDbCommand command, Parameter[] parameters)
        {
            foreach (Parameter item in parameters)
            {
                SetParameter(command, item);
            }
        }
        //add
        protected int Save(string sql, Parameter[] parameters,CommandType commandType = CommandType.StoredProcedure)
        {
            using(IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = commandType;
                SetParameter(command, parameters);
                return command.ExecuteNonQuery();
            }
        } 
        //delete
        protected int Save(string sql, Parameter parameter, CommandType commandType = CommandType.StoredProcedure)
        {
            using(IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = commandType;
                SetParameter(command, parameter);
                return command.ExecuteNonQuery();
            }
        }
        //get 1 list
        protected List<T> Query(string sql, CommandType commandType = CommandType.StoredProcedure)
        {
            using(IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = commandType;
                return FetchAll(command);
            }
        }
        //get 1 list by Id( vd :Get list product By CategoryId)
        protected List<T> Query(string sql, Parameter parameter, CommandType commandType = CommandType.StoredProcedure)
        {
            using(IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = commandType;
                SetParameter(command, parameter);
                return FetchAll(command);
            }
        }
        //Get 1 list by nhieu tham so
        protected List<T> Query(string sql, Parameter[] parameters,CommandType commandType = CommandType.StoredProcedure)
        {
            using(IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = commandType;
                SetParameter(command, parameters);
                return FetchAll(command);
            }
        }
        //get obj (GetTId)   
        protected T QueryOne(string sql, Parameter parameter, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = commandType;
                SetParameter(command, parameter);
                return Fetch(command);
            }
        }
        protected T QueryOne(string sql, Parameter[] parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = commandType;
                SetParameter(command, parameters);
                return Fetch(command);
            }
        }
    }
}
