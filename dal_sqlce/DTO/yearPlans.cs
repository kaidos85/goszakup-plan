using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dal_sqlce.DTO
{
    [Serializable]
    public class yearPlans
    {

        public int abpId{ get; set; }

        public string aditDeliveryKz{ get; set; }

        public string aditDeliveryRu{ get; set; }

        public int count{ get; set; }

        public string descriptionKz{ get; set; }

        public string descriptionRu{ get; set; }

        public int disablePerson{ get; set; }

        public int enstruId{ get; set; }

        public string extraDescriptionKz{ get; set; }

        public string extraDescriptionRu{ get; set; }

        public int finSourceId{ get; set; }

        public decimal firdYearSum{ get; set; }

        public decimal firstYearSum{ get; set; }

        public int guCode{ get; set; }

        public int id{ get; set; }

        public int kato{ get; set; }

        public string mkeiId{ get; set; }

        public int monthCode{ get; set; }

        public int pointTypeCode{ get; set; }

        public decimal prepayment{ get; set; }

        public decimal price{ get; set; }

        public int programId{ get; set; }

        public decimal secondYearSum{ get; set; }

        public decimal specificId{ get; set; }

        public int subProgramId{ get; set; }

        public string subjectTypeCode{ get; set; }

        public string supplyDateKz{ get; set; }

        public string supplyDateRu{ get; set; }

        public int tradeMethodId{ get; set; }

        public int tradeMethodJistiId{ get; set; }
       
    }

}
