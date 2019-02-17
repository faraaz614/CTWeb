CREATE TABLE [dbo].[CT_TRAN_Vehicle] (
    [ID]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [VehicleName]  NVARCHAR (150) NULL,
    [StockID]      NVARCHAR (50)  NULL,
    [Description]  NVARCHAR (500) NULL,
    [BasePrice]    BIGINT         NULL,
    [BidID]        BIGINT         NULL,
    [IsActive]     BIT            NULL,
    [IsDealClosed] BIT            CONSTRAINT [DF_CT_TRAN_Vehicle_IsClosed] DEFAULT ((0)) NULL,
    [IsDelete]     BIT            NULL,
    [CreatedOn]    DATETIME       NULL,
    [CreatedBy]    BIGINT         NULL,
    [ModifiedOn]   DATETIME       NULL,
    [ModifiedBy]   BIGINT         NULL,
    CONSTRAINT [PK_CT_TRAN_Vehicle] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CT_TRAN_Vehicle_CT_TRAN_User] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[CT_TRAN_User] ([ID]),
    CONSTRAINT [FK_CT_TRAN_Vehicle_CT_TRAN_User1] FOREIGN KEY ([ModifiedBy]) REFERENCES [dbo].[CT_TRAN_User] ([ID]),
    CONSTRAINT [FK_CT_TRAN_Vehicle_CT_TRAN_Vehicle1] FOREIGN KEY ([BidID]) REFERENCES [dbo].[CT_TRAN_VehicleBID] ([ID])
);





