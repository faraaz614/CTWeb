﻿using CT.Common.Common;
using CT.Common.Entities;
using CT.Common.Literals;
using CT.Service.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

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
                param.Add("@UserID", userEntity.UserID);
                param.Add("@RoleID", userEntity.RoleID);
                param.Add("@UserName", userEntity.UserName);
                param.Add("@Password", userEntity.Password);
                param.Add("@Mobile", userEntity.Mobile);
                param.Add("@Mobile2", userEntity.Mobile2);
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
                Log.Info("@IsActive" + userEntity.IsActive);
                Log.Info("@ProfilePic" + userEntity.ProfilePic);
                Log.Info("Store Proc Name : USP_CT_UpdateUser");
                Log.Info("----Info UpdateDealer method end----");
                DynamicParameters param = new DynamicParameters();
                param.Add("@FirstName", userEntity.FirstName);
                param.Add("@LastName", userEntity.LastName);
                param.Add("@ID", userEntity.ID);
                param.Add("@UserID", userEntity.UserID);
                param.Add("@RoleID", userEntity.RoleID);
                param.Add("@UserName", userEntity.UserName);
                param.Add("@Password", userEntity.Password);
                param.Add("@Mobile", userEntity.Mobile);
                param.Add("@Mobile2", userEntity.Mobile2);
                param.Add("@IsActive", userEntity.IsActive);
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
                param.Add("@ID", userEntity.ID);
                param.Add("@UserID", userEntity.UserID);
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
                int skip = 0;
                int take = 0;
                skip = (userEntity.PageNo - 1) * userEntity.PageSize;
                take = userEntity.PageSize;
                Log.Info("----Info GetUsers method start----");
                Log.Info("@UserID" + userEntity.UserID);
                Log.Info("@RoleID" + userEntity.RoleID);
                Log.Info("Store Proc Name : USP_CT_GetUsers");
                DynamicParameters param = new DynamicParameters();
                param.Add("@Skip", skip);
                param.Add("@Take", take);
                param.Add("@UserID", userEntity.UserID);
                param.Add("@RoleID", userEntity.RoleID);
                param.Add("@SearchText", userEntity.SearchText);
                param.Add("@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.ListUsers = GetItems<UserEntity>(CommandType.StoredProcedure, UserLiterals.GetUsers, param).AsList<UserEntity>();
                entity.PageTotal = param.Get<dynamic>("@Total");
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                entity.SearchText = userEntity.SearchText;
                entity.PageNo = userEntity.PageNo;
                entity.PageSize = userEntity.PageSize;
                entity.Action = userEntity.Action;
                entity.Controller = userEntity.Controller;
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

        public BaseVehicleBIDEntity GetBIDS(UserEntity userEntity)
        {
            BaseVehicleBIDEntity entity = new BaseVehicleBIDEntity();
            try
            {
                Log.Info("----Info GetBIDS method start----");
                Log.Info("@UserID" + userEntity.UserID);
                Log.Info("@RoleID" + userEntity.RoleID);
                Log.Info("Store Proc Name : USP_CT_GetBids");
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", userEntity.UserID);
                param.Add("@RoleID", userEntity.RoleID);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.ListBids = GetItems<VehicleBIDEntity>(CommandType.StoredProcedure, UserLiterals.GetBids, param).AsList<VehicleBIDEntity>();
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                Log.Info("----Info GetBIDS method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in GetBIDS Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }

        public BaseVehicleBIDEntity ViewBID(VehicleBIDEntity vehicleEntity)
        {
            BaseVehicleBIDEntity entity = new BaseVehicleBIDEntity();
            try
            {
                Log.Info("----Info ViewBID method start----");
                Log.Info("@UserID" + vehicleEntity.UserID);
                Log.Info("@RoleID" + vehicleEntity.RoleID);
                Log.Info("@VehicleID" + vehicleEntity.VehicleID);
                Log.Info("Store Proc Name : USP_CT_ViewBID");
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", vehicleEntity.UserID);
                param.Add("@RoleID", vehicleEntity.RoleID);
                param.Add("@VehicleID", vehicleEntity.VehicleID);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                List<object> bid = GetMultipleList(CommandType.StoredProcedure, UserLiterals.ViewBid, param,
                    x => x.Read<VehicleEntity>().FirstOrDefault(),
                    x => x.Read<VehicleDetailEntity>().FirstOrDefault(),
                    x => x.Read<VehicleImageEntity>().ToList(),
                    x => x.Read<DocumentDetailEntity>().FirstOrDefault(),
                    x => x.Read<TechnicalDetailEntity>().FirstOrDefault(),
                    x => x.Read<VehicleBIDEntity>().ToList());
                entity.VehicleEntity = (VehicleEntity)bid[0];
                if (entity.VehicleEntity != null)
                {
                    entity.VehicleEntity.VehicleDetail = (VehicleDetailEntity)bid[1];
                    entity.VehicleEntity.VehicleImage = (List<VehicleImageEntity>)bid[2];
                    entity.VehicleEntity.DocumentDetail = (DocumentDetailEntity)bid[3];
                    entity.VehicleEntity.TechnicalDetail = (TechnicalDetailEntity)bid[4];
                    entity.ListBids = (List<VehicleBIDEntity>)bid[5];
                }
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                Log.Info("----Info ViewBID method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in ViewBID Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }

        public BaseVehicleEntity GetBIDSByUserID(UserEntity userEntity)
        {
            BaseVehicleEntity entity = new BaseVehicleEntity();
            try
            {
                int skip = 0;
                int take = 0;
                skip = (userEntity.PageNo - 1) * userEntity.PageSize;
                take = userEntity.PageSize;
                Log.Info("----Info GetBIDSByUserID method start----");
                Log.Info("@UserID" + userEntity.UserID);
                Log.Info("@RoleID" + userEntity.RoleID);
                Log.Info("Store Proc Name : USP_CT_GetBIDSByUserID");
                DynamicParameters param = new DynamicParameters();
                param.Add("@Skip", skip);
                param.Add("@Take", take);
                param.Add("@UserID", userEntity.UserID);
                param.Add("@RoleID", userEntity.RoleID);
                param.Add("@SearchText", userEntity.SearchText);
                param.Add("@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.ListVehicles = GetItems<VehicleEntity>(CommandType.StoredProcedure, UserLiterals.GetBIDSByUserID, param).ToList();
                entity.PageTotal = param.Get<dynamic>("@Total");
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                entity.SearchText = userEntity.SearchText;
                entity.PageNo = userEntity.PageNo;
                entity.PageSize = userEntity.PageSize;
                entity.Action = userEntity.Action;
                entity.Controller = userEntity.Controller;
                Log.Info("----Info GetBIDSByUserID method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in GetBIDSByUserID Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }

        public BaseVehicleBIDEntity SaveBIDByUserID(VehicleBIDEntity vehicleEntity)
        {
            BaseVehicleBIDEntity entity = new BaseVehicleBIDEntity();
            try
            {
                Log.Info("----Info SaveBIDByUserID method start----");
                Log.Info("@UserID" + vehicleEntity.UserID);
                Log.Info("@RoleID" + vehicleEntity.RoleID);
                Log.Info("@VehicleID" + vehicleEntity.VehicleID);
                Log.Info("@BIDAmount" + vehicleEntity.BIDAmount);
                Log.Info("Store Proc Name : USP_CT_SaveBIDByUserID");
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", vehicleEntity.UserID);
                param.Add("@RoleID", vehicleEntity.RoleID);
                param.Add("@VehicleID", vehicleEntity.VehicleID);
                param.Add("@BIDAmount", vehicleEntity.BIDAmount);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.VehicleEntity = GetSingleItem<VehicleEntity>(CommandType.StoredProcedure, UserLiterals.SaveBIDByUserID, param);
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                Log.Info("----Info SaveBIDByUserID method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in SaveBIDByUserID Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }

        public BaseUserEntity CloseBID(long BidID)
        {
            BaseUserEntity entity = new BaseUserEntity();
            try
            {
                Log.Info("----Info CloseBID method start----");
                Log.Info("@BidID" + BidID);
                Log.Info("Store Proc Name : USP_CT_CloseBID");
                DynamicParameters param = new DynamicParameters();
                param.Add("@BidID", BidID);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.ListUsers = GetItems<UserEntity>(CommandType.StoredProcedure, UserLiterals.CloseBID, param).ToList();
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                Log.Info("----Info CloseBID method Exit----");
                if (entity.ResponseStatus.Status == 1 && entity.ListUsers.Count > 0)
                {
                    foreach (var user in entity.ListUsers)
                    {
                        try
                        {
                            if (!String.IsNullOrWhiteSpace(user.token))
                            {
                                int result = new FBNotification().SendDealClosedNotification(user.token, string.Empty, user.IsActive);
                                if (result == 1)
                                {
                                    NotificationEntity notificationEntity = new NotificationEntity
                                    {
                                        UserID = 1,
                                        RoleID = 1,
                                        VehicleID = user.VehicleID,
                                        NotificationTo = user.ID,
                                        Body = user.IsActive ? ConfigurationManager.AppSettings["DealClosedWon"] : ConfigurationManager.AppSettings["DealClosedLost"],
                                        Title = ConfigurationManager.AppSettings["DealTitle"]
                                    };
                                    BaseEntity baseEntity = new VehicleService.VehicleService().AddNotification(notificationEntity);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in CloseBID Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }

        public BaseVehicleBIDEntity SaveRegistration(VehicleBIDEntity vehicleEntity)
        {
            BaseVehicleBIDEntity entity = new BaseVehicleBIDEntity();
            try
            {
                Log.Info("----Info SaveRegistration method start----");
                Log.Info("@UserID" + vehicleEntity.UserID);
                Log.Info("@RoleID" + vehicleEntity.RoleID);
                Log.Info("Store Proc Name : USP_CT_SaveBIDByUserID");
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", vehicleEntity.UserID);
                param.Add("@RoleID", vehicleEntity.RoleID);
                param.Add("@refreshedToken", vehicleEntity.refreshedToken);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.GenericErrorInfo = GetSingleItem<GenericErrorInfo>(CommandType.StoredProcedure, UserLiterals.SaveRegistration, param);
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                Log.Info("----Info SaveRegistration method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in SaveRegistration Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }

        public BaseVehicleBIDEntity DeActivateVehicleByID(VehicleBIDEntity vehicleEntity)
        {
            BaseVehicleBIDEntity entity = new BaseVehicleBIDEntity();
            try
            {
                Log.Info("----Info DeActivateVehicleByID method start----");
                Log.Info("@UserID" + vehicleEntity.UserID);
                Log.Info("@RoleID" + vehicleEntity.RoleID);
                Log.Info("@VehicleID" + vehicleEntity.VehicleID);
                Log.Info("@BIDAmount" + vehicleEntity.BIDAmount);
                Log.Info("Store Proc Name : USP_CT_DeActive_Delete_Close_Vehicle");
                Log.Info("Store Proc Name : USP_CT_CloseBID");
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", vehicleEntity.UserID);
                param.Add("@RoleID", vehicleEntity.RoleID);
                param.Add("@VehicleID", vehicleEntity.VehicleID);
                param.Add("@Action", vehicleEntity.VechileStatus);
                param.Add("@BidID", vehicleEntity.BidID);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.GenericErrorInfo = GetSingleItem<GenericErrorInfo>(CommandType.StoredProcedure, UserLiterals.Deactive_Delete_Close_Vehicle, param);
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                Log.Info("----Info DeActivateVehicleByID method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in DeActivateVehicleByID Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }

        public DashEntity DashBoard(UserEntity userEntity)
        {
            DashEntity entity = new DashEntity();
            try
            {
                Log.Info("----Info DashBoard method start----");
                Log.Info("@UserID" + userEntity.UserID);
                Log.Info("@RoleID" + userEntity.RoleID);
                Log.Info("Store Proc Name : USP_CT_DashBoard");
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", userEntity.UserID);
                param.Add("@RoleID", userEntity.RoleID);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.dashEntities = GetItems<DashEntity>(CommandType.StoredProcedure, UserLiterals.DashBoard, param).ToList();
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                Log.Info("----Info DashBoard method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in DashBoard Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }

        public DashEntity DashDeals(UserEntity userEntity)
        {
            DashEntity entity = new DashEntity();
            try
            {
                Log.Info("----Info DashBoard method start----");
                Log.Info("@UserID" + userEntity.UserID);
                Log.Info("@RoleID" + userEntity.RoleID);
                Log.Info("Store Proc Name : USP_CT_DashBoard");
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", userEntity.UserID);
                param.Add("@RoleID", userEntity.RoleID);
                param.Add("@StartDate", userEntity.startdate);
                param.Add("@EndDate", userEntity.enddate);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.dashEntities = GetItems<DashEntity>(CommandType.StoredProcedure, UserLiterals.DashDeals, param).ToList();
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                Log.Info("----Info DashBoard method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in DashBoard Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }
    }
}
