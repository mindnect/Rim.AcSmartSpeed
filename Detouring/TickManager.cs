using System;
using System.Linq;
using RimWorld;
using Verse;

namespace AlcoholV.Detouring
{
    internal class TickManager
    {
        public float TickRateMultiplier
        {
            get
            {
                var slower = Find.TickManager.slower;
                var currTimeSpeed = Find.TickManager.CurTimeSpeed;

                if (slower.ForcedNormalSpeed)
                {
                    if (currTimeSpeed == TimeSpeed.Paused)
                    {
                        return 0f;
                    }
                    switch (AcSmartSpeed.currSetting)
                    {
                        case AcSmartSpeed.Option.Slow:
                            return 0.5f;
                        case AcSmartSpeed.Option.Normal:
                            return 1f;
                        case AcSmartSpeed.Option.Fast:
                            return 2f;
                        case AcSmartSpeed.Option.Half:
                            return speed(currTimeSpeed)/2f;
                        case AcSmartSpeed.Option.Ignore:
                            return speed(currTimeSpeed);
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                return speed(currTimeSpeed);
            }
        }

        private float speed(TimeSpeed currTimeSpeed)
        {
            switch (currTimeSpeed)
            {
                case TimeSpeed.Paused:
                    return 0f;
                case TimeSpeed.Normal:
                    return 1f;
                case TimeSpeed.Fast:
                    return 3f;
                case TimeSpeed.Superfast:
                    if (NothingHappeningInGame())
                    {
                        return 12f;
                    }
                    return 6f;
                case TimeSpeed.Ultrafast:
                    return 15f;
                default:
                    return -1f;
            }
        }

        // Detour용 임시 함수
        private bool NothingHappeningInGame()
        {
            Log.Message("This message never show up");
            return true;
        }
    }


}