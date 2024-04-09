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

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if (item.Name != "Aged Brie" && item.Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (item.Name == "Conjured" && item.Quality > 0)
                    {
                        item.Quality--;
                    }
                    if (item.Quality > 0 && item.Name != "Sulfuras, Hand of Ragnaros")
                    {
                        item.Quality--;
                    }
                }
                else if (item.Quality < 50)
                {
                    if (item.Name == "Backstage passes to a TAFKAL80ETC concert" && item.SellIn < 11)
                    {
                        item.Quality++;
                    }

                    if (item.Name == "Backstage passes to a TAFKAL80ETC concert" && item.SellIn < 6)
                    {
                        item.Quality++;
                    }
                    item.Quality++;
                }

                if (item.Name != "Sulfuras, Hand of Ragnaros")
                {
                    item.SellIn--;
                }

                if (item.SellIn < 0)
                {
                    if (item.Name != "Aged Brie" )
                    {
            
                        if (item.Quality > 0 && item.Name != "Sulfuras, Hand of Ragnaros" && item.Name != "Backstage passes to a TAFKAL80ETC concert")
                        {
                            item.Quality--;
                            if (item.Name == "Conjured")
                            {
                                item.Quality--;
                            }
                        }
                        else item.Quality -= item.Quality;
                    }
                    else if (item.Quality < 50)
                    {
                        item.Quality++;
                    }
                }
            }
        }
    }
}
