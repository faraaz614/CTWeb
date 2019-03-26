CREATE TABLE [dbo].[CT_TRAN_UserToken] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [Token]      VARCHAR (MAX) NULL,
    [UserID]     INT           NULL,
    [CreatedOn]  DATETIME      CONSTRAINT [DF_CT_TRAN_UserToken_CreatedOn] DEFAULT (getdate()) NULL,
    [ModifiedOn] DATETIME      CONSTRAINT [DF_CT_TRAN_UserToken_UpdatedOn] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_CT_TRAN_UserToken] PRIMARY KEY CLUSTERED ([ID] ASC)
);

