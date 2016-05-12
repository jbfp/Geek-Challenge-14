using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNet.Identity;

namespace SGC14.Web.Models.Authentication
{
    public class SqlUserStore : IUserLoginStore<User, Guid>, IUserClaimStore<User, Guid>
    {
        private readonly IAsyncDbConnectionFactory connectionFactory;

        public SqlUserStore(IAsyncDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }
                        
            this.connectionFactory = connectionFactory;
        }

        public async Task CreateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            using (IDbConnection connection = await this.connectionFactory.CreateConnectionAsync())
            {
                await connection.ExecuteAsync("INSERT INTO [dbo].[Users]([Id], [UserName]) VALUES(@Id, @UserName);",
                    new { user.Id, user.UserName });
            }
        }

        public async Task UpdateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            using (IDbConnection connection = await this.connectionFactory.CreateConnectionAsync())
            {
                await connection.ExecuteAsync("UPDATE [dbo].[Users] SET [UserName] = @UserName WHERE [Id] = @Id;",
                    new { user.Id, user.UserName });
            }
        }

        public async Task DeleteAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            using (IDbConnection connection = await this.connectionFactory.CreateConnectionAsync())
            {
                await connection.ExecuteAsync("DELETE FROM [dbo].[Users] WHERE [Id] = @Id;",
                    new { user.Id });
            }
        }

        public async Task<User> FindByIdAsync(Guid userId)
        {
            using (IDbConnection connection = await this.connectionFactory.CreateConnectionAsync())
            {
                IEnumerable<User> users = await connection.QueryAsync<User>("SELECT [Id], [UserName] FROM [dbo].[Users] WHERE [Id] = @userId;",
                    new { userId });
                return users.SingleOrDefault();
            }
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            if (userName == null)
            {
                throw new ArgumentNullException("userName");
            }

            using (IDbConnection connection = await this.connectionFactory.CreateConnectionAsync())
            {
                IEnumerable<User> users = await connection.QueryAsync<User>("SELECT [Id], [UserName] FROM [dbo].[Users] WHERE [UserName] = @userName;",
                    new { userName });
                return users.SingleOrDefault();
            }
        }

        public async Task AddLoginAsync(User user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            using (IDbConnection connection = await this.connectionFactory.CreateConnectionAsync())
            {
                await connection.ExecuteAsync("INSERT INTO [dbo].[UserLogins]([LoginProvider], [ProviderKey], [UserId]) VALUES(@LoginProvider, @ProviderKey, @Id);",
                    new { login.LoginProvider, login.ProviderKey, user.Id });
            }
        }

        public async Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            using (IDbConnection connection = await this.connectionFactory.CreateConnectionAsync())
            {
                await connection.ExecuteAsync("DELETE FROM [dbo].[UserLogins] WHERE [UserId] = @Id;",
                    new { user.Id });
            }
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            using (IDbConnection connection = await this.connectionFactory.CreateConnectionAsync())
            {
                IEnumerable<UserLoginInfo> userLogins = await connection.QueryAsync<UserLoginInfo>("SELECT * FROM [dbo].[UserLogins] WHERE [UserId] = @Id;",
                    new { user.Id });
                return userLogins.ToList();
            }
        }

        public async Task<User> FindAsync(UserLoginInfo login)
        {
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            using (IDbConnection connection = await this.connectionFactory.CreateConnectionAsync())
            {
                IEnumerable<User> users = await connection.QueryAsync<User>("SELECT U.[Id], U.[UserName] FROM [dbo].[Users] AS U " +
                                                                            "JOIN [dbo].[UserLogins] AS UL " +
                                                                            "ON U.[Id] = UL.[UserId] " +
                                                                            "WHERE UL.[LoginProvider]= @LoginProvider AND " +
                                                                            "UL.[ProviderKey] = @ProviderKey;",
                    new { login.LoginProvider, login.ProviderKey });
                return users.SingleOrDefault();
            }
        }

        public async Task<IList<Claim>> GetClaimsAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            using (var connection = await this.connectionFactory.CreateConnectionAsync())
            {
                IEnumerable<UserClaim> claims = await connection.QueryAsync<UserClaim>("SELECT * FROM [dbo].[UserClaims] WHERE [UserId] = @Id;",
                    new { user.Id });
                return claims.Select(uc => new Claim(uc.ClaimType, uc.ClaimValue)).ToList();
            }
        }

        public async Task AddClaimAsync(User user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            using (var connection = await this.connectionFactory.CreateConnectionAsync())
            {
                await connection.ExecuteAsync("INSERT INTO [dbo].[UserClaims]([ClaimId], [UserId], [ClaimType], [ClaimValue]) VALUES(@ClaimId, @Id, @Type, @Value);",
                    new { ClaimId = Guid.NewGuid(), user.Id, claim.Type, claim.Value });
            }
        }

        public async Task RemoveClaimAsync(User user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            using (var connection = await this.connectionFactory.CreateConnectionAsync())
            {
                await connection.ExecuteAsync("DELETE FROM [dbo].[UserClaims] WHERE [ClaimType] = @Type AND [UserId] = @Id;", new { claim.Type, user.Id });
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}