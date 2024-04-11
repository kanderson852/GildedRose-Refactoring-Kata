using System.Collections.Generic;

namespace GildedRoseKata
{
    public class GildedRose
    {
        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

         public static void UpdateItem(Item item) {
            if(item.Quality > 0)
            {
                item.Quality--;
            }
            item.SellIn--;
            if(item.SellIn < 0 && item.Quality > 0)
            {
                item.Quality--;
            }

        }

        public static void UpdateAgedBrie(Item item) {
           if (item.Quality < 50)
            {
                item.Quality++;
            }
            if (item.SellIn < 0 && item.Quality < 50)
            {
                item.Quality++;
            }
            item.SellIn--;
        }

         public static void UpdateSulfuras(Item item) {
            item.Quality = 80;
         }

        public static void UpdateBackstagePasses(Item item) {
            if (item.Quality < 50)
            {
                if (item.SellIn < 11)
                {
                    item.Quality++;
                }
                if (item.SellIn < 6)
                {
                    item.Quality++;
                }
                item.Quality++;
            }
            if(item.SellIn <= 0)
            {
                item.Quality = 0;
            }
            item.SellIn--;
        }

        public static void UpdateConjured(Item item) {
            if (item.Quality > 0)
            {
                item.Quality -= 2;
            }
            if (item.SellIn <= 0 && item.Quality > 0){
                item.Quality -= 2;
            }
            item.SellIn--;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if (item.Name == "Aged Brie") UpdateAgedBrie(item);
                else if (item.Name == "Backstage passes to a TAFKAL80ETC concert") UpdateBackstagePasses(item);
                else if (item.Name == "Sulfuras, Hand of Ragnaros") UpdateSulfuras(item);
                else if (item.Name == "Conjured") UpdateConjured(item);
                else UpdateItem(item);
            }
        }
    }
}
