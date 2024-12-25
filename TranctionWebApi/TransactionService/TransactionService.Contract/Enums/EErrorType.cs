using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionService.Contract.Enums
{
    public enum EErrorType
    {
        BadRequest,
        InternalServerError,
        Conflict,
        NotFound
    }
}