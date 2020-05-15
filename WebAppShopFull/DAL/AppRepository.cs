using System.Data;

namespace DAL
{
    public class AppRepository
    {
        IDbConnection connection;
        public AppRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        ProductRepository product;
        CategoryRepository category;
        RoleRepository role;
        CartRepository cart;
        MemberRepository member;
        MemberInRoleRepository memberInRole;
        public MemberInRoleRepository MemberInRole
        {
            get
            {
                if (memberInRole is null)
                {
                    memberInRole = new MemberInRoleRepository(connection);
                }
                return memberInRole;
            }
        }
        public MemberRepository Member
        {
            get
            {
                if(member is null)
                {
                    member = new MemberRepository(connection);
                }
                return member;
            }
        }
        public CartRepository Cart
        {
            get
            {
                if(cart is null)
                {
                    cart = new CartRepository(connection);
                }
                return cart;
            }
        }
        public RoleRepository Role
        {
            get
            {
                if(role is null)
                {
                    role = new RoleRepository(connection);
                }
                return role;
            }
        }
        public CategoryRepository Category
        {
            get
            {
                if(category is null)
                {
                    category = new CategoryRepository(connection);
                }
                return category;
            }
        }
        public ProductRepository Product
        {
            get
            {
                if(product is null)
                {
                    product = new ProductRepository(connection);
                }
                return product;
            }
        }

    }
}
