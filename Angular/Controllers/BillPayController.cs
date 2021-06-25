using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiBankWebsiteA2.Models;
using Microsoft.AspNetCore.Mvc;


namespace Angular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillPayController : ControllerBase
    {
        private readonly BillPayDataAccessLayer _billPayDataAccessLayer;

        public BillPayController(BillPayDataAccessLayer billPayDataAccessLayer) {
            _billPayDataAccessLayer = billPayDataAccessLayer;
        }

        [HttpGet]
        [Route("Index")]
        public IEnumerable<BillPay> Get()
        {
            return _billPayDataAccessLayer.GetAllBillPays();
        }

        [HttpPut]
        [Route("BlockOrUnblock")]
        public int BlockOrUnblock([FromBody] int billPayID) {
            var billPay = _billPayDataAccessLayer.getBillPay(billPayID);
            billPay.BlockOrUnBlock();
            return _billPayDataAccessLayer.UpdateBillPay(billPay);
        }
    }
}
