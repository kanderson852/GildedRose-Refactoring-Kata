using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests
{
    public class GildedRoseTest
    {
        [Fact]
        // GildedRose Object and its attributes initialize properly
        public void GildedRoseCanCreate()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 5, Quality = 20 } };
            GildedRose app = new GildedRose(Items);

            Assert.Equal("foo", Items[0].Name);
            Assert.Equal(5, Items[0].SellIn);
            Assert.Equal(20, Items[0].Quality);
        }

        [Fact]
        //UpdateQuality method reduces SellIn and Quality by 1 for a regular item before the sell-by date
        public void UpdateQuality_Should_ReduceQualityAndSellIn()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 5, Quality = 20 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.Equal(4, Items[0].SellIn);
            Assert.Equal(19, Items[0].Quality);
        }

        [Fact]
        //Once the sell by date has passed, UpdateQuality method reduces Quality twice as fast
        public void UpdateQuality_Should_ReduceTwiceAsFast_When_SellByDateHasPassed()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = -1, Quality = 20 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.Equal(18, Items[0].Quality);
        }
        
        [Fact]
        //The Quality of an item is never negative
        public void UpdateQuality_Should_NotReduceQuality_When_ItHasReachedZero()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 5, Quality = 0 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.Equal(0, Items[0].Quality);
        }

        [Fact]
        //Aged Brie actually increases in Quality the older it gets
        public void UpdateQuality_Should_IncreaseQuality_When_ItemIsAgedBrie()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 5, Quality = 20 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.Equal(21, Items[0].Quality);
        }

        [Fact]
        //The Quality of an item is never more than 50
        public void UpdateQuality_Should_NotIncreaseQuality_When_ItReachesFifty()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 5, Quality = 50 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.Equal(50, Items[0].Quality);
        }

        [Fact]
        //"Sulfuras", being a legendary item, never has to be sold or decreases in Quality
        public void UpdateQuality_Should_NotChangeQualityOrSellIn_When_TheItemIsSulfuras()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 5, Quality = 20 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.Equal(5, Items[0].SellIn);
            Assert.Equal(20, Items[0].Quality);
        }

        [Fact]
        //"Sulfuras" quality can be set above 50, but will not change 
        public void UpdateQuality_Should_AllowQualityAboveFifty_When_TheItemIsSulfuras()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 5, Quality = 80 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.Equal(5, Items[0].SellIn);
            Assert.Equal(80, Items[0].Quality);
        }

        [Fact]
        //"Backstage passes" increase in Quality as its SellIn value approaches, at a regular rate 
        //when SellIn is greater than 10 days 
        public void UpdateQuality_Should_IncreaseQuality_When_ItemNameIsBackstagePasses_And_SellInGreaterThanTen()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 20, Quality = 20 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.Equal(21, Items[0].Quality);
        }

       [Theory]
       [InlineData(10)]
       [InlineData(6)]
        //"Backstage passes" increase in Quality by 2 when SellIn is less than or equal to 10, but greater than 5
        public void UpdateQuality_Should_IncreaseQualityByTwo_When_ItemNameIsBackstagePasses_And_SellInLessThanOrEqualToTenButGreaterThanFive(int SellIn)
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = SellIn, Quality = 20 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.Equal(22, Items[0].Quality);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(1)]
        //"Backstage passes" increase in Quality by 3 when SellIn is less than or equal to 5, but greater than 0
        public void UpdateQuality_Should_IncreaseQualityByThree_When_ItemNameIsBackstagePasses_And_SellInLessThanOrEqualToFiveButGreaterThanZero(int SellIn)
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = SellIn, Quality = 20 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.Equal(23, Items[0].Quality);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        //"Backstage passes" quality drops to zero once the SellIn date has passed
        public void UpdateQuality_Should_ReduceQualityToZero_When_SellInReahesZeroOrBelow(int SellIn)
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = SellIn, Quality = 20 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.Equal(0, Items[0].Quality);
        }

        [Fact]
        //"Conjured" items degrade in Quality twice as fast as normal items
        public void UpdateQuality_Should_ReduceQualityTwiceAsFast_When_ItemIsNamedConjured()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Conjured", SellIn = 5, Quality = 20 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.Equal(18, Items[0].Quality);
        }

        [Fact]
        //"Conjured" items degrade in Quality twice as fast as normal items, including after expiring
        public void UpdateQuality_Should_ReduceQualityTwiceAsFast_When_ItemIsNamedConjuredAndSellInIsPast()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Conjured", SellIn = -1, Quality = 20 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.Equal(16, Items[0].Quality);
        }
    }
}
