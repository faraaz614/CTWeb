CREATE TABLE [dbo].[CT_TRAN_User] (
    [ID]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [RoleID]     INT            NULL,
    [ProfilePic] NVARCHAR (500) NULL,
    [FirstName]  NVARCHAR (150) NULL,
    [LastName]   NVARCHAR (150) NULL,
    [UserName]   NVARCHAR (150) NULL,
    [Password]   NVARCHAR (50)  NULL,
    [Mobile]     VARCHAR (50)   NULL,
    [Mobile2]    VARCHAR (50)   NULL,
    [IsActive]   BIT            NULL,
    [CreatedBy]  BIGINT         NULL,
    [CreatedOn]  DATETIME       NULL,
    [ModifiedBy] BIGINT         NULL,
    [ModifiedOn] DATETIME       NULL,
    CONSTRAINT [PK_CT_TRAN_User] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CT_TRAN_User_CT_SYS_Role] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[CT_SYS_Role] ([ID]),
    CONSTRAINT [IX_CT_TRAN_User] UNIQUE NONCLUSTERED ([ID] ASC)
);





