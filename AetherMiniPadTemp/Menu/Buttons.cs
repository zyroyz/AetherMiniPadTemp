using GorillaTag.Cosmetics.Summer;
using StupidTemplate.Classes;
using StupidTemplate.Menu;
using StupidTemplate.Mods;
using static StupidTemplate.Settings;
using UnityEngine;
using GorillaTag.Cosmetics;
using StupidTemplate;


namespace AetherTemp.Menu
{
    internal class Buttons
    {
        public static ButtonInfo[][] buttons = new ButtonInfo[][]
        {
            new ButtonInfo[] { // Main Mods
                new ButtonInfo { buttonText = "Settings", method =() => SettingsMods.MainSettings(), isTogglable = false, toolTip = "Opens the main settings page for the menu."},
                new ButtonInfo { buttonText = "advantages", method =() => SettingsMods.advantages(), isTogglable = false, toolTip = "Opens the advantages page for the menu."},
                new ButtonInfo { buttonText = "visuals", method =() => SettingsMods.visuals(), isTogglable = false, toolTip = "Opens the visuals page for the menu."},
                new ButtonInfo { buttonText = "movement", method =() => SettingsMods.movement(), isTogglable = false, toolTip = "Opens the movement page for the menu."},
                new ButtonInfo { buttonText = "overpowered", method =() => SettingsMods.overpowered(), isTogglable = false, toolTip = "Opens the overpowered page for the menu."},
                new ButtonInfo { buttonText = "safety", method =() => SettingsMods.safety(), isTogglable = false, toolTip = "Opens the safety page for the menu."},
                new ButtonInfo { buttonText = "fun", method =() => SettingsMods.fun(), isTogglable = false, toolTip = "Opens the fun page for the menu."},
                new ButtonInfo { buttonText = "guardian", method =() => SettingsMods.guardian(), isTogglable = false, toolTip = "Opens the guardian page for the menu."},
            },

            new ButtonInfo[] { // Main Settings
                new ButtonInfo { buttonText = "home", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Opens the home for the menu."},
                new ButtonInfo { buttonText = "Menu Settings", method =() => SettingsMods.MenuSettings(), isTogglable = false, toolTip = "Opens the Menu settings for the menu."},
                new ButtonInfo { buttonText = "GunLib Settings", method =() => SettingsMods.GunLib(), isTogglable = false, toolTip = "Opens the GunLib settings for the menu."},
                new ButtonInfo { buttonText = "Notifications Settings", method =() => SettingsMods.Notification(), isTogglable = false, toolTip = "Opens the GunLib settings for the menu."},
                new ButtonInfo { buttonText = "Projectile Settings", method =() => SettingsMods.ProjectileSettings(), isTogglable = false, toolTip = "Opens the GunLib settings for the menu."},
            },

            new ButtonInfo[] { // Advantages
                new ButtonInfo { buttonText = "home", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Opens the home for the menu."},
                new ButtonInfo { buttonText = "placeholder", method =() => mods.placeholder(), isTogglable = false, toolTip = "placeholder."},
                new ButtonInfo { buttonText = "Gun Temp", method =() => mods.GunTemplate(), isTogglable = true, toolTip = "placeholder."},
            },

            new ButtonInfo[] { // Movement
                new ButtonInfo { buttonText = "home", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Opens the home for the menu."},
                new ButtonInfo { buttonText = "placeholder", method =() => mods.placeholder(), isTogglable = true, toolTip = "placeholder."},
            },

            new ButtonInfo[] { // visuals
                new ButtonInfo { buttonText = "home", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Opens the home for the menu."},
                new ButtonInfo { buttonText = "placeholder", method =() => mods.placeholder(), isTogglable = false, toolTip = "placeholder."},
            },

            new ButtonInfo[] { // overpowered
                new ButtonInfo { buttonText = "home", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Opens the home for the menu."},
                new ButtonInfo { buttonText = "placeholder", method =() => mods.placeholder(), isTogglable = true, toolTip = "placeholder."},
            },

            new ButtonInfo[] { // safety
                new ButtonInfo { buttonText = "home", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Opens the home for the menu."},
                new ButtonInfo { buttonText = "placeholder", method =() => mods.placeholder(), isTogglable = true, toolTip = "placeholder."},
            },

            new ButtonInfo[] { // fun
                new ButtonInfo { buttonText = "home", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Opens the home for the menu."},
                 new ButtonInfo { buttonText = "Projectiles", method =() => SettingsMods.Projectiles(), isTogglable = false, toolTip = "placeholder."},
                 new ButtonInfo { buttonText = "Particles", method =() => SettingsMods.Particles(), isTogglable = false, toolTip = "placeholder."},
            },

            new ButtonInfo[] { // guardian
                new ButtonInfo { buttonText = "home", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Opens the home for the menu."},
                new ButtonInfo { buttonText = "placeholder", method =() => SettingsMods.Projectiles(), isTogglable = false, toolTip = "placeholder."},
            },



            new ButtonInfo[] { // GunLib
                new ButtonInfo { buttonText = "home", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Opens the home for the menu."},
                new ButtonInfo { buttonText = "Equip Gun", method =() => mods.GunTemplate(), isTogglable = true, toolTip = "Equips a gun."},
                new ButtonInfo { buttonText = $"Smoothness: {(mods.num == 5f ? "Very Fast" : mods.num == 10f ? "Normal" : "Super Smooth")}", method = () => { mods.GunSmoothNess(); foreach (var category in Buttons.buttons) foreach (var button in category) if (button.buttonText.StartsWith("Smoothness")) button.buttonText = $"Smoothness: {(mods.num == 5f ? "Super Smooth" : mods.num == 10f ? "Normal" : "No Smooth")}"; }, isTogglable = false, toolTip = "Changes gun smoothness." },
                new ButtonInfo { buttonText = $"Gun Color: {mods.currentGunColor.name}", method = () => { mods.CycleGunColor(); Buttons.buttons.ForEach(category => category.ForEach(button => { if (button.buttonText.StartsWith("Gun Color")) button.buttonText = $"Gun Color: {mods.currentGunColor.name}"; })); }, isTogglable = false, toolTip = "Cycles through gun colors." },
                new ButtonInfo { buttonText = $"Toggle Sphere Size: {(mods.isSphereEnabled ? "Enabled" : "Disabled")}", method = () => { mods.isSphereEnabled = !mods.isSphereEnabled; if (mods.GunSphere != null) mods.GunSphere.transform.localScale = mods.isSphereEnabled ? new Vector3(0.1f, 0.1f, 0.1f) : new Vector3(0f, 0f, 0f); foreach (var category in Buttons.buttons) foreach (var button in category) if (button.buttonText.StartsWith("Toggle Sphere Size")) button.buttonText = $"Toggle Sphere Size: {(mods.isSphereEnabled ? "Enabled" : "Disabled")}"; }, isTogglable = false, toolTip = "Toggles the size of the gun sphere." }
            },

            new ButtonInfo[] { // MenuSettings
                new ButtonInfo { buttonText = "home", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Opens the home for the menu."},
                new ButtonInfo { buttonText = "Right/Left Hand", enableMethod =() => SettingsMods.RightHand(), disableMethod =() => SettingsMods.LeftHand(), toolTip = "Puts the menu on your right hand."},
                new ButtonInfo { buttonText = "FPS Counter", enableMethod =() => SettingsMods.EnableFPSCounter(), disableMethod =() => SettingsMods.DisableFPSCounter(), enabled = fpsCounter, toolTip = "Toggles the FPS counter."},
                new ButtonInfo { buttonText = "Disconnect Button", enableMethod =() => SettingsMods.EnableDisconnectButton(), disableMethod =() => SettingsMods.DisableDisconnectButton(), enabled = disconnectButton, toolTip = "Toggles the disconnect button."},
                new ButtonInfo { buttonText = $"Delete Time: {(Main.num == 2f ? "Default" : Main.num == 5f ? "Long" : "Fast")}", method = () => { Main.MenuDeleteTime(); foreach (var category in Buttons.buttons) foreach (var button in category) if (button.buttonText.StartsWith("Delete Time")) button.buttonText = $"Delete Time: {(Main.num == 2f ? "Default" : Main.num == 5f ? "Long" : "Fast")}"; }, isTogglable = false, toolTip = "Changes menu delete time." },
                new ButtonInfo { buttonText = $"Trail: {(Main.trailObject?.activeSelf == true ? "Enabled" : "Disabled")}", method = () => { if (Main.trailObject != null) { Main.trailObject.SetActive(!Main.trailObject.activeSelf); foreach (var category in Buttons.buttons) foreach (var button in category) if (button.buttonText.StartsWith("Trail")) button.buttonText = $"Trail: {(Main.trailObject.activeSelf ? "Enabled" : "Disabled")}"; } }, isTogglable = false, toolTip = "Toggles the trail effect on and off." }

    },

            new ButtonInfo[] { // Notifications
                new ButtonInfo { buttonText = "home", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Opens the home for the menu."},
                new ButtonInfo { buttonText = "Notifications", enableMethod =() => SettingsMods.EnableNotifications(), disableMethod =() => SettingsMods.DisableNotifications(), enabled = !disableNotifications, toolTip = "Toggles the notifications."},
            },

            new ButtonInfo[] { // proj settings
                new ButtonInfo { buttonText = "home", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Opens the home for the menu."},
                new ButtonInfo { buttonText = "placeholder", method =() => mods.placeholder(), isTogglable = true, toolTip = "placeholder."},


            },

            new ButtonInfo[] { // projectiles
                new ButtonInfo { buttonText = "home", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Opens the home for the menu."},
                new ButtonInfo { buttonText = "placeholder", method =() => mods.placeholder(), isTogglable = true, toolTip = "placeholder."},

            },

            new ButtonInfo[] { // projectiles
                new ButtonInfo { buttonText = "home", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Opens the home for the menu."},
                new ButtonInfo { buttonText = "Explosion", method =() => Particles.CreateDomain(), isTogglable = true, toolTip = "makes a Explosion."},
                new ButtonInfo { buttonText = "Sukuna's domain", method =() => Particles.CreateDomain2(), isTogglable = true, toolTip = "Sukuna's domain."},
                new ButtonInfo { buttonText = "Fire Fists", method =() => Particles.CreateFireEffect(), isTogglable = true, toolTip = "gives you Fire Fists."},
                new ButtonInfo { buttonText = "Black Hole", method =() => Particles.CreateBlackHole(), isTogglable = true, toolTip = "makes a Black Hole."},
                new ButtonInfo { buttonText = "White Hole", method =() => Particles.CreateWhiteHole(), isTogglable = true, toolTip = "makes a White Hole."},
                new ButtonInfo { buttonText = "Lighting", method =() => Particles.CreateLightningEffect(), isTogglable = true, toolTip = "Lighting."},
                new ButtonInfo { buttonText = "Magic Spell", method =() => Particles.CastMagicSpell(), isTogglable = true, toolTip = "cast a Magic Spell."},
                new ButtonInfo { buttonText = "Spark Magic Spell", method =() => Particles.CastSparkMagic(), isTogglable = true, toolTip = "cast a Spark Magic Spell."},
                new ButtonInfo { buttonText = "Light Magic Spell", method =() => Particles.CastLightMagic(), isTogglable = true, toolTip = "cast a Light Magic Spell."},
                new ButtonInfo { buttonText = "FireBall Magic Spell", method =() => Particles.CastFireballMagic(), isTogglable = true, toolTip = "cast a FireBall."},
                new ButtonInfo { buttonText = "Sword Slash", method =() => Particles.SwordSlash(), isTogglable = true, toolTip = "make a Sword Slash."},
                new ButtonInfo { buttonText = "Lighting Bolt Magic", method =() => Particles.CastLightningBolt(), isTogglable = true, toolTip = "Lighting Bolt Magic."},
                new ButtonInfo { buttonText = "Rift Magic", method =() => Particles.CastVoidRift(), isTogglable = true, toolTip = "Rift Magic."},
                new ButtonInfo { buttonText = "Frost Orb Magic", method =() => Particles.CastFrostOrb(), isTogglable = true, toolTip = "Frost Orb Magic."},
                new ButtonInfo { buttonText = "Nebula Storm", method =() => Particles.CreateNebulaStorm(), isTogglable = true, toolTip = "Nebula Storm."},
                new ButtonInfo { buttonText = "Draw", method =() => Particles.Draw(), isTogglable = true, toolTip = "Draw."},
            },



            //always keep this at the bottom if you add another tab (by going to categories) make sure you put that section above this one:

             new ButtonInfo[] {
                 new ButtonInfo { buttonText = "Disconnect", method =() => mods.Disconnect(), isTogglable = false, toolTip = "Disconnected from lobby"},
             },
           
            new ButtonInfo[] {
                new ButtonInfo { buttonText = "home", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Opens the home for the menu."},
                new ButtonInfo { buttonText = "nextpage", method =() => Main.NextPage(), isTogglable = true,enabled = true, toolTip = "Opens the home for the menu."},
                new ButtonInfo { buttonText = "lastpage", method =() => Main.PreviousPage(), isTogglable = true,enabled = true, toolTip = "Opens the home for the menu."},
            },

        };
    }
}
