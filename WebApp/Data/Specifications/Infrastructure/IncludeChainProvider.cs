using System.Collections.Generic;

namespace WebApp.Data.Specifications.Infrastructure
{
    public interface IIncludeChainProvider
    {
        IIncluder GetIncluder();
    }

    class IncludeChainProvider : IIncludeChainProvider
    {
        private List<BaseIncluder> includersChain;
        private IIncluder chainStart;

        public IncludeChainProvider()
        {
            includersChain = new List<BaseIncluder>();
        }

        public IIncluder GetIncluder()
        {
            if(chainStart == null)
            {
                for (int i = 0; i < includersChain.Count-1; i++)
                {
                    includersChain[i].SetNext(includersChain[i + 1]);
                }
                includersChain[includersChain.Count - 1].SetNext(null);
                chainStart = includersChain[0];
            }

            return chainStart;
        }

        public IncludeChainProvider AddChain(BaseIncluder chain)
        {
            includersChain.Add(chain);
            return this;
        }
    }
}