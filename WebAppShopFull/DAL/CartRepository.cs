using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class CartRepository : BaseRepository<Cart>
    {
        internal CartRepository(IDbConnection connection) : base(connection)
        {
        }

        protected override Cart Fetch(IDataReader reader)
        {
            return new Cart
            {
                Id = (long)reader["CartId"],
                ProductId = (int)reader["ProductId"],
                ProductName = (string)reader["ProductName"],
                Price = (int)reader["Price"],
                ImageUrl = reader["ImageUrl"] != DBNull.Value ? (string)reader["ImageUrl"] : null,
                Quantity = (short)reader["Quantity"]
            };
        }
        public List<Cart> GetCarts(long id)
        {
            return Query("GetCarts", new Parameter { Name = "@Id", Value = id, DbType = DbType.Int64 });
        }
        public int Add(Cart obj)
        {
            Parameter[] parameters =
            {
                new Parameter{ Name = "@Id", Value = obj.Id, DbType = DbType.Int64},
                new Parameter{ Name = "@ProductId", Value = obj.ProductId, DbType = DbType.Int32},
                new Parameter{ Name = "@Quantity", Value = obj.Quantity, DbType = DbType.Int16}
            };
            return Save("AddCart", parameters);
        }
        public int Delete(long id, int productId)
        {
            Parameter[] parameters =
            {
                new Parameter{ Name = "@Id", Value = id, DbType = DbType.Int64},
                new Parameter{ Name = "@ProductId", Value = productId, DbType = DbType.Int32}
            };
            return Save("DeleteCart", parameters);
        }
        public int Edit(Cart obj)
        {
            Parameter[] parameters =
            {
                new Parameter{ Name = "@Id", Value = obj.Id, DbType = DbType.Int64},
                new Parameter{ Name = "@ProductId", Value = obj.ProductId, DbType = DbType.Int32},
                new Parameter{ Name = "@Quantity", Value = obj.Quantity, DbType = DbType.Int16}
            };
            return Save("EditCart", parameters);
        }
    }
}
