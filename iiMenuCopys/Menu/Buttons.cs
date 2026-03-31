using iiMenu.Classes;
using iiMenu.Mods;
using iiMenu.Mods.Spammers;
using UnityEngine;
using static iiMenu.Menu.Main;

/*
 *  HEY SKIDDERS
 *  THE BUTTONS
*/

namespace iiMenu.Menu
{
    internal class Buttons
    {
        public static ButtonInfo[][] buttons = new ButtonInfo[][]
        {
            new ButtonInfo[] { // Main Stuff [0]
                new ButtonInfo { buttonText = "Join Discord", method =() => Important.JoinDiscord(), isTogglable = false, toolTip = "Invites you to join the ii's <b>Stupid</b> Mods Discord server."},

                new ButtonInfo { buttonText = "Settings", method =() => currentCategoryName = "Settings", isTogglable = false, toolTip = "Opens the settings menu."},
                new ButtonInfo { buttonText = "Players", method = Settings.PlayersTab, isTogglable = false, toolTip = "Opens the players tab."},

                new ButtonInfo { buttonText = "Favorite Mods", method =() => currentCategoryName = "Favorite Mods", isTogglable = false, toolTip = "Opens your favorite mods. Favorite mods with left grip."},
                new ButtonInfo { buttonText = "Enabled Mods", method =() => currentCategoryName = "Enabled Mods", isTogglable = false, toolTip = "Opens your enabled mods."},

                new ButtonInfo { buttonText = "Room Mods", method =() => currentCategoryName = "Room Mods", isTogglable = false, toolTip = "Opens the room mods."},
                new ButtonInfo { buttonText = "Important Mods", method =() => currentCategoryName = "Important Mods", isTogglable = false, toolTip = "Opens the important mods."},
                new ButtonInfo { buttonText = "Safety Mods", method =() => currentCategoryName = "Safety Mods", isTogglable = false, toolTip = "Opens the safety mods."},
                new ButtonInfo { buttonText = "Movement Mods", method =() => currentCategoryName = "Movement Mods", isTogglable = false, toolTip = "Opens the movement mods."},
                new ButtonInfo { buttonText = "Advantage Mods", method =() => currentCategoryName = "Advantage Mods", isTogglable = false, toolTip = "Opens the advantage giving mods."},
                new ButtonInfo { buttonText = "Visual Mods", method =() => currentCategoryName = "Visual Mods", isTogglable = false, toolTip = "Opens the visual mods."},
                new ButtonInfo { buttonText = "Fun Mods", method =() => currentCategoryName = "Fun Mods", isTogglable = false, toolTip = "Opens the fun mods."},
                new ButtonInfo { buttonText = "Sound Mods", method =() => currentCategoryName = "Sound Mods", isTogglable = false, toolTip = "Opens the sound mods."},
                new ButtonInfo { buttonText = "Projectile Mods", method =() => currentCategoryName = "Projectile Mods", isTogglable = false, toolTip = "Opens the projectile mods."},
                new ButtonInfo { buttonText = "Master Mods", method =() => currentCategoryName = "Master Mods", isTogglable = false, toolTip = "Opens the master mods."},
                new ButtonInfo { buttonText = "Overpowered Mods", method =() => currentCategoryName = "Overpowered Mods", isTogglable = false, toolTip = "Opens the overpowered mods."},
                new ButtonInfo { buttonText = "Experimental Mods", method =() => currentCategoryName = "Experimental Mods", isTogglable = false, toolTip = "Opens the experimental mods."},
            },

            new ButtonInfo[] { // Settings [1]
                new ButtonInfo { buttonText = "Exit Settings", method =() => currentCategoryName = "Main", isTogglable = false, toolTip = "Returns you back to the main page."},
                new ButtonInfo { buttonText = "Menu Settings", method =() => currentCategoryName = "Menu Settings", isTogglable = false, toolTip = "Opens the settings for the menu."},
                new ButtonInfo { buttonText = "Room Settings", method =() => currentCategoryName = "Room Settings", isTogglable = false, toolTip = "Opens the settings for the room mods."},
                new ButtonInfo { buttonText = "Movement Settings", method =() => currentCategoryName = "Movement Settings", isTogglable = false, toolTip = "Opens the settings for the movement mods."},
                new ButtonInfo { buttonText = "Projectile Settings", method =() => currentCategoryName = "Projectile Settings", isTogglable = false, toolTip = "Opens the settings for the projectiles."}
            },

            new ButtonInfo[] { // Menu (in Settings) [2]
                new ButtonInfo { buttonText = "Exit Menu Settings", method =() => currentCategoryName = "Settings", isTogglable = false, toolTip = "Brings you back to the settings menu."},
                new ButtonInfo { buttonText = "Right Hand", enableMethod =() => rightHand = true, disableMethod =() => rightHand = false, toolTip = "Puts the menu on your right hand."},
                new ButtonInfo { buttonText = "Both Hands", enableMethod =() => bothHands = true, disableMethod =() => bothHands = false, toolTip = "Puts the menu on your both of your hands."},
                new ButtonInfo { buttonText = "Wrist Menu", enableMethod =() => wristMenu = true, disableMethod =() => wristMenu = false, toolTip = "Makes the menu like a weird wrist watch, click your hand to open it."},
                new ButtonInfo { buttonText = "Thick Menu", enableMethod =() => thinmenu = false, disableMethod =() => thinmenu = true, toolTip = "Makes the menu thick."},
                new ButtonInfo { buttonText = "Long Menu", enableMethod =() => longmenu = true, disableMethod =() => longmenu = false, toolTip = "Makes the menu long."},
                new ButtonInfo { buttonText = "Flip Menu", enableMethod =() => flipMenu = true, disableMethod =() => flipMenu = false, toolTip = "Flips the menu to the back of your hand."},

                new ButtonInfo { buttonText = "Change Menu Theme", method =() => Settings.ChangeMenuTheme(), enableMethod =() => Settings.ChangeMenuTheme(), disableMethod =() => Settings.ChangeMenuTheme(false), incremental = true, isTogglable = false, toolTip = "Changes the theme of the menu."},
                new ButtonInfo { buttonText = "Change Page Type", method =() => Settings.ChangePageType(), enableMethod =() => Settings.ChangePageType(), disableMethod =() => Settings.ChangePageType(false), incremental = true, isTogglable = false, toolTip = "Changes the type of page buttons."},
                new ButtonInfo { buttonText = "Change Arrow Type", method =() => Settings.ChangeArrowType(), enableMethod =() => Settings.ChangeArrowType(), disableMethod =() => Settings.ChangeArrowType(false), incremental = true, isTogglable = false, toolTip = "Changes the type of arrows on the page buttons."},
                //new ButtonInfo { buttonText = "Change Font Type", method =() => Settings.ChangeFontType(), isTogglable = false, toolTip = "Changes the type of font."},
                new ButtonInfo { buttonText = "Change Pointer Position", enableMethod =() => Settings.ChangePointerPosition(), method =() => Settings.ChangePointerPosition(), isTogglable = false, toolTip = "Changes the position of the pointer."},
                new ButtonInfo { buttonText = "Disorganize Menu", method =() => Settings.DisorganizeMenu(), isTogglable = false, toolTip = "Disorganizes the entire menu. This cannot be undone."},
                new ButtonInfo { buttonText = "Disable Notifications", enableMethod =() => disableNotifications = true, disableMethod =() => disableNotifications = false, toolTip = "Disables all notifications."},
                new ButtonInfo { buttonText = "Enable FPS Counter", enableMethod =() => fpsCounter = true, disableMethod =() => fpsCounter = false, toolTip = "Disables the fps counter."},
                new ButtonInfo { buttonText = "Disable Home Button", enableMethod =() => homeButton = false, disableMethod =() => homeButton = true, toolTip = "Disables the home button on the menu."},
                new ButtonInfo { buttonText = "Disable Enabled GUI", overlapText = "Disable Arraylist GUI", enableMethod =() => showEnabledModsVR = false, disableMethod =() => showEnabledModsVR = true, toolTip = "Disables the GUI that shows the enabled mods."},
                new ButtonInfo { buttonText = "Disable Incremental Buttons", enableMethod =() => incrementalButtons = false, disableMethod =() => incrementalButtons = true, toolTip = "Disables the buttons with the increment and decrement buttons next to it."},
                new ButtonInfo { buttonText = "Disable Disconnect Button", enableMethod =() => disableDisconnectButton = true, disableMethod =() => disableDisconnectButton = false, toolTip = "Disables the disconnect button at the top of the menu."},

                new ButtonInfo { buttonText = "Hide Pointer", enableMethod =() => hidePointer = true, disableMethod =() => hidePointer = false, toolTip = "Hides the pointer above your hand."},
                new ButtonInfo { buttonText = "High Quality Text", enableMethod =() => highQualityText = true, disableMethod =() => highQualityText = false, toolTip = "Makes the menu's text really high quality."},

                new ButtonInfo { buttonText = "Annoying Mode", enableMethod =() => annoyingMode = true, disableMethod = Settings.AnnoyingModeOff, toolTip = "Turns on the April Fools 2024 settings."},
                new ButtonInfo { buttonText = "Lowercase Mode", enableMethod =() => lowercaseMode = true, disableMethod =() => lowercaseMode = false, toolTip = "Makes the entire menu's text lowercase."},
                new ButtonInfo { buttonText = "Uppercase Mode", enableMethod =() => uppercaseMode = true, disableMethod =() => uppercaseMode = false, toolTip = "Makes the entire menu's text uppercase."},

                new ButtonInfo { buttonText = "Save Preferences", method =() => Settings.SavePreferences(), isTogglable = false, toolTip = "Saves your preferences to a file."},
                new ButtonInfo { buttonText = "Load Preferences", method =() => Settings.LoadPreferences(), isTogglable = false, toolTip = "Loads your preferences from a file."},
                new ButtonInfo { buttonText = "Panic", method =() => Settings.Panic(), isTogglable = false, toolTip = "Disables every single active mod."},
            },

            new ButtonInfo[] { // Room (in Settings) [3]
                new ButtonInfo { buttonText = "Exit Room Settings", method =() => currentCategoryName = "Settings", isTogglable = false, toolTip = "Brings you back to the settings menu."},
                new ButtonInfo { buttonText = "Primary Room Mods", enableMethod =() => Settings.EnablePrimaryRoomMods(), disableMethod =() => Settings.DisablePrimaryRoomMods(), toolTip = "Makes the room mods (disconnect, reconnect, etc) only run when clicking primary."},
            },

            new ButtonInfo[] { // Movement (in Settings) [4]
                new ButtonInfo { buttonText = "Exit Movement Settings", method =() => currentCategoryName = "Settings", isTogglable = false, toolTip = "Brings you back to the settings menu."},
                new ButtonInfo { buttonText = "Change Platform Type", overlapText = "Change Platform Type <color=grey>[</color><color=green>Normal</color><color=grey>]</color>", method =() => Movement.ChangePlatformType(), isTogglable = false, toolTip = "Changes the type of the platforms."},
                new ButtonInfo { buttonText = "Change Platform Shape", overlapText = "Change Platform Shape <color=grey>[</color><color=green>Sphere</color><color=grey>]</color>", method =() => Movement.ChangePlatformShape(), isTogglable = false, toolTip = "Changes the shape of the platforms."},
                new ButtonInfo { buttonText = "Change Fly Speed", overlapText = "Change Fly Speed <color=grey>[</color><color=green>Normal</color><color=grey>]</color>", method =() => Movement.ChangeFlySpeed(), isTogglable = false, toolTip = "Changes the speed of the fly mods, including iron man."},
                new ButtonInfo { buttonText = "Change Speed Boost Amount", overlapText = "Change Speed Boost Amount <color=grey>[</color><color=green>Normal</color><color=grey>]</color>", method =() => Movement.ChangeSpeedBoostAmount(), isTogglable = false, toolTip = "Changes the speed of the speed boosts."},
            },

            new ButtonInfo[] { // Projectiles (in Settings) [5]
                new ButtonInfo { buttonText = "Exit Projectile Settings", method =() => currentCategoryName = "Settings", isTogglable = false, toolTip = "Brings you back to the settings menu."},
                new ButtonInfo { buttonText = "Change Projectile", overlapText = "Change Projectile <color=grey>[</color><color=green>Cube</color>]<color=grey></color>", method =() => Projectiles.ChangeProjectile(), isTogglable = false, toolTip = "Changes the current projectile you want to use" },
            },

            new ButtonInfo[] { // Room Mods [6]
                new ButtonInfo { buttonText = "Exit Room Mods", method =() => currentCategoryName = "Main", isTogglable = false, toolTip = "Returns you back to the main page."},

                new ButtonInfo { buttonText = "Disconnect", method =() => Important.Disconnect(), isTogglable = false, toolTip = "Disconnects you from the lobby."},
                new ButtonInfo { buttonText = "Reconnect", method =() => Important.Reconnect(), isTogglable = false, toolTip = "Reconnects you from and to the lobby."},
                new ButtonInfo { buttonText = "Cancel Reconnect", method =() => Important.CancelReconnect(), isTogglable = false, toolTip = "Cancels the reconnection loop."},
                new ButtonInfo { buttonText = "Join Random", method =() => Important.JoinRandom(), isTogglable = false, toolTip = "Joins a random public lobby." },
                new ButtonInfo { buttonText = "Create Public", method =() => Important.CreatePublic(), isTogglable = false, toolTip = "Creates a public lobby."},

                new ButtonInfo { buttonText = "Auto Join Room \"1\"", method =() => RoomJoiners.JoinRoom("1"), isTogglable = false, toolTip = "Automatically attempts to connect to room \"1\" every 3 seconds until connected." },
                new ButtonInfo { buttonText = "Auto Join Room \"2\"", method =() => RoomJoiners.JoinRoom("2"), isTogglable = false, toolTip = "Automatically attempts to connect to room \"2\" every 3 seconds until connected." },
                new ButtonInfo { buttonText = "Auto Join Room \"MODS\"", method =() => RoomJoiners.JoinRoom("MODS"), isTogglable = false, toolTip = "Automatically attempts to connect to room \"MODS\" every 3 seconds until connected." },
                new ButtonInfo { buttonText = "Auto Join Room \"MOD\"", method =() => RoomJoiners.JoinRoom("MOD"), isTogglable = false, toolTip = "Automatically attempts to connect to room \"MOD\" every 3 seconds until connected." },

                new ButtonInfo { buttonText = "Auto Join Room \"RUN\"", method =() => RoomJoiners.JoinRoom("RUN"), isTogglable = false, toolTip = "Automatically attempts to connect to room \"RUN\" every 3 seconds until connected." },
                new ButtonInfo { buttonText = "Auto Join Room \"DAISY\"", method =() => RoomJoiners.JoinRoom("DAISY"), isTogglable = false, toolTip = "Automatically attempts to connect to room \"DAISY\" every 3 seconds until connected." },
                new ButtonInfo { buttonText = "Auto Join Room \"DAISY09\"", method =() => RoomJoiners.JoinRoom("DAISY09"), isTogglable = false, toolTip = "Automatically attempts to connect to room \"DAISY09\" every 3 seconds until connected." },
                new ButtonInfo { buttonText = "Auto Join Room \"PBBV\"", method =() => RoomJoiners.JoinRoom("PBBV"), isTogglable = false, toolTip = "Automatically attempts to connect to room \"PBBV\" every 3 seconds until connected." },
                new ButtonInfo { buttonText = "Auto Join Room \"BOT\"", method =() => RoomJoiners.JoinRoom("BOT"), isTogglable = false, toolTip = "Automatically attempts to connect to room \"BOT\" every 3 seconds until connected." },
            },

            new ButtonInfo[] // Important Mods [7]
            {
                new ButtonInfo { buttonText = "Exit Important Mods", method =() => currentCategoryName = "Main", isTogglable = false, toolTip = "Returns you back to the main page."},

                new ButtonInfo { buttonText = "Turning", method =() => Important.Turning(), isTogglable = true, toolTip = "Lets you turn while using joystick." },
                new ButtonInfo { buttonText = "Stump Mirror", enableMethod =() => GameObject.Find("mirror (1)").SetActive(true), disableMethod =() => GameObject.Find("mirror (1)").SetActive(false), isTogglable = true, toolTip = "Enables the mirror in stump." },

                new ButtonInfo { buttonText = "Anti AFK", enableMethod =() => Important.EnableAntiAFK(), disableMethod =() => Important.DisableAntiAFK(), toolTip = "Doesn't let you get kicked for being AFK."},
                new ButtonInfo { buttonText = "Disable Network Triggers", enableMethod =() => Important.DisableNetworkTriggers(), disableMethod =() => Important.EnableNetworkTriggers(), toolTip = "Disables the network triggers, so you can change maps without disconnecting."},
                new ButtonInfo { buttonText = "Disable Quit Box", enableMethod =() => Important.DisableQuitBox(), disableMethod =() => Important.EnableQuitBox(), toolTip = "Disables the box under the map that closes your game."},

                new ButtonInfo { buttonText = "Button Click Gun", method =() => Important.PCButtonClick(), isTogglable = true, toolTip = "Lets you press buttons with a gun."},

                new ButtonInfo { buttonText = "FPS Boost", enableMethod =() => Important.EnableFPSBoost(), disableMethod =() => Important.DisableFPSBoost(), toolTip = "Makes everything low quality in an attempt to boost your FPS."},
                new ButtonInfo { buttonText = "60 Hz / 60 FPS", method =() => Important.ForceLagGame(), toolTip = "Caps your FPS at 60 frames per second."},

                new ButtonInfo { buttonText = "Connect to US", method =() => Important.USServers(), isTogglable = false, toolTip = "Connects you to the United States servers."},
                new ButtonInfo { buttonText = "Connect to US West", method =() => Important.USWServers(), isTogglable = false, toolTip = "Connects you to the western United States servers."},
                new ButtonInfo { buttonText = "Connect to EU", method =() => Important.EUServers(), isTogglable = false, toolTip = "Connects you to the Europe servers."},
            },

            new ButtonInfo[] { // Safety Mods [8]
                new ButtonInfo { buttonText = "Exit Safety Mods", method =() => currentCategoryName = "Main", isTogglable = false, toolTip = "Returns you back to the main page."},

                new ButtonInfo { buttonText = "Flush RPCs", method =() => RPCProtection(), isTogglable = false, toolTip = "Flushes all RPC calls, good after you stop spamming." },
                new ButtonInfo { buttonText = "Anti Crash", enableMethod =() => Safety.AntiCrashEnabled(), disableMethod =() => Safety.AntiCrashDisabled(), toolTip = "Doesn't allow projectiles from other users that are far away."},
                new ButtonInfo { buttonText = "Anti Moderator", method =() => Safety.AntiModerator(), toolTip = "When someone with the stick joins, you get disconnected and their player ID and room code gets saved to a file."},

                new ButtonInfo { buttonText = "Anti Report <color=grey>[</color><color=green>Disconnect</color><color=grey>]</color>", method =() => Safety.AntiReportDisconnect(), toolTip = "Disconnects you from the lobby when anyone comes near your report button."},
                new ButtonInfo { buttonText = "Anti Report <color=grey>[</color><color=green>Reconnect</color><color=grey>]</color>", method =() => Safety.AntiReportReconnect(), toolTip = "Disconnects and reconnects you from the lobby when anyone comes near your report button."},
                new ButtonInfo { buttonText = "Anti Report <color=grey>[</color><color=green>Join Random</color><color=grey>]</color>", method =() => Safety.AntiReportJoinRandom(), toolTip = "Connects you to a random lobby when anyone comes near your report button."},

                new ButtonInfo { buttonText = "Show Anti Cheat Reports <color=grey>[</color><color=green>Self</color><color=grey>]</color>", enableMethod =() => Safety.EnableACReportSelf(), disableMethod =() => Safety.DisableACReportSelf(), toolTip = "Gives you a notification every time you have been reported by the anti cheat."},
                new ButtonInfo { buttonText = "Show Anti Cheat Reports <color=grey>[</color><color=green>All</color><color=grey>]</color>", enableMethod =() => Safety.EnableACReportAll(), disableMethod =() => Safety.DisableACReportAll(), toolTip = "Gives you a notification every time anyone has been reported by the anti cheat."},

                new ButtonInfo { buttonText = "Change Identity", method =() => Safety.ChangeIdentity(), isTogglable = false, toolTip = "Changes your name on the leaderboard to a random string, but not on your rig."},
            },

            new ButtonInfo[] { // Movement Mods [9]
                new ButtonInfo { buttonText = "Exit Movement Mods", method =() => currentCategoryName = "Main", isTogglable = false, toolTip = "Returns you back to the main page."},

                new ButtonInfo { buttonText = "Platforms", method =() => Movement.Platforms(), toolTip = "Platforms, they do not show for other players."},
                new ButtonInfo { buttonText = "Trigger Platforms", method =() => Movement.TriggerPlatforms(), toolTip = "Platforms, they do not show for other players."},
                new ButtonInfo { buttonText = "Frozone", method =() => Movement.Frozone(), toolTip = "Spawns slippery blocks under your hands using <color=green>grip</color>."},
                new ButtonInfo { buttonText = "Platform Gun", method =() => Movement.PlatformGun(), toolTip = "Spawns legacy platforms rapidly for those who have networked platforms."},
                new ButtonInfo { buttonText = "Fly <color=grey>[</color><color=green>A</color><color=grey>]</color>", method =() => Movement.Fly(), toolTip = "Sends your character forwards when holding <color=green>A</color>."},
                new ButtonInfo { buttonText = "Trigger Fly <color=grey>[</color><color=green>T</color><color=grey>]</color>", method =() => Movement.TriggerFly(), toolTip = "Sends your character forwards when holding <color=green>trigger</color>."},
                new ButtonInfo { buttonText = "Joystick Fly <color=grey>[</color><color=green>J</color><color=grey>]</color>", method =() => Movement.JoystickFly(), enableMethod =() => Movement.DisableJoystick(), disableMethod =() => Movement.EnableJoystick(), toolTip = "Sends your character in whatever direction you are pointing your <color=green>joystick</color> in."},
                new ButtonInfo { buttonText = "Bark Fly <color=grey>[</color><color=green>J</color><color=grey>]</color>", method =() => Movement.BarkFly(), disableMethod =() => Movement.DisableBarkFly(), toolTip = "Acts like the fly that Bark has. Credits to KyleTheScientist."},
                new ButtonInfo { buttonText = "Slingshot Fly <color=grey>[</color><color=green>A</color><color=grey>]</color>", method =() => Movement.SlingshotFly(), toolTip = "Sends your character forwards, in a more elastic manner, when holding <color=green>A</color>."},
                new ButtonInfo { buttonText = "Zero Gravity Slingshot Fly <color=grey>[</color><color=green>A</color><color=grey>]</color>", method =() => Movement.ZeroGravitySlingshotFly(), toolTip = "Sends your character forwards, in a more elastic manner without gravity, when holding <color=green>A</color>."},
                new ButtonInfo { buttonText = "Drive <color=grey>[</color><color=green>J</color><color=grey>]</color>", method =() => Movement.Drive(), enableMethod =() => Movement.DisableJoystick(), disableMethod =() => Movement.EnableJoystick(), toolTip = "Lets you drive around in your invisible car. Use the <color=green>joystick</color> to move."},
                new ButtonInfo { buttonText = "Iron Man", method =() => Movement.IronMan(), toolTip = "Turns you into iron man, rotate your hands around to change direction."},
                new ButtonInfo { buttonText = "Spider Man <color=grey>[</color><color=red>NW</color><color=grey>]</color>", method =() => Movement.SpiderMan(), disableMethod =() => Movement.DisableSpiderMan(), toolTip = "Turns you into spider man, use your <color=green>grips</color> to shoot webs."},
                new ButtonInfo { buttonText = "Up And Down", method =() => Movement.UpAndDown(), toolTip = "Makes you go up when holding your trigger, and down when holding your <color=green>grip</color>."},
                new ButtonInfo { buttonText = "Auto Funny Run <color=grey>[</color><color=green>G</color><color=grey>]</color>", method =() => Movement.AutoFunnyRun(), toolTip = "Makes your character automatically funny run when folding <color=green>grip</color>."},
                new ButtonInfo { buttonText = "Force Tag Freeze", method =() => Movement.ForceTagFreeze(), disableMethod =() => Movement.NoTagFreeze(), toolTip = "Forces tag freeze on your character."},
                new ButtonInfo { buttonText = "No Tag Freeze", method =() => Movement.NoTagFreeze(), toolTip = "Disables tag freeze on your character."},
                new ButtonInfo { buttonText = "Low Gravity", method =() => Movement.LowGravity(), toolTip = "Makes gravity lower on your character."},
                new ButtonInfo { buttonText = "Zero Gravity", method =() => Movement.ZeroGravity(), toolTip = "Disables gravity on your character."},
                new ButtonInfo { buttonText = "High Gravity", method =() => Movement.HighGravity(), toolTip = "Makes gravity higher on your character."},
                new ButtonInfo { buttonText = "Wall Walk <color=grey>[</color><color=green>G</color><color=grey>]</color>", method =() => Movement.WallWalk(), toolTip = "Makes you get brought towards any wall you touch when holding <color=green>grip</color>."},
                new ButtonInfo { buttonText = "Weak Wall Walk <color=grey>[</color><color=green>G</color><color=grey>]</color>", method =() => Movement.WeakWallWalk(), toolTip = "Makes you get brought towards any wall you touch when holding <color=green>grip</color>, but weaker."},
                new ButtonInfo { buttonText = "Strong Wall Walk <color=grey>[</color><color=green>G</color><color=grey>]</color>", method =() => Movement.StrongWallWalk(), toolTip = "Makes you get brought towards any wall you touch when holding <color=green>grip</color>, but stronger."},
                new ButtonInfo { buttonText = "Teleport to Random", method =() => Movement.TeleportToRandom(), isTogglable = false, toolTip = "Teleports you to a random player."},
                new ButtonInfo { buttonText = "Teleport Gun", method =() => Movement.TeleportGun(), toolTip = "Teleports to wherever your hand desires."},
                new ButtonInfo { buttonText = "Airstrike", method =() => Movement.Airstrike(), toolTip = "Teleports to wherever your hand desires, except farther up, then launches you back down."},
                new ButtonInfo { buttonText = "Checkpoint <color=grey>[</color><color=green>A</color><color=grey>]</color>", method =() => Movement.Checkpoint(), disableMethod =() => Movement.DisableCheckpoint(), toolTip = "Place a checkpoint with <color=green>grip</color> and teleport to it with <color=green>A</color>."},
                new ButtonInfo { buttonText = "C4 <color=grey>[</color><color=green>A</color><color=grey>]</color>", method =() => Movement.Bomb(), disableMethod =() => Movement.DisableBomb(), toolTip = "Place a C4 with <color=green>grip</color> and detonate it with <color=green>A</color>."},
                new ButtonInfo { buttonText = "Speed Boost", method =() => Movement.SpeedBoost(), /*disableMethod =() => Movement.DisableSpeedBoost(),*/ toolTip = "Changes your speed to whatever you set it to."},
                new ButtonInfo { buttonText = "Noclip <color=grey>[</color><color=green>T</color><color=grey>]</color>", method =() => Movement.Noclip(), toolTip = "Makes you go through objects when holding <color=green>trigger</color>."},
                new ButtonInfo { buttonText = "Invisible <color=grey>[</color><color=green>B</color><color=grey>]</color>", method =() => Movement.Invisible(), disableMethod =() => Movement.DisableInvisible(), toolTip = "Makes you go invisible when holding <color=green>B</color>."},
                new ButtonInfo { buttonText = "Ghost <color=grey>[</color><color=green>A</color><color=grey>]</color>", method =() => Movement.Ghost(), disableMethod =() => Movement.EnableRig(), toolTip = "Keeps your rig still when holding <color=green>A</color>."},
                new ButtonInfo { buttonText = "Rig Gun", method =() => Movement.RigGun(), toolTip = "Moves your rig to wherever your hand desires."},
                new ButtonInfo { buttonText = "Grab Rig <color=grey>[</color><color=green>G</color><color=grey>]</color>", method =() => Movement.GrabRig(), toolTip = "Lets you grab your rig when holding <color=green>G</color>."},
                new ButtonInfo { buttonText = "Spaz Rig <color=grey>[</color><color=green>A</color><color=grey>]</color>", enableMethod =() => Movement.EnableSpazRig(), method =() => Movement.SpazRig(), disableMethod =() => Movement.DisableSpazRig(), toolTip = "Makes every part of your rig spaz out a little bit when holding <color=green>A</color>."},
                new ButtonInfo { buttonText = "Spaz Hands <color=grey>[</color><color=green>A</color><color=grey>]</color>", method =() => Movement.SpazHands(), toolTip = "Makes your hands spaz out everywhere when holding <color=green>A</color>."},
                new ButtonInfo { buttonText = "Spaz Head", method =() => Movement.SpazHead(), disableMethod =() => Fun.FixHead(), toolTip = "Makes your head spaz out."},
                new ButtonInfo { buttonText = "Random Spaz Head", method =() => Movement.RandomSpazHead(), disableMethod =() => Fun.FixHead(), toolTip = "Makes your head spaz out for 0 to 1 seconds every 1 to 4 seconds."},
                new ButtonInfo { buttonText = "Laggy Rig", method =() => Movement.LaggyRig(), disableMethod =() => Movement.EnableRig(), toolTip = "Makes your rig laggy."},
                new ButtonInfo { buttonText = "Update Rig <color=grey>[</color><color=green>A</color><color=grey>]</color>", method =() => Movement.UpdateRig(), disableMethod =() => Movement.EnableRig(), toolTip = "Freezes your rig in place. Whenever you click <color=green>A</color>, your rig will update."},
                new ButtonInfo { buttonText = "Freeze Rig Limbs <color=grey>[</color><color=green>A</color><color=grey>]</color>", method =() => Movement.FreezeRigLimbs(), toolTip = "Makes your hands and head freeze on your rig, but not your body, when holding <color=green>A</color>."},
                new ButtonInfo { buttonText = "Freeze Rig Body <color=grey>[</color><color=green>A</color><color=grey>]</color>", method =() => Movement.FreezeRigBody(), toolTip = "Makes your body freeze on your rig, but not your hands and head, when holding <color=green>A</color>."},
                new ButtonInfo { buttonText = "Auto Dance <color=grey>[</color><color=green>A</color><color=grey>]</color>", method =() => Movement.AutoDance(), toolTip = "Makes you dance when holding <color=green>A</color>."},
                new ButtonInfo { buttonText = "Helicopter <color=grey>[</color><color=green>A</color><color=grey>]</color>", method =() => Movement.Helicopter(), toolTip = "Turns you into a helicopter when holding <color=green>A</color>."},
                new ButtonInfo { buttonText = "Ghost Animations", method =() => Movement.GhostAnimations(), disableMethod =() => Movement.EnableRig(), toolTip = "Puts your hands down, and makes you walk when holding <color=green>A</color>. You can also point with <color=green>B</color>"},
                new ButtonInfo { buttonText = "Stare at Nearby", method =() => Movement.StareAtNearby(), toolTip = "Makes you stare at the nearest player."},
                new ButtonInfo { buttonText = "Floating Rig", enableMethod =() => Movement.EnableFloatingRig(), method =() => Movement.FloatingRig(), disableMethod =() => Movement.DisableFloatingRig(), toolTip = "Makes your rig float."},
                new ButtonInfo { buttonText = "Bees", method =() => Movement.Bees(), disableMethod =() => Movement.EnableRig(), toolTip = "Makes your rig teleport to random players, imitating the bees ghost."},
                new ButtonInfo { buttonText = "Size Changer <color=grey>[</color><color=yellow>SPRING CLEANING</color><color=grey>]</color>", method =() => Movement.SizeChanger(), enableMethod =() => Movement.EnableSizeChanger(), disableMethod =() => Movement.EnableSizeChanger(), toolTip = "Increase your size by holding <color=green>trigger</color>, and decrease your size by holding <color=green>grip</color>."},
                new ButtonInfo { buttonText = "Slippery Hands", enableMethod =() => Movement.EnableSlipperyHands(), disableMethod =() => Movement.DisableSlipperyHands(), toolTip = "Makes everything ice, as in extremely slippery."},
                new ButtonInfo { buttonText = "Grippy Hands", enableMethod =() => Movement.EnableGrippyHands(), disableMethod =() => Movement.DisableGrippyHands(), toolTip = "Covers your hands in grip tape, letting you wallrun as high as you want."},
                new ButtonInfo { buttonText = "Slide Control", enableMethod =() => Movement.EnableSlideControl(), disableMethod =() => Movement.DisableSlideControl(), toolTip = "Lets you control yourself on ice perfectly."},
                new ButtonInfo { buttonText = "Weak Slide Control", enableMethod =() => Movement.EnableWeakSlideControl(), disableMethod =() => Movement.DisableSlideControl(), toolTip = "Lets you control yourself on ice a little more perfect than before."},
                new ButtonInfo { buttonText = "Punch Mod", method =() => Movement.PunchMod(), toolTip = "Lets people punch you across the map."},
                new ButtonInfo { buttonText = "Solid Players", method =() => Movement.SolidPlayers(), toolTip = "Lets you walk on top of other players."},
                new ButtonInfo { buttonText = "Throw Controllers", method =() => Movement.ThrowControllers(), toolTip = "Lets you throw your controllers with <color=green>X</color> and <color=green>A</color>."},
                new ButtonInfo { buttonText = "Steam Long Arms", enableMethod =() => Movement.EnableSteamLongArms(), disableMethod =() => Movement.DisableSteamLongArms(), toolTip = "Gives you long arms similar to override world scale."},
                new ButtonInfo { buttonText = "Stick Long Arms", method =() => Movement.StickLongArms(), toolTip = "Makes you look like you're using sticks."},
                new ButtonInfo { buttonText = "Flick Jump <color=grey>[</color><color=green>A</color><color=grey>]</color>", method =() => Movement.FlickJump(), toolTip = "Makes your hand go down really fast when holding <color=green>A</color>."},
                new ButtonInfo { buttonText = "Long Jump <color=grey>[</color><color=green>A</color><color=grey>]</color>", method =() => Movement.LongJump(), toolTip = "Makes you look like you're legitimately long jumping when holding <color=green>A</color>."},
                new ButtonInfo { buttonText = "Copy Movement Gun", method =() => Movement.CopyMovementGun(), toolTip = "Makes your rig copy the movement of whoever your hand desires."},
                new ButtonInfo { buttonText = "Follow Player Gun", method =() => Movement.FollowPlayerGun(), toolTip = "Flies your rig towards whoever your hand desires."},
                new ButtonInfo { buttonText = "Orbit Player Gun", method =() => Movement.OrbitPlayerGun(), toolTip = "Orbits your rig around whoever your hand desires."},
                new ButtonInfo { buttonText = "Jumpscare Gun", method =() => Movement.JumpscareGun(), toolTip = "Makes you jumpscare whoever your hand desires."},
                new ButtonInfo { buttonText = "Intercourse Gun", method =() => Movement.IntercourseGun(), toolTip = "Makes you thrust whoever your hand desires, with sounds."}
            },

            new ButtonInfo[] { // Advantage Mods [10]
                new ButtonInfo { buttonText = "Exit Advantage Mods", method =() => currentCategoryName = "Main", isTogglable = false, toolTip = "Returns you back to the main page."},

                new ButtonInfo { buttonText = "Tag Self", method =() => Advantages.TagSelf(), isTogglable = false, toolTip = "Instantly tags yourself."},
                new ButtonInfo { buttonText = "Physical Tag Aura", method =() => Advantages.PhysicalTagAura(), toolTip = "Moves your hand into nearby players when tagged."},
                new ButtonInfo { buttonText = "Tag Gun", method =() => Advantages.TagGun(), toolTip = "Tags whoever your hand desires."},
                new ButtonInfo { buttonText = "Flick Tag Gun", method =() => Advantages.FlickTagGun(), toolTip = "Moves your hand to wherever your hand desires in an attempt to tag whoever your hand desires."},
                new ButtonInfo { buttonText = "Tag All", method =() => Advantages.TagAll(), disableMethod =() => Movement.EnableRig(), toolTip = "Attempts to tag everyone in the lobby."},
                new ButtonInfo { buttonText = "Tag Bot", method =() => Advantages.TagBot(), disableMethod =() => Movement.EnableRig(), toolTip = "Automatically tags yourself and everyone else on a loop, use <color=green>B</color> to turn it off."},
                new ButtonInfo { buttonText = "No Tag on Join", method =() => Advantages.NoTagOnJoin(), disableMethod =() => Advantages.TagOnJoin(), toolTip = "When you join a lobby, you won't be tagged when you join."},
                new ButtonInfo { buttonText = "Remove Christmas Lights", enableMethod =() => Advantages.EnableRemoveChristmasLights(), disableMethod =() => Advantages.DisableRemoveChristmasLights(), toolTip = "Removes lights, good for walls."},
                new ButtonInfo { buttonText = "Remove Christmas Decorations", enableMethod =() => Advantages.EnableRemoveChristmasDecorations(), disableMethod =() => Advantages.DisableRemoveChristmasDecorations(), toolTip = "Removes snowmen and such, good for anyone but very obvious."},
            },

            new ButtonInfo[] { // Visual Mods [11]
                new ButtonInfo { buttonText = "Exit Visual Mods", method =() => currentCategoryName = "Main", isTogglable = false, toolTip = "Returns you back to the main page."},

                new ButtonInfo { buttonText = "Day Time", method =() => Fun.DayTime(), toolTip = "Makes time day."},
                new ButtonInfo { buttonText = "Night Time", method =() => Fun.NightTime(), toolTip = "Makes time night."},

                new ButtonInfo { buttonText = "Fix Rig Colors", method =() => Visuals.FixRigColors(), toolTip = "Fixes a steam bug where other players' color would be wrong between servers."},
                new ButtonInfo { buttonText = "Remove Leaves", enableMethod =() => Visuals.EnableRemoveLeaves(), disableMethod =() => Visuals.DisableRemoveLeaves(), toolTip = "Removes leaves on trees, good for branching."},

                new ButtonInfo { buttonText = "Casual Tracers", method =() => Visuals.CasualTracers(), toolTip = "Puts tracers on your right hand. Shows untagged when tagged, vice versa."},
                new ButtonInfo { buttonText = "Infection Tracers", method =() => Visuals.InfectionTracers(), toolTip = "Puts tracers on your right hand. Shows everyone."},

                new ButtonInfo { buttonText = "Casual Bone ESP", method =() => Visuals.CasualBoneESP(), toolTip = "Acts like infection tracers color wise, but with bones."},
                new ButtonInfo { buttonText = "Infection Bone ESP", method =() => Visuals.InfectionBoneESP(), toolTip = "Acts like casual tracers color wise, but with bones."},

                new ButtonInfo { buttonText = "Casual Chams", method =() => Visuals.CasualChams(), disableMethod =() => Visuals.DisableChams(), toolTip = "Acts like infection tracers color wise, but lets you see their fur through walls."},
                new ButtonInfo { buttonText = "Infection Chams", method =() => Visuals.InfectionChams(), disableMethod =() => Visuals.DisableChams(), toolTip = "Acts like casual tracers color wise, but lets you see their fur through walls."},

                new ButtonInfo { buttonText = "Casual Beacons", method =() => Visuals.CasualBeacons(), toolTip = "Acts like infection tracers color wise, but it's just a giant line."},
                new ButtonInfo { buttonText = "Infection Beacons", method =() => Visuals.InfectionBeacons(), toolTip = "Acts like casual tracers color wise, but it's just a giant line."},

                new ButtonInfo { buttonText = "Show Button Colliders", method =() => Visuals.ShowButtonColliders(), toolTip = "Shows dots above your hand, such as when you open the menu."},
            },

            new ButtonInfo[] { // Fun Mods [12]
                new ButtonInfo { buttonText = "Exit Fun Mods", method =() => currentCategoryName = "Main", isTogglable = false, toolTip = "Returns you back to the main page."},

                new ButtonInfo { buttonText = "Upside Down Head", method =() => Fun.UpsideDownHead(), disableMethod =() => Fun.FixHead(), toolTip = "Flips your head upside down."},
                new ButtonInfo { buttonText = "Spin Head X", method =() => Fun.SpinHeadX(), disableMethod =() => Fun.FixHead(), toolTip = "Spins your head on the X axis."},
                new ButtonInfo { buttonText = "Spin Head Y", method =() => Fun.SpinHeadY(), disableMethod =() => Fun.FixHead(), toolTip = "Spins your head on the Y axis."},
                new ButtonInfo { buttonText = "Spin Head Z", method =() => Fun.SpinHeadZ(), disableMethod =() => Fun.FixHead(), toolTip = "Spins your head on the Z axis."},

                new ButtonInfo { buttonText = "Flip Hands", method =() => Fun.FlipHands(), toolTip = "Swaps your hands, left is right and right is left."},
                new ButtonInfo { buttonText = "Loud Hand Taps", method =() => Fun.LoudHandTaps(), disableMethod =() => Fun.FixHandTaps(), toolTip = "Makes your hand taps really loud."},
                new ButtonInfo { buttonText = "Silent Hand Taps", method =() => Fun.SilentHandTaps(), disableMethod =() => Fun.FixHandTaps(), toolTip = "Makes your hand taps really quiet."},
                new ButtonInfo { buttonText = "Instant Hand Taps", method =() => Fun.EnableInstantHandTaps(), disableMethod =() => Fun.DisableInstantHandTaps(), toolTip = "Removes the hand tap cooldown."},

                new ButtonInfo { buttonText = "Pop All Balloons", method =() => Fun.PopAllBalloons(), isTogglable = false, toolTip = "Pops every single balloon cosmetic." },
                new ButtonInfo { buttonText = "Grab Balloons", method =() => Fun.GrabBalloons(), toolTip = "Puts every single balloon cosmetic in your hand." },
                new ButtonInfo { buttonText = "Balloon Gun", method =() => Fun.BalloonGun(), toolTip = "Moves every single balloon cosmetic to wherever your hand desires." },
                new ButtonInfo { buttonText = "Destroy Balloons", method =() => Fun.DestroyBalloons(), isTogglable = false, toolTip = "Sends every single balloon cosmetic to hell." },

                new ButtonInfo { buttonText = "Remove Name", method =() => Fun.RemoveName(), isTogglable = false, toolTip = "Sets your name to nothing." },
                new ButtonInfo { buttonText = "Set Name to \"STATUE\"", method =() => Fun.SetNameToSTATUE(), isTogglable = false, toolTip = "Sets your name to \"STATUE\"." },
                new ButtonInfo { buttonText = "Set Name to \"RUN\"", method =() => Fun.SetNameToRUN(), isTogglable = false, toolTip = "Sets your name to \"RUN\"." },
                new ButtonInfo { buttonText = "Set Name to \"BEHINDYOU\"", method =() => Fun.SetNameToBEHINDYOU(), isTogglable = false, toolTip = "Sets your name to \"BEHINDYOU\"." },
                new ButtonInfo { buttonText = "Set Name to \"iiOnTop\"", method =() => Fun.SetNameToiiOnTop(), isTogglable = false, toolTip = "Sets your name to \"iiOnTop\"." },
                new ButtonInfo { buttonText = "PBBV Name Cycle", method =() => Fun.PBBVNameCycle(), toolTip = "Sets your name on a loop to \"PBBV\", \"IS\", and \"HERE\"." },
                new ButtonInfo { buttonText = "J3VU Name Cycle", method =() => Fun.J3VUNameCycle(), toolTip = "Sets your name on a loop to \"J3VU\", \"HAS\", \"BECOME\", and \"HOSTILE\"" },
                new ButtonInfo { buttonText = "Run Rabbit Name Cycle", method =() => Fun.RunRabbitNameCycle(), toolTip = "Sets your name on a loop to \"RUN\" and \"RABBIT\"." },
                new ButtonInfo { buttonText = "Random Name Cycle", method =() => Fun.RandomNameCycle(), toolTip = "Sets your name on a loop to a bunch of random characters." },
                new ButtonInfo { buttonText = "Strobe Color", method =() => Fun.StrobeColor(), toolTip = "Makes your character flash." },
                new ButtonInfo { buttonText = "Rainbow Color", method =() => Fun.RainbowColor(), toolTip = "Makes your character rainbow." },
                new ButtonInfo { buttonText = "Hard Rainbow Color", method =() => Fun.HardRainbowColor(), toolTip = "Makes your character flash from red, green, blue, and magenta." },
                new ButtonInfo { buttonText = "Impossible Color <color=grey>[</color><color=red>Stump</color><color=grey>]</color>", method =() => Fun.NegativeColor(), isTogglable = false, toolTip = "Sets your colors to above the integer limit." },
                new ButtonInfo { buttonText = "Become \"goldentrophy\"", method =() => Fun.BecomeGoldentrophy(), isTogglable = false, toolTip = "Sets your name to \"goldentrophy\" and color to orange." },
                new ButtonInfo { buttonText = "Become \"PBBV\"", method =() => Fun.BecomePBBV(), isTogglable = false, toolTip = "Sets your name to \"PBBV\" and color to sky blue." },
                new ButtonInfo { buttonText = "Become \"ECHO\"", method =() => Fun.BecomeECHO(), isTogglable = false, toolTip = "Sets your name to \"ECHO\" and color to salmon." },
                new ButtonInfo { buttonText = "Become \"DAISY09\"", method =() => Fun.BecomeDAISY09(), isTogglable = false, toolTip = "Sets your name to \"DAISY09\" and color to a light pink." },
                new ButtonInfo { buttonText = "Become Hidden on Leaderboard", method =() => Fun.BecomeHiddenOnLeaderboard(), isTogglable = false, toolTip = "Sets your name to nothing and your color to a dark red, matching the leaderboard." },
                new ButtonInfo { buttonText = "Copy Identity Gun", method =() => Fun.CopyIdentityGun(), toolTip = "Steals the identity of whoever your hand desires." },

                new ButtonInfo { buttonText = "Change Accessories", method =() => Fun.ChangeAccessories(), toolTip = "Use your grips to change what hat you're wearing." },
                new ButtonInfo { buttonText = "Spaz Accessories", method =() => Fun.SpazAccessories(), toolTip = "Spazzes your accessories out." },
                new ButtonInfo { buttonText = "Custom Sound on Join", enableMethod =() => Fun.EnableCustomSoundOnJoin(), disableMethod =() => Fun.DisableCustomSoundOnJoin(), toolTip = "Writes to a file when someone joins or leaves. You must use the python script in the Discord for this to work." },

                new ButtonInfo { buttonText = "Copy ID Gun", method =() => Miscellaneous.CopyIDGun(), toolTip = "Copies the player ID of whoever your hand desires to the clipboard." },
                new ButtonInfo { buttonText = "Grab Player Info", method =() => Miscellaneous.GrabPlayerInfo(), isTogglable = false, toolTip = "Saves every player's name, color, and player ID as a text file and opens it." },
            },

            new ButtonInfo[] { // Sound Spam Mods [13]
                new ButtonInfo { buttonText = "Exit Sound Mods", method =() => currentCategoryName = "Main", isTogglable = false, toolTip = "Returns you back to the sound page."},

                new ButtonInfo { buttonText = "Soundboard", method =() => SoundBoard.LoadSoundboard(), isTogglable = false, toolTip = "A working, customizable soundboard that lets you play audios through your microphone."},

                new ButtonInfo { buttonText = "Random Sound Spam <color=grey>[</color><color=green>G</color><color=grey>]</color>", method =() => Sound.RandomSoundSpam(), toolTip = "Plays random sounds when holding grip." },
                new ButtonInfo { buttonText = "Bass Sound Spam <color=grey>[</color><color=green>G</color><color=grey>]</color>", method =() => Sound.BassSoundSpam(), toolTip = "Plays the loud drum sound when holding grip." },
                new ButtonInfo { buttonText = "Wolf Sound Spam <color=grey>[</color><color=green>G</color><color=grey>]</color>", method =() => Sound.WolfSoundSpam(), toolTip = "Plays the wolf howl when holding grip." },
                new ButtonInfo { buttonText = "Earrape Sound Spam <color=grey>[</color><color=green>G</color><color=grey>]</color>", method =() => Sound.EarrapeSoundSpam(), toolTip = "Plays a high-pitched sound when holding grip." },
                new ButtonInfo { buttonText = "Big Crystal Sound Spam <color=grey>[</color><color=green>G</color><color=grey>]</color>", method =() => Sound.BigCrystalSoundSpam(), toolTip = "Plays a long crystal sound when holding grip." },
                new ButtonInfo { buttonText = "AK-47 Sound Spam <color=grey>[</color><color=green>G</color><color=grey>]</color>", method =() => Sound.AK47SoundSpam(), toolTip = "Plays a sound that sounds like an AK-47 when holding grip." },
                new ButtonInfo { buttonText = "Squeak Sound Spam <color=grey>[</color><color=green>G</color><color=grey>]</color>", method =() => Sound.SqueakSoundSpam(), toolTip = "Plays the squeak sound when holding grip." },
                new ButtonInfo { buttonText = "Siren Sound Spam <color=grey>[</color><color=green>G</color><color=grey>]</color>", method =() => Sound.SirenSoundSpam(), toolTip = "Plays a siren sound when holding grip." },

                new ButtonInfo { buttonText = "- Sound ID", method =() => Sound.DecreaseSoundID(), isTogglable = false, toolTip = "Lowers the Sound ID of the Custom Sound Spam." },
                new ButtonInfo { buttonText = "+ Sound ID", method =() => Sound.IncreaseSoundID(), isTogglable = false, toolTip = "Increases the Sound ID of the Custom Sound Spam." },
                new ButtonInfo { buttonText = "Custom Sound Spam", overlapText = "Custom Sound Spam <color=grey>[</color><color=green>0</color><color=grey>]</color>", method =() => Sound.CustomSoundSpam(), toolTip = "Plays the selected sound when holding grip." },
            },

            new ButtonInfo[] { // Projectile Spam Mods [14]
                new ButtonInfo { buttonText = "Exit Projectile Mods", method =() => currentCategoryName = "Main", isTogglable = false, toolTip = "Returns you back to the projectile page."},
                new ButtonInfo { buttonText = "Projectile Spam <color=grey>[</color><color=green>G</color><color=grey>]</color>", method =() => Projectiles.ProjectileSpam(), toolTip = "Spams projectiles when holding <color=green>grip</color>." },
                new ButtonInfo { buttonText = "Projectile Aura", method =() => Projectiles.Aura(), toolTip = "Spams projectiles inside a aura." },
                new ButtonInfo { buttonText = "Give Projectile Spam Gun", method =() => Projectiles.GiveProjectileSpamGun(), toolTip = "Gives the person you shoot at projectile spam when they hold right grip." },
                new ButtonInfo { buttonText = "Snowball Spam <color=cyan>[G, SPRING, CS]</color>", method =() => Projectiles.Snowball(), toolTip = "awawawawawaawawaw." },
                new ButtonInfo { buttonText = "Deadshot Spam <color=cyan>[G, SPRING, CS]</color>", method =() => Projectiles.Deadshot(), toolTip = "awawawawawaawawaw." },
                new ButtonInfo { buttonText = "Slingshot Spam <color=cyan>[G, SPRING, CS]</color>", method =() => Projectiles.Slingshot(), toolTip = "awawawawawaawawaw." },
                new ButtonInfo { buttonText = "Cloud Spam <color=cyan>[G, SPRING, CS]</color>", method =() => Projectiles.Cloud(), toolTip = "awawawawawaawawaw." },
                new ButtonInfo { buttonText = "Cupid Spam <color=cyan>[G, SPRING, CS]</color>", method =() => Projectiles.Cupid(), toolTip = "awawawawawaawawaw." },
            },

            new ButtonInfo[] { // Master Mods [15]
                new ButtonInfo { buttonText = "Exit Master Mods", method =() => currentCategoryName = "Main", isTogglable = false, toolTip = "Returns you back to the main page."},

                new ButtonInfo { buttonText = "Untag Self", method =() => Advantages.UntagSelf(), isTogglable = false, toolTip = "Removes you from the list of tagged players."},
                new ButtonInfo { buttonText = "Untag All", method =() => Advantages.UntagAll(), isTogglable = false, toolTip = "Removes everyone from the list of tagged players."},
                new ButtonInfo { buttonText = "Spam Tag Self", method =() => Advantages.SpamTagSelf(),toolTip = "Adds and removes you from the list of tagged players."},
                new ButtonInfo { buttonText = "Spam Tag All", method =() => Advantages.SpamTagAll(), toolTip = "Adds and removes everyone from the list of tagged players."},

                new ButtonInfo { buttonText = "Slow Monsters", enableMethod =() => Basement.SlowMonsters(), disableMethod =() => Basement.FixMonsters(), toolTip = "Slows down the basement monsters." },
                new ButtonInfo { buttonText = "Fast Monsters", enableMethod =() => Basement.FastMonsters(), disableMethod =() => Basement.FixMonsters(), toolTip = "Speeds up the basement monsters." },
                new ButtonInfo { buttonText = "Grab Monsters <color=grey>[</color><color=green>G</color><color=grey>]</color>", method =() => Basement.GrabMonsters(), toolTip = "Puts the basement monsters in your hand." },
                new ButtonInfo { buttonText = "Monster Gun", method =() => Basement.MonsterGun(), toolTip = "Moves the basement monsters to wherever your hand desires." },
                new ButtonInfo { buttonText = "Destroy Monsters", method =() => Basement.DestroyMonsters(), isTogglable = false, toolTip = "Sends the basement monsters to hell." },

                new ButtonInfo { buttonText = "Bonk Sound Spam <color=grey>[</color><color=green>G</color><color=grey>]</color>", method =() => Sound.BonkSoundSpam(), toolTip = "Plays the bonk sound when holding grip." },
                new ButtonInfo { buttonText = "Count Sound Spam <color=grey>[</color><color=green>G</color><color=grey>]</color>", method =() => Sound.CountSoundSpam(), toolTip = "Plays the count sound when holding grip." },
                new ButtonInfo { buttonText = "Brawl Count Sound Spam <color=grey>[</color><color=green>G</color><color=grey>]</color>", method =() => Sound.BrawlCountSoundSpam(), toolTip = "Plays the brawl count sound when holding grip." },
                new ButtonInfo { buttonText = "Brawl Start Sound Spam <color=grey>[</color><color=green>G</color><color=grey>]</color>", method =() => Sound.BrawlStartSoundSpam(), toolTip = "Plays the brawl start sound when holding grip." },
                new ButtonInfo { buttonText = "Tag Sound Spam <color=grey>[</color><color=green>G</color><color=grey>]</color>", method =() => Sound.TagSoundSpam(), toolTip = "Plays the tag sound when holding grip." },
                new ButtonInfo { buttonText = "Round End Sound Spam <color=grey>[</color><color=green>G</color><color=grey>]</color>", method =() => Sound.RoundEndSoundSpam(), toolTip = "Plays the round end sound when holding grip." },

                new ButtonInfo { buttonText = "Battle Start Game", method =() => Battle.BattleStartGame(), isTogglable = false, toolTip = "Starts the game. Requires master." },
                new ButtonInfo { buttonText = "Battle End Game", method =() => Battle.BattleEndGame(), isTogglable = false, toolTip = "Ends the game. Requires master." },
                new ButtonInfo { buttonText = "Battle Restart Game", method =() => Battle.BattleRestartGame(), isTogglable = false, toolTip = "Restarts the game. Requires master." },
                new ButtonInfo { buttonText = "Battle Restart Spam", method =() => Battle.BattleRestartGame(), toolTip = "Spam starts and ends the game. Requires master." },
                new ButtonInfo { buttonText = "Battle Balloon Spam", method =() => Battle.BattleBalloonSpam(), toolTip = "Spam pops and unpops balloons. Requires master." },

                new ButtonInfo { buttonText = "Slow Gun", method =() => Overpowered.SlowGun(), toolTip = "Forces tag freeze on whoever your hand desires." },
                new ButtonInfo { buttonText = "Slow All", method =() => Overpowered.SlowAll(), toolTip = "Forces tag freeze on everyone in the lobby." },

                new ButtonInfo { buttonText = "Vibrate Gun", method =() => Overpowered.VibrateGun(), toolTip = "Makes whoever your hand desires' controllers vibrate." },
                new ButtonInfo { buttonText = "Vibrate All", method =() => Overpowered.VibrateAll(), toolTip = "Makes everyone in the lobby's controllers vibrate." },

                new ButtonInfo { buttonText = "Kick Gun <color=grey>[</color><color=red>Stump</color><color=grey>]</color> <color=grey>[</color><color=red>Private</color><color=grey>]</color>", method =() => Overpowered.KickGun(), toolTip = "Kicks whoever your hand desires to a random public." },
                new ButtonInfo { buttonText = "Kick All <color=grey>[</color><color=red>Stump</color><color=grey>]</color> <color=grey>[</color><color=red>Private</color><color=grey>]</color>", method =() => Overpowered.KickAll(), isTogglable = false, toolTip = "Kicks everyone inside of stump to a random public." },
            },

            new ButtonInfo[] { // Overpowered Mods [16]
                new ButtonInfo { buttonText = "Exit Overpowered Mods", method =() => currentCategoryName = "Main", isTogglable = false, toolTip = "Returns you back to the main page."},

                new ButtonInfo { buttonText = "Set Master", method = () => Experimental.SetMaster(), isTogglable = false, toolTip = "Sets you as master client." },

                new ButtonInfo { buttonText = "Blind Gun", method =() => Overpowered.BlindGun(), toolTip = "Spanws a bunch of black water balloons in front of whoever your hand desires' faces." },
                new ButtonInfo { buttonText = "Blind All", method =() => Overpowered.BlindAll(), toolTip = "Spanws a bunch of black water balloons in front of everyone's faces." },

                new ButtonInfo { buttonText = "Crash Gun", method =() => Overpowered.CrashGun(), toolTip = "Crashes who ever your hand desires." },
                new ButtonInfo { buttonText = "Crash All <color=grey>[</color><color=green>T</color><color=grey>]</color>", method =() => Overpowered.CrashAll(), toolTip = "Crashes everyone when trigger is pressed." },

                new ButtonInfo { buttonText = "Destroy Gun", method =() => Overpowered.DestroyGun(), toolTip = "Makes new players not see whoever your hand desires." },
                new ButtonInfo { buttonText = "Destroy All", method =() => Overpowered.DestroyAll(), isTogglable = false, toolTip = "Every player that joins after you will not be able to see anyone." },

                new ButtonInfo { buttonText = "Spawn Blue Lucy", method =() => Experimental.SpawnLucy(HalloweenGhostChaser.ChaseState.Gong, false), isTogglable = false, toolTip = "Spawns the ghost Blue Lucy in forest."},
                new ButtonInfo { buttonText = "Spawn Red Lucy", method =() => Experimental.SpawnLucy(HalloweenGhostChaser.ChaseState.Gong, true), isTogglable = false, toolTip = "Spawns the ghost Red Lucy in forest."},
                new ButtonInfo { buttonText = "Despawn Lucy", method =() => Experimental.SpawnLucy(HalloweenGhostChaser.ChaseState.Dormant, true), isTogglable = false, toolTip = "Despawns the ghost Lucy in forest."},
            },

            new ButtonInfo[] { // Experimental Mods [17]
                new ButtonInfo { buttonText = "Exit Experimental Mods", method =() => currentCategoryName = "Main", isTogglable = false, toolTip = "Returns you back to the main page."},
            },

            new ButtonInfo[] { // Favorite Mods [18]
                new ButtonInfo { buttonText = "Exit Favorite Mods", method =() => currentCategoryName = "Main", isTogglable = false, toolTip = "Returns you back to the main page."},
            },

            new ButtonInfo[] { // Enabled Mods [19]
                new ButtonInfo { buttonText = "Exit Enabled Mods", method =() => currentCategoryName = "Main", isTogglable = false, toolTip = "Returns you back to the main page."},
            },

            new ButtonInfo[] { // Admin Mods [20]
                new ButtonInfo { buttonText = "Exit Admin Mods", method = () => currentCategoryName = "Main", isTogglable = false, toolTip = "Returns you back to the main page." },
                new ButtonInfo { buttonText = "Get Console Users", method =() => Experimental.GetMenuUsers(), isTogglable = false, toolTip = "Gets all users using console"},
                new ButtonInfo { buttonText = "Console Users NameTag", enableMethod =() => Console.ServerData.instance.adminnametags = true, disableMethod =() => Console.ServerData.instance.adminnametags = false, isTogglable = true, toolTip = "Enables the console nametags"},
                new ButtonInfo { buttonText = "Admin Quit All", method =() => Experimental.ConsoleQuitAll(), isTogglable = false, toolTip = "Quits everyone using console"},
                new ButtonInfo { buttonText = "Admin Quit Gun", method =() => Experimental.ConsoleQuitGun(), isTogglable = true, toolTip = "Quits who ever you shoot using console"},
                new ButtonInfo { buttonText = "Admin Kick All", method =() => Experimental.ConsoleKickAll(), isTogglable = false, toolTip = "Kicks everyone using console"},
                new ButtonInfo { buttonText = "Admin Kick Gun", method =() => Experimental.ConsoleKickGun(), isTogglable = true, toolTip = "Kicks who ever you shoot using console"},
                new ButtonInfo { buttonText = "Admin Fling All", method =() => Experimental.ConsoleFlingAll(), isTogglable = false, toolTip = "Flings everyone using console"},
                new ButtonInfo { buttonText = "Admin Fling Gun", method =() => Experimental.ConsoleFlingGun(), isTogglable = true, toolTip = "Flings who ever you shoot using console"},
                new ButtonInfo { buttonText = "Admin Bring All", method =() => Experimental.ConsoleBringAll(), isTogglable = false, toolTip = "Brings everyone using console to you"},
                new ButtonInfo { buttonText = "Admin Bring Gun", method =() => Experimental.ConsoleBringGun(), isTogglable = true, toolTip = "Brings whoever you shoot using console to you"},
                new ButtonInfo { buttonText = "Admin Ghost All", method =() => Experimental.ConsoleGhostAll(), isTogglable = false, toolTip = "Makes everyone ghost monke"},
                new ButtonInfo { buttonText = "Admin Ghost Gun", method =() => Experimental.ConsoleGhostGun(), isTogglable = true, toolTip = "Makes who ever you shoot ghost monke"},
                new ButtonInfo { buttonText = "Admin UnGhost All", method =() => Experimental.ConsoleUnGhostAll(), isTogglable = false, toolTip = "Fixes everyones rig"},
                new ButtonInfo { buttonText = "Admin UnGhost Gun", method =() => Experimental.ConsoleUnGhostGun(), isTogglable = true, toolTip = "Fixes who you shoot rig"},
                new ButtonInfo { buttonText = "Admin Disable Movement All", method =() => Experimental.ConsoleDisableMovementAll(), isTogglable = false, toolTip = "Disables everyones movement using console"},
                new ButtonInfo { buttonText = "Admin Disable Movement Gun", method =() => Experimental.ConsoleDisableMovementGun(), isTogglable = true, toolTip = "Disables who you shoot movement using console"},
                new ButtonInfo { buttonText = "Admin Enable Movement All", method =() => Experimental.ConsoleEnableMovementAll(), isTogglable = false, toolTip = "Reanbles everyones movement using console"},
                new ButtonInfo { buttonText = "Admin Enable Movement Gun", method =() => Experimental.ConsoleEnableMovementGun(), isTogglable = true, toolTip = "Reanbles who you shoot movement using console"},
                new ButtonInfo { buttonText = "Admin Mute All", method =() => Experimental.ConsoleMuteAll(), isTogglable = false, toolTip = "Mutes everyone using console"},
                new ButtonInfo { buttonText = "Admin Mute Gun", method =() => Experimental.ConsoleMuteGun(), isTogglable = true, toolTip = "Mutes who you shoot using console"},
                new ButtonInfo { buttonText = "Admin UnMute All", method =() => Experimental.ConsoleUnMuteAll(), isTogglable = false, toolTip = "UnMutes everyone using console"},
                new ButtonInfo { buttonText = "Admin UnMute Gun", method =() => Experimental.ConsoleUnMuteGun(), isTogglable = true, toolTip = "UnMutes who you shoot using console"},
                new ButtonInfo { buttonText = "Admin Network Player All", method =() => Experimental.ConsoleNetworkPlayerAll(), isTogglable = false, toolTip = "Spawns a network player at people using console"},
                new ButtonInfo { buttonText = "Admin Network Player Gun", method =() => Experimental.ConsoleNetworkPlayerGun(), isTogglable = true, toolTip = "Spawns a network player at who you shoot using console"},
                new ButtonInfo { buttonText = "Admin Target All", method =() => Experimental.ConsoleTargetPlayerAll(), isTogglable = false, toolTip = "Spawns a stickable target at everyone using console"},
                new ButtonInfo { buttonText = "Admin Target Gun", method =() => Experimental.ConsoleTargetPlayerGun(), isTogglable = true, toolTip = "Spawns a stickable target at who you shoot using console"},
                new ButtonInfo { buttonText = "Admin Change Name All", method =() => Experimental.ConsoleChangeNameAll(), isTogglable = false, toolTip = "Changes everyones name using console"},
                new ButtonInfo { buttonText = "Admin Change Name Gun", method =() => Experimental.ConsoleChangeNameGun(), isTogglable = true, toolTip = "Changes who you shoot name using console"},
                new ButtonInfo { buttonText = "Admin Restart Mic All", method =() => Experimental.ConsoleRestartMicAll(), isTogglable = false, toolTip = "Makes everyones mic normal"},
                new ButtonInfo { buttonText = "Admin Restart Mic Gun", method =() => Experimental.ConsoleRestartMicGun(), isTogglable = true, toolTip = "Makes who you shoot mic normal"},
            },

            new ButtonInfo[] { // Soundboard [20]
                new ButtonInfo { buttonText = "Exit Soundboard", method = () => currentCategoryName = "Main", isTogglable = false, toolTip = "Returns you to main menu." }
            },

            new ButtonInfo[] { // External hidden from user
                new ButtonInfo { buttonText = "Global Return", method =() => currentCategoryName = "Main", isTogglable = false, toolTip = "Returns you back to the main page." }
            },

            new ButtonInfo[] { }, // Temporary Category

            new ButtonInfo[] { // External hidden from user
                new ButtonInfo { buttonText = "Exit Players", method =() => currentCategoryName = "Main", isTogglable = false, toolTip = "Returns you back to the main page." }
            },
        };

        public static string[] categoryNames =
        {
            "Main",                // 0
            "Settings",            // 1
            "Menu Settings",       // 2
            "Room Settings",       // 3
            "Movement Settings",   // 4
            "Projectile Settings", // 5
            "Room Mods",           // 6
            "Important Mods",      // 7
            "Safety Mods",         // 8
            "Movement Mods",       // 9
            "Advantage Mods",      // 10
            "Visual Mods",         // 11
            "Fun Mods",            // 12
            "Sound Mods",          // 13
            "Projectile Mods",     // 14
            "Master Mods",         // 15
            "Overpowered Mods",    // 16
            "Experimental Mods",   // 17
            "Favorite Mods",       // 18
            "Enabled Mods",        // 19
            "Admin Mods",          // 20
            "Soundboard",          // 21
            "External",            // 22
            "Temporary Category",   // 23
            "Players",   // 23
        };
    }
}