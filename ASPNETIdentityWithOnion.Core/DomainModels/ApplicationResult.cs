using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETIdentityWithOnion.Core.DomainModels
{
    public class ApplicationResult
    {
        public IEnumerable<string> Errors
        {
            get;
            private set;
        }

        public bool Succeeded
        {
            get;
            private set;
        }

        public ApplicationResult(IEnumerable<string> errors, bool succeeded)
        {
            Succeeded = succeeded;
            Errors = errors;
        }
    }
}
