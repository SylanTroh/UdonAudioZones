
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace AudioZones
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class MicrophoneZone : UdonSharpBehaviour
    {
        public const float defaultVoiceGain = 15.0f;
        public const float defaultVoiceRangeNear = 0.0f;
        public const float defaultVoiceRangeFar = 25.0f;
        public const float defaultVoiceVolumetricRadius = 0.0f;
        public const bool defaultVoiceLowpass = true;

        public float VoiceGain = 15.0f;
        public float VoiceRangeFar = 25.0f;

        private VRCPlayerApi localPlayer;

        void Start()
        {
            localPlayer = Networking.LocalPlayer;
        }
        public override void OnPlayerTriggerEnter(VRCPlayerApi p)
        {
            if (p != localPlayer) SetPlayerVoiceModeMicrophone(p);
        }
        public override void OnPlayerTriggerExit(VRCPlayerApi p)
        {
            if (p != localPlayer) SetPlayerVoiceModeDefault(p);


        }
        public void SetPlayerVoice(VRCPlayerApi player, float voiceGain, float voiceFar, float voiceNear, float voiceVolumetricRadius, bool voiceLowpass)
        {
            if (player.IsValid())
            {
                player.SetVoiceGain(voiceGain);
                player.SetVoiceDistanceFar(voiceFar);
                player.SetVoiceDistanceNear(voiceNear);
                player.SetVoiceVolumetricRadius(voiceVolumetricRadius);
                player.SetVoiceLowpass(!voiceLowpass);
            }
        }
        public void SetPlayerVoiceModeDefault(VRCPlayerApi p)
        {
            //Debug.Log("Setting player " + p.displayName + p.playerId + " to default audio");
            SetPlayerVoice(p, defaultVoiceGain, defaultVoiceRangeFar, defaultVoiceRangeNear, defaultVoiceVolumetricRadius, defaultVoiceLowpass);
        }
        public void SetPlayerVoiceModeMicrophone(VRCPlayerApi p)
        {
            //Debug.Log("Setting player " + p.displayName + p.playerId + " to whisper");
            SetPlayerVoice(p, VoiceGain, VoiceRangeFar, defaultVoiceRangeNear, defaultVoiceVolumetricRadius, false);
        }
    }
}
