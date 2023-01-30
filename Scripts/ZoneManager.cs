
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace AudioZones
{
    public class ZoneManager : UdonSharpBehaviour
    {
        const string inZone = "ZoneID";
        public VRCPlayerApi[] PlayerList = new VRCPlayerApi[80];
        [HideInInspector] public string localZone;

        private void Start()
        {
            localZone = null;
            Networking.LocalPlayer.SetPlayerTag(inZone, null);
        }
        public void SetZone(VRCPlayerApi player, string zoneName)
        {
            if (player == Networking.LocalPlayer)
            {
                SetLocalZone(zoneName);
            }
            else
            {
                player.SetPlayerTag(inZone, zoneName);
            }
        }

        public string GetZone(VRCPlayerApi player)
        {
            if (player == Networking.LocalPlayer)
            {
                return GetLocalZone();
            }
            else
            {
                return player.GetPlayerTag(inZone);
            }
        }
        public void SetLocalZone(string zoneName)
        {
            localZone = zoneName;
        }

        public string GetLocalZone()
        {
            return localZone;
        }
        public void UpdatePlayerList()
        {
            PlayerList = VRCPlayerApi.GetPlayers(PlayerList);
        }
        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            player.SetPlayerTag(inZone, null);
        }
    }
}
