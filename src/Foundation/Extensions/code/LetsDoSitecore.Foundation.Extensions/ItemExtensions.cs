using Sitecore.Data.Items;

namespace LetsDoSitecore.Foundation.Extensions
{
    public static class ItemExtensions
    {
        public static bool AllAncestorsAndSelfArePublishable(this Item item)
        {
            if (item.Publishing.NeverPublish || (item.Parent != null && item.Parent.Publishing.NeverPublish))
            {
                return false;
            }

            if (item.Parent != null)
            {
                return AllAncestorsAndSelfArePublishable(item.Parent);
            }

            return true;
        }
    }
}