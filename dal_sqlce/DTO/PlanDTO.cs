using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dal_sqlce.DTO
{
    public class PlanDTO
    {
        public long Id { get; set; } 
        public string Тип_пункта_плана { get; set; }
        public string Источник_финансирования { get; set; }
        public string Администратор_бюджетной_программы { get; set; }
        public string Программа { get; set; }
        public string Подпрограмма { get; set; }
        public string Специфика { get; set; }
        public string Способ_закупки { get; set; }
        public string Обоснование { get; set; }
        public string Вид_предмета_закупок { get; set; }
        public string КТРУ { get; set; } 
        public string Наименование_каз { get; set; } 
        public string Наименование_рус { get; set; }    
        public string Краткая_каз { get; set; }
        public string Краткая_рус { get; set; }
        public string Дополнительная_каз { get; set; }
        public string Дополнительная_рус { get; set; } 
        public string Единица_измерения { get; set; }
        public int Количество { get; set; }
        public decimal Сумма { get; set; }
        public decimal Цена { get; set; }
        public decimal Прогнозная_сумма1 { get; set; }
        public decimal Прогнозная_сумма2 { get; set; }
        public decimal Прогнозная_сумма3 { get; set; }
        public decimal Аванс { get; set; }        
        public string Планируемый_срок { get; set; }
        public string СрокПоставки_каз { get; set; }
        public string СрокПоставки_рус { get; set; }        
        public string МестоПоставки_каз { get; set; }
        public string МестоПоставки_рус { get; set; }
        public long КАТО { get; set; }
        public string ПризнакПоставщика { get; set; }
        public long Year { get; set; }
        public Nullable<long> abp { get; set; }
        public Nullable<long> FinSource { get; set; }
        public Nullable<long> monthId { get; set; }
        public Nullable<long> TradeMethod { get; set; }
        public Nullable<long> tradeMethodJust { get; set; }
        public Nullable<long> Program { get; set; }
        public Nullable<long> SubProgram { get; set; }
        public Nullable<long> stru { get; set; }
        public Nullable<long> Specific { get; set; }        
        public Nullable<long> pointTypeCode { get; set; }
        public Nullable<long> DisablePerson { get; set; }


        ////     [] NVARCHAR(200),
        ////[Администратор бюджетной программы] NVARCHAR(100),
        ////[Программа] NVARCHAR(50),
        ////[Подпрограмма] NVARCHAR(50),
        ////[Специфика] NVARCHAR(1000),
        ////[Источник финансирования] NVARCHAR(250),
        ////[Вид предмета закупок] NVARCHAR(250),
        ////[КТРУ] NVARCHAR(50),
        ////[Наименование_каз] NVARCHAR(1000),
        ////[Наименование_рус] NVARCHAR(1000),
        ////[Краткая_каз] NVARCHAR(1000),
        ////[Краткая_рус] NVARCHAR(1000),
        ////[Дополнительная_каз] NVARCHAR(1000),
        ////[Дополнительная_рус] NVARCHAR(1000),
        ////[Способ закупок] NVARCHAR(100),
        ////[Обоснование] NVARCHAR(100),
        ////[Количество]
        ////     MONEY,
        ////[Цена]
        ////     MONEY,
        ////[Сумма]
        ////     MONEY,
        ////[Прогнозная сумма1]
        ////     MONEY,
        ////[Прогнозная сумма2]
        ////     MONEY,
        ////[Прогнозная сумма3]
        ////     MONEY,
        ////[Планируемый срок] NVARCHAR(250),
        ////[СрокПоставки_каз] NVARCHAR(500),
        ////[СрокПоставки_рус] NVARCHAR(500),
        ////[КАТО] NVARCHAR(50),
        ////[МестоПоставки_каз] NVARCHAR(1000),
        ////[МестоПоставки_рус] NVARCHAR(1000),
        ////[Аванс] NVARCHAR(50),
        ////[ПризнакПост] NVARCHAR(200),
        ////[god] NVARCHAR(100)

    }
}
