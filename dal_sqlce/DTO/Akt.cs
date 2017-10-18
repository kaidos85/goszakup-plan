using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dal_sqlce.DTO
{
    [Serializable]
    public class Akt
    {
        public string bin { get; set; }

        public string finYear { get; set; }

        public string orgNameKz { get; set; }

        public string orgNameRu { get; set; }

        public AktGuCodes guCodes { get; set; }

        public List<yearPlans> yearPlans1 { get; set; }

    }
}
