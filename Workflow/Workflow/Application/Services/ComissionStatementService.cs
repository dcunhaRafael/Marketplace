using Application.Interfaces.Services;
using Domain.Exceptions;
using Domain.Payload;
using Domain.Util.Log;
using Domain.Util.Extensions;
using Infrastructure.Interfaces.Repositories;
using Integration.Interfaces.Legacy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using Newtonsoft.Json;

namespace Application.Services {
    public class ComissionStatementService : BaseLogger, IComissionStatementService {
        private readonly IComissionStatementStatusRepository comissionStatementStatusRepository;
        private readonly ILegacyBrokerService legacyBrokerService;

        public ComissionStatementService(ILogger<ComissionStatementService> logger,
            IComissionStatementStatusRepository comissionStatementStatusRepository,
            ILegacyBrokerService legacyBrokerService) : base(logger) {
            this.comissionStatementStatusRepository = comissionStatementStatusRepository;
            this.legacyBrokerService = legacyBrokerService;
        }

        public async Task<IList<ComissionStatementStatus>> ListAsync() {
            var methodParameters = new { };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await comissionStatementStatusRepository.ListAsync();
                var payloads = from a in items select new ComissionStatementStatus() { 
                                                            Id = a.Id, 
                                                            Name = a.Name, 
                                                            Description = a.Description,
                                                            LegacyCode = a.LegacyCode, 
                                                            BackgroundColor = a.BackgroundColor, 
                                                            TextColor = a.TextColor 
                                                        };
                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem dos status do extrato de comissão: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public IList<ComissionStatement> ListComissionStatement(int? statementNumber, DateTime? fromDate, DateTime? toDate, int? status, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, int loggedUserId) {
            var methodParameters = new { statementNumber, fromDate, toDate, status, brokerLegacyCode, brokerSusepCode, brokerUserId, loggedUserId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = legacyBrokerService.ListComissionStatement(statementNumber, fromDate, toDate, status, brokerLegacyCode, brokerSusepCode, brokerUserId, new LoggerComplement() { UserId = loggedUserId });
                return items;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem do extrato de comissão: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<ComissionStatementCover> GetComissionStatementCover(int statementNumber, string competency, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, int loggedUserId) {
            var methodParameters = new { statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, loggedUserId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var cover = new ComissionStatementCover();

                var statement = ListComissionStatement(statementNumber, null, null, null, brokerLegacyCode, brokerSusepCode, brokerUserId, loggedUserId).FirstOrDefault(x => x.Competency.Equals(competency));
                cover.Statement = statement;

                var items = legacyBrokerService.ListComissionStatementDetails(statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, new LoggerComplement() { UserId = loggedUserId });
                var detail = items.FirstOrDefault(); ;

                var status = await comissionStatementStatusRepository.GetAsync(detail.StatusName);
                if (!string.IsNullOrWhiteSpace(status?.ImportantWarningText)) {
                    detail.ImportantWarningText = FillText(status.ImportantWarningText, detail);
                }
                cover.Details = detail;

                var types = legacyBrokerService.ListComissionStatementTypes(statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, new LoggerComplement() { UserId = loggedUserId });
                cover.Types = types;

                var business = legacyBrokerService.ListComissionStatementBusiness(statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, new LoggerComplement() { UserId = loggedUserId });
                cover.Business = business;

                var payments = new List<ComissionStatementPayment>();
                foreach (var item in types) {
                    payments.Add(new ComissionStatementPayment() {
                        Name = item.ComissionTypeName,
                        DebitValue = (item.ComissionValue.Value < 0M ? item.ComissionValue.Value * -1M : 0M),
                        CreditValue = (item.ComissionValue.Value > 0M ? item.ComissionValue.Value : 0M)
                    });
                }
                foreach (var item in detail.Taxes) {
                    payments.Add(new ComissionStatementPayment() {
                        Name = item.Name,
                        DebitValue = item.Value.Value,
                        CreditValue = 0M
                    });
                }
                cover.Payments = payments;

                return cover;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro obtendo capa do extrato de comissão: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        private string FillText(string bodyText, ComissionStatementDetail detail) {
            bodyText = bodyText.Replace("@{NUMERO_EXTRATO}", detail.StatementNumber.ToString());
            bodyText = bodyText.Replace("@{NOME_CORRETOR}", detail.Broker.Name);
            bodyText = bodyText.Replace("@{COMPETENCIA}", detail.Competency);
            bodyText = bodyText.Replace("@{NUMERO_RECIBO}", detail.ReceiptNumber.ToString());
            bodyText = bodyText.Replace("@{VALOR_PAGAMENTO}", detail.ComissionNetValue?.FormatCurrency());
            bodyText = bodyText.Replace("@{BANCO_PAGAMENTO}", detail.PaymentBank);
            bodyText = bodyText.Replace("@{AGENCIA_PAGAMENTO}", detail.PaymentBranch);
            bodyText = bodyText.Replace("@{CONTA_PAGAMENTO}", detail.PaymentAccount);
            return bodyText.ToString();
        }


        //public IList<ComissionStatementType> ListComissionStatementTypes(int statementNumber, string competency, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, int loggedUserId) {
        //    var methodParameters = new { statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, loggedUserId };
        //    LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
        //    try {

        //        var items = legacyBrokerService.ListComissionStatementTypes(statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, new LoggerComplement() { UserId = loggedUserId });
        //        return items;

        //    } catch (Exception e) {
        //        if (!(e is ServiceException || e is IntegrationException)) {
        //            LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
        //        }
        //        throw new ServiceException($"Erro na listagem do extrato de comissão por tipo: {e.Message}", e);
        //    } finally {
        //        LogTrace(MethodBase.GetCurrentMethod(), "End");
        //    }
        //}

        //public IList<ComissionStatementBusiness> ListComissionStatementBusiness(int statementNumber, string competency, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, int loggedUserId) {
        //    var methodParameters = new { statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, loggedUserId };
        //    LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
        //    try {

        //        var items = legacyBrokerService.ListComissionStatementBusiness(statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, new LoggerComplement() { UserId = loggedUserId });
        //        return items;

        //    } catch (Exception e) {
        //        if (!(e is ServiceException || e is IntegrationException)) {
        //            LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
        //        }
        //        throw new ServiceException($"Erro na listagem do extrato de comissão por ramo: {e.Message}", e);
        //    } finally {
        //        LogTrace(MethodBase.GetCurrentMethod(), "End");
        //    }
        //}

        public IList<ComissionStatementEntry> ListComissionStatementEntries(int statementNumber, string competency, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, int loggedUserId) {
            var methodParameters = new { statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, loggedUserId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = legacyBrokerService.ListComissionStatementEntries(statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, new LoggerComplement() { UserId = loggedUserId });
                return items;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem dos lançamentos do extrato de comissão: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<ExportedFile> ExportComissionStatement(string templateFile, int statementNumber, string competency, string brokerName, long brokerCnpjNumber, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, int loggedUserId) {
            var methodParameters = new { statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, loggedUserId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var cover = await GetComissionStatementCover(statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, loggedUserId);
                var entries = ListComissionStatementEntries(statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, loggedUserId);


                //var items = legacyBrokerService.ListComissionStatementDetails(statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, new LoggerComplement() { UserId = loggedUserId });
                //var detail = items.FirstOrDefault(); ;

                //var status = await comissionStatementStatusRepository.GetAsync(detail.StatusName);
                //if (!string.IsNullOrWhiteSpace(status?.ImportantWarningText)) {
                //    detail.ImportantWarningText = FillText(status.ImportantWarningText, detail);

                //}

                //Path.Combine(webHostingEnvironment.WebRootPath, "templates", fileName)

                // Carrega as configurações de geração do Excel
                ComissionStatementExportSettings excelSettings = null;
                var excelFileSettings = File.ReadAllText(templateFile + ".json");
                excelSettings = JsonConvert.DeserializeObject<ComissionStatementExportSettings>(excelFileSettings);

                // Abre o arquivo de template
                using (FileStream excelFile = new FileStream(templateFile, FileMode.Open, FileAccess.Read)) {
                    var workbook = new XSSFWorkbook(excelFile);

                    var format = workbook.CreateDataFormat();
                    var styleCurrency = workbook.CreateCellStyle();
                    styleCurrency.DataFormat = format.GetFormat("#,##0.00");

                    // Preenche a capa
                    ISheet sheet = workbook.GetSheetAt(excelSettings.Cover.SheetIndex);
                    FillCell(sheet, excelSettings.Cover.StatementNumber, cover.Statement.StatementNumber);
                    FillCell(sheet, excelSettings.Cover.BrokerCnpjNumber,brokerCnpjNumber.FormatCpfCnpj());
                    FillCell(sheet, excelSettings.Cover.BrokerName, brokerName);
                    FillCell(sheet, excelSettings.Cover.BrokerSusepCode, brokerSusepCode);
                    FillCell(sheet, excelSettings.Cover.Competency, cover.Statement.Competency);
                    FillCell(sheet, excelSettings.Cover.OpeningDate, cover.Statement.OpeningDate);
                    FillCell(sheet, excelSettings.Cover.ClosingDate, cover.Statement.ClosingDate);
                    FillCell(sheet, excelSettings.Cover.EntryCount, cover.Statement.EntryCount);
                    FillCell(sheet, excelSettings.Cover.ComissionValue, (double)cover.Statement.ComissionValue);
                    FillCell(sheet, excelSettings.Cover.StatusName, cover.Statement.StatusName);
                    FillCell(sheet, excelSettings.Cover.Totals.ComissionValue, (double)cover.Details.ComissionValue);
                    FillCell(sheet, excelSettings.Cover.Totals.ComissionNetValue, (double)cover.Details.ComissionNetValue);
                    FillCell(sheet, excelSettings.Cover.Totals.TaxValue, (double)cover.Details.TaxValue);
                    FillCell(sheet, excelSettings.Cover.Totals.TaxableComissionValue, (double)cover.Details.TaxableComissionValue);
                    FillCell(sheet, excelSettings.Cover.Totals.NotTaxableComissionValue, (double)cover.Details.NotTaxableComissionValue);
                    FillCell(sheet, excelSettings.Cover.Payment.PaymentRequestDate, cover.Details.PaymentRequestDate);
                    FillCell(sheet, excelSettings.Cover.Payment.PayDay, cover.Statement.PayDay);
                    FillCell(sheet, excelSettings.Cover.Payment.ReceiptNumber, cover.Details.ReceiptNumber);
                    for (int i = 0; i < cover.Payments.Count; i++) {
                        FillCell(sheet, excelSettings.Cover.Balance.Name, cover.Payments[i].Name, i);
                        FillCell(sheet, excelSettings.Cover.Balance.DebitValue, (double)cover.Payments[i].DebitValue, i);
                        FillCell(sheet, excelSettings.Cover.Balance.CreditValue, (double)cover.Payments[i].CreditValue, i);
                    }
                    FillCell(sheet, excelSettings.Cover.Balance.Name, "Pagamentos", cover.Payments.Count);
                    FillCell(sheet, excelSettings.Cover.Balance.DebitValue, (double)cover.Payments.Sum(x => x.DebitValue), cover.Payments.Count);
                    FillCell(sheet, excelSettings.Cover.Balance.CreditValue, (double)cover.Payments.Sum(x => x.CreditValue), cover.Payments.Count);

                    // Preenche a lista por tipo
                    sheet = workbook.GetSheetAt(excelSettings.Types.SheetIndex);
                    for (int i = 0; i < cover.Types.Count; i++) {
                        FillCell(sheet, excelSettings.Types.ComissionTypeName, cover.Types[i].ComissionTypeName, i);
                        FillCell(sheet, excelSettings.Types.ComissionValue, (double)cover.Types[i].ComissionValue, i);
                    }

                    // Preenche a lista por ramo
                    sheet = workbook.GetSheetAt(excelSettings.Business.SheetIndex);
                    for (int i = 0; i < cover.Business.Count; i++) {
                        FillCell(sheet, excelSettings.Business.BusinessName, cover.Business[i].BusinessName, i);
                        FillCell(sheet, excelSettings.Business.ComissionTypeName, cover.Business[i].ComissionTypeName, i);
                        FillCell(sheet, excelSettings.Business.ComissionValue, (double)cover.Business[i].ComissionValue, i);
                    }

                    // Preenche a lista de lançamentos
                    sheet = workbook.GetSheetAt(excelSettings.Entries.SheetIndex);
                    for (int i = 0; i < entries.Count; i++) {
                        FillCell(sheet, excelSettings.Entries.BusinessName, entries[i].BusinessName, i);
                        FillCell(sheet, excelSettings.Entries.ProposalNumber, entries[i].ProposalNumber, i);
                        FillCell(sheet, excelSettings.Entries.PolicyNumber, entries[i].PolicyNumber, i);
                        FillCell(sheet, excelSettings.Entries.EndorsementNumber, entries[i].EndorsementNumber, i);
                        FillCell(sheet, excelSettings.Entries.InstallmentNumber, entries[i].InstallmentNumber, i);
                        FillCell(sheet, excelSettings.Entries.InsuredName, entries[i].InsuredName, i);
                        FillCell(sheet, excelSettings.Entries.TariffPremiumValue, (double)entries[i].TariffPremiumValue, i);
                        FillCell(sheet, excelSettings.Entries.ComissionTypeName, entries[i].ComissionTypeName, i);
                        FillCell(sheet, excelSettings.Entries.ComissionPercentage, (double)entries[i].ComissionPercentage, i);
                        FillCell(sheet, excelSettings.Entries.ComissionValue, (double)entries[i].ComissionValue, i);
                    }

                    using var buffer = new MemoryStream();
                    workbook.Write(buffer);
                    return new ExportedFile() {
                        Filename = $"ExtratoComissao-{brokerSusepCode}-{competency}-{statementNumber}.xlsx",
                        Base64 = Convert.ToBase64String(buffer.ToArray())
                    };
                }

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na exportação do extrato de comissão: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        private void FillCell(ISheet sheet, ComissionStatementExportSettingsCell cellSettings, dynamic value, int increaseRow = 0) {

            if (sheet.GetRow(cellSettings.Row + increaseRow) == null) {
                var rowInsert = sheet.CreateRow(cellSettings.Row + increaseRow);
                var rowSource = sheet.GetRow(cellSettings.Row);
                var rowStyle = rowSource.RowStyle;//Get the current line style
                for (int col = 0; col < rowSource.LastCellNum; col++) {
                    var cellSource = rowSource.GetCell(col);
                    var cellInsert = rowInsert.CreateCell(col);
                    var cellStyle = cellSource?.CellStyle;
                    if (cellStyle != null)
                        cellInsert.CellStyle = cellSource.CellStyle;

                }
            }
            //if (sheet.GetRow(cellSettings.Row + increaseRow).GetCell(cellSettings.Col) == null) {
            //    sheet.GetRow(cellSettings.Row + increaseRow).CreateCell(cellSettings.Col);
            //}

            sheet.GetRow(cellSettings.Row + increaseRow).GetCell(cellSettings.Col).SetCellValue(value);
        }

        private void ShiftRows(ISheet sheet, int sourceRowIndex, int startRow, int rowCount) {
            sheet.ShiftRows(startRow, sheet.LastRowNum, rowCount, true, false);
            var rowSource = sheet.GetRow(sourceRowIndex);
            var rowStyle = rowSource.RowStyle;//Get the current line style
            for (int i = startRow; i < startRow + rowCount; i++) {
                var rowInsert = sheet.CreateRow(i);
                if (rowStyle != null)
                    rowInsert.RowStyle = rowStyle;
                rowInsert.Height = rowSource.Height;

                for (int col = 0; col < rowSource.LastCellNum; col++) {
                    var cellsource = rowSource.GetCell(col);
                    var cellInsert = rowInsert.CreateCell(col);
                    var cellStyle = cellsource?.CellStyle;
                    //Set cell style
                    if (cellStyle != null)
                        cellInsert.CellStyle = cellsource.CellStyle;

                }
            }
        }

    }
}
