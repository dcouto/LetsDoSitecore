using Sitecore.FakeDb;
using Xunit;
using LetsDoSitecore.Foundation.Extensions;

namespace LetsDoSitecore.Foundation.Extensions.Tests
{
    public class ItemExtensionsTests
    {
        [Fact]
        public void When_ItemIsHomeAndIsPublishable_Expect_AllAncestorsAndSelfToBeTrue()
        {
            var expected = true;

            using(var db = new Db
            {
                new DbItem("Home")
            })
            {
                var home = db.GetItem("/sitecore/content/home");

                var actual = home.AllAncestorsAndSelfArePublishable();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void When_ItemIsChildOfHomeIsPublishableAndHomeIsPublishable_Expect_AllAncestorsAndSelfToBeTrue()
        {
            var expected = true;

            using(var db = new Db
            {
                new DbItem("Home")
                {
                    new DbItem("Sample Item 1")
                }
            })
            {
                var sampleItem1 = db.GetItem("/sitecore/content/home/sample item 1");

                var actual = sampleItem1.AllAncestorsAndSelfArePublishable();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void When_ItemIsDescendantOfHomeIsPublishableAndAllAncestorsArePublishable_Expect_AllAncestorsAndSelfToBeTrue()
        {
            var expected = true;

            using(var db = new Db
            {
                new DbItem("Home")
                {
                    new DbItem("Sample Item 1")
                    {
                        new DbItem("Sample Descendant 1")
                        {
                            new DbItem("Sample Descendant 2")
                        }
                    }
                }
            })
            {
                var sampleDescendant2 = db.GetItem("/sitecore/content/home/sample item 1/sample descendant 1/sample descendant 2");

                var actual = sampleDescendant2.AllAncestorsAndSelfArePublishable();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void When_ItemIsHomeAndIsNotPublishable_Expect_AllAncestorsAndSelfToBeFalse()
        {
            var expected = false;

            using (var db = new Db
            {
                new DbItem("Home")
                {
                    { "__Never publish", "1" }
                }
            })
            {
                var home = db.GetItem("/sitecore/content/home");

                var actual = home.AllAncestorsAndSelfArePublishable();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void When_ItemIsChildOfHomeIsPublishableButHomeIsNotPublishable_Expect_AllAncestorsAndSelfToBeFalse()
        {
            var expected = false;

            using (var db = new Db
            {
                new DbItem("Home")
                {
                    { "__Never publish", "1" },

                    new DbItem("Sample Item 1")
                }
            })
            {
                var sampleItem1 = db.GetItem("/sitecore/content/home/sample item 1");

                var actual = sampleItem1.AllAncestorsAndSelfArePublishable();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void When_ItemIsChildOfHomeIsNotPublishableButHomeIsPublishable_Expect_AllAncestorsAndSelfToBeFalse()
        {
            var expected = false;

            using (var db = new Db
            {
                new DbItem("Home")
                {
                    new DbItem("Sample Item 1")
                    {
                        { "__Never publish", "1" },
                    }
                }
            })
            {
                var sampleItem1 = db.GetItem("/sitecore/content/home/sample item 1");

                var actual = sampleItem1.AllAncestorsAndSelfArePublishable();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void When_ItemIsChildOfHomeIsNotPublishableAndHomeIsNotPublishable_Expect_AllAncestorsAndSelfToBeFalse()
        {
            var expected = false;

            using (var db = new Db
            {
                new DbItem("Home")
                {
                    { "__Never publish", "1" },

                    new DbItem("Sample Item 1")
                    {
                        { "__Never publish", "1" },
                    }
                }
            })
            {
                var sampleItem1 = db.GetItem("/sitecore/content/home/sample item 1");

                var actual = sampleItem1.AllAncestorsAndSelfArePublishable();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void When_ItemIsDesendantOfHomeIsPublishableButParentIsNotPublishable_Expect_AllAncestorsAndSelfToBeFalse()
        {
            var expected = false;

            using (var db = new Db
            {
                new DbItem("Home")
                {
                    new DbItem("Sample Item 1")
                    {
                        { "__Never publish", "1" },

                        new DbItem("Sample Descendant 1")
                    }
                }
            })
            {
                var sampleDescendant1 = db.GetItem("/sitecore/content/home/sample item 1/sample descendant 1");

                var actual = sampleDescendant1.AllAncestorsAndSelfArePublishable();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void When_ItemIsDescendantOfHomeIsPublishableButHomeIsNotPublishable_Expect_AllAncestorsAndSelfToBeFalse()
        {
            var expected = false;

            using (var db = new Db
            {
                new DbItem("Home")
                {
                    { "__Never publish", "1" },

                    new DbItem("Sample Item 1")
                    {
                        new DbItem("Sample Descendant 1")
                    }
                }
            })
            {
                var sampleDescendant1 = db.GetItem("/sitecore/content/home/sample item 1/sample descendant 1");

                var actual = sampleDescendant1.AllAncestorsAndSelfArePublishable();

                Assert.Equal(expected, actual);
            }
        }
    }
}
