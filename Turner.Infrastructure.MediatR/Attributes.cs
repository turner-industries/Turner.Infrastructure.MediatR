using System;

namespace Turner.Infrastructure.MediatR
{
    public class DoNotLog : Attribute { }

    public class DoNotCommit : Attribute { }

    public class DoNotValidate : Attribute { }

    public class DoNotRegisterWithIoc : Attribute { }
}