CREATE TABLE [dbo].[CT_SYS_FuelType] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Type]        NVARCHAR (50)  NULL,
    [Description] NVARCHAR (150) NULL,
    [IsActive]    BIT            CONSTRAINT [DF_CT_SYS_FuelType_IsActive] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_CT_SYS_FuelType] PRIMARY KEY CLUSTERED ([ID] ASC)
);

