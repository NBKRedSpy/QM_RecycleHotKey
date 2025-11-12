[h1]Quasimorph Recycle Hotkey[/h1]


All functionality is configurable.

This mod can optionally let the player use amputation weapons directly from their inventory, without needing to equip them. This feature is disabled by default.

Adds the following functions to the corpse screen:
[list]
[*]Hotkey for the recycle button.  Z by default.
[*]Hotkey to amputate all limbs.  A by default.
[*]Do not recycle certain items.  By default the items are: Ammo, Mine, Grenade, RepairKit, Parts.
[/list]

Optional and disabled by default:
[list]
[*]Allow amputations weapons in inventory to be used instead of needing to equip them.
[*]Do not require a weapon to amputate.
[*]Automatically recycle items and amputate all limbs when the recycle hotkey is pressed.
[*]Do not automatically close the corpse window if there is more than one tab.
[/list]

See the Configuration section below for the options.

[h1]Filtering Out Items[/h1]

Use the mod "I Don't Want That! (Filter pickup items)" to avoid picking up items that are not wanted after recycle/amputation.  For example: legs, hands, flesh, more meat than is inventory, etc.

[h1]Configuration[/h1]

This mod can either be configured via the MCM's Mod button on the main menu, or directly in the config file.
Currently some values can only be edited in the config file.

The configuration file will be created on the first game run and can be found at [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\QM_RecycleHotKey\config.json[/i].
[table]
[tr]
[td]Key
[/td]
[td]Default
[/td]
[td]Description
[/td]
[/tr]
[tr]
[td]RecycleCurrentPageKey
[/td]
[td]Z
[/td]
[td]Invokes the recycle/dismember action for the current page
[/td]
[/tr]
[tr]
[td]AmputateKey
[/td]
[td]A
[/td]
[td]Amputates all of the corpse's parts
[/td]
[/tr]
[tr]
[td]RecycleAlsoAmputates
[/td]
[td]false
[/td]
[td]If true, will also amputate when the recycling hotkey is pressed
[/td]
[/tr]
[tr]
[td]AmputateWithoutWeapon
[/td]
[td]false
[/td]
[td]If true, will execute the amputation action without requiring or using a weapon that can amputate.
[/td]
[/tr]
[tr]
[td]AllowAmputationWeaponFromInventory
[/td]
[td]false
[/td]
[td]If enabled, will allow amputation weapons to be used from the inventory instead of requiring them to be equipped.  If 'Amputate Without Weapon' is enabled, this option is ignored. The weapon priority is: Quick slots first, then items in backpack, sorted by price.  Bones are treated as if they have zero price. If there are multiple items with the same price, the weapon that is closest to the upper left of the storage will be used.
[/td]
[/tr]
[tr]
[td]DoNotRecycleSpecialItems
[/td]
[td]true
[/td]
[td]If true, will not recycle items in the [i]DoNotRecycleItems[/i] list.
[/td]
[/tr]
[tr]
[td]DoNotRecycleItems
[/td]
[td]Ammo, Mine, Grenade, RepairKit, Parts
[/td]
[td]The categories to not recycle.
[/td]
[/tr]
[tr]
[td]DoNoCloseWindowOnEmpty
[/td]
[td]false
[/td]
[td](See the DoNoCloseWindowOnEmpty Option section). If true, will prevent the game from automatically closing the corpse window if there is more than one tab."
[/td]
[/tr]
[/table]

To configure different hotkeys, valid key names can be found at the bottom of https://docs.unity3d.com/ScriptReference/KeyCode.html

There are some oddities such as the number 1 is actually Alpha1.

[h2]DoNoCloseWindowOnEmpty Option[/h2]

By default, the game will automatically close the corpse dialog if there was only one tab (one corpse, no piles around, etc.) and the corpse is completely empty when the Take All command is executed.  Normally the game doesn't include any tabs that were created by dropping items while that dialog is open.

Note:  At the time of writing, there is a bug where the game's "tab count" is incorrect and will always close the dialog if the current corpse tab is empty on "Take All".  A bug has been filed with the developers.

[h1]Support[/h1]

If you enjoy my mods and want to buy me a coffee, check out my [url=https://ko-fi.com/nbkredspy71915]Ko-Fi[/url] page.
Thanks!

[h1]Source Code[/h1]

Source code is available on GitHub at https://github.com/NBKRedSpy/QM_RecycleHotKey

[h1]Change Log[/h1]

[h2]2.1.0[/h2]
[list]
[*]Using Amputation from Inventory:
[list]
[*]Fixed: amputation weapon was not searching from upper left.
[*]Now prioritizes using a bone first.
[/list]
[*]Added MCM bindings for hotkeys.
[*]Updated MCM config and ModConfig to latest versions.
[/list]

[h2]2.0.2[/h2]
[list]
[*]Fix: Config's DoNotRecycleItems was being overwritten with the defaults on game start.
[list]
[*]Thanks to Steam user GolpeBaixo for reporting this.
[/list]
[/list]

[h2]2.0.1[/h2]
[list]
[*]Internal Additional error logging.
[*]Internal Fixed hotkeys repeating.
[/list]

[h2]2.0.0[/h2]
[list]
[*]Allow amputations weapons in inventory to be used instead of needing to equip them.
[/list]

[h2]1.7.3[/h2]
[list]
[*]Fixed config typo "Do no" instead of "Do not"
[/list]

[h2]1.7.1[/h2]
[list]
[*]Added color for the config only values.
[/list]

[h2]1.7.0[/h2]
[list]
[*]Added MCM
[/list]
