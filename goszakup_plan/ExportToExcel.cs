using dal_sqlce.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClosedXML.Excel;
using DomainClasses;
using dal_sqlce.Context;

namespace goszakup_plan
{
    public class ExportToExcel
    {
        List<PlanDTO> plan;
        string filePath = @"\Reports\shablon_year_plan.xlsx";
        public ExportToExcel(List<PlanDTO> _plan)
        {
            plan = _plan;
            System.Reflection.Assembly a = System.Reflection.Assembly.GetEntryAssembly();
            string baseDir = System.IO.Path.GetDirectoryName(a.Location);
            filePath = baseDir + filePath;
        }

        public void Export(string year, string destDir)
        {
            using (var workBook = new XLWorkbook(filePath))
            {
                var org = new EFRepozitory<Orgs>().GetList().FirstOrDefault();
                var sheet = workBook.Worksheet("Лист1");
                sheet.Cell(6, 2).Value = org?.BIN;
                sheet.Cell(6, 3).Value = org?.KodGU;
                sheet.Cell(6, 4).Value = org?.name_kz;
                sheet.Cell(6, 5).Value = org?.name_ru;
                sheet.Cell(6, 6).Value = year;
                int i = 1;
                foreach (var item in plan)
                {
                    sheet.Cell(i+10, 2).Value = i;
                    sheet.Cell(i + 10, 3).Value = item.Тип_пункта_плана;
                    sheet.Cell(i + 10, 4).Value = item.Администратор_бюджетной_программы;
                    sheet.Cell(i + 10, 5).Value = item.Программа;
                    sheet.Cell(i + 10, 6).Value = item.Подпрограмма;
                    sheet.Cell(i + 10, 7).Value = item.Специфика;
                    sheet.Cell(i + 10, 8).Value = item.Источник_финансирования;
                    sheet.Cell(i + 10, 9).Value = item.Вид_предмета_закупок;
                    sheet.Cell(i + 10, 10).Value = item.КТРУ;
                    sheet.Cell(i + 10, 11).Value = item.Наименование_каз;
                    sheet.Cell(i + 10, 12).Value = item.Наименование_рус;
                    sheet.Cell(i + 10, 13).Value = item.Краткая_каз;
                    sheet.Cell(i + 10, 14).Value = item.Краткая_рус;
                    sheet.Cell(i + 10, 15).Value = item.Дополнительная_каз;
                    sheet.Cell(i + 10, 16).Value = item.Дополнительная_рус;
                    sheet.Cell(i + 10, 17).Value = item.Способ_закупки;
                    sheet.Cell(i + 10, 18).Value = item.Обоснование;
                    sheet.Cell(i + 10, 19).Value = item.Единица_измерения;
                    sheet.Cell(i + 10, 20).Value = item.Количество;
                    sheet.Cell(i + 10, 21).Value = item.Цена;
                    sheet.Cell(i + 10, 22).Value = item.Сумма;
                    sheet.Cell(i + 10, 23).Value = item.Прогнозная_сумма1;
                    sheet.Cell(i + 10, 24).Value = item.Прогнозная_сумма2;
                    sheet.Cell(i + 10, 25).Value = item.Прогнозная_сумма3;
                    sheet.Cell(i + 10, 26).Value = item.Планируемый_срок;
                    sheet.Cell(i + 10, 27).Value = item.СрокПоставки_каз;
                    sheet.Cell(i + 10, 28).Value = item.СрокПоставки_рус;
                    sheet.Cell(i + 10, 29).Value = item.МестоПоставки_каз;
                    sheet.Cell(i + 10, 30).Value = item.МестоПоставки_рус;
                    sheet.Cell(i + 10, 31).Value = item.Программа;
                    sheet.Cell(i + 10, 32).Value = item.Аванс;
                    sheet.Cell(i + 10, 33).Value = item.ПризнакПоставщика == "1"? "Организация инвалидов" : "";
                    CellBorder(sheet.Range(i+10, 2, i+10, 33));
                    i++;
                }
                workBook.SaveAs(destDir);
            }

        }


        private void CellBorder(IXLRange cells)
        {
            foreach (var item in cells.Cells())
            {
                item.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }
        }
    }
}
