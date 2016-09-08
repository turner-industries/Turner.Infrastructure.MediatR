using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turner.Infrastructure.MediatR.Decorators
{
    public class DoNotLog : Attribute { }

    public class DoNotCommit : Attribute { }

    public class DoNotValidate : Attribute { }

    public class DoNotRegisterWithIoc : Attribute { }
}
