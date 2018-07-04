using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotData 
{
    public int Count { get; private set; }
   public Item CurrentItem { get; private set; }

   public int ItemAmount { get; private set; }

   public SlotData(int count)
   {
       this.Count = count;
   }
   public void SetSlot(Item cItem, int amount)
   {
       this.CurrentItem = cItem;
       this.ItemAmount = amount;
   } 
}
