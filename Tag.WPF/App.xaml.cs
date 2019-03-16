using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tag.Setting;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;

namespace Tag.WPF
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Application : System.Windows.Application
    {
        public static Notifier notifier = new Notifier(cfg =>
         {
             cfg.PositionProvider = new WindowPositionProvider(
                 parentWindow: Application.Current.MainWindow,
                 corner: Corner.BottomRight,
                 offsetX: 10,
                 offsetY: 20);

             cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                 notificationLifetime: TimeSpan.FromSeconds(3),
                 maximumNotificationCount: MaximumNotificationCount.FromCount(5));

             cfg.Dispatcher = Application.Current.Dispatcher;
         });

        public Application()
        {
            Global.Load();
        }
        
    }
}
