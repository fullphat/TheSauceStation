using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using SauceStation;
using SauceStation.Services;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Reflection;
using System.Xml;

namespace TheSauceStation
{
    public partial class Form1 : Form
    {


        //private static Dictionary<string,EventSauce> mySauces = new Dictionary<string,EventSauce>();
        //private static Recipe currentRecipe = new Recipe();
        //private static string lastLookupText = "";

        private static List<Recipe> _recipes = new List<Recipe>();

        private const string SWS_SIGNATURE = "the_sauce_station";

        public Form1()
        {
            InitializeComponent();
            Stuff.RegisterAppInRegistry(Stuff.BrowserEmulationValue.IE8Always);
        }

        private void _RegisterRecipeWithSnarl(Recipe recipe)
        {
            if (recipe != null)
            {
                if (recipe.IsValid())
                {
                    Snarl.DoRequest("register" +
                        "?app-sig=" + SWS_SIGNATURE + "/" + recipe.Sauce.Name +
                        "&title=" + recipe.Sauce.FriendlyName +
                        "&icon=" + Store.GetIcon(recipe.Sauce.Name) +
                        "&hint=" + recipe.Sauce.Description);

                    Snarl.DoRequest("addclass" +
                                    "?app-sig=" + SWS_SIGNATURE + "/" + recipe.Sauce.Name +
                                    "&id=" + recipe.Guid +
                                    "&name=" + recipe.Action());
                }
            }
        }


        //private void _uninitialiseSnarl()
        //{
        //    Snarl.DoRequest("unregister?app-sig=" + SWS_SIGNATURE + "/yweather");
        //    Snarl.DoRequest("unregister?app-sig=" + SWS_SIGNATURE + "/facebook");
        //    Snarl.DoRequest("unregister?app-sig=" + SWS_SIGNATURE + "/tumblr");
        //    Snarl.DoRequest("unregister?app-sig=" + SWS_SIGNATURE + "/flickr");
        //    Snarl.DoRequest("unregister?app-sig=" + SWS_SIGNATURE + "/twitter");
        //    Snarl.DoRequest("unregister?app-sig=" + SWS_SIGNATURE + "/500px");
        //    Snarl.DoRequest("unregister?app-sig=" + SWS_SIGNATURE + "/feedly");
        //    Snarl.DoRequest("unregister?app-sig=" + SWS_SIGNATURE + "/dropbox");
        //    Snarl.DoRequest("unregister?app-sig=" + SWS_SIGNATURE + "/instagram");
        //}



        //private string _TranslateIngredients(Sauce trigger)
        //{
        //    string result = "";
        //    string type = "";

        //    foreach (Ingredient ingredient in trigger.Ingredients)
        //    {
        //        switch (ingredient.Type)
        //        {
        //            case "string array":
        //                type = "comma_separated_list";
        //                break;

        //            default:
        //                type = ingredient.Type;
        //                break;
                        
        //        }
        //        //result += ingredient.Name + "={" + ((ingredient.IsRequired) ? "required" : "optional") + "};";
        //        result += ingredient.Name + "={" + type + "};";
        //    }
        //    return result.TrimEnd(';');
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            //_SetStyle(lblIngredientHint, ControlStyles.SupportsTransparentBackColor, true);
            //lblIngredientHint.BackColor = Color.Transparent;
           
            // manually create and add some providers (for now)
            Store.AddSupplier(new Instagram(), OnSomeRecipeReady);
            Store.AddSupplier(new FiveHundredPx(), OnSomeRecipeReady);
            Store.AddSupplier(new Dropbox(), OnSomeRecipeReady);
            Store.AddSupplier(new Flickr(), OnSomeRecipeReady);
            //Store.AddSauce(new Facebook(), OnSomeRecipeReady, OnAuthCompleted);
            Store.AddSupplier(new Space(), OnSomeRecipeReady);
            Store.AddSupplier(new TMDB(), OnSomeRecipeReady);
            Store.AddSupplier(new YahooWeather(), OnSomeRecipeReady);
            Store.AddSupplier(new Tumblr(), OnSomeRecipeReady);


            foreach (KeyValuePair<string,SauceSupplier> es in Store.Sauces)
            {
                Image bm = Bitmap.FromFile(Environment.CurrentDirectory + @"\icons\small\" + es.Value.Name + ".png");
                imageList1.Images.Add(es.Value.Name, bm);
            }

            // get recipes
            _LoadRecipes();

        }


        private void OnSomeRecipeReady(Recipe recipe)
        {
            Debug.WriteLine("=== RECIPE READY ===");
            Debug.WriteLine("guid   >" + recipe.Guid);
            Debug.WriteLine("action >" + recipe.Action());
            Debug.WriteLine("sauce  >" + recipe.Sauce.Name + " (" + recipe.Sauce.FriendlyName + ")");
            Debug.WriteLine("trigger>" + recipe.Trigger.Name);
            Debug.WriteLine("uid    >" + recipe.EventInfo.Uid);

            StringBuilder extra = new StringBuilder();
            foreach (KeyValuePair<string,string> entry in recipe.EventInfo.ExtraData)
            {
                extra.Append("&" + entry.Key + "=" + entry.Value);
            }

            // register app and event now in case Snarl vanished and came back
            _RegisterRecipeWithSnarl(recipe);

            int hr = Snarl.NewDoRequest("notify" +
                                       "?app-sig=" + SWS_SIGNATURE + "/" + recipe.Sauce.Name +
                                       "&id=" + recipe.Guid +
                                       "&title=" + recipe.EventInfo.Title +
                                       "&text=" + recipe.EventInfo.Text +
                                       "&icon=" + (recipe.EventInfo.Icon != "" ? recipe.EventInfo.Icon : ".") +
                                       (recipe.EventInfo.Uid != "" ? "&uid=" + recipe.EventInfo.Uid : "") +
                                       (recipe.EventInfo.Callback != "" ? "&callback=" + recipe.EventInfo.Callback : "") +
                                       extra.ToString()
                                       );

            Debug.WriteLine(hr.ToString());
            Debug.WriteLine("=== DONE ===");
        }


        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            foreach (Recipe recipe in _recipes)
            {
                Debug.WriteLine("unloading: " + recipe.Sauce.Name + "...");
                Snarl.DoRequest("unregister?app-sig=" + SWS_SIGNATURE + "/" + recipe.Sauce.Name);
            }
            Debug.WriteLine("closing...");
        }



        

        private bool _SaveRecipes()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                       "full phat",
                                       "sauce station",
                                       "recipes.conf");

            XmlTextWriter w = new XmlTextWriter(path, Encoding.UTF8);
            w.WriteStartDocument();
            w.WriteStartElement("config");
            w.WriteStartElement("recipes");

            foreach (Recipe recipe in _recipes)
            {
                w.WriteStartElement("recipe");
                w.WriteElementString("sauce", recipe.Sauce.Name);
                w.WriteElementString("event", recipe.Trigger.Name);
                w.WriteStartElement("ingredients");
                foreach (RecipeIngredient ri in recipe.Ingredients)
                {
                    w.WriteStartElement("ingredient");
                    w.WriteElementString("name", ri.Ingredient.Name);
                    w.WriteElementString("value", ri.Value);
                    w.WriteElementString("friendly", ri.Friendly);
                    w.WriteEndElement();
                }
                w.WriteEndElement();
                w.WriteEndElement();
            }
            w.WriteEndElement();                // close recipes
            w.WriteEndElement();                // close config
            w.WriteEndDocument();
            w.Close();
            return true;
        }


        private bool _LoadRecipes()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                       "full phat",
                                       "sauce station");

            Directory.CreateDirectory(path);
            path = Path.Combine(path, "recipes.conf");
            if (File.Exists(path))
            {
                // load it up
                XmlDocument config = new XmlDocument();
                config.Load(path);

                XmlNode recipes = config.SelectSingleNode("config/recipes");
                if (recipes != null)
                {
                    foreach (XmlNode recipe in recipes)
                    {
                        Recipe loadedRecipe = new Recipe();
                        loadedRecipe.Sauce = Store.Sauces[recipe["sauce"].InnerText];
                        if (loadedRecipe.Sauce != null)
                        {
                            string eventName = recipe["event"].InnerText;
                            if (loadedRecipe.Sauce.HasTrigger(eventName))
                            {
                                loadedRecipe.Trigger = loadedRecipe.Sauce.Triggers().Find(i => i.Name == eventName);
                                if (loadedRecipe.IsValid())
                                {
                                    // set ingredients
                                    XmlNode ingredients = recipe["ingredients"];
                                    foreach (XmlNode ingredient in ingredients)
                                    {
                                        RecipeIngredient ri = new RecipeIngredient();
                                        string name = ingredient["name"].InnerText;
                                        ri.Ingredient = loadedRecipe.Trigger.Ingredients.Find(i => i.Name == name);
                                        if (ri.Ingredient != null)
                                        {
                                            if (ingredient["value"] != null)
                                                ri.Value = ingredient["value"].InnerText;

                                            if (ingredient["friendly"] != null)
                                                ri.Friendly = ingredient["friendly"].InnerText;

                                            Debug.WriteLine("adding ingredient '" + ri.Ingredient.Name + "':" + ri.Value + " (" + ri.Friendly + ")");
                                            loadedRecipe.Ingredients.Add(ri);
                                        }
                                        else
                                        {
                                            Debug.Write("**couldn't find ingredient '" + name + "' in event**");
                                        }
                                    }
                                    _AddRecipe(loadedRecipe);
                                }
                                else
                                {
                                    Debug.Write("**invalid recipe (not valid) skipped**");
                                }
                            }
                            else
                            {
                                Debug.Write("**invalid recipe (bad event name) skipped**");
                            }
                        }
                        else
                        {
                            Debug.Write("**invalid recipe (bad sauce name) skipped**");
                        }
                    }
                }
            }
            return true;
        }

        //private string _GetIcon(string sauceName)
        //{
        //    return Environment.CurrentDirectory + "\\icons\\web-" + sauceName + ".png";
        //}





        //void OnAuthCompleted(SauceSupplier sauce, AuthorizationWindow.AuthEventArgs eventArgs)
        //{
        //    // called when the full OAuth process completes
        //    Debug.WriteLine("** OAuth for " + sauce.FriendlyName + " completed ** [Success=" + eventArgs.success + "]");
        //    if (eventArgs.success)
        //    {
        //        MessageBox.Show(sauce.FriendlyName + " is now authenticated!", "Authorised", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    else
        //    {
        //        MessageBox.Show(sauce.FriendlyName + "failed to authenticate", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //}

        private void bnAddRecipe_Click(object sender, EventArgs e)
        {
            //if (!currentRecipe.IsValid())
            //{
            //    MessageBox.Show("Recipe wasn't created properly - try selecting a different sauce.", "Gah", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //if (!currentRecipe.Sauce.IsReady())
            //{
            //    SauceInfo info = currentRecipe.Sauce.GetInfo();
            //    if (MessageBox.Show(currentRecipe.Sauce.FriendlyName + " needs authorising before you can add recipes that use it.  Do you want to authorise it now?",
            //                        "Sauce not authorised",
            //                        MessageBoxButtons.YesNo,
            //                        MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        Debug.WriteLine("auth");
            //        currentRecipe.Sauce.BeginAuthentication();
            //    }
            //    return;
            //}

            //if (_AddRecipe(currentRecipe))
            //{
            //    _SaveRecipes();
            //    // create a new template
            //    currentRecipe = new Recipe();
            //    currentRecipe.Sauce = (EventSauce)cbProviders.SelectedItem;
            //    lbTriggers_SelectedIndexChanged(cbProviders, new EventArgs());
            //}
        }


        private bool _AddRecipe(Recipe recipe)
        {
            bool success = false;

            if (success)
            {
                // nonsense to be fixed
            }
            else
            {
                // combine the sauce, trigger and ingredients
                Debug.WriteLine("building recipe...");
                if (recipe.Build2())
                {
                    // submit it to the sauce
                    Debug.WriteLine("Submitting recipe...");
                    if (recipe.Sauce.SubmitRecipe(recipe))
                    {
                        // add it
                        Debug.WriteLine("Adding to recipe list and telling Snarl...");
                        _recipes.Add(recipe);

                        ListViewItem lvi = listView1.Items.Add(recipe.Guid, "When {" + recipe.Action() + "}", recipe.Sauce.Name);

                        //ListViewItem lvi = listView1.Items.Add(recipe.Guid, recipe.Sauce.FriendlyName, recipe.Sauce.Name);
                        //lvi.SubItems.Add(recipe.Action());

                        _RegisterRecipeWithSnarl(recipe);
                        Debug.WriteLine("recipe " + recipe.ToString() + " (" + recipe.Guid + ") added");
                        success = true;
                    }
                    else
                    {
                        string err = recipe.Sauce.LastError();
                        if (!string.IsNullOrEmpty(err))
                        {
                            err = "/r/n" + err;
                        }
                        MessageBox.Show("There was a problem submitting the recipe." + err, "Bummer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("This recipe is missing a required ingredient!",
                                    "Required ingredient missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            return success;
        }

        //private static bool _SetStyle(Control c, ControlStyles Style, bool value)
        //{
        //    bool retval = false;
        //    Type typeTB = typeof(Control);
        //    System.Reflection.MethodInfo misSetStyle = typeTB.GetMethod("SetStyle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        //    if (misSetStyle != null && c != null)
        //    { 
        //        misSetStyle.Invoke(c, new object[] { Style, value }); retval = true;
        //    }
        //    Debug.WriteLine("!!!!!!!!!!!!!" + retval);
        //    return retval;
        //}

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            contextMenuStrip1.Close();
            ListViewItem lvi = listView1.SelectedItems[0];
            if (lvi != null)
            {
                switch (e.ClickedItem.Name)
                {
                    case "mnuCookNow":
                        Recipe toCook = _recipes.Find(i => i.Guid == lvi.Name);
                        if (toCook != null)
                        {
                            toCook.RunNow();
                        }
                        else
                        {
                            Debug.WriteLine("mnuCookNow: recipe " + lvi.Name + " not in recipe list");
                        }
                        break;

                    case "mnuDelete":
                        Recipe toDelete = _recipes.Find(i => i.Guid == lvi.Name);
                        if (toDelete != null)
                        {
                            if (MessageBox.Show("Are you sure you want to delete this recipe?\n\nYou can be honest: it was the " + _GetRandomFood() + ", wasn't it?",
                                                "Recipe Desiccation Query",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            {
                                toDelete.Sauce.RemoveRecipe(toDelete);
                                listView1.Items.RemoveByKey(toDelete.Guid);
                                _recipes.Remove(_recipes.Find(i => i.Guid == toDelete.Guid));
                                _SaveRecipes();

                                if (_recipes.Count(i => i.Sauce.Name == toDelete.Sauce.Name) == 0)
                                {
                                    Debug.WriteLine("no more recipes with Sauce '" + toDelete.Sauce.Name + "'");
                                    Snarl.DoRequest("unregister?app-sig=" + SWS_SIGNATURE + "/" + toDelete.Sauce.Name);
                                }

                            }
                        }
                        else
                        {
                            Debug.WriteLine("mnuDelete: recipe " + lvi.Name + " not in recipe list");
                        }
                        break;

                    default:
                        Debug.WriteLine("@:" + e.ClickedItem.Name);
                        break;
                }
            }
            else
            {
                Debug.WriteLine("menu: couldn't retrieve selected item");
            }
        }

        private void bnAbout_Click(object sender, EventArgs e)
        {
            using (AboutBox1 about = new AboutBox1())
            {
                about.ShowDialog(this);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Recipe newRecipe;

            using (RecipeBuilder builder = new RecipeBuilder())
            {
                builder.OnRecipeCreated += MyRecipeCreatedHandler;
                builder.ShowDialog(this);
                newRecipe = builder.CompletedRecipe;
            }
           
        }

        private void MyRecipeCreatedHandler(Recipe recipe)
        {
            if (!recipe.IsValid())
            {
                MessageBox.Show("Damn... something's wrong with that recipe that we didn't pick up on.  Try creating it again and if it doesn't work this time, raise a support ticket.", 
                                "Anchovy Mismatch Error", 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Warning);
                return;
            }

            if (_AddRecipe(recipe))
                _SaveRecipes();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        private string _GetRandomFood()
        {
            List<string> food1 = new List<string>
                {
                    "spinach",
                    "celery",
                    "watercress",
                    "anchovy",
                    "raspberry",
                    "peanut butter",
                };

            List<string> food2 = new List<string>
                {
                    "duck",
                    "goose",
                    "vennison",
                    "aligator",
                    "emu",
                    "quail",
                    "wood pigeon",
                };

            List<string> food3 = new List<string>
                {
                    "pate",
                    "compote",
                    "terrine",
                    "sauce",
                    "jus",
                };

            return string.Format("{0} and {1} {2}",
                                 food1[new Random().Next(food1.Count)],
                                 food2[new Random().Next(food2.Count)],
                                 food3[new Random().Next(food3.Count)]);

        }

    }
}


//private void button3_Click(object sender, EventArgs e)
//{
//    Authorisation.clientSideFlow apnAuthProcess = new Authorisation.clientSideFlow("appdotnet", 
//                                                                                   "https://account.app.net/oauth/authenticate?client_id={0}&response_type=token&redirect_uri={1}&scope={2}&state={3}",
//                                                                                   "DGnEW7P5cL2WXpS3f7DUZ8ceeJZCT7Kz", 
//                                                                                   "http://fullphat.net/support/callback/normal.html", 
//                                                                                   "basic stream write_post follow messages files", 
//                                                                                   false);
//    //apnAuthProcess.AuthSuccess += authProcess_AuthSuccess;
//    //apnAuthProcess.showAuthWindow();
//}

//public class ComboboxItem
//{
//    public string Text { get; set; }
//    public string Value { get; set; }

//    public ComboboxItem(string text, string value)
//    {
//        this.Text = text;
//        this.Value = value;
//    }

//    public override string ToString()
//    {
//        return Text;
//    }
//}