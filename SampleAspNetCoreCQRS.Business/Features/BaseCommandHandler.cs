using System;

namespace SampleAspNetCoreCQRS.Business.Features
{
    public abstract class BaseCommandHandler
    {

        protected readonly IContext Context;
        
        public BaseCommandHandler(IContext context)
        {
            Context = context;
        }
    }
}
