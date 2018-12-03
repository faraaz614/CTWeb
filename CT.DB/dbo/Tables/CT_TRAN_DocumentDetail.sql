CREATE TABLE [dbo].[CT_TRAN_DocumentDetail] (
    [ID]                   BIGINT   IDENTITY (1, 1) NOT NULL,
    [IsRCavailable]        BIT      NULL,
    [Hypothication]        BIT      NULL,
    [IsNOCavailable]       BIT      NULL,
    [NoOfOwners]           INT      NULL,
    [NoOfKeys]             INT      NULL,
    [IsInsuranceAvailable] BIT      NULL,
    [IsComprehensive]      BIT      NULL,
    [IsThirdParty]         BIT      NULL,
    [InsuranceExpiryDate]  DATE     NULL,
    [VehicleID]            BIGINT   NULL,
    [CreatedBy]            BIGINT   NULL,
    [CreatedOn]            DATETIME NULL,
    [ModifiedBy]           BIGINT   NULL,
    [ModifiedOn]           DATETIME NULL,
    CONSTRAINT [PK_CT_TRAN_DocumentDetail] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CT_TRAN_DocumentDetail_CT_TRAN_Vehicle] FOREIGN KEY ([VehicleID]) REFERENCES [dbo].[CT_TRAN_Vehicle] ([ID])
);

