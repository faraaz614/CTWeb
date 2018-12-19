﻿using CT.Common.Common;
using CT.Common.Literals;
using CT.Service.Repository;
using Dapper;
using System;
using System.Data;

namespace CT.Service.UserService
{
    public class UserService : GenericRepository<UserEntity>
    {
        public BaseEntity InsertDealer(UserEntity userEntity)
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
                param.Add("@UserID", userEntity.ID);
                param.Add("@RoleID", userEntity.RoleID);
                param.Add("@UserName", userEntity.UserName);
                param.Add("@Password", userEntity.Password);
                param.Add("@ProfilePic", userEntity.ProfilePic);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.GenericErrorInfo = GetSingleItem<GenericErrorInfo>(CommandType.StoredProcedure, UserLiterals.SaveUser, param);
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
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

        public BaseEntity UpdateDealer(UserEntity userEntity)
        {
            BaseEntity entity = new BaseEntity();
            try
            {
                Log.Info("----Info UpdateDealer method start----");
                Log.Info("@FirstName" + userEntity.FirstName);
                Log.Info("@LastName" + userEntity.LastName);
                Log.Info("@UserName" + userEntity.UserName);
                Log.Info("@Password" + userEntity.Password);
                Log.Info("@ProfilePic" + userEntity.ProfilePic);
                Log.Info("Store Proc Name : USP_CT_UpdateUser");
                Log.Info("----Info UpdateDealer method end----");
                DynamicParameters param = new DynamicParameters();
                param.Add("@FirstName", userEntity.FirstName);
                param.Add("@LastName", userEntity.LastName);
                param.Add("@UserID", userEntity.ID);
                param.Add("@RoleID", userEntity.RoleID);
                param.Add("@UserName", userEntity.UserName);
                param.Add("@Password", userEntity.Password);
                param.Add("@ProfilePic", userEntity.ProfilePic);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.GenericErrorInfo = GetSingleItem<GenericErrorInfo>(CommandType.StoredProcedure, UserLiterals.UpdateUser, param);
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                Log.Info("----Info UpdateDealer method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in UpdateDealer Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }

        public BaseEntity DeleteDealerByID(UserEntity userEntity)
        {
            BaseEntity entity = new BaseEntity();
            try
            {
                Log.Info("----Info DeleteDealerByID method start----");
                Log.Info("@UserID" + userEntity.ID);
                Log.Info("@RoleID" + userEntity.RoleID);
                Log.Info("Store Proc Name : USP_CT_DeleteUserByID");
                Log.Info("----Info DeleteDealerByID method end----");
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", userEntity.ID);
                param.Add("@RoleID", userEntity.RoleID);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.GenericErrorInfo = GetSingleItem<GenericErrorInfo>(CommandType.StoredProcedure, UserLiterals.DeleteUserByID, param);
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                Log.Info("----Info DeleteDealerByID method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in DeleteDealerByID Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }

        public BaseUserEntity GetDealerByID(UserEntity userEntity)
        {
            BaseUserEntity entity = new BaseUserEntity();
            try
            {
                Log.Info("----Info GetUserByID method start----");
                Log.Info("@UserID" + userEntity.ID);
                Log.Info("@RoleID" + userEntity.RoleID);
                Log.Info("Store Proc Name : USP_CT_GetUserByID");
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", userEntity.ID);
                param.Add("@RoleID", userEntity.RoleID);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.UserEntity = GetSingleItem<UserEntity>(CommandType.StoredProcedure, UserLiterals.GetUserByID, param);
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                Log.Info("----Info GetUserByID method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in GetUserByID Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }

        public BaseUserEntity GetDealers(UserEntity userEntity)
        {
            BaseUserEntity entity = new BaseUserEntity();
            try
            {
                Log.Info("----Info GetUsers method start----");
                Log.Info("@UserID" + userEntity.ID);
                Log.Info("@RoleID" + userEntity.RoleID);
                Log.Info("Store Proc Name : USP_CT_GetUsers");
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", userEntity.ID);
                param.Add("@RoleID", userEntity.RoleID);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.ListUsers = GetItems<UserEntity>(CommandType.StoredProcedure, UserLiterals.GetUsers, param).AsList<UserEntity>();
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                Log.Info("----Info GetUsers method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in GetUsers Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }
    }
}