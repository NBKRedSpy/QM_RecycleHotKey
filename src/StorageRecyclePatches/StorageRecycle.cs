using MGSC;
using System;
using System.Collections.Generic;
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

            //Only the items storage.
            if(!Input.GetKeyDown(Plugin.Config.RecycleCurrentPageKey))
            {
                return;
            }

            ItemStorage storage = Component._tabsView.FirstSelectedTab()?.Content as ItemStorage;

            if(storage == null) return;



        }

        
    }
}
