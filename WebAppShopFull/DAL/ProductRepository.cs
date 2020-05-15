using DTO;
using System;
using System.Collections.Generic;
using System.Data;


namespace DAL
{
    public class ProductRepository : BaseRepository<Product>
    {
        internal ProductRepository(IDbConnection connection) : base(connection) { }
        protected override Product Fetch(IDataReader reader)
        {
            return new Product
            {
                Id = (int)reader["ProductId"],
                Name = (string)reader["ProductName"],
                CategoryId = (int)reader["CategoryId"],
                Price = (int)reader["Price"],
                Quantity = (short)reader["Quantity"],
                ImageUrl = reader["ImageUrl"] != DBNull.Value ? (string)reader["ImageUrl"] : null,
                Description = (string)reader["Description"]
            };
        }
        //Add obj
        public int Add(Product obj)
        {
            Parameter[] parameters =
            {
                //new Parameter{ Name = "@Id", Value = obj.Id, DbType = DbType.Int32},
                new Parameter{ Name = "@Name", Value = obj.Name, DbType = DbType.String},
                new Parameter{ Name = "@CategoryId", Value = obj.CategoryId, DbType = DbType.Int32},
                new Parameter{ Name = "@Price", Value = obj.Price, DbType = DbType.Int32},
                new Parameter{ Name = "@Quantity", Value = obj.Quantity, DbType = DbType.Int16},
                new Parameter{ Name = "@ImageUrl", Value = obj.ImageUrl, DbType = DbType.String},
                new Parameter{ Name = "@Description", Value = obj.Description, DbType = DbType.String},
            };
            return Save("AddProduct", parameters);
        }
        //Get 1 list
        public List<Product> GetProducts()
        {
            return Query("GetProducts");
        }
        //get 1 obj by Id
        public Product GetProductById(int id)
        {
            return QueryOne("GetProductById", new Parameter { Name = "@Id", Value = id, DbType = DbType.Int32 });
        }
        //get list product by categoryId
        public List<Product> GetProductsByCategory(int id)
        {
            return Query("GetProductsByCategory", new Parameter { Name = "@Id", Value = id, DbType = DbType.Int32 });
        }
        //delete
        public int Edit(Product obj)
        {
            Parameter[] parameters =
            {
                new Parameter{ Name = "@Id", Value = obj.Id, DbType = DbType.Int32},
                new Parameter{ Name = "@Name", Value = obj.Name, DbType = DbType.String},
                new Parameter{ Name = "@CategoryId", Value = obj.CategoryId, DbType = DbType.Int32},
                new Parameter{ Name = "@Price", Value = obj.Price, DbType = DbType.Int32},
                new Parameter{ Name = "@Quantity", Value = obj.Quantity, DbType = DbType.Int16},
                new Parameter{ Name = "@ImageUrl", Value = obj.ImageUrl, DbType = DbType.String},
                new Parameter{ Name = "@Description", Value = obj.Description, DbType = DbType.String},
            };
            return Save("EditProduct", parameters);
        }
        public int Delete(int id)
        {
            return Save("DeleteProduct", new Parameter { Name = "@Id", Value = id, DbType = DbType.Int32 });
        }
        //Delete Ids
        public int Delete(int[] ids)
        {
            using(IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "DeleteProduct";
                command.CommandType = CommandType.StoredProcedure;
                IDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@Id";
                parameter.DbType = DbType.Int32;
                command.Parameters.Add(parameter);

                //gia tri tra ve
                int ret = 0;
                foreach (int item in ids)
                {
                    parameter.Value = item;
                    ret += command.ExecuteNonQuery();
                }
                return ret;
            }
        }
        //Pagination
        public List<Product> GetProductsPagination(out int total,int index,int size)
        {
            using(IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "GetProductsPagination";
                command.CommandType = CommandType.StoredProcedure;
                Parameter[] parameters =
                {
                    new Parameter { Name = "@Start", DbType = DbType.Int32, Value = ((index-1)*size + 1)},
                    new Parameter { Name = "@End", DbType = DbType.Int32, Value = index * size},
                    new Parameter { Name = "@Total", DbType = DbType.Int32, Direction = ParameterDirection.Output},
                };
                SetParameter(command, parameters);
                List<Product> list = FetchAll(command);
                IDbDataParameter parameter = (IDbDataParameter)command.Parameters["@Total"];
                total = (int)parameter.Value;
                return list;
            }
        }
        //Get ProductById And Get 1 list product(not get Product by id)
        public List<Product> GetProductsRelation(int id, int categoryId)
        {
            Parameter[] parameters =
            {
                new Parameter{ Name = "@Id", Value = id, DbType = DbType.Int32},
                new Parameter{ Name = "@CategoryId", Value = categoryId, DbType = DbType.Int32}
            };
            return Query("GetProductsRelation", parameters);
        }
        //Get products by Id(Category)
        public Product GetProductsDetail(int id)
        {
            using(IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "GetProductsDetail";
                command.CommandType = CommandType.StoredProcedure;
                SetParameter(command, new Parameter { Name = "@Id", Value = id, DbType = DbType.Int32 });
                using(IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Product obj = Fetch(reader);
                        if (reader.NextResult())
                        {
                            obj.Relation = FetchAll(reader);
                        }
                        return obj;
                    }
                    return null;
                }
            }
        }
        //Search
        public List<Product> Search(string q , out int total, int index, int size = 9)
        {
            using(IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "SearchProductsPagination";
                command.CommandType = CommandType.StoredProcedure;
                Parameter[] parameters =
                {
                    new Parameter { Name = "@Q", Value = ('%' + q + '%'), DbType = DbType.String},
                    new Parameter { Name = "@Start", DbType = DbType.Int32, Value = ((index-1)*size + 1)},
                    new Parameter { Name = "@End", DbType = DbType.Int32, Value = index * size},
                    new Parameter { Name = "@Total", DbType = DbType.Int32, Direction = ParameterDirection.Output},
                };
                SetParameter(command, parameters);
                List<Product> list = FetchAll(command);
                IDbDataParameter parameter = (IDbDataParameter)command.Parameters["@Total"];
                total = (int)parameter.Value;
                return list;
            }
        }
        public List<Product> SearchAdvance(int? cid, int? price, string name)
        {
            Parameter[] parameters =
            {
                new Parameter{ Name = "@CategoryId", Value = cid, DbType = DbType.Int32},
                new Parameter{ Name = "@Price", Value = price, DbType = DbType.Int32},
                new Parameter{ Name = "@Name", Value = name, DbType = DbType.String}
            };
            return Query("SearchProductsAdvance", parameters);
        }
    }
}
