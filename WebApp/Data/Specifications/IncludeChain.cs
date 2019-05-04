using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WebApp.Data.Specifications
{
    public class IncludeChain
    {
        public IncludeChain()
        {
            ThenIncludes = new List<LambdaExpression>();
        }

        public LambdaExpression Include { get; protected set; }
        public List<LambdaExpression> ThenIncludes { get; protected set; }
    }

    public class IncludeChain<IncludeT,ThenIncludeT>:IncludeChain
    {
        public IncludeChain(Expression<Func<IncludeT, IEnumerable<ThenIncludeT>>> include):base()
        {
            Include = include;
        }
        public IncludeChain(Expression<Func<IncludeT, ThenIncludeT>> include) : base() 
        {
            Include = include;
        }


        public IncludeChain<IncludeT,ThenIncludeT> AddThenInclude(Expression<Func<ThenIncludeT, object>> thenInclude)
        {
            ThenIncludes.Add(thenInclude);
            return this;
        }
    }
}
