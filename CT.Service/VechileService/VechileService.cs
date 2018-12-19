﻿using CT.Common.Common;
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
                Log.Info("Store Proc Name : USP_CT_DeleteVehicleByID");
                Log.Info("----Info DeleteVehicleByID method end----");
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", VehicleEntity.UserID);
                param.Add("@RoleID", VehicleEntity.RoleID);
                param.Add("@VehicleID", VehicleEntity.ID);
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
                if (VehicleEntity.ID > 0)
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
                    var vehcile = GetMultipleList(CommandType.StoredProcedure, VehicleLiterals.GetVehicleByID, param,
                        x => x.Read<VehicleEntity>().FirstOrDefault(),
                        x => x.Read<VehicleDetailEntity>().FirstOrDefault(),
                        x => x.Read<VehicleImageEntity>().FirstOrDefault(),
                        x => x.Read<DocumentDetailEntity>().FirstOrDefault(),
                        x => x.Read<TechnicalDetailEntity>().FirstOrDefault());
                    entity.VehicleEntity = (VehicleEntity)vehcile[0];
                    if (entity.VehicleEntity != null)
                    {
                        entity.VehicleEntity.VehicleDetail = (VehicleDetailEntity)vehcile[1];
                        entity.VehicleEntity.VehicleImage = (VehicleImageEntity)vehcile[2];
                        entity.VehicleEntity.DocumentDetail = (DocumentDetailEntity)vehcile[3];
                        entity.VehicleEntity.TechnicalDetail = (TechnicalDetailEntity)vehcile[4];
                    }
                    entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                    entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                    Log.Info("----Info GetVehicleByID method Exit----");
                }
                DynamicParameters param1 = new DynamicParameters();
                param1.Add("@UserID", VehicleEntity.UserID);
                param1.Add("@RoleID", VehicleEntity.RoleID);
                param1.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param1.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                var list = GetItems<FuelTypeEntity>(CommandType.StoredProcedure, CommonLiterals.GetFuelType, param1).Select(x => new Combo { ID = x.ID, Value = x.Type }).ToList();
                entity.VehicleEntity.VehicleDetail.FuelTypeList = list;
                entity.ResponseStatus.Status = param1.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param1.Get<dynamic>("@Message");
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

        public BaseEntity AddVehicleDetails(VehicleEntity VehicleEntity)
        {
            BaseEntity entity = new BaseEntity();
            try
            {
                Log.Info("----Info AddVehicleDetails method start----");
                Log.Info("@UserID" + VehicleEntity.UserID);
                Log.Info("@RoleID" + VehicleEntity.RoleID);
                Log.Info("@VehicleID" + VehicleEntity.ID);
                Log.Info("@Make" + VehicleEntity.VehicleDetail.Make);
                Log.Info("@Model" + VehicleEntity.VehicleDetail.Model);
                Log.Info("@Variant" + VehicleEntity.VehicleDetail.Variant);
                Log.Info("@YearOfManufacturing" + VehicleEntity.VehicleDetail.YearOfManufacturing);
                Log.Info("@FuelTypeID" + VehicleEntity.VehicleDetail.FuelTypeID);
                Log.Info("@Kilometers" + VehicleEntity.VehicleDetail.Kilometers);
                Log.Info("@Transmission" + VehicleEntity.VehicleDetail.Transmission);
                Log.Info("@RegistrationNo" + VehicleEntity.VehicleDetail.RegistrationNo);
                Log.Info("Store Proc Name : USP_CT_AddVehicleDetails");
                Log.Info("----Info AddVehicleDetails method end----");
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", VehicleEntity.UserID);
                param.Add("@RoleID", VehicleEntity.RoleID);
                param.Add("@VehicleID", VehicleEntity.ID);
                param.Add("@Make", VehicleEntity.VehicleDetail.Make);
                param.Add("@Model", VehicleEntity.VehicleDetail.Model);
                param.Add("@Variant", VehicleEntity.VehicleDetail.Variant);
                param.Add("@YearOfManufacturing", VehicleEntity.VehicleDetail.YearOfManufacturing);
                param.Add("@FuelTypeID", VehicleEntity.VehicleDetail.FuelTypeID);
                param.Add("@Kilometers", VehicleEntity.VehicleDetail.Kilometers);
                param.Add("@Transmission", VehicleEntity.VehicleDetail.Transmission);
                param.Add("@RegistrationNo", VehicleEntity.VehicleDetail.RegistrationNo);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.GenericErrorInfo = GetSingleItem<GenericErrorInfo>(CommandType.StoredProcedure, VehicleLiterals.AddVehicleDetails, param);
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                Log.Info("----Info AddVehicleDetails method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in AddVehicleDetails Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }

        public BaseEntity AddVehicleDocument(VehicleEntity VehicleEntity)
        {
            BaseEntity entity = new BaseEntity();
            try
            {
                Log.Info("----Info AddVehicleDocument method start----");
                Log.Info("@UserID" + VehicleEntity.UserID);
                Log.Info("@RoleID" + VehicleEntity.RoleID);
                Log.Info("@VehicleID" + VehicleEntity.ID);
                Log.Info("@IsRCavailable" + VehicleEntity.DocumentDetail.IsRCavailable);
                Log.Info("@Hypothication" + VehicleEntity.DocumentDetail.Hypothication);
                Log.Info("@IsNOCavailable" + VehicleEntity.DocumentDetail.IsNOCavailable);
                Log.Info("@NoOfOwners" + VehicleEntity.DocumentDetail.NoOfOwners);
                Log.Info("@NoOfKeys" + VehicleEntity.DocumentDetail.NoOfKeys);
                Log.Info("@IsInsuranceAvailable" + VehicleEntity.DocumentDetail.IsInsuranceAvailable);
                Log.Info("@IsComprehensive" + VehicleEntity.DocumentDetail.IsComprehensive);
                Log.Info("@IsThirdParty" + VehicleEntity.DocumentDetail.IsThirdParty);
                Log.Info("@InsuranceExpiryDate" + VehicleEntity.DocumentDetail.InsuranceExpiryDate);
                Log.Info("Store Proc Name : USP_CT_AddVehicleDocument");
                Log.Info("----Info AddVehicleDocument method end----");
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", VehicleEntity.UserID);
                param.Add("@RoleID", VehicleEntity.RoleID);
                param.Add("@VehicleID", VehicleEntity.ID);
                param.Add("@IsRCavailable" , VehicleEntity.DocumentDetail.IsRCavailable);
                param.Add("@Hypothication" , VehicleEntity.DocumentDetail.Hypothication);
                param.Add("@IsNOCavailable" , VehicleEntity.DocumentDetail.IsNOCavailable);
                param.Add("@NoOfOwners" , VehicleEntity.DocumentDetail.NoOfOwners);
                param.Add("@NoOfKeys" , VehicleEntity.DocumentDetail.NoOfKeys);
                param.Add("@IsInsuranceAvailable" , VehicleEntity.DocumentDetail.IsInsuranceAvailable);
                param.Add("@IsComprehensive" , VehicleEntity.DocumentDetail.IsComprehensive);
                param.Add("@IsThirdParty" , VehicleEntity.DocumentDetail.IsThirdParty);
                param.Add("@InsuranceExpiryDate" , VehicleEntity.DocumentDetail.InsuranceExpiryDate);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.GenericErrorInfo = GetSingleItem<GenericErrorInfo>(CommandType.StoredProcedure, VehicleLiterals.AddVehicleDocument, param);
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                Log.Info("----Info AddVehicleDocument method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in AddVehicleDocument Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }

        public BaseEntity AddVehicleTechnical(VehicleEntity VehicleEntity)
        {
            BaseEntity entity = new BaseEntity();
            try
            {
                Log.Info("----Info AddVehicleTechnical method start----");
                Log.Info("@UserID" + VehicleEntity.UserID);
                Log.Info("@RoleID" + VehicleEntity.RoleID);
                Log.Info("@VehicleID" + VehicleEntity.ID);
                Log.Info("@Engine" + VehicleEntity.VehicleDetail.Make);
                Log.Info("@Body" + VehicleEntity.VehicleDetail.Model);
                Log.Info("@SuspensionSteeringSystem" + VehicleEntity.VehicleDetail.Variant);
                Log.Info("@Transmission" + VehicleEntity.VehicleDetail.YearOfManufacturing);
                Log.Info("@Electrical" + VehicleEntity.VehicleDetail.FuelTypeID);
                Log.Info("@AirCondition" + VehicleEntity.VehicleDetail.Kilometers);
                Log.Info("@Brakes" + VehicleEntity.VehicleDetail.Transmission);
                Log.Info("@TyresCondition" + VehicleEntity.VehicleDetail.RegistrationNo);
                Log.Info("@OtherInformation" + VehicleEntity.VehicleDetail.RegistrationNo);
                Log.Info("Store Proc Name : USP_CT_AddVehicleTechnical");
                Log.Info("----Info AddVehicleTechnical method end----");
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", VehicleEntity.UserID);
                param.Add("@RoleID", VehicleEntity.RoleID);
                param.Add("@VehicleID", VehicleEntity.ID);
                param.Add("@Engine" , VehicleEntity.TechnicalDetail.Engine);
                param.Add("@Body" , VehicleEntity.TechnicalDetail.Body);
                param.Add("@SuspensionSteeringSystem" , VehicleEntity.TechnicalDetail.SuspensionSteeringSystem);
                param.Add("@Transmission" , VehicleEntity.TechnicalDetail.Transmission);
                param.Add("@Electrical" , VehicleEntity.TechnicalDetail.Electrical);
                param.Add("@AirCondition" , VehicleEntity.TechnicalDetail.AirCondition);
                param.Add("@Brakes" , VehicleEntity.TechnicalDetail.Brakes);
                param.Add("@TyresCondition" , VehicleEntity.TechnicalDetail.TyresCondition);
                param.Add("@OtherInformation" , VehicleEntity.TechnicalDetail.OtherInformation);
                param.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                entity.GenericErrorInfo = GetSingleItem<GenericErrorInfo>(CommandType.StoredProcedure, VehicleLiterals.AddVehicleTechnical, param);
                entity.ResponseStatus.Status = param.Get<dynamic>("@Status");
                entity.ResponseStatus.Message = param.Get<dynamic>("@Message");
                Log.Info("----Info AddVehicleTechnical method Exit----");
                return entity;
            }
            catch (Exception ex)
            {
                entity.ResponseStatus.Status = 0;
                entity.ResponseStatus.Message = ex.Message;
                Log.Error("Error in AddVehicleTechnical Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return entity;
            }
        }
    }
}