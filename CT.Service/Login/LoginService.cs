using CT.Common.Common;
using CT.Common.Literals;
using CT.Service.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Service.Login
{
    public class LoginService : GenericRepository<UserEntity>
    {
        public UserEntity Login(UserEntity user)
        {
            UserEntity entity = new UserEntity();
            try
            {
                Log.Info("----Info Login method start----");
                Log.Info("@UserName" + user.UserName);
                Log.Info("@Password" + user.Password);
                Log.Info("Store Proc Name : USP_CT_Login");
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserName", user.UserName);
                param.Add("@Password", user.Password);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity = GetSingleItem<UserEntity>(CommandType.StoredProcedure, UserLiterals.Login, param);
                if (entity == null)
                    entity = new UserEntity();
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                Log.Info("----Info Login method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                if (entity == null)
                    entity = new UserEntity();
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in Login Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }
    }
}
