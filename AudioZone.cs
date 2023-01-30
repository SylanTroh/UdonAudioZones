using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace AudioZones
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class AudioZone : UdonSharpBehaviour
    {
        public const float defaultVoiceGain = 15.0f;
        public const float defaultVoiceRangeNear = 0.0f;
        public const float defaultVoiceRangeFar = 15.0f;
        public const float defaultVoiceVolumetricRadius = 0.0f;
        public const bool defaultVoiceLowpass = true;

        public float voiceGain = defaultVoiceGain;
        public float voiceRangeFar = defaultVoiceRangeFar;

        public string zoneName;
        public ZoneManager zoneManager;

        void Start()
        {
            SetAudio();
        }
        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            if (player != Networking.LocalPlayer) {
                SetAudioSingle(player);
            }
        }
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (zoneManager.GetZone(player) != null) return;

            zoneManager.SetZone(player,zoneName);

            Debug.Log("Set player " + player.displayName + player.playerId + " to " + zoneName);
            zoneManager.SetZone(player, zoneName);

            SetAudio(player);
        }
        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            if (zoneManager.GetZone(player) == null) return;

            zoneManager.SetZone(player, null);

            Debug.Log("Set player " + player.displayName + player.playerId + " to default zone");
            zoneManager.SetZone(player, null);

            SetAudio(player);
        }
        public void SetAudio()
        {
            zoneManager.UpdatePlayerList();
            foreach (VRCPlayerApi p in zoneManager.PlayerList)
            {
                if (p == null || p == Networking.LocalPlayer) continue;
                if (zoneManager.GetZone(p) == zoneManager.localZone)
                {
                    SetPlayerVoiceModeDefault(p);
                }
                else if (zoneManager.localZone != null)
                {
                    SetPlayerVoiceModeWhisper(p);
                }
                else
                {
                    SetPlayerVoiceModeWhisper(p);
                }
            }
        }
        public void SetAudio(VRCPlayerApi player)
        {
            if (player != Networking.LocalPlayer)
            {
                SetAudioSingle(player);
                return;
            }
            zoneManager.UpdatePlayerList();
            foreach (VRCPlayerApi p in zoneManager.PlayerList)
            {
                if (p == null || p == Networking.LocalPlayer) continue;
                if (zoneManager.GetZone(p) == zoneManager.localZone)
                {
                    SetPlayerVoiceModeDefault(p);
                }
                else if (zoneManager.localZone != null)
                { 
                    SetPlayerVoiceModeWhisper(p);
                }
                else 
                {
                    SetPlayerVoiceModeWhisper(p);
                }
            }
        }
        public void SetAudioSingle(VRCPlayerApi p)
        {
            if (p == null || p == Networking.LocalPlayer) return;
            if (zoneManager.GetZone(p) == zoneManager.localZone)
            {
                SetPlayerVoiceModeDefault(p);            
            }
            else if (zoneManager.localZone != null)
            {
                SetPlayerVoiceModeWhisper(p);
            }
            else
            {
                SetPlayerVoiceModeWhisper(p);
            }
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
        public void SetPlayerVoiceModeDefault(VRCPlayerApi player)
        {
            SetPlayerVoice(player, defaultVoiceGain, defaultVoiceRangeFar, defaultVoiceRangeNear, defaultVoiceVolumetricRadius, defaultVoiceLowpass);
        }
        public void SetPlayerVoiceModeWhisper(VRCPlayerApi player)
        {
            SetPlayerVoice(player, voiceGain, voiceRangeFar, defaultVoiceRangeNear, defaultVoiceVolumetricRadius, false);
        }
    }
}
