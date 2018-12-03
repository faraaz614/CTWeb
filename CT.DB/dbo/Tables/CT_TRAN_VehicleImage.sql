CREATE TABLE [dbo].[CT_TRAN_VehicleImage] (
    [ID]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [ImageName]  NVARCHAR (150) NULL,
    [VehicleID]  BIGINT         NULL,
    [CreatedOn]  DATETIME       NULL,
    [CreatedBy]  BIGINT         NULL,
    [ModifiedOn] DATETIME       NULL,
    [ModifiedBy] BIGINT         NULL,
    CONSTRAINT [PK_CT_TRAN_VehicleImage] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CT_TRAN_VehicleImage_CT_TRAN_Vehicle] FOREIGN KEY ([VehicleID]) REFERENCES [dbo].[CT_TRAN_Vehicle] ([ID])
);

