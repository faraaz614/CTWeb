CREATE TABLE [dbo].[CT_TRAN_Notification] (
    [ID]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [VehicleID] BIGINT        NULL,
    [Body]      VARCHAR (200) NULL,
    [Title]     VARCHAR (100) NULL,
    [CreatedBy] BIGINT        NULL,
    [CreatedOn] DATETIME      NULL,
    CONSTRAINT [PK_CT_TRAN_Notification] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CT_TRAN_Notification_CT_TRAN_Notification] FOREIGN KEY ([VehicleID]) REFERENCES [dbo].[CT_TRAN_Vehicle] ([ID])
);

