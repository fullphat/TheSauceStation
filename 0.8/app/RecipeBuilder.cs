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

namespace TheSauceStation
{
    public partial class RecipeBuilder : Form
    {
        private Recipe _theRecipe = new Recipe();
        public delegate void RecipeCreatedHandler(Recipe recipe);
        public event RecipeCreatedHandler OnRecipeCreated;

        public Recipe CompletedRecipe
        {
            get
            {
                return _theRecipe;
            }
        }

        public RecipeBuilder()
        {
            InitializeComponent();
        }

        private void RecipeBuilder_Load(object sender, EventArgs e)
        {
            // populate the combo
            foreach (KeyValuePair<string, SauceSupplier> sauce in Store.Sauces)
                cbProviders.Items.Add(sauce.Value);

            if (cbProviders.Items.Count > 0)
                cbProviders.SelectedIndex = 0;

        }

        private void cbProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get the source
            SauceSupplier sauce = (SauceSupplier)cbProviders.SelectedItem;

            lblWarn.Visible = (!sauce.IsReady());
            if (lblWarn.Visible)
                lblWarn.Text = sauce.FriendlyName + " will need authorising before it can be used.";

            pbIcon.ImageLocation = Store.GetIcon(sauce.Info.SauceName);
            Debug.WriteLine("selected: " + sauce.Info.SauceName + "/" + sauce.Info.FriendlyName + " (" + sauce.Info.Description + ")");

            _theRecipe = new Recipe();
            _theRecipe.Sauce = sauce;

            // populate the list of triggers
            cbTriggers.Items.Clear();
            foreach (Sauce trigger in sauce.Info.triggers)
            {
                cbTriggers.Items.Add(trigger);
            }

            if (cbTriggers.Items.Count > 0)
                cbTriggers.SelectedIndex = 0;

        }

        private void cbTriggers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sauce selectedTrigger = (Sauce)cbTriggers.SelectedItem;
            if (selectedTrigger != null)
            {
                // set current recipe
                _theRecipe.Trigger = selectedTrigger;
                _theRecipe.Ingredients.Clear();

                // build ingredients helper list
                lbIngredients.Items.Clear();
                lbIngredients.Enabled = (selectedTrigger.Ingredients.Count > 0);

                if (lbIngredients.Enabled)
                {
                    foreach (Ingredient ingredient in selectedTrigger.Ingredients)
                    {
                        // add to the current recipe
                        RecipeIngredient ri = new RecipeIngredient();
                        ri.Ingredient = ingredient;
                        ri.Value = "";
                        _theRecipe.Ingredients.Add(ri);

                        // add to the list box
                        lbIngredients.Items.Add(ri);
                    }
                    lbIngredients.SelectedIndex = 0;
                }
                else
                {
                    lbIngredients.Items.Add("(No ingredients in this trigger)");
                }
            }
            else
            {
                Debug.WriteLine("no trigger selected");
            }
        }


        void MyAuthCompletedHandler(SauceSupplier sauce, AuthorizationWindow.AuthEventArgs eventArgs)
        {
            // called when the full OAuth process completes
            Debug.WriteLine("** OAuth for " + sauce.FriendlyName + " completed ** [Success=" + eventArgs.success + "]");
            if (eventArgs.success)
            {
                MessageBox.Show(sauce.FriendlyName + " is now authenticated!", "Authorised", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblWarn.Visible = false;
            }
            else
            {
                MessageBox.Show(sauce.FriendlyName + "failed to authenticate", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bnAddRecipe_Click(object sender, EventArgs e)
        {
            string result = "";
            if (_theRecipe.IsValid())
            {
                // is valid...
                if (_theRecipe.Sauce.IsReady())
                {
                    // is authorised...
                    if (_theRecipe.HasRequiredIngredients(out result))
                    {
                        // has required ingredients...
                        OnRecipeCreated(_theRecipe);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ingredient " + result + " is required for this recipe.\n\nTrust us, it'll taste just awful without it, the Chef will have another Turn and the inspectors will be in again.  It's just not worth it...", "Ingedient Underflow Error.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    // not authorised
                    if (MessageBox.Show(_theRecipe.Sauce.FriendlyName + " needs authorising before you can add recipes that use it.  Do you want to authorise it now?",
                                        "Sauce not authorised",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Debug.WriteLine("auth");
                        _theRecipe.Sauce.OnAuthenticationCompleted += MyAuthCompletedHandler;
                        _theRecipe.Sauce.BeginAuthentication();
                    }
                }
            }
            else
            {
                MessageBox.Show("Weirdly, either the Sauce or the Event isn't set.  This is weird because it shouldn't be able to happen.  Please report it.", "Salad Regression Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbIngredients_SelectedIndexChanged(object sender, EventArgs e)
        {

            //try
            //{
            //    RecipeIngredient ri = (RecipeIngredient)lbIngredients.SelectedItem;
            //    tbHint.Text = (ri.Ingredient.IsRequired ? "Required" : "Optional") + (ri.Ingredient.Hint != "" ? ": " + ri.Ingredient.Hint : "");
            //    tbHint.ForeColor = (ri.Ingredient.IsRequired ? Color.OrangeRed : Color.SteelBlue);
            //    tbRIValue.Text = ri.Value;
            //    cbLookup.Items.Clear();
            //    bnLookup.Visible = (ri.Ingredient.Type == "lookup");
            //    cbLookup.Visible = (ri.Ingredient.Type == "lookup");
            //}
            //catch
            //{
            //    tbHint.Text = "";
            //    tbRIValue.Text = "";
            //    bnLookup.Visible = false;
            //    cbLookup.Visible = false;
            //}
        }

        private void lbIngredients_DoubleClick(object sender, EventArgs e)
        {
            RecipeIngredient ri = (RecipeIngredient)lbIngredients.SelectedItem;
            using (IngredientSetter setter = new IngredientSetter(ri, _theRecipe))
            {
                setter.OnIngredientChanged += MyIngredientChangedHandler;
                setter.ShowDialog(this);
            }
        }

        private void MyIngredientChangedHandler(RecipeIngredient ingredient)
        {
            lbIngredients.DisplayMember = "";
            lbIngredients.DisplayMember = "foo";
        }

        private void pbIcon_Click(object sender, EventArgs e)
        {
            SauceSupplier supplier = (SauceSupplier)cbProviders.SelectedItem;
            if (supplier != null)
            {
                supplier.BeginAuthentication();
            }

        }
    }
}
