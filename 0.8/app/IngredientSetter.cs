using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using SauceStation;

namespace TheSauceStation
{
    // lookup           - search field
    // dynamic_list     - similar to lookup but needs no input

    public partial class IngredientSetter : Form
    {
        private RecipeIngredient _theIngredient;
        private Recipe _theRecipe;

        public delegate void IngredientChangedHandler(RecipeIngredient ingredient);
        public event IngredientChangedHandler OnIngredientChanged;

        public IngredientSetter(RecipeIngredient ingredient, Recipe recipe)
        {
            InitializeComponent();
            _theIngredient = ingredient;
            _theRecipe = recipe;

        }

        private void IngredientSetter_Load(object sender, EventArgs e)
        {
            this.Text = "Set Ingredient Value...";
            lblHint.Text = "Enter the value for ingredient {" + _theIngredient.Ingredient.Name + "}";
            label1.Text = (_theIngredient.Ingredient.Hint != "" ? "" + _theIngredient.Ingredient.Hint.TrimEnd('.') + "." : "");

            //(_theIngredient.Ingredient.IsRequired ? "Required" : "Optional") +
            //tbHint.ForeColor = (_theIngredient.Ingredient.IsRequired ? Color.OrangeRed : Color.SteelBlue);
            tbRIValue.Text = _theIngredient.Value;
            cbLookup.Items.Clear();

            // set controls

            bnLookup.Visible = false;
            cbLookup.Visible = false;
            cbLookup.Width = 310;
            cbValue.Visible = false;

            switch (_theIngredient.Ingredient.Type)
            {
                case Ingredient.IngredientType.SwitchIngredient:
                    cbValue.Text = _theIngredient.Ingredient.Name;
                    cbValue.Visible = true;
                    break;

                case Ingredient.IngredientType.LookupIngredient:
                    bnLookup.Visible = true;
                    cbLookup.Visible = true;
                    break;

                case Ingredient.IngredientType.ListIngredient:
                    cbLookup.Width = tbRIValue.Width;
                    cbLookup.Visible = true;

                    foreach (KeyValuePair<string, string> kvp in _theIngredient.Ingredient.Contents)
                    {
                        cbLookup.Items.Add(new SimpleItem(kvp.Key, kvp.Value));
                    }

                    break;

                case Ingredient.IngredientType.DynamicListIngredient:
                    cbLookup.Width = tbRIValue.Width;
                    cbLookup.Visible = true;
                    bnLookup_Click(this, new EventArgs());
                    break;

            }
        }

        private void bnSetValue_Click(object sender, EventArgs e)
        {
            switch (_theIngredient.Ingredient.Type)
            {
                case Ingredient.IngredientType.SwitchIngredient:
                    _theIngredient.Value = (cbValue.Checked ? "1" : "0");
                    break;

                case Ingredient.IngredientType.DynamicListIngredient:
                case Ingredient.IngredientType.ListIngredient:
                case Ingredient.IngredientType.LookupIngredient:
                    SimpleItem si = (SimpleItem)cbLookup.SelectedItem;
                    if (si != null)
                    {
                        _theIngredient.Value = si.Key;
                        _theIngredient.Friendly = si.Text;
                    }
                    else
                    {
                        MessageBox.Show("This ingredient requires you to pick a value from the list of results displayed.", "Ingredient Mismatch Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    break;

                case Ingredient.IngredientType.StringArrayIngredient:
                case Ingredient.IngredientType.StringIngredient:
                    _theIngredient.Value = tbRIValue.Text;
                    break;

            }
            OnIngredientChanged(_theIngredient);
            this.Close();
        }

        private void bnLookup_Click(object sender, EventArgs e)
        {
            //lastLookupText = cbLookup.Text;
            cbLookup.Items.Clear();
            Results results = _theRecipe.Sauce.Translate((Sauce)_theRecipe.Trigger, _theIngredient.Ingredient, cbLookup.Text);
            if (results.Items.Count > 0)
            {
                foreach (SimpleItem item in results.Items)
                {
                    cbLookup.Items.Add(item);
                }
                cbLookup.Text = "[" + results.Items.Count + " found]";
            }
            else
            {
                cbLookup.Text = "[No results]";
            }

        }
    }
}
