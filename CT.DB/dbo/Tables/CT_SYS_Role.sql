CREATE TABLE [dbo].[CT_SYS_Role] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Role]        NVARCHAR (150) NULL,
    [Description] NVARCHAR (500) NULL,
    [IsActive]    BIT            NULL,
    CONSTRAINT [PK_CT_SYS_Role] PRIMARY KEY CLUSTERED ([ID] ASC)
);

