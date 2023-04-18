USE VCAS_DB

/* **********************************************************************

 FOR VCAS_debitAccounts

********************************************************************** */
ALTER TABLE VCAS_debitAccounts ADD starting_balance nvarchar(max)  NULL;
ALTER TABLE VCAS_debitAccounts ADD type nvarchar(max)  NULL;
ALTER TABLE VCAS_debitAccounts ADD active nvarchar(max)  NULL;

/* **********************************************************************

 FOR VCAS_reconcile

********************************************************************** */
CREATE TABLE [dbo].[VCAS_reconcile] ( -- Creating table 'VCAS_reconcile'
    [Id] int IDENTITY(1,1) NOT NULL,
    [FK_debitAccountsId] int  NOT NULL,
    [bank_ending_amt] float  NULL,
    [bank_ending_date] datetime  NULL,
    [service_charge_amt] float  NULL,
    [service_charge_date] nvarchar(max) NULL,
    [interest_earned_amt] float  NULL,
    [interest_earned_date] nvarchar(max) NULL,
    [difference] float  NULL,
    [reconcile] nvarchar(max)  NULL,
);
GO
ALTER TABLE [dbo].[VCAS_reconcile] -- Creating primary key on [Id] in table 'VCAS_reconcile'
ADD CONSTRAINT [PK_VCAS_reconcile]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO
ALTER TABLE [dbo].[VCAS_reconcile] -- Creating foreign key on [FK_debitAccountsId] in table 'VCAS_reconcile'
ADD CONSTRAINT [FK_VCAS_debitAccountsVCAS_reconcile]
    FOREIGN KEY ([FK_debitAccountsId])
    REFERENCES [dbo].[VCAS_debitAccounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
CREATE INDEX [IX_FK_VCAS_debitAccountsVCAS_reconcile] -- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_debitAccountsVCAS_reconcile'
ON [dbo].[VCAS_reconcile]
    ([FK_debitAccountsId]);
GO
SET ANSI_NULLS ON -- Create Stored Procedure
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Austin Lazarus
-- Create date: 23rd Mar, 2023
-- Description:	Reconciliation {/Do} List
-- =============================================
CREATE PROCEDURE usp_SelectRecon
	-- Add the parameters for the stored procedure here
	@dAccount int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
	service_charge_date as datetime,
	service_charge_amt + interest_earned_amt as amount,
	'Service Charge + Interest' as memo,
	'Bank related fees' as reference
	FROM VCAS_DB.dbo.VCAS_reconcile
	Where FK_debitAccountsId = @dAccount

	UNION

	SELECT 
	datetime,
	amount,
	receiverName as memo,
	b.name as reference
	FROM 
	VCAS_issuedChecks a
	INNER JOIN VCAS_expenses b on b.Id = a.FK_expensesId
	where 
	complete = 1
	and FK_debitAccountsId = @dAccount 
END
GO

/* **********************************************************************

 FOR VCAS_undepositedFunds

********************************************************************** */
ALTER TABLE VCAS_undepositedFunds ADD recieved_amount float null;
ALTER TABLE VCAS_undepositedFunds ADD checkNo int null;
ALTER TABLE VCAS_undepositedFunds ADD comment nvarchar(max) null;
ALTER TABLE VCAS_undepositedFunds ADD receiptNo nvarchar(max) null;
ALTER TABLE VCAS_undepositedFunds ADD FK_paymentType int null;
ALTER TABLE VCAS_undepositedFunds ADD FK_bankDetails int null;
ALTER TABLE VCAS_undepositedFunds ADD deposited bit NULL;
ALTER TABLE VCAS_undepositedFunds ADD depositedID int NULL;
SET ANSI_NULLS ON  -- Create Trigger
GO
SET QUOTED_IDENTIFIER ON
GO
-- -------------------------------
-- Triggers when a new INSERT is
-- executed from table capture_payments
-- and writes to VCAS_undepositedFunds with
-- corresponding rows
--
-- by @_ra_lazarus - 04.05.2021
-- ------------------------------
ALTER TRIGGER [dbo].[trg_ins_undepositedFunds]  
ON [dbo].[VCAS_capture_payments]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.VCAS_undepositedFunds (
		amount,
		recieved_amount,
		checkNo,
		comment,
		receiptNo,
		datetime,
		FK_paymentType,
		FK_bankDetails,
		FK_location
	) 
	SELECT 
		amount,
		recieved_amount,
		checkNo,
		comment,
		receiptNo,
		datetime,
		FK_paymentType,
		FK_bankDetails,
		FK_location
	FROM inserted;
END

SET ANSI_NULLS ON -- Create Trigger
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[trg_upd_undepositedFunds]  
ON [dbo].[VCAS_capture_payments]
FOR UPDATE
AS
BEGIN
    SET NOCOUNT ON;
	UPDATE dbo.VCAS_undepositedFunds 
	SET dbo.VCAS_undepositedFunds.recieved_amount = inserted.recieved_amount
	FROM inserted
	WHERE dbo.VCAS_undepositedFunds.receiptNo = inserted.receiptNo
END

SET ANSI_NULLS ON  -- Create Stored Procedure
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Austin Lazarus
-- Create date: Mar 31st, 2023
-- Description:	Upate Undeposited Funds Status from Deposited
-- =============================================
CREATE PROCEDURE usp_UpdateUndepositedFundsStatus
	-- Add the parameters for the stored procedure here
	@loc int,
	@dID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE VCAS_undepositedFunds
	SET deposited = 1, depositedID = @dID
	WHERE FK_location = @loc AND deposited = 0
END
GO
/* **********************************************************************

 FOR VCAS_deposits

********************************************************************** */
-- Creating table 'VCAS_deposit'
CREATE TABLE [dbo].[VCAS_deposit] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [endind_date] nvarchar(max)  NOT NULL,
    [cash_amount] nvarchar(max)  NULL,
    [check_amount] nvarchar(max)  NULL,
    [visa_debit_amount] nvarchar(max)  NULL,
    [visa_credit_amount] nvarchar(max)  NULL,
    [bt_amount] nvarchar(max)  NULL,
    [FK_councilId] int  NOT NULL,
	[FK_debitAccount] int null;	
);
GO
-- Creating primary key on [Id] in table 'VCAS_deposit'
ALTER TABLE [dbo].[VCAS_deposit]
ADD CONSTRAINT [PK_VCAS_deposit]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO
-- Creating foreign key on [FK_councilId] in table 'VCAS_deposit'
ALTER TABLE [dbo].[VCAS_deposit]
ADD CONSTRAINT [FK_VCAS_councilVCAS_deposit]
    FOREIGN KEY ([FK_councilId])
    REFERENCES [dbo].[VCAS_council]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_councilVCAS_deposit'
CREATE INDEX [IX_FK_VCAS_councilVCAS_deposit]
ON [dbo].[VCAS_deposit]
    ([FK_councilId]);
GO
GO

SET ANSI_NULLS ON -- Create Stored Procedure
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Austin Lazarus
-- Create date: 29th Mar, 2023
-- Description:	Fetch Undeposited Funds to Insert into Deposits
-- =============================================
CREATE PROCEDURE [dbo].[usp_InsertDeposit]
	-- Add the parameters for the stored procedure here
	@location int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @END_DATE datetime;
	DECLARE @CASH_AMT Float;
	DECLARE @CHECK_AMT Float;
	DECLARE @VISAD_AMT Float;
	DECLARE @VISAC_AMT Float;
	DECLARE @BT_AMT Float;
	DECLARE @LOC int

	SELECT @END_DATE = (SELECT TOP(1) datetime FROM VCAS_undepositedFunds WHERE FK_location = @location AND deposited = 0 ORDER BY datetime DESC)
	SELECT @CASH_AMT = (SELECT SUM(recieved_amount) AS amount FROM VCAS_undepositedFunds WHERE FK_paymentType = 1 AND FK_location = @location AND deposited = 0)
	SELECT @CHECK_AMT = (SELECT SUM(recieved_amount) AS amount FROM VCAS_undepositedFunds WHERE FK_paymentType = 2 AND FK_location = @location AND deposited = 0)
	SELECT @VISAD_AMT = (SELECT SUM(recieved_amount) AS amount FROM VCAS_undepositedFunds WHERE FK_paymentType = 3 AND FK_location = @location AND deposited = 0)
	SELECT @VISAC_AMT = (SELECT SUM(recieved_amount) AS amount FROM VCAS_undepositedFunds WHERE FK_paymentType = 4 AND FK_location = @location AND deposited = 0)
	SELECT @BT_AMT = (SELECT SUM(recieved_amount) AS amount FROM VCAS_undepositedFunds WHERE FK_paymentType = 5 AND FK_location = @location AND deposited = 0)
	SELECT @LOC = (SELECT TOP(1) FK_location FROM VCAS_undepositedFunds WHERE FK_location = 1)

	SELECT 
	CASE WHEN @END_DATE IS NOT NULL THEN @END_DATE ELSE GETDATE() END as date_ending, 
	CASE WHEN @CASH_AMT IS NOT NULL THEN @CASH_AMT ELSE 0 END as cash_amount, 
	CASE WHEN @CHECK_AMT IS NOT NULL THEN @CHECK_AMT ELSE 0 END as check_amount, 
	CASE WHEN @BT_AMT IS NOT NULL THEN @BT_AMT ELSE 0 END as bt_amount, 
	CASE WHEN @VISAD_AMT IS NOT NULL THEN @VISAD_AMT ELSE 0 END as visa_debit_amount, 
	CASE WHEN @VISAC_AMT IS NOT NULL THEN @VISAC_AMT ELSE 0 END as visa_credit_amount, 
	@LOC as location 
END

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON -- Create Stored Procedure
GO
-- =============================================
-- Author:		Austin Lazarus
-- Create date: Mar 31st, 2023
-- Description:	Update Account's amount from Deposit
-- =============================================
CREATE PROCEDURE usp_UpdateAcctAmt 
	-- Add the parameters for the stored procedure here
	@amt float,
	@id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE VCAS_debitAccounts
	SET amount = @amt
	WHERE Id = @id
END
GO

SET ANSI_NULLS ON  -- Create Stored Procedure
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Austin Lazarus
-- Create date: Mar 30th, 2023
-- Description:	List Deposit records from Undeposit Funds
-- =============================================
CREATE PROCEDURE [dbo].[usp_SelectDeposit]
	-- Add the parameters for the stored procedure here
	@loc int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
	a.receiptNo
	,b.name as payType
	,a.recieved_amount as amount
	,CASE WHEN a.checkNo IS NULL THEN 0 ELSE a.checkNo END as checkNo 
	,CASE WHEN c.name IS NULL THEN '...' ELSE c.name END as bankType 
	FROM VCAS_undepositedFunds a
	INNER JOIN VCAS_REF_payment_type b on a.FK_paymentType = b.Id
	LEFT JOIN VCAS_REF_bank_details c on a.FK_bankDetails = c.Id
	WHERE a.FK_location = @loc AND a.deposited = 0
	GROUP BY b.name, a.receiptNo, a.recieved_amount, a.checkNo, c.name
END
