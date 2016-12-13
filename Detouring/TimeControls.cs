using System;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace AlcoholV.Detouring
{
    public static class TimeControls
    {
        private static readonly string[] SpeedSounds =
        {
            "ClockStop",
            "ClockNormal",
            "ClockFast",
            "ClockSuperfast",
            "ClockSuperfast"
        };

        private static readonly TimeSpeed[] CachedTimeSpeedValues = (TimeSpeed[]) Enum.GetValues(typeof (TimeSpeed));

        private static void PlaySoundOf(TimeSpeed speed)
        {
            SoundDef.Named(SpeedSounds[(int) speed]).PlayOneShotOnCamera();
        }

        public static void DoTimeControlsGUI(Rect timerRect)
        {
            timerRect.x -= 14f;
            var tickManager = Find.TickManager;
            GUI.BeginGroup(timerRect);
            var rect = new Rect(0f, 0f, 28f, 24f);
            for (var i = 0; i < CachedTimeSpeedValues.Length; i++)
            {
                var timeSpeed = CachedTimeSpeedValues[i];

                if (Widgets.ButtonImage(rect, TexButton._SpeedButtonTextures[(int) timeSpeed]))
                {
                    if (timeSpeed == TimeSpeed.Paused)
                    {
                        tickManager.TogglePaused();
                    }
                    else
                    {
                        tickManager.CurTimeSpeed = timeSpeed;
                    }
                    PlaySoundOf(tickManager.CurTimeSpeed);
                    PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.TimeControls, KnowledgeAmount.SpecificInteraction);
                }
                if (tickManager.CurTimeSpeed == timeSpeed)
                {
                    GUI.DrawTexture(rect, TexUI.HighlightTex);
                }
                rect.x += rect.width;
            }

            if (Find.TickManager.slower.ForcedNormalSpeed)
            {
                Widgets.DrawLineHorizontal(rect.width*2f, rect.height/2f, rect.width*3f);
            }
            GUI.EndGroup();
            GenUI.AbsorbClicksInRect(timerRect);
            UIHighlighter.HighlightOpportunity(timerRect, "TimeControls");

            if (Event.current.type == EventType.KeyDown)
            {
                if (KeyBindingDefOf.TogglePause.KeyDownEvent)
                {
                    Find.TickManager.TogglePaused();
                    PlaySoundOf(Find.TickManager.CurTimeSpeed);
                    PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.Pause,
                        KnowledgeAmount.SpecificInteraction);
                    Event.current.Use();
                }
                if (KeyBindingDefOf.TimeSpeedNormal.KeyDownEvent)
                {
                    Find.TickManager.CurTimeSpeed = TimeSpeed.Normal;
                    PlaySoundOf(Find.TickManager.CurTimeSpeed);
                    PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.TimeControls,
                        KnowledgeAmount.SpecificInteraction);
                    Event.current.Use();
                }
                if (KeyBindingDefOf.TimeSpeedFast.KeyDownEvent)
                {
                    Find.TickManager.CurTimeSpeed = TimeSpeed.Fast;
                    PlaySoundOf(Find.TickManager.CurTimeSpeed);
                    PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.TimeControls,
                        KnowledgeAmount.SpecificInteraction);
                    Event.current.Use();
                }
                if (KeyBindingDefOf.TimeSpeedSuperfast.KeyDownEvent)
                {
                    Find.TickManager.CurTimeSpeed = TimeSpeed.Superfast;
                    PlaySoundOf(Find.TickManager.CurTimeSpeed);
                    PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.TimeControls,
                        KnowledgeAmount.SpecificInteraction);
                    Event.current.Use();
                }
                if (KeyBindingDefOf.TimeSpeedUltrafast.KeyDownEvent)
                {
                    Find.TickManager.CurTimeSpeed = TimeSpeed.Ultrafast;
                    PlaySoundOf(Find.TickManager.CurTimeSpeed);
                    Event.current.Use();
                }
                if (Prefs.DevMode)
                {
                    if (KeyBindingDefOf.TickOnce.KeyDownEvent && tickManager.CurTimeSpeed == TimeSpeed.Paused)
                    {
                        tickManager.DoSingleTick();
                        SoundDef.Named(SpeedSounds[0]).PlayOneShotOnCamera();
                    }
                }
            }
        }
    }
}