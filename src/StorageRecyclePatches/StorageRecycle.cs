using MGSC;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_RecycleHotKey.StorageRecyclePatches
{

    /// <summary>
    /// Handles recycling items from storage containers.
    /// Must be an Update compoonent since ItemsStorageView doesn't have an update method.
    /// </summary>
    public class StorageRecycle : UpdateComponent<InventoryScreen>   
    {
        override public void Update()
        {

            //Do not include the cargo shuttle 
            //TODO:  identify drop pod.
            if(!Input.GetKeyDown(Plugin.Config.RecycleCurrentPageKey) ||
                object.ReferenceEquals(Component._tabsView.FirstSelectedTab()._relatedWindow, 
                    Component?._shuttleCargoView))
            {
                return;
            }

            ItemStorage storage = Component._tabsView.FirstSelectedTab()?.Content as ItemStorage;

            if(storage == null) return;

            //Recycle all items in the storage.
            DisassembleAllItems(storage);
        }

        private void DisassembleAllItems(ItemStorage storage)
        {
            //COPY WARNING: This is a logical copy of CorpseInspectWindow.DisassemblyAllItems()

            //Using the the same variables as the decompiled version for easier comparison.

            bool flag = false;  //any item was disassembled.
            bool flag2 = false; //A weapon was disassembled.

            List<BasePickupItem> list = new List<BasePickupItem>(storage.Items);

            for (int i = 0; i < list.Count ; i++)
            {
                if (ItemInteractionSystem.CanDisassemble(list[i]))
                {
                    UI.Get<InventoryScreen>().DisassembleItem(list[i], -1, spendTurn: true, playSound: false);
                    flag2 |= UI.Get<InventoryScreen>().TryUnloadWeapon(list[i], playSound: false);
                    flag = true;
                }
            }
            if (flag)
            {
                AudioClip clip = (flag2 ? SingletonMonoBehaviour<SoundsStorage>.Instance.AmmoReceived : SingletonMonoBehaviour<SoundsStorage>.Instance.EmptyAttack);
                SingletonMonoBehaviour<SoundController>.Instance.PlayUiSound(clip);
                PlayerInteractionSystem.EndPlayerTurn(Component._creatures, PlayerEndTurnContext.InventoryInteraction);
                Component._creatures.Player.CreatureData.EffectsController.PropagateAction(PlayerActionHappened.HandAction);


                //Sort and refresh the tabs.
                storage.SortWithExpandByTypeAndName(Bootstrap._state.Get<SpaceTime>());
                Component._tabsView.RefreshAllTabs();
            }

        }
    }

}
