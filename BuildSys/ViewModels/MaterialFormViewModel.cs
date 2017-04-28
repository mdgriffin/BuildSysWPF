using BuildSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BuildSys.ViewModels
{
    class MaterialFormViewModel : BaseViewModel
    {
        
        // The form for creating a new material
        public MaterialFormViewModel(BaseViewModel parent)
        {
            this.parent = parent;
            material = new MaterialModel();
            // Since we are registering, set the button to the correct text
            btnText = "Register Material";
        }

        // Form for updating a material
        public MaterialFormViewModel(BaseViewModel parent, int materalId)
        {
            this.parent = parent;

            // Get the material being updating
            material = MaterialModel.getMaterial(materalId);
            btnText = "Update Material";
        }

        // Properties accessible from the view
        public MaterialModel material { get; set; }
        public String btnText { get; set; }

        // Save material command
        public ICommand saveMaterialCommand
        {
            get
            {
                return new RelayCommand(param => saveMaterial(), param => canSaveMaterial());
            }
        }

        // Saves or updates the material
        public void saveMaterial()
        {
            // Forece validation of all properties
            material.validateAllProps();

            // If the material  does not have errors
            if (!material.HasErrors)
            {
                // Check if registering or updating
                if (btnText.Equals("Update Material"))
                {
                    // Update the Customer's record
                    material.update();

                    MessageBox.Show("Material Updated Successfully");

                    // Navigate to the same view, resetting form 
                    navigateTo(new MaterialManageViewModel(parent));
                }
                else
                {
                    // Insert the material into the database
                    material.insertMaterial();

                    MessageBox.Show("Sucessfully Register a new Material");

                    // Reset the form
                    parent.ViewModel = new MaterialFormViewModel(parent);
                }
            }
        }

        // Used to enable and disable the save button
        public Boolean canSaveMaterial()
        {
            return !material.HasErrors;
        }
    }

}
