CREATE TABLE [dbo].[CT_TRAN_TechnicalDetails] (
    [ID]                       BIGINT         IDENTITY (1, 1) NOT NULL,
    [Engine]                   NVARCHAR (500) NULL,
    [Body]                     NVARCHAR (500) NULL,
    [SuspensionSteeringSystem] NVARCHAR (500) NULL,
    [Transmission]             NVARCHAR (500) NULL,
    [Electrical]               NVARCHAR (500) NULL,
    [AirCondition]             NVARCHAR (500) NULL,
    [Brakes]                   NVARCHAR (500) NULL,
    [TyresCondition]           NVARCHAR (500) NULL,
    [OtherInformation]         NVARCHAR (500) NULL,
    [VehicleID]                BIGINT         NULL,
    [CreatedOn]                DATETIME       NULL,
    [CreatedBy]                BIGINT         NULL,
    [ModifiedOn]               DATETIME       NULL,
    [ModifiedBy]               BIGINT         NULL,
    CONSTRAINT [PK_CT_TRAN_TechnicalDetails] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CT_TRAN_TechnicalDetails_CT_TRAN_Vehicle] FOREIGN KEY ([VehicleID]) REFERENCES [dbo].[CT_TRAN_Vehicle] ([ID])
);

