using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Core;
using CacheRemover.Internals;
using Logger = CacheRemover.Internals.Logger;
using CacheRemover.Utils;

#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
#pragma warning disable IDE0051 // Remove unused private members
#pragma warning disable IDE0040


namespace CacheRemover.Utils
{

    static class Events
    {
        public static event Action OnUiManagerInit;

        public static IEnumerator StartUI()
        {
            while (VRCUiManager.prop_VRCUiManager_0 == null)
                yield return null;
            while (GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)") == null)
                yield return null;

            OnUiManagerInit?.Invoke();
        }

        /*
        private static void OnInstanceChange(ApiWorld __0, ApiWorldInstance __1)
        {
            if (__0 == null || __1 == null) return;

            OnInstanceChanged?.DelegateSafeInvoke(__0, __1);
        }

        public static void DelegateSafeInvoke(this Delegate @delegate, params object[] args)
        {
            if (@delegate == null)
                return;

            foreach (Delegate @delegates in @delegate.GetInvocationList())
            {
                try
                {
                    @delegates.DynamicInvoke(args);
                }
                catch (Exception ex)
                {
                    Logger.Error("Error while invoking delegate:\n" + ex.ToString());
                }
            }
        }
        */
    }
}
