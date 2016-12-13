using System.Linq;
using System.Reflection;
using AlcoholV.Detouring;
using Verse;
using TickManager = Verse.TickManager;
using TimeControls = RimWorld.TimeControls;

namespace AlcoholV
{
    [StaticConstructorOnStartup]
    public class AcSmartSpeed
    {
        public enum Option
        {
            Slow,
            Normal,
            Fast,
            Half,
            Ignore
        }

        public static Option currSetting = Option.Normal;

        //public static SettingHandle<Option> CurrSetting { get; private set; }

        static AcSmartSpeed()
        {
            InitUltraFastMode();
            InitEventSpeedControl();
            Log.Message(AssemblyName + " injected.");
        }

        private static Assembly Assembly => Assembly.GetAssembly(typeof(AcSmartSpeed));
        private static string AssemblyName => Assembly.FullName.Split(',').First();
        public string ModIdentifier => AssemblyName;

        //public void DefsLoaded()
        //{
        //    LongEventHandler.ExecuteWhenFinished(PrepareSettingsHandles);
        //}

        //private void PrepareSettingsHandles()
        //{
        //    CurrSetting = Settings.GetHandle("EventSpeed", "EventSpeed".Translate(), "SetEventSpeed".Translate(), Option.Normal, null, "AcSmartSpeed");
        //}

        private static void InitUltraFastMode()
        {
            var source = typeof(TimeControls).GetMethod("DoTimeControlsGUI", BindingFlags.Static | BindingFlags.Public);
            var dest = typeof(Detouring.TimeControls).GetMethod("DoTimeControlsGUI", BindingFlags.Static | BindingFlags.Public);
            Detour.TryDetourFromTo(source, dest);
        }

        private static void InitEventSpeedControl()
        {
            var source = typeof(TickManager).GetProperty("TickRateMultiplier", BindingFlags.Instance | BindingFlags.Public).GetGetMethod();
            var dest = typeof(Detouring.TickManager).GetProperty("TickRateMultiplier", BindingFlags.Instance | BindingFlags.Public).GetGetMethod();
            Detour.TryDetourFromTo(source, dest);


            source = typeof(Detouring.TickManager).GetMethod("NothingHappeningInGame", BindingFlags.Instance | BindingFlags.NonPublic);
            dest = typeof(TickManager).GetMethod("NothingHappeningInGame", BindingFlags.Instance | BindingFlags.NonPublic);
            Detour.TryDetourFromTo(source, dest);
        }
    }
}