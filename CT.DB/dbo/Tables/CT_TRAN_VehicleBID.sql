CREATE TABLE [dbo].[CT_TRAN_VehicleBID] (
    [ID]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [VehicleID]   BIGINT         NULL,
    [BIDAmount]   DECIMAL (18)   NULL,
    [Description] NVARCHAR (500) NULL,
    [DealerID]    BIGINT         NULL,
    [CreatedOn]   DATETIME       NULL,
    [ModifiedOn]  DATETIME       NULL,
    CONSTRAINT [PK_CT_TRAN_VehicleBID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CT_TRAN_VehicleBID_CT_TRAN_User] FOREIGN KEY ([DealerID]) REFERENCES [dbo].[CT_TRAN_User] ([ID]),
    CONSTRAINT [FK_CT_TRAN_VehicleBID_CT_TRAN_Vehicle] FOREIGN KEY ([VehicleID]) REFERENCES [dbo].[CT_TRAN_Vehicle] ([ID])
);

