using BMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseBusiness.DTO
{
    public class PaymentOrderDetailDTO : PaymentOrderDetailModel
    {
        public List<UserTeamSaleModel>  ListUserTeamSale{ get; set; }
    }
}
