using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class MemberInRoleRepository :BaseRepository<MemberInRole>
    {
        internal MemberInRoleRepository(IDbConnection connection) : base(connection) { }

        protected override MemberInRole Fetch(IDataReader reader)
        {
            return new MemberInRole
            {
                MemberId = (long)reader["MemberId"],
                RoleId = (int)reader["RoleId"]
            };
        }
        public int Save(MemberInRole obj)
        {
            Parameter[] parameters =
            {
                new Parameter{ Name = "@MemberId", Value = obj.MemberId, DbType = DbType.Int64},
                new Parameter{ Name = "@RoleId", Value = obj.RoleId, DbType = DbType.Int32}
            };
            return Save("SaveMemberInRole", parameters);
        }
    }
}
