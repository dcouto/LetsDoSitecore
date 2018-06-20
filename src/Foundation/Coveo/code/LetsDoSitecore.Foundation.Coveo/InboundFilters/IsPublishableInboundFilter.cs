using Coveo.SearchProvider.InboundFilters;
using Coveo.SearchProvider.Pipelines;
using LetsDoSitecore.Foundation.Extensions;

namespace LetsDoSitecore.Foundation.Coveo.InboundFilters
{
    public class IsPublishableInboundFilter : AbstractCoveoInboundFilterProcessor
    {
        public override void Process(CoveoInboundFilterPipelineArgs p_Args)
        {
            if (p_Args.IndexableToIndex != null
                && p_Args.IndexableToIndex.Item != null
                && p_Args.IndexableToIndex.Item.SitecoreItem != null
                && !p_Args.IsExcluded
                && ShouldExecute(p_Args))
            {
                var scItem = p_Args.IndexableToIndex.Item.SitecoreItem;

                p_Args.IsExcluded = !scItem.AllAncestorsAndSelfArePublishable();
            }
        }
    }
}