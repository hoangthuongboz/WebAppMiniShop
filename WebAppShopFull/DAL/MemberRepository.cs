using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class MemberRepository : BaseRepository<Member>
    {
        public MemberRepository(IDbConnection connection) : base(connection) { }
        protected override Member Fetch(IDataReader reader)
        {
            return new Member
            {
                Id = (long)reader["MemberId"],
                Username = (string)reader["Username"],
                Email = (string)reader["Email"]
            };
        }
        //get role
        static Role FetchRole(IDataReader reader)
        {
            return new Role
            {
                Id = (int)reader["RoleId"],
                Name = (string)reader["RoleName"],
                Checked = (bool)reader["Checked"]
            };
        }
        static Role FetchOneRole(IDataReader reader)
        {
            return new Role
            {
                Id = (int)reader["RoleId"],
                Name = (string)reader["RoleName"]
            };
        }
        //get list role
        static List<Role> FetchRoles(IDataReader reader)
        {
            List<Role> list = new List<Role>();
            while (reader.Read())
            {
                list.Add(FetchRole(reader));
            }
            return list;
        }
        static List<Role> FetchAllRole(IDataReader reader)
        {
            List<Role> list = new List<Role>();
            while (reader.Read())
            {
                list.Add(FetchOneRole(reader));
            }
            return list;
        }
        public Member GetMemberAndRoles(long id)
        {
            using(IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "GetMemberAndRoles";
                command.CommandType = CommandType.StoredProcedure;
                SetParameter(command, new Parameter { Name = "@Id", Value = id, DbType = DbType.Int64 });
                using(IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Member obj = Fetch(reader);
                        if (reader.NextResult())
                        {
                            obj.Roles = FetchRoles(reader);
                        }
                        return obj;
                    }
                    return null;
                }
            }
        }
        //Hash Password
        static byte[] Hash(string username, string password)
        {
            return Helper.Hash(username + "$@#%%@@" + password);
        }
        //Add obj
        public int Add(Member obj)
        {
            using(IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "AddMember";
                command.CommandType = CommandType.StoredProcedure;

                Parameter[] parameters =
                {
                new Parameter{ Name = "@Id", Value = obj.Id, DbType = DbType.Int64},
                new Parameter{ Name = "@Username", Value = obj.Username, DbType = DbType.String},
                new Parameter{ Name = "@Password", Value = Hash(obj.Username,obj.Password), DbType = DbType.Binary},
                new Parameter{ Name = "@Email", Value = obj.Email, DbType = DbType.String},
                new Parameter{ Name = "@Ret", DbType = DbType.Int32, Direction = ParameterDirection.ReturnValue}
                };
                SetParameter(command, parameters);

                if (command.ExecuteNonQuery() > 0)
                {
                    return 2;
                }
                return (int)command.Parameters["@Ret"];
            }
            
        }
        //Sign In
        public Member SignIn(string usr,string pwd)
        {
            using(IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "SignIn";
                command.CommandType = CommandType.StoredProcedure;
                Parameter[] parameters =
                {
                    new Parameter{ Name = "@Username", Value = usr, DbType = DbType.String},
                    new Parameter{ Name = "@Password", Value = Hash(usr,pwd), DbType = DbType.Binary}
                };
                SetParameter(command, parameters);
                using(IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Member obj = Fetch(reader);
                        if (obj != null && reader.NextResult())
                        {
                            obj.Roles = FetchAllRole(reader);
                        }
                        return obj;
                    }
                    return null;
                }
            }
        }
        //Get obj
        public Member GetMemberById(long id)
        {
            return QueryOne("GetMemberById", new Parameter { Name = "@Id", Value = id, DbType = DbType.Int64});
        }
        //Get list obj
        public List<Member> GetMembers()
        {
            return Query("GetMembers");
        }
        public int Update(Member obj)
        {
            Parameter[] parameters =
            {
                new Parameter{ Name = "@Username", Value = obj.Username, DbType = DbType.String},
                new Parameter{ Name = "@OldPassword", Value = Hash(obj.Username,obj.OldPassword), DbType = DbType.Binary},
                new Parameter{ Name = "@NewPassword", Value =  Hash(obj.Username,obj.Password), DbType = DbType.Binary}
            };
            return Save("UpdateMember", parameters);
        }
    }
}
