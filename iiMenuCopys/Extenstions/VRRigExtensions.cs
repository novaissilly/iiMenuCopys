using GorillaNetworking;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace iiMenu.Extensions
{
    public static class VRRigExtensions
    {
        public static bool IsSteam(this VRRig rig) =>
            rig.GetPlatform() != "Standalone";

        public static string GetPlatform(this VRRig rig)
        {
            int suspiciouslySteam = 0;
            int suspiciouslyPC = 0;
            int suspiciouslyQuest = 0;
            string concatStringOfCosmeticsAllowed = rig.Cosmetics();

            if (concatStringOfCosmeticsAllowed.Contains("S. FIRST LOGIN"))
                suspiciouslySteam++;

            if (concatStringOfCosmeticsAllowed.Contains("FIRST LOGIN") || rig.GetPhotonPlayer().CustomProperties.Count >= 2)
                suspiciouslyPC++;

            if (suspiciouslySteam > suspiciouslyPC && suspiciouslySteam > suspiciouslyQuest) return "Steam";
            if (suspiciouslyPC > suspiciouslySteam && suspiciouslyPC > suspiciouslyQuest) return "PC";
            if (suspiciouslyQuest > suspiciouslySteam && suspiciouslyQuest > suspiciouslyPC) return "Standalone";

            return "Standalone";
        }

        public static Color GetColor(this VRRig rig)
        {
            switch (rig.setMatIndex)
            {
                case 1:
                    return Color.red;
                case 2:
                case 11:
                    return new Color32(255, 128, 0, 255);
                case 3:
                case 7:
                    return Color.blue;
                case 12:
                    return Color.green;
                default:
                    return rig.playerColor();
            }
        }

        public static Color playerColor(this VRRig rig)
        {
            Color c = new Color(rig.mainSkin.material.color.r, rig.mainSkin.material.color.g, rig.mainSkin.material.color.b);
            return c;
        }

        public static float Distance(this VRRig rig, Vector3 position) =>
            Vector3.Distance(rig.transform.position, position);

        public static float Distance(this VRRig rig, VRRig otherRig) =>
            rig.Distance(otherRig.transform.position);

        public static float Distance(this VRRig rig) =>
            rig.Distance(GorillaTagger.Instance.bodyCollider.transform.position);

        public static string GetName(this VRRig rig) =>
            rig.photonView.Owner?.NickName ?? "null";

        public static Photon.Realtime.Player GetPlayer(this VRRig rig) =>
            rig.photonView.Owner;

        public static Photon.Realtime.Player GetPhotonPlayer(this VRRig rig) =>
            rig.photonView.Owner;

        public static VRRig GetVRRigWithoutMe(this VRRig rig)
        {
            if (rig != null && rig.photonView.Owner.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber && rig != GorillaTagger.Instance.myVRRig)
            {
                return rig;
            }
            return null;
        }

        public static float[] GetSpeed(this VRRig rig)
        {
            Photon.Realtime.Player player = rig.GetPlayer();

            if (player == null || GorillaComputer.instance == null)
                return new[] { 6.5f, 1.1f };

            string mode = GameMode.GameModeString();

            if (mode == "INFECTION" && GorillaGameManager.instance is GorillaTagManager tagManager)
            {
                if (tagManager.isCurrentlyTag)
                {
                    if (player == tagManager.currentIt)
                    {
                        return new[]
                        {
                            tagManager.fastJumpLimit,
                            tagManager.fastJumpMultiplier
                        };
                    }

                    return new[]
                    {
                        tagManager.slowJumpLimit,
                        tagManager.slowJumpMultiplier
                    };
                }

                if (tagManager.currentInfected.Contains(player))
                {
                    return new[]
                    {
                        tagManager.InterpolatedInfectedJumpSpeed(tagManager.currentInfected.Count),
                        tagManager.InterpolatedInfectedJumpMultiplier(tagManager.currentInfected.Count)
                    };
                }

                return new[]
                {
                    tagManager.InterpolatedNoobJumpSpeed(tagManager.currentInfected.Count),
                    tagManager.InterpolatedNoobJumpMultiplier(tagManager.currentInfected.Count)
                };
            }

            return new[] { 6.5f, 1.1f };
        }

        public static float GetMaxSpeed(this VRRig rig) =>
            rig.GetSpeed()[0];

        public static float GetSpeedMultiplier(this VRRig rig) =>
            rig.GetSpeed()[1];

        public static string Cosmetics(this VRRig rig) =>
            string.Concat(rig.concatStringOfCosmeticsAllowed);
    }

    public static class GameMode 
    {
        public static string GameModeString()
        {
            if (GorillaComputer.instance == null)
                return "INFECTION";

            if (GorillaComputer.instance.currentQueue == "CASUAL")
                return "CASUAL";

            if (GorillaComputer.instance.currentQueue == "HUNT")
                return "HUNT";

            return "INFECTION";
        }
    }
}