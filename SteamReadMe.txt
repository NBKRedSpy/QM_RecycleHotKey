[h1]Quasimorph Recycle Hotkey[/h1]


All functionality is configurable.

Adds the following functions to the corpse screen:
[list]
[*]Hotkey for the recycle button.  Z by default.
[*]Hotkey to amputate all limbs.  A by default.
[*]Do not recycle certain items.  By default the items are: Ammo, Mine, Grenade, RepairKit, Parts.
[/list]

Optional and disabled by default:
[list]
[*]Do not require a weapon to amputate.
[*]Automatically recycle items and amputate all limbs when the recycle hotkey is pressed.
[/list]

See the Configuration section below for the options.

[h1]Filtering Out Items[/h1]

Use the mod "I Don't Want That! (Filter pickup items)" to avoid picking up items that are not wanted after recycle/amputation.  For example: legs, hands, flesh, more meat than is inventory, etc.

[h1]Configuration[/h1]

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
[/table]

To configure different hotkeys, valid key names can be found at the bottom of https://docs.unity3d.com/ScriptReference/KeyCode.html

There are some oddities such as the number 1 is actually Alpha1.

[h1]Support[/h1]

If you enjoy my mods and want to buy me a coffee, check out my [url=https://ko-fi.com/nbkredspy71915]Ko-Fi[/url] page.
Thanks!

[h1]Source Code[/h1]

Source code is available on GitHub at https://github.com/NBKRedSpy/QM_RecycleHotKey

[h1]Change Log[/h1]

[h2]1.6.0[/h2]
[list]
[*]Added DoNotRecycleSpecialItems to not recycle common items like ammo.
[/list]

[h2]1.5.0[/h2]

Fix: This mod was preventing the "I Don't Want That!" from filtering amputated items when the AmputateWithoutWeapon option was enabled.
Thank you to Discord user Necrosx for reporting the issue.

[h2]1.4.0[/h2]
[list]
[*]Version 0.8.6 compatibility
[/list]

[h2]1.3.0[/h2]
[list]
[*]v0.8.5 compatible.
[/list]

[h2]1.2.0[/h2]
[list]
[*]Moved config file directory.
[/list]

[h2]1.1.0[/h2]
[list]
[*]Added option to amputate without a melee weapon.
[list]
[*]Suggested by Steam user VETXP
[/list]
[/list]
