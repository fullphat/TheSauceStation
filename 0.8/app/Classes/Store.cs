using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using SauceStation;

namespace TheSauceStation
{
    public static class Store
    {

        public static Dictionary<string, SauceSupplier> Sauces = new Dictionary<string, SauceSupplier>();

        public static void AddSupplier(SauceSupplier supplier, SauceSupplier.EventSauceHandler handler = null)
        {
            if (supplier != null)
            {
                if (supplier.Initialise())
                {
                    supplier.OnCookingEvent += handler;
                    //supplier.OnAuthenticationCompleted += authHandler;
                    Sauces.Add(supplier.Info.SauceName, supplier);
                    Debug.WriteLine("_addSauce: added " + supplier.Info.FriendlyName);
                }
                else
                {
                    Debug.WriteLine("_addSauce: sauce refused Load(): " + supplier.LastError());
                }
            }
        }

        public static string GetIcon(string sauceName)
        {
            return Environment.CurrentDirectory + "\\icons\\web-" + sauceName + ".png";
        }


    }
}
