using Domain.Exceptions;
using Domain.Util.Extensions;
using LazyCache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Presentation.Web.Models.UpdateIS;
using Presentation.Web.Services.Proxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Presentation.Web.Controllers {
    public class UpdateIsController : BaseController {
        private readonly ILogger<UpdateIsController> _logger;
        private readonly IRenewalService renewalService;

        public UpdateIsController(IAppCache memoryCache, ILogger<UpdateIsController> logger,
            ICommonService commonService, IRenewalService renewalService) : base(memoryCache, logger, commonService) {
            _logger = logger;
            this.renewalService = renewalService;
        }

        public IActionResult UpdateIs() {
            try {

                var model = new UpdateISViewModel();
                return View("~/Views/UpdateIS/UpdateIs.cshtml", model);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { }, e);
                }
            }
        }

        private IList<ExcelItem> ProcessItems(Stream stream) {
            var items = new List<ExcelItem>();

            XSSFWorkbook hssfwb;
            hssfwb = new XSSFWorkbook(stream);

            for (int i = 0; i < hssfwb.NumberOfSheets; i++) {
                ISheet sheet = hssfwb.GetSheetAt(i);
                for (int row = 1; row <= sheet.LastRowNum; row++) {
                    var item = new ExcelItem() {
                        SheetName = sheet.SheetName,
                        TakerName = sheet.GetRow(row).GetCell(0).StringCellValue,
                        InsuredName = sheet.GetRow(row).GetCell(1).StringCellValue,
                        PolicyNumber = (long)sheet.GetRow(row).GetCell(2).NumericCellValue,
                        StartOfTerm = sheet.GetRow(row).GetCell(3).DateCellValue,
                        EndOfTerm = sheet.GetRow(row).GetCell(4).DateCellValue,
                        InsuredAmount = (decimal)sheet.GetRow(row).GetCell(5).NumericCellValue,
                        UpdatedInsuredAmount = null,
                    };
                    item.UpdatedInsuredAmount = renewalService.ApplySelicCorrection(item.InsuredAmount, item.StartOfTerm, DateTime.Now.Date);
                    items.Add(item);
                }
            }

            return items;
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile file) {
            try {

                if (file == null || file.Length == 0) {
                    return base.ReturnError("Arquivo não chegou no servidor.");
                }
                if (file.Length > 5242880) {    //TODO Configurar, hj fixado em 5MB
                    return base.ReturnError("Arquivo excede o tamanho máximo permitido.");
                }

                var model = new UpdateISViewModel() {
                    Results = ProcessItems(file.OpenReadStream())
                };
                return PartialView("~/Views/UpdateIS/UpdateIsGrid.cshtml", model);

            } catch (ApplicationException e) {
                return base.ReturnError(e.Message);
            } catch (Exception e) {
                return base.ReturnException(MethodBase.GetCurrentMethod(), file, e, e.Message);
            }
        }

        [HttpPost]
        public IActionResult DownloadExcel(IFormFile file) {
            try {

                if (file == null || file.Length == 0) {
                    return base.ReturnError("Arquivo não chegou no servidor.");
                }
                if (file.Length > 5242880) {    //TODO Configurar, hj fixado em 5MB
                    return base.ReturnError("Arquivo excede o tamanho máximo permitido.");
                }

                var items = ProcessItems(file.OpenReadStream());

                var newWorkbook = new XSSFWorkbook();
                var format = newWorkbook.CreateDataFormat();
                var styleDate = newWorkbook.CreateCellStyle();
                styleDate.DataFormat = format.GetFormat("dd/mm/yyyy");
                var styleCurrency = newWorkbook.CreateCellStyle();
                styleCurrency.DataFormat = format.GetFormat("#,##0.00");
                var styleLong = newWorkbook.CreateCellStyle();
                styleLong.DataFormat = format.GetFormat("##0");

                var insuredObjectText = new StringBuilder();
                insuredObjectText.AppendLine("Pelo presente endosso, em complemento a Apólice nº {0}, a partir da presente data o valor da garantia, passando de {1} para {2}.");
                insuredObjectText.AppendLine("Permanecem inalteradas todas as disposições da Apólice que não tenham sido expressamente alteradas por este endosso.");

                foreach (var sheetName in items.Select(x => x.SheetName).Distinct()) {
                    var rowIndex = 0;
                    var newSheet = newWorkbook.CreateSheet(sheetName);
                    var newRow = newSheet.CreateRow(rowIndex);
                    var newCell = newRow.CreateCell(0); newCell.SetCellValue("Tomador");
                    newCell = newRow.CreateCell(1); newCell.SetCellValue("Segurado");
                    newCell = newRow.CreateCell(2); newCell.SetCellValue("Apólice");
                    newCell = newRow.CreateCell(3); newCell.SetCellValue("Início Vigência");
                    newCell = newRow.CreateCell(4); newCell.SetCellValue("Fim Vigência");
                    newCell = newRow.CreateCell(5); newCell.SetCellValue("IS");
                    newCell = newRow.CreateCell(6); newCell.SetCellValue("Valor atualizado");
                    newCell = newRow.CreateCell(7); newCell.SetCellValue("Diferença");
                    newCell = newRow.CreateCell(8); newCell.SetCellValue("Prêmio");
                    newCell = newRow.CreateCell(9); newCell.SetCellValue("Indice");
                    newCell = newRow.CreateCell(10); newCell.SetCellValue("Objeto segurado");
                    rowIndex++;

                    var sheetItems = items.Where(x => x.SheetName.Equals(sheetName)).ToList();
                    foreach (var item in sheetItems) {
                        newRow = newSheet.CreateRow(rowIndex);
                        newCell = newRow.CreateCell(0); newCell.SetCellValue(item.TakerName);
                        newCell = newRow.CreateCell(1); newCell.SetCellValue(item.InsuredName);
                        newCell = newRow.CreateCell(2); newCell.SetCellValue(item.PolicyNumber); newCell.CellStyle = styleLong;
                        newCell = newRow.CreateCell(3); newCell.SetCellValue(item.StartOfTerm); newCell.CellStyle = styleDate;
                        newCell = newRow.CreateCell(4); newCell.SetCellValue(item.EndOfTerm); newCell.CellStyle = styleDate;
                        newCell = newRow.CreateCell(5); newCell.SetCellValue((double)item.InsuredAmount); newCell.CellStyle = styleCurrency;
                        newCell = newRow.CreateCell(6); newCell.SetCellValue((double)item.UpdatedInsuredAmount); newCell.CellStyle = styleCurrency;
                        newCell = newRow.CreateCell(7); newCell.SetCellValue((double)(item.UpdatedInsuredAmount - item.InsuredAmount)); newCell.CellStyle = styleCurrency;
                        newCell = newRow.CreateCell(8); newCell.SetCellValue("");
                        newCell = newRow.CreateCell(9); newCell.SetCellValue("");
                        newCell = newRow.CreateCell(10); newCell.SetCellValue(string.Format(insuredObjectText.ToString(), item.PolicyNumber, item.InsuredAmount.FormatCurrency(), item.InsuredAmount.FormatCurrency()));
                        rowIndex++;
                    }
                }

                using var exportData = new MemoryStream(); 
                newWorkbook.Write(exportData);
                return File(exportData.ToArray(), "application/octet-stream", $"ATUALIZADO-{file.FileName}");

            } catch (ApplicationException e) {
                return base.ReturnError(e.Message);
            } catch (Exception e) {
                return base.ReturnException(MethodBase.GetCurrentMethod(), file, e, e.Message);
            }
        }

    }
}
