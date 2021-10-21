using FilterItems;
using MarsSettingsGUI;
using System;
using System.ComponentModel;
using System.Threading;
using wManager.Plugin;

public class Main : IPlugin
{
  public static bool isLaunched;
  private BackgroundWorker pulseThread = new BackgroundWorker();

  public void Dispose()
  {
    isLaunched = false;
    Helpers.Logger("Stopped");
  }

  public void Initialize()
  {
    Helpers.Logger("Started");
    FilterItemSettings.Load();
    isLaunched = true;
    if (!pulseThread.IsBusy)
    {
      pulseThread.DoWork += Pulse;
      pulseThread.RunWorkerAsync();
    }
  }

  public void Settings()
  {
    FilterItemSettings.Load();
    new SettingsWindow(FilterItemSettings.CurrentSetting).ShowDialog();
    FilterItemSettings.CurrentSetting.Save();
  }

  public static void Pulse(object sender, DoWorkEventArgs args)
  {
    try
    {
      while (isLaunched)
      {
        Helpers.DestroyItem();
        Thread.Sleep(1000);
      }
    }
    catch(Exception ex)
    {
      Helpers.Logger("Something wrong ..." + ex);
    }
  }

}
