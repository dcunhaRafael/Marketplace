using Domain.Enumerators;
using Domain.Payload;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Portal.Web.Models.ComissionStatement {
    public class ComissionStatementExportSettings {
        public ComissionStatementExportSettingsCover Cover { get; set; }
        public ComissionStatementExportSettingsBusiness Business { get; set; }
        public ComissionStatementExportSettingsTypes Types { get; set; }
        public ComissionStatementExportSettingsEntries Entries { get; set; }
    }

    public class ComissionStatementExportSettingsCover {
        public int SheetIndex { get; set; }
        public ComissionStatementExportSettingsCell StatementNumber { get; set; }
        public ComissionStatementExportSettingsCell BrokerCnpjNumber { get; set; }
        public ComissionStatementExportSettingsCell BrokerName { get; set; }
        public ComissionStatementExportSettingsCell BrokerSusepCode { get; set; }
        public ComissionStatementExportSettingsCell Competency { get; set; }
        public ComissionStatementExportSettingsCell OpeningDate { get; set; }
        public ComissionStatementExportSettingsCell ClosingDate { get; set; }
        public ComissionStatementExportSettingsCell EntryCount { get; set; }
        public ComissionStatementExportSettingsCell ComissionValue { get; set; }
        public ComissionStatementExportSettingsCell StatusName { get; set; }
        public ComissionStatementExportSettingsCoverTotals Totals { get; set; }
        public ComissionStatementExportSettingsCoverPayment Payment { get; set; }
        public ComissionStatementExportSettingsCoverBalance Balance { get; set; }
    }

    public class ComissionStatementExportSettingsCoverTotals {
        public ComissionStatementExportSettingsCell ComissionValue { get; set; }
        public ComissionStatementExportSettingsCell ComissionNetValue { get; set; }
        public ComissionStatementExportSettingsCell TaxValue { get; set; }
        public ComissionStatementExportSettingsCell TaxableComissionValue { get; set; }
        public ComissionStatementExportSettingsCell NotTaxableComissionValue { get; set; }
    }
    public class ComissionStatementExportSettingsCoverBalance {
        public ComissionStatementExportSettingsCell Name { get; set; }
        public ComissionStatementExportSettingsCell DebitValue { get; set; }
        public ComissionStatementExportSettingsCell CreditValue { get; set; }
    }
    public class ComissionStatementExportSettingsCoverPayment {
        public ComissionStatementExportSettingsCell PaymentRequestDate { get; set; }
        public ComissionStatementExportSettingsCell PayDay { get; set; }
        public ComissionStatementExportSettingsCell ReceiptNumber { get; set; }
    }

    public class ComissionStatementExportSettingsTypes {
        public int SheetIndex { get; set; }
        public ComissionStatementExportSettingsCell ComissionTypeName { get; set; }
        public ComissionStatementExportSettingsCell ComissionValue { get; set; }
    }

    public class ComissionStatementExportSettingsBusiness {
        public int SheetIndex { get; set; }
        public ComissionStatementExportSettingsCell BusinessName { get; set; }
        public ComissionStatementExportSettingsCell ComissionTypeName { get; set; }
        public ComissionStatementExportSettingsCell ComissionValue { get; set; }
    }

    public class ComissionStatementExportSettingsEntries {
        public int SheetIndex { get; set; }
        public ComissionStatementExportSettingsCell BusinessName { get; set; }
        public ComissionStatementExportSettingsCell ProposalNumber { get; set; }
        public ComissionStatementExportSettingsCell PolicyNumber { get; set; }
        public ComissionStatementExportSettingsCell EndorsementNumber { get; set; }
        public ComissionStatementExportSettingsCell InstallmentNumber { get; set; }
        public ComissionStatementExportSettingsCell InsuredName { get; set; }
        public ComissionStatementExportSettingsCell TariffPremiumValue { get; set; }
        public ComissionStatementExportSettingsCell ComissionTypeName { get; set; }
        public ComissionStatementExportSettingsCell ComissionPercentage { get; set; }
        public ComissionStatementExportSettingsCell ComissionValue { get; set; }
    }


    public class ComissionStatementExportSettingsCell {
        public int Row { get; set; }
        public int Col { get; set; }
    }
}