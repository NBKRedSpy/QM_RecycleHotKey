# Quasimorph Recycle Hotkey

![thumbnail icon](media/thumbnail.png)

All functionality is configurable. 

This mod can optionally let the player use amputation weapons directly from their inventory, without needing to equip them. This feature is disabled by default.

Adds the following functions to the corpse screen:
* Hotkey for the recycle button.  Z by default.
* Hotkey to amputate all limbs.  A by default.
* Do not recycle certain items.  By default the items are: Ammo, Mine, Grenade, RepairKit, Parts.  

Optional and disabled by default:
* Allow amputations weapons in inventory to be used instead of needing to equip them.
* Do not require a weapon to amputate.
* Automatically recycle items and amputate all limbs when the recycle hotkey is pressed.
* Do not automatically close the corpse window if there is more than one tab.

See the [Configuration](#configuration) section below for the options.

# Filtering Out Items
Use the mod "I Don't Want That! (Filter pickup items)" to avoid picking up items that are not wanted after recycle/amputation.  For example: legs, hands, flesh, more meat than is inventory, etc.

# Configuration

This mod can either be configured via the MCM's Mod button on the main menu, or directly in the config file.
Currently some values can only be edited in the config file.

The configuration file will be created on the first game run and can be found at `%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\QM_RecycleHotKey\config.json`.

|Key|Default|Description|
|--|--|--|
|RecycleCurrentPageKey|Z|Invokes the recycle/dismember action for the current page|
|AmputateKey|A|Amputates all of the corpse's parts|
|RecycleAlsoAmputates|false|If true, will also amputate when the recycling hotkey is pressed|
|AmputateWithoutWeapon|false|If true, will execute the amputation action without requiring or using a weapon that can amputate.|
|AllowAmputationWeaponFromInventory|false| If enabled, will allow amputation weapons to be used from the inventory instead of requiring them to be equipped.  If 'Amputate Without Weapon' is enabled, this option is ignored. The weapon priority is: Quick slots first, then items in backpack, sorted by price (cheapest first). If there are multiple items with the same price, the weapon that is closest to the upper left of the storage will be used.|
|DoNotRecycleSpecialItems|true|If true, will not recycle items in the `DoNotRecycleItems` list.|
|DoNotRecycleItems|Ammo, Mine, Grenade, RepairKit, Parts|The categories to not recycle.|
|DoNoCloseWindowOnEmpty|false|(See the [DoNoCloseWindowOnEmpty Option section](#donoclosewindowonempty-option)). If true, will prevent the game from automatically closing the corpse window if there is more than one tab."

To configure different hotkeys, valid key names can be found at the bottom of https://docs.unity3d.com/ScriptReference/KeyCode.html

There are some oddities such as the number 1 is actually Alpha1.

## DoNoCloseWindowOnEmpty Option
By default, the game will automatically close the corpse dialog if there was only one tab (one corpse, no piles around, etc.) and the corpse is completely empty when the Take All command is executed.  Normally the game doesn't include any tabs that were created by dropping items while that dialog is open.

Note:  At the time of writing, there is a bug where the game's "tab count" is incorrect and will always close the dialog if the current corpse tab is empty on "Take All".  A bug has been filed with the developers.

# Support
If you enjoy my mods and want to buy me a coffee, check out my [Ko-Fi](https://ko-fi.com/nbkredspy71915) page.
Thanks!

# Source Code
Source code is available on GitHub at https://github.com/NBKRedSpy/QM_RecycleHotKey

# Change Log

## 2.0.1
* Internal Additional error logging.
* Internal Fixed hotkeys repeating.

## 2.0.0
* Allow amputations weapons in inventory to be used instead of needing to equip them.

## 1.7.3
* Fixed config typo "Do no" instead of "Do not"

## 1.7.1
* Added color for the config only values.

## 1.7.0
* Added MCM
