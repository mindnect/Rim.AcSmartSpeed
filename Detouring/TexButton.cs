using UnityEngine;
using Verse;

namespace AlcoholV.Detouring
{
    [StaticConstructorOnStartup]
    internal class TexButton
    {
        public static readonly Texture2D[] _SpeedButtonTextures =
        {
            ContentFinder<Texture2D>.Get("UI/TimeControls/TimeSpeedButton_Pause", true),
            ContentFinder<Texture2D>.Get("UI/TimeControls/TimeSpeedButton_Normal", true),
            ContentFinder<Texture2D>.Get("UI/TimeControls/TimeSpeedButton_Fast", true),
            ContentFinder<Texture2D>.Get("UI/TimeControls/TimeSpeedButton_Superfast", true),
            ContentFinder<Texture2D>.Get("UI/TimeControls/TimeSpeedButton_Ultrafast", true)
        };
    }
}