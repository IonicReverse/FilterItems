using FilterItems;
using robotManager.Helpful;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

public class FilterItemSettings : Settings
{

  [Setting]
  [Category("General")]
  [DisplayName("Auto Destroy Item")]
  [Description("Enable Auto Destroy Item")]
  public bool AutoDestroy { get; set; }

  [Setting]
  [Category("List")]
  [DisplayName("List of Item")]
  [Description("List of item need to destroy")]
  public List<int> DestroyItemList { get; set; }

  private FilterItemSettings()
  {
    AutoDestroy = true;
    DestroyItemList = new List<int>();
  }

  public static FilterItemSettings CurrentSetting { get; set; }
  
  public bool Save()
  {
    try
    {
      return Save(AdviserFilePathAndName("FilterItem", ObjectManager.Me.Name + "." + Usefuls.RealmName));
    }
    catch(Exception ex)
    {
      Helpers.Logger("Save " + ex);
      return false;
    }
  }

  public static bool Load()
  {
    try
    {
      if (File.Exists(AdviserFilePathAndName("FilterItem", ObjectManager.Me.Name + "." + Usefuls.RealmName))) 
      {
        CurrentSetting = Load<FilterItemSettings>(AdviserFilePathAndName("FilterItem", ObjectManager.Me.Name + "." + Usefuls.RealmName));
        return true;
      }
      CurrentSetting = new FilterItemSettings();
    }
    catch(Exception ex)
    {
      Helpers.Logger("Load " + ex);
    }
    return false;
  }
}

