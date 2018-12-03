CREATE TABLE [dbo].[CT_TRAN_VehicleDetail] (
    [ID]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [Make]                NVARCHAR (150) NULL,
    [Model]               NVARCHAR (150) NULL,
    [Variant]             NVARCHAR (150) NULL,
    [YearOfManufacturing] DATE           NULL,
    [FuelTypeID]          INT            NULL,
    [VehicleID]           BIGINT         NULL,
    [Kilometers]          INT            NULL,
    [Transmission]        NVARCHAR (150) NULL,
    [RegistrationNo]      NVARCHAR (50)  NULL,
    [CreatedBy]           BIGINT         NULL,
    [CreatedOn]           DATETIME       NULL,
    [ModifiedBy]          BIGINT         NULL,
    [ModifiedOn]          DATETIME       NULL,
    CONSTRAINT [PK_CT_TRAN_VehicleDetail] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CT_TRAN_VehicleDetail_CT_SYS_FuelType] FOREIGN KEY ([FuelTypeID]) REFERENCES [dbo].[CT_SYS_FuelType] ([ID]),
    CONSTRAINT [FK_CT_TRAN_VehicleDetail_CT_TRAN_Vehicle] FOREIGN KEY ([VehicleID]) REFERENCES [dbo].[CT_TRAN_Vehicle] ([ID])
);

