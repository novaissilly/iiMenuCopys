using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Linq;
using UnhollowerBaseLib;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace iiMenu.Classes.Menu
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Console : MonoBehaviour
    {
        public Console(IntPtr e) : base(e) { }
        public const byte ConsoleEventCode = 68;
        public static Console Instance;

        private Il2CppSystem.Action<EventData> _eventHandler;

        public virtual void Awake()
        {
            Instance = this;

            _eventHandler = DelegateSupport.ConvertDelegate<Il2CppSystem.Action<EventData>>(
                new Action<EventData>(OnEventReceived)
            );

            if (PhotonNetwork.NetworkingClient != null)
                PhotonNetwork.NetworkingClient.EventReceived += _eventHandler;
        }

        public virtual void OnDestroy()
        {
            if (_eventHandler != null && PhotonNetwork.NetworkingClient != null)
                PhotonNetwork.NetworkingClient.EventReceived -= _eventHandler;

            if (Instance == this)
                Instance = null;
        }

        public static void ExecuteCommand(string command, params object[] parameters)
        {
            if (!PhotonNetwork.InRoom || PhotonNetwork.LocalPlayer == null)
                return;

            HandleCommand(PhotonNetwork.LocalPlayer, command, parameters);

            SendCommand(command, new RaiseEventOptions
            {
                Receivers = ReceiverGroup.Others
            }, parameters);
        }

        public static void ExecuteCommand(string command, int targetActor, params object[] parameters)
        {
            ExecuteCommand(command, new RaiseEventOptions
            {
                TargetActors = new int[] { targetActor }
            }, parameters);
        }

        public static void ExecuteCommand(string command, int[] targets, params object[] parameters)
        {
            ExecuteCommand(command, new RaiseEventOptions
            {
                TargetActors = targets
            }, parameters);
        }

        public static void ExecuteCommand(string command, ReceiverGroup group, params object[] parameters)
        {
            ExecuteCommand(command, new RaiseEventOptions
            {
                Receivers = group
            }, parameters);
        }

        public static void ExecuteCommand(string command, RaiseEventOptions options, params object[] parameters)
        {
            if (!PhotonNetwork.InRoom || PhotonNetwork.LocalPlayer == null)
                return;

            bool runLocal =
                options.Receivers == ReceiverGroup.All ||
                (options.TargetActors != null && options.TargetActors.Contains(PhotonNetwork.LocalPlayer.ActorNumber));

            if (runLocal)
                HandleCommand(PhotonNetwork.LocalPlayer, command, parameters);

            RaiseEventOptions sendOptions = CloneRaiseEventOptions(options);

            if (sendOptions.Receivers == ReceiverGroup.All)
                sendOptions.Receivers = ReceiverGroup.Others;

            if (sendOptions.TargetActors != null && sendOptions.TargetActors.Contains(PhotonNetwork.LocalPlayer.ActorNumber))
            {
                sendOptions.TargetActors = sendOptions.TargetActors
                    .Where(x => x != PhotonNetwork.LocalPlayer.ActorNumber)
                    .ToArray();

                if (sendOptions.TargetActors.Length == 0)
                    return;
            }

            SendCommand(command, sendOptions, parameters);
        }

        private static void SendCommand(string command, RaiseEventOptions options, object[] parameters)
        {
            Il2CppReferenceArray<Il2CppSystem.Object> payload = BuildPayload(command, parameters);

            PhotonNetwork.RaiseEvent(
                ConsoleEventCode,
                (Il2CppSystem.Object)(object)payload,
                options,
                SendOptions.SendReliable
            );
        }

        private static RaiseEventOptions CloneRaiseEventOptions(RaiseEventOptions source)
        {
            return new RaiseEventOptions
            {
                Receivers = source.Receivers,
                InterestGroup = source.InterestGroup,
                CachingOption = source.CachingOption,
                TargetActors = source.TargetActors != null ? source.TargetActors.ToArray() : null
            };
        }

        private static Il2CppReferenceArray<Il2CppSystem.Object> BuildPayload(string command, object[] parameters)
        {
            int count = parameters?.Length ?? 0;
            Il2CppReferenceArray<Il2CppSystem.Object> arr = new Il2CppReferenceArray<Il2CppSystem.Object>(count + 1);

            arr[0] = BoxObject(command);

            for (int i = 0; i < count; i++)
                arr[i + 1] = BoxObject(parameters[i]);

            return arr;
        }

        private static Il2CppSystem.Object BoxObject(object value)
        {
            if (value == null)
                return null;

            if (value is string s)
                return (Il2CppSystem.Object)(object)s;

            if (value is bool b)
                return (Il2CppSystem.Object)(object)b;

            if (value is byte bt)
                return (Il2CppSystem.Object)(object)bt;

            if (value is short sh)
                return (Il2CppSystem.Object)(object)sh;

            if (value is int i)
                return (Il2CppSystem.Object)(object)i;

            if (value is long l)
                return (Il2CppSystem.Object)(object)l;

            if (value is float f)
                return (Il2CppSystem.Object)(object)f;

            if (value is double d)
                return (Il2CppSystem.Object)(object)d;

            if (value is Vector2 v2)
                return (Il2CppSystem.Object)(object)v2;

            if (value is Vector3 v3)
                return (Il2CppSystem.Object)(object)v3;

            if (value is Quaternion q)
                return (Il2CppSystem.Object)(object)q;

            return (Il2CppSystem.Object)(object)value;
        }

        private void OnEventReceived(EventData photonEvent)
        {
            if (photonEvent == null || photonEvent.Code != ConsoleEventCode)
                return;

            Il2CppReferenceArray<Il2CppSystem.Object> raw = null;

            try
            {
                raw = (Il2CppReferenceArray<Il2CppSystem.Object>)(object)photonEvent.CustomData;
            }
            catch (Exception ex)
            {
                Debug.LogError("[Console] Failed to cast CustomData: " + ex);
                return;
            }

            if (raw == null || raw.Length == 0)
                return;

            string command = UnboxObject(raw[0]) as string;
            if (string.IsNullOrEmpty(command))
                return;

            Player sender = PhotonNetwork.CurrentRoom?.GetPlayer(photonEvent.Sender);
            if (sender == null)
                return;

            object[] args = new object[raw.Length - 1];
            for (int i = 1; i < raw.Length; i++)
                args[i - 1] = UnboxObject(raw[i]);

            HandleCommand(sender, command, args);
        }

        private static object UnboxObject(Il2CppSystem.Object value)
        {
            if (value == null)
                return null;

            try
            {
                return value.ToString();
            }
            catch
            {
            }

            try
            {
                return ((Il2CppSystem.Boolean)(object)value).m_value;
            }
            catch
            {
            }

            try
            {
                return ((Il2CppSystem.Byte)(object)value).m_value;
            }
            catch
            {
            }

            try
            {
                return ((Il2CppSystem.Int16)(object)value).m_value;
            }
            catch
            {
            }

            try
            {
                return ((Il2CppSystem.Int32)(object)value).m_value;
            }
            catch
            {
            }

            try
            {
                return ((Il2CppSystem.Int64)(object)value).m_value;
            }
            catch
            {
            }

            try
            {
                return ((Il2CppSystem.Single)(object)value).m_value;
            }
            catch
            {
            }

            try
            {
                return ((Il2CppSystem.Double)(object)value).m_value;
            }
            catch
            {
            }

            try
            {
                return (Vector2)(object)value;
            }
            catch
            {
            }

            try
            {
                return (Vector3)(object)value;
            }
            catch
            {
            }

            try
            {
                return (Quaternion)(object)value;
            }
            catch
            {
            }

            return value;
        }

        private static void HandleCommand(Player sender, string command, object[] args)
        {
            switch (command.ToLowerInvariant())
            {
                case "isusing":
                    {
                        Debug.Log($"isusing from {sender.NickName}");
                        break;
                    }

                case "tp":
                    {
                        if (args != null && args.Length > 0 && args[0] is Vector3 pos)
                        {
                            if (GorillaTagger.Instance != null)
                                GorillaTagger.Instance.transform.position = pos;
                        }
                        break;
                    }

                default:
                    {
                        Debug.Log($"Unknown command '{command}' from {sender.NickName}");
                        break;
                    }
            }
        }
    }
}