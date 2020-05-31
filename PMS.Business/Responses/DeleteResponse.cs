using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Business.Responses
{
    public class DeleteResponse
    {
        public bool Perfomed { get; set; }

        public ValidationResponse Error { get; set; }
    }
}
