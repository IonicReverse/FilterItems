using robotManager.Helpful;
using System.Collections.Generic;
using System.Linq;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace FilterItems
{
  public class Helpers
  {
    public static void DestroyItem()
    {
      var bagItems = Bag.GetBagItem().Where(x => FilterItemSettings.CurrentSetting.DestroyItemList.Contains(x.Entry)).FirstOrDefault();

      if (bagItems != null && !ObjectManager.Me.IsDeadMe && !ObjectManager.Me.InCombat)
      {
        List<int> itemContainerBagIdAndSlot = Bag.GetItemContainerBagIdAndSlot(bagItems.Entry);
        Lua.LuaDoString(string.Format("PickupContainerItem({0}, {1})", itemContainerBagIdAndSlot[0], itemContainerBagIdAndSlot[1]));
        Lua.LuaDoString("DeleteCursorItem()");
      }
    }

    public static void Logger(string message)
    {
      Logging.Write("[FilterItem] " + message, Logging.LogType.Normal, System.Drawing.Color.ForestGreen);
    }
  }
}
