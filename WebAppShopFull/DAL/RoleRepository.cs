using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class RoleRepository : BaseRepository<Role>
    {
        public RoleRepository(IDbConnection connection) : base(connection)
        {
        }

        protected override Role Fetch(IDataReader reader)
        {
            return new Role
            {
                Id = (int)reader["RoleId"],
                Name = (string)reader["RoleName"]
            };
        }

        public List<Role> GetRoles()
        {
            return Query("GetRoles");
        }

        public int Add(Role obj)
        {
            Parameter[] parameters =
            {
                new Parameter{ Name = "@Id", Value = Helper.RandomInt(), DbType = DbType.Int32},
                new Parameter{ Name = "@Name", Value = obj.Name, DbType = DbType.String}
            };
            return Save("AddRole", parameters);
        }
        public Role GetRoleById(int id)
        {
            return QueryOne("GetRoleById", new Parameter { Name = "@Id", Value = id, DbType = DbType.Int32 });
        }

        public int Edit(Role obj)
        {
            Parameter[] parameters =
            {
                new Parameter{ Name = "@Id", Value = obj.Id, DbType = DbType.Int32},
                new Parameter{ Name = "@Name", Value = obj.Name, DbType = DbType.String}
            };
            return Save("EditRole", parameters);
        }

        public int Delete(int id)
        {
            return Save("DeleteRole", new Parameter { Name = "@Id", Value = id, DbType = DbType.Int32 });
        }
    }
}
