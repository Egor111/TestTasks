using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionService.Domain.WebApi.Response
{
    public class TransactionResponse
    {
        public decimal ClientBalance { get; set; }

        public DateTime InsertDateTime { get; set; }
    }
}
