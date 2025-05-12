# Quasimorph Recycle Hotkey

![thumbnail icon](media/thumbnail.png)

All functionality is configurable. 

Adds the following functions to the corpse screen:
* Hotkey for the recycle button.  Z by default.
* Hotkey to amputate all limbs.  A by default.
* Do not recycle certain items.  By default the items are: Ammo, Mine, Grenade, RepairKit, Parts.  

Optional and disabled by default:
* Do not require a weapon to amputate.
* Automatically recycle items and amputate all limbs when the recycle hotkey is pressed.
* Do not automatically close the corpse window if there is more than one tab.

See the [Configuration](#configuration) section below for the options.

# Filtering Out Items
Use the mod "I Don't Want That! (Filter pickup items)" to avoid picking up items that are not wanted after recycle/amputation.  For example: legs, hands, flesh, more meat than is inventory, etc.

# Configuration

The configuration file will be created on the first game run and can be found at `%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\QM_RecycleHotKey\config.json`.

|Key|Default|Description|
|--|--|--|
|RecycleCurrentPageKey|Z|Invokes the recycle/dismember action for the current page|
|AmputateKey|A|Amputates all of the corpse's parts|
|RecycleAlsoAmputates|false|If true, will also amputate when the recycling hotkey is pressed|
|AmputateWithoutWeapon|false|If true, will execute the amputation action without requiring or using a weapon that can amputate.|
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

## 1.6.2
* Fix: DoNoCloseWindowOnEmpty was inverted and not enabled by default.

## 1.6.1
* Added DoNoCloseWindowOnEmpty option.

## 1.6.0
* Added DoNotRecycleSpecialItems to not recycle common items like ammo.

## 1.5.0
Fix: This mod was preventing the "I Don't Want That!" from filtering amputated items when the AmputateWithoutWeapon option was enabled.
Thank you to Discord user Necrosx for reporting the issue.

## 1.4.0
* Version 0.8.6 compatibility

## 1.3.0
* v0.8.5 compatible.

## 1.2.0
* Moved config file directory.

## 1.1.0
* Added option to amputate without a melee weapon.  
    * Suggested by Steam user VETXP


