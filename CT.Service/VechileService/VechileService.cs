using CT.Common.Common;
using CT.Common.Entities;
using CT.Common.Literals;
using CT.Service.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Service.VehicleService
{
    public class VehicleService : GenericRepository<VehicleEntity>
    {
        public BaseEntity InsertVehicle(VehicleEntity VehicleEntity)
        {
            BaseEntity entity = new BaseEntity();
            try
            {
                Log.Info("----Info SaveVehicle method start----");
                Log.Info("@VehicleName" + VehicleEntity.VehicleName);
                Log.Info("@Description" + VehicleEntity.Description);
                Log.Info("@IsDealClosed" + VehicleEntity.IsDealClosed);
                Log.Info("Store Proc Name : USP_CT_SaveVehicle");
                Log.Info("----Info SaveVehicle method end----");
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", VehicleEntity.UserID);
                param.Add("@RoleID", VehicleEntity.RoleID);
                param.Add("@VehicleName", VehicleEntity.VehicleName);
                param.Add("@Description", VehicleEntity.Description);
                param.Add("@IsDealClosed", VehicleEntity.IsDealClosed);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.GenericErrorInfo = GetSingleItem<GenericErrorInfo>(CommandType.StoredProcedure, VehicleLiterals.SaveVehicle, param);
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                Log.Info("----Info SaveVehicle method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in InsertVehicle Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }

        public BaseEntity UpdateVehicle(VehicleEntity VehicleEntity)
        {
            BaseEntity entity = new BaseEntity();
            try
            {
                Log.Info("----Info UpdateVehicle method start----");
                Log.Info("@VehicleID" + VehicleEntity.ID);
                Log.Info("@VehicleName" + VehicleEntity.VehicleName);
                Log.Info("@Description" + VehicleEntity.Description);
                Log.Info("@IsDealClosed" + VehicleEntity.IsDealClosed);
                Log.Info("Store Proc Name : USP_CT_UpdateVehicle");
                Log.Info("----Info UpdateVehicle method end----");
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", VehicleEntity.UserID);
                param.Add("@RoleID", VehicleEntity.RoleID);
                param.Add("@VehicleID", VehicleEntity.ID);
                param.Add("@VehicleName", VehicleEntity.VehicleName);
                param.Add("@Description", VehicleEntity.Description);
                param.Add("@IsDealClosed", VehicleEntity.IsDealClosed);
                param.Add("@IsActive", VehicleEntity.IsActive);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.GenericErrorInfo = GetSingleItem<GenericErrorInfo>(CommandType.StoredProcedure, VehicleLiterals.UpdateVehicle, param);
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                Log.Info("----Info UpdateVehicle method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in UpdateVehicle Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }

        public BaseEntity DeleteVehicleByID(VehicleEntity VehicleEntity)
        {
            BaseEntity entity = new BaseEntity();
            try
            {
                Log.Info("----Info DeleteVehicleByID method start----");
                Log.Info("@VehicleID" + VehicleEntity.ID);
                Log.Info("@VehicleName" + VehicleEntity.VehicleName);
                Log.Info("@Description" + VehicleEntity.Description);
                Log.Info("@IsDealClosed" + VehicleEntity.IsDealClosed);
                Log.Info("Store Proc Name : USP_CT_DeleteVehicleByID");
                Log.Info("----Info DeleteVehicleByID method end----");
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", VehicleEntity.UserID);
                param.Add("@RoleID", VehicleEntity.RoleID);
                param.Add("@VehicleID", VehicleEntity.ID);
                param.Add("@VehicleName", VehicleEntity.VehicleName);
                param.Add("@Description", VehicleEntity.Description);
                param.Add("@IsDealClosed", VehicleEntity.IsDealClosed);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.GenericErrorInfo = GetSingleItem<GenericErrorInfo>(CommandType.StoredProcedure, VehicleLiterals.DeleteVehicleByID, param);
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                Log.Info("----Info DeleteVehicleByID method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in DeleteVehicleByID Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }

        public BaseVehicleEntity GetVehicleByID(VehicleEntity VehicleEntity)
        {
            BaseVehicleEntity entity = new BaseVehicleEntity();
            try
            {
                Log.Info("----Info GetVehicleByID method start----");
                Log.Info("@VehicleID" + VehicleEntity.ID);
                Log.Info("Store Proc Name : USP_CT_GetVehicleByID");
                Log.Info("----Info GetVehicleByID method end----");
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", VehicleEntity.UserID);
                param.Add("@RoleID", VehicleEntity.RoleID);
                param.Add("@VehicleID", VehicleEntity.ID);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.VehicleEntity = GetSingleItem<VehicleEntity>(CommandType.StoredProcedure, VehicleLiterals.GetVehicleByID, param);
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                Log.Info("----Info GetVehicleByID method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in GetVehicleByID Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }

        public BaseVehicleEntity GetVehicles(VehicleEntity VehicleEntity)
        {
            BaseVehicleEntity entity = new BaseVehicleEntity();
            try
            {
                Log.Info("----Info GetVehicles method start----");
                Log.Info("Store Proc Name : USP_CT_GetVehicles");
                Log.Info("----Info GetVehicles method end----");
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", VehicleEntity.UserID);
                param.Add("@RoleID", VehicleEntity.RoleID);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.ListVehicles = GetItems<VehicleEntity>(CommandType.StoredProcedure, VehicleLiterals.GetVehicles, param).ToList();
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                Log.Info("----Info GetVehicles method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in GetVehicles Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }
    }
}
