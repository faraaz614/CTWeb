using CT.Service.Entities;
using CT.Service.Literals;
using CT.Service.Repository;
using Dapper;
using System;
using System.Data;

namespace CT.Service.UserService
{
    public class UserService : GenericRepository<UserEntity>
    {

        public BaseEntity SaveDealer(UserEntity userEntity)
        {
            BaseEntity entity = new BaseEntity();
            try
            {
                Log.Info("----Info SaveDealer method start----");
                Log.Info("@FirstName" + userEntity.FirstName);
                Log.Info("@LastName" + userEntity.LastName);
                Log.Info("@UserName" + userEntity.UserName);
                Log.Info("@Password" + userEntity.Password);
                Log.Info("@ProfilePic" + userEntity.ProfilePic);
                Log.Info("Store Proc Name : USP_CT_SaveUser");
                Log.Info("----Info SaveDealer method end----");
                DynamicParameters param = new DynamicParameters();
                param.Add("@FirstName", userEntity.FirstName);
                param.Add("@LastName", userEntity.LastName);
                param.Add("@UserName", userEntity.UserName);
                param.Add("@RoleID", userEntity.RoleID);
                param.Add("@UserName", userEntity.UserName);
                param.Add("@Password", userEntity.Password);
                param.Add("@ProfilePic", userEntity.ProfilePic);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                int updateUser = Execute(CommandType.StoredProcedure, UserLiterals.SaveUser, param);
                if (updateUser != 0) updateUser = param.Get<dynamic>("@spResult");
                Log.Info("----Info SaveDealer method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in SaveDealer Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }
    }
}
