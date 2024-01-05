
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/22/2023 15:59:46
-- Generated from EDMX file: C:\Users\g-sta\OneDrive\Documents\GitHub\VCAS_v2\VCAS\Models\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [VCAS_DB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_VCAS_capture_paymentsVCAS_creditTrans]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_creditTrans1] DROP CONSTRAINT [FK_VCAS_capture_paymentsVCAS_creditTrans];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_councilVCAS_Captured_Payment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_capture_payments] DROP CONSTRAINT [FK_VCAS_councilVCAS_Captured_Payment];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_councilVCAS_debitAccounts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_debitAccounts] DROP CONSTRAINT [FK_VCAS_councilVCAS_debitAccounts];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_councilVCAS_deposit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_deposit] DROP CONSTRAINT [FK_VCAS_councilVCAS_deposit];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_councilVCAS_district]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_district] DROP CONSTRAINT [FK_VCAS_councilVCAS_district];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_councilVCAS_forms]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_forms] DROP CONSTRAINT [FK_VCAS_councilVCAS_forms];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_councilVCAS_inventory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_inventory] DROP CONSTRAINT [FK_VCAS_councilVCAS_inventory];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_councilVCAS_issuedChecks]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_issuedChecks] DROP CONSTRAINT [FK_VCAS_councilVCAS_issuedChecks];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_councilVCAS_orders]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_orders] DROP CONSTRAINT [FK_VCAS_councilVCAS_orders];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_councilVCAS_REF_expense_location]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_REF_expense_location] DROP CONSTRAINT [FK_VCAS_councilVCAS_REF_expense_location];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_councilVCAS_REF_items_location]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_REF_items_location] DROP CONSTRAINT [FK_VCAS_councilVCAS_REF_items_location];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_councilVCAS_REF_reports_params]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_REF_reports_params] DROP CONSTRAINT [FK_VCAS_councilVCAS_REF_reports_params];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_councilVCAS_undepositedFunds]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_undepositedFunds] DROP CONSTRAINT [FK_VCAS_councilVCAS_undepositedFunds];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_customerVCAS_orders]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_orders] DROP CONSTRAINT [FK_VCAS_customerVCAS_orders];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_debitAccountsVCAS_debitTrans]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_debitTrans1] DROP CONSTRAINT [FK_VCAS_debitAccountsVCAS_debitTrans];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_debitAccountsVCAS_issuedChecks]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_issuedChecks] DROP CONSTRAINT [FK_VCAS_debitAccountsVCAS_issuedChecks];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_debitAccountsVCAS_reconcile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_reconcile] DROP CONSTRAINT [FK_VCAS_debitAccountsVCAS_reconcile];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_expensesVCAS_issuedChecks]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_issuedChecks] DROP CONSTRAINT [FK_VCAS_expensesVCAS_issuedChecks];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_expensesVCAS_REF_expense_location]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_REF_expense_location] DROP CONSTRAINT [FK_VCAS_expensesVCAS_REF_expense_location];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_formsVCAS_REF_forms]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_REF_forms] DROP CONSTRAINT [FK_VCAS_formsVCAS_REF_forms];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_inventoryVCAS_orders]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_orders] DROP CONSTRAINT [FK_VCAS_inventoryVCAS_orders];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_order_statusVCAS_orders]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_orders] DROP CONSTRAINT [FK_VCAS_order_statusVCAS_orders];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_REF_bank_detailsVCAS_Captured_Payment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_capture_payments] DROP CONSTRAINT [FK_VCAS_REF_bank_detailsVCAS_Captured_Payment];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_REF_itemsVCAS_Captured_Payment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_capture_payments] DROP CONSTRAINT [FK_VCAS_REF_itemsVCAS_Captured_Payment];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_REF_itemsVCAS_inventory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_inventory] DROP CONSTRAINT [FK_VCAS_REF_itemsVCAS_inventory];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_REF_itemsVCAS_REF_items_location]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_REF_items_location] DROP CONSTRAINT [FK_VCAS_REF_itemsVCAS_REF_items_location];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_REF_payment_typeVCAS_Captured_Payment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_capture_payments] DROP CONSTRAINT [FK_VCAS_REF_payment_typeVCAS_Captured_Payment];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_REF_payment_typeVCAS_debitAccounts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_debitAccounts] DROP CONSTRAINT [FK_VCAS_REF_payment_typeVCAS_debitAccounts];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_REF_receiverID_TypesVCAS_issuedChecks]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_issuedChecks] DROP CONSTRAINT [FK_VCAS_REF_receiverID_TypesVCAS_issuedChecks];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_REF_reportsVCAS_REF_reports_params]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_REF_reports_params] DROP CONSTRAINT [FK_VCAS_REF_reportsVCAS_REF_reports_params];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_REF_userRolesVCAS_forms]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_forms] DROP CONSTRAINT [FK_VCAS_REF_userRolesVCAS_forms];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_REF_userRolesVCAS_REF_reports]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_reports] DROP CONSTRAINT [FK_VCAS_REF_userRolesVCAS_REF_reports];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_REF_userRolesVCAS_supportDocs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_supportDocs] DROP CONSTRAINT [FK_VCAS_REF_userRolesVCAS_supportDocs];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_REF_userRolesVCAS_users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_users] DROP CONSTRAINT [FK_VCAS_REF_userRolesVCAS_users];
GO
IF OBJECT_ID(N'[dbo].[FK_VCAS_usersVCAS_district]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VCAS_district] DROP CONSTRAINT [FK_VCAS_usersVCAS_district];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[VCAS_capture_payments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_capture_payments];
GO
IF OBJECT_ID(N'[dbo].[VCAS_council]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_council];
GO
IF OBJECT_ID(N'[dbo].[VCAS_creditTrans1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_creditTrans1];
GO
IF OBJECT_ID(N'[dbo].[VCAS_customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_customer];
GO
IF OBJECT_ID(N'[dbo].[VCAS_debitAccounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_debitAccounts];
GO
IF OBJECT_ID(N'[dbo].[VCAS_debitTrans1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_debitTrans1];
GO
IF OBJECT_ID(N'[dbo].[VCAS_deposit]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_deposit];
GO
IF OBJECT_ID(N'[dbo].[VCAS_district]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_district];
GO
IF OBJECT_ID(N'[dbo].[VCAS_expenses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_expenses];
GO
IF OBJECT_ID(N'[dbo].[VCAS_forms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_forms];
GO
IF OBJECT_ID(N'[dbo].[VCAS_inventory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_inventory];
GO
IF OBJECT_ID(N'[dbo].[VCAS_issuedChecks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_issuedChecks];
GO
IF OBJECT_ID(N'[dbo].[VCAS_orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_orders];
GO
IF OBJECT_ID(N'[dbo].[VCAS_reconcile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_reconcile];
GO
IF OBJECT_ID(N'[dbo].[VCAS_REF_bank_details]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_REF_bank_details];
GO
IF OBJECT_ID(N'[dbo].[VCAS_REF_expense_location]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_REF_expense_location];
GO
IF OBJECT_ID(N'[dbo].[VCAS_REF_forms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_REF_forms];
GO
IF OBJECT_ID(N'[dbo].[VCAS_REF_items]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_REF_items];
GO
IF OBJECT_ID(N'[dbo].[VCAS_REF_items_location]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_REF_items_location];
GO
IF OBJECT_ID(N'[dbo].[VCAS_REF_order_status]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_REF_order_status];
GO
IF OBJECT_ID(N'[dbo].[VCAS_REF_payment_type]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_REF_payment_type];
GO
IF OBJECT_ID(N'[dbo].[VCAS_REF_receiverID_Types]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_REF_receiverID_Types];
GO
IF OBJECT_ID(N'[dbo].[VCAS_REF_reports_params]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_REF_reports_params];
GO
IF OBJECT_ID(N'[dbo].[VCAS_REF_userRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_REF_userRoles];
GO
IF OBJECT_ID(N'[dbo].[VCAS_reports]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_reports];
GO
IF OBJECT_ID(N'[dbo].[VCAS_session]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_session];
GO
IF OBJECT_ID(N'[dbo].[VCAS_supportDocs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_supportDocs];
GO
IF OBJECT_ID(N'[dbo].[VCAS_undepositedFunds]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_undepositedFunds];
GO
IF OBJECT_ID(N'[dbo].[VCAS_users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VCAS_users];
GO
IF OBJECT_ID(N'[ModelStoreContainer].[vw_inventoryItems]', 'U') IS NOT NULL
    DROP TABLE [ModelStoreContainer].[vw_inventoryItems];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'VCAS_debitTrans1'
CREATE TABLE [dbo].[VCAS_debitTrans1] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FK_debitAccountsId] int  NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [amount] float  NOT NULL,
    [remittance] nvarchar(max)  NOT NULL,
    [payee] nvarchar(max)  NOT NULL,
    [datetime] datetime  NOT NULL,
    [attach_statement] nvarchar(max)  NULL
);
GO

-- Creating table 'VCAS_creditTrans1'
CREATE TABLE [dbo].[VCAS_creditTrans1] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FK_capture_paymentsId] int  NOT NULL,
    [datetime] datetime  NOT NULL,
    [payer] nvarchar(max)  NOT NULL,
    [amount] float  NOT NULL,
    [receiptNo] nvarchar(max)  NOT NULL,
    [issuer] nvarchar(max)  NOT NULL,
    [voidCheck] bit  NOT NULL
);
GO

-- Creating table 'VCAS_REF_payment_type'
CREATE TABLE [dbo].[VCAS_REF_payment_type] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'VCAS_REF_bank_details'
CREATE TABLE [dbo].[VCAS_REF_bank_details] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'VCAS_REF_receiverID_Types'
CREATE TABLE [dbo].[VCAS_REF_receiverID_Types] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'VCAS_users'
CREATE TABLE [dbo].[VCAS_users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [fullName] nvarchar(max)  NOT NULL,
    [userName] nvarchar(max)  NOT NULL,
    [email] nvarchar(max)  NULL,
    [password] nvarchar(max)  NOT NULL,
    [FK_userRolesId] int  NOT NULL
);
GO

-- Creating table 'VCAS_REF_userRoles'
CREATE TABLE [dbo].[VCAS_REF_userRoles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'VCAS_reports'
CREATE TABLE [dbo].[VCAS_reports] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [desc] nvarchar(max)  NOT NULL,
    [FK_REF_userRolesId] int  NOT NULL,
    [paramCheck] bit  NOT NULL
);
GO

-- Creating table 'VCAS_district'
CREATE TABLE [dbo].[VCAS_district] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [FK_location] int  NOT NULL,
    [FK_usersId] int  NOT NULL
);
GO

-- Creating table 'VCAS_supportDocs'
CREATE TABLE [dbo].[VCAS_supportDocs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NULL,
    [desc] nvarchar(max)  NULL,
    [media] nvarchar(max)  NULL,
    [media_type] nvarchar(max)  NULL,
    [FK_REF_userRolesId] int  NOT NULL
);
GO

-- Creating table 'VCAS_expenses'
CREATE TABLE [dbo].[VCAS_expenses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [desc] nvarchar(max)  NULL
);
GO

-- Creating table 'VCAS_issuedChecks'
CREATE TABLE [dbo].[VCAS_issuedChecks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [datetime] datetime  NULL,
    [amount] float  NOT NULL,
    [checkNo] nvarchar(max)  NULL,
    [issuer] nvarchar(max)  NOT NULL,
    [receiverName] nvarchar(max)  NULL,
    [FK_receiverID_TypesId] int  NOT NULL,
    [receiverID] nvarchar(max)  NULL,
    [signature] nvarchar(max)  NULL,
    [FK_debitAccountsId] int  NOT NULL,
    [FK_location] int  NOT NULL,
    [approver] nvarchar(max)  NULL,
    [approval] int  NULL,
    [pending_approval] int  NULL,
    [auth_signature] nvarchar(max)  NULL,
    [complete] bit  NULL,
    [FK_expensesId] int  NULL,
    [attach_ID] nvarchar(max)  NULL,
    [attach_Doc] nvarchar(max)  NULL,
    [attach_Receipt] nvarchar(max)  NULL,
    [comments] nvarchar(max)  NULL
);
GO

-- Creating table 'VCAS_session'
CREATE TABLE [dbo].[VCAS_session] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [role] int  NOT NULL,
    [location] int  NOT NULL
);
GO

-- Creating table 'VCAS_REF_items_location'
CREATE TABLE [dbo].[VCAS_REF_items_location] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FK_councilId] int  NOT NULL,
    [FK_REF_itemsId] int  NOT NULL
);
GO

-- Creating table 'VCAS_REF_expense_location'
CREATE TABLE [dbo].[VCAS_REF_expense_location] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FK_councilId] int  NOT NULL,
    [FK_expensesId] int  NOT NULL
);
GO

-- Creating table 'VCAS_REF_reports_params'
CREATE TABLE [dbo].[VCAS_REF_reports_params] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [param_key] nvarchar(max)  NOT NULL,
    [param_value] nvarchar(max)  NOT NULL,
    [param_dataType] nvarchar(max)  NOT NULL,
    [FK_REF_reportsId] int  NOT NULL,
    [FK_location] int  NULL
);
GO

-- Creating table 'VCAS_REF_order_status'
CREATE TABLE [dbo].[VCAS_REF_order_status] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NULL,
    [desc] nvarchar(max)  NULL
);
GO

-- Creating table 'VCAS_REF_items'
CREATE TABLE [dbo].[VCAS_REF_items] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [desc] nvarchar(max)  NULL
);
GO

-- Creating table 'VCAS_forms'
CREATE TABLE [dbo].[VCAS_forms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NULL,
    [desc] nvarchar(max)  NULL,
    [form] nvarchar(max)  NULL,
    [dateModified] datetime  NOT NULL,
    [FK_location] int  NOT NULL,
    [FK_REF_userRolesId] int  NOT NULL
);
GO

-- Creating table 'VCAS_council'
CREATE TABLE [dbo].[VCAS_council] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [location] nvarchar(max)  NOT NULL,
    [vendor_Id] nvarchar(max)  NOT NULL,
    [app_name] nvarchar(max)  NULL,
    [receipt_logo] nvarchar(max)  NULL,
    [receipt_footer] nvarchar(max)  NULL,
    [receipt_header] nvarchar(max)  NULL,
    [app_cover] nvarchar(max)  NULL,
    [twilio_SID] nvarchar(max)  NULL,
    [twilio_TOKEN] nvarchar(max)  NULL,
    [twilio_NUMBER] nvarchar(max)  NULL,
    [twilio_XML] nvarchar(max)  NULL
);
GO

-- Creating table 'VCAS_REF_forms'
CREATE TABLE [dbo].[VCAS_REF_forms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [txtInput_01] nvarchar(max)  NULL,
    [txtInput_02] nvarchar(max)  NULL,
    [txtInput_03] nvarchar(max)  NULL,
    [txtInput_04] nvarchar(max)  NULL,
    [txtInput_05] nvarchar(max)  NULL,
    [txtInput_06] nvarchar(max)  NULL,
    [txtInput_07] nvarchar(max)  NULL,
    [txtInput_08] nvarchar(max)  NULL,
    [txtInput_09] nvarchar(max)  NULL,
    [txtInput_10] nvarchar(max)  NULL,
    [txtInput_11] nvarchar(max)  NULL,
    [txtInput_12] nvarchar(max)  NULL,
    [txtInput_13] nvarchar(max)  NULL,
    [checkInput_01] nvarchar(max)  NULL,
    [checkInput_02] nvarchar(max)  NULL,
    [checkInput_03] nvarchar(max)  NULL,
    [selectInput_01] nvarchar(max)  NULL,
    [selectInput_02] nvarchar(max)  NULL,
    [selectInput_03] nvarchar(max)  NULL,
    [txtAreaInput_01] nvarchar(max)  NULL,
    [txtAreaInput_02] nvarchar(max)  NULL,
    [txtAreaInput_03] nvarchar(max)  NULL,
    [fileInput_01] nvarchar(max)  NULL,
    [fileInput_02] nvarchar(max)  NULL,
    [formBtn] nvarchar(max)  NULL,
    [FK_formsId] int  NOT NULL,
    [frmHeader] nvarchar(max)  NULL,
    [frmFooter] nvarchar(max)  NULL,
    [signatureBox] nvarchar(max)  NULL
);
GO

-- Creating table 'VCAS_inventory'
CREATE TABLE [dbo].[VCAS_inventory] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [desc] nvarchar(max)  NULL,
    [dateModified] datetime  NOT NULL,
    [partNumber] nvarchar(max)  NULL,
    [label] nvarchar(max)  NULL,
    [startStock] float  NOT NULL,
    [currentStock] float  NULL,
    [quantity] float  NULL,
    [size] float  NULL,
    [unit] nvarchar(max)  NULL,
    [unitPrice] float  NULL,
    [sellingPrice] float  NULL,
    [image] nvarchar(max)  NULL,
    [FK_REF_itemsId] int  NULL,
    [FK_location] int  NULL
);
GO

-- Creating table 'VCAS_orders'
CREATE TABLE [dbo].[VCAS_orders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [datetime] datetime  NOT NULL,
    [quantity] float  NULL,
    [FK_order_statusId] int  NOT NULL,
    [FK_customerId] int  NOT NULL,
    [FK_inventoryId] int  NOT NULL,
    [FK_location] int  NULL,
    [comment] nvarchar(max)  NULL
);
GO

-- Creating table 'VCAS_capture_payments'
CREATE TABLE [dbo].[VCAS_capture_payments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [datetime] datetime  NOT NULL,
    [payer] nvarchar(max)  NOT NULL,
    [amount] float  NOT NULL,
    [checkNo] int  NULL,
    [comment] nvarchar(max)  NULL,
    [receiptNo] nvarchar(max)  NOT NULL,
    [issuer] nvarchar(max)  NOT NULL,
    [voidCheck] bit  NOT NULL,
    [FK_paymentType] int  NOT NULL,
    [FK_bankDetails] int  NOT NULL,
    [FK_items] int  NOT NULL,
    [FK_location] int  NOT NULL,
    [payerID] int  NULL,
    [orderID] int  NULL,
    [invoice] bit  NULL,
    [recieved_amount] float  NULL
);
GO

-- Creating table 'VCAS_debitAccounts'
CREATE TABLE [dbo].[VCAS_debitAccounts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [acctNum] nvarchar(max)  NULL,
    [amount] float  NULL,
    [FK_payment_Type] int  NOT NULL,
    [remittance] nvarchar(max)  NULL,
    [payee] nvarchar(max)  NULL,
    [datetime] datetime  NOT NULL,
    [attach_statement] nvarchar(max)  NULL,
    [FK_location] int  NOT NULL,
    [starting_balance] nvarchar(max)  NULL,
    [type] nvarchar(max)  NULL,
    [active] nvarchar(max)  NULL
);
GO

-- Creating table 'VCAS_reconcile'
CREATE TABLE [dbo].[VCAS_reconcile] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FK_debitAccountsId] int  NOT NULL,
    [bank_ending_amt] float  NULL,
    [bank_ending_date] datetime  NULL,
    [service_charge_amt] float  NULL,
    [service_charge_date] nvarchar(max)  NULL,
    [interest_earned_amt] float  NULL,
    [interest_earned_date] nvarchar(max)  NULL,
    [difference] float  NULL,
    [reconcile] nvarchar(max)  NULL
);
GO

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
    [FK_debitAccount] int  NULL
);
GO

-- Creating table 'VCAS_undepositedFunds'
CREATE TABLE [dbo].[VCAS_undepositedFunds] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [amount] float  NULL,
    [datetime] datetime  NULL,
    [FK_location] int  NOT NULL,
    [recieved_amount] float  NULL,
    [checkNo] int  NULL,
    [comment] nvarchar(max)  NULL,
    [receiptNo] nvarchar(max)  NULL,
    [FK_paymentType] int  NULL,
    [FK_bankDetails] int  NULL,
    [deposited] bit  NULL,
    [depositedID] int  NULL
);
GO

-- Creating table 'vw_inventoryItems'
CREATE TABLE [dbo].[vw_inventoryItems] (
    [Id] int  NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [FK_councilId] int  NOT NULL
);
GO

-- Creating table 'VCAS_customer'
CREATE TABLE [dbo].[VCAS_customer] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [firstName] nvarchar(max)  NULL,
    [lastName] nvarchar(max)  NULL,
    [address] nvarchar(max)  NULL,
    [state] nvarchar(max)  NULL,
    [phone] nvarchar(max)  NULL,
    [email] nvarchar(max)  NULL,
    [FK_Location] int  NULL
);
GO

-- Creating table 'VCAS_inventory_AUDIT'
CREATE TABLE [dbo].[VCAS_inventory_AUDIT] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NULL,
    [startStock] float  NULL,
    [currentStock] float  NULL,
    [FK_REF_itemsId] int  NULL,
    [datetime] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'VCAS_debitTrans1'
ALTER TABLE [dbo].[VCAS_debitTrans1]
ADD CONSTRAINT [PK_VCAS_debitTrans1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_creditTrans1'
ALTER TABLE [dbo].[VCAS_creditTrans1]
ADD CONSTRAINT [PK_VCAS_creditTrans1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_REF_payment_type'
ALTER TABLE [dbo].[VCAS_REF_payment_type]
ADD CONSTRAINT [PK_VCAS_REF_payment_type]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_REF_bank_details'
ALTER TABLE [dbo].[VCAS_REF_bank_details]
ADD CONSTRAINT [PK_VCAS_REF_bank_details]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_REF_receiverID_Types'
ALTER TABLE [dbo].[VCAS_REF_receiverID_Types]
ADD CONSTRAINT [PK_VCAS_REF_receiverID_Types]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_users'
ALTER TABLE [dbo].[VCAS_users]
ADD CONSTRAINT [PK_VCAS_users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_REF_userRoles'
ALTER TABLE [dbo].[VCAS_REF_userRoles]
ADD CONSTRAINT [PK_VCAS_REF_userRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_reports'
ALTER TABLE [dbo].[VCAS_reports]
ADD CONSTRAINT [PK_VCAS_reports]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_district'
ALTER TABLE [dbo].[VCAS_district]
ADD CONSTRAINT [PK_VCAS_district]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_supportDocs'
ALTER TABLE [dbo].[VCAS_supportDocs]
ADD CONSTRAINT [PK_VCAS_supportDocs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_expenses'
ALTER TABLE [dbo].[VCAS_expenses]
ADD CONSTRAINT [PK_VCAS_expenses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_issuedChecks'
ALTER TABLE [dbo].[VCAS_issuedChecks]
ADD CONSTRAINT [PK_VCAS_issuedChecks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_session'
ALTER TABLE [dbo].[VCAS_session]
ADD CONSTRAINT [PK_VCAS_session]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_REF_items_location'
ALTER TABLE [dbo].[VCAS_REF_items_location]
ADD CONSTRAINT [PK_VCAS_REF_items_location]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_REF_expense_location'
ALTER TABLE [dbo].[VCAS_REF_expense_location]
ADD CONSTRAINT [PK_VCAS_REF_expense_location]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_REF_reports_params'
ALTER TABLE [dbo].[VCAS_REF_reports_params]
ADD CONSTRAINT [PK_VCAS_REF_reports_params]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_REF_order_status'
ALTER TABLE [dbo].[VCAS_REF_order_status]
ADD CONSTRAINT [PK_VCAS_REF_order_status]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_REF_items'
ALTER TABLE [dbo].[VCAS_REF_items]
ADD CONSTRAINT [PK_VCAS_REF_items]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_forms'
ALTER TABLE [dbo].[VCAS_forms]
ADD CONSTRAINT [PK_VCAS_forms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_council'
ALTER TABLE [dbo].[VCAS_council]
ADD CONSTRAINT [PK_VCAS_council]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_REF_forms'
ALTER TABLE [dbo].[VCAS_REF_forms]
ADD CONSTRAINT [PK_VCAS_REF_forms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_inventory'
ALTER TABLE [dbo].[VCAS_inventory]
ADD CONSTRAINT [PK_VCAS_inventory]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_orders'
ALTER TABLE [dbo].[VCAS_orders]
ADD CONSTRAINT [PK_VCAS_orders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_capture_payments'
ALTER TABLE [dbo].[VCAS_capture_payments]
ADD CONSTRAINT [PK_VCAS_capture_payments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_debitAccounts'
ALTER TABLE [dbo].[VCAS_debitAccounts]
ADD CONSTRAINT [PK_VCAS_debitAccounts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_reconcile'
ALTER TABLE [dbo].[VCAS_reconcile]
ADD CONSTRAINT [PK_VCAS_reconcile]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_deposit'
ALTER TABLE [dbo].[VCAS_deposit]
ADD CONSTRAINT [PK_VCAS_deposit]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_undepositedFunds'
ALTER TABLE [dbo].[VCAS_undepositedFunds]
ADD CONSTRAINT [PK_VCAS_undepositedFunds]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [name], [FK_councilId] in table 'vw_inventoryItems'
ALTER TABLE [dbo].[vw_inventoryItems]
ADD CONSTRAINT [PK_vw_inventoryItems]
    PRIMARY KEY CLUSTERED ([Id], [name], [FK_councilId] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_customer'
ALTER TABLE [dbo].[VCAS_customer]
ADD CONSTRAINT [PK_VCAS_customer]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VCAS_inventory_AUDIT'
ALTER TABLE [dbo].[VCAS_inventory_AUDIT]
ADD CONSTRAINT [PK_VCAS_inventory_AUDIT]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [FK_userRolesId] in table 'VCAS_users'
ALTER TABLE [dbo].[VCAS_users]
ADD CONSTRAINT [FK_VCAS_REF_userRolesVCAS_users]
    FOREIGN KEY ([FK_userRolesId])
    REFERENCES [dbo].[VCAS_REF_userRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_REF_userRolesVCAS_users'
CREATE INDEX [IX_FK_VCAS_REF_userRolesVCAS_users]
ON [dbo].[VCAS_users]
    ([FK_userRolesId]);
GO

-- Creating foreign key on [FK_REF_userRolesId] in table 'VCAS_reports'
ALTER TABLE [dbo].[VCAS_reports]
ADD CONSTRAINT [FK_VCAS_REF_userRolesVCAS_REF_reports]
    FOREIGN KEY ([FK_REF_userRolesId])
    REFERENCES [dbo].[VCAS_REF_userRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_REF_userRolesVCAS_REF_reports'
CREATE INDEX [IX_FK_VCAS_REF_userRolesVCAS_REF_reports]
ON [dbo].[VCAS_reports]
    ([FK_REF_userRolesId]);
GO

-- Creating foreign key on [FK_usersId] in table 'VCAS_district'
ALTER TABLE [dbo].[VCAS_district]
ADD CONSTRAINT [FK_VCAS_usersVCAS_district]
    FOREIGN KEY ([FK_usersId])
    REFERENCES [dbo].[VCAS_users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_usersVCAS_district'
CREATE INDEX [IX_FK_VCAS_usersVCAS_district]
ON [dbo].[VCAS_district]
    ([FK_usersId]);
GO

-- Creating foreign key on [FK_REF_userRolesId] in table 'VCAS_supportDocs'
ALTER TABLE [dbo].[VCAS_supportDocs]
ADD CONSTRAINT [FK_VCAS_REF_userRolesVCAS_supportDocs]
    FOREIGN KEY ([FK_REF_userRolesId])
    REFERENCES [dbo].[VCAS_REF_userRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_REF_userRolesVCAS_supportDocs'
CREATE INDEX [IX_FK_VCAS_REF_userRolesVCAS_supportDocs]
ON [dbo].[VCAS_supportDocs]
    ([FK_REF_userRolesId]);
GO

-- Creating foreign key on [FK_expensesId] in table 'VCAS_issuedChecks'
ALTER TABLE [dbo].[VCAS_issuedChecks]
ADD CONSTRAINT [FK_VCAS_expensesVCAS_issuedChecks]
    FOREIGN KEY ([FK_expensesId])
    REFERENCES [dbo].[VCAS_expenses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_expensesVCAS_issuedChecks'
CREATE INDEX [IX_FK_VCAS_expensesVCAS_issuedChecks]
ON [dbo].[VCAS_issuedChecks]
    ([FK_expensesId]);
GO

-- Creating foreign key on [FK_receiverID_TypesId] in table 'VCAS_issuedChecks'
ALTER TABLE [dbo].[VCAS_issuedChecks]
ADD CONSTRAINT [FK_VCAS_REF_receiverID_TypesVCAS_issuedChecks]
    FOREIGN KEY ([FK_receiverID_TypesId])
    REFERENCES [dbo].[VCAS_REF_receiverID_Types]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_REF_receiverID_TypesVCAS_issuedChecks'
CREATE INDEX [IX_FK_VCAS_REF_receiverID_TypesVCAS_issuedChecks]
ON [dbo].[VCAS_issuedChecks]
    ([FK_receiverID_TypesId]);
GO

-- Creating foreign key on [FK_expensesId] in table 'VCAS_REF_expense_location'
ALTER TABLE [dbo].[VCAS_REF_expense_location]
ADD CONSTRAINT [FK_VCAS_expensesVCAS_REF_expense_location]
    FOREIGN KEY ([FK_expensesId])
    REFERENCES [dbo].[VCAS_expenses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_expensesVCAS_REF_expense_location'
CREATE INDEX [IX_FK_VCAS_expensesVCAS_REF_expense_location]
ON [dbo].[VCAS_REF_expense_location]
    ([FK_expensesId]);
GO

-- Creating foreign key on [FK_REF_reportsId] in table 'VCAS_REF_reports_params'
ALTER TABLE [dbo].[VCAS_REF_reports_params]
ADD CONSTRAINT [FK_VCAS_REF_reportsVCAS_REF_reports_params]
    FOREIGN KEY ([FK_REF_reportsId])
    REFERENCES [dbo].[VCAS_reports]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_REF_reportsVCAS_REF_reports_params'
CREATE INDEX [IX_FK_VCAS_REF_reportsVCAS_REF_reports_params]
ON [dbo].[VCAS_REF_reports_params]
    ([FK_REF_reportsId]);
GO

-- Creating foreign key on [FK_REF_itemsId] in table 'VCAS_REF_items_location'
ALTER TABLE [dbo].[VCAS_REF_items_location]
ADD CONSTRAINT [FK_VCAS_REF_itemsVCAS_REF_items_location]
    FOREIGN KEY ([FK_REF_itemsId])
    REFERENCES [dbo].[VCAS_REF_items]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_REF_itemsVCAS_REF_items_location'
CREATE INDEX [IX_FK_VCAS_REF_itemsVCAS_REF_items_location]
ON [dbo].[VCAS_REF_items_location]
    ([FK_REF_itemsId]);
GO

-- Creating foreign key on [FK_REF_userRolesId] in table 'VCAS_forms'
ALTER TABLE [dbo].[VCAS_forms]
ADD CONSTRAINT [FK_VCAS_REF_userRolesVCAS_forms]
    FOREIGN KEY ([FK_REF_userRolesId])
    REFERENCES [dbo].[VCAS_REF_userRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_REF_userRolesVCAS_forms'
CREATE INDEX [IX_FK_VCAS_REF_userRolesVCAS_forms]
ON [dbo].[VCAS_forms]
    ([FK_REF_userRolesId]);
GO

-- Creating foreign key on [FK_location] in table 'VCAS_district'
ALTER TABLE [dbo].[VCAS_district]
ADD CONSTRAINT [FK_VCAS_councilVCAS_district]
    FOREIGN KEY ([FK_location])
    REFERENCES [dbo].[VCAS_council]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_councilVCAS_district'
CREATE INDEX [IX_FK_VCAS_councilVCAS_district]
ON [dbo].[VCAS_district]
    ([FK_location]);
GO

-- Creating foreign key on [FK_location] in table 'VCAS_forms'
ALTER TABLE [dbo].[VCAS_forms]
ADD CONSTRAINT [FK_VCAS_councilVCAS_forms]
    FOREIGN KEY ([FK_location])
    REFERENCES [dbo].[VCAS_council]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_councilVCAS_forms'
CREATE INDEX [IX_FK_VCAS_councilVCAS_forms]
ON [dbo].[VCAS_forms]
    ([FK_location]);
GO

-- Creating foreign key on [FK_location] in table 'VCAS_issuedChecks'
ALTER TABLE [dbo].[VCAS_issuedChecks]
ADD CONSTRAINT [FK_VCAS_councilVCAS_issuedChecks]
    FOREIGN KEY ([FK_location])
    REFERENCES [dbo].[VCAS_council]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_councilVCAS_issuedChecks'
CREATE INDEX [IX_FK_VCAS_councilVCAS_issuedChecks]
ON [dbo].[VCAS_issuedChecks]
    ([FK_location]);
GO

-- Creating foreign key on [FK_councilId] in table 'VCAS_REF_expense_location'
ALTER TABLE [dbo].[VCAS_REF_expense_location]
ADD CONSTRAINT [FK_VCAS_councilVCAS_REF_expense_location]
    FOREIGN KEY ([FK_councilId])
    REFERENCES [dbo].[VCAS_council]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_councilVCAS_REF_expense_location'
CREATE INDEX [IX_FK_VCAS_councilVCAS_REF_expense_location]
ON [dbo].[VCAS_REF_expense_location]
    ([FK_councilId]);
GO

-- Creating foreign key on [FK_councilId] in table 'VCAS_REF_items_location'
ALTER TABLE [dbo].[VCAS_REF_items_location]
ADD CONSTRAINT [FK_VCAS_councilVCAS_REF_items_location]
    FOREIGN KEY ([FK_councilId])
    REFERENCES [dbo].[VCAS_council]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_councilVCAS_REF_items_location'
CREATE INDEX [IX_FK_VCAS_councilVCAS_REF_items_location]
ON [dbo].[VCAS_REF_items_location]
    ([FK_councilId]);
GO

-- Creating foreign key on [FK_location] in table 'VCAS_REF_reports_params'
ALTER TABLE [dbo].[VCAS_REF_reports_params]
ADD CONSTRAINT [FK_VCAS_councilVCAS_REF_reports_params]
    FOREIGN KEY ([FK_location])
    REFERENCES [dbo].[VCAS_council]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_councilVCAS_REF_reports_params'
CREATE INDEX [IX_FK_VCAS_councilVCAS_REF_reports_params]
ON [dbo].[VCAS_REF_reports_params]
    ([FK_location]);
GO

-- Creating foreign key on [FK_formsId] in table 'VCAS_REF_forms'
ALTER TABLE [dbo].[VCAS_REF_forms]
ADD CONSTRAINT [FK_VCAS_formsVCAS_REF_forms]
    FOREIGN KEY ([FK_formsId])
    REFERENCES [dbo].[VCAS_forms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_formsVCAS_REF_forms'
CREATE INDEX [IX_FK_VCAS_formsVCAS_REF_forms]
ON [dbo].[VCAS_REF_forms]
    ([FK_formsId]);
GO

-- Creating foreign key on [FK_location] in table 'VCAS_inventory'
ALTER TABLE [dbo].[VCAS_inventory]
ADD CONSTRAINT [FK_VCAS_councilVCAS_inventory]
    FOREIGN KEY ([FK_location])
    REFERENCES [dbo].[VCAS_council]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_councilVCAS_inventory'
CREATE INDEX [IX_FK_VCAS_councilVCAS_inventory]
ON [dbo].[VCAS_inventory]
    ([FK_location]);
GO

-- Creating foreign key on [FK_location] in table 'VCAS_orders'
ALTER TABLE [dbo].[VCAS_orders]
ADD CONSTRAINT [FK_VCAS_councilVCAS_orders]
    FOREIGN KEY ([FK_location])
    REFERENCES [dbo].[VCAS_council]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_councilVCAS_orders'
CREATE INDEX [IX_FK_VCAS_councilVCAS_orders]
ON [dbo].[VCAS_orders]
    ([FK_location]);
GO

-- Creating foreign key on [FK_inventoryId] in table 'VCAS_orders'
ALTER TABLE [dbo].[VCAS_orders]
ADD CONSTRAINT [FK_VCAS_inventoryVCAS_orders]
    FOREIGN KEY ([FK_inventoryId])
    REFERENCES [dbo].[VCAS_inventory]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_inventoryVCAS_orders'
CREATE INDEX [IX_FK_VCAS_inventoryVCAS_orders]
ON [dbo].[VCAS_orders]
    ([FK_inventoryId]);
GO

-- Creating foreign key on [FK_REF_itemsId] in table 'VCAS_inventory'
ALTER TABLE [dbo].[VCAS_inventory]
ADD CONSTRAINT [FK_VCAS_REF_itemsVCAS_inventory]
    FOREIGN KEY ([FK_REF_itemsId])
    REFERENCES [dbo].[VCAS_REF_items]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_REF_itemsVCAS_inventory'
CREATE INDEX [IX_FK_VCAS_REF_itemsVCAS_inventory]
ON [dbo].[VCAS_inventory]
    ([FK_REF_itemsId]);
GO

-- Creating foreign key on [FK_order_statusId] in table 'VCAS_orders'
ALTER TABLE [dbo].[VCAS_orders]
ADD CONSTRAINT [FK_VCAS_order_statusVCAS_orders]
    FOREIGN KEY ([FK_order_statusId])
    REFERENCES [dbo].[VCAS_REF_order_status]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_order_statusVCAS_orders'
CREATE INDEX [IX_FK_VCAS_order_statusVCAS_orders]
ON [dbo].[VCAS_orders]
    ([FK_order_statusId]);
GO

-- Creating foreign key on [FK_capture_paymentsId] in table 'VCAS_creditTrans1'
ALTER TABLE [dbo].[VCAS_creditTrans1]
ADD CONSTRAINT [FK_VCAS_capture_paymentsVCAS_creditTrans]
    FOREIGN KEY ([FK_capture_paymentsId])
    REFERENCES [dbo].[VCAS_capture_payments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_capture_paymentsVCAS_creditTrans'
CREATE INDEX [IX_FK_VCAS_capture_paymentsVCAS_creditTrans]
ON [dbo].[VCAS_creditTrans1]
    ([FK_capture_paymentsId]);
GO

-- Creating foreign key on [FK_location] in table 'VCAS_capture_payments'
ALTER TABLE [dbo].[VCAS_capture_payments]
ADD CONSTRAINT [FK_VCAS_councilVCAS_Captured_Payment]
    FOREIGN KEY ([FK_location])
    REFERENCES [dbo].[VCAS_council]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_councilVCAS_Captured_Payment'
CREATE INDEX [IX_FK_VCAS_councilVCAS_Captured_Payment]
ON [dbo].[VCAS_capture_payments]
    ([FK_location]);
GO

-- Creating foreign key on [FK_bankDetails] in table 'VCAS_capture_payments'
ALTER TABLE [dbo].[VCAS_capture_payments]
ADD CONSTRAINT [FK_VCAS_REF_bank_detailsVCAS_Captured_Payment]
    FOREIGN KEY ([FK_bankDetails])
    REFERENCES [dbo].[VCAS_REF_bank_details]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_REF_bank_detailsVCAS_Captured_Payment'
CREATE INDEX [IX_FK_VCAS_REF_bank_detailsVCAS_Captured_Payment]
ON [dbo].[VCAS_capture_payments]
    ([FK_bankDetails]);
GO

-- Creating foreign key on [FK_items] in table 'VCAS_capture_payments'
ALTER TABLE [dbo].[VCAS_capture_payments]
ADD CONSTRAINT [FK_VCAS_REF_itemsVCAS_Captured_Payment]
    FOREIGN KEY ([FK_items])
    REFERENCES [dbo].[VCAS_REF_items]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_REF_itemsVCAS_Captured_Payment'
CREATE INDEX [IX_FK_VCAS_REF_itemsVCAS_Captured_Payment]
ON [dbo].[VCAS_capture_payments]
    ([FK_items]);
GO

-- Creating foreign key on [FK_paymentType] in table 'VCAS_capture_payments'
ALTER TABLE [dbo].[VCAS_capture_payments]
ADD CONSTRAINT [FK_VCAS_REF_payment_typeVCAS_Captured_Payment]
    FOREIGN KEY ([FK_paymentType])
    REFERENCES [dbo].[VCAS_REF_payment_type]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_REF_payment_typeVCAS_Captured_Payment'
CREATE INDEX [IX_FK_VCAS_REF_payment_typeVCAS_Captured_Payment]
ON [dbo].[VCAS_capture_payments]
    ([FK_paymentType]);
GO

-- Creating foreign key on [FK_location] in table 'VCAS_debitAccounts'
ALTER TABLE [dbo].[VCAS_debitAccounts]
ADD CONSTRAINT [FK_VCAS_councilVCAS_debitAccounts]
    FOREIGN KEY ([FK_location])
    REFERENCES [dbo].[VCAS_council]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_councilVCAS_debitAccounts'
CREATE INDEX [IX_FK_VCAS_councilVCAS_debitAccounts]
ON [dbo].[VCAS_debitAccounts]
    ([FK_location]);
GO

-- Creating foreign key on [FK_debitAccountsId] in table 'VCAS_debitTrans1'
ALTER TABLE [dbo].[VCAS_debitTrans1]
ADD CONSTRAINT [FK_VCAS_debitAccountsVCAS_debitTrans]
    FOREIGN KEY ([FK_debitAccountsId])
    REFERENCES [dbo].[VCAS_debitAccounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_debitAccountsVCAS_debitTrans'
CREATE INDEX [IX_FK_VCAS_debitAccountsVCAS_debitTrans]
ON [dbo].[VCAS_debitTrans1]
    ([FK_debitAccountsId]);
GO

-- Creating foreign key on [FK_debitAccountsId] in table 'VCAS_issuedChecks'
ALTER TABLE [dbo].[VCAS_issuedChecks]
ADD CONSTRAINT [FK_VCAS_debitAccountsVCAS_issuedChecks]
    FOREIGN KEY ([FK_debitAccountsId])
    REFERENCES [dbo].[VCAS_debitAccounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_debitAccountsVCAS_issuedChecks'
CREATE INDEX [IX_FK_VCAS_debitAccountsVCAS_issuedChecks]
ON [dbo].[VCAS_issuedChecks]
    ([FK_debitAccountsId]);
GO

-- Creating foreign key on [FK_debitAccountsId] in table 'VCAS_reconcile'
ALTER TABLE [dbo].[VCAS_reconcile]
ADD CONSTRAINT [FK_VCAS_debitAccountsVCAS_reconcile]
    FOREIGN KEY ([FK_debitAccountsId])
    REFERENCES [dbo].[VCAS_debitAccounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_debitAccountsVCAS_reconcile'
CREATE INDEX [IX_FK_VCAS_debitAccountsVCAS_reconcile]
ON [dbo].[VCAS_reconcile]
    ([FK_debitAccountsId]);
GO

-- Creating foreign key on [FK_payment_Type] in table 'VCAS_debitAccounts'
ALTER TABLE [dbo].[VCAS_debitAccounts]
ADD CONSTRAINT [FK_VCAS_REF_payment_typeVCAS_debitAccounts]
    FOREIGN KEY ([FK_payment_Type])
    REFERENCES [dbo].[VCAS_REF_payment_type]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_REF_payment_typeVCAS_debitAccounts'
CREATE INDEX [IX_FK_VCAS_REF_payment_typeVCAS_debitAccounts]
ON [dbo].[VCAS_debitAccounts]
    ([FK_payment_Type]);
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

-- Creating foreign key on [FK_location] in table 'VCAS_undepositedFunds'
ALTER TABLE [dbo].[VCAS_undepositedFunds]
ADD CONSTRAINT [FK_VCAS_councilVCAS_undepositedFunds]
    FOREIGN KEY ([FK_location])
    REFERENCES [dbo].[VCAS_council]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_councilVCAS_undepositedFunds'
CREATE INDEX [IX_FK_VCAS_councilVCAS_undepositedFunds]
ON [dbo].[VCAS_undepositedFunds]
    ([FK_location]);
GO

-- Creating foreign key on [FK_customerId] in table 'VCAS_orders'
ALTER TABLE [dbo].[VCAS_orders]
ADD CONSTRAINT [FK_VCAS_customerVCAS_orders]
    FOREIGN KEY ([FK_customerId])
    REFERENCES [dbo].[VCAS_customer]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VCAS_customerVCAS_orders'
CREATE INDEX [IX_FK_VCAS_customerVCAS_orders]
ON [dbo].[VCAS_orders]
    ([FK_customerId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------