using DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class CategoryRepository : BaseRepository<Category>
    {
        //su dung internal de tranh khai tao doi tuong 
        internal CategoryRepository(IDbConnection connection) : base(connection) { }
        protected override Category Fetch(IDataReader reader)
        {
            return new Category
            {
                Id = (int)reader["CategoryId"],
                Name = (string)reader["CategoryName"],
                Description = (string)reader["Description"]
            };
        }
        static Product FetchProduct(IDataReader reader)
        {
            return new Product
            {
                Id = (int)reader["ProductId"],
                Name = (string)reader["ProductName"],
                CategoryId = (int)reader["CategoryId"],
                Description = (string)reader["Description"],
                Price = (int)reader["Price"],
                Quantity = (short)reader["Quantity"],
                ImageUrl = reader["ImageUrl"] != DBNull.Value ? (string)reader["ImageUrl"] : null
            };
        }
        static List<Product> GetProducts(IDataReader reader)
        {
            List<Product> list = new List<Product>();
            while (reader.Read())
            {
                list.Add(FetchProduct(reader));
            }
            return list;
        }
        public List<Category> GetCategoriesAndProducts()
        {
            using(IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "GetCategoriesAndProducts";
                command.CommandType = CommandType.StoredProcedure;
                using(IDataReader reader = command.ExecuteReader())
                {
                    Dictionary<int, List<Product>> dict = new Dictionary<int, List<Product>>();
                    while (reader.Read())
                    {
                        Product product = FetchProduct(reader);
                        if (!dict.ContainsKey(product.CategoryId))
                        {
                            dict[product.CategoryId] = new List<Product>();
                        }
                        dict[product.CategoryId].Add(product);
                    }
                    if (reader.NextResult())
                    {
                        List<Category> list = new List<Category>();
                        while (reader.Read())
                        {
                            Category obj = Fetch(reader);
                            obj.Products = dict[obj.Id];
                            list.Add(obj);
                        }
                        return list;
                    }
                }
            }
            return null;
        }
        //Add obj
        public int Add(Category obj)
        {
            Parameter[] parameters =
            {
                new Parameter{Name="@Name",Value=obj.Name,DbType=DbType.String},
                new Parameter{Name="@Description",Value=obj.Description,DbType=DbType.String}
            };
            return Save("AddCategory", parameters);
        }
        //get 1 list
        public List<Category> GetCategories()
        {
            return Query("GetCategories");
        }
        //get 1 obj by Id
        public Category GetCategoryById(int id)
        {
            return QueryOne("GetCategoryById", new Parameter { Name = "@Id", Value = id, DbType = DbType.Int32 });
        }
        //delete obj
        public int Delete(int id)
        {
            return Save("DeleteCategory", new Parameter { Name = "@Id", Value = id, DbType = DbType.Int32 });
        }
        //Delete ids
        public int Delete(int[] ids)
        {
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "DeleteCategory";
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
        //Edit
        public int Edit(Category obj)
        {
            Parameter[] parameters =
            {
                new Parameter{ Name = "@Id", Value = obj.Id, DbType = DbType.Int32},
                new Parameter{ Name = "@Name", Value = obj.Name, DbType = DbType.String},
                new Parameter{ Name = "@Description", Value = obj.Description, DbType = DbType.String}
            };
            return Save("EditCategory", parameters);
        }
        
        public Category GetCategoryDetail(int id)
        {
            using(IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "GetCategoryDetail";
                command.CommandType = CommandType.StoredProcedure;
                SetParameter(command, new Parameter { Name = "@Id", Value = id, DbType = DbType.Int32 });
                using(IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Category obj = Fetch(reader);
                        if (reader.NextResult())
                        {
                            obj.Products = GetProducts(reader);
                        }
                        return obj;
                    }
                    return null;
                }
            }
        }
    }
}
